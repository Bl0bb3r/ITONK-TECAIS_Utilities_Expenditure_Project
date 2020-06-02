using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accountancy.Models;

namespace Accountancy.Data
{
    public class AccountancyContext : DbContext
    {
        public AccountancyContext(DbContextOptions<AccountancyContext> options) : base(options) { }

        public DbSet<AccountingInfo> BillingInfo { get; set; }

        public DbSet<HouseholdModel> Households { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<AccountingInfo>().ToTable("Accounting Information");
            model.Entity<HouseholdModel>().ToTable("Household Model");
        }

    }
}
