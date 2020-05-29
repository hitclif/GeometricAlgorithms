namespace SweepLine
{
    public class IntersectionsResult
    {
        public int TotalEvents { get; private set; }
        public int Intersections { get; private set; }

        public int EventType3 { get; private set; } = -1;
        public int EventType17 { get; private set; } = -1;
        public int EventType99 { get; private set; } = -1;

        public void Handle(Event @event)
        {
            TotalEvents++;
            if(@event.EventType == EventType.Intersection)
            {
                Intersections++;
            }

            switch (TotalEvents)
            {
                case 3:
                    this.EventType3 = this.EventTypeToInt(@event.EventType);
                    break;
                case 17:
                    this.EventType17 = this.EventTypeToInt(@event.EventType);
                    break;
                case 99:
                    this.EventType99 = this.EventTypeToInt(@event.EventType);
                    break;
            }
        }

        private int EventTypeToInt(EventType eventType)
        {
            switch (eventType)
            {
                case EventType.Begin:
                    return 1;
                case EventType.Intersection:
                    return 0;
                case EventType.End:
                    return 2;
                default:
                    throw new System.Exception("unhandled value");
            }
        }
    }
}
