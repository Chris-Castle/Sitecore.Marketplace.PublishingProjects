using Sitecore.ExperienceEditor.Speak.Server.Requests;
using Sitecore.ExperienceEditor.Speak.Server.Contexts;

namespace Sitecore.Marketplace.PublishingProjects.Speak.Requests
{
    public class IsItemProjectable : PipelineProcessorControlStateRequest<ItemContext>
    {
        public override bool GetControlState()
        {
            this.RequestContext.ValidateContextItem();
            if (this.RequestContext.WebEditMode != "edit")
                return false;

            return this.RequestContext.Item.IsItemProjectable();
        }
    }
}