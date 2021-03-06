using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Diagnostics;
using System;

namespace ExternalReviewers.Agents
{
    /// <summary>
    /// This is an agent that should be scheduled to run at regular intervals to remove expired review items.
    /// Once the expiration date has reached the current date and time, the item will be removed and the
    /// external user won't be able to access to the preview item.
    /// </summary>
    public class ReviewsCleanerAgent
    {
        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>The database.</value>
        public Database Database { get; private set; }

        /// <summary>
        /// Gets the item root.
        /// </summary>
        /// <value>The item root.</value>
        public string Root { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExternalReviewers.Agentss.ReviewsCleanerAgent" /> class.
        /// </summary>
        /// <param name="root">The path to Sitecore 'root' item.</param>
        /// <param name="database">The database to load review items from.</param>
        protected ReviewsCleanerAgent(string root, Database database)
        {
            Assert.ArgumentNotNullOrEmpty(root, "root");
            Assert.ArgumentNotNull(database, "database");
            this.Root = root;
            this.Database = database;
        }


        /// <summary>
        /// This is the method the Scheduler should call when executing this agent.
        /// </summary>
        public void Run()
        {
            var item = this.Database.GetItem(this.Root);
            if (item != null)
            {
                var descendants = item.Axes.GetDescendants();

                foreach (var descendant in descendants)
                {
                    DateField date = descendant.Fields["Date"];
                    if (date != null && date.DateTime <= DateTime.UtcNow)
                    {
                        descendant.Delete();
                    }
                }
            }
        }
    }
}
