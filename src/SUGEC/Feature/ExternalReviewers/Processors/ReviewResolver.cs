﻿using Sitecore;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.IO;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.SecurityModel;
using System;
using System.Linq;
using Version = Sitecore.Data.Version;

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
            if (item != null)
            {
                DateField date = item.Fields["Date"];
                if (date != null && date.DateTime <= DateTime.UtcNow || !item.HasChildren) return null;

                var pageItem = item.Children.First();
                this.TraceInfo(string.Concat(new object[] { "External review \"", args.LocalPath, "\" which points to \"", pageItem.DisplayName, "\"" }));

                return pageItem;
            }

            return null;
        }
    }
}