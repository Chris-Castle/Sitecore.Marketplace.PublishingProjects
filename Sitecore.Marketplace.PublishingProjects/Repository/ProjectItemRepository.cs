using Sitecore.Data;
using Sitecore.Marketplace.PublishingProjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Marketplace.PublishingProjects.Repository
{
    public class ProjectItemRepository : Sitecore.Services.Core.IRepository<ProjectItem> 
     { 
         public void Add(ProjectItem entity)
         { 
             throw new NotImplementedException(); 
         } 
 
         public void Delete(ProjectItem entity)
         { 
             throw new NotImplementedException(); 
         } 
 
         public bool Exists(ProjectItem entity)
         { 
             var item = Sitecore.Data.Database.GetDatabase("master").GetItem(new ID(entity.Id)); 
             return item != null; 
         } 
  
         public ProjectItem FindById(string id)
         { 
            var item = Sitecore.Data.Database.GetDatabase("master").GetItem(new ID(id)); 
             if (item != null) return new ProjectItem() { Id = item.ID.ToString(), Name = item.Name }; 
           
            return null;
         } 
  
         public IQueryable<ProjectItem> GetAll()
         {
            throw new NotImplementedException();
        }

        /*public IEnumerable<ProjectItem> GetItemsForProject()
        {
            var workflows = Sitecore.Data.Database.GetDatabase("master").WorkflowProvider.GetWorkflows();
            var results = workflows.Select(workflow => new Models.Workflow() { DisplayName = workflow.Appearance.DisplayName, Id = workflow.WorkflowID });
            return results.AsQueryable();
        }*/

        public void Update(ProjectItem entity)
         {
            throw new NotImplementedException();
        } 
     } 
 } 
