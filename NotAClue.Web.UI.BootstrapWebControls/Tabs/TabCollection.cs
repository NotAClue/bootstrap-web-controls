using System;
using System.Web.UI;

namespace NotAClue.Web.UI.BootstrapWebControls
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1035:ICollectionImplementationsHaveStronglyTypedMembers", Justification = "Unnecessary for this specialized class")]
	public class TabCollection : ControlCollection
	{
		public TabCollection(Control owner)
			: base(owner)
		{
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Assembly is not localized")]
		public override void Add(Control child)
		{
			if (!(child is Tab))
			{
				throw new ArgumentException("TabPanelCollection can only contain TabPanel controls.");
			}
			base.Add(child);
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Assembly is not localized")]
		public override void AddAt(int index, Control child)
		{
			if (!(child is Tab))
			{
				throw new ArgumentException("TabPanelCollection can only contain TabPanel controls.");
			}
			base.AddAt(index, child);
		}

		public new Tab this[int index]
		{
			get
			{
				return (Tab)base[index];
			}
		}
	}
}
