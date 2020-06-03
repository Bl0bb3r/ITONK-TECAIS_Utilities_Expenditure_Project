using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenditureMeasurements
{
    class MeasurementDevice
    {
        public Guid ID { get; }

        public int HouseID { get; }

        public MeasurementCategory ExpenditureType { get; }

        public string SerialNumber { get; }

        public string Manufacturer { get; }

        public DateTime Timestamp { get; private set; }

        public double CurrVal { get; private set; }

        public double PrevVal { get; private set; }


        public MeasurementDevice(Guid id, int houseID, MeasurementCategory expenditureType, string serialNumber, string manufacturer, double devVal)
        {
            ID = id;
            HouseID = houseID;
            ExpenditureType = expenditureType;
            SerialNumber = serialNumber ?? throw new ArgumentNullException(nameof(serialNumber));
            Manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));
            Timestamp = DateTime.Now;
            CurrVal = devVal;
            PrevVal = 0;
        }

        public void UpdateVal()
        {
            var randomVal = new Random();
            CurrVal += randomVal.NextDouble() * 100;
            PrevVal = CurrVal;
        }

        public Measurement GenerateAMeasurement()
        {
            UpdateVal();
            Measurement newMeasurement = Measurement.Create(ID, HouseID != 0 ? HouseID : new Random().Next(0, 10), ExpenditureType, CurrVal);
            Timestamp = DateTime.Now;
            return newMeasurement;
        }

        public StatusReport GenerateAStatusReport(int houseID)
        {
            return new StatusReport
            {
                DeviceID = ID,
                Message = "Everything looks normal",
                Status = Status.Ok,
                Timestamp = DateTime.Now
            };
        }
    }
}
