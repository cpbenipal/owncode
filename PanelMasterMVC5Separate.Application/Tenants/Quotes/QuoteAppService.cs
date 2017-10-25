using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using PanelMasterMVC5Separate.Brokers;
using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.Insurer;
using PanelMasterMVC5Separate.Quotings;
using PanelMasterMVC5Separate.Tenants.Quotes.Dto;
using PanelMasterMVC5Separate.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Tenants.Quotes
{
    public class QuoteAppService : PanelMasterMVC5SeparateAppServiceBase, IQuoteAppService
    {
        private readonly IRepository<QuoteStatus> _qstatusrepository;
        private readonly IRepository<QuoteCategories> _quotecatrepository;
        private readonly IRepository<RepairTypes> _repairtyperepository;
        private readonly IRepository<QuoteMaster> _quotemasterrepository;
        private readonly IRepository<Jobs> _jobsrrepository;

        private readonly IRepository<BrokerMaster> _brokerrrepository;
        private readonly IRepository<InsurerMaster> _insurerrrepository;

        private readonly IRepository<VehicleMake> _makerepository;
        private readonly IRepository<VehicleModels> _modelepository;

        private readonly IAppFolders _appFolders;

        public QuoteAppService(IAppFolders appFolders, IRepository<QuoteStatus> qstatusrepository, IRepository<QuoteCategories> quotecatrepository,
          IRepository<RepairTypes> repairtyperepository, IRepository<QuoteMaster> quotemasterrepository, IRepository<Jobs> jobsrrepository
            , IRepository<InsurerMaster> insurerrrepository, IRepository<BrokerMaster> brokerrrepository, IRepository<VehicleMake> makerepository,
            IRepository<VehicleModels> modelepository)
        {
            _appFolders = appFolders; _qstatusrepository = qstatusrepository; _quotecatrepository = quotecatrepository;
            _repairtyperepository = repairtyperepository; _quotemasterrepository = quotemasterrepository; _jobsrrepository = jobsrrepository;
            _insurerrrepository = insurerrrepository; _brokerrrepository = brokerrrepository; _makerepository = makerepository;
            _modelepository = modelepository;
        }


        public ListResultDto<QuoteMastersDto> ViewQuotations(GetQuoteInput input)
        {
            var JobMaster = _jobsrrepository.GetAll().ToList();

            var quotestatus = _qstatusrepository.GetAll().ToList();

            var quotecategories = _quotecatrepository.GetAll().ToList();

            var repairtypes = _repairtyperepository.GetAll().ToList();

            var qmaster = _quotemasterrepository.GetAll().ToList();

            var quoteMaster = (from y1 in qmaster
                               join s in quotestatus on y1.QuoteStatusID equals s.Id
                               join c in quotecategories on y1.QuoteCatID equals c.Id
                               join r in repairtypes on y1.RepairTypeId equals r.Id
                               select new
                               {
                                   Id = y1.Id,
                                   JobId = y1.JobId,
                                   Value = y1.Value,
                                   CreationTime = y1.CreationTime,
                                   QuoteCat = c.Description,
                                   RepairType = r.Description,
                                   QuoteStatus = s.Description

                               }).ToList();

            var finalQuery = (from v in JobMaster
                              join master in quoteMaster on v.Id equals master.JobId into pp
                              from y1 in pp.DefaultIfEmpty()

                              select new QuoteMastersDto
                              {
                                  JobId = v.Id,
                                  Id = y1 == null ? 0 : y1.Id,
                                  Job = v.RegNo,
                                  Value = y1 == null ? "" : y1.Value,
                                  CreationTime = y1 == null ? DateTime.MinValue : y1.CreationTime,
                                  QuoteCat = y1 == null ? "" : y1.QuoteCat,
                                  RepairType = y1 == null ? "" : y1.RepairType,
                                  QuoteStatus = y1 == null ? "" : y1.QuoteStatus
                              }).WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                              u =>
                              u.Job.Contains(input.Filter) ||
                              u.QuoteCat.Contains(input.Filter) ||
                              u.RepairType.Contains(input.Filter) ||
                              u.QuoteStatus.Contains(input.Filter)
                              ).ToList();

            return new ListResultDto<QuoteMastersDto>(ObjectMapper.Map<List<QuoteMastersDto>>(finalQuery));
        }
        // Get Quote type for Create or Edit 
        public async Task<QuoteMasterDto> GetQuoteForNewQuotation(GetJobInput input)
        {             
            var output = new QuoteMasterDto();            
            
            if (input.id != 0)//Edit
            { 
                var quote = _quotemasterrepository.FirstOrDefault(p => p.Id == input.id);
                input.jobId = quote.JobId;
                try
                {
                    output = quote.MapTo<QuoteMasterDto>();
                }
                catch (Exception c){
                    throw c;
                }                

            }
            var job = await _jobsrrepository.GetAsync(input.jobId);
            output.RegNo = job.RegNo;
            return output;
        }

        public ListResultDto<QuoteStatusDto> GetQuoteStatus()
        {
            var status = _qstatusrepository
                .GetAll()
                .Where(p => p.IsDeleted == false && p.Enabled == true)
                .ToList();

            return new ListResultDto<QuoteStatusDto>(ObjectMapper.Map<List<QuoteStatusDto>>(status));
        }

        public ListResultDto<QuoteCategoriesDto> GetQuoteCategories()
        {
            var status = _quotecatrepository
                .GetAll()
                .Where(p => p.IsDeleted == false && p.Enabled == true)
                .ToList();

            return new ListResultDto<QuoteCategoriesDto>(ObjectMapper.Map<List<QuoteCategoriesDto>>(status));
        }

        public ListResultDto<RepairTypeDto> GetRepairTypes()
        {
            var status = _repairtyperepository
                .GetAll()
                .Where(p => p.IsDeleted == false && p.Enabled == true)
                .ToList();

            return new ListResultDto<RepairTypeDto>(ObjectMapper.Map<List<RepairTypeDto>>(status));
        }

        public int CreateOrUpdateQuotation(QuoteMasterToDto input)
        {
            input.QuoteStatusID = 1; // Default Status : Quote Preparation             
            var query = input.MapTo<QuoteMaster>();
            return _quotemasterrepository.InsertOrUpdateAndGetId(query);
        }

        public QuoteSummaryDto GetQuoteJobSummary(GetQuoteInput input)
        {
            int Id = Convert.ToInt32(input.Filter);

            var qmaster = _quotemasterrepository.GetAll().Where(c => c.Id == Id).FirstOrDefault();

            var JobMaster = _jobsrrepository.GetAll().Where(c => c.Id == qmaster.JobId).FirstOrDefault();

            var InsurerName = _insurerrrepository.GetAll().Where(c => c.Id == JobMaster.InsuranceID).FirstOrDefault().InsurerName;

            var BrokerName = _brokerrrepository.GetAll().Where(c => c.Id == JobMaster.BrokerID).FirstOrDefault().BrokerName;

            var quotestatus = _qstatusrepository.GetAll().Where(c => c.Id == qmaster.QuoteStatusID).FirstOrDefault().Description;

            var quotecategories = _quotecatrepository.GetAll().Where(c => c.Id == qmaster.QuoteCatID).FirstOrDefault().Description;

            var repairtypes = _repairtyperepository.GetAll().Where(c => c.Id == qmaster.RepairTypeId).FirstOrDefault().Description;

            var model = _modelepository.GetAll().Where(c => c.Id == JobMaster.ModelID).FirstOrDefault();

            var make = _makerepository.GetAll().Where(c => c.Id == model.VehicleMakeID).FirstOrDefault().Description;            

            var sdtos = new QuoteSummaryDto();
            sdtos.JobId = JobMaster.Id;
            sdtos.QuoteStatus = quotestatus;
            sdtos.QuoteCreated = qmaster.CreationTime.ToShortDateString();
            sdtos.Value = qmaster.Value;
            sdtos.VehicleYear = JobMaster.Year;
            sdtos.VehicleColor = JobMaster.Colour;
            sdtos.VehicleReg = JobMaster.RegNo;
            sdtos.VehicleVin = JobMaster.VinNumber;
            sdtos.VehicleCreatedBy = JobMaster.CreationTime.ToShortDateString();
            sdtos.RepairType = repairtypes;
            sdtos.Pre_Auth = qmaster.Pre_Auth ? "Yes" : "No";
            sdtos.VehicleMake = make;
            sdtos.VehicleModal = model.Model;
            sdtos.Broker = BrokerName;
            sdtos.Insurer = InsurerName;
            sdtos.Id = Id;

            return sdtos.MapTo<QuoteSummaryDto>();
        }
    }
}
