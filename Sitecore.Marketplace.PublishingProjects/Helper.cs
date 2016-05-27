using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Marketplace.PublishingProjects.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Marketplace.PublishingProjects
{
    public static class Helper
    {
        /// <summary>
        /// Retreives the item versions that are associated to a publishing project.
        /// </summary>
        /// <param name="item">Project Item ID</param>
        /// <returns>A list of item versions in the specifiec project.</returns>
        public static List<Item> SearchForItems(ID ProjectId)
        {
            List<Item> results = new List<Item>();
            string indexname = "sitecore_master_index";
            using (var context = ContentSearchManager.GetIndex(indexname).CreateSearchContext())
            {
                var query = context.GetQueryable<ProjectItem>().Where(item => item.ProjectId == ProjectId).GetResults();

                foreach (var hit in query.Hits)
                    results.Add(hit.Document.GetItem());

                return results;
            }
        }

        /// <summary>
        /// Extension method to determine if an Item is linked to a project
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>true if Item is in a project</returns>
        public static bool IsProjectItem(this Item item)
        {
            if (!string.IsNullOrEmpty(item.Fields[Data.ProjectFieldId].Value))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Extension method to determine if an Item can be linked to a project
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>true if Item can be in a project</returns>
        public static bool IsItemProjectable(this Item item)
        {
            //CE
            if (item.Fields.Contains(Data.ProjectFieldId)) return true;

            //EE
            return (item.Fields[Data.ProjectFieldId].Name != String.Empty);
        }

        /// <summary>
        /// Gets the project title from the project definition
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>Project title</returns>
        public static string ProjectTitle(this Item item)
        {
            return item.Database.GetItem(new ID(item.Fields[Data.ProjectFieldId].Value)).DisplayName;
        }
    }
}