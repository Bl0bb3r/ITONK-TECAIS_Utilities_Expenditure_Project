using ElectricityConsumptionSubmission.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityConsumptionSubmission.Services
{
    public interface IPricingService
    {
        Task<PricingInformation> GetPricingInformationAsync();
    }
}
