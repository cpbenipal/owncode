using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Abp;
using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Microsoft.AspNet.Identity;
using PanelMasterMVC5Separate.Authorization.Roles;
using PanelMasterMVC5Separate.Authorization.Claim;
using PanelMasterMVC5Separate.Editions;
using PanelMasterMVC5Separate.MultiTenancy.Demo;
using Abp.Extensions;
using Abp.Notifications;
using Abp.Runtime.Security;
using PanelMasterMVC5Separate.Notifications;
using System.Collections.Generic;
using System;
using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.Vendors;
using System.Collections.ObjectModel;
using PanelMasterMVC5Separate.Insurer;
using PanelMasterMVC5Separate.Brokers;

namespace PanelMasterMVC5Separate.MultiTenancy
{
    /// <summary>
    /// Tenant manager.
    /// </summary>
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<CountryandCurrency> _countryandcurrency;
        private readonly IRepository<Countries> _countries;
        private readonly UserManager _userManager;
        private readonly IUserEmailer _userEmailer;
        private readonly TenantDemoDataBuilder _demoDataBuilder;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly IAppNotifier _appNotifier;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;
        private readonly IRepository<SignonPlans> _SignonPlansRepository;
        private readonly IRepository<TenantProfile> _TenantProfile;
        private readonly IRepository<TenantPlanBillingDetails> _TenantPlanBillingDetails;
        private readonly IRepository<TowOperator> _TowOperator;
        private readonly IRepository<Banks> _Banks;
        private readonly IRepository<InsurerMaster> _insurer;
        private readonly IRepository<BrokerMaster> _broker;
        private readonly IRepository<InsurerPics> _insurerpic;
        private readonly IRepository<BrokerMasterPics> _brokerpic;
        private readonly IRepository<VendorMain> _vendors;
        private readonly IRepository<TowOperator> _tow;
        public TenantManager(
            IRepository<TowOperator> towoperator,
            IRepository<SignonPlans> signonplansrepository,
             IRepository<TenantProfile> tenantprofile,
              IRepository<TenantPlanBillingDetails> tenantplanbillingdetails,
            IRepository<Tenant> tenantRepository,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
            EditionManager editionManager,
            IUnitOfWorkManager unitOfWorkManager,
            RoleManager roleManager,
            IUserEmailer userEmailer,
            TenantDemoDataBuilder demoDataBuilder,
            UserManager userManager,
            IRepository<Countries> countries,
            IRepository<CountryandCurrency> countryandcurrency,
            INotificationSubscriptionManager notificationSubscriptionManager,
            IAppNotifier appNotifier,
            IAbpZeroFeatureValueStore featureValueStore,
            IAbpZeroDbMigrator abpZeroDbMigrator,
            IRepository<Banks> bank,
            IRepository<InsurerMaster> insurer,
            IRepository<BrokerMaster> broker,
               IRepository<InsurerPics> insurerpic,
            IRepository<BrokerMasterPics> brokerpic,
            IRepository<VendorMain> vendors,
            IRepository<TowOperator> town
            )
            : base(
                  tenantRepository,
                  tenantFeatureRepository,
                  editionManager,
                  featureValueStore)
        {
            _TowOperator = towoperator;
            _SignonPlansRepository = signonplansrepository;
            _TenantPlanBillingDetails = tenantplanbillingdetails;
            _TenantProfile = tenantprofile;
            _unitOfWorkManager = unitOfWorkManager;
            _roleManager = roleManager;
            _userEmailer = userEmailer;
            _demoDataBuilder = demoDataBuilder;
            _userManager = userManager;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _appNotifier = appNotifier;
            _abpZeroDbMigrator = abpZeroDbMigrator;
            _countries = countries;
            _countryandcurrency = countryandcurrency;
            _Banks = bank;
            _insurer = insurer;
            _broker = broker;
            _insurerpic = insurerpic;
            _brokerpic = brokerpic;
            _vendors = vendors;
            _tow = town;
        }

        public async Task<int> CreateWithAdminUserAsync(string tenancyName, string name, string adminPassword, string adminEmailAddress, string connectionString, bool isActive, int? editionId, bool shouldChangePasswordOnNextLogin, bool sendActivationEmail)
        {
            int newTenantId;
            long newAdminId;

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                //Create tenant
                var tenant = new Tenant(tenancyName, name)
                {
                    IsActive = isActive,
                    EditionId = editionId,
                    ConnectionString = connectionString.IsNullOrWhiteSpace() ? null : SimpleStringCipher.Instance.Encrypt(connectionString)
                };

                await CreateAsync(tenant);
                await _unitOfWorkManager.Current.SaveChangesAsync(); //To get new tenant's id.

                //Create tenant database
                _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);

                //We are working entities of new tenant, so changing tenant filter
                using (_unitOfWorkManager.Current.SetTenantId(tenant.Id))
                {
                    //Create static roles for new tenant
                    CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));
                    await _unitOfWorkManager.Current.SaveChangesAsync(); //To get static role ids

                    //grant all permissions to admin role
                    var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                    adminRole.RoleCategoryID = 1;
                    await _roleManager.GrantAllPermissionsAsync(adminRole);

                    //User role should be default
                    var userRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.User);
                    userRole.IsDefault = true;
                    userRole.RoleCategoryID = 2;

                    CheckErrors(await _roleManager.UpdateAsync(userRole));

                    //Estimator should be default

                    var claimHandlerRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Claims_Handler);
                    claimHandlerRole.IsDefault = false;
                    claimHandlerRole.RoleCategoryID = 3;
                    CheckErrors(await _roleManager.UpdateAsync(claimHandlerRole));

                    var csaRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.CSA);
                    csaRole.IsDefault = false;
                    csaRole.RoleCategoryID = 4;
                    CheckErrors(await _roleManager.UpdateAsync(csaRole));

                    var partsBuyerRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Parts_Buyer);
                    partsBuyerRole.IsDefault = false;
                    partsBuyerRole.RoleCategoryID = 5;
                    CheckErrors(await _roleManager.UpdateAsync(partsBuyerRole));

                    var estimatorRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Estimator);
                    estimatorRole.IsDefault = false;
                    estimatorRole.RoleCategoryID = 6;
                    CheckErrors(await _roleManager.UpdateAsync(estimatorRole));

                    var keyAccManagerRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Key_Accounts_Manager);
                    keyAccManagerRole.IsDefault = false;
                    keyAccManagerRole.RoleCategoryID = 7;
                    CheckErrors(await _roleManager.UpdateAsync(keyAccManagerRole));

                    var SwitchboardRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Swithchboard);
                    SwitchboardRole.IsDefault = false;
                    SwitchboardRole.RoleCategoryID = 8;
                    CheckErrors(await _roleManager.UpdateAsync(SwitchboardRole));

                    var partsReceiverRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Parts_Receiver);
                    partsReceiverRole.IsDefault = false;
                    partsReceiverRole.RoleCategoryID = 9;
                    CheckErrors(await _roleManager.UpdateAsync(partsReceiverRole));

                    var costingClerkRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Costing_Clerk);
                    costingClerkRole.IsDefault = false;
                    costingClerkRole.RoleCategoryID = 10;
                    CheckErrors(await _roleManager.UpdateAsync(costingClerkRole));

                    var FinancialmanagerRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Financial_Manager);
                    FinancialmanagerRole.IsDefault = false;
                    FinancialmanagerRole.RoleCategoryID = 11;
                    CheckErrors(await _roleManager.UpdateAsync(FinancialmanagerRole));

                    var InsurerRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Insurer);
                    InsurerRole.IsDefault = false;
                    InsurerRole.RoleCategoryID = 12;
                    CheckErrors(await _roleManager.UpdateAsync(InsurerRole));

                    var brokerRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Broker);
                    brokerRole.IsDefault = false;
                    brokerRole.RoleCategoryID = 13;
                    CheckErrors(await _roleManager.UpdateAsync(brokerRole));


                    //Create admin user for the tenant
                    if (adminPassword.IsNullOrEmpty())
                    {
                        adminPassword = User.CreateRandomPassword();
                    }

                    var adminUser = User.CreateTenantAdminUser(tenant.Id, adminEmailAddress, adminPassword);
                    adminUser.ShouldChangePasswordOnNextLogin = shouldChangePasswordOnNextLogin;
                    adminUser.IsActive = true;

                    CheckErrors(await _userManager.CreateAsync(adminUser));
                    await _unitOfWorkManager.Current.SaveChangesAsync(); //To get admin user's id

                    //Assign admin user to admin role!
                    CheckErrors(await _userManager.AddToRoleAsync(adminUser.Id, adminRole.Name));

                    //Notifications
                    await _appNotifier.WelcomeToTheApplicationAsync(adminUser);

                    //Send activation email
                    if (sendActivationEmail)
                    {
                        adminUser.SetNewEmailConfirmationCode();
                        await _userEmailer.SendEmailActivationLinkAsync(adminUser, adminPassword);
                    }

                    await _unitOfWorkManager.Current.SaveChangesAsync();

                    await _demoDataBuilder.BuildForAsync(tenant);

                    newTenantId = tenant.Id;
                    newAdminId = adminUser.Id;
                }

                await uow.CompleteAsync();
            }

            //Used a second UOW since UOW above sets some permissions and _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync needs these permissions to be saved.
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                {
                    await _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(new UserIdentifier(newTenantId, newAdminId));
                    await _unitOfWorkManager.Current.SaveChangesAsync();
                    await uow.CompleteAsync();
                }
            }

            return newTenantId;
        }

        public async Task<int> CreateWithAdminUserAsync(string tenancyName, string name, string adminPassword, string adminEmailAddress,
            string fullName, string cellnumber,
            //string phoneNumber, string companyRegistrationNo, string companyVatNo, string address, string city, 
            string countrycode, string currencycode,
            //string invoicinginstruction,
            //string billingcountrycode, string billingcurrencycode,
            //string timezone, 
            string cardHoldersName, string cardNumber, string cardExpiration, string cVV, string payment, int planId, string connectionString,
            bool isActive, int? editionId, bool shouldChangePasswordOnNextLogin, bool sendActivationEmail)
        {


            int newTenantId;
            long newAdminId;

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                //Create tenant
                var tenant = new Tenant(tenancyName.Replace(" ", "-"), name)
                {
                    IsActive = isActive,
                    EditionId = editionId,
                    ConnectionString = connectionString.IsNullOrWhiteSpace() ? null : SimpleStringCipher.Instance.Encrypt(connectionString)
                };
                string invoicinginstruction = "Payment instructions if necessary. Tell recipients how to make out their check (cheque) payment. If you expect payments by wire transfer, you should provide your bank ";
                await CreateAsync(tenant);
                await _unitOfWorkManager.Current.SaveChangesAsync(); //To get new tenant's id.

                //Verify if tblBanks/tblInsurerMaster/tblBrokerMaster contain "OTHER" and "NONE" for the specific country,
                //if already created, then don’t add. If not exist for current country, then add "OTHER" and "NONE" to tblbanks/tblBrokerMaster/tblInsurerMaster with countryid and enable
                VerifyDefaultData(countrycode);

                //Save Newly tenant Profile
                var tenantprofile = new TenantProfile()
                {
                    FullName = fullName,
                    //PhoneNumber = phoneNumber,
                    CompanyName = tenancyName,
                    CellNumber = cellnumber,
                    // CompanyRegistrationNo = companyRegistrationNo,
                    // CompanyVatNo = companyVatNo,
                    // Address = address,
                    // City = city,
                    CountryCode = countrycode,
                    CurrencyCode = currencycode,
                    // Timezone = timezone,
                    InvoicingInstruction = invoicinginstruction,
                    TenantId = tenant.Id
                };

                await _TenantProfile.InsertAsync(tenantprofile);
                // Save Newly Tenant Proflie billing Details
                var tenantbilling = new TenantPlanBillingDetails()
                {
                    CardHoldersName = cardHoldersName,
                    CardNumber = cardNumber,
                    CardExpiration = cardExpiration,
                    CVV = cVV,
                    planId = planId,
                    TenantId = tenant.Id,
                    //BillingCountryCode = billingcountrycode,
                    //CurrencyCode = billingcountrycode,
                    PaymentOptions = payment
                };
                await _TenantPlanBillingDetails.InsertAsync(tenantbilling);

                // Add TowOperators
                //CreateTowOperators(tenant.Id, countrycode);

                //Create tenant database
                _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);

                //We are working entities of new tenant, so changing tenant filter
                using (_unitOfWorkManager.Current.SetTenantId(tenant.Id))
                {
                    //Create static roles for new tenant
                    CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));
                    await _unitOfWorkManager.Current.SaveChangesAsync(); //To get static role ids

                    //grant all permissions to admin role
                    var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                    await _roleManager.GrantAllPermissionsAsync(adminRole);

                    //User role should be default
                    var userRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.User);
                    userRole.IsDefault = true;
                    CheckErrors(await _roleManager.UpdateAsync(userRole));

                    //Create admin user for the tenant
                    if (adminPassword.IsNullOrEmpty())
                    {
                        adminPassword = User.CreateRandomPassword();
                    }

                    var adminUser = User.CreateTenantAdminUser(tenant.Id, adminEmailAddress, adminPassword);
                    adminUser.ShouldChangePasswordOnNextLogin = shouldChangePasswordOnNextLogin;
                    adminUser.IsActive = true;

                    CheckErrors(await _userManager.CreateAsync(adminUser));
                    await _unitOfWorkManager.Current.SaveChangesAsync(); //To get admin user's id

                    //Assign admin user to admin role!
                    CheckErrors(await _userManager.AddToRoleAsync(adminUser.Id, adminRole.Name));

                    //Notifications
                    await _appNotifier.WelcomeToTheApplicationAsync(adminUser);

                    //Send activation email
                    if (sendActivationEmail)
                    {
                        adminUser.SetNewEmailConfirmationCode();
                        await _userEmailer.SendEmailActivationLinkAsync(adminUser, adminPassword);
                    }

                    await _unitOfWorkManager.Current.SaveChangesAsync();

                    await _demoDataBuilder.BuildForAsync(tenant);

                    newTenantId = tenant.Id;
                    newAdminId = adminUser.Id;
                }

                await uow.CompleteAsync();
            }

            //Used a second UOW since UOW above sets some permissions and _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync needs these permissions to be saved.
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                {
                    await _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(new UserIdentifier(newTenantId, newAdminId));
                    await _unitOfWorkManager.Current.SaveChangesAsync();
                    await uow.CompleteAsync();
                }
            }

            return newTenantId;

        }

        public void VerifyDefaultData(string countrycode)
        {
            int CountryID = _countries.FirstOrDefault(x => x.Code == countrycode).Id;

            string[] defaults = new string[] { "OTHER", "NONE" };
            string defaultlogo = "default-profile-picture.png";
            // Verify Bank
            foreach (var data in defaults)
            {
                var bank = _Banks.FirstOrDefault(c => c.BankName == data && c.CountryID == CountryID);
                // If not exist for current country, then add  banknames "OTHER" and "NONE" to tblbanks with countryid and enable
                if (bank == null)
                {
                    var client = new Banks()
                    {
                        BankName = data,
                        CountryID = CountryID,
                        isActive = true
                    };
                    _Banks.Insert(client);
                }
                else // Enable Bank if not
                {
                    bank.isActive = true;
                    _Banks.Update(bank);
                }

                // Verify Insurer

                var insurer = _insurer.FirstOrDefault(c => c.InsurerName == data && c.CountryID == CountryID);
                // If not exist for current country, then add  InsurerName "OTHER" and "NONE" to tblinsurerMaster with countryid and enable
                if (insurer == null)
                {

                    var client = new InsurerMaster()
                    {
                        InsurerName = data,
                        CountryID = CountryID,
                        LogoPicture = defaultlogo,
                        Mask = data,
                        IsActive = true
                    };
                    int Id = _insurer.InsertAndGetId(client);

                    var logo = new InsurerPics()
                    {
                        Bytes = GetBytes(defaultlogo),
                        InsurerID = Id,
                    };
                    _insurerpic.Insert(logo);
                }
                else // Enable Bank if not
                {
                    insurer.IsActive = true;
                    _insurer.Update(insurer);
                }

                // Verify Broker

                var broker = _broker.FirstOrDefault(c => c.BrokerName == data && c.CountryID == CountryID);
                // If not exist for current country, then add  brokerName "OTHER" and "NONE" to tblBrokerMaster with countryid and enable
                if (broker == null)
                {
                    var client = new BrokerMaster()
                    {
                        BrokerName = data,
                        CountryID = CountryID,
                        LogoPicture = defaultlogo,
                        Mask = data,
                        IsActive = true
                    };
                    int id = _broker.InsertAndGetId(client);


                    var logobroker = new BrokerMasterPics()
                    {
                        Bytes = GetBytes(defaultlogo),
                        BrokerID = id
                    };
                    _brokerpic.Insert(logobroker);
                }
                else // Enable Bank if not
                {
                    broker.IsActive = true;
                    _broker.Update(broker);
                }
                //Default Vendors
                var vendor = _vendors.FirstOrDefault(c => c.SupplierName == data && c.CountryID == CountryID);
                // If not exist for current country, then add  SupplierName "OTHER" and "NONE" to tblVendorMain with countryid and enable
                if (vendor == null)
                {
                    var client = new VendorMain()
                    {
                        SupplierCode = Guid.NewGuid(),
                        SupplierName = data,
                        CountryID = CountryID
                    };
                    _vendors.Insert(client);
                }

                //Default Towoperator
                var tow = _tow.FirstOrDefault(c => c.Description == data && c.CountryID == CountryID);
                // If not exist for current country, then add  Description "OTHER" and "NONE" to tblTowOperator with countryid and enable
                if (tow == null)
                {
                    var client = new TowOperator()
                    {
                        Description = data,
                        CountryID = CountryID,
                        isActive = true
                    };
                    _tow.Insert(client);
                }
            }
        }

        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private void CreateTowOperators(int tenantId, string CountryCode)
        {
            int CountryID = _countries.FirstOrDefault(x => x.Code == CountryCode).Id;

            string allstatus = "1 TIME TOWING,112 AUTOROADSIDE,A1 ASSIST,AA TOWING,ABOVE TOWING,ABS TOWING,ABSOLUTE TOWING,ACJ TOWING," +
               "ADNANCED RECOVERIES,AFRICA TOWING,AGT TOWING,ALBERTON TOWING,ALL WAYS TOWING,ALLTOW SERVICES,AM TOWING,ATS TOWING,AUTO ACCIDENT ASSIST," +
               "AUTO TECH TOWING,AUTOHAUS TOWING,BAPELA TOWING,BEUKES TOWING,BIG D ROADSIDE ASSIST,CAS TOWING,CENTOW TOWING,CLASSIQUE TOWING,DA TOWING," +
               "DAANTJIES TOWING,DC TOWING,DIVERSE TOWING &LOGISTICS,DOT TOWING,EAGLE TOWING,EASYWAY TOWING,EXCLUSIVE TOWING,EXECUTIVE CARRIERS,EXTREME TOWING," +
               "FIRST ROAD EMERGENCY,FLEETSIDE TOWING,FREDS AUTOBODY,GLOBAL TOW ASSIST,GLYNMART TOWING,J.J TOWING,JIDZ RECOVERIES,JML TOWING,MAGALIES AUTO CENTRE," +
               "MAGIC TOWING,METRO ACCIDENT ASSISTANCE,MILLENIUM TOWING,MIRACLE TOWING,MOMOS TOWING,NEWLANDS TOWING,NONE,ON CALL TOWING,OTHER,PJ'S TOWING,SEDS 24 HOUR TOWING," +
               "SNAP 123 TOWING,SOUTHSIDE TOWING,UNIQUE TOWING";
            string[] str = allstatus.Split(',');
            TowOperator tow = null;
            for (int i = 0; i < str.Length; i++)
            {
                tow = new TowOperator();
                tow.Description = str[i];
                tow.TenantId = tenantId;
                tow.isActive = false;
                tow.CountryID = CountryID;
                _TowOperator.Insert(tow);
            }
        }

        public virtual string GetCurrentTenantPlan(int PlanId)
        {
            return _SignonPlansRepository.FirstOrDefault(x => x.Id == PlanId).PlanName;
        }
        public virtual List<SignonPlans> GetTenantPlans()
        {
            List<SignonPlans> data = _SignonPlansRepository.GetAll().Where(x=>x.isActive == true && x.IsDeleted == false).ToList();

            return data;
        }
        public virtual List<Tenant> GetTenants()
        {
            var tenants = Tenants.ToList();

            return tenants;
        }
        public virtual List<Countries> GetCountries()
        {
            return _countries.GetAll().ToList();
        }
        public virtual List<CountryandCurrency> GetCurrencies()
        {
            return _countryandcurrency.GetAll().ToList();
        }
        public virtual ReadOnlyCollection<TimeZoneInfo> GetTimeZones()
        {
            return TimeZoneInfo.GetSystemTimeZones();
            //foreach (TimeZoneInfo timeZoneInfo in TimeZoneInfo.GetSystemTimeZones())
            //{
            //    Console.WriteLine("{0}", timeZoneInfo.DisplayName, timeZoneInfo.Id);
            //}
        }
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
