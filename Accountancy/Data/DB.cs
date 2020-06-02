using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accountancy.Models;
using Microsoft.EntityFrameworkCore;

namespace Accountancy.Data
{
    public class DB
    {
        public static void DBInit(AccountancyContext context)
        {
            context.Database.EnsureCreated();

            if (context.Households.Any())
            {
                return;
            }

            var households = new HouseholdModel[]
            {
                new HouseholdModel{ID = 500}, new HouseholdModel{ID = 1000}, new HouseholdModel{ID = 1500}
                
            };

            foreach (HouseholdModel Hm in households)
            {
                context.Households.Add(Hm);
            }

            context.Database.OpenConnection();

            try
            {
                context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT HouseholdModel ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT HouseholdModel OFF");
            } finally
            {
                context.Database.CloseConnection();
            }

            var UtilityBills = new AccountingInfo[]
            {
                new AccountingInfo{HouseholdModelID = 500, BillCategory = "Water", NetVal = 500, TimestampDateTime = DateTime.Now},
                new AccountingInfo{HouseholdModelID = 500, BillCategory = "Heat", NetVal = 500, TimestampDateTime = DateTime.Now},
                new AccountingInfo{HouseholdModelID = 500, BillCategory = "Electricity", NetVal = 500, TimestampDateTime = DateTime.Now},
                new AccountingInfo{HouseholdModelID = 1000, BillCategory = "Water", NetVal = 500, TimestampDateTime = DateTime.Now},
                new AccountingInfo{HouseholdModelID = 1000, BillCategory = "Heat", NetVal = 500, TimestampDateTime = DateTime.Now},
                new AccountingInfo{HouseholdModelID = 1000, BillCategory = "Electricity", NetVal = 500, TimestampDateTime = DateTime.Now},
                new AccountingInfo{HouseholdModelID = 1500, BillCategory = "Water", NetVal = 0, TimestampDateTime = DateTime.Now},
                new AccountingInfo{HouseholdModelID = 1500, BillCategory = "Heat", NetVal = 500, TimestampDateTime = DateTime.Now}

            };
            foreach (AccountingInfo Ai in UtilityBills)
            {
                context.BillingInfo.Add(Ai);
            }

            context.SaveChanges();


        }
    }
}
