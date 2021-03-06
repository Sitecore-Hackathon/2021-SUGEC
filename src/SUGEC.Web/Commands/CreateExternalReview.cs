using System;
using System.Collections.Specialized;
using System.Web;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Layouts;
using Sitecore.Links.UrlBuilders;
using Sitecore.Publishing;
using Sitecore.SecurityModel;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Edit = Sitecore.Web.UI.HtmlControls.Edit;

namespace SUGEC.Web.Commands
{
    public class CreateExternalReview : DialogForm
    {
        protected DateTimePicker ExpirationDate;

        private const string ExternalReviewsSystemFolder = "{4BCA3B14-AA4C-4FB3-BC41-2CE2EFCA0FA2}";
        private const string ExternalReviewTemplateId = "{C5E3907C-2ACD-4E57-9BA1-C982BD35D0FF}";
       
        protected override void OnOK(object sender, EventArgs e)
        {
            Assert.ArgumentNotNull((object)e, nameof(e));
            if (!Context.ClientPage.IsEvent)
                return;
            Sitecore.Data.Database masterDB = Sitecore.Configuration.Factory.GetDatabase("master");
            Item parentItem = masterDB.GetItem(ExternalReviewsSystemFolder);
            var template = masterDB.GetTemplate(ExternalReviewTemplateId);
            var itemName = Guid.NewGuid().ToString("N");
            Item newItem = parentItem.Add(itemName, template);
            if (newItem == null)
            {
                SheerResponse.Alert("Link could not be created");
                return;
            }

            using (new Sitecore.SecurityModel.SecurityDisabler())
            {

                var sourceItem = UIUtil.GetItemFromQueryString(Context.ContentDatabase);
                newItem.Editing.BeginEdit();
                newItem["link expiration date"] = ExpirationDate.Value;
                newItem["linked item id"] = sourceItem.ID.ToString();
                newItem.Editing.EndEdit();

                var duplicatedItem = sourceItem.CopyTo(newItem, sourceItem.DisplayName, ID.NewID, false);

                duplicatedItem.Editing.BeginEdit();
                duplicatedItem["__default workflow"] = "";
                duplicatedItem["__workflow"] = "";
                duplicatedItem.Editing.EndEdit();

                var currentVersion = duplicatedItem.Versions.GetLatestVersion();
                foreach (var itemVersion in duplicatedItem.Versions.GetVersions(true))
                {
                    if (!itemVersion.Version.Number.Equals(currentVersion.Version.Number))
                    {
                        itemVersion.Versions.RemoveVersion();
                    }
                }

                var cdsHostName = Settings.GetSetting("Feature.ExternalReviewers.CDsHostName");
                var host = !string.IsNullOrEmpty(cdsHostName)?cdsHostName:string.Empty;
                newItem.Editing.BeginEdit();
                newItem["review link"] = $"/{host}/{itemName}/{duplicatedItem.DisplayName}";
                newItem.Editing.EndEdit();
                
                try
                {
                    Sitecore.Data.Database webDB = Sitecore.Configuration.Factory.GetDatabase("web");
                    PublishOptions po = new PublishOptions(masterDB, webDB, PublishMode.Smart,
                        Sitecore.Context.Item.Language, DateTime.Now)
                    {
                        RootItem = newItem,
                        Deep = true
                    };


                    new Publisher(po).Publish();
                }
                catch (Exception ex)
                {
                    Sitecore.Diagnostics.Log.Error("Exception publishing items from custom pipeline! : " + ex,
                        this);
                }
                SheerResponse.CloseWindow();

                SheerResponse.Alert($"Review Link Created: /{newItem.DisplayName}");

            }

        }
    }
}