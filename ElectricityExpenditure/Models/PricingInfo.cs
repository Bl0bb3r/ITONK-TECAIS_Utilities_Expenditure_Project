using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityExpenditure.Models
{
    public class PricingInfo
    {
        public DateTime Timestamp { get; set; }
        public long MillisUTC { get; set; }
        public double Price { get; set; }
    }
}
