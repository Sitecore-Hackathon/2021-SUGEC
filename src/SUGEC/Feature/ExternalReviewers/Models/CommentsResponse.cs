using System;

namespace ExternalReviewers.Models
{
    public class CommentsResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Date { get; set; }
        public string Body { get; set; }
        public string Location { get; set; }
    }
}
