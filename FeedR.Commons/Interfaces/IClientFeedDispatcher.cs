namespace FeedR.Commons.Interfaces
{
    /// <summary>
    /// Base dispatcher for items to clients subscribed for a topic.
    /// </summary>
    public interface IClientFeedDispatcher<in T>
    {
        /// <summary>
        /// Broadcast an item to all the clients.
        /// </summary>
        /// <param name="topic"> </param>
        /// <param name="item"></param>
        void BroadcastAll(string topic, T item);
    }
}
