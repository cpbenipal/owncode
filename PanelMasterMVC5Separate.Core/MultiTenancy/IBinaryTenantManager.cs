using System;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.MultiTenancy
{
    public interface IBinaryTenantManager
    {
        Task<TenantCompanyLogo> GetOrNullAsync(Guid id);
        
        Task SaveAsync(TenantCompanyLogo file);
        
        Task DeleteAsync(Guid id);
    }
}