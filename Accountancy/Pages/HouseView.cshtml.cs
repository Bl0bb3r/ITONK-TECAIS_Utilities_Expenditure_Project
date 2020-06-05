using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Accountancy.Data;
using Accountancy.Models;

namespace Accountancy.Pages
{
    public class HouseViewModel : PageModel
    {
        private readonly Accountancy.Data.AccountancyContext _context;

        public HouseViewModel(Accountancy.Data.AccountancyContext context)
        {
            _context = context;
        }

        public IList<HouseholdModel> HouseholdModel { get;set; }
        public IList<AccountancyInfo> Bills { get; set; }

        public async Task OnGetAsync()
        {
            HouseholdModel = await _context.Households.ToListAsync();
            Bills = await _context.BillingInfo.ToListAsync();
        }
    }
}
