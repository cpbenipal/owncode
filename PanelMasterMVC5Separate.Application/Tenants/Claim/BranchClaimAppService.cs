using Abp.Application.Services.Dto;
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
        
        public BranchClaimAppService(IClaimsListExcelExporter claimListExcelExporter,
                                     IRepository<Jobs> claimRepository, IRepository<Client> clientRepository,
                                     IRepository<InsurerMaster> InsuranceRepository, IRepository<VehicleMake> manufactureRepository,
                                     IRepository<BrokerMaster> brokerRepository, IRepository<VehicleModels> vehicleModelRepository,
                                     IRepository<BranchClaimStatus> claimStatusRepository)
        {
            _claimListExcelExporter = claimListExcelExporter;
            _claimRepository = claimRepository;
            _clientRepository = clientRepository;
            _InsuranceRepository = InsuranceRepository;
            _manufactureRepository = manufactureRepository;
            _brokerRepository = brokerRepository;
            _vehicleModelRepository = vehicleModelRepository;
            _claimStatusRepository = claimStatusRepository;
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
         

    }
}
