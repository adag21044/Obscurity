using System.Collections.Generic;

public static class EventManager
{
    private static readonly Dictionary<string, IEvent> events = new Dictionary<string, IEvent>();

    public static IEvent GetEvent(string eventName)
    {
        if (!events.TryGetValue(eventName, out var existingEvent))
        {
            existingEvent = new Event();
            events.Add(eventName, existingEvent);
        }
        return existingEvent;
    }
}
