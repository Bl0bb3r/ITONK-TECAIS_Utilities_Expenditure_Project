using ElectricityExpenditure.Models;
using ElectricityExpenditure.Models.Events;
using ElectricityExpenditure.Services;
using log4net;
using RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityExpenditure.Handlers
{
    public class MeasurementReceivedEventHandler : IEventHandler<Measurement>
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
            await Task.WhenAll(chargingInfoTask, pricingInfoTask).ConfigureAwait(false);

            var chargingInfo = await chargingInfoTask.ConfigureAwait(false);
            var pricingInfo = await pricingInfoTask.ConfigureAwait(false);
            var price = CalculatePrice(pricingInfo.Price, chargingInfo);

            var accountingMessage = AccountancyRelay.Create(price, @event.HouseId, pricingInfo, chargingInfo);
            _log.Debug($"Event had house id: {@event.HouseId}, value: {@event.Value}, timestamp: {@event.Timestamp}, deviceId: {@event.DeviceId}");
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
