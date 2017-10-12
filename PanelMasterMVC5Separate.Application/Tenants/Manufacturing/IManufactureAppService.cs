using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.Tenants.Manufacturing.Dto;
using Abp.Application.Services;
using PanelMasterMVC5Separate.Vehicle;
using PanelMasterMVC5Separate.Dto;

namespace PanelMasterMVC5Separate.Tenants.Manufacturing
{
    public interface IManufactureAppService : IApplicationService
    {
        ListResultDto<MakelListDto> GetVehicleInformation(GetVehicleInput input);
        ListResultDto<ModelMadeListDto> GetVehicleMades(GetVehicleInput input);
        Task<VehicleModelLogos> GetOrNullAsync(int id);
        Task CreateMake(VehicleMakeDto input);
        VehicleMakeDto GetThisMake(GetVehicleInput input);
        VehicleFromModelsDto GetThisMade(GetVehicleInput input);
        ListResultDto<VehicleMakeDto> GetMakes();
        Task UpdateMake(MakeUpDto input);
        void ChangeStatus(StatusVehicleDto input);
        void CreateOrUpdateMade(VehicleModelsDto input);
        Task<FileDto> GetMakesToExcel();
        Task<FileDto> GetMadesToExcel();
    }
}