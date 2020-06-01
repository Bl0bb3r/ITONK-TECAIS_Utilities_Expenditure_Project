using ElectricityConsumptionSubmission.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ElectricityConsumptionSubmission.Services
{
    public class ChargingService : IChargingService
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private HttpClient _httpClient;

        public ChargingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChargingInformation> GetChargingInformationForConsumerAsync(Guid deviceId)
        {
            try
            {
                var chargingInformationResult = await _httpClient.GetAsync("charging/info").ConfigureAwait(false);
                var chargingInformationAsString = await chargingInformationResult.Content.ReadAsStringAsync();
                var chargingInformationDeserialized =
                    JsonConvert.DeserializeObject<ChargingInformation>(chargingInformationAsString);

                _log.Info("Electricity Charging-API return value: " + chargingInformationDeserialized.CurrentTaxRate);

                return chargingInformationDeserialized;
            }
            catch (Exception ex)
            {
                _log.Error("Electricity Charging-API failed with exception: " + ex);
                throw;
            }
        }
    }
}
