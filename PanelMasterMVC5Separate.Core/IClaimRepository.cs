using Abp.Domain.Repositories;
using PanelMasterMVC5Separate.Authorization.Claim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate
{
    public interface IClaimRepository : IRepository<User, long>
    {
        Task<List<string>> GetUserNames();
    }
}
