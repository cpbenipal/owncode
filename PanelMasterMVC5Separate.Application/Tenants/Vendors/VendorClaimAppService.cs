using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Runtime.Session;
using PanelMasterMVC5Separate.Authorization;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.MultiTenancy;
using PanelMasterMVC5Separate.Tenants.Vendors.Dto;
using PanelMasterMVC5Separate.Tenants.Vendors.Exporting;
using PanelMasterMVC5Separate.Vendors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Tenants.Vendors
{
    public class VendorClaimAppService : PanelMasterMVC5SeparateAppServiceBase, IVendorClaimAppService
    {

        private readonly IRepository<VendorSub> _vendorSubRepository;
        private readonly IRepository<VendorMain> _vendorMainRepository;
        private readonly IRepository<Banks> _bankRepository;
        private readonly IRepository<CountryandCurrency> _currRepository;
        private readonly IAbpSession _abpSession;
        private readonly IVendorExporter _vendorListExcelExporter;
        private readonly IRepository<TenantProfile> _TenantProfile;
        private readonly IRepository<Countries> _countryRepository;
        public VendorClaimAppService(IAbpSession abpSession,
            IVendorExporter vendorListExcelExporter,
            IRepository<Banks> BankRepository,
            IRepository<CountryandCurrency> CurrenciesRepository, IRepository<VendorMain> vendorMainRepository,
            IRepository<VendorSub> vendorSubRepository,
            IRepository<TenantProfile> tenantprofile,
            IRepository<Countries> countryRepository)
        {
            _abpSession = abpSession;
            _bankRepository = BankRepository;
            _currRepository = CurrenciesRepository;
            _vendorMainRepository = vendorMainRepository;
            _vendorSubRepository = vendorSubRepository;
            _TenantProfile = tenantprofile;
            _vendorListExcelExporter = vendorListExcelExporter;
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

        public async Task<FileDto> GetClaimsToExcel()
        {
            int countryId = GetCountryIdByCode();
            var query = await _vendorMainRepository.GetAll().Where(p => p.CountryID.Equals(countryId))
             .OrderByDescending(p => p.LastModificationTime)
             .ToListAsync();

            var sub_query = await _vendorSubRepository.GetAll()
                            .Where(sv => sv.TenantId == _abpSession.TenantId)
                            .ToListAsync();


            var finalQuery = (from master in query
                              join v in sub_query on master.Id equals v.VendorID into ps
                              from y1 in ps.DefaultIfEmpty()

                              select new GVendorsListDto
                              { 
                                  SupplierCode = master.SupplierCode,
                                  SupplierName = master.SupplierName,
                                  ContactEmail = y1 == null ? "" : y1.ContactEmail,
                                  ContactPhone = y1 == null ? "" : y1.ContactPhone,
                                  ContactFax = y1 == null ? "" : y1.ContactFax,
                                  Address1 = y1 == null ? "" : y1.Address1,
                                  Address2 = y1 == null ? "" : y1.Address2,
                                  Address3 = y1 == null ? "" : y1.Address3,
                                  Location = y1 == null ? "" : y1.Location,
                                  RegistrationNumber = y1 == null ? "" : y1.RegistrationNumber,
                                  TaxRegistrationNumber = y1 == null ? "" : y1.TaxRegistrationNumber,
                                  SupplierAccount = y1 == null ? "" : y1.SupplierAccount,
                                  PaymentTerms = y1 == null ? "" : y1.PaymentTerms,
                                  AccountNumber = y1 == null ? "" : y1.AccountNumber,
                                  Type = y1 == null ? "" : y1.Type,
                                  Branch = y1 == null ? "" : y1.Branch,
                                  Bank = y1 == null ? "" : _bankRepository.FirstOrDefault(x => x.Id == y1.BankID).BankName,
                                  Currency = y1 == null ? "" : _currRepository.FirstOrDefault(x => x.Id == y1.CurrencyID).CountryAndCurrency
                              }).ToList();

            var ListDtos = finalQuery.MapTo<List<GVendorsListDto>>();
            return _vendorListExcelExporter.ExportToFile(ListDtos);
        }

        public ListResultDto<CurrencyDto> GetCurrencies()
        {
            var banks = _currRepository
                .GetAll()
                .OrderBy(p => p.CurrencyCode)
                .ToList();

            return new ListResultDto<CurrencyDto>(ObjectMapper.Map<List<CurrencyDto>>(banks));
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_Host_SystemDefaults)]
        public ListResultDto<VendorMainListDto> GetMasterVendors(GetClaimsInput input)
        {
            var countries = _countryRepository.GetAll().AsNoTracking().ToList();

            var query = _vendorMainRepository.GetAll()
              .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.SupplierName.Equals(input.Filter)
              )
             .OrderByDescending(p => p.LastModificationTime)
             .ToList();

            var sub_query = _vendorSubRepository.GetAll().ToList();


            var finalQuery = (from master in query
                              join c in countries on master.CountryID equals c.Id
                              join v in sub_query on master.Id equals v.VendorID into ps
                              from y1 in ps.DefaultIfEmpty()

                              select new VendorMainListDto
                              {
                                  id = master.Id,
                                  SupplierCode = master.SupplierCode,
                                  SupplierName = master.SupplierName,
                                  Country = c.Country,
                                  IsActive = y1 == null ? false : y1.IsActive,
                                  HasSub = y1 == null ? false : true
                              }).ToList();


            return new ListResultDto<VendorMainListDto>(finalQuery);

        }

        public ListResultDto<GVendorsListDto> GetVendors(GetClaimsInput input)
        { 
            int countryId = GetCountryIdByCode();
            var query = _vendorMainRepository.GetAll()
              .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.SupplierName.Equals(input.Filter)
              )
              .Where(p => p.CountryID.Equals(countryId))
             .OrderByDescending(p => p.LastModificationTime)
             .ToList();

            var sub_query = _vendorSubRepository.GetAll()
                            .Where(sv => sv.TenantId == _abpSession.TenantId)
                            .ToList();


            var finalQuery = (from master in query
                              join v in sub_query on master.Id equals v.VendorID into ps
                              from y1 in ps.DefaultIfEmpty()

                              select new GVendorsListDto
                              {
                                  VendorID = master.Id,
                                  SupplierCode = master.SupplierCode,
                                  SupplierName = master.SupplierName,
                                  ContactEmail = y1 == null ? "" : y1.ContactEmail,
                                  ContactPhone = y1 == null ? "" : y1.ContactPhone,
                                  IsActive = y1 == null ? false : y1.IsActive
                              }).ToList();


            return new ListResultDto<GVendorsListDto>(finalQuery);

        }


        public void AddSubVendor(VendorSubListDto input)
        {
            if (string.IsNullOrEmpty(input.subVendorID.ToString()))
            {
                VendorSub newSubVendor = new VendorSub();
                newSubVendor.IsActive = true;
                newSubVendor.TenantId = input.TenantId;
                newSubVendor.VendorID = Convert.ToInt32(input.VendorID);
                newSubVendor.ContactName = input.ContactName;
                newSubVendor.ContactPhone = input.ContactPhone;
                newSubVendor.ContactFax = input.ContactFax;
                newSubVendor.ContactEmail = input.ContactEmail;
                newSubVendor.Address1 = input.Address1;
                newSubVendor.Address2 = input.Address2;
                newSubVendor.Address3 = input.Address3;
                newSubVendor.Location = input.Location;
                newSubVendor.SupplierAccount = input.SupplierAccount;
                newSubVendor.PaymentTerms = input.PaymentTerms;
                newSubVendor.AccountNumber = input.AccountNumber;
                newSubVendor.Type = input.Type;
                newSubVendor.Branch = input.Branch;
                newSubVendor.CurrencyID = input.CurrencyID;
                newSubVendor.BankID = input.BankID;
                newSubVendor.RegistrationNumber = input.RegistrationNumber;
                newSubVendor.TaxRegistrationNumber = input.TaxRegistrationNumber;
                _vendorSubRepository.Insert(newSubVendor);
            }
            else
            {
                input.IsActive = true;
                UpdateVendor(input);
            }
        }

        public VendorMain AddMainVendor(VendorMainListDto input)
        {
            VendorMain newVendor = new VendorMain();
            newVendor.SupplierCode = System.Guid.NewGuid();
            newVendor.SupplierName = input.SupplierName;
            newVendor.CountryID = GetCountryIdByCode();
            return _vendorMainRepository.Insert(newVendor);

        }

        public void UpdateMainVendor(VendorMainListDto input)
        {
            try
            {
                VendorMain updateMainVendor = _vendorMainRepository.GetAll().Where(s => s.Id == input.id).First();

                updateMainVendor.SupplierName = input.SupplierName;
                updateMainVendor.SupplierCode = input.SupplierCode;

                _vendorMainRepository.Update(updateMainVendor);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public VendorDto GetMainVendor(GetClaimsInput input)
        {
            int Id = Convert.ToInt32(input.Filter);
            int CountryID = GetCountryIdByCode();

            var main_query = _vendorMainRepository
              .GetAll().FirstOrDefault(c => c.Id == Id && (CountryID == 0 || c.CountryID == CountryID));

            return main_query.MapTo<VendorDto>();
        }

        public ListResultDto<VendorSubListDto> GetSubVendor(GetClaimsInput input)
        {
            int Id = Convert.ToInt32(input.Filter);            
            var sub_query = _vendorSubRepository
                .GetAll().Where(s => s.VendorID == Id && s.TenantId == _abpSession.TenantId)
                .ToList();

            var newList = new List<VendorSubListDto>();
            foreach (VendorSub vendor_obj in sub_query)
            {
                newList.Add(new VendorSubListDto
                {
                    subVendorID = vendor_obj.Id,
                    TenantId = vendor_obj.TenantId,
                    VendorID = vendor_obj.VendorID,
                    ContactName = vendor_obj.ContactName,
                    ContactPhone = vendor_obj.ContactPhone,
                    ContactFax = vendor_obj.ContactFax,
                    ContactEmail = vendor_obj.ContactEmail,
                    Address1 = vendor_obj.Address1,
                    Address2 = vendor_obj.Address2,
                    Address3 = vendor_obj.Address3,
                    Location = vendor_obj.Location,
                    SupplierAccount = vendor_obj.SupplierAccount,
                    PaymentTerms = vendor_obj.PaymentTerms,
                    AccountNumber = vendor_obj.AccountNumber,
                    Type = vendor_obj.Type,
                    Branch = vendor_obj.Branch,
                    CurrencyID = vendor_obj.CurrencyID,
                    BankID = vendor_obj.BankID,
                    TaxRegistrationNumber = vendor_obj.TaxRegistrationNumber,
                    RegistrationNumber = vendor_obj.RegistrationNumber,
                    IsActive = vendor_obj.IsActive
                });
            }

            return new ListResultDto<VendorSubListDto>(newList);
        }

        public void UpdateVendor(VendorSubListDto input)
        {
            try
            {
                VendorSub updateSubVendor = _vendorSubRepository.GetAll().Where(s => s.TenantId == input.TenantId && s.VendorID == input.VendorID).First();

                updateSubVendor.ContactName = input.ContactName;
                updateSubVendor.ContactPhone = input.ContactPhone;
                updateSubVendor.ContactFax = input.ContactFax;
                updateSubVendor.ContactEmail = input.ContactEmail;
                updateSubVendor.Address1 = input.Address1;
                updateSubVendor.Address2 = input.Address2;
                updateSubVendor.Address3 = input.Address3;
                updateSubVendor.Location = input.Location;
                updateSubVendor.SupplierAccount = input.SupplierAccount;
                updateSubVendor.PaymentTerms = input.PaymentTerms;
                updateSubVendor.AccountNumber = input.AccountNumber;
                updateSubVendor.Type = input.Type;
                updateSubVendor.Branch = input.Branch;
                updateSubVendor.CurrencyID = input.CurrencyID;
                updateSubVendor.BankID = input.BankID;
                updateSubVendor.IsActive = input.IsActive;
                updateSubVendor.TaxRegistrationNumber = input.TaxRegistrationNumber;
                updateSubVendor.RegistrationNumber = input.RegistrationNumber;
                _vendorSubRepository.Update(updateSubVendor);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void ChangeStatus(VendorSubListDto input)
        {
            int VendorID = Convert.ToInt32(input.VendorID);
            int TenantId = Convert.ToInt32(input.TenantId);

            var query = _vendorSubRepository
             .GetAll().Where(c => c.VendorID == VendorID && c.TenantId == TenantId)
             .FirstOrDefault();

            query.IsActive = input.IsActive;

            _vendorSubRepository.Update(query);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_Host_SystemDefaults)]
        public void ChangeVendorStatus(MasterStatusDto input)
        {
            var query = _vendorSubRepository
             .GetAll().Where(c => c.VendorID == input.Id)
             .FirstOrDefault();

            query.IsActive = input.Status;

            _vendorSubRepository.Update(query);
        }
        public ListResultDto<CountryDto> GetCountry()
        {
            var banks = _countryRepository
                .GetAll()
                .OrderBy(p => p.Country)
                .ToList();

            return new ListResultDto<CountryDto>(ObjectMapper.Map<List<CountryDto>>(banks));
        }
        private int GetCountryIdByCode()
        {
            var CountryCode = _TenantProfile.FirstOrDefault(x => x.TenantId == _abpSession.TenantId);
            return (CountryCode == null ? 0 : _countryRepository.FirstOrDefault(x => x.Code == CountryCode.CountryCode).Id);
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_Host_SystemDefaults)]
        public void AddUpdateVendor(VendorSaveDto input)
        {
            var vendor = input.MapTo<VendorMain>();
            if (input.Id == 0)
                vendor.SupplierCode = Guid.NewGuid();
            _vendorMainRepository.InsertOrUpdate(vendor);
        }
        // Tenant Specific
        public void AddEditVendor(VendorSaveDto input)
        {
            var vendor = input.MapTo<VendorMain>();
            vendor.CountryID = GetCountryIdByCode();
            if (input.Id == 0)
                vendor.SupplierCode = Guid.NewGuid();
            _vendorMainRepository.InsertOrUpdate(vendor);
        }
    }
}
