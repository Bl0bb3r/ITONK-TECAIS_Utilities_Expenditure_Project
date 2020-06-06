using Accountancy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountancy.Data
{
    public class AccountancyContext : DbContext
    {
        public AccountancyContext(DbContextOptions<AccountancyContext> options) : base(options) { }

        public DbSet<AccountancyInfo> BillingInfo { get; set; }

        public DbSet<HouseholdModel> Households { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<AccountancyInfo>().ToTable("AccountancyInformation");
            model.Entity<HouseholdModel>().ToTable("HouseholdModel");
        }

    }
}
