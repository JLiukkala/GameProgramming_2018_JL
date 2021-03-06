﻿using System;

namespace TankGame.Messaging
{
    public class Subscription<TMessage> : ISubscription<TMessage>
        where TMessage : IMessage
    {
        public Action<TMessage> Action { get; private set; }

        public IMessageBus MessageBus { get; private set; }

        public Subscription(IMessageBus messageBus, Action<TMessage> action)
        {
            if (messageBus == null)
            {
                throw new ArgumentNullException("messageBus");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            MessageBus = messageBus;
            Action = action;
        }

        protected virtual void Dispose(bool disposing)
        {
            MessageBus.Unsubscribe(this);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Subscription()
        {
            Dispose(false);
        }
    }
}
