using System;
using FeedR.Commons.Interfaces;
using FeedR.Commons.Model;

namespace FeedR.Commons.Messaging
{
    /// <summary>
    /// Handles dispatching of feed items for stream. Represents an opposite sink than FeedSource.
    /// </summary>
    public class FeedDispatcher : IObserver<FeedItem>
    {
        #region Members.

        private IDisposable _feedUnsubscriber;
        private readonly IClientFeedDispatcher<FeedItem> _clientDispatcher;

        #endregion

        #region IObserver.

        public FeedDispatcher(IClientFeedDispatcher<FeedItem> clientDispatcher)
        {
            if (clientDispatcher == null)
                throw new ArgumentNullException("clientDispatcher");

            _clientDispatcher = clientDispatcher;
        }

        /// <summary>
        /// Subscribes to feed source.
        /// </summary>
        /// <param name="feedProvider"></param>
        public virtual void Subscribe(IFeedSource feedProvider)
        {
            _feedUnsubscriber = feedProvider.Subscribe(this);
        }

        /// <summary>
        /// Unsubscribes from the feed ticker.
        /// </summary>
        public virtual void Unsubscribe()
        {
            _feedUnsubscriber.Dispose();
        }

        public void OnNext(FeedItem value)
        {
            // Broadcast to all the clients.
            _clientDispatcher.BroadcastAll(value.Topic, value);
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
