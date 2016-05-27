using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.ExperienceEditor.Speak.Server.Requests;
using Sitecore.ExperienceEditor.Speak.Server.Contexts;
using Sitecore.Shell.Applications.ContentManager;
using Sitecore.ExperienceEditor.Speak.Server.Responses;
using Sitecore.Data.Items;

namespace Sitecore.Marketplace.PublishingProjects.Speak.Requests
{
    public class GenerateFieldEditorUrlForProject : PipelineProcessorRequest<ItemContext>
    {
        public string GenerateUrl()
        {
            var fieldList = CreateFieldDescriptors(RequestContext.Argument.ToLower());
            var fieldeditorOption = new FieldEditorOptions(fieldList);
            //Save item when ok button is pressed
            fieldeditorOption.SaveItem = true;
            return fieldeditorOption.ToUrlString().ToString();
        }

        private List<FieldDescriptor> CreateFieldDescriptors(string accessKey)
        {
            Item editItem = this.RequestContext.Item;
            var fieldList = new List<FieldDescriptor>();
            
           if (editItem.Template.GetField(new ID("{9E26F08E-5510-4E45-BC98-3B1C31F7D89F}")) != null)
                fieldList.Add(new FieldDescriptor(editItem, "Project"));
            
            return fieldList;
        }

        public override PipelineProcessorResponseValue ProcessRequest()
        {
            return new PipelineProcessorResponseValue
            {
                Value = GenerateUrl()
            };
        }
    }
}