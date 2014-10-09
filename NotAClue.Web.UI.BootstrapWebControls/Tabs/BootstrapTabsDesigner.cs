using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web.UI.Design;

namespace NotAClue.Web.UI.BootstrapWebControls
{
	public class BootstrapTabsDesigner : ControlDesigner
	{
		public BootstrapTabsDesigner()
		{
		}

		/// <summary>
		/// Helper property to get the TabContainer we're designing.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Security handled by base class")]
		private TabContainer TabContainer
		{
			get
			{
				return (BootstrapTabs)Component;
			}
		}

		/// <summary>
		/// Create and return our action list - this is what creates the fly out panel with the verb commands
		/// </summary>
		public override DesignerActionListCollection ActionLists
		{
			[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Security handled by base class")]
			get
			{
				DesignerActionListCollection actionLists = new DesignerActionListCollection();
				actionLists.AddRange(base.ActionLists);
				actionLists.Add(new TabContainerDesignerActionList(this));

				return actionLists;
			}
		}

		/// <summary>
		/// Tell the designer we're creating our own UI.
		/// </summary>
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Security handled by base class")]
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			if (regions == null)
			{
				throw new ArgumentNullException("regions");
			}

			if (TabContainer.ActiveTab != null)
			{
				// create the main editable region
				//
				EditableDesignerRegion region = new EditableDesignerRegion(this, String.Format(CultureInfo.InvariantCulture, "c{0}", TabContainer.ActiveTab.ID));

				regions.Add(region);

				// build out the content HTML.  We'll need this later.
				//
				string contentHtml = GetTabContent(TabContainer.ActiveTab, true);

				StringBuilder clickRegions = new StringBuilder();

				// now build out the design time tab UI.
				//
				// we do this by looping through the tabs and either building a link for clicking, and a plain DesignerRegion, or,
				// we build a plain span an attach an EditableDesigner region to it.

				int count = 2; // start with two since we've already got two regions. these numbers need to correspond to the order in the regions collection
				foreach (TabPanel tp in TabContainer.Tabs)
				{
					bool isActive = tp.Active;

					string headerText = GetTabContent(tp, false);

					// Build out the HTML for one of the tabs.  No, I don't usually write code like this, but this is just kind
					// of icky no matter how you do it.  Nothing to see here.
					//
					clickRegions.AppendFormat(CultureInfo.InvariantCulture, ClickRegionHtml,
						DesignerRegion.DesignerRegionAttributeName,
						(isActive ? 1 : count), // if it's the editable one, it has to be index 1, see below
						String.Format(CultureInfo.InvariantCulture, isActive ? ActiveTabLink : TabLink, headerText),
						ColorTranslator.ToHtml(SystemColors.ControlText),
						(isActive ? ColorTranslator.ToHtml(SystemColors.Window) : "transparent"),
						(isActive ? "border-left:thin white outset;border-right:thin white outset;" : "")
					  );


					// the region names are arbitrary.  for this purpose, we encode them by a letter - h or t for header or tab, respectively,
					// and then pop on the tab ID.
					//
					if (isActive)
					{
						// the editable header region is always to be 1, so we insert it there.
						//
						regions.Insert(1, new EditableDesignerRegion(this, String.Format(CultureInfo.InvariantCulture, "h{0}", tp.ID)));
					}
					else
					{
						// otherwise, just create a plain region.
						//
						DesignerRegion clickRegion = new DesignerRegion(this, String.Format(CultureInfo.InvariantCulture, "t{0}", tp.ID));
						clickRegion.Selectable = true;
						count++;
						regions.Add(clickRegion);
					}
				}


				// OK build out the final full HTML for this control.
				//
				StringBuilder sb = new StringBuilder(1024);
				var actualHeight =
					(!TabContainer.Height.IsEmpty && TabContainer.Height.Type == UnitType.Pixel)
					? (TabContainer.Height.Value - 62).ToString() + "px" :
					"100%";

				sb.Append(String.Format(CultureInfo.InvariantCulture,
										DesignTimeHtml,
										ColorTranslator.ToHtml(SystemColors.ControlText),
										ColorTranslator.ToHtml(SystemColors.ControlDark),
										TabContainer.ID,
										ColorTranslator.ToHtml(SystemColors.ControlText),
										ColorTranslator.ToHtml(SystemColors.Control),
										clickRegions.ToString(),
										contentHtml,
										TabContainer.Width,
										TabContainer.Height,
										DesignerRegion.DesignerRegionAttributeName,
										ColorTranslator.ToHtml(SystemColors.Window),
										actualHeight,
										HideOverflowContent ? "hidden" : "visible"
										));
				return sb.ToString();
			}
			else
			{
				// build the empty tab html.

				StringBuilder sb = new StringBuilder(512);

				sb.AppendFormat(CultureInfo.InvariantCulture, EmptyDesignTimeHtml,
										ColorTranslator.ToHtml(SystemColors.ControlText),
										ColorTranslator.ToHtml(SystemColors.ControlDark),
										TabContainer.ID,
										DesignerRegion.DesignerRegionAttributeName);

				// add a designer region for the "AddTab" UI.
				//
				DesignerRegion dr = new DesignerRegion(this, AddTabName);
				regions.Add(dr);
				return sb.ToString();
			}
		}
	}
}
