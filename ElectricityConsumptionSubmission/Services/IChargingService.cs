using System;
using System.Threading.Tasks;
using ElectricityConsumptionSubmission.Models;

namespace ElectricityConsumptionSubmission.Services
{
    public interface IChargingService
    {
        Task<ChargingInformation> GetChargingInformationForConsumerAsync(Guid deviceId);
    }
}
