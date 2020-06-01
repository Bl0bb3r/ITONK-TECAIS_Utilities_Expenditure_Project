using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityConsumptionSubmission.Services
{
    public class IPricingService
    {
        Task<PricingInformation> GetPricingInformationAsync();
    }
}
