﻿using Abp.Application.Services.Dto;
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
using PanelMasterMVC5Separate.Authorization.Claim;
using Abp.Authorization.Users;
using PanelMasterMVC5Separate.RolesCategories;
using PanelMasterMVC5Separate.Authorization.Roles;
using PanelMasterMVC5Separate.Authorization.Roles.Dto;

namespace PanelMasterMVC5Separate.Tenants.Claim
{

    public class BranchClaimAppService : PanelMasterMVC5SeparateAppServiceBase, IBranchClaimAppService
    {
        private readonly IAbpSession _abpSession;
        private readonly IClaimsListExcelExporter _claimListExcelExporter;
        private readonly IRepository<Jobs> _claimRepository;
        private readonly IRepository<BrVehicle> _brVehicleRepository;
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
        private readonly IRepository<PaintTypes> _paintTypesRepository;
        private readonly RoleManager _roleManager;


        public BranchClaimAppService(IAbpSession abpSession, IClaimsListExcelExporter claimListExcelExporter, IRepository<Jobs> claimRepository,
                                     IRepository<BrVehicle> brVehicleRepository, IRepository<Client> clientRepository, IRepository<InsurerMaster> InsuranceRepository,
                                     IRepository<VehicleMake> manufactureRepository, IRepository<BrokerMaster> brokerRepository,
                                     IRepository<VehicleModels> vehicleModelRepository, IRepository<Jobstatus> jobstatusRepository, IRepository<JobstatusMask> jobstatusmaskRepository,
                                     IRepository<JobstatusTenant> jobstatustenantRepository, IRepository<TowOperator> towoperatorrepository,
                                     IRepository<TenantProfile> tenantprofile, IRepository<Countries> countryRepository, IRepository<TowSubOperator> towsuboperatorrepositry,
                                     IRepository<User, long> userRepository, IRepository<UserRole, long> userRoleRepository, IRepository<RolesCategory> rolesCategoryRepository,
                                     RoleManager roleManager, IRepository<PaintTypes> paintTypesRepository)
        {
            _abpSession = abpSession;
            _claimListExcelExporter = claimListExcelExporter;
            _brVehicleRepository = brVehicleRepository;
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
            _paintTypesRepository = paintTypesRepository;

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

            var queryveh = _brVehicleRepository.GetAll().Where(p => p.TenantId == _abpSession.TenantId).ToList();

            var queryClient = _clientRepository.GetAll().ToList();

            var queryStatus = _jobstatusRepository.GetAll().Where(p=>p.IsDeleted == false).ToList();

            var queryInsurance = _InsuranceRepository.GetAll().Where(p => p.IsDeleted == false && p.IsActive == true).ToList();

            var queryBroker = _brokerRepository.GetAll().Where(p => p.IsDeleted == false && p.IsActive == true).ToList();

            var query = (from j in queryjobs
                         join c in queryClient on j.ClientID equals c.Id
                         join n in queryInsurance on j.InsuranceID equals n.Id
                         join v in queryveh on j.VehicleID equals v.Id
                         join js in queryStatus on j.JobStatusID equals js.Id
                         join br in queryBroker on j.BrokerID equals br.Id
                         select new BranchClaimListDto
                         {
                             Id = j.Id,
                             Name = c.Name,
                             Surname = c.Surname,
                             Insurance = n.InsurerName,
                             Broker = br.BrokerName,
                             RegNo = v.RegistrationNumber,
                             BranchEntryMethod = j.BranchEntryMethod,
                             JobStatusDesc = js.Description,
                             CreationTime = j.CreationTime,
                             ShopAllocation  = j.ShopAllocation
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

        public ListResultDto<RoleCategoriesDto> GetRoles()
        {
            var user = _userRepository.GetAll().ToList();
            var userRoles = _userRoleRepository.GetAll().ToList();
            var rolesCategory = _rolesCategoryRepository.GetAll().ToList();
            var roles = _roleManager.Roles.ToList();

            var query = (from u in user
                         join ur in userRoles on u.Id equals ur.UserId
                         join r in roles on ur.RoleId equals r.Id
                         join rc in rolesCategory on r.RoleCategoryID equals rc.Id
                         where u.TenantId == _abpSession.TenantId

                         select new RoleCategoriesDto
                         {
                             ID = Convert.ToInt16(u.Id),
                             Description = u.Name,
                             RolesCategoryID = rc.Id
                         }).ToList();

            return new ListResultDto<RoleCategoriesDto>(ObjectMapper.Map<List<RoleCategoriesDto>>(query));
        }



        public async Task<FileDto> GetClaimsToExcel()
        {
            var claims = await _claimRepository.GetAll().Where(p => p.TenantID == _abpSession.TenantId).ToListAsync();
            var claimListDtos = claims.MapTo<List<BranchClaimListDto>>();

            return _claimListExcelExporter.ExportToFile(claimListDtos);
        }

        public ListResultDto<VehicleMake> GetVehicleMakes(int makeID)
        {
            var vMake = _manufactureRepository.GetAll().Where(m => m.Id != makeID).ToList();

            var res = (from m in vMake       
                       
               select new VehicleMake
               {
                   Id = m.Id,
                   Description = m.Description                   
               }).ToList();

            return new ListResultDto<VehicleMake>(ObjectMapper.Map<List<VehicleMake>>(res));
        }

        public ListResultDto<VehicleModels> GetVehicleModels(int modelID)
        {
            var vModel = _vehicleModelRepository.GetAll().Where(m => m.Id != modelID).ToList();

            var res = (from m in vModel

                       select new VehicleModels
                       {
                           Id = m.Id,
                           Model = m.Model
                       }).ToList();

            return new ListResultDto<VehicleModels>(ObjectMapper.Map<List<VehicleModels>>(res));
        }

        public ListResultDto<PaintTypes> GetPaintType(int paintTypeID)
        {
            var vPaintType = _paintTypesRepository.GetAll().Where(p => p.Id != paintTypeID).ToList();

            var res = (from p in vPaintType

                       select new PaintTypes
                       {
                           Id = p.Id,
                           PaintType = p.PaintType
                       }).ToList();

            return new ListResultDto<PaintTypes>(ObjectMapper.Map<List<PaintTypes>>(res));
        }

        public BranchClaimListDto GetJobDetails(GetClaimsInput input)
        {
            //string dd = input.Filter;
            int Id = Convert.ToInt32(input.Filter);

            //Get Jobs by Id
            var thisJob = _claimRepository
               .GetAll().Where(c => c.Id == Id)
               .FirstOrDefault();

            //Get Vehicle by Id
            var thisVeh = _brVehicleRepository
               .GetAll().Where(c => c.Id == thisJob.VehicleID)
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
                .GetAll().Where(m => m.Id == thisVeh.ModelId)
                .FirstOrDefault();

            //Get Make/Manufacture by Id
            var thisMake = _manufactureRepository
                .GetAll().Where(m => m.Id == thisVeh.MakeId)
                .FirstOrDefault();

            //Get Job Status

            var thisJobStatus = _jobstatusRepository
                .GetAll().Where(js => js.Id == thisJob.JobStatusID)
                .FirstOrDefault();

            var thisPaintType = _paintTypesRepository
                .GetAll().Where(pt => pt.Id == thisVeh.PaintTypeId)
                .FirstOrDefault();

            //get users

            var ExistingCsa = _userRepository.GetAll().Where(u => u.Id == thisJob.CSAID).FirstOrDefault();
            var ExistingClaimHandler = _userRepository.GetAll().Where(u => u.Id == thisJob.ClaimHandlerID).FirstOrDefault();
            var ExistingEstimator = _userRepository.GetAll().Where(u => u.Id == thisJob.EstimatorID).FirstOrDefault();
            var ExistingPartsBuyer = _userRepository.GetAll().Where(u => u.Id == thisJob.PartsBuyerID).FirstOrDefault();

            var finalQuery = (new BranchClaimListDto
            {
                ClientID = thisclient.Id,
                Name = thisclient.Name,
                Surname = thisclient.Surname,
                Email = thisclient.Email,
                Tel = thisclient.Tel,

                Id = thisVeh.Id,
                Colour = thisVeh.Color,
                Year = thisVeh.Year,
                RegNo = thisVeh.RegistrationNumber,
                VinNumber = thisVeh.VinNumber,
                VehicleCode = thisVeh.VehicleCode,
                mmCode = thisVeh.MM_Code,
                VehicleOtherInfo = thisVeh.OtherInformation,
                IsLuxury = thisVeh.IsLuxury,
                IsSpecialisedType = thisVeh.IsSpecialisedType,
                
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
                New_Comeback = thisJob.New_Comeback,
                UnderWaranty = thisJob.UnderWaranty,
                BranchEntryMethod = thisJob.BranchEntryMethod,
                IsUnrelatedDamageReason = thisJob.IsUnrelatedDamangeReason,
                ShopAllocation = thisJob.ShopAllocation,
                HighPriority = thisJob.HighPriority,
                Contents = thisJob.Contents,
                JobNotProceeding = thisJob.JobNotProceeding,
                CurrentKMs = thisJob.CurrentKMs,
                OtherInformation = thisJob.OtherInformation,
                DamageReason = thisJob.DamangeReason,
                CsaID = thisJob.CSAID,
                CsaDesc = ExistingCsa == null ? " " : ExistingCsa.Name,               
                ClaimHandlerID = thisJob.ClaimHandlerID,
                ClaimHandlerDesc = ExistingClaimHandler == null ? " " : ExistingClaimHandler.Name,
                EstimatorID = thisJob.EstimatorID,
                EstimatorDesc = ExistingEstimator == null ? " " : ExistingEstimator.Name,
                PartsBuyerID = thisJob.PartsBuyerID,
                PartsBuyerDesc = ExistingPartsBuyer == null ? " " : ExistingPartsBuyer.Name,
                ShopAllocationID = thisJob.ShopAllocation,
                ClaimAdministrator = thisJob.ClaimAdministrator,
                ClaimNumber = thisJob.ClaimNumber,
                InsuranceOtherInfo = thisJob.InsuranceOtherInfo,
                PolicyNumber = thisJob.PolicyNumber,
                PaintTypeId = thisPaintType.Id,
                PaintTypeDesc = thisPaintType.PaintType                
                

            }).MapTo<BranchClaimListDto>();

            return finalQuery;

        }

        public void UpdateVehicleInfo(BranchClaimListDto input)
        {
            try
            {
                var vehicle = _brVehicleRepository.Get(input.VehicleId);

                vehicle.RegistrationNumber = input.RegNo;
                vehicle.VinNumber = input.VinNumber;
                vehicle.MakeId = input.ManufactureID;
                vehicle.ModelId = input.ModelID;
                vehicle.Color = input.Colour;
                vehicle.Year = input.Year;
                vehicle.PaintTypeId = input.PaintTypeId;
                vehicle.VehicleCode = input.VehicleCode;
                vehicle.MM_Code = input.mmCode;
                vehicle.OtherInformation = input.VehicleOtherInfo;
                vehicle.IsLuxury = input.IsLuxury;
                vehicle.IsSpecialisedType = input.IsSpecialisedType;

                _brVehicleRepository.Update(vehicle);

            }
            catch (Exception x)
            {
                throw x;
            }
        }


        public void UpdateJobInfo(BranchClaimListDto input)
        {
            try
            {

                var jobs = _claimRepository.Get(input.Id);
                jobs.JobStatusID = input.JobStatusID;
                jobs.BranchEntryMethod = input.BranchEntryMethod;
                jobs.CSAID = input.CsaID;
                jobs.ClaimHandlerID = input.ClaimHandlerID;
                jobs.EstimatorID = input.EstimatorID;
                jobs.PartsBuyerID = input.PartsBuyerID;
                jobs.ShopAllocation = input.ShopAllocationID;
                jobs.DamangeReason = input.DamageReason;
                jobs.CurrentKMs = input.CurrentKMs;
                jobs.OtherInformation = input.OtherInformation;
                jobs.New_Comeback = input.New_Comeback;
                jobs.UnderWaranty = input.UnderWaranty;
                jobs.IsUnrelatedDamangeReason = input.IsUnrelatedDamageReason;
                jobs.JobNotProceeding = input.JobNotProceeding;
                jobs.HighPriority = input.HighPriority;
                jobs.Contents = input.Contents;

                jobs.InsuranceID = input.InsuranceID;
                jobs.BrokerID = input.BrokerID;
                jobs.ClaimAdministrator = input.ClaimAdministrator;
                jobs.ClaimNumber = input.ClaimNumber;
                jobs.InsuranceOtherInfo = input.InsuranceOtherInfo;
                jobs.PolicyNumber = input.PolicyNumber;

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
            int country_id = GetCountryIdByCode();

            var insurance = _InsuranceRepository
                .GetAll()
                .Where(c => c.CountryID == country_id)
                .OrderBy(p => p.InsurerName)
                .ToList();

            return new ListResultDto<InsurersDto>(ObjectMapper.Map<List<InsurersDto>>(insurance));
        }

        public ListResultDto<BrokersDto> GetBrokers()
        {
            int country_id = GetCountryIdByCode();

            var broker = _brokerRepository
                .GetAll()
                .Where(b => b.CountryID == country_id)
                .OrderBy(p => p.BrokerName)
                .ToList();

            return new ListResultDto<BrokersDto>(ObjectMapper.Map<List<BrokersDto>>(broker));
        }


        public ListResultDto<JobStatusDto> GetJobStatuses_1(int jobStatusID)
        {
           
            var queryjobstatus = _jobstatusRepository.GetAll().Where(t => t.Id != (jobStatusID)).ToList();
            
            var query = (from j in queryjobstatus                     

                         select new JobStatusDto
                         {
                             Id = j.Id,                            
                             Jobstatus = j.Description
                            
                         })                
            .OrderBy(p => p.Id)
            .ToList();

            return new ListResultDto<JobStatusDto>(ObjectMapper.Map<List<JobStatusDto>>(query));
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
