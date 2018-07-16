using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI.WebControls;

namespace NotAClue.Web.UI.BootstrapWebControls
{
	#region wwWebTabControl

	/// <summary>
	/// Summary description for wwWebTabControl.
	/// </summary>
	[ToolboxData("<{0}:wwWebTabControl runat=server></{0}:wwWebTabControl>")]
	[ToolboxBitmap(typeof(System.Web.UI.WebControls.Image))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class wwWebTabControl : Control
	{
		/// <summary>
		/// Collection of Tabpages.
		/// </summary>
		//[Bindable(true)]
		//[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]  // Content generates code for each page
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TabPageCollection TabPages
		{
			get { return Tabs; }
		}
		private TabPageCollection Tabs = new TabPageCollection();


		/// <summary>
		/// The completed control output
		/// </summary>
		private string Output = "";

		/// <summary>
		/// The output for the tabs generated by RenderTabs
		/// </summary>
		private string TabOutput = "";

		/// <summary>
		/// The output of the Script block required to handle tab activation
		/// </summary>
		private string Script = "";

		protected System.Web.UI.WebControls.Literal txtActivationScript;
		protected System.Web.UI.WebControls.Literal txtTabPlaceHolder;
		private new bool DesignMode = (HttpContext.Current == null);

		/// <summary>
		/// The Selected Tab. Set this to the TabPageClientId of the tab that you want to have selected
		/// </summary>
		[Browsable(true), Description("The TabPageClientId of the selected tab. This TabPageClientId must map to TabPageClientId assigned to a tab. Should also match an ID tag in the doc that is shown or hidden when tab is activated.")]
		public string SelectedTab
		{
			get { return this.cSelectedTab; }
			set { this.cSelectedTab = value; }
		}
		string cSelectedTab = "";

		/// <summary>
		/// The width for each of the tabs. Each tab will be this width.
		/// </summary>
		[Browsable(true), Description("The width of all the individual tabs in pixels"), DefaultValue(110)]
		public int TabWidth
		{
			get { return this._TabWidth; }
			set { this._TabWidth = value; }
		}
		int _TabWidth = 110;

		/// <summary>
		/// The height of each of the tabs.
		/// </summary>
		[Browsable(true), Description("The Height of all the individual tabs in pixels"), DefaultValue(25)]
		public int TabHeight
		{
			get { return this._TabHeight; }
			set { this._TabHeight = value; }
		}
		int _TabHeight = 25;

		/// <summary>
		/// The CSS class that is used to render a selected button. Defaults to selectedtabbutton.
		/// </summary>
		[Browsable(true), Description("The CSS style used for the selected tab"), DefaultValue("selectedtabbutton")]
		public string SelectedTabCssClass
		{
			get { return this.cSelectedTabCssClass; }
			set { this.cSelectedTabCssClass = value; }
		}
		string cSelectedTabCssClass = "selectedtabbutton";

		/// <summary>
		/// The CSS class that is used to render nonselected tabs.
		/// </summary>
		[Browsable(true), Description("The CSS style used for non selected tabs"), DefaultValue("tabbutton")]
		public string TabCssClass
		{
			get { return this.cTabCssClass; }
			set { this.cTabCssClass = value; }
		}
		string cTabCssClass = "tabbutton";


		protected override void OnLoad(EventArgs e)
		{
			// *** Handle the Selected Tab Postback operation
			if (this.Page.IsPostBack) // && !this.TabSelected) 
			{
				string TabSelection = this.Page.Request.Form["wwClientTabControlSelection_" + this.ID];
				if (TabSelection != "")
					this.SelectedTab = TabSelection;
			}

			base.OnLoad(e);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			bool NoTabs = false;
			string Selected = null;
			// *** If no tabs have been defined in design mode write a canned HTML display 
			if (this.DesignMode && (this.TabPages == null || this.TabPages.Count == 0))
			{
				NoTabs = true;
				this.AddTab("No Tabs", "default", "Tab1");
				this.AddTab("No Tabs 2", "default", "Tab2");
				Selected = this.SelectedTab;
				this.SelectedTab = "Tab2";
			}

			// *** Render the actual control
			this.RenderControl();

			// *** Dump the output into the ASP out stream
			writer.Write(this.Output);

			// *** Call the base to let it output the writer's output
			base.Render(writer);

			if (NoTabs)
			{
				this.TabPages.Clear();
				this.SelectedTab = Selected;
			}
		}

		/// <summary>
		/// High level routine that 
		/// </summary>
		private void RenderControl()
		{
			// *** Generate the HTML for the tabs and script blocks
			this.RenderTabs();

			// *** Generate the HTML for the base page and embed
			// *** the script etc into the page
			this.Output = string.Format(wwWebTabControl.MasterTemplate,
										this.TabOutput, this.Script, this.SelectedTabCssClass);

			// *** Persist our selection into a hidden var since it's all client side 
			// *** Script updates this var
			this.Page.RegisterHiddenField("wwClientTabControlSelection_" + this.ID, this.SelectedTab);

			// *** Force the page to show the Selected Tab
			if (this.cSelectedTab != "")
				this.Page.RegisterStartupScript("wwWebTabControl_Startup", "<script language='javascript'>ShowTabPage('" + this.SelectedTab + "');</script>");

		}


		/// <summary>
		/// Creates various string properties that are merged into the output template.
		/// Creates the tabs and the associated script code.
		/// </summary>
		private void RenderTabs()
		{
			if (this.Tabs != null)
			{
				// *** ActivateTab script code
				StringBuilder Script = new StringBuilder();

				// *** ShowTabPage script code
				StringBuilder Script2 = new StringBuilder();

				// *** HtmlTextWriter to handle output generation for the HTML
				StringBuilder sb = new StringBuilder();
				StringWriter sw = new StringWriter(sb);
				HtmlTextWriter TabWriter = new HtmlTextWriter(sw);

				Script.Append(
@"
<script>
function ActivateTab(object)
{
PageName = object.id;
");
				Script2.Append(@"function ShowTabPage(PageName)
{
");

				TabWriter.Write("<table border='0' cellspacing='0'><tr>");

				int Count = 0;
				foreach (TabPage Tab in this.Tabs)
				{
					Count++;
					string Id = this.ID + "_" + Count;

					TabWriter.WriteBeginTag("td");

					TabWriter.WriteAttribute("height", this.TabHeight.ToString() + "px");
					TabWriter.WriteAttribute("width", this.TabWidth.ToString() + "px");

					TabWriter.WriteAttribute("ID", Id);

					string ActionLink = this.FixupActionLink(Tab);

					if (ActionLink != "")
						TabWriter.WriteAttribute("onclick", ActionLink);


					if (this.Tabs == null)
						return;

					if (Tab.TabPageClientId != "" && Tab.TabPageClientId == this.cSelectedTab)
						TabWriter.WriteAttribute("class", this.SelectedTabCssClass);
					else
						TabWriter.WriteAttribute("class", this.TabCssClass);

					TabWriter.Write(HtmlTextWriter.TagRightChar);
					TabWriter.Write(Tab.Caption);
					TabWriter.WriteEndTag("td");
					TabWriter.Write("\r\n");

					Script.Append("document.getElementById('" + Id + "').className='tabbutton';\r\n");

					// *** If we have a TabPageClientId we need the page to be udpateable
					// *** through a matching ID tag.
					if (Tab.TabPageClientId != "")
						Script2.Append("document.getElementById('" + Tab.TabPageClientId + "').style.display='none'\r\n");

				}
				TabWriter.Write("</tr></table>");

				Script.Append("document.getElementById(PageName).className='selectedtabbutton';\r\n");
				Script2.Append("document.getElementById(PageName).style.display='';\r\n");
				Script2.Append("document.getElementById('wwClientTabControlSelection_" + this.ID + "').value=PageName;\r\n");

				this.TabOutput = sb.ToString();
				TabWriter.Close();

				Script.Append("}\r\n");

				Script.Append(Script2.ToString() + "}\r\n");
				Script.Append("</script>\r\n");

				this.Script = Script.ToString();
			}
		}



		/// <summary>
		/// Adds a new item to the Tab collection.
		/// </summary>
		/// <param name="Caption">The caption of the tab</param>
		/// <param name="Link">The HTTP or JavaScript link that is fired when the tab is activated. Can optionally be Default which activates the tab and activates the page ID.</param>
		/// <param name="TabPageClientId">The ID for this tab - map this to an ID tag in the HTML form.</param>
		public void AddTab(string Caption, string Link, string TabPageClientId)
		{
			TabPage Tab = new TabPage();
			Tab.Caption = Caption;
			Tab.ActionLink = Link;
			Tab.TabPageClientId = TabPageClientId;
			this.AddTab(Tab);
		}

		/// <summary>
		/// Adds a new item to the Tab collection.
		/// </summary>
		/// <param name="Caption">The caption of the tab</param>
		/// <param name="Link">The HTTP or JavaScript link that is fired when the tab is activated. Can optionally be Default which activates the tab and activates the page ID.</param>
		public void AddTab(string Caption, string Link)
		{
			this.AddTab(Caption, Link, "");
		}
		public void AddTab(TabPage Tab)
		{
			this.Tabs.Add(Tab);
		}


		/// <summary>
		/// Fixes up the ActionLink property to final script code
		/// suitable for an onclick handler
		/// </summary>
		/// <returns></returns>
		protected string FixupActionLink(TabPage Tab)
		{
			string Action = Tab.ActionLink;

			if (Action.ToLower() == "default")
				Action = "javascript:ActivateTab(this);ShowTabPage('" + Tab.TabPageClientId + "');";

			// *** If we don't have 'javascript:' in the text it's a Url: must use document to go there
			if (Action != "" && Action.IndexOf("script:") < 0)
				Action = "document.location='" + Action + "';";

			return Action;
		}



		/// <summary>
		/// Required to be able to properly deal with the Collection object
		/// </summary>
		/// <param name="obj"></param>
		protected override void AddParsedSubObject(object obj)
		{
			if (obj is TabPage)
			{
				this.TabPages.Add((TabPage)obj);
				return;
			}
		}

		/// <summary>
		/// The master HTML template into which the dynamically generated tab display is rendered.
		/// </summary>
		private static string MasterTemplate =
												@"
												{1}
												<table width='100%' border='0' cellspacing='0' cellpadding='0'>
												   <tr>
													  <td>{0}</td>
												   </tr>
												   <tr>
													  <td class='{2}' height='3'></td>
												   </tr>
												</table>
												";

	}

	#endregion

	#region TabPage Class

	/// <summary>
	/// The individual TabPage class that holds the intermediary Tab page values
	/// </summary>
	[ToolboxData("<{0}:TabPage runat=server></{0}:TabPage>")]
	public class TabPage : Control
	{
		[NotifyParentProperty(true)]
		[Browsable(true), Description("The display caption for the Tab.")]
		public string Caption
		{
			get { return cCaption; }
			set { cCaption = value; }
		}
		string cCaption = "";

		[NotifyParentProperty(true)]
		[Browsable(true), Description("The TabPageClientId for this item. If you create a TabPageClientId you must create a matching ID tag in your HTML that is to be enabled and disabled automatically.")]
		public string TabPageClientId
		{
			get { return cTabPageClientId; }
			set { cTabPageClientId = value; }
		}
		string cTabPageClientId = "";

		[NotifyParentProperty(true)]
		[Browsable(true), Description("The Url or javascript: code that fires. You can use javascript:ActivateTab(this);ShowPage('TabPageClientIdValue'); to force a tab and page to display.")]
		public string ActionLink
		{
			get { return Href; }
			set { Href = value; }
		}
		string Href = "";


		//		[NotifyParentProperty(true)]
		//		[Browsable(true),Description("Determines whether this tab shows as selected.")]
		//		public bool Selected 
		//		{
		//			get { return this.bSelected; }
		//			set { 
		//					this.bSelected = value;
		//				if (this.Parent != null)  
		//				{
		//					((wwWebTabControl) this.Parent).SelectedTab = this.TabPageClientId;
		//				}
		//				}
		//			
		//		}
		//		bool bSelected = false;
	}

	#endregion

	#region TabPageCollection Class
	/// <summary>
	/// Holds a collection of Tab Pages
	/// </summary>
	public class TabPageCollection : CollectionBase
	{
		public TabPageCollection()
		{
		}

		/// <summary>
		/// Indexer property for the collection that returns and sets an item
		/// </summary>
		public TabPage this[int index]
		{
			get
			{
				return (TabPage)this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		/// <summary>
		/// Adds a new error to the collection
		/// </summary>
		public void Add(TabPage Tab)
		{
			this.List.Add(Tab);
		}

		public void Insert(int index, TabPage item)
		{
			this.List.Insert(index, item);
		}

		public void Remove(TabPage Tab)
		{
			List.Remove(Tab);
		}

		public bool Contains(TabPage Tab)
		{
			return this.List.Contains(Tab);
		}

		//Collection IndexOf method 
		public int IndexOf(TabPage item)
		{
			return List.IndexOf(item);
		}

		public void CopyTo(TabPage[] array, int index)
		{
			List.CopyTo(array, index);
		}
	}
	#endregion
}
