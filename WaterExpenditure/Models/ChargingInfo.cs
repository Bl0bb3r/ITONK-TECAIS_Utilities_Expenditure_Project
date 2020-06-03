using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterExpenditure.Models
{
    public class ChargingInfo
    {
        public DateTime Timestamp { get; set; }
        public double CurrentTaxRate { get; set; }
        public IReadOnlyList<double> Charges { get; set; }
        public Guid ConsumerId { get; set; }
    }
}
