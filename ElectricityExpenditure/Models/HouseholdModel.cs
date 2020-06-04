using ElectricityExpenditure.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityExpenditure.Models
{
    public class HouseholdModel
    {
        public int ID { get; set; }
        public IEnumerable<AccountancyRelay> Invoices { get; set; }
    }
}
