﻿using HeatExpenditure.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HeatExpenditure.Services
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
                var chargingInfo = await _httpClient.GetAsync("charging/info");
                var responseAsString = await chargingInfo.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ChargingInfo>(responseAsString);
                _log.Info("Heat Charging-API return value: " + result.CurrentTaxRate);
                return result;
            }
            catch (Exception ex)
            {
                _log.Error("Heat Charging-API failed with exception: " + ex);
                throw;
            }
        }
    }
}
