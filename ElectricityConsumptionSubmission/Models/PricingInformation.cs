using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityConsumptionSubmission.Models
{
    public class PricingInformation
    {
        public DateTime Timestamp { get; set; }
        public long MillisUTC { get; set; }
        public double Price { get; set; }
    }
}
