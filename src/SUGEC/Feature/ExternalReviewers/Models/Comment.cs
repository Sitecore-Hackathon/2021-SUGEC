using System;

namespace ExternalReviewers.Models
{
    public class Comment
    {
        public string UserName { get; set; }
        public string Date { get; set; }
        public string Body { get; set; }
        public Location Location { get; set; }
    }

    public class Location
    {
        public string Left { get; set; }
        public string Top { get; set; }
    }
}
