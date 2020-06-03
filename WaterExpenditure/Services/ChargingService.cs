using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WaterExpenditure.Models;

namespace WaterExpenditure.Services
{
    public class ChargingService : IChargingService
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private HttpClient _httpClient;

        public ChargingService() { }

        public ChargingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChargingInfo> GetChargingInfoForConsumerAsync(Guid deviceId)
        {
            try
            {
                var chargingInfoResult = await _httpClient.GetAsync("charging/info").ConfigureAwait(false);
                var chargingInfoResultAsString = await chargingInfoResult.Content.ReadAsStringAsync();
                var chargingInfoDeserialized = JsonConvert.DeserializeObject<ChargingInfo>(chargingInfoResultAsString);

                _log.Info("Water Charging-API return value: " + chargingInfoDeserialized.CurrentTaxRate);
                return chargingInfoDeserialized;
            }
            catch (Exception ex)
            {
                _log.Error("Water Charging-API failed with exception: " + ex);
                throw;
            }
        }
    }
}
