using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using System;

namespace Sitecore.Marketplace.PublishingProjects.UI.Models
{
    public class ProjectItem : SearchResultItem
    {
        public string Title { get; set; }

		public string ProjectSM
		{ get {
				try { return this.Fields["project"].ToString(); }
				catch (Exception e) { return String.Empty; } }
			set { return; }
		}

		/*[IndexField("project_sm")]*/
        public ID ProjectId
		{
			get
			{
				try
				{
					Sitecore.Data.ID id = new ID(this.Fields["project"].ToString());
					return id;
				}
				catch (Exception) { return null; }
			}
			set { return; }
		}

		[IndexField("project")]
		public string ProjectStr { get; set; }
	}
}