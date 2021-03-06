﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Runtime.Session;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PanelMasterMVC5Separate.Brokers;
using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.Insurer;
using PanelMasterMVC5Separate.Job.Dto;
using PanelMasterMVC5Separate.Quotings;
using PanelMasterMVC5Separate.Tenants.Quotes.Dto;
using PanelMasterMVC5Separate.Vehicle;
using PanelMasterMVC5Separate.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Tenants.Quotes
{
    public class QuoteAppService : PanelMasterMVC5SeparateAppServiceBase, IQuoteAppService
    {
        private readonly IRepository<QLocation> _qlocationrepository;
        private readonly IRepository<QAction> _qactionrepository;
        private readonly IRepository<QuoteDetails> _quotedetailsrepository;
        private readonly IRepository<QuoteStatus> _qstatusrepository;
        private readonly IRepository<QuoteCategories> _quotecatrepository;
        private readonly IRepository<RepairTypes> _repairtyperepository;
        private readonly IRepository<QuoteMaster> _quotemasterrepository;
        private readonly IRepository<Jobs> _jobsrrepository;
        private readonly IRepository<BrokerMaster> _brokerrrepository;
        private readonly IRepository<InsurerMaster> _insurerrrepository;
        private readonly IRepository<VehicleMake> _makerepository;
        private readonly IRepository<VehicleModels> _modelepository;
        private readonly IRepository<BrVehicle> _vehiclerrepository;
        private readonly IRepository<PaintTypes> _painttypesrepository;
        private readonly IAbpSession _abpSession;
        private readonly IAppFolders _appFolders;

        public QuoteAppService(IAbpSession abpSession, IAppFolders appFolders, IRepository<QuoteStatus> qstatusrepository,
            IRepository<QuoteDetails> quotedetailsrepository, IRepository<QuoteCategories> quotecatrepository,
         IRepository<BrVehicle> vehiclerrepository, IRepository<RepairTypes> repairtyperepository, IRepository<QuoteMaster> quotemasterrepository, IRepository<Jobs> jobsrrepository
            , IRepository<InsurerMaster> insurerrrepository, IRepository<BrokerMaster> brokerrrepository, IRepository<VehicleMake> makerepository,
            IRepository<VehicleModels> modelepository, IRepository<PaintTypes> painttypesrepository, IRepository<QLocation> qlocationrepository
            , IRepository<QAction> qactionrepository)
        {

            _quotedetailsrepository = quotedetailsrepository; _abpSession = abpSession; _appFolders = appFolders; _qstatusrepository = qstatusrepository; _quotecatrepository = quotecatrepository;
            _repairtyperepository = repairtyperepository; _quotemasterrepository = quotemasterrepository; _jobsrrepository = jobsrrepository;
            _insurerrrepository = insurerrrepository; _brokerrrepository = brokerrrepository; _makerepository = makerepository;
            _modelepository = modelepository; _vehiclerrepository = vehiclerrepository; _painttypesrepository = painttypesrepository;
            _qactionrepository = qactionrepository; _qlocationrepository = qlocationrepository;
        }
        public ListResultDto<PaintTypesDto> GetPaintType()
        {
            var paints = _painttypesrepository
                .GetAll()
                .OrderBy(p => p.PaintType)
                .ToList();

            return new ListResultDto<PaintTypesDto>(ObjectMapper.Map<List<PaintTypesDto>>(paints));
        }

        public ListResultDto<QuoteMastersDto> ViewQuotations(GetQuoteInput input)
        {
            var JobMaster = _jobsrrepository.GetAll().Where(c => c.TenantID == _abpSession.TenantId && c.IsDeleted == false).ToList();

            var VehicleMaster = _vehiclerrepository.GetAll().Where(c => c.TenantId == _abpSession.TenantId && c.IsDeleted == false).ToList();

            var quotestatus = _qstatusrepository.GetAll().Where(x => x.Enabled == true && x.IsDeleted == false).ToList();

            var quotecategories = _quotecatrepository.GetAll().Where(x => x.Enabled == true && x.IsDeleted == false).ToList();

            var repairtypes = _repairtyperepository.GetAll().Where(x => x.Enabled == true && x.IsDeleted == false).ToList();

            var qmaster = _quotemasterrepository.GetAll().Where(c => c.TenantId == _abpSession.TenantId && c.IsDeleted == false).ToList();

            var user = UserManager.Users.ToList();

            var quoteMaster = (from y1 in qmaster
                               join s in quotestatus on y1.QuoteStatusID equals s.Id
                               join c in quotecategories on y1.QuoteCatID equals c.Id
                               join r in repairtypes on y1.RepairTypeId equals r.Id
                               join u in user on (y1.LastModifierUserId != null ? y1.LastModifierUserId : y1.CreatorUserId) equals u.Id
                               select new
                               {
                                   y1.Id,
                                   y1.JobId,
                                   y1.Value,
                                   y1.CreationTime,
                                   QuoteCat = c.Description,
                                   RepairType = r.Description,
                                   QuoteStatus = s.Description,
                                   y1.Pre_Auth,
                                   u.Surname,
                                   u.Name
                               }).ToList();

            var finalQuery = (from v in JobMaster
                              join master in quoteMaster on v.Id equals master.JobId
                              //into pp from y1 in pp.DefaultIfEmpty()

                              select new QuoteMastersDto
                              {
                                  JobId = v.Id,
                                  Id = master == null ? 0 : master.Id,
                                  Job = _vehiclerrepository.FirstOrDefault(p => p.Id == v.VehicleID).RegistrationNumber,
                                  Value = master == null ? "" : master.Value,
                                  CreationTime = master == null ? DateTime.MinValue : master.CreationTime,
                                  QuoteCat = master == null ? "" : master.QuoteCat,
                                  RepairType = master == null ? "" : master.RepairType,
                                  QuoteStatus = master == null ? "" : master.QuoteStatus,
                                  Pre_Auth = master == null ? false : master.Pre_Auth,
                                  LastModifierUser = master == null ? "" : master.Surname + " " + master.Name,
                              }).WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                              u =>
                              u.Job.Contains(input.Filter) ||
                              u.QuoteCat.Contains(input.Filter) ||
                              u.RepairType.Contains(input.Filter) ||
                              u.QuoteStatus.Contains(input.Filter)
                              ).OrderByDescending(p=>p.CreationTime).ToList();

            return new ListResultDto<QuoteMastersDto>(ObjectMapper.Map<List<QuoteMastersDto>>(finalQuery));
        }
        // Get Quote type for Create or Edit 
        public async Task<QuoteMasterDto> QuoteDetailToEdit(GetJobInput input)
        {
            var output = new QuoteMasterDto();
            var quote = _quotemasterrepository.FirstOrDefault(p => p.Id == input.id);
            if (quote != null)
            {
                output = quote.MapTo<QuoteMasterDto>();
                output.JobId = quote.JobId;
            }
            else
                output.JobId = input.id; // jobId

            var VehicleID = _jobsrrepository.FirstOrDefault(p => p.Id == quote.JobId);
            if (VehicleID != null)
            {
                var vehicle = await _vehiclerrepository.GetAsync(VehicleID.VehicleID);
                if (vehicle != null)
                {
                    output.RegNo = vehicle.RegistrationNumber;
                    output.IsSpecialisedType = vehicle.IsSpecialisedType;
                    output.IsLuxury = vehicle.IsLuxury;
                    output.UnderWaranty = VehicleID.UnderWaranty;
                    output.PaintTypeId = vehicle.PaintTypeId;
                    output.vehicleId = VehicleID.VehicleID;
                    //output = vehicle.MapTo<QuoteMasterDto>();
                }
            }
            return output;
        }
        // Get Quote type for Create or Edit 
        public async Task<QuoteMasterDto> GetQuoteForNewQuotation(GetJobInput input)
        {
            var VehicleID = _jobsrrepository.FirstOrDefault(p => p.Id == input.id);
            var vehicle = await _vehiclerrepository.GetAsync(VehicleID.VehicleID);
            var output = new QuoteMasterDto();
            output.JobId = input.id;
            output.vehicleId = VehicleID.VehicleID;
            output.RegNo = vehicle.RegistrationNumber;
            output.IsSpecialisedType = vehicle.IsSpecialisedType;
            output.IsLuxury = vehicle.IsLuxury;
            output.UnderWaranty = VehicleID.UnderWaranty;
            output.PaintTypeId = vehicle.PaintTypeId;
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

        public int CreateOrUpdateQuotation(QuoteMasterToDto vehicle)
        {
            try
            {
                var output = _vehiclerrepository.Get(vehicle.vehicleId);
                output.IsSpecialisedType = vehicle.IsSpecialisedType;
                output.IsLuxury = vehicle.IsLuxury;
                //output.UnderWaranty = vehicle.UnderWaranty;
                output.PaintTypeId = vehicle.PaintTypeId;
                _vehiclerrepository.InsertOrUpdate(output);

                var update_job = _jobsrrepository.Get(vehicle.JobId);
                update_job.UnderWaranty = vehicle.UnderWaranty;
                _jobsrrepository.InsertOrUpdate(update_job);

                vehicle.QuoteStatusID = 1; // Default Status : Quote Preparation                          
                var query = vehicle.MapTo<QuoteMaster>();
                return _quotemasterrepository.InsertOrUpdateAndGetId(query);
            }
            catch
            {
                return 0;
            }
        }

        public QuoteSummaryDto GetQuoteJobSummary(GetQuoteInput input)
        {
            int Id = Convert.ToInt32(input.Filter);

            var qmaster = _quotemasterrepository.GetAll().Where(c => c.Id == Id).FirstOrDefault();

            var JobMaster = _jobsrrepository.GetAll().Where(c => c.Id == qmaster.JobId).FirstOrDefault();

            var VehicleMaster = _vehiclerrepository.GetAll().Where(c => c.Id == JobMaster.VehicleID).FirstOrDefault();

            var InsurerName = _insurerrrepository.GetAll().Where(c => c.Id == JobMaster.InsuranceID).FirstOrDefault().InsurerName;

            var BrokerName = _brokerrrepository.GetAll().Where(c => c.Id == JobMaster.BrokerID).FirstOrDefault().BrokerName;

            var quotestatus = _qstatusrepository.GetAll().Where(c => c.Id == qmaster.QuoteStatusID).FirstOrDefault().Description;

            var quotecategories = _quotecatrepository.GetAll().Where(c => c.Id == qmaster.QuoteCatID).FirstOrDefault().Description;

            var repairtypes = _repairtyperepository.GetAll().Where(c => c.Id == qmaster.RepairTypeId).FirstOrDefault().Description;

            var model = _modelepository.GetAll().Where(c => c.Id == VehicleMaster.ModelId).FirstOrDefault();

            var make = _makerepository.GetAll().Where(c => c.Id == model.VehicleMakeID).FirstOrDefault().Description;

            var createdBy = UserManager.Users.FirstOrDefault(c => c.Id == qmaster.CreatorUserId).UserName;

            var sdtos = new QuoteSummaryDto();
            sdtos.JobId = JobMaster.Id;
            sdtos.QuoteCat = quotecategories;
            sdtos.QuoteStatus = quotestatus;
            sdtos.QuoteCreated = qmaster.CreationTime.ToShortDateString();
            sdtos.Value = qmaster.Value;
            sdtos.VehicleYear = VehicleMaster.Year;
            sdtos.VehicleColor = VehicleMaster.Color;
            sdtos.VehicleReg = VehicleMaster.RegistrationNumber;
            sdtos.VehicleVin = VehicleMaster.VinNumber;
            sdtos.VehicleCreatedBy = JobMaster.CreationTime.ToShortDateString();
            sdtos.RepairType = repairtypes;
            sdtos.Pre_Auth = qmaster.Pre_Auth ? "Yes" : "No";
            sdtos.VehicleMake = make;
            sdtos.VehicleModal = model.Model;
            sdtos.Broker = BrokerName;
            sdtos.Insurer = InsurerName;
            sdtos.Id = Id;
            sdtos.CreatedBy = createdBy;
            return sdtos.MapTo<QuoteSummaryDto>();
        }

        public ListResultDto<QuoteDetailDto> GetQuotes(GetQuoteInput input)
        {
            int Id = Convert.ToInt32(input.Filter);

            var quotes = _quotedetailsrepository.GetAll().Where(p => p.QuoteId == Id && p.tenantid == _abpSession.TenantId && p.IsDeleted.Equals(false) && p.QuoteStatusId == input.StatusId).ToList();

            if (quotes.Count == 0)
            {
                var aa = new QuoteDetails();
                aa.QuoteStatusId = 0;
                aa.IsCompleted = false;
                aa.QuoteId = Id;
                aa.QLocation = "";
                aa.QAction = "";
                aa.Description = "";
                aa.Part = "";
                quotes = new List<QuoteDetails>();
                quotes.Add(aa);
            }
            var finalquery = new ListResultDto<QuoteDetailDto>(ObjectMapper.Map<List<QuoteDetailDto>>(quotes));
            var EstimatedValues = _quotemasterrepository.FirstOrDefault(p => p.Id == Id && p.TenantId == _abpSession.TenantId);

            if (EstimatedValues != null)
            {
                finalquery.Items[0].RepairerEstimatedDays = EstimatedValues.RepairerEstimatedDays;
            }
            return finalquery;

        }

        public string GetHeaders()
        {

            StringBuilder xmlfile = new StringBuilder();
            xmlfile.Append("[{ name: \"Ref#\", datatype: \"integer\", editable: false },");//0
            xmlfile.Append("{ name: \"action\", datatype: \"string\", editable: true },");//1
            xmlfile.Append("{ name: \"location\", datatype: \"string\", editable: true },");//2

            xmlfile.Append("{ name: \"description\", datatype: \"string\", editable: true },");//3
            xmlfile.Append("{ name: \"towork\", datatype: \"boolean\", editable: true },");//4
            xmlfile.Append("{ name: \"outwork\", datatype: \"boolean\", editable: true },");//5

            xmlfile.Append("{ name: \"quantity\", datatype: \"integer\", editable: true },");//6
            xmlfile.Append("{ name: \"price\", datatype: \"double(2)\", editable: true },");//7
            xmlfile.Append("{ name: \"total\", datatype: \"double(2)\", editable: false },");//8
            xmlfile.Append("{ name: \"part\", datatype: \"string\", editable: true },");//9

            xmlfile.Append("{ name: \"pbhrs\", datatype: \"double(2)\", editable: true },");//10
            xmlfile.Append("{ name: \"pbrate\", datatype: \"double(2)\", editable: true },");//11
            xmlfile.Append("{ name: \"pbvalue\", datatype: \"double(2)\", editable: false },");//12

            xmlfile.Append("{ name: \"phrs\", datatype: \"double(2)\", editable: true },");//13
            xmlfile.Append("{ name: \"prate\", datatype: \"double(2)\", editable: true },");//14
            xmlfile.Append("{ name: \"pvalue\", datatype: \"double(2)\", editable: false },");//15

            xmlfile.Append("{ name: \"sahrs\", datatype: \"double(2)\", editable: true },");//16
            xmlfile.Append("{ name: \"sarate\", datatype: \"double(2)\", editable: true },");//17
            xmlfile.Append("{ name: \"savalue\", datatype: \"double(2)\", editable: false },");//18

            xmlfile.Append("{ name: \"notaxvat\", datatype: \"html\", editable: false },");//19
            xmlfile.Append("{ name: \"gtotal\", datatype: \"double(2)\", editable: false },");//20

            //  xmlfile.Append("{ name: \"photo\", datatype: \"html\", hidden: false },");
            xmlfile.Append("{ name: \"copydelete\", datatype: \"html\", editable: false }]");




            return xmlfile.ToString();
        }

        public string GetLocations()
        {
            var actions = _qlocationrepository
                .GetAll()
                .OrderBy(p => p.Location)
                .ToList();
            int i = 1;
            string strJson = "[";
            foreach (var a in actions)
            {
                strJson += "{id:" + i + " , name:'" + a.Location + "'},";
                i++;
            }
            strJson = strJson.Trim(',');
            strJson += "]";

            return strJson;
        }

        public string GetActions()
        {
            var actions = _qactionrepository
                .GetAll()
                // .Where(p => p.tblqparttypeId == Id)
                .OrderBy(p => p.Action)
                .ToList();

            int i = 1;
            string strJson = "[";
            foreach (var a in actions)
            {
                strJson += "{id:" + i + " , name:'" + a.Action + "'},";
                i++;
            }
            strJson = strJson.Trim(',');
            strJson += "]";

            return strJson;
        }

        public void SaveQuote(string quote)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<QuoteObject>(quote).quote.ToList();
                //var result = (List<QuoteDetailDto>)JsonConvert.DeserializeObject(quote, typeof(List<QuoteDetailDto>));

                int QuoteId = result[0].QuoteId;

                //var ifany = _quotedetailsrepository.GetAll().Where(p => p.QuoteId == QuoteId).ToList();
                //if (ifany.Count > 0)
                //{
                //    foreach (QuoteDetails q in ifany)
                //    {
                //        q.IsCurrent = false;
                //        q.QuoteId = QuoteId;
                //        _quotedetailsrepository.Update(q);
                //    }
                //}              

                var finalresults = (from f in result
                                    select new QuoteDetails()
                                    {
                                        Id = f.Id,
                                        tenantid = _abpSession.TenantId,
                                        QuoteStatusId = f.quoteStatusId,
                                        IsCurrent = true,
                                        Description = f.Description,
                                        QuoteId = f.QuoteId,
                                        QAction = f.QAction,
                                        QLocation = f.QLocation,
                                        ToOrder = f.ToOrder,
                                        Outwork = f.Outwork,
                                        PartQty = f.PartQty,
                                        PartPrice = f.PartPrice,
                                        Part = f.Part,
                                        PanelHrs = f.PanelHrs,
                                        PanelRate = f.PanelRate,
                                        PaintHrs = f.PaintHrs,
                                        PaintRate = f.PaintRate,
                                        SAHrs = f.SAHrs,
                                        SARate = f.SARate,
                                        NoTaxVat = f.NoTaxVat,
                                        IsCompleted = false,
                                    }).ToList();
                int statusId = 0;
                foreach (QuoteDetails q in finalresults)
                {
                    if (q.QuoteStatusId == 2)// move to revision
                    {
                        statusId = 3;
                        q.QuoteStatusId = 3;
                        _quotedetailsrepository.Insert(q);
                    }
                    else
                    {
                        statusId = q.QuoteStatusId;
                        _quotedetailsrepository.InsertOrUpdate(q);
                    }
                }

                var qmaster = _quotemasterrepository.FirstOrDefault(p => p.Id == QuoteId);
                qmaster.TotalQuotedValue = result[0].TotalQuotedValue;
                qmaster.EstimatedRepairDays = result[0].EstimatedRepairDays;
                qmaster.RepairerEstimatedDays = result[0].RepairerEstimatedDays;
                qmaster.QuoteStatusID = statusId;
                _quotemasterrepository.Update(qmaster);
            }
            catch
            {

            }
        }
        public void DeleteQuote(GetQuoteInput input)
        {
            int Id = Convert.ToInt32(input.Filter);
            _quotedetailsrepository.Delete(Id);
        }
        public void CompleteQuote(GetQuoteInput input)
        {
            int Id = Convert.ToInt32(input.Filter);
            var Tonewrow = _quotedetailsrepository.GetAll().Where(p => p.QuoteId == Id && p.IsDeleted == false && p.IsCompleted == false && p.QuoteStatusId == 1).ToList();
            foreach (var qmaster in Tonewrow)
            {
                qmaster.IsCompleted = true;
                qmaster.QuoteStatusId = 1;
                qmaster.QuoteId = Id;
                _quotedetailsrepository.Update(qmaster);
            }
        }
        public void CompleteQuoteCallBack(GetQuoteInput input)
        {
            int Id = Convert.ToInt32(input.Filter);
            var Tonewrow = _quotedetailsrepository.GetAll().Where(p => p.QuoteId == Id && p.IsDeleted == false && p.IsCompleted == true && p.QuoteStatusId == 1).ToList();
            foreach (var qmaster in Tonewrow)
            {
                qmaster.IsCompleted = false;
                qmaster.QuoteStatusId = input.StatusId;
                _quotedetailsrepository.Insert(qmaster);
            }
            var master = _quotemasterrepository.FirstOrDefault(p => p.Id == Id);
            master.QuoteStatusID = input.StatusId;
            _quotemasterrepository.Update(master);
        }
        public bool IsQuotePreparationCompleted(GetQuoteInput input)
        {
            int Id = Convert.ToInt32(input.Filter);
            return _quotedetailsrepository.FirstOrDefault(p => p.QuoteId == Id && p.IsDeleted == false && p.QuoteStatusId == input.StatusId).IsCompleted;
        }
        public int CurrentQuoteStatusId(GetQuoteInput input)
        {
            int Id = Convert.ToInt32(input.Filter);
            return _quotemasterrepository.FirstOrDefault(p => p.Id == Id).QuoteStatusID;
        }

        public void CompleteItems(string quote)
        {
            try
            {


                var result = JsonConvert.DeserializeObject<QuoteObject>(quote).quote.ToList();

                int QuoteId = result[0].QuoteId;
                var quotes = _quotedetailsrepository.GetAll()
                .Where(p => p.QuoteId == QuoteId && p.QuoteStatusId == 1 && p.tenantid == _abpSession.TenantId && p.IsDeleted.Equals(false)).ToList();

                foreach (var a in quotes)
                {
                    a.IsCompleted = true;
                    _quotedetailsrepository.Update(a);
                }

                var finalresults = (from f in result
                                    select new QuoteDetails()
                                    {
                                        Id = f.Id,
                                        tenantid = _abpSession.TenantId,
                                        QuoteStatusId = f.quoteStatusId,
                                        IsCurrent = true,
                                        Description = f.Description,
                                        QuoteId = f.QuoteId,
                                        QAction = f.QAction,
                                        QLocation = f.QLocation,
                                        ToOrder = f.ToOrder,
                                        Outwork = f.Outwork,
                                        PartQty = f.PartQty,
                                        PartPrice = f.PartPrice,
                                        Part = f.Part,
                                        PanelHrs = f.PanelHrs,
                                        PanelRate = f.PanelRate,
                                        PaintHrs = f.PaintHrs,
                                        PaintRate = f.PaintRate,
                                        SAHrs = f.SAHrs,
                                        SARate = f.SARate,
                                        NoTaxVat = f.NoTaxVat,
                                        IsCompleted = false
                                    }).ToList();
                int statusId = 0;
                foreach (QuoteDetails q in finalresults)
                {
                    statusId = 2;
                    q.QuoteStatusId = 2;
                    _quotedetailsrepository.Insert(q);
                }

                var qmaster = _quotemasterrepository.FirstOrDefault(p => p.Id == QuoteId);
                qmaster.TotalQuotedValue = result[0].TotalQuotedValue;
                qmaster.EstimatedRepairDays = result[0].EstimatedRepairDays;
                qmaster.RepairerEstimatedDays = result[0].RepairerEstimatedDays;
                qmaster.QuoteStatusID = statusId;
                _quotemasterrepository.Update(qmaster);

            }
            catch (Exception x)
            {
                throw x;
            }
        }
    }
}
