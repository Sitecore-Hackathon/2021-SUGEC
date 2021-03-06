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

        //public override void Execute(CommandContext context)
        //{
        //    string renderingId = context.Parameters["renderingId"];
        //    NameValueCollection parameters = new NameValueCollection();
        //    if (!string.IsNullOrEmpty(renderingId))
        //    {
        //        parameters["renderingItemId"] = renderingId;
        //        Context.ClientPage.Start((object)this, "Run", parameters);
        //    }
        //}

        //protected virtual void Run(ClientPipelineArgs args)
        //{
        //    Assert.ArgumentNotNull((object)args, nameof(args));
        //    if (!SheerResponse.CheckModified())
        //        return;
        //    Assert.IsNotNull((object)args, nameof(args));
        //    string parameter = args.Parameters["renderingItemId"];
        //    Assert.IsNotNullOrEmpty(parameter, "renderingItemId");
        //    string formValue = WebUtil.GetFormValue("scLanguage");
        //    Assert.IsNotNullOrEmpty(formValue, "lang");
        //    Item obj = Client.ContentDatabase.GetItem(parameter, Language.Parse(formValue));
        //    if (obj == null)
        //    {
        //        SheerResponse.Alert("Item not found.");
        //        return;
        //    }

        //    if (args.IsPostBack)
        //    {
        //        if (!args.HasResult)
        //            return;
        //    }
        //    else
        //    {
        //        SheerResponse.ShowModalDialog(this.GetOptions(args).ToUrlString().ToString(), "740", "600", obj.Name + " Help", true);
        //        args.WaitForPostBack(true);
        //    }
        //}

        //protected virtual HelpDialogOptions GetOptions(ClientPipelineArgs args)
        //{
        //    Assert.ArgumentNotNull((object)args, nameof(args));
        //    string parameter = args.Parameters["renderingItemId"];
        //    Language language1 = WebEditUtil.GetClientContentLanguage();
        //    if ((object)language1 == null)
        //        language1 = Context.Language;
        //    Language language2 = language1;
        //    Item obj = Client.ContentDatabase.GetItem(parameter, language2);
        //    Assert.IsNotNull((object)obj, "item");
        //    return new HelpDialogOptions(obj);
        //}
    }
}