using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IO;
using Abp.Runtime.Session;
using Abp.UI;
using PanelMasterMVC5Separate.Brokers;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.MultiTenancy;
using PanelMasterMVC5Separate.Tenants.Brokers.Dto;
using PanelMasterMVC5Separate.Tenants.Brokers.Exporting;
using PanelMasterMVC5Separate.Tenants.Vendors.Dto;
using PanelMasterMVC5Separate.Vendors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Tenants.Brokers
{
    public class BrokerAppService : PanelMasterMVC5SeparateAppServiceBase, IBrokerAppService
    {
        private readonly IRepository<BrokerMaster> _BrokersRepository;
        private readonly IRepository<BrokerSubMaster> _BrokerSubMasterRepository;
        private readonly IRepository<Banks> _bankRepository;
        private readonly IRepository<CountryandCurrency> _currRepository;        
        private readonly IAppFolders _appFolders;
        private readonly IRepository<BrokerMasterPics, int> _binaryObjectRepository;
        private readonly IBrokerExporter _BrokerListExcelExporter;
        private readonly IAbpSession _abpSession;
        private readonly IRepository<TenantPlanBillingDetails> _TenantPlanBillingDetails;
        private readonly IRepository<Countries> _countryRepository;
        public BrokerAppService(IAbpSession abpSession, IAppFolders appFolders,
            IBrokerExporter BrokerListExcelExporter,
            IRepository<BrokerMasterPics, int> binaryObjectRepository,
            IRepository<BrokerSubMaster> BrokerSubMasterrepositry,
            IRepository<BrokerMaster> BrokersRepository,
            IRepository<Banks> BankRepository,
            IRepository<CountryandCurrency> CurrenciesRepository,
            IRepository<BrokerMasterPics> BrokerMasterPicsRepository,
            IRepository<TenantPlanBillingDetails> tenantplanbillingdetails,
            IRepository<Countries> countryRepository)
        {
            _abpSession = abpSession;
            _binaryObjectRepository = binaryObjectRepository;
            _appFolders = appFolders;
            _BrokersRepository = BrokersRepository;
            _BrokerSubMasterRepository = BrokerSubMasterrepositry;
            _bankRepository = BankRepository;
            _currRepository = CurrenciesRepository;
            _BrokerListExcelExporter = BrokerListExcelExporter;
            _TenantPlanBillingDetails = tenantplanbillingdetails;
            _countryRepository = countryRepository;
        }

        public ListResultDto<BankDto> GetBanks()
        {
            var banks = _bankRepository
                .GetAll()
                .OrderBy(p => p.BankName)
                .ToList();

            return new ListResultDto<BankDto>(ObjectMapper.Map<List<BankDto>>(banks));
        }

        public ListResultDto<CurrencyDto> GetCurrencies()
        {
            var banks = _currRepository
                .GetAll()
                .OrderBy(p => p.CurrencyCode)
                .ToList();

            return new ListResultDto<CurrencyDto>(ObjectMapper.Map<List<CurrencyDto>>(banks));
        }

        public ListResultDto<BrokersListDto> GetBrokers(GetBrokerInput input)
        {
            var BrokerMaster = _BrokersRepository.GetAll()
             .WhereIf(
                 !input.Filter.IsNullOrWhiteSpace(),
                 u =>
                     u.BrokerName.Contains(input.Filter)
             )
             .OrderByDescending(p => p.LastModificationTime)
             .ToList();

            var Brokers = _BrokerSubMasterRepository.GetAll().Where(p => p.TenantID == _abpSession.TenantId)
             .WhereIf(
                 !input.Filter.IsNullOrWhiteSpace(),
                 u =>
                     u.ContactEmail.Contains(input.Filter) ||
                     u.ContactName.Contains(input.Filter)
             )
             .OrderByDescending(p => p.LastModificationTime)
             .ToList();


            var finalQuery = (from master in BrokerMaster
                              join v in Brokers on master.Id equals v.BrokerID into ps
                              from y1 in ps.DefaultIfEmpty()

                              select new BrokersListDto
                              {
                                  Id = master.Id,
                                  BrokerID = y1 == null ? 0 : y1.BrokerID,
                                  TenantID = y1 == null ? 0 : y1.TenantID,
                                  BrokerName = master.BrokerName,
                                  ContactName = y1 == null ? "--" : y1.ContactName,
                                  ContactEmail = y1 == null ? "--" : y1.ContactEmail,
                                  IsActive = y1 == null ? false : y1.IsActive,
                                  SubpkId = y1 == null ? 0 : y1.Id,
                              }).ToList();

            return new ListResultDto<BrokersListDto>(ObjectMapper.Map<List<BrokersListDto>>(finalQuery));
        }

        private byte[] FetchByteArray(string FileName)
        {
            var tempProfilePicturePath = Path.Combine(_appFolders.TempFileDownloadFolder, FileName);

            byte[] byteArray;

            using (var fsTempProfilePicture = new FileStream(tempProfilePicturePath, FileMode.Open))
            {
                byteArray = new byte[fsTempProfilePicture.Length];
                fsTempProfilePicture.Read(byteArray, 0, (int)fsTempProfilePicture.Length);
                fsTempProfilePicture.Close();
            }

            if (byteArray.LongLength > 102400) //100 KB
            {
                throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit"));
            }
            FileHelper.DeleteIfExists(tempProfilePicturePath);
            return byteArray;
        }

        public async Task CreateBrokerMaster(BrokersDto input)
        {
            var country = _TenantPlanBillingDetails.FirstOrDefault(x => x.TenantId == _abpSession.TenantId);
            int countryId = 250;
            if (country != null)
            {
                countryId = _countryRepository.FirstOrDefault(x => x.Code == country.BillingCountryCode).Id;

            }
            var NewBroker = new BrokerMaster(input.BrokerName, input.Mask, input.LogoPicture, input.Id, countryId);             
          
            int BrokerNewId = await _BrokersRepository.InsertAndGetIdAsync(NewBroker);

            byte[] byteArray = FetchByteArray(input.LogoPicture);

            var NewPicForBroker = new BrokerMasterPics(byteArray, BrokerNewId);

            await _binaryObjectRepository.InsertAsync(NewPicForBroker);
        }
        public async Task UpdateBrokerMaster(BrokersUDto input)
        {
            if (input.NewFileName != null)
            {
                byte[] byteArray = FetchByteArray(input.NewFileName);
                var pics = _binaryObjectRepository.GetAll().Where(c => c.BrokerID == input.Id).FirstOrDefault();
                //var NewPicForBroker = new BrokerMasterPics(byteArray, input.Id, 3);               
                pics.Bytes = byteArray;
                await _binaryObjectRepository.UpdateAsync(pics);
                input.LogoPicture = input.NewFileName;
            }
            //var NewBroker = new BrokerMaster(input.BrokerName, input.Mask, input.LogoPicture, input.Id);
            var master = _BrokersRepository.FirstOrDefault(input.Id);
            master.BrokerName = input.BrokerName;
            master.Mask = input.Mask;
            master.LogoPicture = input.LogoPicture;
            await _BrokersRepository.UpdateAsync(master);
        }
        public void CreateOrUpdateSubBroker(BrokersToListDto input)
        {
            var client = input.MapTo<BrokerSubMaster>();

            client.BrokerID = input.BrokerID;
            if (input.Id == 0)
            {
                _BrokerSubMasterRepository.Insert(client);
            }
            else
            {
                _BrokerSubMasterRepository.Update(client);
            }
        }

        public BrokersDto GetBrokerMasterDetail(GetClaimsInput input)
        {
            int Id = Convert.ToInt32(input.Filter);

            var query = _BrokersRepository
              .GetAll().Where(c => c.Id == Id)
              .FirstOrDefault();

            return query.MapTo<BrokersDto>();
        }
        public BrokersForListDto GetBrokerSubMasterDetail(GetClaimsInput input)
        {
            int Id = Convert.ToInt32(input.Filter);


            var query = _BrokerSubMasterRepository
              .GetAll().Where(c => c.BrokerID == Id && c.TenantID == _abpSession.TenantId)
              .FirstOrDefault().MapTo<BrokersForListDto>();

            var querymain = _BrokersRepository
              .GetAll().Where(c => c.Id == query.BrokerID)
              .FirstOrDefault();

            query.BrokerName = querymain.BrokerName;
            query.MaskMain = querymain.Mask;
            query.BrokerID = query.Id;

            return query;
        }
        public Task<BrokerMasterPics> GetOrNullAsync(int id)
        {
            return _binaryObjectRepository.GetAll().Where(c => c.BrokerID == id)
              .FirstOrDefaultAsync();
        }
        public async Task<FileDto> GetClaimsToExcel()
        {
            var BrokerMaster = await _BrokersRepository.GetAll().ToListAsync();
            var Brokers = await _BrokerSubMasterRepository.GetAll().Where(p => p.TenantID == _abpSession.TenantId).ToListAsync();

            var finalQuery = (from master in BrokerMaster
                              join sub in Brokers on master.Id equals sub.BrokerID into ps
                              from v in ps.DefaultIfEmpty()

                              select new BrokersListDto
                              {
                                  BrokerName = master.BrokerName,
                                  Mask = master.Mask,
                                  ContactName = v == null ? null : v.ContactName,
                                  ContactEmail = v == null ? "" : v.ContactEmail,
                                  ContactPhone = v == null ? "" : v.ContactPhone,
                                  ContactFax = v == null ? "" : v.ContactFax,
                                  Address1 = v == null ? "" : v.Address1,
                                  Address2 = v == null ? "" : v.Address2,
                                  Address3 = v == null ? "" : v.Address3,
                                  Location = v == null ? "" : v.Location,
                                  RegistrationNumber = v == null ? "" : v.RegistrationNumber,
                                  TaxRegistrationNumber = v == null ? "" : v.TaxRegistrationNumber,
                                  BrokerAccount = v == null ? "" : v.BrokerAccount,
                                  PaymentTerms = v == null ? "" : v.PaymentTerms,
                                  AccountNumber = v == null ? "" : v.AccountNumber,
                                  Type = v == null ? "" : v.Type,
                                  Branch = v == null ? "" : v.Branch,
                                  Bank = v == null ? "" : (_bankRepository.GetAll().Where(c => c.Id == v.BankID).SingleOrDefault()).BankName,
                                  Currency = v == null ? "" : (_currRepository.GetAll().Where(c => c.Id == v.CurrencyID).FirstOrDefault()).CurrencyCode,
                                  CreationTime = master.CreationTime
                              }).ToList();

            var ListDtos = finalQuery.MapTo<List<BrokersListDto>>();

            return _BrokerListExcelExporter.ExportToFile(ListDtos);
        }
        public void ChangeStatus(StatusDto input)
        {
            var query = _BrokerSubMasterRepository.GetAll().Where(c => c.Id == input.Id && c.TenantID == _abpSession.TenantId)
             .FirstOrDefault();
            query.IsActive = input.Status;
            _BrokerSubMasterRepository.Update(query);
        }
    }
}
