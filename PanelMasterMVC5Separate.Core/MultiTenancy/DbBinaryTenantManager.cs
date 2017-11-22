using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;

namespace PanelMasterMVC5Separate.MultiTenancy
{
    public class DbBinaryTenantManager : IBinaryTenantManager, ITransientDependency
    {
        private readonly IRepository<TenantCompanyLogo, Guid> _binaryObjectRepository;

        public DbBinaryTenantManager(IRepository<TenantCompanyLogo, Guid> binaryObjectRepository)
        {
            _binaryObjectRepository = binaryObjectRepository;
        }

        public Task<TenantCompanyLogo> GetOrNullAsync(Guid id)
        {
            return _binaryObjectRepository.FirstOrDefaultAsync(id);
        }

        public Task SaveAsync(TenantCompanyLogo file)
        {
            var logoObject = _binaryObjectRepository.FirstOrDefault(x=>x.CompanyId == file.CompanyId);

            if (logoObject.CompanyId.HasValue)
            {
                _binaryObjectRepository.DeleteAsync(logoObject);
            }
            return _binaryObjectRepository.InsertAsync(file);
        }

        public Task DeleteAsync(Guid id)
        {
            return _binaryObjectRepository.DeleteAsync(id);
        }
    }
}