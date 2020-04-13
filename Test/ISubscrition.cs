using System;


namespace Test
{
    interface ISubscrition
    {
        Type RelativeType { get; }

        void RaiseEvent(EventArgs args);
    }
}
