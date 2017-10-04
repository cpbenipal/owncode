using System.Data.SqlClient;
using Shouldly;
using Xunit;

namespace PanelMasterMVC5Separate.Tests.General
{
    public class ConnectionString_Tests
    {
        [Fact]
        public void SqlConnectionStringBuilder_Test()
        {
            var csb = new SqlConnectionStringBuilder("Server=localhost; Database=PanelMasterMVC5Separate; Trusted_Connection=True;");
            csb["Database"].ShouldBe("PanelMasterMVC5Separate");
        }
    }
}
