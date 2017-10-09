using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IO;
using Abp.UI;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Insurer;
using PanelMasterMVC5Separate.Tenants.Insurer.Dto;
using PanelMasterMVC5Separate.Tenants.Insurer.Exporting;
using PanelMasterMVC5Separate.Tenants.Vendors.Dto;
using PanelMasterMVC5Separate.Vendors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Tenants.Insurer
{
    public class InsurerAppService : PanelMasterMVC5SeparateAppServiceBase, IInsurerAppService
    {
        private readonly IRepository<InsurerMaster> _insurersRepository;
        private readonly IRepository<InsurerSub> _insurersubRepository;
        private readonly IRepository<Banks> _bankRepository;
        private readonly IRepository<Currencies> _currRepository;
        private readonly IAppFolders _appFolders;
        private readonly IRepository<InsurerPics, int> _binaryObjectRepository;
        private readonly IInsurerExporter _insurerListExcelExporter;
         

        public InsurerAppService(IAppFolders appFolders,
            IInsurerExporter insurerListExcelExporter,
            IRepository<InsurerPics, int> binaryObjectRepository,
            IRepository<InsurerSub> insurersubmasterrepositry,
            IRepository<InsurerMaster> insurersRepository,
            IRepository<Banks> BankRepository,
            IRepository<Currencies> CurrenciesRepository,
            IRepository<InsurerPics> InsurerPicsRepository)
        {
            _binaryObjectRepository = binaryObjectRepository;
            _appFolders = appFolders;
            _insurersRepository = insurersRepository;
            _insurersubRepository = insurersubmasterrepositry;
            _bankRepository = BankRepository;
            _currRepository = CurrenciesRepository;
            _insurerListExcelExporter = insurerListExcelExporter;
            
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

        public ListResultDto<InsurersListDto> GetInsurers(GetInsurerInput input)
        {
            var insurerMaster = _insurersRepository.GetAll()
             .WhereIf(
                 !input.Filter.IsNullOrWhiteSpace(),
                 u =>
                     u.InsurerName.Contains(input.Filter)
             )
             .OrderByDescending(p => p.LastModificationTime)
             .ToList();

            var insurers = _insurersubRepository.GetAll()
             .WhereIf(
                 !input.Filter.IsNullOrWhiteSpace(),
                 u =>
                     u.ContactEmail.Contains(input.Filter) ||
                     u.ContactName.Contains(input.Filter)
             )
             .OrderByDescending(p => p.LastModificationTime)
             .ToList();


            var finalQuery = (from master in insurerMaster
                              join v in insurers on master.Id equals v.InsurerID into ps
                              from y1 in ps.DefaultIfEmpty()

                              select new InsurersListDto
                              {
                                  Id = master.Id,
                                  InsurerID = y1 == null ? 0 : y1.InsurerID,
                                  TenantID = y1 == null ? 0 : y1.TenantID,
                                  InsurerName = master.InsurerName,
                                  ContactName = y1 == null ? "--" : y1.ContactName,
                                  ContactEmail = y1 == null ? "--" : y1.ContactEmail,
                                  IsActive = y1 == null ? false : y1.IsActive,
                                  SubpkId = y1 == null ? 0 : y1.Id,
                              }).ToList();

            return new ListResultDto<InsurersListDto>(ObjectMapper.Map<List<InsurersListDto>>(finalQuery));
        }

        private byte[] GetByteArray(string FileName)
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

        public async Task CreateInsurerMaster(InsurersDto input)
        {
            var Newinsurer = new InsurerMaster(input.InsurerName, input.Mask, input.LogoPicture, input.Id);
            int InsurerNewId = await _insurersRepository.InsertAndGetIdAsync(Newinsurer);

            byte[] byteArray = GetByteArray(input.LogoPicture);

            var NewPicForInsurer = new InsurerPics(byteArray, InsurerNewId);

            await _binaryObjectRepository.InsertAsync(NewPicForInsurer);
        }
        public async Task UpdateInsurerMaster(InsurersUDto input)
        {
            if (input.NewFileName != null)
            {
                byte[] byteArray = GetByteArray(input.NewFileName);
                var pics = _binaryObjectRepository.GetAll().Where(c => c.InsurerID == input.Id).FirstOrDefault();
                //var NewPicForInsurer = new InsurerPics(byteArray, input.Id, 3);               
                pics.Bytes = byteArray;
                await _binaryObjectRepository.UpdateAsync(pics);
                input.LogoPicture = input.NewFileName;
            }
            //var Newinsurer = new InsurerMaster(input.InsurerName, input.Mask, input.LogoPicture, input.Id);
            var master = _insurersRepository.FirstOrDefault(input.Id);
            master.InsurerName = input.InsurerName;
            master.Mask = input.Mask;
            master.LogoPicture = input.LogoPicture;
            await _insurersRepository.UpdateAsync(master);
        }
        public void CreateOrUpdateSubInsurer(InsurersToListDto input)
        {
            var client = input.MapTo<InsurerSub>();
             
            client.InsurerID = input.InsurerID;
            if (input.Id == 0)
            {
                _insurersubRepository.Insert(client);
            }
            else
            {
                _insurersubRepository.Update(client);
            }
        }

        public InsurersDto GetInsurerMasterDetail(GetClaimsInput input)
        {
            int Id = Convert.ToInt32(input.Filter);

            var query = _insurersRepository
              .GetAll().Where(c => c.Id == Id)
              .FirstOrDefault();

            return query.MapTo<InsurersDto>();
        }
        public InsurersForListDto GetInsurerSubDetail(GetClaimsInput input)
        {
            int Id = Convert.ToInt32(input.Filter);


            var query = _insurersubRepository
              .GetAll().Where(c => c.InsurerID == Id)
              .FirstOrDefault().MapTo<InsurersForListDto>();

            var querymain = _insurersRepository
              .GetAll().Where(c => c.Id == query.InsurerID)
              .FirstOrDefault();

            query.InsurerName = querymain.InsurerName;
            query.MaskMain = querymain.Mask;
            //query.InsurerID = query.Id;

            return query;
        }
        public Task<InsurerPics> GetOrNullAsync(int id)
        {
            return _binaryObjectRepository.GetAll().Where(c => c.InsurerID == id)
              .FirstOrDefaultAsync();
        }
        public async Task<FileDto> GetClaimsToExcel()
        {
            var insurerMaster = await _insurersRepository.GetAll().ToListAsync();
            var insurers = await _insurersubRepository.GetAll().ToListAsync();

            var finalQuery = (from master in insurerMaster
                              join sub in insurers on master.Id equals sub.InsurerID into ps
                              from v in ps.DefaultIfEmpty()

                              select new InsurersListDto
                              {
                                  InsurerName = master.InsurerName,
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
                                  InsurerAccount = v == null ? "" : v.InsurerAccount,
                                  PaymentTerms = v == null ? "" : v.PaymentTerms,
                                  AccountNumber = v == null ? "" : v.AccountNumber,
                                  Type = v == null ? "" : v.Type,
                                  Branch = v == null ? "" : v.Branch,
                                  Bank = v == null ? "" : (_bankRepository.GetAll().Where(c => c.Id == v.BankID).SingleOrDefault()).BankName,
                                  Currency = v == null ? "" : (_currRepository.GetAll().Where(c => c.Id == v.CurrencyID).FirstOrDefault()).CurrencyCode,
                                  CreationTime = master.CreationTime
                              }).ToList();

            var ListDtos = finalQuery.MapTo<List<InsurersListDto>>();

            return _insurerListExcelExporter.ExportToFile(ListDtos);
        }
        public void ChangeStatus(StatusDto input)
        { 
            var query = _insurersubRepository.Get(input.Id);
            query.IsActive = input.Status;            
            _insurersubRepository.Update(query);
        }
    }
}
