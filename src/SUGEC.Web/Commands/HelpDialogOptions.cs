using System;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;

namespace SUGEC.Web.Commands
{
    public class HelpDialogOptions
    {
        //public Item Item { get; protected set; }

        //public ID RenderingId { get; set; }

        //public HelpDialogOptions(Item item)
        //{
        //    Assert.ArgumentNotNull((object) item, nameof(item));
        //    this.Item = item;
        //    this.RenderingId = item.ID;
        //}

        //public virtual UrlString ToUrlString()
        //{
        //    Assert.IsNotNull((object) Context.Site, "context site");
        //    UrlString urlString = new UrlString(Context.Site.XmlControlPage);
        //    urlString["xmlcontrol"] = this.GetXmlControl();
        //    this.Item.Uri.AddToUrlString(urlString);
        //    if (!ID.IsNullOrEmpty(this.RenderingId))
        //        urlString["renderingId"] = this.RenderingId.ToString();
        //    return Assert.ResultNotNull<UrlString>(urlString);
        //}

        ///// <summary>The get xml control.</summary>
        ///// <returns>The get xml control.</returns>
        //protected virtual string GetXmlControl()
        //{
        //    return "Sitecore.Shell.Applications.Dialogs.HelpDialog";
        //}

        //public static HelpDialogOptions Parse()
        //{
        //    ItemUri queryString1 = ItemUri.ParseQueryString();
        //    Assert.IsNotNull((object) queryString1, "itemUri is null");

        //    Item obj = Database.GetItem(queryString1);
        //    Assert.IsNotNull((object) obj, "Item \"{0}\" not found", (object) queryString1);
        //    HelpDialogOptions helpDialogOptions = new HelpDialogOptions(obj);
        //    return helpDialogOptions;

        //}
    }
}