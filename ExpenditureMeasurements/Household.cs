using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpenditureMeasurements
{
    class Household
    {
        public int ID { get; set; }

        public IReadOnlyList<MeasurementDevice> Devices { get; }

        public Household(int id, IEnumerable<MeasurementDevice> devices)
        {
            ID = id;
            Devices = devices.ToList();
        }
    }
}
