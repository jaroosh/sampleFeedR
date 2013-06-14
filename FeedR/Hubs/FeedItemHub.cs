using FeedR.Commons.Interfaces;
using FeedR.Commons.Model;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace FeedR.Hubs
{
    /// <summary>
    /// Actual signalR item hub.
    /// </summary>
    [HubName("feedDispatcher")]
    public class FeedItemHub : Hub, IClientFeedDispatcher<FeedItem>
    {
        /// <summary>
        /// Will add a client with given connection to a group.
        /// </summary>
        /// <param name="group"></param>
        public void AddToGroup(string group)
        {
            Groups.Add(Context.ConnectionId, group);
        }

        /// <summary>
        /// Will broadcast to all client interested in a topic.
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="item"></param>
        public void BroadcastAll(string topic, FeedItem item)
        {
            GetClients(topic).addFeedItem(item);
        }

        /// <summary>
        /// Gets all the clients.
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        private static dynamic GetClients(string topic)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<FeedItemHub>();
            return context.Clients.Group(topic);
        }
    }
}