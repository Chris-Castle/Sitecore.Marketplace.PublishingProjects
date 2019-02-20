using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Events;
using System;
using System.Collections.Generic;

namespace Sitecore.Marketplace.PublishingProjects.Handlers.ItemSaved
{
	public class HandlePublishingTogether
	{
		private static readonly SynchronizedCollection<Sitecore.Data.ID> inProcess = new SynchronizedCollection<Sitecore.Data.ID>();

		protected void OnItemSaved(object sender, EventArgs args)
		{
			//ensures arguments aren't null
			Assert.ArgumentNotNull(sender, "sender");
			Assert.ArgumentNotNull(args, "args");

			//gets item parameter from event arguments
			object obj = Event.ExtractParameter(args, 0);
			Item item = obj as Item;

			// if it is a project definition item, we need to see if publish together is checked.  If so, make sure everything that is released is noved to holding unless everything is approved.
			/*if (item.TemplateID == Data.ProjectDetails)
			{
				Database db = Sitecore.Context.ContentDatabase;
				if (db != null && db.Name == "master")
				{
					foreach (Item i in Helper.SearchForItems(item.ID))
					{
						//               UpdateRestrictionsOnVersion(i);
					}
				}
			}*/

			// if saved item is in a project, we need to see if Publish Together is checked
			// if it is then we may need to move the current item to holding
			if (item.IsProjectItem() && isItemInProjectMarkedPublishTogether(item))
			{
				// If current item is publishable, move it to holding
				if (isItemPublishable(item))
				{
					// See if all other items in project are in holding state
					bool areAllReady = true;
					foreach (Item i in Helper.SearchForItems(new ID(item["Project"])))
					{
						// if this item is not in the holding state, everything is not ready
						if (!isItemInHoldingState(i) && i.ID != item.ID)
							areAllReady = false;
					}

					if (areAllReady)
					{
						// everything is ready so we need to publish them all
						foreach (Item i in Helper.SearchForItems(new ID(item["Project"])))
						{
							MoveItemToFinalState(i);
						}
						MoveItemToFinalState(item);  // we need to move the initial item because it is in the Syncronized Collection
					}
					else
					{
						MoveItemToHoldingState(item);
					}
				}

				
							

				// TODO: Now see if this is a new item to a project that should unpublish the rest of the project
			}
		}

		private bool isItemInHoldingState(Item item)
		{
			return item.Fields["__Workflow State"].Value == getHoldingStateForItem(item);
		}

		private bool isItemInFinalState(Item item)
		{
			return item.Fields["__Workflow State"].Value == getFinalStateForItem(item);
		}

		private static string getHoldingStateForItem(Item item)
		{
			return "{13473C8E-1F7A-4A9A-BEAC-808B292B8FF9}";
		}

		private static string getProjectWorkflowForItem(Item item)
		{
			return "{EC08B249-0DD8-4E36-BDDA-29DB96B57D38}";
		}

		private static string getFinalStateForItem(Item item)
		{
			return "{71A35048-17D3-4815-A2AE-33488BFA5E26}";
		}
		private bool isItemInProjectMarkedPublishTogether(Item item)
		{
			return true;
		}

		private bool isItemPublishable(Item item)
		{
			string workflowState = item["__Workflow State"];
			if (workflowState == string.Empty)
				return true; // if the new item had no workflow, it was publishable

			Database db = Sitecore.Data.Database.GetDatabase("master");
			Item workflowStateDefinition = db.GetItem(new ID(workflowState));
			if (workflowStateDefinition != null && workflowStateDefinition["Final"] == "1")
				return true;

			return false; 
		}

		private static void MoveItemToFinalState(Item item)
		{
			MoveItemToState(item, getFinalStateForItem(item));
		}

		private static void MoveItemToHoldingState(Item item)
		{
			MoveItemToState(item, getHoldingStateForItem(item));
		}

		private static void MoveItemToState(Item item, String state)
		{
			if (inProcess.Contains(item.ID))
			{
				return;
			}

			inProcess.Add(item.ID);
			try
			{
				Database db = Sitecore.Context.ContentDatabase;
				if (db != null && db.Name == "master")
				{
					Item project = db.GetItem(item[Data.ProjectFieldId]);
					if (project != null)
					{
						if (!item.Editing.IsEditing) item.Editing.BeginEdit();
						item.Fields["__Workflow State"].Value = state; //final
						item.Editing.EndEdit();
					}
				}
			}
			finally
			{
				inProcess.Remove(item.ID);
			}
		}
	}
}