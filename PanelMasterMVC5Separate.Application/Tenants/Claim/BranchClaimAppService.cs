﻿using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using PanelMasterMVC5Separate.Authorization;
using PanelMasterMVC5Separate.Authorization.Claim.Dto;
using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.Dto;
using PanelMasterMVC5Separate.Tenants.Claim.Dto;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.Authorization.Roles;
using PanelMasterMVC5Separate.Authorization.Claim.Exporting;
using PanelMasterMVC5Separate.Tenants.Claim.Exporting;
using PanelMasterMVC5Separate.Clients;
using PanelMasterMVC5Separate.Job;
using PanelMasterMVC5Separate.Job.Dto;
using PanelMasterMVC5Separate.Vehicle;
using PanelMasterMVC5Separate.Insurer;
using PanelMasterMVC5Separate.Tenants.Insurer.Dto;
using PanelMasterMVC5Separate.Brokers;
using PanelMasterMVC5Separate.Tenants.Brokers.Dto;

namespace PanelMasterMVC5Separate.Tenants.Claim
{

    public class BranchClaimAppService : PanelMasterMVC5SeparateAppServiceBase, IBranchClaimAppService
    {

        private readonly IClaimsListExcelExporter _claimListExcelExporter;
        private readonly IRepository<Jobs> _claimRepository;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<InsurerMaster> _InsuranceRepository;

        private readonly IRepository<VehicleMake> _manufactureRepository;
        private readonly IRepository<VehicleModels> _vehicleModelRepository;
        private readonly IRepository<BrokerMaster> _brokerRepository;
        private readonly IRepository<BranchClaimStatus> _claimStatusRepository;

        private readonly IRepository<Jobstatus> _jobstatusRepository;
        private readonly IRepository<JobstatusMask> _jobstatusmaskRepository;
        private readonly IRepository<JobstatusTenant> _jobstatustenantRepository;

        public BranchClaimAppService(IClaimsListExcelExporter claimListExcelExporter,
                                     IRepository<Jobs> claimRepository, IRepository<Client> clientRepository,
                                     IRepository<InsurerMaster> InsuranceRepository, IRepository<VehicleMake> manufactureRepository,
                                     IRepository<BrokerMaster> brokerRepository, IRepository<VehicleModels> vehicleModelRepository,
                                     IRepository<BranchClaimStatus> claimStatusRepository
            , IRepository<Jobstatus> jobstatusRepository, IRepository<JobstatusMask> jobstatusmaskRepository, IRepository<JobstatusTenant> jobstatustenantRepository)
        {
            _claimListExcelExporter = claimListExcelExporter;
            _claimRepository = claimRepository;
            _clientRepository = clientRepository;
            _InsuranceRepository = InsuranceRepository;
            _manufactureRepository = manufactureRepository;
            _brokerRepository = brokerRepository;
            _vehicleModelRepository = vehicleModelRepository;
            _claimStatusRepository = claimStatusRepository;
            _jobstatusRepository = jobstatusRepository;
            _jobstatusmaskRepository = jobstatusmaskRepository;
            _jobstatustenantRepository = jobstatustenantRepository;
        }

        public ListResultDto<BranchClaimListDto> GetClaims(GetClaimsInput input)
        {
            var queryjobs = _claimRepository.GetAll().ToList();

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
        public async Task<FileDto> GetClaimsToExcel()
        {
            var claims = await _claimRepository.GetAll().ToListAsync();
            var claimListDtos = claims.MapTo<List<BranchClaimListDto>>();

            return _claimListExcelExporter.ExportToFile(claimListDtos);
        }


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

            //Get Job Status by Id
            var thisClaimStatus = _claimStatusRepository
                .GetAll().Where(m => m.Id == thisJob.ClaimStatusID)
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

                BrokerID = thisBroker.Id,
                Broker = thisBroker.BrokerName,

                InsuranceID = thisInsurance.Id,
                Insurance = thisInsurance.InsurerName,

                ClaimStatusID = thisClaimStatus.Id,
                ClaimStatusDescription = thisClaimStatus.Description,

                ManufactureID = thisMake.Id,
                Manufacture = thisMake.Description,

                ModelID = thisModel.Id,
                Model = thisModel.Model

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
            var querystatustenant = _jobstatustenantRepository.GetAll().ToList();

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
            .OrderByDescending(p => p.CreationTime)
            .ThenBy(p => p.Jobstatus)
            .ToList();

            return new ListResultDto<JobStatusDto>(ObjectMapper.Map<List<JobStatusDto>>(query));

        }
        public async Task<FileDto> GetJobStatusToExcel()
        {
            var querystatustenant = await _jobstatustenantRepository.GetAll().ToListAsync();
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
            .OrderByDescending(p => p.CreationTime)
            .ThenBy(p => p.Jobstatus)
            .ToList();
            var claimListDtos = query.MapTo<List<JobStatusDto>>();
            return _claimListExcelExporter.ExportToFile(claimListDtos);
        }
        public ListResultDto<JobStatusMasksDto> GetJobStatusMasks()
        {
            var status = _jobstatusmaskRepository
                .GetAll()
                .Where(p => p.IsDeleted == false)
                .ToList();

            return new ListResultDto<JobStatusMasksDto>(ObjectMapper.Map<List<JobStatusMasksDto>>(status));
        }
        // By jobstaticId
        public async Task<JobstatusTenantDto> GetJobStatusForEdit(GetJobInput input)
        {
            var output = new JobstatusTenantDto();
            var ifexist = _jobstatustenantRepository.FirstOrDefault(p => p.JobStatusID == input.id);
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
             .GetAll().Where(c => c.Id == Id)
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
    }
}
