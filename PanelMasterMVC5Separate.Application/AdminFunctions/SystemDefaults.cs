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
using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.MultiTenancy;
using PanelMasterMVC5Separate.Quotings;
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
        
        private readonly IRepository<TowOperator> _towoperatorrepositry;
        private readonly IRepository<RepairTypes> _repairtypesrepositry;
        private readonly IRepository<QuoteStatus> _quotestatusrepositry;
        private readonly IRepository<RolesCategory> _rolescategory;
        private readonly IRepository<CountryandCurrency> _countryandcurrency;
        private readonly IRepository<Countries> _countries;
        private readonly IRepository<Banks> _bank;
        private readonly IRepository<SignonPlans> _signonplans;
        private readonly IBankExport _IMListExcelExporter;
        private readonly IRepository<Jobstatus> _jobstatusRepository;
        private readonly IRepository<JobstatusMask> _jobstatusmaskRepository;

        public SystemDefaults(IRepository<RepairTypes> repairtypesrepositry, IRepository<RolesCategory> rolescategory, IRepository<CountryandCurrency> countryandcurrency
                    , IRepository<Countries> countries, IRepository<Banks> banks, IRepository<SignonPlans> signonplans,
            IRepository<Jobstatus> jobstatusRepository, IBankExport imlistexcelexporter, IRepository<JobstatusMask> jobstatusmaskRepository,
            IRepository<QuoteStatus> quotestatusrepositry, IRepository<TowOperator> towoperatorrepositry)
        {
            _rolescategory = rolescategory;
            _countryandcurrency = countryandcurrency;
            _countries = countries;
            _signonplans = signonplans;
            _bank = banks;
            _IMListExcelExporter = imlistexcelexporter;
            _jobstatusRepository = jobstatusRepository;
            _jobstatusmaskRepository = jobstatusmaskRepository;
            _quotestatusrepositry = quotestatusrepositry;
            _repairtypesrepositry = repairtypesrepositry;
            _towoperatorrepositry = towoperatorrepositry;            
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
                bank.isActive = true;
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
                         CreationTime = f.CreationTime,
                         isActive = f.isActive
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

        public void ChangeStatus(ActiveDto input)
        {
            var client = _bank.FirstOrDefault(input.Id);
            client.isActive = input.Status;
            _bank.Update(client);
        }
        // Job Status 
        public ListResultDto<JobStatusDto> GetJobStatuses(GetInput input)
        {
            var queryjobstatus = _jobstatusRepository.GetAll()
                .WhereIf(
                !input.Filter.IsNullOrWhiteSpace(),
                u =>
                    u.Description.Contains(input.Filter))
                    .OrderBy(p => p.Id)
            .ToList();

            return new ListResultDto<JobStatusDto>(ObjectMapper.Map<List<JobStatusDto>>(queryjobstatus));
        }

        public void CreateOrUpdateJobStatus(JobStatusToDto input)
        {
            var jobstatus = input.MapTo<Jobstatus>();
            _jobstatusRepository.InsertOrUpdate(jobstatus);
        }

        public JobStatusDto GetJobStatus(GetClaimsInput input)
        {
            var query = _jobstatusRepository
              .GetAll().FirstOrDefault(c => c.Id == input.Id).MapTo<JobStatusDto>();
            return query;
        }

        public async Task<FileDto> GetJobStatusToExcel()
        {
            var queryjobstatus = await _jobstatusRepository.GetAll().ToListAsync();
            var claimListDtos = queryjobstatus.MapTo<List<JobStatusDto>>();
            return _IMListExcelExporter.ExportToFile(claimListDtos);
        }
        // Job Mask Status 
        public ListResultDto<JobStatusMaskDto> GetJobMaskStatuses(GetInput input)
        {
            var queryjobstatus = _jobstatusmaskRepository.GetAll()
                .WhereIf(
                !input.Filter.IsNullOrWhiteSpace(),
                u =>
                    u.Description1.Contains(input.Filter))
                    .OrderBy(p => p.Id)
            .ToList();
            return new ListResultDto<JobStatusMaskDto>(ObjectMapper.Map<List<JobStatusMaskDto>>(queryjobstatus));
        }
        public void CreateOrUpdateJobMaskStatus(JobStatusMaskToDto input)
        {
            input.Enabled = true;
            var jobstatus = input.MapTo<JobstatusMask>();
            _jobstatusmaskRepository.InsertOrUpdate(jobstatus);
        }
        public JobStatusMaskDto GetJobStatusMask(GetClaimsInput input)
        {
            var query = _jobstatusmaskRepository
              .GetAll().FirstOrDefault(c => c.Id == input.Id).MapTo<JobStatusMaskDto>();
            return query;
        }
        public void ChangeJobMaskStatus(ActiveDto input)
        {
            var client = _jobstatusmaskRepository.FirstOrDefault(input.Id);
            client.Enabled = input.Status;
            _jobstatusmaskRepository.Update(client);
        }
        public async Task<FileDto> GetJobStatusMaskToExcel()
        {
            var queryjobstatus = await _jobstatusmaskRepository.GetAll().ToListAsync();
            var claimListDtos = queryjobstatus.MapTo<List<JobStatusMaskDto>>();
            return _IMListExcelExporter.ExportToFile(claimListDtos);
        }

        // Quote Status 
        public ListResultDto<QuoteStatusDto> GetQuoteStatuses(GetInput input)
        {
            var queryjobstatus = _quotestatusrepositry.GetAll()
                .WhereIf(
                !input.Filter.IsNullOrWhiteSpace(),
                u =>
                    u.Description.Contains(input.Filter))
                    .OrderBy(p => p.Id)
            .ToList();
            return new ListResultDto<QuoteStatusDto>(ObjectMapper.Map<List<QuoteStatusDto>>(queryjobstatus));
        }
        public void CreateOrUpdateQuoteStatus(QuoteStatusToDto input)
        {
            input.Enabled = true;
            var jobstatus = input.MapTo<QuoteStatus>();
            _quotestatusrepositry.InsertOrUpdate(jobstatus);
        }
        public QuoteStatusDto GetQuoteStatus(GetClaimsInput input)
        {
            var query = _quotestatusrepositry
              .GetAll().FirstOrDefault(c => c.Id == input.Id).MapTo<QuoteStatusDto>();
            return query;
        }
        public void ChangeQuoteStatusStatus(ActiveDto input)
        {
            var client = _quotestatusrepositry.FirstOrDefault(input.Id);
            client.Enabled = input.Status;
            _quotestatusrepositry.Update(client);
        }
        public async Task<FileDto> GetQuoteStatusToExcel()
        {
            var queryjobstatus = await _quotestatusrepositry.GetAll().ToListAsync();
            var claimListDtos = queryjobstatus.MapTo<List<QuoteStatusDto>>();
            return _IMListExcelExporter.ExportToFile(claimListDtos);
        }

        // Repair Type
        public ListResultDto<RepairTypeDto> GetRepairTypes(GetInput input)
        {
            var queryjobstatus = _repairtypesrepositry.GetAll()
                .WhereIf(
                !input.Filter.IsNullOrWhiteSpace(),
                u =>
                    u.Description.Contains(input.Filter))
                    .OrderBy(p => p.Id)
            .ToList();
            return new ListResultDto<RepairTypeDto>(ObjectMapper.Map<List<RepairTypeDto>>(queryjobstatus));
        }
        public void CreateOrUpdateRepairType(RepairTypeToDto input)
        {
            input.Enabled = true;
            var jobstatus = input.MapTo<RepairTypes>();
            _repairtypesrepositry.InsertOrUpdate(jobstatus);
        }
        public RepairTypeDto GetRepairType(GetClaimsInput input)
        {
            var query = _repairtypesrepositry
              .GetAll().FirstOrDefault(c => c.Id == input.Id).MapTo<RepairTypeDto>();
            return query;
        }
        public void ChangeRepairTypeStatus(ActiveDto input)
        {
            var client = _repairtypesrepositry.FirstOrDefault(input.Id);
            client.Enabled = input.Status;
            _repairtypesrepositry.Update(client);
        }
        public async Task<FileDto> GetRepairTypeToExcel()
        {
            var queryjobstatus = await _repairtypesrepositry.GetAll().ToListAsync();
            var claimListDtos = queryjobstatus.MapTo<List<RepairTypeDto>>();
            return _IMListExcelExporter.ExportToFile(claimListDtos);
        }

        // Role Category
        public ListResultDto<RoleCategoryDto> GetRoleCategories(GetInput input)
        {
            var queryjobstatus = _rolescategory.GetAll()
                .WhereIf(
                !input.Filter.IsNullOrWhiteSpace(),
                u =>
                    u.Description.Contains(input.Filter))
                    .OrderBy(p => p.Id)
            .ToList();
            return new ListResultDto<RoleCategoryDto>(ObjectMapper.Map<List<RoleCategoryDto>>(queryjobstatus));
        }
        public void CreateOrUpdateRoleCategory(RoleCategoryToDto input)
        {
            input.Enabled = true;
            var jobstatus = input.MapTo<RolesCategory>();
            _rolescategory.InsertOrUpdate(jobstatus);
        }
        public RoleCategoryDto GetRoleCategory(GetClaimsInput input)
        {
            var query = _rolescategory
              .GetAll().FirstOrDefault(c => c.Id == input.Id).MapTo<RoleCategoryDto>();
            return query;
        }
        public void ChangeRoleCategoryStatus(ActiveDto input)
        {
            var client = _rolescategory.FirstOrDefault(input.Id);
            client.Enabled = input.Status;
            _rolescategory.Update(client);
        }
        public async Task<FileDto> GetRoleCategoryToExcel()
        {
            var query = await _rolescategory.GetAll().ToListAsync();
            var claimListDtos = query.MapTo<List<RoleCategoryDto>>();
            return _IMListExcelExporter.ExportToFile(claimListDtos);
        }

        public ListResultDto<SignOnDto> GetSignOnPlans(GetInputs input)
        {
            var data = _signonplans.GetAll()
         .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
         u => u.PlanName.ToLower().Contains(input.Filter.ToLower())
         )
         .OrderByDescending(p => p.LastModificationTime)
         .ToList();

            return new ListResultDto<SignOnDto>(ObjectMapper.Map<List<SignOnDto>>(data));
        }

        public void CreateOrUpdateSignOnPlan(SignOnToDto input)
        {
            input.isActive = true;
            var client = input.MapTo<SignonPlans>();
            _signonplans.InsertOrUpdate(client);
        }

        public void ChangePlanStatus(ActiveDto input)
        {
            var client = _signonplans.FirstOrDefault(input.Id);
            client.isActive = input.Status;
            _signonplans.Update(client);
        }

        public SignOnDto GetPlanDetail(GetClaimsInput input)
        {
            var query = _signonplans
              .GetAll().FirstOrDefault(c => c.Id == input.Id).MapTo<SignOnDto>();
            return query;
        }
        public async Task<FileDto> GetSignOnToExcel()
        {
            var query = await _signonplans.GetAll().ToListAsync();
            var claimListDtos = query.MapTo<List<SignOnDto>>();
            return _IMListExcelExporter.ExportToFile(claimListDtos);
        }

        public ListResultDto<TowOperatorDto> GetTowOperators(GetInputs input)
        {
            var towops = _towoperatorrepositry.GetAll()
                           .WhereIf(
                           !input.Filter.IsNullOrWhiteSpace(),
                           u =>
                               u.Description.Contains(input.Filter))
                               .OrderBy(p => p.Id)
                       .ToList();
            var countries = _countries.GetAll().AsNoTracking().ToList();

            var final = (
                from x in towops
                join v in countries on x.CountryID equals v.Id
                select new TowOperatorDto
                {
                    Id = x.Id,
                    Description = x.Description,
                    CreationTime = x.CreationTime,
                    Country = v.Country,
                    isActive = x.isActive
                });

            return new ListResultDto<TowOperatorDto>(ObjectMapper.Map<List<TowOperatorDto>>(final));
        }

        public void CreateOrUpdateTowOperator(TowOperatorToDto input)
        {
            var client = input.MapTo<TowOperator>();
            _towoperatorrepositry.InsertOrUpdate(client);
        }

        public TowOperatorDto GetTowOperator(GetClaimsInput input)
        {
            var query = _towoperatorrepositry
             .GetAll().FirstOrDefault(c => c.Id == input.Id).MapTo<TowOperatorDto>();
            return query;
        }

        public async Task<FileDto> GetTowOperatorsExcel()
        {
            var query = await _towoperatorrepositry.GetAll().ToListAsync();
            var claimListDtos = query.MapTo<List<TowOperatorDto>>();
            return _IMListExcelExporter.ExportToFile(claimListDtos);
        }

        public void ChangeTowOperatorStatus(ActiveDto input)
        {
            var client = _towoperatorrepositry.FirstOrDefault(input.Id);
            client.isActive = input.Status;
            _towoperatorrepositry.Update(client);
        }
    }
}
