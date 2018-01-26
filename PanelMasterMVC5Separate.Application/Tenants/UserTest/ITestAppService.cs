using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PanelMasterMVC5Separate.Tenants.UserTest.Dto;

namespace PanelMasterMVC5Separate.Tenants.UserTest
{
    public interface ITestAppService : IApplicationService
    {
        ListResultDto<uDto> GetUser(GetBrokerInput input);
    }
}