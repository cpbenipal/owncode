﻿/* 'app' MODULE DEFINITION */
var appModule = angular.module("app", [
    "ui.router",
    "ui.bootstrap",
    'ui.utils',
    "ui.jq",
    'ui.grid',
    'ui.grid.pagination',
    "oc.lazyLoad",
    "ngSanitize",
    'angularFileUpload',
    'daterangepicker',
    'angularMoment',
    'frapontillo.bootstrap-switch',
    'abp'
]);

/* LAZY LOAD CONFIG */

/* This application does not define any lazy-load yet but you can use $ocLazyLoad to define and lazy-load js/css files.
 * This code configures $ocLazyLoad plug-in for this application.
 * See it's documents for more information: https://github.com/ocombe/ocLazyLoad
 */
appModule.config(['$ocLazyLoadProvider', function ($ocLazyLoadProvider) {
    $ocLazyLoadProvider.config({
        cssFilesInsertBefore: 'ng_load_plugins_before', // load the css files before a LINK element with this ID.
        debug: false,
        events: true,
        modules: []
    });
}]);

/* THEME SETTINGS */
App.setAssetsPath(abp.appPath + 'metronic/assets/');
appModule.factory('settings', ['$rootScope', function ($rootScope) {
    var settings = {
        layout: {
            pageSidebarClosed: false, // sidebar menu state
            pageContentWhite: true, // set page content layout
            pageBodySolid: false, // solid body color state
            pageAutoScrollOnLoad: 1000 // auto scroll to top on page load
        },
        layoutImgPath: App.getAssetsPath() + 'admin/layout4/img/',
        layoutCssPath: App.getAssetsPath() + 'admin/layout4/css/',
        assetsPath: abp.appPath + 'metronic/assets',
        globalPath: abp.appPath + 'metronic/assets/global',
        layoutPath: abp.appPath + 'metronic/assets/layouts/layout4'
    };

    $rootScope.settings = settings;

    return settings;
}]);

/* ROUTE DEFINITIONS */

appModule.config([
    '$stateProvider', '$urlRouterProvider', '$qProvider',
    function ($stateProvider, $urlRouterProvider, $qProvider) {

        // Default route (overrided below if user has permission)
        $urlRouterProvider.otherwise("/welcome");

        //Welcome page
        $stateProvider.state('welcome', {
            url: '/welcome',
            templateUrl: '~/App/common/views/welcome/index.cshtml'
        });

        //COMMON routes

        if (abp.auth.hasPermission('Pages.Administration.Roles')) {
            $stateProvider.state('roles', {
                url: '/roles',
                templateUrl: '~/App/common/views/roles/index.cshtml'
            });
        }

        if (abp.auth.hasPermission('Pages.Administration.Users')) {
            $stateProvider.state('users', {
                url: '/users?filterText',
                templateUrl: '~/App/common/views/users/index.cshtml'
            });
        }

        if (abp.auth.hasPermission('Pages.Administration.Languages')) {
            $stateProvider.state('languages', {
                url: '/languages',
                templateUrl: '~/App/common/views/languages/index.cshtml'
            });

            if (abp.auth.hasPermission('Pages.Administration.Languages.ChangeTexts')) {
                $stateProvider.state('languageTexts', {
                    url: '/languages/texts/:languageName?sourceName&baseLanguageName&targetValueFilter&filterText',
                    templateUrl: '~/App/common/views/languages/texts.cshtml'
                });
            }
        }     
                 
        if (abp.auth.hasPermission('Pages.Administration.AuditLogs')) {
            $stateProvider.state('auditLogs', {
                url: '/auditLogs',
                templateUrl: '~/App/common/views/auditLogs/index.cshtml'
            });
        }

        if (abp.auth.hasPermission('Pages.Administration.OrganizationUnits')) {
            $stateProvider.state('organizationUnits', {
                url: '/organizationUnits',
                templateUrl: '~/App/common/views/organizationUnits/index.cshtml'
            });
        }

        $stateProvider.state('notifications', {
            url: '/notifications',
            templateUrl: '~/App/common/views/notifications/index.cshtml'
        });

        //HOST routes

        $stateProvider.state('host', {
            'abstract': true,
            url: '/host',
            template: '<div ui-view class="fade-in-up"></div>'
        });

        if (abp.auth.hasPermission('Pages.Tenants')) {
            $urlRouterProvider.otherwise("/host/tenants"); //Entrance page for the host
            $stateProvider.state('host.tenants', {
                url: '/tenants?filterText',
                templateUrl: '~/App/host/views/tenants/index.cshtml'
            });
        }

        if (abp.auth.hasPermission('Pages.Editions')) {
            $stateProvider.state('host.editions', {
                url: '/editions',
                templateUrl: '~/App/host/views/editions/index.cshtml'
            });
        }

        if (abp.auth.hasPermission('Pages.Administration.Host.Maintenance')) {
            $stateProvider.state('host.maintenance', {
                url: '/maintenance',
                templateUrl: '~/App/host/views/maintenance/index.cshtml'
            });
        }

        if (abp.auth.hasPermission('Pages.Administration.Host.Settings')) {
            $stateProvider.state('host.settings', {
                url: '/settings',
                templateUrl: '~/App/host/views/settings/index.cshtml'
            });
        }

        if (abp.auth.hasPermission('Pages.Administration.Host.SystemDefaults')) {

            $stateProvider.state('host.Banks', {
            url: '/Banks',
            templateUrl: '~/App/host/views/Banks/index.cshtml'
        }); 
            $stateProvider.state('host.VehicleMakes', {
            url: '/VehicleMakes',
            templateUrl: '~/App/host/views/VehicleMakes/index.cshtml'
        });
            $stateProvider.state('host.VehicleMades', {
            url: '/VehicleMades',
            templateUrl: '~/App/host/views/VehicleMades/index.cshtml'
        });
            $stateProvider.state('host.AddVehicleMake', {
            url: '/AddVehicleMake',
            templateUrl: '~/App/host/views/AddVehicleMake/index.cshtml'
        });
        $stateProvider.state('host.EditVehicleMake', {
            url: '/EditVehicleMake/:id',
            templateUrl: '~/App/host/views/EditVehicleMake/index.cshtml'
        });
        $stateProvider.state('host.AddVehicleMade', {
            url: '/AddVehicleMade',
            templateUrl: '~/App/host/views/AddVehicleMade/index.cshtml'
        });
        $stateProvider.state('host.EditVehicleMade', {
            url: '/EditVehicleMade/:id',
            templateUrl: '~/App/host/views/EditVehicleMade/index.cshtml'
        });
        $stateProvider.state('host.Insurers', {
            url: '/Insurers',
            templateUrl: '~/App/host/views/Insurers/index.cshtml'
        }); 
        $stateProvider.state('host.AddInsurer', {
            url: '/AddEditInsurer',
            templateUrl: '~/App/host/views/AddEditInsurer/index.cshtml'
        });
        $stateProvider.state('host.EditInsurer', {
            url: '/AddEditInsurer/:id',
            templateUrl: '~/App/host/views/AddEditInsurer/index.cshtml'
        });
        $stateProvider.state('host.Brokers', {
            url: '/Brokers',
            templateUrl: '~/App/host/views/Brokers/index.cshtml'
        }); 
        $stateProvider.state('host.AddBroker', {
            url: '/AddEditBroker',
            templateUrl: '~/App/host/views/AddEditBroker/index.cshtml'
        });
        $stateProvider.state('host.EditBroker', {
            url: '/AddEditBroker/:id',
            templateUrl: '~/App/host/views/AddEditBroker/index.cshtml'
        });

        $stateProvider.state('host.Vendors', {
            url: '/Vendors',
            templateUrl: '~/App/host/views/Vendors/index.cshtml'
        });
        $stateProvider.state('host.AddVendor', {
            url: '/AddEditVendor',
            templateUrl: '~/App/host/views/AddEditVendor/index.cshtml'
        });
        $stateProvider.state('host.EditVendor', {
            url: '/AddEditVendor/:id',
            templateUrl: '~/App/host/views/AddEditVendor/index.cshtml'
        });
        $stateProvider.state('host.towoperators', {
            url: '/towoperators',
            templateUrl: '~/App/host/views/towoperators/index.cshtml'
        });
        $stateProvider.state('host.JobStatuses', {
            url: '/JobStatuses',
            templateUrl: '~/App/host/views/JobStatuses/index.cshtml'
        });
        $stateProvider.state('host.JobMaskStatuses', {
            url: '/JobMaskStatuses',
            templateUrl: '~/App/host/views/JobMaskStatuses/index.cshtml'
        });
        $stateProvider.state('host.QuoteStatus', {
            url: '/QuoteStatus',
            templateUrl: '~/App/host/views/QuoteStatus/index.cshtml'
        });
        $stateProvider.state('host.RepairTypes', {
            url: '/RepairTypes',
            templateUrl: '~/App/host/views/RepairTypes/index.cshtml'
        });
        $stateProvider.state('host.RoleCategories', {
            url: '/RoleCategories',
            templateUrl: '~/App/host/views/RoleCategories/index.cshtml'
        });
        $stateProvider.state('host.SignonPlans', {
            url: '/SignonPlans',
            templateUrl: '~/App/host/views/SignonPlans/index.cshtml'
        });
       }

        //TENANT routes

        $stateProvider.state('tenant', {
            'abstract': true,
            url: '/tenant',
            template: '<div ui-view class="fade-in-up"></div>'
        });       

        if (abp.auth.hasPermission('Pages.Tenant.Dashboard')) {
            $urlRouterProvider.otherwise("/tenant/dashboard"); //Entrance page for a tenant
            $stateProvider.state('tenant.dashboard', {
                url: '/dashboard',
                templateUrl: '~/App/tenant/views/dashboard/index.cshtml'
            });
        }

        if (abp.auth.hasPermission('Pages.Administration.Tenant.Settings')) {
            $stateProvider.state('tenant.settings', {
                url: '/settings',
                templateUrl: '~/App/tenant/views/settings/index.cshtml'
            });
        }

        $stateProvider.state('tenant.jobdetails', {
            url: '/jobdetails',
            templateUrl: '~/App/tenant/views/jobdetails/index.cshtml'
        });

        $stateProvider.state('tenant.job', {
            url: '/jobdetails/job/:languageName?sourceName&baseLanguageName&targetValueFilter&filterText',
            templateUrl: '~/App/tenant/views/jobdetails/job.cshtml'
        });  

        $stateProvider.state('tenant.quoting', {
            url: '/quoting',
            templateUrl: '~/App/tenant/views/quoting/index.cshtml'
        });

        $stateProvider.state('tenant.reporting', {
            url: '/reporting',
            templateUrl: '~/App/tenant/views/reporting/index.cshtml'
        });

        $stateProvider.state('tenant.viewQuotations', {
            url: '/viewQuotations',
            templateUrl: '~/App/tenant/views/viewQuotations/index.cshtml'
        });

        $stateProvider.state('tenant.quoteheaders', {
            url: '/quoteheaders/:id',
            templateUrl: '~/App/tenant/views/quoteheaders/index.cshtml'
        });

        $stateProvider.state('tenant.partsordering', {
            url: '/partsordering',
            templateUrl: '~/App/tenant/views/partsordering/index.cshtml'
        });       

        $stateProvider.state('tenant.NewJob', {
            url: '/NewJob',
            templateUrl: '~/App/tenant/views/NewJob/index.cshtml'
        });

        $stateProvider.state('tenant.Editjob', {
            url: '/Editjob/:id',
            templateUrl: '~/App/tenant/views/Editjob/index.cshtml'
        });

        $stateProvider.state('tenant.AddVendor', {
            url: '/AddEditVendor',
            templateUrl: '~/App/tenant/views/AddEditVendor/index.cshtml'
        });

        $stateProvider.state('tenant.AddSubVendor', {
            url: '/AddSubVendor/:id',
            templateUrl: '~/App/tenant/views/AddSubVendor/index.cshtml'
        }); 

        $stateProvider.state('tenant.AddEditVendor', {
            url: '/AddEditVendor/:id',
            templateUrl: '~/App/tenant/views/AddEditVendor/index.cshtml'
        });        

        $stateProvider.state('tenant.VendorList', {
            url: '/VendorList',
            templateUrl: '~/App/tenant/views/VendorList/index.cshtml'
        });

        $stateProvider.state('tenant.EditVendor', {
            url: '/EditVendor/:id',
            templateUrl: '~/App/tenant/views/EditVendor/index.cshtml'
        });

        $stateProvider.state('tenant.profile_account', {
            url: '/profile_account',
            templateUrl: '~/App/tenant/views/profile_account/index.cshtml'
        });     
        
        $stateProvider.state('tenant.job_landing',{
            url: '/job_landing',
            templateUrl: '~/App/tenant/views/job_landing/index.cshtml'
        });

        $stateProvider.state('tenant.job_landing.job_information', {
            url: '/job_information/:id',
            templateUrl: '~/App/tenant/views/job_information/index.cshtml'
        });

        $stateProvider.state('tenant.job_landing.vehicle_information', {
            url: '/vehicle_information/:id',
            templateUrl: '~/App/tenant/views/vehicle_information/index.cshtml'
        });

       
        $stateProvider.state('tenant.job_landing.client_information', {
            url: '/client_information/:id',
            templateUrl: '~/App/tenant/views/client_information/index.cshtml'
        });        

        $stateProvider.state('tenant.job_landing.quotedvalues', {
            url: '/quotedvalues/:id',
            templateUrl: '~/App/tenant/views/quotedvalues/index.cshtml'
        });

        $stateProvider.state('tenant.job_landing.authorised_values', {
            url: '/authorised_values/:id',
            templateUrl: '~/App/tenant/views/authorised_values/index.cshtml'
        });

        $stateProvider.state('tenant.job_landing.invoiced_values', {
            url: '/invoiced_values/:id',
            templateUrl: '~/App/tenant/views/invoiced_values/index.cshtml'
        });

        $stateProvider.state('tenant.job_landing.dates_information', {
            url: '/dates_information/:id',
            templateUrl: '~/App/tenant/views/dates_information/index.cshtml'
        });

        $stateProvider.state('tenant.job_landing.comments_information', {
            url: '/comments_information/:id',
            templateUrl: '~/App/tenant/views/comments_information/index.cshtml'
        });

        $stateProvider.state('tenant.job_landing.audit_trail', {
            url: '/audit_trail/:id',
            templateUrl: '~/App/tenant/views/audit_trail/index.cshtml'
        });

        $stateProvider.state('tenant.job_landing.communications', {
            url: '/communications/:id',
            templateUrl: '~/App/tenant/views/communications/index.cshtml'
        });

        $stateProvider.state('tenant.job_landing.uploads', {
            url: '/uploads/:id',
            templateUrl: '~/App/tenant/views/uploads/index.cshtml'
        });

        $stateProvider.state('tenant.job_landing.photos', {
            url: '/photos/:id',
            templateUrl: '~/App/tenant/views/photos/index.cshtml'
        });

        $stateProvider.state('tenant.productivity', {
            url: '/productivity',
            templateUrl: '~/App/tenant/views/productivity/index.cshtml'
        });

        $stateProvider.state('tenant.towing', {
            url: '/towing',
            templateUrl: '~/App/tenant/views/towing/index.cshtml'
        });

        $stateProvider.state('tenant.costing', {
            url: '/costing',
            templateUrl: '~/App/tenant/views/costing/index.cshtml'
        });

        $stateProvider.state('tenant.Insurers', {
            url: '/Insurers',
            templateUrl: '~/App/tenant/views/Insurers/index.cshtml'
        });
  
        $stateProvider.state('tenant.AddInsurerSub', {
            url: '/AddInsurerSub/:id',
            templateUrl: '~/App/tenant/views/AddInsurerSub/index.cshtml'
        });
        $stateProvider.state('tenant.EditInsurerSub', {
            url: '/EditInsurerSub/:id',
            templateUrl: '~/App/tenant/views/EditInsurerSub/index.cshtml'
        });


        $stateProvider.state('tenant.Brokers', {
            url: '/Brokers',
            templateUrl: '~/App/tenant/views/Brokers/index.cshtml'
        });  
        $stateProvider.state('tenant.AddBrokerSub', {
            url: '/AddBrokerSub/:id',
            templateUrl: '~/App/tenant/views/AddBrokerSub/index.cshtml'
        });
        $stateProvider.state('tenant.EditBrokerSub', {
            url: '/EditBrokerSub/:id',
            templateUrl: '~/App/tenant/views/EditBrokerSub/index.cshtml'
        });       

        //$stateProvider.state('tenant.jobstatuses', {
        //    url: '/jobstatuses',
        //    templateUrl: '~/App/tenant/views/jobstatuses/index.cshtml'
        //});
        $stateProvider.state('tenant.createjob', {
            url: '/createjob',
            templateUrl: '~/App/tenant/views/createjob/index.cshtml'
        });
        $stateProvider.state('tenant.towoperators', {
            url: '/towoperators',
            templateUrl: '~/App/tenant/views/towoperators/index.cshtml'
        });
        $stateProvider.state('tenant.phonebook', {
            url: '/phonebook',
            templateUrl: '~/App/tenant/views/phonebook/index.cshtml'
        });
        //$qProvider settings
        $qProvider.errorOnUnhandledRejections(false);
    }
]);

appModule.run(["$rootScope", "settings", "$state", 'i18nService', '$uibModalStack', function ($rootScope, settings, $state, i18nService, $uibModalStack) {
    $rootScope.$state = $state;
    $rootScope.$settings = settings;

    $rootScope.$on('$stateChangeSuccess', function () {
        $uibModalStack.dismissAll();
    });

    //Set Ui-Grid language
    if (i18nService.get(abp.localization.currentCulture.name)) {
        i18nService.setCurrentLang(abp.localization.currentCulture.name);
    } else {
        i18nService.setCurrentLang("en");
    }

    $rootScope.safeApply = function (fn) {
        var phase = this.$root.$$phase;
        if (phase === '$apply' || phase === '$digest') {
            if (fn && (typeof (fn) === 'function')) {
                fn();
            }
        } else {
            this.$apply(fn);
        }
    };
}]);