using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountancy.Models
{
    public class ChargingInfo
    {
        public double TaxRate { get; set; }

        public Guid UtilityConsumerID { get; set; }

        public IReadOnlyList<double>ChargesList { get; set; }

        public DateTime Timestamp { get; set; }

    }
}
