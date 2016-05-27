using Sitecore.Data.Items;
using Sitecore.Shell.Applications.ContentEditor.Gutters;

namespace Sitecore.Marketplace.PublishingProjects.Gutters
{
    public class ShowProjectGutter : GutterRenderer
    {
        protected override GutterIconDescriptor GetIconDescriptor(Item item)
        {
            GutterIconDescriptor iconDescriptor = base.GetIconDescriptor(item);
            if (item.IsProjectItem())
            {
                string iconPath = "Office/32x32/document_pinned.png";
                GutterIconDescriptor gutterIcon = new GutterIconDescriptor()
                {
                    Icon = iconPath,
                    Tooltip = "Item belongs to '" + item.ProjectTitle() + "' project"
                };

                return gutterIcon;
            }

            return null;
        }
    }
}
