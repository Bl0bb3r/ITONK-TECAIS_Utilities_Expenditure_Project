using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenditureMeasurements
{
    class Measurement
    {
        public Guid ID { get; set; }

        public Guid DeviceID { get; set; }

        public int HouseID { get; set; }

        public MeasurementCategory ExpenditureType { get; set; }

        public double Value { get; set; }

        public DateTime Timestamp { get; set; }

        private Measurement(Guid id, Guid deviceID, int houseID, MeasurementCategory expenditureType, double value, DateTime timestamp)
        {
            ID = id;
            DeviceID = deviceID;
            HouseID = houseID;
            ExpenditureType = expenditureType;
            Value = value;
            Timestamp = timestamp;
        }

        public static Measurement Create(Guid deviceID, int houseID, MeasurementCategory expenditureType, double value)
        {
            return new Measurement(Guid.NewGuid(), deviceID, houseID, expenditureType, value, DateTime.Now);
        }
    }

    public enum Status
    {
        Ok, Warning, Error, Fatal
    }

    public class StatusReport
    {
        public Guid DeviceID { get; set; }

        public Status Status { get; set; }

        public string Message { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
