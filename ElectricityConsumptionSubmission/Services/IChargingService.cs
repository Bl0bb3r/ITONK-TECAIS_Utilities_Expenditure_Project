using System;
using System.Threading.Tasks;
using ElectricityExpenditure.Models;

namespace ElectricityExpenditure.Services
{
    public interface IChargingService
    {
        Task<ChargingInformation> GetChargingInformationForConsumerAsync(Guid deviceId);
    }
}
