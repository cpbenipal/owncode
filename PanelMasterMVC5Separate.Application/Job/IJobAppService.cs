using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.Clients;
using PanelMasterMVC5Separate.Job.Dto;
using PanelMasterMVC5Separate.Tenants.Estimators.Dto;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Vehicle
{
    public interface IJobAppService : IApplicationService
    {
        ListResultDto<ManufactureListDto> GetManufacture();
        ListResultDto<VehicleModelListDto> GetVehicleModel(GetVehicleModelInput input);
        ListResultDto<InsuranceListDto> GetInsurances();
        ListResultDto<BrokerListDto> GetBrokers();
        ListResultDto<EstimatorListDto> GetEstimators();
        Task CreateJob(CreateJobInput input);
        Task<Client> AddClient(CreateClientInput input);
    }
}