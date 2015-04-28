using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Specialized;

namespace NotAClue.Web.UI.BootstrapWebControls
{
    [ParseChildren(typeof(FooterButton))]
    [PersistChildren(false)]
    [ToolboxBitmap(typeof(BootstrapModalDialog), "ModalDialog.BootstrapModalDialog.ico")]
    [DefaultEvent("ButtonClicked")]
    [DefaultProperty("Text")]
    public class BootstrapModalDialog : Control, IPostBackDataHandler
    {
        #region [Constants]
        private const string CLASS = "class";
        #endregion

        #region [Fields]
        private FooterButtonCollection _FooterButtons = new FooterButtonCollection();
        #endregion

        #region [Events]
        private static readonly object ButtonClickedEvent = new object();

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            ButtonClickedName = postCollection[this.UniqueID];
            return true;
        }

        public void RaisePostDataChangedEvent()
        {
            OnButtonClicked(EventArgs.Empty);
        }

        protected virtual void OnButtonClicked(EventArgs e)
        {
            EventHandler buttonClickedHandler = (EventHandler)Events[ButtonClickedEvent];
            if (buttonClickedHandler != null)
                buttonClickedHandler(this, e);
        }

        [Category("Action")]
        [Description("Raised when text changes")]
        public event EventHandler ButtonClicked
        {
            add
            {
                Events.AddHandler(ButtonClickedEvent, value);
            }
            remove
            {
                Events.RemoveHandler(ButtonClickedEvent, value);
            }
        }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        [Browsable(true)]
        [Bindable(true)]
        [Localizable(false)]
        [Category("Appearance")]
        [Description("Button Clicked Id")]
        public String ButtonClickedName
        {
            get
            {
                var s = (String)ViewState["Values"];
                return (String.IsNullOrEmpty(s) ? String.Empty : s);
            }
            set
            {
                ViewState["Values"] = value;
            }
        }
        #endregion

        #region [Properties]
        [DefaultValue(BootstrapModalSizes.Default)]
        [Category("Behavior")]
        public BootstrapModalSizes ModalSize { get; set; }

        [DefaultValue(true)]
        [Category("Behavior")]
        public Boolean DisplayFooter { get; set; }

        [DefaultValue("")]
        [Category("Behavior")]
        public String CssClass { get; set; }

        [DefaultValue(true)]
        [Category("Behavior")]
        public Boolean FadeIn { get; set; }

        [DefaultValue(true)]
        [Category("Behavior")]
        public Boolean Backdrop { get; set; }

        [DefaultValue(true)]
        [Category("Behavior")]
        public Boolean Keyboard { get; set; }

        [DefaultValue(false)]
        [Category("Behavior")]
        public Boolean Show { get; set; }

        [DefaultValue("")]
        [Category("Appearance")]
        public String Title { get; set; }

        [Browsable(true)]
        [Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public FooterButtonCollection FooterButtons
        {
            get { return _FooterButtons; }
        }
        #endregion

        #region [Constructors]
        public BootstrapModalDialog()
        {
        }
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
        protected override void AddParsedSubObject(object obj)
        {
            var footerButton = obj as FooterButton;
            if (footerButton != null)
            {
                this._FooterButtons.Add(footerButton);
                return;
            }
            else
            {
                this.Controls.Add((Control)obj);
                return;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
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

            if (!this.DesignMode)
                writer.WriteAttribute("aria-hidden", "true");

            writer.Write(HtmlTextWriter.TagRightChar);
            writer.WriteLine();
            writer.Indent++;

            //  <div class="modal-dialog">
            writer.WriteBeginTag("div");

            var modalClass = "modal-dialog";
            switch (ModalSize)
            {
                case BootstrapModalSizes.Small:
                    modalClass += " modal-sm";
                    break;
                case BootstrapModalSizes.Large:
                    modalClass += " modal-lg";
                    break;
            }

            writer.WriteAttribute(CLASS, modalClass);

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

            // render child controls here
            if (this.Controls.Count > 0)
            {
                writer.WriteLine();
                writer.Indent++;

                base.RenderChildren(writer);
                writer.WriteLine();
                writer.Indent--;

                //	    </div>
                writer.Indent--;
                writer.WriteLine();
                writer.WriteEndTag("div");
            }

            if (this.DisplayFooter)
            {
                //	    <div class="modal-footer">
                writer.WriteBeginTag("div");
                writer.WriteAttribute(CLASS, "modal-footer");
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.WriteLine();
                writer.Indent++;

                // render footer buttons
                foreach (FooterButton footerButton in _FooterButtons)
                {
                    footerButton.RenderControl(writer);
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
        #endregion
    }
}
