﻿using Martiscoin.EventBus;
using Microsoft.Extensions.Logging;

namespace Martiscoin.Signals
{
    public interface ISignals : IEventBus
    {
    }

    public class Signals : InMemoryEventBus, ISignals
    {
        public Signals(ILoggerFactory loggerFactory, ISubscriptionErrorHandler subscriptionErrorHandler) : base(loggerFactory, subscriptionErrorHandler) { }
    }
}
