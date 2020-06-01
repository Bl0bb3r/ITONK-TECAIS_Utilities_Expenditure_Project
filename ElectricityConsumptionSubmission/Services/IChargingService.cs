using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectricityConsumptionSubmission.Models;

namespace ElectricityConsumptionSubmission.Services
{
    public class IChargingService
    {
        Task<ChargingInformation> GetCharginInformationForConsumerAsync(Guid deviceId);
    }
}
