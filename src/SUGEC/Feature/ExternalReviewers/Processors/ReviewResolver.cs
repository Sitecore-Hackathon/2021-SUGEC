using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.IO;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.SecurityModel;
using System.Linq;

namespace ExternalReviewers.Processors
{
    /// <summary>
    /// Resolves reviews.
    /// </summary>
    public class ReviewResolver : HttpRequestProcessor
    {
        /// <summary>
        /// Runs the processor.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public override void Process(HttpRequestArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            
            var database = Context.Database;
            if (database == null)
            {
                Tracer.Warning("There is no context database in ReviewResolver.");
                return;
            }
            this.StartProfilingOperation("Resolve review.", args);

            if (ExistsReview(args.LocalPath))
            {
                Context.Item = ProcessItem(args);
            }

            this.EndProfilingOperation(string.Empty, args);
        }

        /// <summary>
        /// Checks if a review exits in system.
        /// </summary>
        /// <param name="review">The trailing part of the URL.</param>
        /// <returns>True if the review item exist.</returns>
        private bool ExistsReview(string review)
        {
            var item = ItemManager.GetItem(FileUtil.MakePath("/sitecore/system/reviews", review, '/'), Language.Invariant, Version.First, Context.Database, SecurityCheck.Disable);
            return item != null;
        }
        
        /// <summary>
        /// Processes the item.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The item.</returns>
        private Item ProcessItem(HttpRequestArgs args)
        {
            var item = ItemManager.GetItem(FileUtil.MakePath("/sitecore/system/reviews", args.LocalPath, '/'), Language.Invariant, Version.First, Context.Database, SecurityCheck.Disable);
            if (item != null && item.HasChildren)
            {
                var pageItem = item.Children.First();
                //this.TraceInfo(string.Concat(new object[] { "Reviewing for \"", args.LocalPath, "\" which points to \"", target.ID, "\"" }));
                
                return pageItem;
            }
            return null;
        }
    }
}
