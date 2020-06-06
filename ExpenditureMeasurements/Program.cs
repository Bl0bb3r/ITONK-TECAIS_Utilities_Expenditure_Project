using System;
using System.Collections.Generic;
using System.Threading;

namespace ExpenditureMeasurements
{
    class Program
    {
        static void Main(string[] args)
        {
            var rabbitMqClient = new RabbitMqClient("Measurement");
            var households = new List<Household>
            {
                new Household(1, new List<MeasurementDevice>
                {
                    new MeasurementDevice(Guid.NewGuid(), 1, MeasurementCategory.Water, "ABCDE123", "Grundfos", 111),
                    new MeasurementDevice(Guid.NewGuid(), 1, MeasurementCategory.Heat, "ABCDE123", "Grundfos", 222),
                    new MeasurementDevice(Guid.NewGuid(), 1, MeasurementCategory.Electricity, "ABCDE123", "Grundfos",
                        333),

                }),
                new Household(2, new List<MeasurementDevice>
                {
                    new MeasurementDevice(Guid.NewGuid(), 2, MeasurementCategory.Water, "FGHIJ456", "Kamstrup", 111),
                    new MeasurementDevice(Guid.NewGuid(), 2, MeasurementCategory.Heat, "FGHIJ456", "Kamstrup", 222),
                    new MeasurementDevice(Guid.NewGuid(), 2, MeasurementCategory.Electricity, "FGHIJ456", "Kamstrup",
                        333),

                }),
                new Household(3, new List<MeasurementDevice>
                {
                    new MeasurementDevice(Guid.NewGuid(), 3, MeasurementCategory.Heat, "UVXYZ789", "Kamstrup", 222),
                    new MeasurementDevice(Guid.NewGuid(), 3, MeasurementCategory.Electricity, "UVXYZ789", "Kamstrup",
                        333),

                })
            };

            while (true)
            {
                MeasurementReport(households, rabbitMqClient);
                StatusReport(households, rabbitMqClient);
                Thread.Sleep(8000);
            }

            static void MeasurementReport(IEnumerable<Household> households, RabbitMqClient rabbitMqClient)
            {
                foreach (var house in households)
                {
                    foreach (var measurementDevice in house.Devices)
                    {
                        var newMeasurement = measurementDevice.GenerateAMeasurement();
                        rabbitMqClient.SendMessage(message: newMeasurement, routingKey: newMeasurement.ExpenditureType.ToString().ToLower());
                    }
                }
            }

            static void StatusReport(IEnumerable<Household> households, RabbitMqClient rabbitMqClient)
            {
                foreach (var house in households)
                {
                    foreach (var measurementDevice in house.Devices)
                    {
                        var newStatus = measurementDevice.GenerateAStatusReport(house.ID);
                        rabbitMqClient.SendMessage(message: newStatus, routingKey: "status");
                    }
                }
            }
        }
    }
}
