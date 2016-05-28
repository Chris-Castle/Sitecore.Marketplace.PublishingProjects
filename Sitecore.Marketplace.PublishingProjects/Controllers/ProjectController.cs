using System.Collections.Generic;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Sitecore.Services;
using Sitecore.Marketplace.PublishingProjects.Models;
using Sitecore.Data.Items;
using Sitecore.Data;

namespace Sitecore.Marketplace.PublishingProjects.Controllers
{
    [ServicesController]
    public class ProjectController : EntityService<Project>
    {
        public ProjectController(IRepository<Project> repository)
             : base(repository)
        {
        }

        public List<ProjectItem> GetProjectItems(string id)
        {
            List<ProjectItem> items = new List<ProjectItem>();
            Database master = Database.GetDatabase("master");

            foreach (Item item in Helper.SearchForItems(new Sitecore.Data.ID(id)))
            {
                ProjectItem p = new ProjectItem();
                //p.Icon
                p.Id = item.ID.ToString();
                p.Name = item.Name; //TODO: Use DisplayName???
                p.TemplateName = item.TemplateName;
                p.Version = item.Version.Number;
                p.Language = item.Language.Name;

                Item workflow = master.GetItem(item["__workflow state"]);
                if (workflow != null)
                {
                    p.Workflow = workflow.Name;
                    // Add an is final flag???
                }
                items.Add(p);
            }
            return items;
        }
    }
}