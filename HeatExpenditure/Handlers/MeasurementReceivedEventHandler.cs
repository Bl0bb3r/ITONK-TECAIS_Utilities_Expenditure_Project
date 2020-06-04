using HeatExpenditure.Models;
using HeatExpenditure.Models.Events;
using HeatExpenditure.Services;
using RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeatExpenditure.Handlers
{
    public class MeasurementReceivedEventHandler : IEventHandler<Measurement>
    {
        private readonly IChargingService _chargingService;
        private readonly IPricingService _pricingService;
        private readonly IEventBus _eventBus;

        public MeasurementReceivedEventHandler(IChargingService chargingService, IPricingService pricingService, IEventBus eventBus)
        {
            _chargingService = chargingService;
            _pricingService = pricingService;
            _eventBus = eventBus;
        }

        public async Task Handle(Measurement @event)
        {
            var chargingInfoTask = _chargingService.GetChargingInfoForConsumerAsync(@event.DeviceId);
            var pricingInfoTask = _pricingService.GetPricingInfoAsync();
            await Task.WhenAll(chargingInfoTask, pricingInfoTask);

            var chargingInfo = await chargingInfoTask;
            var pricingInfo = await pricingInfoTask;
            var price = CalculatePrice(pricingInfo.Price, chargingInfo);

            var accountingMessage = AccountancyRelay.Create(price, @event.HouseId, pricingInfo, chargingInfo);
            _eventBus.Publish(accountingMessage);
        }

        private double CalculatePrice(double basePrice, ChargingInfo chargingInfo)
        {
            var price = basePrice * chargingInfo.CurrentTaxRate;
            foreach (var charge in chargingInfo.Charges)
            {
                price += charge;
            }

            return price;
        }
    }
}
