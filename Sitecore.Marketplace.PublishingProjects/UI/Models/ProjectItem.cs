using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;

namespace Sitecore.Marketplace.PublishingProjects.UI.Models
{
    public class ProjectItem : SearchResultItem
    {
        public string Title { get; set; }

        [IndexField("project")]
        public ID ProjectId { get; set; }
    }
}