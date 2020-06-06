using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountancy.Models
{
    public class AccountancyInfo
    {
        public int ID { get; set; }

        public int HouseholdModelID { get; set; }

        public string BillCategory { get; set; }

        public double NetVal { get; set; }

        public DateTime TimestampDateTime { get; set; }
    }
}
