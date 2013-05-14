using System;
using System.Collections;
using System.Collections.Generic;

namespace Sapphire.EventDispatchers
{
    public class CollectingEventDispatcher : IEventDispatcher, IEnumerable<IEvent>
    {
        [ThreadStatic] private static Queue<IEvent> _occurredEvents;

        private static Queue<IEvent> OccurredEvents
        {
            get { return _occurredEvents ?? (_occurredEvents = new Queue<IEvent>()); }
        }

        public IEnumerator<IEvent> GetEnumerator()
        {
            return OccurredEvents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispatch<T>(T @event) where T : IEvent
        {
            OccurredEvents.Enqueue(@event);
        }

        public void Clear()
        {
            OccurredEvents.Clear();
        }
    }
}