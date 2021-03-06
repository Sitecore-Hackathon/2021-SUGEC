using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Configuration;
using Microsoft.Owin;
using Owin;
using Sitecore.Diagnostics;

[assembly: OwinStartup(typeof(SignalR.Example.Startup))]
namespace SignalR.Example
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            Log.Info("Initializing log SignalR service...", this);
            app.MapSignalR("/sitecore/signalr", new HubConfiguration() { });
        }
    }
}