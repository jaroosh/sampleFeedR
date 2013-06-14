using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FeedR.Commons.Information;
using FeedR.Commons.Interfaces;
using FeedR.Commons.Model;
using FeedR.Commons.Scheduling;
using FeedR.Commons.Utilities;

namespace FeedR.Commons.Feed
{
    /// <summary>
    /// Represents a SOURCE of feed stream.
    /// </summary>
    /// <typeparam name="TFeedGator">Class responsible for actualy GETTING the feed from some stream.</typeparam>
    public class FeedSource<TFeedGator> : PeriodicRunner, IFeedSource
        where TFeedGator : FeedGator, new()
    {
        #region Declarations.

        /// <summary>
        /// Used to unsubscribe.
        /// </summary>
        public class FeedUnsubscriber : IDisposable
        {
            private readonly List<IObserver<FeedItem>> _observers;
            private readonly IObserver<FeedItem> _observer;

            public FeedUnsubscriber(List<IObserver<FeedItem>> observers, IObserver<FeedItem> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        #endregion

        #region Members.

        private bool _isStopping;
        private readonly BlockingCollection<FeedItem> _feeds;
        private readonly Dictionary<string, CancellationTokenSource> _topics;
        private static readonly object _locker = new object();
        private readonly List<IObserver<FeedItem>> _observers;

        #endregion

        public FeedSource()
        {
            _observers = new List<IObserver<FeedItem>>();
            _topics = new Dictionary<string, CancellationTokenSource>();
            _feeds = new BlockingCollection<FeedItem>();

        }

        /// <summary>
        /// Start the service.
        /// </summary>
        public void Start()
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var item in _feeds.GetConsumingEnumerable())
                {
                    // Send a new feed item from the blocking queue to the observers.
                    _observers.ForEach((o) => o.OnNext(item));
                }
            });
        }

        /// <summary>
        /// Stop the ticker.
        /// </summary>
        public void Stop()
        {
            // Kill the topic subscribers for this source.
            foreach (var cts in _topics.Values)
                cts.Cancel();

            // Stop the loop.
            _feeds.CompleteAdding();
        }

        /// <summary>
        /// Subscribe to receive feed items from this ticker.
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<FeedItem> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new FeedUnsubscriber(_observers, observer);
        }


        /// <summary>
        /// Subscribe client for topic.
        /// </summary>
        /// <param name="topic"></param>
        public void SubscribeForTopic(string topic)
        {
            lock (_locker)
            {
                if (!_topics.ContainsKey(topic))
                {
                    var gator = new TFeedGator();
                    gator.Initialize(_feeds, topic);

                    // Start collecting.
                    _topics[topic] = StartTask(gator.Collect, Registry.GetValue<int>(Registry.Keys.DefaultGatorIntervalKey));
                }
            }
        }
    }

}
