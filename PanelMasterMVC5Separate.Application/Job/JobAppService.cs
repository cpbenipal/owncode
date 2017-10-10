using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.Clients;
using PanelMasterMVC5Separate.Estimations;
using PanelMasterMVC5Separate.Insurance_Brokers;
using PanelMasterMVC5Separate.Job.Dto;
using PanelMasterMVC5Separate.Tenants.Estimators.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Vehicle
{
    public class JobAppService : PanelMasterMVC5SeparateAppServiceBase, IJobAppService
    {
        private readonly IRepository<Manufacture> _manufactureRepository;
        private readonly IRepository<VehicleModel> _vehiclemodelRepository;
        private readonly IRepository<Insurance> _insuranceRepository;
        private readonly IRepository<Broker> _brokerRepository;
        private readonly IRepository<Estimator> _estimatorRepository;
        private readonly IRepository<Jobs> _jobsRepository;
        private readonly IRepository<Client> _clientRepository;

        public JobAppService(IRepository<Manufacture> manufactureRepository, IRepository<VehicleModel> vehiclemodelRepository
                             ,IRepository<Insurance> insuranceRepository, IRepository<Broker> brokerRepository
                             ,IRepository<Estimator> estimatorRepository, IRepository<Jobs> jobsRepository, IRepository<Client> clientRepository)
        {
            _manufactureRepository = manufactureRepository;
            _vehiclemodelRepository = vehiclemodelRepository;
            _insuranceRepository = insuranceRepository;
            _brokerRepository = brokerRepository;
            _estimatorRepository = estimatorRepository;
            _jobsRepository = jobsRepository;
            _clientRepository = clientRepository;
        }
        public ListResultDto<ManufactureListDto> GetManufacture()
        {
            var manufacture = _manufactureRepository
                .GetAll()                
                .OrderBy(p => p.Manufacture_Desc)                
                .ToList();

            return new ListResultDto<ManufactureListDto>(ObjectMapper.Map<List<ManufactureListDto>>(manufacture));
        }

        public ListResultDto<ClaimStatusListDto> GetClaimStatuses()
        {
            var claim_status = _manufactureRepository
                .GetAll()
                .OrderBy(p => p.Manufacture_Desc)
                .ToList();

            return new ListResultDto<ClaimStatusListDto>(ObjectMapper.Map<List<ClaimStatusListDto>>(claim_status));
        }

        public ListResultDto<VehicleModelListDto> GetVehicleModel(GetVehicleModelInput input)
        {
            var vehicleModel = _vehiclemodelRepository
                .GetAll()
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.ManufactureID.Equals( Convert.ToInt32(input.Filter))                       
                )
                .OrderBy(p => p.Model_Desc)               
                .ToList();

            return new ListResultDto<VehicleModelListDto>(ObjectMapper.Map<List<VehicleModelListDto>>(vehicleModel));
        }

        public ListResultDto<InsuranceListDto> GetInsurances()
        {
            var insurance = _insuranceRepository
                .GetAll()
                .OrderBy(p => p.Insurance_Desc)
                .ToList();

            return new ListResultDto<InsuranceListDto>(ObjectMapper.Map<List<InsuranceListDto>>(insurance));
        }

        public ListResultDto<BrokerListDto> GetBrokers()
        {
            var broker = _brokerRepository
                .GetAll()
                .OrderBy(p => p.Broker_Desc)
                .ToList();

            return new ListResultDto<BrokerListDto>(ObjectMapper.Map<List<BrokerListDto>>(broker));
        }

        public ListResultDto<EstimatorListDto> GetEstimators()
        {
            var estimator = _estimatorRepository
                .GetAll()
                .OrderBy(p => p.Estimator_Desc)
                .ToList();

            return new ListResultDto<EstimatorListDto>(ObjectMapper.Map<List<EstimatorListDto>>(estimator));
        }

        public async Task<Client> AddClient(CreateClientInput input)
        {
            var client = input.MapTo<Client>();
            return await _clientRepository.InsertAsync(client);
        }

        public async Task CreateJob(CreateJobInput input)
        {
            var job = input.MapTo<Jobs>();

            job.ClaimStatusID = 3;

            await _jobsRepository.InsertAsync(job);
        }

        
    }

}
