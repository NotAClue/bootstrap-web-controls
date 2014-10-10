using System;
using System.Collections.Generic;
using System.Linq;
using NotAClue.Web.UI.BootstrapWebControls;
using System.Web.UI.WebControls;

namespace TestWebSite
{
	public partial class TabsTest : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var bootstrapTab = new BootstrapTabs();
			bootstrapTab.ID = "RuntimeRenderedTab";
			PlaceHolder1.Controls.Add(bootstrapTab);
			for (int i = 0; i < 4; i++)
			{
				var tab = new Tab();
				tab.ID = String.Format("Tab-{0}", i);
				tab.TitleText = String.Format("Tab #{0}", i);

				if (i == 0)
					tab.Active = true;

				var text = new Literal();
				text.ID = String.Format("Text{0}", i);
				text.Text = String.Format("{0} Some random text will be placed here {0}", i);

				tab.Controls.Add(text);

				bootstrapTab.Controls.Add(tab);
			}
		}
	}
}
