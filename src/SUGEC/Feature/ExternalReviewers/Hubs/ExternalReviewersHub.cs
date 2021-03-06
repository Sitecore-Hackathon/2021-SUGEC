using Microsoft.AspNet.SignalR;

namespace ExternalReviewers.Hubs
{
    public class ExternalReviewersHub : Hub
    {
        public async System.Threading.Tasks.Task commentsHub(string user, string body, string date, string location)
        {
            await Clients.All.SendAsync("ReceiveComment", user, body, date, location);
        }
    }
}