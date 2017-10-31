using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.EntityFramework;
using PanelMasterMVC5Separate.MultiTenancy;
using PanelMasterMVC5Separate.Quotings;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace MasterData
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PanelMasterMVC5SeparateDbContext("Data Source=localhost;Initial Catalog=PanelMasterMVC5Separate;Integrated Security=True");


            System.Console.WriteLine("Would you like to add master data to tblJobstatus? Y/N");
            var anwser = System.Console.ReadLine();
            if (anwser?.ToLower() == "Y".ToLower())
                AddJobstatus(context);

            System.Console.WriteLine("Would you like to add master data to tblJobstatusMask? Y /N");
            anwser = System.Console.ReadLine();
            if (anwser?.ToLower() == "Y".ToLower())
                AddJobstatusMask(context);

            System.Console.WriteLine("Would you like to create for tblNotProceedReason? Y/N");
            anwser = System.Console.ReadLine();

            if (anwser?.ToLower() == "Y".ToLower())
                NotProceedReason(context);

            System.Console.WriteLine("Would you like to create master SignOnPlans? Y/N");
            anwser = System.Console.ReadLine();

            if (anwser?.ToLower() == "Y".ToLower())
                SignOnPlans(context);

            System.Console.WriteLine("Would you like to create master Quote Status? Y/N");
            anwser = System.Console.ReadLine();

            if (anwser?.ToLower() == "Y".ToLower())
                AddQuoteStatus(context);

            System.Console.WriteLine("Would you like to create master repair types? Y/N");
            anwser = System.Console.ReadLine();

            if (anwser?.ToLower() == "Y".ToLower())
                AddRepairTypes(context);

            System.Console.WriteLine("Would you like to create master quote categories? Y/N");
            anwser = System.Console.ReadLine();

            if (anwser?.ToLower() == "Y".ToLower())
                AddQuoteCategories(context);

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                DisplayMessage(e.Message);
            }

            System.Console.WriteLine("complete");
            System.Console.ReadLine();
        }

        private static void AddJobstatusMask(PanelMasterMVC5SeparateDbContext context)
        {
            var data = new List<JobstatusMask>();

            data.AddRange(GetJobStatusMask());

            context.JobstatusMask.AddOrUpdate(data.ToArray());
        }

        private static IEnumerable<JobstatusMask> GetJobStatusMask()
        {
            string allstatus = "Quoting,Authorised,Conversion,Work In Progress,Delivery,Invoicing,Completed,Other";
            string[] str = allstatus.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                yield return JobstatusMask(str[i]);
            }
        }
        private static JobstatusMask JobstatusMask(string desc)
        {
            return new JobstatusMask()
            {
                Description1 = desc
            };
        }
        private static void AddJobstatus(PanelMasterMVC5SeparateDbContext context)
        {
            var data = new List<Jobstatus>();

            data.AddRange(GetStatuses());

            context.Jobstatus.AddOrUpdate(data.ToArray());
        }

        private static IEnumerable<Jobstatus> GetStatuses()
        {
            string allstatus = "Quoting,Repair Authorised,Parts Confirmation/ Pre Order,Repair Booked,Missed Appointment,Checked in On Site," +
                "Awaiting Additionals Authorization,Converted,Parts Ordering,Parts Received,Express Repair,Panelbeating,Paint Preparation,Painting," +
                "Assembly,Polishing,Cleaning,Quality Control,Ready,Collection Booked,Collection Missed Appointment,Delivered,Final Costing,Invoiced," +
                "Payment Received,Closed,Not Proceeding";
            string[] str = allstatus.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                yield return Jobstatus(str[i]);
            }
        }

        private static Jobstatus Jobstatus(string desc)
        {
            return new Jobstatus()
            {
                Description = desc
            };
        }
        private static void NotProceedReason(PanelMasterMVC5SeparateDbContext context)
        {
            var data = new List<NotProceedReason>();
            data.AddRange(GetNotProceedReason());
            context.NotProceedReason.AddOrUpdate(data.ToArray());
        }

        private static IEnumerable<NotProceedReason> GetNotProceedReason()
        {
            yield return NotProceedReasons("Vehicle Written Off");
            yield return NotProceedReasons("Assessor sent job elsewhere");
            yield return NotProceedReasons("Insurer / Broker sent job elsewhere");
            yield return NotProceedReasons("Insurer rejected claim");
            yield return NotProceedReasons("Client prefer to repair elsewhere");
            yield return NotProceedReasons("Client withdrew Claim");
            yield return NotProceedReasons("Funding - No Excess Money");
            yield return NotProceedReasons("Funding - No Money to Repair");
            yield return NotProceedReasons("Funding - Quote too Expensive");
            yield return NotProceedReasons("Vehicle sold before repair");
            yield return NotProceedReasons("Wrong Geographic location");
            yield return NotProceedReasons("No Manufacturer Approvals");
            yield return NotProceedReasons("Parts not Available");
            yield return NotProceedReasons("Duplicate Job");
            yield return NotProceedReasons("Other");
        }

        private static NotProceedReason NotProceedReasons(string desc)
        {
            return new NotProceedReason()
            {
                Description = desc
            };
        }

        private static void SignOnPlans(PanelMasterMVC5SeparateDbContext context)
        {
            var data = new List<SignonPlans>();
            data.AddRange(GetPlans());
            context.SignonPlan.AddOrUpdate(data.ToArray());
        }
        private static IEnumerable<SignonPlans> GetPlans()
        {
            yield return SignonPlans("Bugdet", 24.00, "blue", 3);
            yield return SignonPlans("Solo", 39.00, "red", 5);
            yield return SignonPlans("Start Up", 59.00, "green", 20);
            yield return SignonPlans("Enterprise", 128.00, "purple", 100);
        }
        public static SignonPlans SignonPlans(string planname, double price, string color, int member)
        {
            return new SignonPlans()
            {
                PlanName = planname,
                Price = price,
                HeaderColor = color,
                Members = member
            };
        }

        //Master Data entry : QuoteStatus
        private static void AddQuoteStatus(PanelMasterMVC5SeparateDbContext context)
        {
            var data = new List<QuoteStatus>();
            data.AddRange(GetQuoteStatus());
            context.QuoteStatus.AddOrUpdate(data.ToArray());
        }
        private static IEnumerable<QuoteStatus> GetQuoteStatus()
        {
            yield return QuoteStatus("Quote Preparation");
            yield return QuoteStatus("Quote Send - Awating Authorization");
            yield return QuoteStatus("Quote Approved");
            yield return QuoteStatus("Quote Rejected");
            yield return QuoteStatus("Closed");

        }
        public static QuoteStatus QuoteStatus(string description)
        {
            return new QuoteStatus()
            {
                Enabled = true,
                Description = description
            };
        }

        //Master Data entry : QuoteCategories
        private static void AddQuoteCategories(PanelMasterMVC5SeparateDbContext context)
        {
            var data = new List<QuoteCategories>();
            data.AddRange(GetQuoteCategories());
            context.QuoteCategories.AddOrUpdate(data.ToArray());
        }
        private static IEnumerable<QuoteCategories> GetQuoteCategories()
        {
            yield return QuoteCategories("Initial Repair");
            yield return QuoteCategories("Upsell");

        }
        public static QuoteCategories QuoteCategories(string description)
        {
            return new QuoteCategories()
            {
                Enabled = true,
                Description = description
            };
        }


        //Master Data entry : RepairTypes
        private static void AddRepairTypes(PanelMasterMVC5SeparateDbContext context)
        {
            var data = new List<RepairTypes>();
            data.AddRange(GetRepairTypes());
            context.RepairTypes.AddOrUpdate(data.ToArray());
        }
        private static IEnumerable<RepairTypes> GetRepairTypes()
        {
            yield return RepairTypes("Cash");
            yield return RepairTypes("Insured");
            yield return RepairTypes("Warantee");
        }
        public static RepairTypes RepairTypes(string description)
        {
            return new RepairTypes()
            {
                Enabled = true,
                Description = description
            };
        }

        private static void DisplayMessage(string message)
        {
            System.Console.WriteLine();
            System.Console.ReadLine();
        }
    }
}
