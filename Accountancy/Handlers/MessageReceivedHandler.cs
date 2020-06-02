﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accountancy.Data;
using Accountancy.Models;
using Accountancy.Models.Events;
using log4net;
using Microsoft.EntityFrameworkCore;
using TECAIS.RabbitMq;

namespace Accountancy.Handlers
{
    public class MessageReceivedHandler : IEventHandler<AccountancyRelay>
    {

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly AccountancyContext _context;

        public MessageReceivedHandler(AccountancyContext context)
        {
            _context = context;
        }

        public Task Handle(AccountancyRelay @event)
        {
            _log.Debug($"Entering Handler with event - ID: {@event.HouseID}, Netto Value {@event.NetVal}, Timestamp {@event.Timestamp}, Type: {@event.Type}");
            try
            {
                if (@event.HouseID != 0)
                {
                    _log.Debug("Entering if-statement(@event.HouseID): " + @event.HouseID);
                    HouseholdModel House = new HouseholdModel {ID = @event.HouseID};

                    List<HouseholdModel> HouseList = _context.Households.ToList();
                    bool exists = false;

                    foreach (HouseholdModel Hm in HouseList)
                    {
                        if (Hm.ID == @event.HouseID)
                        {
                            exists = true;
                        }
                    }

                    Console.WriteLine($"Received message with netto value {@event.NetVal}");
                    if (exists)
                    {
                        _log.Debug("Entering Not-Found statement");
                        _context.Households.Add(House);
                        _context.Database.OpenConnection();

                        try
                        {
                            _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT HouseholdModel ON");
                            _context.SaveChanges();
                            _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT HouseholdModel OFF");
                        }
                        finally
                        {
                            _context.Database.CloseConnection();
                        }
                    }

                    // var 

                }
            }
        }
        // not completed - pushing again when done
    }
}
