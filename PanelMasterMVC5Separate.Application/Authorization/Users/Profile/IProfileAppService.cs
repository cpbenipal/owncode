using System.Threading.Tasks;
using Abp.Application.Services;
using PanelMasterMVC5Separate.Authorization.Claim.Profile.Dto;

namespace PanelMasterMVC5Separate.Authorization.Claim.Profile
{
    public interface IProfileAppService : IApplicationService
    {
        Task<CurrentUserProfileEditDto> GetCurrentUserProfileForEdit();

        Task UpdateCurrentUserProfile(CurrentUserProfileEditDto input);
        
        Task ChangePassword(ChangePasswordInput input);

        Task UpdateProfilePicture(UpdateProfilePictureInput input);

        Task<GetPasswordComplexitySettingOutput> GetPasswordComplexitySetting();
    }
}
