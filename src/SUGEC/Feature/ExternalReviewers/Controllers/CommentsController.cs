using ExternalReviewers.Models;
using Newtonsoft.Json;
using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.IO;
using Sitecore.Security.Accounts;
using Sitecore.Services.Infrastructure.Web.Http;
using Sitecore.Workflows.Simple;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;

namespace ExternalReviewers.Controllers
{

    public class CommentsController : ServicesApiController
    {
        [System.Web.Mvc.HttpGet]
        public IHttpActionResult Index()
        {
            var response = new List<CommentsResponse>();
            var jsonSettings = new JsonSerializerSettings();

            var urlReferrer = Request.RequestUri;
            var master = Factory.GetDatabase("master");
            var rootItem = Sitecore.Context.Database.GetItem(FileUtil.MakePath("/sitecore/system/External Reviews", urlReferrer.PathAndQuery, '/'));

            if (rootItem == null)
            {
                return new JsonResult<List<CommentsResponse>>(response, jsonSettings, Encoding.UTF8, this);
            }

            var referencedItemId = rootItem["linked item id"];
            var currentItem = master.GetItem(referencedItemId);
            
            if (master.WorkflowProvider?.GetWorkflow(currentItem) == null)
            {
                return new JsonResult<List<CommentsResponse>>(response, jsonSettings, Encoding.UTF8, this);
            }

            var workflowHistory = master.WorkflowProvider?.GetWorkflow(currentItem).GetHistory(currentItem);
            
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

            response = response.OrderBy(x => x.Date).ToList();

            return new JsonResult<List<CommentsResponse>>(response, jsonSettings, Encoding.UTF8, this);
        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult Index(Comment model)
        {
            var urlReferrer = Request.RequestUri;
            var master = Factory.GetDatabase("master");
            var rootItem = Sitecore.Context.Database.GetItem(FileUtil.MakePath("/sitecore/system/External Reviews", urlReferrer.PathAndQuery, '/'));

            var jsonSettings = new JsonSerializerSettings();

            if (rootItem == null)
            {
                return new JsonResult<Comment>(model, jsonSettings, Encoding.UTF8, this);
            }

            var referencedItemId = rootItem["linked item id"];
            var currentItem = master.GetItem(referencedItemId);

            if (master.WorkflowProvider?.GetWorkflow(currentItem) == null)
            {
                return new JsonResult<Comment>(model, jsonSettings, Encoding.UTF8, this);
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

            return new JsonResult<Comment>(model, jsonSettings, Encoding.UTF8, this);
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
