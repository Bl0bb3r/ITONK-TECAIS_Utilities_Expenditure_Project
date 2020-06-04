using ElectricityExpenditure.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ElectricityExpenditure.Services
{
    public class PricingService : IPricingService
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private HttpClient _httpClient;
        public PricingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PricingInfo> GetPricingInfoAsync()
        {
            try
            {
                var pricingInfoResult = await _httpClient.GetAsync("api?type=currenthouraverage").ConfigureAwait(false);
                var pricingInfoAsString = await pricingInfoResult.Content.ReadAsStringAsync();

                //JSON string contains an array with an single object - Trimming square brackets before deserializing.
                var pricingInfoDeserialized =
                    JsonConvert.DeserializeObject<PricingInfo>(pricingInfoAsString
                    .Substring(1, pricingInfoAsString.Length - 3));

                _log.Info("Electricity Pricing-API returning value: " + pricingInfoDeserialized.Price);
                return pricingInfoDeserialized;
            }
            catch (Exception ex)
            {
                _log.Error("Electricity Pricing-API failed with exception: " + ex);
                throw;
            }
        }
    }
}
