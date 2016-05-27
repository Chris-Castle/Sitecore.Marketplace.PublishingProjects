using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Resources;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;

namespace Sitecore.Marketplace.PublishingProjects.UI
{
    class ProjectItemForm : DialogForm
    {
        protected Scrollbox Links;
        protected Scrollbox Status;
        Item item;
        Sitecore.Data.Database master;

        protected override void OnLoad(EventArgs e)
        {
            master = Factory.GetDatabase("master");
            if (!Context.ClientPage.IsEvent)
            {
                item = GetCurrentItem();

                if (!string.IsNullOrEmpty(item.Fields[Data.ProjectFieldId].Value) || item.TemplateID == Data.ProjectDetails)
                {
                    StringBuilder result = new StringBuilder();
                    StringBuilder resultStatus = new StringBuilder();
                    RenderProjectItems(result, resultStatus);

                    Links.Controls.Add(new LiteralControl(result.ToString()));
                    Status.Controls.Add(new LiteralControl(resultStatus.ToString()));
                }
            }
            base.OnLoad(e);
        }

        protected override void OnOK(object sender, EventArgs args)
        {
            base.OnOK(sender, args);
        }


        private void RenderProjectItems(StringBuilder ProjItems, StringBuilder ItemStatus)
        {
            List<Item> searchedItems;
            ID ProjectId = item.ID;
            if (item.TemplateID != Data.ProjectDetails)
            {
                ProjectId = new ID(item.Fields[Data.ProjectFieldId].Value);
            }

            searchedItems = Helper.SearchForItems(ProjectId);

            foreach (Item result in searchedItems)
            {
                string lnk = string.Format("self.close();window.opener.scForm.postRequest(\"\",\"\",\"\",\"item:load(id={0})\");", result.ID.ToString()); 

                ProjItems.Append(
                    string.Concat(
                        new object[] { "<a href=\"#\" class=\"scLink\" onclick='",lnk, "'\">",
                            Images.GetImage(result.Appearance.Icon, 0x10, 0x10, "absmiddle", "0px 4px 0px 0px"), String.Format("{0} [{1}:{2}]",result.DisplayName, result.Language.Name, result.Version), " - [", result.Paths.Path, "]</a>" 
                        }
                   )
                );

                Item wfitem = GetItemWorkflowState(result.Fields["__Workflow state"].Value);
                if (wfitem != null)
                {
                    ItemStatus.Append(
                        string.Concat(
                            new object[] { "<a href=\"#\" class=\"scLink\" style=\"text-decoration:none;\">", Images.GetImage(wfitem.Appearance.Icon, 0x10, 0x10, "absmiddle", "0px 4px 0px 0px"), wfitem.Name, "</a>" }
                        )
                    );
                }
                else
                {
                    ItemStatus.Append("<a href=\"#\" class=\"scLink\" style=\"text-decoration:none;\">Not in workflow</a>");
                }
            }
        }
                
        

        private Item GetCurrentItem()
        {
            // Parse the querystring to get the item
            string queryString = WebUtil.GetQueryString("id");
            Language language = Language.Parse(WebUtil.GetQueryString("la"));
            Sitecore.Data.Version version = Sitecore.Data.Version.Parse(WebUtil.GetQueryString("vs"));
            return master.GetItem(new ID(queryString), language, version);
        }

        private Item GetItemWorkflowState(string ItemworkflowState)
        {
            if (string.IsNullOrEmpty(ItemworkflowState))
            {
                return null;
            }
            return master.GetItem(new ID(ItemworkflowState));
        }
    }
}
