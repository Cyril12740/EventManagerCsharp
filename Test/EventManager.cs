using System;
using System.Collections.Generic;


namespace Test
{
    class EventManager
    {
        public Dictionary<Type, ISubscrition> Registractions = new Dictionary<Type, ISubscrition>();

        public ISubscription<TEventArgs> Of<TEventArgs>() where TEventArgs : EventArgs
        {
            if(Registractions.ContainsKey(typeof(TEventArgs)))
            {
                return Registractions[typeof(TEventArgs)] as ISubscription<TEventArgs>;
            }
            else
            {
                ISubscription<TEventArgs> subscrition = new Subscrition<TEventArgs>();
                Registractions[typeof(TEventArgs)] = subscrition;
                return subscrition;
            }
        }

        public void RaiseEvent(EventArgs args)
        {
            Registractions[args.GetType()].RaiseEvent(args);
        }

        public void RaiseEvent<TEventArgs>(TEventArgs args) where TEventArgs : EventArgs
        {
            Of<TEventArgs>().RaiseEvent(args);
        }
    }
}
