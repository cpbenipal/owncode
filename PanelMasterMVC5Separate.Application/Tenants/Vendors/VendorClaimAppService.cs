using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using PanelMasterMVC5Separate.Dto;
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
        private readonly IRepository<Vendor> _vendorsRepository;
        private readonly IRepository<Banks> _bankRepository;
        private readonly IRepository<Currencies> _currRepository;

        private readonly IVendorExporter _vendorListExcelExporter;

        public VendorClaimAppService(
            IVendorExporter vendorListExcelExporter,
            IRepository<Vendor> VendorsRepository, IRepository<Banks> BankRepository, IRepository<Currencies> CurrenciesRepository)
        {
            _vendorsRepository = VendorsRepository;
            _bankRepository = BankRepository;
            _currRepository = CurrenciesRepository;

            _vendorListExcelExporter = vendorListExcelExporter;
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
            var vendors = await _vendorsRepository.GetAll().ToListAsync();
            var bank = await _bankRepository.GetAll().ToListAsync();
            var currency = await _currRepository.GetAll().ToListAsync();

            var finalQuery = (from v in vendors
                              join b in bank on v.BankID equals b.Id
                              join c in currency on v.CurrencyID equals c.Id
                              select new VendorsListDto
                              {
                                  SupplierCode = v.SupplierCode,
                                  SupplierName = v.SupplierName,
                                  ContactName = v.ContactName,
                                  ContactPhone = v.ContactPhone,
                                  ContactFax = v.ContactFax,
                                  ContactEmail = v.ContactEmail,
                                  Address1 = v.Address1,
                                  Address2 = v.Address2,
                                  Address3 = v.Address3,
                                  Location = v.Location,
                                  RegistrationNumber = v.RegistrationNumber,
                                  TaxRegistrationNumber = v.TaxRegistrationNumber,
                                  SupplierAccount = v.SupplierAccount,
                                  PaymentTerms = v.PaymentTerms,
                                  AccountNumber = v.AccountNumber,
                                  Type = v.Type,
                                  Branch = v.Branch,
                                  Bank = b.BankName,
                                  Currency = c.CurrencyCode,
                                  CreationTime = v.CreationTime
                              }).ToList();

            var ListDtos = finalQuery.MapTo<List<VendorsListDto>>();

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

        public ListResultDto<VendorsListDto> GetVendors(GetClaimsInput input)
        {
            var query = _vendorsRepository.GetAll()
             .WhereIf(
                 !input.Filter.IsNullOrWhiteSpace(),
                 u =>
                     u.SupplierName.Contains(input.Filter) ||
                     u.ContactEmail.Contains(input.Filter) ||
                     u.ContactName.Contains(input.Filter)
             )
             .OrderByDescending(p => p.LastModificationTime)
             .ToList();

            return new ListResultDto<VendorsListDto>(ObjectMapper.Map<List<VendorsListDto>>(query));
        }


        public void AddVendor(GVendorsListDto input)
        {
            var client = input.MapTo<Vendor>();
            client.IsActive = true;
            client.SupplierCode = System.Guid.NewGuid();
            _vendorsRepository.Insert(client);
        }


        public VendorsListDto GetVendor(GetClaimsInput input)
        {
            int Id = Convert.ToInt32(input.Filter);

            var query = _vendorsRepository
              .GetAll().Where(c => c.Id == Id)
              .FirstOrDefault();

            return query.MapTo<VendorsListDto>();
        }

        public void UpdateVendor(GVendorsListDto input)
        {
            try
            {
                var entities = input.MapTo<Vendor>();
                _vendorsRepository.Update(entities);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void ChangeStatus(StatusDto input)
        {
            int Id = Convert.ToInt32(input.Id);

            var query = _vendorsRepository
             .GetAll().Where(c => c.Id == Id)
             .FirstOrDefault();

            query.IsActive = input.Status;
            query.Id = Id;
            _vendorsRepository.Update(query);
        }
    }
}
