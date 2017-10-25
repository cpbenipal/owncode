using Abp.Application.Navigation;
using Abp.Localization;
using PanelMasterMVC5Separate.Authorization;
using PanelMasterMVC5Separate.Web.Navigation;

namespace PanelMasterMVC5Separate.Web.App.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class AppNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(new MenuItemDefinition(
                    PageNames.App.Host.Tenants,
                    L("Tenants"),
                    url: "host.tenants",
                    icon: "icon-globe",
                    requiredPermissionName: AppPermissions.Pages_Tenants
                    )
                ).AddItem(new MenuItemDefinition(
                    PageNames.App.Host.Editions,
                    L("Editions"),
                    url: "host.editions",
                    icon: "icon-grid",
                    requiredPermissionName: AppPermissions.Pages_Editions
                    )
                ).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Dashboard,
                    L("Dashboard"),
                    url: "tenant.dashboard",
                    icon: "icon-home",
                    requiredPermissionName: AppPermissions.Pages_Tenant_Dashboard
                    )
                )                
                .AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Quote,
                    L("Quoting"),
                    icon: "glyphicon glyphicon-tasks"
                    ).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Quotations,
                    L("ViewQuotations"),
                    url: "tenant.quoting",
                    icon: "glyphicon glyphicon-collapse-down"
                    )).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Quotations,
                    L("NewQuotation"),
                    url: "tenant.quoting",
                    icon: "glyphicon glyphicon-search"
                    ))
                )                
                .AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.PartsOrdering,
                    L("PartsOrdering"),
                    url: "tenant.partsordering",
                    icon: "glyphicon glyphicon-link"
                    )
                ).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Claim,
                    L("JobDetails"),
                    icon: "glyphicon glyphicon-tasks"
                    ).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.NewJob,
                    L("NewJob"),
                    url: "tenant.NewJob",
                    icon: "glyphicon glyphicon-collapse-down"
                    )).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.JobDetails,
                    L("Search"),
                    url: "tenant.jobdetails",
                    icon: "glyphicon glyphicon-search"
                    ))

                ).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Productivity,
                    L("Productivity"),
                    url: "tenant.productivity",
                    icon: "glyphicon glyphicon-calendar"
                    )
                ).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Towing,
                    L("Towing"),
                    url: "tenant.towing",
                    icon: "glyphicon glyphicon-book"
                    )
                ).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Costing,
                    L("Costing"),
                    url: "tenant.costing",
                    icon: "glyphicon glyphicon-usd"
                    )
                ).AddItem(new MenuItemDefinition(
                    PageNames.App.Common.Administration,
                    L("Administration"),
                    icon: "icon-wrench"
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Common.OrganizationUnits,
                        L("OrganizationUnits"),
                        url: "organizationUnits",
                        icon: "icon-layers",
                        requiredPermissionName: AppPermissions.Pages_Administration_OrganizationUnits
                        )
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Common.Roles,
                        L("Roles"),
                        url: "roles",
                        icon: "icon-briefcase",
                        requiredPermissionName: AppPermissions.Pages_Administration_Roles
                        )
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Common.Users,
                        L("Users"),
                        url: "users",
                        icon: "icon-users",
                        requiredPermissionName: AppPermissions.Pages_Administration_Users
                        )
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Common.Languages,
                        L("Languages"),
                        url: "languages",
                        icon: "icon-flag",
                        requiredPermissionName: AppPermissions.Pages_Administration_Languages
                        )
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Common.AuditLogs,
                        L("AuditLogs"),
                        url: "auditLogs",
                        icon: "icon-lock",
                        requiredPermissionName: AppPermissions.Pages_Administration_AuditLogs
                        )
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Host.Maintenance,
                        L("Maintenance"),
                        url: "host.maintenance",
                        icon: "icon-wrench",
                        requiredPermissionName: AppPermissions.Pages_Administration_Host_Maintenance
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                        PageNames.App.Host.Settings,
                        L("Settings"),
                        url: "host.settings",
                        icon: "icon-settings",
                        requiredPermissionName: AppPermissions.Pages_Administration_Host_Settings
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                        PageNames.App.Tenant.Settings,
                        L("Settings"),
                        url: "tenant.settings",
                        icon: "icon-settings",
                        requiredPermissionName: AppPermissions.Pages_Administration_Tenant_Settings
                        )
                    )
                    
                    .AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Vendors,
                    L("Vendors"),
                    icon: "glyphicon glyphicon-tasks"
                    ).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.AddVendor,
                    L("AddVendor"),
                    url: "tenant.AddVendor",
                    icon: "glyphicon glyphicon-collapse-down"
                    )).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.VendorList,
                    L("Search"),
                    url: "tenant.VendorList",
                    icon: "glyphicon glyphicon-search"
                    ))
                    )
                    
                    .AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Insurers,
                    L("Insurers"),
                    icon: "glyphicon glyphicon-tasks"
                    ).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.AddInsurer,
                    L("AddInsurer"),
                    url: "tenant.AddInsurer",
                    icon: "glyphicon glyphicon-collapse-down"
                    )).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Insurers,
                    L("Search"),
                    url: "tenant.Insurers",
                    icon: "glyphicon glyphicon-search"
                    ))
                    )

                    .AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Brokers,
                    L("Brokers"),
                    icon: "glyphicon glyphicon-tasks"
                    ).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.AddBroker,
                    L("AddBroker"),
                    url: "tenant.AddBroker",
                    icon: "glyphicon glyphicon-collapse-down"
                    )).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Brokers,
                    L("Search"),
                    url: "tenant.Brokers",
                    icon: "glyphicon glyphicon-search"
                    ))
                    )
                    
                    .AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.VehicleManufacturer,
                    L("VehicleManufacturer"),
                    icon: "glyphicon glyphicon-tasks"
                    )
                    .AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.VehicleMakes,
                    L("AllMakes"),
                    url: "tenant.VehicleMakes",
                    icon: "glyphicon glyphicon-search"
                    ))
                    .AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.AddMake,
                    L("AddVehicleMake"),
                    url: "tenant.AddVehicleMake",
                    icon: "glyphicon glyphicon-collapse-down"
                    ))
                    .AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.VehicleMades,
                    L("AllMades"),
                    url: "tenant.VehicleMades",
                    icon: "glyphicon glyphicon-search"
                    ))
                    .AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.AddModel,
                    L("AddVehicleMade"),
                    url: "tenant.AddVehicleMade",
                    icon: "glyphicon glyphicon-collapse-down"
                    ))

                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PanelMasterMVC5SeparateConsts.LocalizationSourceName);
        }
    }
}
