using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestWebSite
{
    public partial class ModalDialogTest : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //Button1.OnClientClick = String.Format("$('#{0}').modal('show')", myModal.ClientID);
            var cs = Page.ClientScript;
            cs.RegisterClientScriptBlock(this.GetType(), "Open", String.Format("$('#{0}').modal(options)", myModal.ClientID), true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var cs = Page.ClientScript;
            cs.RegisterClientScriptBlock(this.GetType(), "Open", String.Format("$('#{0}').modal(options)", myModal.ClientID), true);
        }
    }
}