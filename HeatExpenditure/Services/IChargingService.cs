using HeatExpenditure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeatExpenditure.Services
{
    public interface IChargingService
    {
        Task<ChargingInfo> GetChargingInfoForConsumerAsync(Guid deviceId);
    }
}
