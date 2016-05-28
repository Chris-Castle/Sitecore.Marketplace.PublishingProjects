using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Events;
using System;

namespace Sitecore.Marketplace.PublishingProjects.Handlers.ItemVerisonAdded
{
    public class ClearProjectField
    {
        protected void OnVersionAdded(object sender, EventArgs args)
        {
            if (Sitecore.Configuration.Settings.GetBoolSetting("PublishingProjects.RemoveNewVersionsFromProject", true))
            {
                //ensures arguments aren't null
                Assert.ArgumentNotNull(sender, "sender");
                Assert.ArgumentNotNull(args, "args");

                //gets item parameter from event arguments
                object obj = Event.ExtractParameter(args, 0);
                Item item = obj as Item;
                if (item != null && item.IsProjectItem())
                {
                    if (!item.Editing.IsEditing) item.Editing.BeginEdit();
                    item["Project"] = String.Empty;
                    item.Editing.EndEdit();
                }
            }
        }
    }
}