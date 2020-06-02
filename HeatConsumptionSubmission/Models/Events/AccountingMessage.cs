﻿using RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeatConsumptionSubmission.Models.Events
{
    public class AccountingMessage : EventBase
    {
        public double Amount { get; }
        public int HouseId { get; }
        public PricingInformation PricingInformation { get; }
        public ChargingInformation ChargingInformation { get; }
        public DateTime Timestamp { get; }
        public String Type { get; }
        private AccountingMessage(string eventType, int houseID, double amount, PricingInformation pricingInformation, ChargingInformation chargingInformation, DateTime timestamp, String type) : base(eventType)
        {
            HouseId = houseID;
            Amount = amount;
            PricingInformation = pricingInformation;
            ChargingInformation = chargingInformation;
            Timestamp = timestamp;
            Type = type;
        }

        public static AccountingMessage Create(double amount, int houseID, PricingInformation pricingInformation,
            ChargingInformation chargingInformation)
        {
            return new AccountingMessage("accounting", houseID, amount, pricingInformation, chargingInformation, DateTime.Now, "Heat");
        }
    }
}