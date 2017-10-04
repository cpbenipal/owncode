using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PanelMasterMVC5Separate.Auditing;
using Shouldly;
using Xunit;

namespace PanelMasterMVC5Separate.Tests.Auditing
{
    public class NamespaceStripper_Tests: AppTestBase
    {
        private readonly INamespaceStripper _namespaceStripper;

        public NamespaceStripper_Tests()
        {
            _namespaceStripper = Resolve<INamespaceStripper>();
        }

        [Fact]
        public void Should_Stripe_Namespace()
        {
            var controllerName = _namespaceStripper.StripNameSpace("PanelMasterMVC5Separate.Web.Controllers.HomeController");
            controllerName.ShouldBe("HomeController");
        }

        [Theory]
        [InlineData("PanelMasterMVC5Separate.Auditing.GenericEntityService`1[[PanelMasterMVC5Separate.Storage.BinaryObject, PanelMasterMVC5Separate.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null]]", "GenericEntityService<BinaryObject>")]
        [InlineData("CompanyName.ProductName.Services.Base.EntityService`6[[CompanyName.ProductName.Entity.Book, CompanyName.ProductName.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[CompanyName.ProductName.Services.Dto.Book.CreateInput, N...", "EntityService<Book, CreateInput>")]
        [InlineData("PanelMasterMVC5Separate.Auditing.XEntityService`1[PanelMasterMVC5Separate.Auditing.AService`5[[PanelMasterMVC5Separate.Storage.BinaryObject, PanelMasterMVC5Separate.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[PanelMasterMVC5Separate.Storage.TestObject, PanelMasterMVC5Separate.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],]]", "XEntityService<AService<BinaryObject, TestObject>>")]
        public void Should_Stripe_Generic_Namespace(string serviceName, string result)
        {
            var genericServiceName = _namespaceStripper.StripNameSpace(serviceName);
            genericServiceName.ShouldBe(result);
        }
    }
}
