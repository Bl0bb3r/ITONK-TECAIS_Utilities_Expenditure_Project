using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMq
{
    public abstract class EventBase
    {
        public string EventType { get; }

        public EventBase(string eventType)
        {
            EventType = eventType;
        }
    }
}
