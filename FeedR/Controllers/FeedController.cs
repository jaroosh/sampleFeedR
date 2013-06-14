using System.Net;
using System.Net.Http;
using System.Web.Http;
using FeedR.Commons.Interfaces;
using FeedR.ViewModels;

namespace FeedR.Controllers
{
    public class FeedController : ApiController
    {
        // In normal application Im using multiple sources but for this demo to make it shorter
        // Ive added a single source which is google :)
        private readonly IFeedSource _source;

        public FeedController(IFeedSource singleSource)
        {
            _source = singleSource;
        }

        [HttpPost]
        public HttpResponseMessage Post(FeedSubscriptionViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.SearchKeyword))
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            // On each post - try to subscribe for a topic.
            // If this topic - seach keyword - is already run by some FeedGator, just add as a new observer to it.
            _source.SubscribeForTopic(viewModel.SearchKeyword);

            // Everything went smooth.
            return Request.CreateResponse(HttpStatusCode.OK, viewModel.SearchKeyword);
        }
    }
}