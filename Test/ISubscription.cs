using System;


namespace Test
{
    interface ISubscription<TEventArgs> : ISubscrition where TEventArgs : EventArgs
    {
        event EventHandler<TEventArgs> Subscriptions;

        void RaiseEvent(TEventArgs args);
    }
}
