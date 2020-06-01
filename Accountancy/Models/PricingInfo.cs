using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountancy.Models
{
    public class PricingInfo
    {
        public double Price { get; set; }
        
        public DateTime Timestamp { get; set; }

        public long UTCTimeToMillis { get; set; }
    }
}
