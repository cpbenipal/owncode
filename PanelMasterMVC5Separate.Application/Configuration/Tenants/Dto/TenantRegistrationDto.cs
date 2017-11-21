using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Configuration.Tenants.Dto
{
    public class TenantCompanyDto
    {
        public int TenantId { get; set; }
        [Required]
        public string companyName { get; set; }
        [Required]
        public string phoneNumber { get; set; } 
        public string faximileeNumber { get; set; }        
        public string address { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string country { get; set; }
        [Required]
        public string timezone { get; set; }
        [Required]
        public string currency { get; set; }
        [Required]
        public string companyRegistrationNo { get; set; }
        [Required]
        public string companyVatNo { get; set; }
        public string invoicingInstruction { get; set; }

        public string countryName { get; set; } 
        public string currencyName { get; set; }

        
    }
    public class TenantRegisterDto
    {
        public int TenantId { get; set; }
        [Required]
        public int planId { get; set; } 

        [Required]        
        [StringLength(128)]
        public string CardHoldersName { get; set; }

        [Required]
        [StringLength(16)]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(7)]
        public string CardExpiration { get; set; }

        [Required]
        [StringLength(4)]
        public string CVV { get; set; }

        public string payment { get; set; }
        public bool paymentoption1 { get; set; }
        public bool paymentoption2 { get; set; }
        public string CurrentPlan { get; set; }
        public string FullName { get; set; }
    }
    public class CurrencyDto
    {
        public string CurrencyCode { get; set; }
        public string CurrencyType { get; set; }
    }

    public class CountriesDto
    {
        public string Code { get; set; }
        public string Country { get; set; }
    }

    public class TimeZoneDto
    {
        public string DisplayName { get; set; }
        public string Id { get; set; }
    }

    public class PlanDto
    {
        public string PlanName { get; set; }
        public int Id { get; set; }
    }
}
