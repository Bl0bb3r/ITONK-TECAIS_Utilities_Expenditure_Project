using ElectricityExpenditure.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityExpenditure.Models
{
    public class HouseHoldModel
    {
        public int ID { get; set; }
        public IEnumerable<AccountingMessage> Invoices { get; set; }
    }
}
