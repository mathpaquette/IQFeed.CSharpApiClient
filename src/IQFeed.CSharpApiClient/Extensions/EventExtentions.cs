using System;

namespace IQFeed.CSharpApiClient.Extensions
{
    public static class EventExtensions
    {
        public static void RaiseEvent(this EventHandler @event, object sender, EventArgs e)
        {
            if (@event != null)
                @event(sender, e);
        }

        public static void RaiseEvent<T>(this EventHandler<T> @event, object sender, T e)
            where T : EventArgs
        {
            if (@event != null)
                @event(sender, e);
        }
    }
}