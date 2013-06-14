using System;
using FeedR.Commons.Information;
using FeedR.Commons.Model;

namespace FeedR.Data
{
    /// <summary>
    /// Simple feed gator that will just add one feed item at a time ;)
    /// </summary>
    /// <remarks>
    /// In real application this uses async / await on Bing, Google and various 
    /// </remarks>
    public class SimpleNewsGator : FeedGator
    {
        private static int _counter = 0;

        public override void Collect()
        {
            PublishItem(new FeedItem()
                {
                    Date = DateTime.Now, 
                    Id = ++_counter, 
                    Title = String.Format("{0} Item" , Topic), 
                    Description = "Something", 
                    Topic = this.Topic
                });
        }
    }
}