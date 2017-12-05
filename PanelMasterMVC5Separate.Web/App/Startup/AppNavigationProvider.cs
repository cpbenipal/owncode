﻿using Abp.Application.Navigation;
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
                    url: "tenant.viewQuotations",
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
                    icon: "glyphicon glyphicon-wrench"
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
                    PageNames.App.Tenant.Productivity,
                    @L("Report"),
                    url: "tenant.reporting",
                    icon: "glyphicon glyphicon-info-sign"
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
                    ).AddItem(new MenuItemDefinition(
                    PageNames.App.Host.SystemDefaults,
                    L("SystemDefaults"),
                   icon: "icon-wrench",
                    requiredPermissionName: AppPermissions.Pages_Administration_Host_SystemDefaults
                    )
                     .AddItem(new MenuItemDefinition(
                    PageNames.App.Host.Banks,
                    L("Banks"),
                    url: "host.Banks",
                    icon: "glyphicon glyphicon-search"
                    ))

                    .AddItem(new MenuItemDefinition(
                    PageNames.App.Host.VehicleMakes,
                    L("VehicleMakes"),
                     url: "host.VehicleMakes",
                   icon: "glyphicon glyphicon-search"
                    )
                    )

                     .AddItem(new MenuItemDefinition(
                    PageNames.App.Host.VehicleMades,
                    L("VehicleMades"),
                    url: "host.VehicleMades",
                    icon: "glyphicon glyphicon-search"
                    )
                    )

                     .AddItem(new MenuItemDefinition(
                        PageNames.App.Host.Insurers,
                        L("Insurers"),
                        url: "host.Insurers",
                       icon: "glyphicon glyphicon-search"
                        )
                    )

                    .AddItem(new MenuItemDefinition(
                        PageNames.App.Host.Brokers,
                        L("Brokers"),
                        url: "host.Brokers",
                       icon: "glyphicon glyphicon-search"
                        )
                    )

                    .AddItem(new MenuItemDefinition(
                    PageNames.App.Host.MainVendors,
                    L("Vendors"),
                    url: "host.Vendors",
                     icon: "glyphicon glyphicon-search"
                    ))

                    .AddItem(new MenuItemDefinition(
                        PageNames.App.Host.TowOperators,
                        L("TowOperators"),
                        url: "host.towoperators",
                       icon: "glyphicon glyphicon-search"
                        )
                    )

                    .AddItem(new MenuItemDefinition(
                        PageNames.App.Host.JobStatuses,
                        L("JobStatuses"),
                        url: "host.JobStatuses",
                        icon: "glyphicon glyphicon-search"
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                        PageNames.App.Host.JobMaskStatuses,
                        L("JobMaskStatuses"),
                        url: "host.JobMaskStatuses",
                        icon: "glyphicon glyphicon-search"
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                        PageNames.App.Host.QuoteStatus,
                        L("QuoteStatuses"),
                        url: "host.QuoteStatus",
                        icon: "glyphicon glyphicon-search"
                        )
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Host.RepairTypes,
                        L("RepairTypes"),
                        url: "host.RepairTypes",
                        icon: "glyphicon glyphicon-search"
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                        PageNames.App.Host.SignonPlans,
                        L("SignOnPlans"),
                        url: "host.SignonPlans",
                        icon: "glyphicon glyphicon-search"
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                        PageNames.App.Host.RoleCategories,
                        L("RoleCategories"),
                        url: "host.RoleCategories",
                        icon: "glyphicon glyphicon-search"
                        )
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
                        PageNames.App.Tenant.TowOperators,
                        L("TowOperators"),
                        url: "tenant.towoperators",
                        icon: "icon-settings",
                        requiredPermissionName: AppPermissions.Pages_Administration_Tenant_Settings
                        )
                    )
                    // .AddItem(new MenuItemDefinition(
                    //    PageNames.App.Tenant.JobStatuses,
                    //    L("JobStatuses"),
                    //    url: "tenant.jobstatuses",
                    //    icon: "icon-settings",
                    //    requiredPermissionName: AppPermissions.Pages_Administration_Tenant_Settings
                    //    )
                    //) 
                    .AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Vendors,
                    L("Vendors"),
                     icon: "glyphicon glyphicon-search",
                    url: "tenant.VendorList",
                    requiredPermissionName: AppPermissions.Pages_Administration_Tenant_Settings
                    ))

                    .AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Insurers,
                    L("Insurers"),
                    icon: "glyphicon glyphicon-search",
                     url: "tenant.Insurers",
                     requiredPermissionName: AppPermissions.Pages_Administration_Tenant_Settings
                    ))

                    .AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Brokers,
                    L("Brokers"),
                     icon: "glyphicon glyphicon-search",
                     url: "tenant.Brokers",
                     requiredPermissionName: AppPermissions.Pages_Administration_Tenant_Settings
                    ))
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PanelMasterMVC5SeparateConsts.LocalizationSourceName);
        }
    }
}
