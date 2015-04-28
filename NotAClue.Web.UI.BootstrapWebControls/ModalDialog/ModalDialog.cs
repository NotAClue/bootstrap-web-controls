using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

namespace NotAClue.Web.UI.BootstrapWebControls
{
	[System.Drawing.ToolboxBitmap(typeof(BootstrapTabs), "Tabs.BootstrapModalDialog.ico")]
	public class BootstrapModalDialog : WebControl
	{
		#region [Constants]
		private const string CLASS = "class";
		#endregion

		#region [Fields]
		#endregion

		#region [Properties]
		[DefaultValue(ModalSizes.Default)]
		[Category("Behavior")]
		public ModalSizes ModalSize { get; set; }

		[DefaultValue(true)]
		[Category("Behavior")]
		public Boolean ShowFooter { get; set; }

		[DefaultValue(true)]
		[Category("Behavior")]
		public Boolean FadeIn { get; set; }

		[DefaultValue(true)]
		[Category("Behavior")]
		public Boolean Backdrop { get; set; }

		[DefaultValue(true)]
		[Category("Behavior")]
		public Boolean Keyboard { get; set; }

		[DefaultValue(true)]
		[Category("Behavior")]
		public Boolean Show { get; set; }

		[DefaultValue(true)]
		[Category("Behavior")]
		public ModalButtons FooterButtons { get; set; }

		[DefaultValue("")]
		[Category("Appearance")]
		public String Title { get; set; }

		[DefaultValue("")]
		[Category("Appearance")]
		public String Message { get; set; }
		#endregion

		#region [Constructors]
		protected BootstrapModalDialog() { }

		public BootstrapModalDialog(System.Web.UI.HtmlTextWriterTag tag) : base(tag) { }

		protected BootstrapModalDialog(string tag) : base(tag) { }
		#endregion

		#region [Initializer]
		/// <summary>
		/// Fires when pages initializes.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			Page.RegisterRequiresControlState(this);

			//// add client script
			//var script = new StringBuilder();
			//script.Append("$(document).ready(function () {\n");
			////script.Append("		alert('I got run!');\n");

			//// clear cookie if this is hte initial page load
			//if (!this.Page.IsPostBack)
			//	script.AppendFormat("		$.cookie('{0}-last_tab', null, {{ path: '/' }});\n", this.ClientID);

			//script.AppendFormat("	$('#{0} a[data-toggle=tab]').on('shown.bs.tab', function (e) {{\n", this.ClientID);
			//script.Append("		//save the latest tab using a cookie:\n");
			//script.AppendFormat("		$.cookie('{0}-last_tab', $(e.target).attr('href'));\n", this.ClientID);
			////script.Append("		alert($(e.target).attr('href'));\n");
			//script.Append("	});\n");
			//script.Append("	//activate latest tab, if it exists:\n");
			//script.AppendFormat("	var lastTab = $.cookie('{0}-last_tab');\n", this.ClientID);
			//script.Append("	if (lastTab) {\n");
			////script.Append("		alert(lastTab);\n");
			//script.Append("		$('a[href=' + lastTab + ']').tab('show');\n");
			//script.Append("	}\n");
			//script.Append("	else {\n");
			//script.Append("		// Set the first tab if cookie does not exist\n");
			//script.AppendFormat("		$('#{0} a[data-toggle=\"tab\"]:first').tab('show');\n", this.ClientID);
			//script.Append("	}\n");
			//script.Append("});\n");

			//var scriptKey = this.ClientID + "-script";
			//Type scriptType = this.GetType();

			//// Get a ClientScriptManager reference from the Page class.
			//ClientScriptManager csm = Page.ClientScript;

			//// Check to see if the Client Script Include is already registered.
			//if (!csm.IsClientScriptIncludeRegistered(scriptType, scriptKey))
			//{
			//	csm.RegisterStartupScript(scriptType, scriptKey, script.ToString(), true);
			//}
		}
		#endregion

		#region [Methods]
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			//<!-- Modal -->
			//<div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			writer.WriteBeginTag("div");
			writer.WriteAttribute("id", ClientID);

			// set css class
			var modalCssClass = "modal";
			if (this.FadeIn)
				modalCssClass += " fade";

			// add custom css class
			if (!String.IsNullOrEmpty(this.CssClass))
				modalCssClass += " " + this.CssClass;

			writer.WriteAttribute(CLASS, modalCssClass);

			writer.WriteAttribute("tabindex", "-1");
			writer.WriteAttribute("role", "dialog");
			writer.WriteAttribute("aria-labelledby", ClientID + "-Label");
			writer.WriteAttribute("aria-hidden", "true");
			writer.Write(HtmlTextWriter.TagRightChar);
			writer.WriteLine();
			writer.Indent++;

			//  <div class="modal-dialog">
			writer.WriteBeginTag("div");
			writer.WriteAttribute(CLASS, "modal-dialog");
			writer.Write(HtmlTextWriter.TagRightChar);
			writer.WriteLine();
			writer.Indent++;


			//	  <div class="modal-content">
			writer.WriteBeginTag("div");
			writer.WriteAttribute(CLASS, "modal-content");
			writer.Write(HtmlTextWriter.TagRightChar);
			writer.WriteLine();
			writer.Indent++;


			//	    <div class="modal-header">
			writer.WriteBeginTag("div");
			writer.WriteAttribute(CLASS, "modal-header");
			writer.Write(HtmlTextWriter.TagRightChar);
			writer.WriteLine();
			writer.Indent++;


			//	  	  <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
			writer.WriteBeginTag("button");
			writer.WriteAttribute("type", "button");
			writer.WriteAttribute(CLASS, "close");
			writer.WriteAttribute("data-dismiss", "modal");
			writer.Write(HtmlTextWriter.TagRightChar);

			//<span aria-hidden="true">&times;</span>
			writer.WriteBeginTag("span");
			writer.WriteAttribute("aria-hidden", "true");
			writer.Write(HtmlTextWriter.TagRightChar);
			writer.Write("&times;");
			writer.WriteEndTag("span");

			//<span class="sr-only">Close</span>
			writer.WriteBeginTag("span");
			writer.WriteAttribute(CLASS, "sr-only");
			writer.Write(HtmlTextWriter.TagRightChar);
			writer.Write("Close");
			writer.WriteEndTag("span");

			//</button>
			writer.WriteEndTag("button");

			//	  	  <h4 class="modal-title" id="myModalLabel">Modal title</h4>
			writer.WriteBeginTag("h4");
			writer.WriteAttribute(CLASS, "modal-title");
			writer.WriteAttribute("id", ClientID + "-Label");
			writer.Write(HtmlTextWriter.TagRightChar);
			writer.Write(this.Title);
			writer.WriteEndTag("h4");

			//	    </div>
			writer.Indent--;
			writer.WriteLine();
			writer.WriteEndTag("div");

			//	    <div class="modal-body">
			writer.WriteBeginTag("div");
			writer.WriteAttribute(CLASS, "modal-body");
			writer.Write(HtmlTextWriter.TagRightChar);
			writer.WriteLine();
			writer.Indent++;

			//	  	body text
			writer.Write(this.Message);

			//	    </div>
			writer.Indent--;
			writer.WriteLine();
			writer.WriteEndTag("div");

			if (this.ShowFooter)
			{
				//	    <div class="modal-footer">
				writer.WriteBeginTag("div");
				writer.WriteAttribute(CLASS, "modal-footer");
				writer.Write(HtmlTextWriter.TagRightChar);
				writer.WriteLine();
				writer.Indent++;

				// footer buttons
				if (((FooterButtons & ModalButtons.Cancel) == ModalButtons.Cancel) || ((FooterButtons & ModalButtons.Close) == ModalButtons.Close))
				{
					//<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
					writer.WriteBeginTag("button");
					writer.WriteAttribute("id", ClientID + "-close");
					writer.WriteAttribute("type", "button");
					writer.WriteAttribute(CLASS, "btn btn-default");
					writer.WriteAttribute("data-dismiss", "modal");
					writer.Write(HtmlTextWriter.TagRightChar);

					if ((FooterButtons & ModalButtons.Close) == ModalButtons.Close) { writer.Write("Close"); }

					if ((FooterButtons & ModalButtons.Cancel) == ModalButtons.Cancel) { writer.Write("Cancel"); }

					writer.WriteEndTag("button");
				}




				//	  	  <button type="button" class="btn btn-primary">Save changes</button>
				if ((FooterButtons & ModalButtons.Save) == ModalButtons.Save)
				{
					writer.WriteBeginTag("button");
					writer.WriteAttribute("id", ClientID + "-save");
					writer.WriteAttribute("type", "button");
					writer.WriteAttribute(CLASS, "btn btn-primary");
					writer.WriteAttribute("data-modal-command", "save");
					writer.WriteEndTag("button");
				}

				//	  	  <button type="button" class="btn btn-primary">OK</button>
				if ((FooterButtons & ModalButtons.OK) == ModalButtons.OK)
				{
					writer.WriteBeginTag("button");
					writer.WriteAttribute("id", ClientID + "-ok");
					writer.WriteAttribute("type", "button");
					writer.WriteAttribute(CLASS, "btn btn-primary");
					writer.WriteAttribute("data-modal-command", "save");
					writer.WriteEndTag("button");
				}

				//	    </div>
				writer.Indent--;
				writer.WriteLine();
				writer.WriteEndTag("div");
			}


			//	    </div>
			writer.Indent--;
			writer.WriteLine();
			writer.WriteEndTag("div");

			//	  </div>
			writer.Indent--;
			writer.WriteLine();
			writer.WriteEndTag("div");

			//  </div>
			writer.Indent--;
			writer.WriteLine();
			writer.WriteEndTag("div");

			//</div>
			writer.Indent--;
			writer.WriteLine();
			writer.WriteEndTag("div");
		}

		public override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
		}
		#endregion
	}
}
