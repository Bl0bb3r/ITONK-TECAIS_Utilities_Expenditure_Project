using RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterExpenditure.Models.Events
{
    public class Measurement : EventBase
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public int HouseId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }

        public Measurement(string eventType) : base(eventType)
        {
        }
    }
}
