﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.Clients;
using PanelMasterMVC5Separate.Job.Dto;
using PanelMasterMVC5Separate.Tenants.Brokers.Dto;
using PanelMasterMVC5Separate.Tenants.Estimators.Dto;
using PanelMasterMVC5Separate.Tenants.Insurer.Dto;
using PanelMasterMVC5Separate.Tenants.Manufacturing.Dto;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Vehicle
{
    public interface IJobAppService : IApplicationService
    {
        ListResultDto<VehicleMakeDto> GetManufacture();
        ListResultDto<ModelMadeListDto> GetVehicleModel(GetVehicleModelInput input);
        //ListResultDto<InsuranceListDto> GetInsurances();
        ListResultDto<GetInsurersDto> GetInsurances();
        ListResultDto<GetBrokersDto> GetBrokers();
        ListResultDto<EstimatorListDto> GetEstimators();
        Task CreateJob(CreateJobInput input);
        Task<Client> AddClient(CreateClientInput input);
    }
}