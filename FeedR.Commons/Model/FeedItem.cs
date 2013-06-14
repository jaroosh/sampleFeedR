using System;

namespace FeedR.Commons.Model
{
    /// <summary>
    /// Single feed item.
    /// </summary>
    public class FeedItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Topic { get; set; }
        public string Source { get; set; }
    }
}
