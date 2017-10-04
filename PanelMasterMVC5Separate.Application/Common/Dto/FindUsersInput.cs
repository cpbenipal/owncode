using PanelMasterMVC5Separate.Dto;

namespace PanelMasterMVC5Separate.Common.Dto
{
    public class FindUsersInput : PagedAndFilteredInputDto
    {
        public int? TenantId { get; set; }
    }
}