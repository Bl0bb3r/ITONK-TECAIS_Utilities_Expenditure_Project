using RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeatExpenditure.Models.Events
{
    public class AccountancyRelay : EventBase
    {
        public double Amount { get; }
        public int HouseId { get; }
        public PricingInfo PricingInformation { get; }
        public ChargingInfo ChargingInformation { get; }
        public DateTime Timestamp { get; }
        public String Type { get; }
        private AccountancyRelay(string eventType, int houseID, double amount, PricingInfo pricingInfo, ChargingInfo chargingInfo, DateTime timestamp, String type) : base(eventType)
        {
            HouseId = houseID;
            Amount = amount;
            PricingInformation = pricingInfo;
            ChargingInformation = chargingInfo;
            Timestamp = timestamp;
            Type = type;
        }

        public static AccountancyRelay Create(double amount, int houseID, PricingInfo pricingInfo,
            ChargingInfo chargingInfo)
        {
            return new AccountancyRelay("accounting", houseID, amount, pricingInfo, chargingInfo, DateTime.Now, "Heat");
        }
    }
}
