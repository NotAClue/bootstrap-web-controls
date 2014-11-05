using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotAClue.Web.UI.BootstrapWebControls
{
    [ParseChildren(false)]
    [ToolboxItem(false)]
    [DefaultProperty("Text")]
    public class FooterButton : Control
    {
        #region [Constants]
        private const string CLASS = "class";
        private const string TEXT = "TitleText";
        #endregion

        #region [Fields]
        private BootstrapTabs _owner;
        #endregion

        #region [Constructors]
        public FooterButton()
        {

        }
        #endregion

        #region [Properties]
        [DefaultValue("")]
        [Category("Appearance")]
        public String Text
        {
            get { return (string)(ViewState[TEXT] ?? string.Empty); }
            set { ViewState[TEXT] = value; }
        }

        [DefaultValue("Cancel")]
        [Category("Appearance")]
        public FooterButtonType ButtonType { get; set; }

        [DefaultValue("Default")]
        [Category("Appearance")]
        public FooterButtonStyle ButtonStyle { get; set; }
        #endregion

        #region [Methods]
        protected override void Render(HtmlTextWriter writer)
        {
            //<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            writer.WriteBeginTag("button");
            writer.WriteAttribute("id", this.ClientID);
            writer.WriteAttribute("type", "button");
            writer.WriteAttribute("class", String.Format("btn btn-{0}", this.ButtonStyle.ToString().ToLower()));
            writer.WriteAttribute("data-dismiss", "modal");
            writer.Write(HtmlTextWriter.TagRightChar);

            if (!String.IsNullOrEmpty(this.Text))
                writer.Write(this.Text);
            else
                base.RenderChildren(writer);

            //</button>
            writer.WriteEndTag("button");
        }

        internal void SetOwner(BootstrapTabs owner)
        {
            _owner = owner;
        }
        #endregion
    }
}