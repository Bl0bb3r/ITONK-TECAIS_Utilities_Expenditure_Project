using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterConsumptionSubmission.Models;

namespace WaterConsumptionSubmission.Services
{
    public interface IPricingService
    {
        Task<PricingInformation> GetPricingInformationAsync();
    }
}
