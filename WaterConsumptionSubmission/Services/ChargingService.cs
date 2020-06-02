using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterConsumptionSubmission.Services
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

        public async Task<ChargingInformation> GetChargingInformationForConsumerAsync(Guid deviceId)
        {
            try
            {
                var chargingInformationResult = await _httpClient.GetAsync("charging/info").ConfigureAwait(false);
                var chargingInformationResultAsString = await chargingInformationResult.Content.ReadAsStringAsync();
                var chargingInformationDeserialized = JsonConvert.DeserializeObject<ChargingInformation>(chargingInformationResultAsString);

                _log.Info("Water Charging-API return value: " + chargingInformationDeserialized.CurrentTaxRate);
                return chargingInformationDeserialized;
            }
            catch (Exception ex)
            {
                _log.Error("Water Charging-API failed with exception: " + ex);
                throw;
            }
        }
    }
}
