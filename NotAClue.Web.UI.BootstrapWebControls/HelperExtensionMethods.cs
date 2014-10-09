using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace NotAClue.Web.UI.BootstrapWebControls
{
	public static class HelperExtensionMethods
	{
		public static void WriteTargetAttribute(this HtmlTextWriter writer, string targetValue)
		{
			if ((writer != null) && (!String.IsNullOrEmpty(targetValue)))
			{
				if (targetValue.Equals("_blank", StringComparison.OrdinalIgnoreCase))
				{
					var js = "window.open(this.href, '_blank', ''); return false;";
					writer.WriteAttribute("onclick", js);
					writer.WriteAttribute("onkeypress", js);
				}
				else
				{
					writer.WriteAttribute("target", targetValue);
				}
			}
		}
	}
}
