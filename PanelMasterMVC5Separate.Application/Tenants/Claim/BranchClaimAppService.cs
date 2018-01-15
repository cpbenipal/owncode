using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Claim.Dto;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.Tenants.Claim.Exporting;
using PanelMasterMVC5Separate.Clients;
using PanelMasterMVC5Separate.Vehicle;
using PanelMasterMVC5Separate.Insurer;
using PanelMasterMVC5Separate.Tenants.Insurer.Dto;
using PanelMasterMVC5Separate.Brokers;
using PanelMasterMVC5Separate.Tenants.Brokers.Dto;
using Abp.Runtime.Session;
using PanelMasterMVC5Separate.Vendors;
using PanelMasterMVC5Separate.MultiTenancy;
using Abp.Authorization.Users;
using PanelMasterMVC5Separate.Authorization.Claim;
using PanelMasterMVC5Separate.Authorization.Roles;
using PanelMasterMVC5Separate.RolesCategories;
using PanelMasterMVC5Separate.Authorization.Roles.Dto;

namespace PanelMasterMVC5Separate.Tenants.Claim
{

    public class BranchClaimAppService : PanelMasterMVC5SeparateAppServiceBase, IBranchClaimAppService
    {
        private readonly IAbpSession _abpSession;
        private readonly IClaimsListExcelExporter _claimListExcelExporter;
        private readonly IRepository<Jobs> _claimRepository;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<InsurerMaster> _InsuranceRepository;

        private readonly IRepository<VehicleMake> _manufactureRepository;
        private readonly IRepository<VehicleModels> _vehicleModelRepository;
        private readonly IRepository<BrokerMaster> _brokerRepository;

        private readonly IRepository<Jobstatus> _jobstatusRepository;
        private readonly IRepository<JobstatusMask> _jobstatusmaskRepository;
        private readonly IRepository<JobstatusTenant> _jobstatustenantRepository;
        private readonly IRepository<TowOperator> _towoperatorrepository;
        private readonly IRepository<TowSubOperator> _towsuboperatorrepositry;
        private readonly IRepository<TenantProfile> _TenantProfile;
        private readonly IRepository<Countries> _countryRepository;
        
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<RolesCategory> _rolesCategoryRepository;
        private readonly RoleManager _roleManager;


        public BranchClaimAppService(IAbpSession abpSession, IClaimsListExcelExporter claimListExcelExporter, IRepository<Jobs> claimRepository,
                                     IRepository<Client> clientRepository, IRepository<InsurerMaster> InsuranceRepository,
                                     IRepository<VehicleMake> manufactureRepository, IRepository<BrokerMaster> brokerRepository,
                                     IRepository<VehicleModels> vehicleModelRepository, IRepository<Jobstatus> jobstatusRepository, IRepository<JobstatusMask> jobstatusmaskRepository,
                                     IRepository<JobstatusTenant> jobstatustenantRepository, IRepository<TowOperator> towoperatorrepository,
                                     IRepository<TenantProfile> tenantprofile, IRepository<Countries> countryRepository, IRepository<TowSubOperator> towsuboperatorrepositry,
                                     IRepository<User, long> userRepository, IRepository<UserRole, long> userRoleRepository, IRepository<RolesCategory> rolesCategoryRepository,
                                     RoleManager roleManager)
        {
            _abpSession = abpSession;
            _claimListExcelExporter = claimListExcelExporter;
            _claimRepository = claimRepository;
            _clientRepository = clientRepository;
            _InsuranceRepository = InsuranceRepository;
            _manufactureRepository = manufactureRepository;
            _brokerRepository = brokerRepository;
            _vehicleModelRepository = vehicleModelRepository;
            _jobstatusRepository = jobstatusRepository;
            _jobstatusmaskRepository = jobstatusmaskRepository;
            _jobstatustenantRepository = jobstatustenantRepository;
            _towoperatorrepository = towoperatorrepository;
            _towsuboperatorrepositry = towsuboperatorrepositry;
            _TenantProfile = tenantprofile;
            _countryRepository = countryRepository;

            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _rolesCategoryRepository = rolesCategoryRepository;
            _roleManager = roleManager;
          
        }
        public ListResultDto<Brokers.Dto.CountriesDto> GetCountry()
        {
            var banks = _countryRepository
                .GetAll()
                .OrderBy(p => p.Country)
                .ToList();

            return new ListResultDto<Brokers.Dto.CountriesDto>(ObjectMapper.Map<List<Brokers.Dto.CountriesDto>>(banks));
        }      
      
        public ListResultDto<BranchClaimListDto> GetClaims(GetClaimsInput input)
        {
            var queryjobs = _claimRepository.GetAll().Where(p=>p.TenantID == _abpSession.TenantId).ToList();

            var queryClient = _clientRepository.GetAll().ToList();

            var queryInsurance = _InsuranceRepository.GetAll().ToList();

            var query = (from j in queryjobs
                         join c in queryClient on j.ClientID equals c.Id
                         join n in queryInsurance on j.InsuranceID equals n.Id

                         select new BranchClaimListDto
                         {
                             Id = j.Id,
                             Name = c.Name,
                             Surname = c.Surname,
                             Insurance = n.InsurerName,
                             RegNo = j.RegNo,
                             CreationTime = j.CreationTime
                         }).WhereIf(
                !input.Filter.IsNullOrWhiteSpace(),
                u =>
                    u.Name.Contains(input.Filter) ||
                    u.Surname.Contains(input.Filter) ||
                    u.Insurance.Contains(input.Filter) ||
                    u.RegNo.Contains(input.Filter)
            )
            .OrderBy(p => p.Name)
            .ThenBy(p => p.Surname)
            .ToList();


            return new ListResultDto<BranchClaimListDto>(ObjectMapper.Map<List<BranchClaimListDto>>(query));

        }

        public ListResultDto<RoleCategoriesDto> GetRoles(int RolesCategoryID)
        {
            var user = _userRepository.GetAll().ToList();
            var userRoles = _userRoleRepository.GetAll().ToList();
            var rolesCategory = _rolesCategoryRepository.GetAll().ToList();
            var roles = _roleManager.Roles.ToList();

            var query = (from u in user
                         join ur in userRoles on u.Id equals ur.UserId
                         join r in roles on ur.RoleId equals r.Id
                         join rc in rolesCategory on r.RoleCategoryID equals rc.Id
                         where u.TenantId == _abpSession.TenantId && rc.Id == RolesCategoryID

                         select new RoleCategoriesDto
                         {
                             ID = Convert.ToInt16(u.Id),
                             Description = u.Name
                         }).ToList();

            return new ListResultDto<RoleCategoriesDto>(ObjectMapper.Map<List<RoleCategoriesDto>>(query));
        }

        public async Task<FileDto> GetClaimsToExcel()
        {
            var claims = await _claimRepository.GetAll().Where(p => p.TenantID == _abpSession.TenantId).ToListAsync();
            var claimListDtos = claims.MapTo<List<BranchClaimListDto>>();

            return _claimListExcelExporter.ExportToFile(claimListDtos);
        }

        /*public ListResultDto<ClaimStatusListDto> GetJobStatuses(GetClaimsInput input)
        {
            int Id = Convert.ToInt32(input.Filter);

            //Get Jobs by Id
            var thisJob = _claimRepository.GetAll().Where(c => c.Id == Id).FirstOrDefault();

            //Get Current Job Status
            var thisCurrentClaimStatus = _claimStatusRepository.GetAll().Where(m => m.Id == thisJob.ClaimStatusID).FirstOrDefault();

            //Get All statuses except currrent status
            var thisAllClaimStatus = _claimStatusRepository.GetAll().Where(m => m.Id != thisJob.ClaimStatusID && m.ShowStatus == true).ToList();

            var statuses = new List<ClaimStatusListDto>();

            statuses.Add(new ClaimStatusListDto {

                Id = thisCurrentClaimStatus.Id,
                Description = thisCurrentClaimStatus.Description
            });

            return new ListResultDto<ClaimStatusListDto>(statuses);
        }*/

        public BranchClaimListDto GetJobDetails(GetClaimsInput input)
        {
            //string dd = input.Filter;
            int Id = Convert.ToInt32(input.Filter);

            //Get Jobs by Id
            var thisJob = _claimRepository
               .GetAll().Where(c => c.Id == Id)
               .FirstOrDefault();

            //Get Client by Id
            var thisclient = _clientRepository
               .GetAll().Where(c => c.Id == thisJob.ClientID)
               .FirstOrDefault();

            //Get Insurance by Id
            var thisInsurance = _InsuranceRepository
               .GetAll().Where(c => c.Id == thisJob.InsuranceID)
               .FirstOrDefault();

            //Get Broker by Id
            var thisBroker = _brokerRepository
               .GetAll().Where(c => c.Id == thisJob.BrokerID)
               .FirstOrDefault();

            //Get Model by Id
            var thisModel = _vehicleModelRepository
                .GetAll().Where(m => m.Id == thisJob.ModelID)
                .FirstOrDefault();

            //Get Make/Manufacture by Id
            var thisMake = _manufactureRepository
                .GetAll().Where(m => m.Id == thisJob.ManufactureID)
                .FirstOrDefault();

            //Get Job Status by JobStatusID
            var thisJobStatus = _jobstatusRepository
               .GetAll().Where(j => j.Id == thisJob.JobStatusID)
               .FirstOrDefault();

            var finalQuery = (new BranchClaimListDto
            {
                ClientID = thisclient.Id,
                Name = thisclient.Name,
                Surname = thisclient.Surname,
                Email = thisclient.Email,
                Tel = thisclient.Tel,

                Id = thisJob.Id,
                Colour = thisJob.Colour,
                Year = thisJob.Year,
                RegNo = thisJob.RegNo,
                VinNumber = thisJob.VinNumber,
                BranchEntryMethod = thisJob.BranchEntryMethod,

                BrokerID = thisBroker.Id,
                Broker = thisBroker.BrokerName,

                InsuranceID = thisInsurance.Id,
                Insurance = thisInsurance.InsurerName,

                ManufactureID = thisMake.Id,
                Manufacture = thisMake.Description,

                ModelID = thisModel.Id,
                Model = thisModel.Model,

                JobStatusID = thisJobStatus.Id,
                JobStatusDesc = thisJobStatus.Description,

                New_Comeback = thisJob.New_Comeback

                

            }).MapTo<BranchClaimListDto>();

            return finalQuery;

        }


        public void UpdateVehicleInfo(BranchClaimListDto input)
        {
            try
            {

                var jobs = _claimRepository.Get(input.Id);
                jobs.Colour = input.Colour;
                jobs.RegNo = input.RegNo;
                jobs.VinNumber = input.VinNumber;
                jobs.Year = input.Year;

                _claimRepository.Update(jobs);

            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void UpdateInsuranceInfo(BranchClaimListDto input)
        {
            try
            {
                var jobs = _claimRepository.Get(input.Id);

                jobs.BrokerID = input.BrokerID;
                jobs.InsuranceID = input.InsuranceID;
                _claimRepository.Update(jobs);

            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void UpdateClient(BranchClaimListDto input)
        {
            try
            {
                var clients = _clientRepository.Get(input.ClientID);
                clients.Name = input.Name;
                clients.Surname = input.Surname;
                _clientRepository.Update(clients);

            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public ListResultDto<InsurersDto> GetInsurances()
        {
            var insurance = _InsuranceRepository
                .GetAll()
                .OrderBy(p => p.InsurerName)
                .ToList();

            return new ListResultDto<InsurersDto>(ObjectMapper.Map<List<InsurersDto>>(insurance));
        }

        public ListResultDto<BrokersDto> GetBrokers()
        {
            var broker = _brokerRepository
                .GetAll()
                .OrderBy(p => p.BrokerName)
                .ToList();

            return new ListResultDto<BrokersDto>(ObjectMapper.Map<List<BrokersDto>>(broker));
        }

        public ListResultDto<JobStatusDto> GetJobStatuses(GetClaimsInput input)
        {
            var querystatustenant = _jobstatustenantRepository.GetAll().Where(p => p.Tenant == _abpSession.TenantId).ToList();

            var queryjobstatus = _jobstatusRepository.GetAll().ToList();

            var queryjobstatusmask = _jobstatusmaskRepository.GetAll().ToList();

            var query = (from j in queryjobstatus
                         join c in querystatustenant on j.Id equals c.JobStatusID into ps
                         from py1s in ps.DefaultIfEmpty()

                         select new JobStatusDto
                         {
                             Id = j.Id,
                             pkId = py1s == null ? 0 : py1s.Id,
                             Jobstatus = j.Description,
                             JobstatusMask = py1s == null ? "--" : (_jobstatusmaskRepository.FirstOrDefault(p => p.Id == py1s.Mask).Description1),
                             CreationTime = py1s == null ? "--" : py1s.CreationTime.ToShortDateString(),
                             Sortorder = py1s == null ? 0 : py1s.Sortorder,
                             IsActive = py1s == null ? false : py1s.isActive,
                             ShowAwaiting = py1s == null ? "--" : ((py1s.ShowAwaiting == true) ? "Yes" : "No"),
                             ShowSpeedbump = py1s == null ? "--" : ((py1s.ShowSpeedbump == true) ? "Yes" : "No")
                         }).WhereIf(
                !input.Filter.IsNullOrWhiteSpace(),
                u =>
                    u.Jobstatus.Contains(input.Filter) ||
                    u.JobstatusMask.Contains(input.Filter) ||
                    u.ShowAwaiting.Contains(input.Filter) ||
                    u.ShowSpeedbump.Contains(input.Filter)
            )
            .OrderBy(p => p.Id)
            .ToList();

            return new ListResultDto<JobStatusDto>(ObjectMapper.Map<List<JobStatusDto>>(query));
        }
        public async Task<FileDto> GetJobStatusToExcel()
        {
            var querystatustenant = await _jobstatustenantRepository.GetAll().Where(p => p.Tenant == _abpSession.TenantId).ToListAsync();
            var queryjobstatus = await _jobstatusRepository.GetAll().ToListAsync();
            var queryjobstatusmask = await _jobstatusmaskRepository.GetAll().ToListAsync();
            var query = (from j in queryjobstatus
                         join c in querystatustenant on j.Id equals c.JobStatusID into ps
                         from py1s in ps.DefaultIfEmpty()

                         select new JobStatusDto
                         {
                             Id = j.Id,
                             Jobstatus = j.Description,
                             JobstatusMask = py1s == null ? "--" : (_jobstatusmaskRepository.FirstOrDefault(p => p.Id == py1s.Mask).Description1),
                             CreationTime = py1s == null ? "--" : py1s.CreationTime.ToShortDateString(),
                             Sortorder = py1s == null ? 0 : py1s.Sortorder,
                             IsActive = py1s == null ? false : py1s.isActive,
                             ShowAwaiting = py1s == null ? "--" : ((py1s.ShowAwaiting == true) ? "Yes" : "No"),
                             ShowSpeedbump = py1s == null ? "--" : ((py1s.ShowSpeedbump == true) ? "Yes" : "No")
                         })
            .OrderBy(p => p.Id)
            .ToList();
            var claimListDtos = query.MapTo<List<JobStatusDto>>();
            return _claimListExcelExporter.ExportToFile(claimListDtos);
        }
        public ListResultDto<JobStatusMasksDto> GetJobStatusMasks()
        {
            var status = _jobstatusmaskRepository
                .GetAll()
                .Where(p => p.IsDeleted == false && p.Enabled == true)
                .ToList();

            return new ListResultDto<JobStatusMasksDto>(ObjectMapper.Map<List<JobStatusMasksDto>>(status));
        }
        // By jobstaticId
        public async Task<JobstatusTenantDto> GetJobStatusForEdit(GetJobInput input)
        {
            var output = new JobstatusTenantDto();
            var ifexist = _jobstatustenantRepository.FirstOrDefault(p => p.JobStatusID == input.id && p.Tenant == _abpSession.TenantId);
            if (ifexist != null)//Job Static doesn't exist in tblJobstatusTenant
            {
                try
                {
                    output = ifexist.MapTo<JobstatusTenantDto>();
                }
                catch (Exception c)
                {
                    throw c;
                }
            }
            var job = await _jobstatusRepository.GetAsync(input.id);
            output.JobStatus = job.Description;
            output.JobStatusID = job.Id;
            return output;
        }
        public void ChangeStatus(JobStatusDto input)
        {
            int Id = Convert.ToInt32(input.Id);

            var query = _jobstatustenantRepository
             .GetAll().Where(c => c.Id == Id && c.Tenant == _abpSession.TenantId)
             .FirstOrDefault();

            query.isActive = input.IsActive;

            _jobstatustenantRepository.Update(query);
        }
        public void CreateOrUpdateJobStatus(JobstatusTenantToDto input)
        {
            input.isActive = input.isActive; // Default Status : Quote Preparation             
            var query = input.MapTo<JobstatusTenant>();
            _jobstatustenantRepository.InsertOrUpdate(query);
        }

        public ListResultDto<int> GetSortOrders(int jobStatusId)
        {
            int currentSortOrder = 0;
            if (jobStatusId != 0)
            {
                var sortOrder = _jobstatustenantRepository.FirstOrDefault(p => p.JobStatusID == jobStatusId && p.Tenant == _abpSession.TenantId);
                if (sortOrder != null)//Job Static doesn't exist in tblJobstatusTenant
                {
                    currentSortOrder = sortOrder.Sortorder;
                }
            }
            var status = _jobstatustenantRepository.GetAll()
                .Where(p => p.IsDeleted == false && p.Tenant == _abpSession.TenantId && p.Sortorder != currentSortOrder).Select(x => x.Sortorder).ToList();

            var numberList = Enumerable.Range(1, 100).Except(status).ToList();

            return new ListResultDto<int>(ObjectMapper.Map<List<int>>(numberList));
        }
        public ListResultDto<TowOperatorDto> GetTowOperators(GetClaimsInput input)
        {
            int countryId = GetCountryIdByCode();
            var towops = _towoperatorrepository.GetAll().Where(p => p.CountryID == countryId)
               .WhereIf(
                !input.Filter.IsNullOrWhiteSpace(),
                u =>
                    u.Description.Contains(input.Filter)
            )
            .OrderBy(p => p.Id)
            .ToList();

            var towsub = _towsuboperatorrepositry.GetAll().Where(p => p.TenantId == _abpSession.TenantId)
             .WhereIf(
                 !input.Filter.IsNullOrWhiteSpace(),
                 u =>
                     u.ContactNumber.Contains(input.Filter) ||
                     u.ContactPerson.Contains(input.Filter) ||
                     u.EmailAddress.Contains(input.Filter)
             )
             .OrderByDescending(p => p.LastModificationTime)
             .ToList();

            var final = (from master in towops
                         join v in towsub on master.Id equals v.TowOperatorId into ps
                         from x in ps.DefaultIfEmpty()
                         select new TowOperatorDto
                         {
                             Id = master.Id,
                             SubpkId = x == null ? 0 : x.Id,
                             Description = master.Description,
                             ContactNumber = x == null ? "--" : x.ContactNumber,
                             ContactPerson = x == null ? "--" : x.ContactPerson,
                             EmailAddress = x == null ? "--" : x.EmailAddress,
                             isActive = x == null ? false : x.isActive
                         });


            return new ListResultDto<TowOperatorDto>(ObjectMapper.Map<List<TowOperatorDto>>(final));
        }
        public TowOperatorDto GetTow(NullableIdDto<int> input)
        {
            TowOperatorDto towsub = new TowOperatorDto();
            var querymain = _towsuboperatorrepositry.FirstOrDefault(c => c.TowOperatorId == input.Id);
            if (querymain != null)
            {
                towsub.ContactNumber = querymain.ContactNumber;
                towsub.ContactPerson = querymain.ContactPerson;
                towsub.EmailAddress = querymain.EmailAddress;
                towsub.TenantId = querymain.TenantId;
                towsub.Id = querymain.Id; // pk->subtow
            }
            var towquery = _towoperatorrepository.FirstOrDefault(p => p.Id == input.Id);
            towsub.Description = towquery.Description;
            towsub.TowOperatorId = towquery.Id; // pk->fk             
            return towsub;
        }
        public void CreateOrUpdateTowOperator(TowTenantDto input)
        {
            var client = input.MapTo<TowSubOperator>();
            _towsuboperatorrepositry.InsertOrUpdate(client);
        }
        public async Task<FileDto> GetTowOperatorsToExcel()
        {
            int countryId = GetCountryIdByCode();
            var towops = await _towoperatorrepository.GetAll().Where(p => p.CountryID == countryId)
            .OrderBy(p => p.Id)
            .ToListAsync();

            var towsub = await _towsuboperatorrepositry.GetAll().Where(p => p.TenantId == _abpSession.TenantId)
             .OrderByDescending(p => p.LastModificationTime)
             .ToListAsync();

            var final = (from master in towops
                         join v in towsub on master.Id equals v.TowOperatorId into ps
                         from x in ps.DefaultIfEmpty()
                         select new TowOperatorDto
                         {
                             Id = master.Id,
                             SubpkId = x == null ? 0 : x.Id,
                             Description = master.Description,
                             ContactNumber = x == null ? "--" : x.ContactNumber,
                             ContactPerson = x == null ? "--" : x.ContactPerson,
                             EmailAddress = x == null ? "--" : x.EmailAddress,
                             isActive = x == null ? false : x.isActive
                         });

            var claimListDtos = final.MapTo<List<TowOperatorDto>>();
            return _claimListExcelExporter.ExportToFile(claimListDtos);
        }
        public void ChangeTowStatus(TowOperatorDto input)
        {
            int Id = Convert.ToInt32(input.Id);

            var query = _towsuboperatorrepositry
             .GetAll().Where(c => c.Id == Id && c.TenantId == _abpSession.TenantId)
             .FirstOrDefault();

            query.isActive = input.isActive;

            _towsuboperatorrepositry.Update(query);
        }

        private int GetCountryIdByCode()
        {
            var CountryCode = _TenantProfile.FirstOrDefault(x => x.TenantId == _abpSession.TenantId);
            return (CountryCode == null ? 0 : _countryRepository.FirstOrDefault(x => x.Code == CountryCode.CountryCode).Id);
        }

        public void AddUpdateTowOperator(TowOperatorMainToDto input)
        {
            var client = input.MapTo<TowOperator>();
            client.CountryID = GetCountryIdByCode();
            if (input.Id == 0) // Active by default new tenant
                client.isActive = true;
            _towoperatorrepository.InsertOrUpdate(client);
        }

        public TowOperatorMainDto GetMainTowOperator(GetClaimsInput input)
        {
            int id = Convert.ToInt32(input.Filter);
            var query = _towoperatorrepository.FirstOrDefault(c => c.Id == id).MapTo<TowOperatorMainDto>();
            return query;
        }
    }
}
