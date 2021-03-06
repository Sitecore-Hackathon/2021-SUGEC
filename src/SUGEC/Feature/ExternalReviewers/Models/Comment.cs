using System;

namespace ExternalReviewers.Models
{
    public class Comment
    {
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public string Location { get; set; }
    }
}
