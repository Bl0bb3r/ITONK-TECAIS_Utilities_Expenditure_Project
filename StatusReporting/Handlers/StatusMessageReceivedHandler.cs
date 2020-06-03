using StatusReporting.Data;
using StatusReporting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMq;
using Status = StatusReporting.Data.Status;

namespace StatusReporting.Handlers
{
    public class StatusMessageReceivedHandler : IEventHandler<StatusReportMessage>
    {
        private readonly StatusDbContext _dbContext;

        public StatusMessageReceivedHandler(StatusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(StatusReportMessage @event)
        {
            var statusEntity = new Status
            {
                CurrentAmount = @event.CurrentAmount,
                DeviceId = @event.DeviceId,
                DeviceStatus = @event.Status.ToString(),
                HouseId = @event.HouseId
            };
            await _dbContext.Statuses.AddAsync(statusEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
