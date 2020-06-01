﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accountancy.Models
{
    public class AccountingInfo
    {
        public int ID { get; set; }

        public int HouseholdModelID { get; set; }

        public string Bill { get; set; }

        public double NetVal { get; set; }

        public DateTime TimestampDateTime { get; set; }

    }
}
