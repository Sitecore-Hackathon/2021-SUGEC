using System;
using System.Collections.Specialized;
using Sitecore;
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
        public string ItemID { get; set; }

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

                var sourceItem = masterDB.GetItem(ItemID);
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

                newItem.Editing.BeginEdit();
                newItem["review link"] = $"/host/{itemName}/{duplicatedItem.DisplayName}";
                newItem.Editing.EndEdit();

                //string renderingId = "{2561F888-1F48-4347-9ADA-7BE6E70443D8}";
                //string placeholder = "main";

                //LayoutField layoutField = new LayoutField(duplicatedItem.Fields[FieldIDs.LayoutField]);
                //LayoutDefinition layoutDefinition = LayoutDefinition.Parse(layoutField.Value);                                      
                //string defaultDeviceId = "{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}";
                //DeviceDefinition deviceDefinition = layoutDefinition.GetDevice(defaultDeviceId);
                //DeviceItem deviceItem = new DeviceItem(masterDB.GetItem(defaultDeviceId));

                //if (deviceDefinition != null && deviceItem != null)
                //{
                //    /// Create a RenderingDefinition and add the reference of sublayout or rendering
                //    RenderingDefinition renderingDefinition = new RenderingDefinition();
                //    renderingDefinition.ItemID = renderingId;
                //    /// Set placeholder where the rendering should be displayed
                //    renderingDefinition.Placeholder = placeholder;
                //    /// Get all added renderings
                //    RenderingReference[] renderings = duplicatedItem.Visualization.GetRenderings(deviceItem, true);
                //    /// Add rendering to end of list
                //    deviceDefinition.AddRendering(renderingDefinition);

                //    /// Save the layout changes
                //    using (new SecurityDisabler())
                //    {
                //        duplicatedItem.Editing.BeginEdit();
                //        layoutField.Value = layoutDefinition.ToXml();
                //        duplicatedItem.Editing.EndEdit();
                //    }



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