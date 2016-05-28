using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Services.Core;
using System.Web;
using Sitecore.Marketplace.PublishingProjects.Models;
using Sitecore.Data.Managers;
using Sitecore.Services.Infrastructure.Sitecore.Services;

namespace Sitecore.Marketplace.PublishingProjects.Controllers
{
    [ServicesController]
    public class ProjectItemController : EntityService<ProjectItem>
    {
        public ProjectItemController(IRepository<ProjectItem> repository)
             : base(repository) 
         {
        }

        public List<ProjectItem> GetComponents(string id, string database, string language)
         { 
             //Contract.Assert(!string.IsNullOrEmpty(id), "id is required"); 
              var db = Sitecore.Data.Database.GetDatabase(database); 
  
             //Contract.Assume(db != null, "Invalid database"); 
              var languageItem = LanguageManager.GetLanguage(language, db); 
  
             //Contract.Assume(languageItem != null, "Invalid language"); 
              var item = db.GetItem(id, languageItem); 
  
             //Contract.Assume(item != null, "Invalid ID, item doesn't exist"); 
              if (item.Fields[Sitecore.FieldIDs.LayoutField] == null || string.IsNullOrEmpty(item.Fields[Sitecore.FieldIDs.LayoutField].Value)) 
                 return null;

            return null; // GetComponents(item).ToList(); 
         }
    }
}