using Sitecore.Data;
using Sitecore.Marketplace.PublishingProjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Marketplace.PublishingProjects.Repository
{
    public class ProjectRepository : Sitecore.Services.Core.IRepository<Project> 
     { 
         public void Add(Project entity)
         { 
             throw new NotImplementedException(); 
         } 
 
         public void Delete(Project entity)
         { 
             throw new NotImplementedException(); 
         } 
 
         public bool Exists(Project entity)
         { 
             var item = Sitecore.Data.Database.GetDatabase("master").GetItem(new ID(entity.Id)); 
             return item != null; 
         } 
  
         public Project FindById(string id)
         { 
            var item = Sitecore.Data.Database.GetDatabase("master").GetItem(new ID(id)); 
             if (item != null) return new Project() { Id = item.ID.ToString(), Name = item.Name }; 
           
            return null;
         } 
  
         public IQueryable<Project> GetAll()
         {
            throw new NotImplementedException();
        }

        public void Update(Project entity)
         {
            throw new NotImplementedException();
        } 
     } 
 } 
