//using ExternalReviewers;
//using Microsoft.AspNet.SignalR;
//using Microsoft.Owin;
//using Owin;
//using Sitecore.Diagnostics;

//[assembly: OwinStartup(typeof(Startup))]
//namespace ExternalReviewers
//{
//    public class Startup
//    {
//        public void Configuration(IAppBuilder app)
//        {
//            // Any connection or hub wire up and configuration should go here
//            Log.Info("Initializing log SignalR service...", this);
//            app.MapSignalR("/sitecore/signalr", new HubConfiguration() { });
//        }
//    }
//}