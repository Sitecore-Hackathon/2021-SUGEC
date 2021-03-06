using ExternalReviewers.Models;
using Sitecore;
using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Workflows.Simple;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using Microsoft.AspNet.SignalR;
using Sitecore.IO;
using Sitecore.Security.Accounts;
using Sitecore.Workflows;

namespace ExternalReviewers.Controllers
{
    
    public class CommentsController : Controller
    {
        //private readonly IHubContext
        [HttpGet]
        public ActionResult Index(string itemName)
        {
            var response = new List<CommentsResponse>();

            var urlReferrer = Request.UrlReferrer?.PathAndQuery;
            var master = Factory.GetDatabase("master");
            var rootItem = Sitecore.Context.Database.GetItem(FileUtil.MakePath("/sitecore/system/External Reviews", urlReferrer, '/'));
          
            if (rootItem == null) return Json(response, JsonRequestBehavior.AllowGet);

            var referencedItemId = rootItem["linked item id"];
            var currentItem = master.GetItem(referencedItemId);
            
            if (master.WorkflowProvider?.GetWorkflow(currentItem) == null)
            {
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            var workflowHistory = master.WorkflowProvider?.GetWorkflow(currentItem).GetHistory(currentItem).OrderBy(x=>x.Date);


            foreach (var workflowEvent in workflowHistory)
            {
                response.Add(new CommentsResponse
                {
                    Body = workflowEvent.CommentFields["Comments"],
                    Location = workflowEvent.CommentFields["Location"],
                    Date = workflowEvent.Date.ToString("g"),
                    UserName = workflowEvent.User,
                    Id = currentItem.ID.ToShortID().ToString()
                });
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Index(Comment model)
        {
            var urlReferrer = Request.UrlReferrer?.PathAndQuery;
            var master = Factory.GetDatabase("master");
            var rootItem = Sitecore.Context.Database.GetItem(FileUtil.MakePath("/sitecore/system/External Reviews", urlReferrer, '/'));

            if (rootItem == null) return Json(model, JsonRequestBehavior.AllowGet);

            var referencedItemId = rootItem["linked item id"];
            var currentItem = master.GetItem(referencedItemId);

            if (master.WorkflowProvider?.GetWorkflow(currentItem) == null)
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }

            if (currentItem.Database.WorkflowProvider is WorkflowProvider workflowProvider)
            {
                string workflowState = GetWorkflowState(currentItem);

                var virtualUser = Sitecore.Security.Authentication.AuthenticationManager.BuildVirtualUser($"ExternalReviewer\\{model.UserName}", true);

                using (new UserSwitcher(virtualUser))
                {
                    workflowProvider.HistoryStore.AddHistory(currentItem, workflowState, workflowState,
                        new StringDictionary
                        {
                            {"Comments", model.Body},
                            {"Location", model.Location},
                        });
                }

            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }


        #region Infrastructure

        private static string GetWorkflowState(Item item)
        {
            var info = item.Database.DataManager.GetWorkflowInfo(item);
            return info != null ? info.StateID : string.Empty;
        }

        #endregion
    }
}
