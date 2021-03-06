using ExternalReviewers.Models;
using Sitecore;
using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Workflows.Simple;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ExternalReviewers.Controllers
{
    [Route("/api/comments")]
    public class ReviewController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var response = new List<CommentsResponse>();

            var currentItem = Context.Item;
            if (currentItem == null) return Json(response, JsonRequestBehavior.AllowGet);

            var master = Factory.GetDatabase("master");

            var workflowHistory = master.WorkflowProvider.GetWorkflow(currentItem).GetHistory(currentItem);
            foreach (var workflowEvent in workflowHistory)
            {
                response.Add(new CommentsResponse
                {
                    Body = workflowEvent.CommentFields["Comments"],
                    Date = workflowEvent.Date,
                    UserName = workflowEvent.User,
                    Id = currentItem.ID.ToShortID().ToString()
                });
            }

            return Json(response.OrderBy(x => x.Date), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Index(Comment model)
        {
            var currentItem = Context.Item;
            if (currentItem == null) return Json(null, JsonRequestBehavior.AllowGet);

            if (currentItem.Database.WorkflowProvider is WorkflowProvider workflowProvider)
            {
                string workflowState = GetWorkflowState(currentItem);
                workflowProvider.HistoryStore.AddHistory(currentItem, workflowState, workflowState,
                    new StringDictionary
                    {
                        {"Comments", model.Body}
                    });
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
