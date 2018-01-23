using System.ComponentModel.DataAnnotations;

namespace PanelMasterMVC5Separate.Authorization.Claim.Dto
{
    public class CreateOrUpdateUserInput
    {
        [Required]
        public UserEditDto User { get; set; }

        [Required]
        public string[] AssignedRoleNames { get; set; }

        public bool SendActivationEmail { get; set; }

        public bool SetRandomPassword { get; set; }
    }
    public class UpdateUserRoles
    { 
        [Required]
        public string[] AssignedRoleNames { get; set; }         
    }
}