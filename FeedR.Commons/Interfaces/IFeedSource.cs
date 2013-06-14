using System;
using FeedR.Commons.Model;

namespace FeedR.Commons.Interfaces
{
    /// <summary>
    /// Represents a single abstract source of information, that can be subscribed to.
    /// </summary>
    public interface IFeedSource : IObservable<FeedItem>
    {
        /// <summary>
        /// Starts listening on the source.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the source.
        /// </summary>
        void Stop();

        /// <summary>
        /// Subscribes client to a topic on a source.
        /// </summary>
        /// <param name="topic"></param>
        void SubscribeForTopic(string topic);
    }
}
