using Sitecore.Data.Items;
using Sitecore.Pipelines.GetPageEditorNotifications;

namespace Sitecore.Marketplace.PublishingProjects.Pipelines
{
    class DisplayProjectExperienceEditorWarning
    {
        public void Process(GetPageEditorNotificationsArgs args)
        {
            Item contextItem = args.ContextItem;

            if (contextItem.IsProjectItem())
            {
                string command = string.Format("Sitecore Project: This item is part of the '{0}' project", contextItem.ProjectTitle());
                PageEditorNotification editorNotification = new PageEditorNotification(command, PageEditorNotificationType.Information);
                args.Notifications.Add(editorNotification);
            }
        }
    }
}
