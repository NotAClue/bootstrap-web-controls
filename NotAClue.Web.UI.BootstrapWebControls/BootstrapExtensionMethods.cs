using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace NotAClue.Web.UI.BootstrapWebControls
{
	public class DateTimePickerFormat
	{
		public String MomentJSFormat { get; set; }

		public String CSharpFormat { get; set; }
	}
	public static class BootstrapExtensionMethods
	{
		private static Dictionary<String, DateTimePickerFormat> dateFormats = new Dictionary<String, DateTimePickerFormat>();
		private static Dictionary<String, DateTimePickerFormat> timeFormats = new Dictionary<String, DateTimePickerFormat>();
		private static Dictionary<String, DateTimePickerFormat> dateTimeFormats = new Dictionary<String, DateTimePickerFormat>();

		static BootstrapExtensionMethods()
		{
			dateFormats = new Dictionary<String, DateTimePickerFormat>()
			{
				{"en-GB", new DateTimePickerFormat() { MomentJSFormat = "DD/MM/YYYY", CSharpFormat = "dd/MM/yyyy" } },
				{"en-US", new DateTimePickerFormat() { MomentJSFormat = "MM/DD/YYYY", CSharpFormat = "MM/dd/yyyy" }  },
			};
			timeFormats = new Dictionary<String, DateTimePickerFormat>()
			{
				{"en-GB", new DateTimePickerFormat() { MomentJSFormat = "HH:mm", CSharpFormat = "HH:mm" }  },
				{"en-US", new DateTimePickerFormat() { MomentJSFormat = "HH:mm", CSharpFormat = "HH:mm" }  },
			};
			dateTimeFormats = new Dictionary<String, DateTimePickerFormat>()
			{
				{"en-GB", new DateTimePickerFormat() { MomentJSFormat = "DD/MM/YYYY HH:mm", CSharpFormat = "dd/MM/yyyy HH:mm" }  },
				{"en-US", new DateTimePickerFormat() { MomentJSFormat = "MM/DD/YYYY HH:mm", CSharpFormat = "MM/dd/yyyy HH:mm" }  },
			};
		}

		public static DateTimePickerFormat GetDateTimePickerDateFormat(this CultureInfo cultureInfo)
		{
			//var shortDatePattern = cultureInfo.DateTimeFormat.ShortDatePattern.ToUpper();
			//format: 'MM/DD/YYYY'
			//return shortDatePattern;

			//return "MM/DD/YYYY";
			if (dateFormats.ContainsKey(cultureInfo.Name))
				return dateFormats[cultureInfo.Name];

			return dateFormats["en-US"];
		}

		public static DateTimePickerFormat GetDateTimePickerTimeFormat(this CultureInfo cultureInfo)
		{
			//var shortTimePattern = cultureInfo.DateTimeFormat.ShortTimePattern.Replace(" tt", "").Replace("h", "HH").Replace(":ss", "");
			//format: 'HH:mm'
			//return shortTimePattern;

			//return "HH:mm";
			if (timeFormats.ContainsKey(cultureInfo.Name))
				return timeFormats[cultureInfo.Name];

			return timeFormats["en-US"];
		}

		public static DateTimePickerFormat GetDateTimePickerDateTimeFormat(this CultureInfo cultureInfo)
		{
			//var shortDatePattern = cultureInfo.DateTimeFormat.ShortDatePattern.ToUpper();
			//var shortTimePattern = cultureInfo.DateTimeFormat.ShortTimePattern.Replace(" tt", "").Replace("h", "HH").Replace(":ss", "");
			//format: 'MM/DD/YYYY HH:mm'
			//return shortDatePattern + " " + shortTimePattern;

			//return "MM/DD/YYYY HH:mm";
			if (dateTimeFormats.ContainsKey(cultureInfo.Name))
				return dateTimeFormats[cultureInfo.Name];

			return dateTimeFormats["en-US"];
		}
	}
}
