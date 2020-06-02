﻿using HeatConsumptionSubmission.Models;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HeatConsumptionSubmission.Services
{
    public class PricingService : IPricingService
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private HttpClient _httpClient;

        public PricingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<PricingInformation> GetPricingInformationAsync()
        {
            try
            {
                var pricingInformationResult = await _httpClient.GetAsync("series/?api_key=67b6cde351cdb9052134a6221589155b&series_id=NG.N9130US3.A").ConfigureAwait(false);
                var pricingInformationAsString = await pricingInformationResult.Content.ReadAsStringAsync();

                //get data from JSON Object
                JObject obj = JObject.Parse(pricingInformationAsString);
                var objPrice = (double)obj["series"][0]["data"][0][1];

                PricingInformation pricingInformation = new PricingInformation
                {
                    Price = objPrice
                };

                log.Info("Heat Pricing-API returning value: " + pricingInformation.Price);
                return pricingInformation;
            }
            catch (Exception ex)
            {
                log.Info("Heat Pricing-API failed with exception: " + ex);
                throw;
            }

        }
    }
}