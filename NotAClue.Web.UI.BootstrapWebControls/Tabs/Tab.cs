using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotAClue.Web.UI.BootstrapWebControls
{
	[ParseChildren(false)]
	[PersistChildren(true)]
	[ToolboxItem(false)]
	public class Tab : WebControl
	{
		#region [Constants]
		private const string CLASS = "class";
		private const string TAB_TEXT = "TitleText";
		#endregion

		#region [Fields]
		private BootstrapTabs _owner;
		#endregion

		#region [Constructors]
		public Tab() : base(HtmlTextWriterTag.Div) { }

		public Tab(HtmlTextWriterTag tag) : base(tag) { }

		protected Tab(string tag) : base(tag) { }
		#endregion

		#region [Properties]
		[DefaultValue("")]
		[Category("Appearance")]
		public String TitleText
		{
			get
			{
				return (string)(ViewState[TAB_TEXT] ?? string.Empty);
			}
			set
			{
				ViewState[TAB_TEXT] = value;
			}
		}

		[TypeConverter(typeof(Boolean))]
		[Category("Appearance")]
		[DefaultValue(null)]
		[Description("Disables (true) or enables (false) the tab.")]
		public Boolean Disabled { get; set; }

		[Category("Behavior")]
		[DefaultValue(0)]
		[Description("Zero-based index of the tab to be selected on initialization. To set all tabs to unselected pass -1 as value.")]
		public Boolean Active { get; set; }
		#endregion

		#region [Methods]
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			if (String.IsNullOrEmpty(this.TitleText))
				throw new InvalidOperationException("Each tab must have it's TitleText property set.");

			if (String.IsNullOrEmpty(this.ID))
				throw new InvalidOperationException("Each tab must have it's ID property set.");

			if (((BootstrapTabs)Parent).Tabs.OfType<Tab>().Count(t => t.ID == this.ID) > 1)
				throw new InvalidOperationException("Each tab must have it's ID property must be unique.");
		}

		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			//base.RenderBeginTag(writer);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			//<div
			writer.WriteBeginTag("div");

			var cssClass = "tab-pane";

			// set as active
			if (this.Active)
				cssClass += " active";

			// add any extra css classes
			if (!String.IsNullOrEmpty(this.CssClass))
				cssClass += " " + this.CssClass;

			//<div class="tab-pane active"
			writer.WriteAttribute(CLASS, cssClass);

			//<div class="tab-pane active" id="home"
			writer.WriteAttribute("id", String.Format("{0}-content-{1}", Parent.ClientID, this.ClientID));

			//<div class="tab-pane active" id="home">
			writer.Write(HtmlTextWriter.TagRightChar);
			writer.WriteLine();
			writer.Indent++;

			// render child controls here
			base.RenderChildren(writer);
			writer.WriteLine();
			writer.Indent--;

			//<div class="active"><a href="#home" role="tab" data-toggle="tab">Home</a></div>
			writer.WriteEndTag("div");
		}

		public override void RenderEndTag(HtmlTextWriter writer)
		{
			//base.RenderEndTag(writer);
		}

		internal void SetOwner(BootstrapTabs owner)
		{
			_owner = owner;
		}
		#endregion
	}
}
