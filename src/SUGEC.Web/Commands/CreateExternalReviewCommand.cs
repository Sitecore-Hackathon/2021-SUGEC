using System;
using System.Collections.Specialized;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Sites;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;

namespace SUGEC.Web.Commands
{
    public class CreateExternalReviewCommand : Sitecore.Shell.Framework.Commands.Command
    {
        public override void Execute(CommandContext context)
        {
            Sitecore.Context.ClientPage.Start(this, "Run", context.Parameters);
        }

        protected static void Run(ClientPipelineArgs args)
        {
            if (!args.IsPostBack)
            {
                UrlString urlString = new UrlString(UIUtil.GetUri("control:ExternalReview"));
                SheerResponse.ShowModalDialog(urlString.ToString(), "800", "300", "", true);
                args.WaitForPostBack();
            }
            else
            {
                if (args.HasResult)
                {
                    if (Sitecore.Context.Item.Name == "Content Editor")
                    {
                        Sitecore.Context.ClientPage.ClientResponse.SetLocation(Sitecore.Links.LinkManager.GetItemUrl(Sitecore.Context.Item));
                    }
                }
            }
        }

        public override CommandState QueryState(CommandContext context)
        {
            Error.AssertObject((object)context, "context");
            
            return context.Items[0].Paths.FullPath.ToLower().StartsWith(SiteContext.Current.StartPath.ToLower()) ? CommandState.Enabled : CommandState.Disabled;
        }

    }
}