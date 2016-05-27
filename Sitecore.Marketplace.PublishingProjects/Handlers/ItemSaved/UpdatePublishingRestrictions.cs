using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Events;
using System;
using System.Collections.Generic;

namespace Sitecore.Marketplace.PublishingProjects.Handlers.ItemSaved
{
    public class UpdatePublishingRestrictions
    {
        private static readonly SynchronizedCollection<Sitecore.Data.ID> inProcess = new SynchronizedCollection<Sitecore.Data.ID>();

        protected void OnItemSaved(object sender, EventArgs args)
        {
            //ensures arguments aren't null
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");

            //gets item parameter from event arguments
            object obj = Event.ExtractParameter(args, 0);
            Item item = obj as Item;

            // if it is a project definition item, we need to update all of the items in the project.
            if (item.TemplateID == Data.ProjectDetails)
            {
                Database db = Sitecore.Context.ContentDatabase;
                if (db != null && db.Name == "master")
                {
                    foreach (Item i in Helper.SearchForItems(item.ID))
                    {
                        UpdateRestrictionsOnVersion(i);
                    }
                }
            }

            // if it is an item in a project, update the publishing restrictions to match the project release date
            if (item.IsProjectItem())
            {
                UpdateRestrictionsOnVersion(item);
            }
        }

        private static void UpdateRestrictionsOnVersion(Item item)
        {
            if (inProcess.Contains(item.ID))
            {
                return;
            }

            inProcess.Add(item.ID);
            try
            {
                Database db = Sitecore.Context.ContentDatabase;
                if (db != null && db.Name == "master")
                {
                    Item project = db.GetItem(item[Data.ProjectFieldId]);
                    if (project != null)
                    {
                        if (!item.Editing.IsEditing) item.Editing.BeginEdit();
                        item.Publishing.ValidFrom = ((DateField)project.Fields[Data.ProjectDetailsReleaseDate]).DateTime;
                        item.Editing.EndEdit();
                    }
                }
            }
            finally
            {
                inProcess.Remove(item.ID);
            }
        }
    }
}