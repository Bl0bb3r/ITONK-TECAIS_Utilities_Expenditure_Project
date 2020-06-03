using HeatExpenditure.Models;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HeatExpenditure.Services
{
    public class PricingService : IPricingService
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private HttpClient _httpClient;

        public PricingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<PricingInfo> GetPricingInfoAsync()
        {
            try
            {
                var pricingInfoResult = await _httpClient.GetAsync("series/?api_key=67b6cde351cdb9052134a6221589155b&series_id=NG.N9130US3.A").ConfigureAwait(false);
                var pricingInfoAsString = await pricingInfoResult.Content.ReadAsStringAsync();

                //get data from JSON Object
                JObject obj = JObject.Parse(pricingInfoAsString);
                var objPrice = (double)obj["series"][0]["data"][0][1];

                PricingInfo pricingInfo = new PricingInfo
                {
                    Price = objPrice
                };

                log.Info("Heat Pricing-API returning value: " + pricingInfo.Price);
                return pricingInfo;
            }
            catch (Exception ex)
            {
                log.Info("Heat Pricing-API failed with exception: " + ex);
                throw;
            }

        }
    }
}
