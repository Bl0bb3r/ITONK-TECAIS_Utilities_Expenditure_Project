using RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterExpenditure.Models.Events
{
    public class AccountancyRelay : EventBase
    {
        public double Amount { get; }
        public int HouseId { get; }
        public PricingInfo PricingInfo { get; }
        public ChargingInfo ChargingInfo { get; }
        public DateTime Timestamp { get; }
        public String Type { get; }
        private AccountancyRelay(string eventType, int houseId, double amount, PricingInfo pricingInfo, ChargingInfo chargingInfo, DateTime timestamp, String type) : base(eventType)
        {
            HouseId = houseId;
            Amount = amount;
            PricingInfo = pricingInfo;
            ChargingInfo = chargingInfo;
            Timestamp = timestamp;
            Type = type;
        }

        public static AccountancyRelay Create(double amount, int houseID, PricingInfo pricingInfo,
            ChargingInfo chargingInfo)
        {
            return new AccountancyRelay("accounting", houseID, amount, pricingInfo, chargingInfo, DateTime.Now, "Water");
        }
    }
}
