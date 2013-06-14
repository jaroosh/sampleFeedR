using System;
using System.Collections.Concurrent;
using FeedR.Commons.Model;

namespace FeedR.Commons.Information
{
    /// <summary>
    /// Gathers information from the specific topic.
    /// </summary>
    public abstract class FeedGator
    {
        #region Memebers.

        private string _topic;
        private BlockingCollection<FeedItem> _feedCollection;

        #endregion

        public string Topic { get { return _topic; } }

        public void Initialize(BlockingCollection<FeedItem> feedCollection, string topic)
        {
            _topic = topic;
            _feedCollection = feedCollection;
        }

        /// <summary>
        /// Publishes a single feed item to the source sink.
        /// </summary>
        /// <param name="item"></param>
        protected void PublishItem(FeedItem item)
        {
            _feedCollection.Add(item);            
        }

        /// <summary>
        /// Starts gathering information.
        /// </summary>
        public abstract void Collect();
    }
}
