using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace ExternalReviewers.Hubs
{
    public class ExternalReviewersHub : Hub
    {
        public async Task CommentsHub(string user, string body, string date)
        {
            await Clients.All.SendAsync("ReceiveComment", user, body, date);
        }
    }
}