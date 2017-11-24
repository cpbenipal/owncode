using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using PanelMasterMVC5Separate.AdminFunctions.Dto;
using PanelMasterMVC5Separate.Quotings;
using PanelMasterMVC5Separate.RolesCategories;
using System.Collections.Generic;
using System.Linq;
using System;
using PanelMasterMVC5Separate.Claim;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.Vendors;
using PanelMasterMVC5Separate.MultiTenancy;
using Abp.Authorization;
using PanelMasterMVC5Separate.Authorization;

namespace PanelMasterMVC5Separate.AdminFunctions
{
    public class Functions : PanelMasterMVC5SeparateAppServiceBase, IFunctions
    {
        private readonly IRepository<RolesCategory> _rolescategory;
        private readonly IRepository<RepairTypes> _repairtypes;
        private readonly IRepository<QuoteStatus> _quotestatus;
        private readonly IRepository<Jobstatus> _jobstatus;
        private readonly IRepository<JobstatusMask> _jobstatusmask;
        private readonly IRepository<CountryandCurrency> _countryandcurrency;
        private readonly IRepository<Countries> _countries;
        private readonly IRepository<Banks> _bank;
        private readonly IRepository<SignonPlans> _signonplans;

        public Functions(IRepository<RolesCategory> rolescategory, IRepository<RepairTypes> repairtypes, IRepository<QuoteStatus> quotestatus,
            IRepository<Jobstatus> jobstatus, IRepository<JobstatusMask> jobstatusmask, IRepository<CountryandCurrency> countryandcurrency
            , IRepository<Countries> countries, IRepository<Banks> banks, IRepository<SignonPlans> signonplans)
        {
            _rolescategory = rolescategory; _repairtypes = repairtypes; _quotestatus = quotestatus;
            _jobstatus = jobstatus; _jobstatusmask = jobstatusmask;
            _countryandcurrency = countryandcurrency; _countries = countries; _signonplans = signonplans; _bank = banks;
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
        ListResultDto<functionDto> GetRepairType(GetInputs input)
        {
            var data = _repairtypes.GetAll()
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
        ListResultDto<functionDto> GetQuoteStatus(GetInputs input)
        {
            var data = _quotestatus.GetAll()
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
        private ListResultDto<functionDto> GetJobStatus(GetInputs input)
        {
            var data = _jobstatus.GetAll()
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
                         Enabled = false,
                         CreationTime = f.CreationTime,
                         ShowSwitch = false
                     }).ToList();


            return new ListResultDto<functionDto>(ObjectMapper.Map<List<functionDto>>(a));
        }
        private ListResultDto<functionDto> GetJobstatusMask(GetInputs input)
        {
            var data = _jobstatusmask.GetAll()
        .WhereIf(
            !input.Filter.IsNullOrWhiteSpace(),
            u =>
                u.Description1.ToLower().Contains(input.Filter.ToLower())
        )
        .OrderByDescending(p => p.LastModificationTime)
        .ToList();

            var a = (from f in data
                     select new functionDto
                     {
                         Description = f.Description1,
                         Id = f.Id,
                         Enabled = f.Enabled,
                         CreationTime = f.CreationTime,
                         ShowSwitch = true
                     }).ToList();


            return new ListResultDto<functionDto>(ObjectMapper.Map<List<functionDto>>(a));
        }
        public ListResultDto<functionDto> GetMasterRecords(GetInputs input)
        {
            ListResultDto<functionDto> fun = null;
            if (input.tableIndex == 1)//Rolescategory
                fun = GetRolescategory(input);
            else if (input.tableIndex == 2)//RepairType
                fun = GetRepairType(input);
            else if (input.tableIndex == 3)//QuoteStatus
                fun = GetQuoteStatus(input);
            else if (input.tableIndex == 4)//JobStatus
                fun = GetJobStatus(input);
            else if (input.tableIndex == 10)//JobstatusMask
                fun = GetJobstatusMask(input);
            return fun;
        }

        public void ChangeStatus(StatusDto input)
        {
            if (input.tableIndex == 1)//Rolescategory
            {
                var client = new RolesCategory()
                {
                    Id = input.Id,
                    Enabled = input.Status
                };
                _rolescategory.Update(client);
            }
            else if (input.tableIndex == 2)//RepairType
            {
                var client = new RepairTypes()
                {
                    Id = input.Id,
                    Enabled = input.Status
                };
                _repairtypes.Update(client);
            }
            else if (input.tableIndex == 3)//QuoteStatus
            {
                var client = new QuoteStatus()
                {
                    Id = input.Id,
                    Enabled = input.Status
                };
                _quotestatus.Update(client);
            }
             
            else if (input.tableIndex == 10)//JobstatusMask
            {
                var client = new JobstatusMask()
                {
                    Id = input.Id,
                    Enabled = input.Status
                };
                _jobstatusmask.Update(client);
            }
        }

        public void CreateOrUpdateDescription(TableDescriptionDto input)
        {
            if (input.tableIndex == 1)//Rolescategory
            {
                var client = new RolesCategory()
                {
                    Id = input.Id,
                    Description = input.Description
                };
                _rolescategory.InsertOrUpdate(client);
            }
            else if (input.tableIndex == 2)//RepairType
            {
                var client = new RepairTypes()
                {
                    Id = input.Id,
                    Description = input.Description
                };
                _repairtypes.InsertOrUpdate(client);
            }
            else if (input.tableIndex == 3)//QuoteStatus
            {
                var client = new QuoteStatus()
                {
                    Id = input.Id,
                    Description = input.Description
                };
                _quotestatus.InsertOrUpdate(client);
            }
            else if (input.tableIndex == 4)//JobStatus
            {
                var client = new Jobstatus()
                {
                    Id = input.Id,
                    Description = input.Description
                };
                _jobstatus.InsertOrUpdate(client);
            }
            else if (input.tableIndex == 10)//JobstatusMask
            {
                var client = new JobstatusMask()
                {
                    Id = input.Id,
                    Description1 = input.Description
                };
                _jobstatusmask.InsertOrUpdate(client);
            }
        }

        // Countries and Currency stuff 
        public ListResultDto<functionCCDto> GetCountryOrCurrency(GetInputs input)
        {
            ListResultDto<functionCCDto> fun = null;
            if (input.tableIndex == 1)//Currencies
                fun = GetCurrencies(input);
            else if (input.tableIndex == 2)//Countries
                fun = GetCountries(input);

            return fun;
        }

        private ListResultDto<functionCCDto> GetCountries(GetInputs input)
        {
            var data = _countries.GetAll()
            .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
            u => u.Country.ToLower().Contains(input.Filter.ToLower())
             || u.Code.ToLower().Contains(input.Filter.ToLower())
            )
            .OrderByDescending(p => p.LastModificationTime)
            .ToList();

            var a = (from f in data
                     select new functionCCDto
                     {
                         Description = f.Country,
                         Id = f.Id,
                         Code = f.Code,
                         CreationTime = f.CreationTime
                     }).ToList();


            return new ListResultDto<functionCCDto>(ObjectMapper.Map<List<functionCCDto>>(a));
        }

        private ListResultDto<functionCCDto> GetCurrencies(GetInputs input)
        {
            var data = _countryandcurrency.GetAll()
           .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
           u => u.CountryAndCurrency.ToLower().Contains(input.Filter.ToLower())
            || u.CurrencyCode.ToLower().Contains(input.Filter.ToLower())
           )
           .OrderByDescending(p => p.LastModificationTime)
           .ToList();

            var a = (from f in data
                     select new functionCCDto
                     {
                         Description = f.CountryAndCurrency,
                         Id = f.Id,
                         Code = f.CurrencyCode,
                         CreationTime = f.CreationTime
                     }).ToList();


            return new ListResultDto<functionCCDto>(ObjectMapper.Map<List<functionCCDto>>(a));
        }

        public void CreateOrUpdateCodes(CodeDto input)
        {
            if (input.tableIndex == 1)//Currency
            {
                var client = new CountryandCurrency()
                {
                    Id = input.Id,
                    CountryAndCurrency = input.Description,
                    CurrencyCode = input.Code,
                    LastModificationTime = DateTime.Now
                };
                _countryandcurrency.InsertOrUpdate(client);
            }
            else if (input.tableIndex == 1)//Countries
            {
                var client = new Countries()
                {
                    Id = input.Id,
                    Country = input.Description,
                    Code = input.Code,
                    LastModificationTime = DateTime.Now
                };
                _countries.InsertOrUpdate(client);
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
                         CountryCode = _countries.FirstOrDefault(x=>x.Id == f.CountryID).Code,
                         Id = f.Id,
                         BankName = f.BankName,
                         CreationTime = f.CreationTime
                     }).ToList();


            return new ListResultDto<BankDto>(ObjectMapper.Map<List<BankDto>>(a));
        }

        public void CreateOrUpdateBank(CodeDto input)
        {
            var client = new Banks()
            {
                Id = input.Id,
                BankName = input.Description,
                CountryID = input.CountryID,
                LastModificationTime = DateTime.Now
            };
            _bank.InsertOrUpdate(client);

        }

        public ListResultDto<PlanDto> GetSignOnPlans(GetInputs input)
        {
            var data = _signonplans.GetAll()
            .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
            u => u.PlanName.ToLower().Contains(input.Filter.ToLower())
            )
            .OrderByDescending(p => p.LastModificationTime)
            .ToList();

            var a = (from f in data
                     select new PlanDto
                     {                       
                         Id = f.Id,
                         PlanName = f.PlanName,
                         HeaderColor = f.HeaderColor,
                         Members  = f.Members,
                         Price = f.Price,
                         CreationTime = f.CreationTime
                     }).ToList();


            return new ListResultDto<PlanDto>(ObjectMapper.Map<List<PlanDto>>(a));
        }


        public void CreateOrUpdateSignOnPlan(PlanDto f)
        {
            var client = new SignonPlans()
            {
                Id = f.Id,
                PlanName = f.PlanName,
                HeaderColor = f.HeaderColor.ToLower(),
                Members = f.Members,
                Price = f.Price,
            };
            _signonplans.InsertOrUpdate(client);
        }
    }
}