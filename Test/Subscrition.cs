using System;


namespace Test
{
    public class Subscrition<TEventArgs> : ISubscription<TEventArgs> where TEventArgs : EventArgs
    {
        public Type RelativeType => typeof(TEventArgs);

        public event EventHandler<TEventArgs> Subscriptions;

        public void RaiseEvent(TEventArgs args)
        {
            Subscriptions?.Invoke(this, args);
        }

        public void RaiseEvent(EventArgs args)
        {
            if (args is TEventArgs args1)
            {
                RaiseEvent(args1);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
