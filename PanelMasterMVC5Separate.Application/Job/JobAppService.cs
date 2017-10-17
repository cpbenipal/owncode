using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using PanelMasterMVC5Separate.Brokers;
using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.Clients;
using PanelMasterMVC5Separate.Estimations;
using PanelMasterMVC5Separate.Insurer;
using PanelMasterMVC5Separate.Job.Dto;
using PanelMasterMVC5Separate.Tenants.Brokers.Dto;
using PanelMasterMVC5Separate.Tenants.Estimators.Dto;
using PanelMasterMVC5Separate.Tenants.Insurer.Dto;
using PanelMasterMVC5Separate.Tenants.Manufacturing.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Vehicle
{
    public class JobAppService : PanelMasterMVC5SeparateAppServiceBase, IJobAppService
    {
        private readonly IRepository<VehicleMake> _manufactureRepository;
        private readonly IRepository<VehicleModels> _vehiclemodelRepository;

        private readonly IRepository<InsurerMaster> _insurersRepository;       
        private readonly IRepository<BrokerMaster> _brokerRepository;
        private readonly IRepository<Estimator> _estimatorRepository;
        private readonly IRepository<Jobs> _jobsRepository;
        private readonly IRepository<Client> _clientRepository;

        public JobAppService(IRepository<VehicleMake> manufactureRepository, IRepository<VehicleModels> vehiclemodelRepository
                             , IRepository<InsurerMaster> insurersRepository,IRepository<BrokerMaster> brokerRepository
                             ,IRepository<Estimator> estimatorRepository, IRepository<Jobs> jobsRepository, IRepository<Client> clientRepository)
        {
            _manufactureRepository = manufactureRepository;
            _vehiclemodelRepository = vehiclemodelRepository;
            _insurersRepository = insurersRepository;
          
            _brokerRepository = brokerRepository;
            _estimatorRepository = estimatorRepository;
            _jobsRepository = jobsRepository;
            _clientRepository = clientRepository;
        }
        public ListResultDto<VehicleMakeDto> GetManufacture()
        {
            var manufacture = _manufactureRepository
                .GetAll()                
                .OrderBy(p => p.Description)                
                .ToList();

            return new ListResultDto<VehicleMakeDto>(ObjectMapper.Map<List<VehicleMakeDto>>(manufacture));
        }

        public ListResultDto<ClaimStatusListDto> GetClaimStatuses()
        {
            var claim_status = _manufactureRepository
                .GetAll()
                .OrderBy(p => p.Description)
                .ToList();

            return new ListResultDto<ClaimStatusListDto>(ObjectMapper.Map<List<ClaimStatusListDto>>(claim_status));
        }

        public ListResultDto<ModelMadeListDto> GetVehicleModel(GetVehicleModelInput input)
        {
            var vehicleModel = _vehiclemodelRepository
                .GetAll()
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.Id.Equals( Convert.ToInt32(input.Filter))                       
                )
                .OrderBy(p => p.Model)               
                .ToList();

           
            var newList = new List<ModelMadeListDto>();
            foreach (VehicleModels model_obj in vehicleModel)
            {
                newList.Add(new ModelMadeListDto
                {
                    MadeID = model_obj.Id,
                    Make = model_obj.VehicleMake.Description,
                    Model = model_obj.Model,
                    MMCode = model_obj.MMCode                 
                });
            }

            return new ListResultDto<ModelMadeListDto>(newList);

        }

        public ListResultDto<GetInsurersDto> GetInsurances()
        {
            var insurerMaster = _insurersRepository.GetAll()            
             .OrderByDescending(p => p.InsurerName)
             .ToList();
            var newList = new List<GetInsurersDto>();
            foreach (InsurerMaster ins_obj in insurerMaster)
            {
                newList.Add(new GetInsurersDto
                {
                    Id = ins_obj.Id,
                    InsurerName = ins_obj.InsurerName,
                    Mask = ins_obj.Mask
                });
            }

            return new ListResultDto<GetInsurersDto>(newList);
        }
        
        public ListResultDto<GetBrokersDto> GetBrokers()
        {
            var broker = _brokerRepository
                .GetAll()
                .OrderBy(p => p.BrokerName)
                .ToList();

            var newList = new List<GetBrokersDto>();
            foreach (BrokerMaster broker_obj in broker)
            {
                newList.Add(new GetBrokersDto
                {
                    Id = broker_obj.Id,
                    BrokerName = broker_obj.BrokerName,
                    Mask = broker_obj.Mask
                });
            }

            return new ListResultDto<GetBrokersDto>(newList);
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

            job.ClaimStatusID = 1;

            await _jobsRepository.InsertAsync(job);
        }

        
    }

}
