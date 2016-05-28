using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Marketplace.PublishingProjects.Models
{
    public class ProjectItem : Sitecore.Services.Core.Model.EntityIdentity
    {
        public string itemId
        {
            get { return Id; }
        }

        public string Icon { get; set; }

        public string Name { get; set; }

        public string TemplateName { get; set; }

        public int Version { get; set; }

        public string Language { get; set; }

        public string Workflow { get; set; }
    }
}
