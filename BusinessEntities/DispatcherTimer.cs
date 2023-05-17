using System;

namespace BusinessEntities
{
    internal class DispatcherTimer
    {
        private TimeSpan timeSpan;
        private object normal;
        private Action p;
        private object dispatcher;

        public DispatcherTimer(TimeSpan timeSpan, object normal, Action p, object dispatcher)
        {
            this.timeSpan = timeSpan;
            this.normal = normal;
            this.p = p;
            this.dispatcher = dispatcher;
        }
    }
}