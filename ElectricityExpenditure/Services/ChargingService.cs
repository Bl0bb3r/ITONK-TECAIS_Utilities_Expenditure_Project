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
    public class ChargingService : IChargingService
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private HttpClient _httpClient;

        public ChargingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChargingInfo> GetChargingInfoForConsumerAsync(Guid deviceId)
        {
            try
            {
                var chargingInfoResult = await _httpClient.GetAsync("charging/info").ConfigureAwait(false);
                var chargingInfoAsString = await chargingInfoResult.Content.ReadAsStringAsync();
                var chargingInfoDeserialized =
                    JsonConvert.DeserializeObject<ChargingInfo>(chargingInfoAsString);

                _log.Info("Electricity Charging-API return value: " + chargingInfoDeserialized.CurrentTaxRate);

                return chargingInfoDeserialized;
            }
            catch (Exception ex)
            {
                _log.Error("Electricity Charging-API failed with exception: " + ex);
                throw;
            }
        }
    }
}
