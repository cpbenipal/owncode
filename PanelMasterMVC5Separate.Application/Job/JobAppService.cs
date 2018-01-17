using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Runtime.Session;
using PanelMasterMVC5Separate.Brokers;
using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.Clients;
using PanelMasterMVC5Separate.Insurer;
using PanelMasterMVC5Separate.Job.Dto;
using PanelMasterMVC5Separate.MultiTenancy;
using PanelMasterMVC5Separate.Tenants.Brokers.Dto;
using PanelMasterMVC5Separate.Tenants.Insurer.Dto;
using PanelMasterMVC5Separate.Tenants.Manufacturing.Dto;
using PanelMasterMVC5Separate.Vendors;
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
        private readonly IRepository<Jobs> _jobsRepository;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<PaintTypes> _painttypesrepository;
        private readonly IRepository<BrVehicle> _brvehiclerepository;
        private readonly IRepository<VehicleInsurance> _vehicleinsurancerepository;
        private readonly IRepository<TenantProfile> _TenantProfile;
        private readonly IRepository<Countries> _countryRepository;
        private readonly IAbpSession _abpSession;
        public JobAppService(IAbpSession abpSession, IRepository<VehicleMake> manufactureRepository, IRepository<VehicleModels> vehiclemodelRepository
                             , IRepository<InsurerMaster> insurersRepository, IRepository<BrokerMaster> brokerRepository
                             , IRepository<Jobs> jobsRepository, IRepository<Client> clientRepository,
            IRepository<PaintTypes> painttypesrepository, IRepository<BrVehicle> brvehiclerepository,
            IRepository<VehicleInsurance> vehicleinsurancerepository, IRepository<TenantProfile> tenantprofile,
            IRepository<Countries> countryRepository)
        {
            _abpSession = abpSession;
            _manufactureRepository = manufactureRepository;
            _vehiclemodelRepository = vehiclemodelRepository;
            _insurersRepository = insurersRepository;
            _brokerRepository = brokerRepository;
            _jobsRepository = jobsRepository;
            _clientRepository = clientRepository;
            _painttypesrepository = painttypesrepository;
            _brvehiclerepository = brvehiclerepository;
            _vehicleinsurancerepository = vehicleinsurancerepository;
            _TenantProfile = tenantprofile;
            _countryRepository = countryRepository;
        }

        public ImportDto ImportExistingData(GetClaimsInput input)
        {
            ImportDto importDto = new ImportDto();
            var query = _clientRepository.GetAll()
              .Where(p =>
                   p.IdNumber.Equals(input.FilterText) ||
                   p.Tel.Equals(input.FilterText) ||
                   p.Email.Equals(input.FilterText))
             .FirstOrDefault();

            if (query != null)
            {
                importDto.Id = query.Id;
                importDto.Name = query.Name;
                importDto.Surname = query.Surname;
                importDto.Title = query.Title;
                importDto.Email = query.Email;
                importDto.Tel = query.Tel;
                importDto.IdNumber = query.IdNumber;
                importDto.CommunicationType = query.CommunicationType;
                importDto.ContactAfterService = query.ContactAfterService;
                importDto.ClientOtherInformation = query.OtherInformation;

            }
            var query1 = _brvehiclerepository.GetAll()
              .Where(p => p.RegistrationNumber.Equals(input.FilterText1) || p.VinNumber.Equals(input.FilterText1))
             .FirstOrDefault();

            if (query1 != null)
            {
                importDto.MakeId = query1.MakeId;
                importDto.ModelId = query1.ModelId;
                importDto.Colour = query1.Color;
                importDto.PaintTypeId = query1.PaintTypeId;
                importDto.Year = query1.Year;
                importDto.RegistrationNumber = query1.RegistrationNumber;
                importDto.VinNumber = query1.VinNumber;
                //importDto.UnderWaranty = query1.UnderWaranty;
                importDto.IsSpecialisedType = query1.IsSpecialisedType;
                importDto.IsLuxury = query1.IsLuxury;
                importDto.VehicleOtherInformation = query1.OtherInformation;
                importDto.VId = query1.Id;
            }
            return importDto;
        }

        public ListResultDto<VehicleMakeDto> GetManufacture()
        {
            var manufacture = _manufactureRepository
                 .GetAll().Where(c => c.IsActive == true)
                .OrderBy(p => p.Description)
                .ToList();

            return new ListResultDto<VehicleMakeDto>(ObjectMapper.Map<List<VehicleMakeDto>>(manufacture));
        }

        public ListResultDto<PaintTypesDto> GetPaintType()
        {
            var paints = _painttypesrepository
                .GetAll()
                .OrderBy(p => p.PaintType)
                .ToList();

            return new ListResultDto<PaintTypesDto>(ObjectMapper.Map<List<PaintTypesDto>>(paints));
        }

        public ListResultDto<ModelMadeListDto> GetVehicleModel(GetVehicleModelInput input)
        {
            int make_id = Convert.ToInt32(input.Filter);

            var vehicleModel = _vehiclemodelRepository
                .GetAll()
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.VehicleMakeID.Equals(make_id)
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
            int countryid = GetCountryIdByCode();
            var insurerMaster = _insurersRepository.GetAll()
                .Where(p => p.CountryID == countryid)
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
            int countryid = GetCountryIdByCode();
            var broker = _brokerRepository
                .GetAll()
                .Where(p => p.CountryID == countryid)
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

        public async Task<Client> AddClient(CreateClientInput input)
        {
            var client = input.MapTo<Client>();
            return await _clientRepository.InsertAsync(client);
        }

        public async Task CreateJob(CreateJobInput input)
        {
            var job = input.MapTo<Jobs>();

            await _jobsRepository.InsertAsync(job);
        }

        public void CreateNewJob(Accident clientDto)
        {
            //var a = await _brokerRepository.FirstOrDefaultAsync(1);
            var clients = new Client()
            {
                Id = clientDto.Id,
                Name = clientDto.Name,
                Surname = clientDto.Surname,
                Title = clientDto.Title,
                Email = clientDto.Email,
                Tel = clientDto.Tel,
                IdNumber = clientDto.IdNumber,
                CommunicationType = clientDto.CommunicationType,
                ContactAfterService = clientDto.ContactAfterService,
                OtherInformation = clientDto.ClientOtherInformation
            };

            int id = _clientRepository.InsertOrUpdateAndGetId(clients);


            var vehicle = new BrVehicle()
            {
                Id = clientDto.VId,
                MakeId = clientDto.MakeId,
                ModelId = clientDto.ModelId,
                Color = clientDto.Colour,
                PaintTypeId = clientDto.PaintTypeId,
                Year = clientDto.Year,
                RegistrationNumber = clientDto.RegistrationNumber,
                VinNumber = clientDto.VinNumber,
                //UnderWaranty = clientDto.UnderWaranty,
                IsSpecialisedType = clientDto.IsSpecialisedType,
                IsLuxury = clientDto.IsLuxury,
                OtherInformation = clientDto.VehicleOtherInformation,
                TenantId = _abpSession.TenantId
            };
            int vehicleId = _brvehiclerepository.InsertOrUpdateAndGetId(vehicle);

            var jobs = new Jobs()
            {
                ClientID = id,
                //ManufactureID = clientDto.MakeId,
                //ModelID = clientDto.ModelId,
                //Year = clientDto.Year,
                //RegNo = clientDto.RegistrationNumber,
                //VinNumber = clientDto.VinNumber,
                CurrentKMs = clientDto.CurrentKMs,
                DamangeReason = clientDto.DamangeReason,
                BranchEntryMethod = clientDto.BranchEntryMethod,
                IsUnrelatedDamangeReason = clientDto.IsUnrelatedDamangeReason,
                InsuranceID = clientDto.InsurerId,
                BrokerID = clientDto.BrokerId,
                // Colour = clientDto.Colour,
                // UnderWaranty = clientDto.UnderWaranty ? "Yes": "No",
                OtherInformation = clientDto.RepairOtherInformation,
                VehicleID = vehicleId,
                TenantID = _abpSession.TenantId,
                CSAID = clientDto.CSAID,
                JobStatusID = clientDto.JobStatusID,
                ClaimHandlerID = clientDto.ClaimHandlerID,
                PartsBuyerID = clientDto.PartsBuyerID,
                KeyAccountManagerID = clientDto.KeyAccountManagerID,
                EstimatorID = clientDto.EstimatorID,
                New_Comeback = clientDto.New_Comeback,
                UnderWaranty = clientDto.UnderWaranty
            };

            id = _jobsRepository.InsertOrUpdateAndGetId(jobs);

            var quote = new VehicleInsurance()
            {
                BrokerId = clientDto.BrokerId,
                ClaimAdministrator = clientDto.ClaimAdministrator,
                InsurerId = clientDto.InsurerId,
                PolicyNumber = clientDto.PolicyNumber,
                ClaimNumber = clientDto.ClaimNumber,
                OtherInformation = clientDto.InsurerOtherInformation
            };

            _vehicleinsurancerepository.InsertOrUpdate(quote);
        }

        private int GetCountryIdByCode()
        {
            var CountryCode = _TenantProfile.FirstOrDefault(x => x.TenantId == _abpSession.TenantId);
            return (CountryCode == null ? 0 : _countryRepository.FirstOrDefault(x => x.Code == CountryCode.CountryCode).Id);
        }
    }
}
