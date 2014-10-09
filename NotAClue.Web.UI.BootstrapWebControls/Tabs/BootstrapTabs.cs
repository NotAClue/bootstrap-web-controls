using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotAClue.Web.UI.BootstrapWebControls
{
	[ParseChildren(typeof(Tab))]
	[System.Drawing.ToolboxBitmap(typeof(BootstrapTabs), "Tabs.BootstrapTabs.ico")]
	public class BootstrapTabs : WebControl
	{
		#region [Constants]
		private const string CLASS = "class";
		#endregion

		#region [Fields]
		private bool _initialized;
		private int _activeTabIndex = -1;
		private int _cachedActiveTabIndex = -1;
		#endregion

		#region [Properties]
		/// <summary>
		/// Index of active tab.
		/// </summary>
		[DefaultValue(-1)]
		[Category("Behavior")]
		public virtual int ActiveTabIndex
		{
			get
			{
				if (_cachedActiveTabIndex > -1)
				{
					return _cachedActiveTabIndex;
				}
				if (Tabs.Count == 0)
				{
					return -1;
				}
				return _activeTabIndex;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (Tabs.Count == 0 && !_initialized)
				{
					_cachedActiveTabIndex = value;
				}
				else
				{
					if (ActiveTabIndex != value)
					{
						if (ActiveTabIndex != -1 && ActiveTabIndex < Tabs.Count)
						{
							Tabs[ActiveTabIndex].Active = false;
						}
						if (value >= Tabs.Count)
						{
							_activeTabIndex = Tabs.Count - 1;
							_cachedActiveTabIndex = value;
						}
						else
						{
							_activeTabIndex = value;
							_cachedActiveTabIndex = -1;
						}
						if (ActiveTabIndex != -1 && ActiveTabIndex < Tabs.Count)
						{
							Tabs[ActiveTabIndex].Active = true;
						}
					}
				}
			}
		}

		/// <summary>
		/// Keeps collection of tabPanels those will be contained by the TabContainer.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TabCollection Tabs
		{
			get
			{
				return (TabCollection)Controls;
			}
		}

		/// <summary>
		/// Provides object of current active tab panel.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Tab ActiveTab
		{
			get
			{
				EnsureActiveTab();
				var i = ActiveTabIndex;
				if (i < 0 || i >= Tabs.Count)
				{
					return null;
				}
				return Tabs[i];
			}
			set
			{
				var i = Tabs.IndexOf(value);
				if (i < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ActiveTabIndex = i;
			}
		}

		/// <summary>
		/// Index of last active tab
		/// </summary>
		private int LastActiveTabIndex
		{
			get
			{
				return (int)(ViewState["LastActiveTabIndex"] ?? -1);
			}
			set
			{
				ViewState["LastActiveTabIndex"] = value;
			}
		}
		#endregion

		#region [Constructors]
		public BootstrapTabs() : base(HtmlTextWriterTag.Div) { }

		public BootstrapTabs(HtmlTextWriterTag tag) : base(tag) { }

		protected BootstrapTabs(string tag) : base(tag) { }
		#endregion

		#region [Methods]
		/// <summary>
		/// Fires when pages initializes.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			Page.RegisterRequiresControlState(this);

			_initialized = true;
			if (_cachedActiveTabIndex > -1)
			{
				ActiveTabIndex = _cachedActiveTabIndex;
				if (ActiveTabIndex < Tabs.Count)
				{
					Tabs[ActiveTabIndex].Active = true;
				}
			}
			else
			{
				if (Tabs.Count > 0)
				{
					ActiveTabIndex = 0;
				}
			}

			// add client script
			var script = new StringBuilder();
			script.Append("$(function ()\n");
			script.Append("{\n");
			script.AppendFormat("	$('#{0} a[data-toggle=tab]').on('shown.bs.tab', function (e) {{\n", this.ClientID);
			script.Append("		//save the latest tab using a cookie:\n");
			script.AppendFormat("		$.cookie('{0}-last_tab', $(e.target).attr('href'));\n", this.ClientID);
			script.Append("	});\n");
			script.Append("	//activate latest tab, if it exists:\n");
			script.AppendFormat("	var lastTab = $.cookie('{0}-last_tab');\n", this.ClientID);
			script.Append("	if (lastTab) {\n");
			script.Append("		$('a[href=' + lastTab + ']').tab('show');\n");
			script.Append("	}\n");
			script.Append("	else {\n");
			script.Append("		// Set the first tab if cookie do not exist\n");
			script.AppendFormat("		$('#{0} a[data-toggle=\"tab\"]:first').tab('show');\n", this.ClientID);
			script.Append("	}\n");
			script.Append("});\n");

			var scriptKey = this.ClientID + "-script";
			Type scriptType = this.GetType();

			// Get a ClientScriptManager reference from the Page class.
			ClientScriptManager csm = Page.ClientScript;

			// Check to see if the Client Script Include is already registered.
			if (!csm.IsClientScriptIncludeRegistered(scriptType, scriptKey))
			{
				csm.RegisterStartupScript(scriptType, scriptKey, script.ToString(), true);
			}
		}

		/// <summary>
		/// AddParseSubObject checks if object is not null and type of TabPanel.
		/// </summary>
		/// <param name="obj">Object to add in the container</param>
		protected override void AddParsedSubObject(object obj)
		{
			var objTabPanel = obj as Tab;
			if (null != objTabPanel)
			{
				Controls.Add(objTabPanel);
			}
			else if (!(obj is LiteralControl))
			{
				throw new HttpException(string.Format(CultureInfo.CurrentCulture, "TabContainer cannot have children of type '{0}'.", obj.GetType()));
			}
		}

		/// <summary>
		/// Sets TabContainer as owner of control and Adds the control into the base
		/// at specified index.
		/// </summary>
		/// <param name="control">Object of type TabPanel</param>
		/// <param name="index">Index where to add the control</param>
		protected override void AddedControl(Control control, int index)
		{
			((Tab)control).SetOwner(this);
			base.AddedControl(control, index);
		}

		/// <summary>
		/// Removed the TabPanel control from the base.
		/// </summary>
		/// <param name="control">Object of type TabPanel</param>
		protected override void RemovedControl(Control control)
		{
			var controlTabPanel = control as Tab;
			if (control != null && controlTabPanel.Active && ActiveTabIndex < Tabs.Count)
			{
				EnsureActiveTab();
			}
			controlTabPanel.SetOwner(null);
			base.RemovedControl(control);
		}

		/// <summary>
		/// provides collection of TabPanels.
		/// </summary>
		/// <returns>ControlCollection</returns>
		protected override ControlCollection CreateControlCollection()
		{
			return new TabCollection(this);
		}

		///// <summary>
		///// Loads the savedState of TabContainer.
		///// </summary>
		///// <param name="savedState">savedState</param>
		//protected override void LoadControlState(object savedState)
		//{
		//	var p = (Pair)savedState;
		//	if (p != null)
		//	{
		//		base.LoadControlState(p.First);
		//		ActiveTabIndex = (int)p.Second;
		//	}
		//	else
		//	{
		//		base.LoadControlState(null);
		//	}
		//}

		///// <summary>
		///// Saves the controlState to load next post back.
		///// </summary>
		///// <returns>object containing saved state</returns>
		//protected override object SaveControlState()
		//{
		//	LastActiveTabIndex = ActiveTabIndex;

		//	var p = new Pair();
		//	p.First = base.SaveControlState();
		//	p.Second = ActiveTabIndex;
		//	if (p.First == null && p.Second == null)
		//	{
		//		return null;
		//	}
		//	else
		//	{
		//		return p;
		//	}
		//}

		/// <summary>
		/// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
		/// </summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
		}

		/// <summary>
		/// Renders the control to the specified HTML writer.
		/// </summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content.</param>
		protected override void Render(HtmlTextWriter writer)
		{
			//// add hidden field to keep tabs on the current TAB
			//var tabIdHiddenField = new HiddenField()
			//{
			//	ID = String.Format("{0}HiddenField", this.ClientID),
			//	Value = _selected_tab
			//};
			//tabIdHiddenField.RenderControl(writer);

			//<!-- Nav tabs -->
			//<ul class="nav nav-tabs" role="tablist">
			writer.WriteBeginTag("ul");
			//add controls id
			writer.WriteAttribute("id", ClientID);
			writer.WriteAttribute(CLASS, "nav nav-tabs");
			writer.WriteAttribute("role", "tablist");
			writer.Write(HtmlTextWriter.TagRightChar);
			writer.WriteLine();
			writer.Indent++;

			//<li class="active"><a href="#home" role="tab" data-toggle="tab">Home</a></li>
			//<li><a href="#profile" role="tab" data-toggle="tab">Profile</a></li>
			//<li><a href="#messages" role="tab" data-toggle="tab">Messages</a></li>
			//<li><a href="#settings" role="tab" data-toggle="tab">Settings</a></li>
			foreach (Tab tab in Tabs)
			{
				//<li
				writer.WriteBeginTag("li");

				//add controls id
				writer.WriteAttribute("id", "tab-" + ClientID);

				//<li class="active"
				if (ActiveTab.ID == tab.ID)
					writer.WriteAttribute("class", "active");

				//<li class="active">
				writer.Write(HtmlTextWriter.TagRightChar);

				//<a
				writer.WriteBeginTag("a");

				//<a href="#home"
				writer.WriteAttribute("href", String.Format("#{0}-content-{1}", this.ClientID, tab.ClientID));

				//<a href="#home" role="tab"
				writer.WriteAttribute("role", "tab");

				//<a href="#home" role="tab" data-toggle="tab"
				writer.WriteAttribute("data-toggle", "tab");

				//<a href="#home" role="tab" data-toggle="tab">
				writer.Write(HtmlTextWriter.TagRightChar);

				//<a href="#home" role="tab" data-toggle="tab">Home
				writer.Write(tab.TitleText.Trim());

				//<a href="#home" role="tab" data-toggle="tab">Home</a>
				writer.WriteEndTag("a");

				//<li class="active"><a href="#home" role="tab" data-toggle="tab">Home</a></li>
				writer.WriteEndTag("li");
			}

			writer.Indent--;
			writer.WriteLine();
			//</ul>
			writer.WriteEndTag("ul");
			writer.WriteLine();

			//<!-- Tab panes -->
			//<div class="tab-content">
			//  <div class="tab-pane active" id="home">...</div>
			//  <div class="tab-pane" id="profile">...</div>
			//  <div class="tab-pane" id="messages">...</div>
			//  <div class="tab-pane" id="settings">...</div>
			//</div>
			//<!-- Nav tabs -->

			//<div
			writer.WriteBeginTag("div");
			//<div class="tab-content"
			writer.WriteAttribute(CLASS, "tab-content");
			//<div class="tab-content">
			writer.Write(HtmlTextWriter.TagRightChar);
			writer.WriteLine();
			writer.Indent++;

			//<div class="tab-pane active" id="home">...</div>
			//<div class="tab-pane" id="profile">...</div>
			//<div class="tab-pane" id="messages">...</div>
			//<div class="tab-pane" id="settings">...</div>
			foreach (Tab tab in Tabs)
			{
				tab.RenderControl(writer);
			}

			//</div>
			writer.Indent--;
			writer.WriteLine();
			writer.WriteEndTag("div");

		}

		/// <summary>
		/// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
		/// </summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
		}

		/// <summary>
		/// Ensures the active tab.
		/// </summary>
		private void EnsureActiveTab()
		{
			if (_activeTabIndex < 0 || _activeTabIndex >= Tabs.Count)
			{
				_activeTabIndex = 0;
			}

			for (var i = 0; i < Tabs.Count; i++)
			{
				if (i == ActiveTabIndex)
				{
					Tabs[i].Active = true;
				}
				else
				{
					Tabs[i].Active = false;
				}
			}
		}
		#endregion
	}
}
