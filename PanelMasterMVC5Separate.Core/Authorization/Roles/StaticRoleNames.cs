namespace PanelMasterMVC5Separate.Authorization.Roles
{
    public static class StaticRoleNames
    {
        public static class Host
        {
            public const string Admin = "Admin";
        }

        public static class Tenants
        {
            public const string None = "None";

            public const string Admin = "Admin";

            public const string User = "User";

            public const string Claims_Handler = "Claims Handler";

            public const string CSA = "Customer Service Advisor";

            public const string Parts_Buyer = "Parts Buyer";

            public const string Estimator = "Estimator";

            public const string Key_Accounts_Manager = "Key Accounts Manager";

            public const string Swithchboard = "Swithchboard";

            public const string Parts_Receiver = "Parts Receiver";

            public const string Costing_Clerk = "Costing Clerk";

            public const string Financial_Manager = "Financial Manager";

            public const string Insurer = "Insurer";

            public const string Broker = "Broker";

        }
    }
}