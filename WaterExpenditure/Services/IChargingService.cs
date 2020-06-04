using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterExpenditure.Models;

namespace WaterExpenditure.Services
{
    public interface IChargingService
    {
        Task<ChargingInfo> GetChargingInfoForConsumerAsync(Guid deviceId);
    }
}
