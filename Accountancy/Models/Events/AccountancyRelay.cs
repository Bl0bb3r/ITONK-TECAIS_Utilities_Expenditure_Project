using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.RabbitMq;

namespace Accountancy.Models.Events
{
    public class AccountancyRelay : EventBase
    {
        public int HouseID { get; set; }

        public double NetVal { get; set; }

        public PricingInfo PricingInformation { get; set; }

        public ChargingInfo ChargingInformation { get; set; }

        public DateTime Timestamp { get; set; }

        public String Type { get; set; }

        public AccountancyRelay(string eventType) : base(eventType)
        {

        }
    }
}
