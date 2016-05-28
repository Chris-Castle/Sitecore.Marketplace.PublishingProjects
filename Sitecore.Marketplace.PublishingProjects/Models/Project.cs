using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Marketplace.PublishingProjects.Models
{
    public class Project : Sitecore.Services.Core.Model.EntityIdentity
    {
        public string itemId
        {
            get { return Id; }
        }

        public string Name { get; set; }
    }
}
