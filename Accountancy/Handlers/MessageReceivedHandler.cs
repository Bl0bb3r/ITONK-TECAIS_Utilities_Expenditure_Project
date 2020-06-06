
using Accountancy.Data;
using Accountancy.Models;
using Accountancy.Models.Events;
using log4net;
using Microsoft.EntityFrameworkCore;
using RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                    HouseholdModel House = new HouseholdModel { ID = @event.HouseID };

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
                            _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT HouseholdModel ON");
                            _context.SaveChanges();
                            _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT HouseholdModel OFF");
                        }
                        finally
                        {
                            _context.Database.CloseConnection();
                        }
                    }

                    var accountingInfo = new AccountancyInfo
                    {
                        HouseholdModelID = @event.HouseID,
                        BillCategory = @event.Type,
                        NetVal = @event.NetVal,
                        TimestampDateTime = @event.Timestamp
                    };

                    _context.BillingInfo.Add(accountingInfo);
                    _context.SaveChanges();

                }

                _log.Debug("Returning from MessageReceivedHandler");
                return Task.CompletedTask;
            }
            catch (Exception exception)
            {
                _log.Error("Failed with exception: " + exception);
                throw;
            }
        }
    }
}
