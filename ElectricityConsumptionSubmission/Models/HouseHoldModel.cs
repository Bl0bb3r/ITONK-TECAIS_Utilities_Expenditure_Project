using ElectricityConsumptionSubmission.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityConsumptionSubmission.Models
{
    public class HouseHoldModel
    {
        public int ID { get; set; }
        public IEnumerable<AccountingMessage> Invoices { get; set; }
    }
}
