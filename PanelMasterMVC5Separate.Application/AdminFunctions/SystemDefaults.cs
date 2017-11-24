using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using PanelMasterMVC5Separate.AdminFunctions.Dto;
using PanelMasterMVC5Separate.AdminFunctions.Exporting;
using PanelMasterMVC5Separate.Authorization;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.MultiTenancy;
using PanelMasterMVC5Separate.RolesCategories;
using PanelMasterMVC5Separate.Vendors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.AdminFunctions
{
    [AbpAuthorize(AppPermissions.Pages_Administration_Host_SystemDefaults)]
    public class SystemDefaults : PanelMasterMVC5SeparateAppServiceBase, ISystemDefaults
    {
        private readonly IRepository<RolesCategory> _rolescategory;
        private readonly IRepository<CountryandCurrency> _countryandcurrency;
        private readonly IRepository<Countries> _countries;
        private readonly IRepository<Banks> _bank;
        private readonly IRepository<SignonPlans> _signonplans;
        private readonly IBankExport _IMListExcelExporter;
        public SystemDefaults(IRepository<RolesCategory> rolescategory, IRepository<CountryandCurrency> countryandcurrency
                    , IRepository<Countries> countries, IRepository<Banks> banks, IRepository<SignonPlans> signonplans,
             IBankExport imlistexcelexporter)
        {
            _rolescategory = rolescategory;
            _countryandcurrency = countryandcurrency;
            _countries = countries;
            _signonplans = signonplans;
            _bank = banks;
            _IMListExcelExporter = imlistexcelexporter;
        }
        public ListResultDto<CountriesDto> GetCountry()
        {
            var banks = _countries
                .GetAll()
                .OrderBy(p => p.Code)
                .ToList();

            return new ListResultDto<CountriesDto>(ObjectMapper.Map<List<CountriesDto>>(banks));
        }

        ListResultDto<functionDto> GetRolescategory(GetInputs input)
        {
            var data = _rolescategory.GetAll()
           .WhereIf(
               !input.Filter.IsNullOrWhiteSpace(),
               u =>
                  u.Description.ToLower().Contains(input.Filter.ToLower())
           )
           .OrderByDescending(p => p.LastModificationTime)
           .ToList();

            var a = (from f in data
                     select new functionDto
                     {
                         Description = f.Description,
                         Id = f.Id,
                         Enabled = f.Enabled,
                         CreationTime = f.CreationTime,
                         ShowSwitch = true
                     }).ToList();


            return new ListResultDto<functionDto>(ObjectMapper.Map<List<functionDto>>(a));
        }

        public void CreateOrUpdateBank(BankToDto input)
        {
            var bank = input.MapTo<Banks>();

            var id = (dynamic)null;
            if (input.Id == 0)
            {
                id = _bank.FirstOrDefault(c => c.BankName == input.BankName && c.CountryID == input.CountryId);
            }
            else
            {
                id = _bank.FirstOrDefault(c => (c.BankName == input.BankName && c.CountryID == input.CountryId) && c.Id != input.Id);
            }
            if (id != null)
            {
                throw new UserFriendlyException("Duplicate bank name not allowed");
            }
            else
            {
                _bank.InsertOrUpdate(bank);
            }
        }

        public ListResultDto<BankDto> GetBanks(GetInputs input)
        {
            var data = _bank.GetAll()
           .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
           u => u.BankName.ToLower().Contains(input.Filter.ToLower())
           )
           .OrderByDescending(p => p.LastModificationTime)
           .ToList();

            var a = (from f in data
                     select new BankDto
                     {
                         CountryCode = _countries.FirstOrDefault(x => x.Id == f.CountryID).Code,
                         Id = f.Id,
                         BankName = f.BankName,
                         CreationTime = f.CreationTime
                     }).ToList();


            return new ListResultDto<BankDto>(ObjectMapper.Map<List<BankDto>>(a));
        }

        public BankDetailDto GetBank(GetClaimsInput input)
        {
            var query = _bank
              .GetAll().Where(c => c.Id == input.Id)
              .FirstOrDefault();

            var finalquery = query.MapTo<BankDetailDto>();
            finalquery.CountryId = input.Id != 0 ? _countries.FirstOrDefault(query.CountryID).Id : 0;

            return finalquery;
        }
        public async Task<FileDto> GetBanksExcel()
        {
            var data = await _bank.GetAll().ToListAsync();

            var finalQuery = (from f in data
                              select new BankDto
                              {
                                  CountryCode = _countries.FirstOrDefault(x => x.Id == f.CountryID).Code,
                                  BankName = f.BankName,
                                  CreationTime = f.CreationTime
                              }).ToList();


            var ListDtos = finalQuery.MapTo<List<BankDto>>();
            return _IMListExcelExporter.ExportToFile(ListDtos);
        }
    }
}
