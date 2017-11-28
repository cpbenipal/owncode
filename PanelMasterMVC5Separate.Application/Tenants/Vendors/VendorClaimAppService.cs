using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Runtime.Session;
using AutoMapper;
using PanelMasterMVC5Separate.Authorization;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Job.Dto;
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
            var Mainvendors = await _vendorMainRepository.GetAll().ToListAsync();
            var Subvendors = _vendorSubRepository.GetAll().ToList();
            var bank = await _bankRepository.GetAll().ToListAsync();
            var currency = await _currRepository.GetAll().ToListAsync();

            var finalQuery = (from sv in Subvendors
                              select new GVendorsListDto
                              {
                                  SupplierCode = sv.VendorMains.SupplierCode
                                  /*SupplierName = mv.SupplierName,
                                  ContactName = sv.ContactName,
                                  ContactPhone = sv.ContactPhone,
                                  ContactFax = sv.ContactFax,
                                  ContactEmail = sv.ContactEmail,
                                  Address1 = sv.Address1,
                                  Address2 = sv.Address2,
                                  Address3 = sv.Address3,
                                  Location = sv.Location,
                                  RegistrationNumber = mv.RegistrationNumber,
                                  TaxRegistrationNumber = mv.TaxRegistrationNumber,
                                  SupplierAccount = sv.SupplierAccount,
                                  PaymentTerms = sv.PaymentTerms,
                                  AccountNumber = sv.AccountNumber,
                                  Type = sv.Type,
                                  Branch = sv.Branch,
                                  Bank = b.BankName,
                                  Currency = c.CurrencyCode*/

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
                    p => p.SupplierName.Equals(input.Filter) ||
                         p.RegistrationNumber.Equals(input.Filter) ||
                         p.TaxRegistrationNumber.Equals(input.Filter)
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
                                  RegistrationNumber = master.RegistrationNumber,
                                  TaxRegistrationNumber = master.TaxRegistrationNumber,
                                  Country = c.Country,
                                  IsActive = y1 == null ? false : y1.IsActive,
                                  HasSub = y1 == null ? false : true
                              }).ToList();


            return new ListResultDto<VendorMainListDto>(finalQuery);

        }

        public ListResultDto<VendorMainListDto> GetVendors(GetClaimsInput input, string tenantID)
        {
            int tenant_id = Convert.ToInt16(tenantID);
            int countryId = GetCountryIdByCode();
            var query = _vendorMainRepository.GetAll()
              .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.SupplierName.Equals(input.Filter) ||
                         p.RegistrationNumber.Equals(input.Filter) ||
                         p.TaxRegistrationNumber.Equals(input.Filter)
              )
              .Where(p => p.CountryID.Equals(countryId))
             .OrderByDescending(p => p.LastModificationTime)
             .ToList();

            var sub_query = _vendorSubRepository.GetAll()
                            .Where(sv => sv.TenantId == tenant_id)
                            .ToList();


            var finalQuery = (from master in query
                              join v in sub_query on master.Id equals v.VendorID into ps
                              from y1 in ps.DefaultIfEmpty()

                              select new VendorMainListDto
                              {
                                  id = master.Id,
                                  SupplierCode = master.SupplierCode,
                                  SupplierName = master.SupplierName,
                                  RegistrationNumber = master.RegistrationNumber,
                                  TaxRegistrationNumber = master.TaxRegistrationNumber,
                                  IsActive = y1 == null ? false : y1.IsActive
                              }).ToList();


            return new ListResultDto<VendorMainListDto>(finalQuery);

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
            newVendor.RegistrationNumber = input.RegistrationNumber;
            newVendor.TaxRegistrationNumber = input.TaxRegistrationNumber;
            newVendor.CountryID = GetCountryIdByCode();
            return _vendorMainRepository.Insert(newVendor);

        }

        public void UpdateMainVendor(VendorMainListDto input)
        {
            try
            {
                VendorMain updateMainVendor = _vendorMainRepository.GetAll().Where(s => s.Id == input.id).First();

                updateMainVendor.SupplierName = input.SupplierName;
                updateMainVendor.RegistrationNumber = input.RegistrationNumber;
                updateMainVendor.SupplierCode = input.SupplierCode;
                updateMainVendor.TaxRegistrationNumber = input.TaxRegistrationNumber;

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

        public ListResultDto<VendorSubListDto> GetSubVendor(GetClaimsInput input, string tenantID)
        {
            int Id = Convert.ToInt32(input.Filter);
            int tenant_id = Convert.ToInt16(tenantID);
            var sub_query = _vendorSubRepository
                .GetAll().Where(s => s.VendorID == Id && s.TenantId == tenant_id)
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
