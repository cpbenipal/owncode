using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using PanelMasterMVC5Separate.Tenants.UserTest.Dto;
using PanelMasterMVC5Separate.UserTest;
using System.Collections.Generic;
using System.Linq;

namespace PanelMasterMVC5Separate.Tenants.UserTest
{
    public class TestAppService : PanelMasterMVC5SeparateAppServiceBase, ITestAppService
    {
        private readonly IRepository<uClass> _uRepository;

        public TestAppService(IRepository<uClass> uRepository)
        {
            _uRepository = uRepository;
        }

        public ListResultDto<uDto> GetUser(GetBrokerInput input)
        {
            var query = _uRepository.GetAll()
            .WhereIf(
                !input.Filter.IsNullOrWhiteSpace(),
                u =>
                    u.FN.Contains(input.Filter) ||
                    u.LN.Contains(input.Filter) 
            )
            .OrderByDescending(p => p.LastModificationTime)
            .ToList();

            return new ListResultDto<uDto>(ObjectMapper.Map<List<uDto>>(query));
        }
    }
}


