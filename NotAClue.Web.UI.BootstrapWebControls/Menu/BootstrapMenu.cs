using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace NotAClue.Web.UI.BootstrapWebControls
{
	/// <summary>
	/// Class BootstrapMenu.
	/// </summary>
	[ControlValueProperty("SelectedValue")]
	[DefaultEvent("MenuItemClick")]
	[SupportsEventValidation]
	[ToolboxData("<{0}:BootstrapMenu runat=\"server\"></{0}:BootstrapMenu>")]
	[System.Drawing.ToolboxBitmap(typeof(BootstrapMenu), "Menu.BootstrapMenu.ico")]
	public class BootstrapMenu : Menu
	{
		/// <summary>
		/// Adds tag attributes and writes the markup for the opening tag of the control to the output stream emitted to the browser or device.
		/// </summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"/> containing methods to build and render the device-specific output.</param>
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
		}

		protected override void Render(HtmlTextWriter writer)
		{
			writer.Indent++;
			BuildItems(Items, MenuItemType.Root, writer);
			writer.Indent--;
			writer.WriteLine();
		}

		/// <summary>
		/// Performs final markup and writes the HTML closing tag of the control to the output stream emitted to the browser or device.
		/// </summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"/> containing methods to build and render the device-specific output.</param>
		public override void RenderEndTag(HtmlTextWriter writer)
		{
		}

		public enum MenuItemType
		{
			Root,
			Normal,
			Split
		}

		/// <summary>
		/// Builds the items.
		/// </summary>
		/// <param name="items">The items.</param>
		/// <param name="menuItemType">if set to <c>true</c> [is root].</param>
		/// <param name="writer">The writer.</param>
		private void BuildItems(MenuItemCollection items, MenuItemType menuItemType, HtmlTextWriter writer)
		{
			if (items.Count > 0)
			{
				writer.WriteLine();

				writer.WriteBeginTag("ul");

				switch (menuItemType)
				{
					case MenuItemType.Root:
						var navClass = "nav";
						if (!String.IsNullOrEmpty(CssClass))
						{
							navClass += " " + CssClass;
						}
						writer.WriteAttribute("class", navClass);
						break;
					case MenuItemType.Normal:
						writer.WriteAttribute("class", "dropdown-menu");
						break;
					case MenuItemType.Split:
						writer.WriteAttribute("class", "dropdown-menu pull-right");
						break;
				}

				writer.Write(HtmlTextWriter.TagRightChar);
				writer.Indent++;

				foreach (MenuItem item in items)
				{
					BuildItem(item, writer);
				}

				writer.Indent--;
				writer.WriteLine();
				writer.WriteEndTag("ul");
			}
		}

		/// <summary>
		/// Builds the item.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="writer">The writer.</param>
		private void BuildItem(MenuItem item, HtmlTextWriter writer)
		{
			if (item != null && writer != null)
			{
				writer.WriteLine();
				writer.WriteBeginTag("li");

				var cssClass = (item.ChildItems.Count > 0) ? "dropdown" : string.Empty;
				var selectedStatusClass = GetSelectStatusClass(item);
				if (!String.IsNullOrEmpty(selectedStatusClass))
				{
					cssClass += " " + selectedStatusClass;
				}
				writer.WriteAttribute("class", cssClass);

				writer.Write(HtmlTextWriter.TagRightChar);
				writer.Indent++;
				writer.WriteLine();

				if ((item.ChildItems != null) && (item.ChildItems.Count > 0))
				{
					if (IsLink(item))
					{
						writer.WriteBeginTag("li");
						writer.WriteAttribute("class", "dropdown-split-left");
						writer.Write(HtmlTextWriter.TagRightChar);

						writer.WriteBeginTag("a");
						if (!String.IsNullOrEmpty(item.NavigateUrl))
						{
							writer.WriteAttribute("href", Page.Server.HtmlEncode(ResolveClientUrl(item.NavigateUrl)));
						}
						else
						{
							writer.WriteAttribute("href", Page.ClientScript.GetPostBackClientHyperlink(this, "b" + item.ValuePath.Replace(PathSeparator.ToString(), "\\"), true));
						}

						writer.WriteTargetAttribute(item.Target);

						if (!String.IsNullOrEmpty(item.ToolTip))
						{
							writer.WriteAttribute("title", item.ToolTip);
						}
						writer.Write(HtmlTextWriter.TagRightChar);
						writer.WriteLine();
						writer.Write(item.Text.Trim());

						writer.WriteEndTag("a");
						writer.Indent--;
						writer.WriteEndTag("li");

						writer.WriteBeginTag("li");
						writer.WriteAttribute("class", "dropdown dropdown-split-right hidden-sm");
						writer.Write(HtmlTextWriter.TagRightChar);

						writer.WriteBeginTag("a");
						writer.WriteAttribute("href", "#");
						writer.WriteAttribute("class", "dropdown-toggle");
						writer.WriteAttribute("data-toggle", "dropdown");
						writer.Write(HtmlTextWriter.TagRightChar);

						writer.WriteBeginTag("i");
						writer.WriteAttribute("class", "caret");
						writer.Write(HtmlTextWriter.TagRightChar);
						writer.WriteEndTag("i");

						writer.WriteEndTag("a");

						BuildItems(item.ChildItems, MenuItemType.Split, writer);

						writer.WriteEndTag("li");
					}
					else
					{
						writer.WriteBeginTag("a");
						writer.WriteAttribute("class", "dropdown-toggle");
						writer.WriteAttribute("data-toggle", "dropdown");
						writer.WriteAttribute("href", "#");
						writer.Write(HtmlTextWriter.TagRightChar);
						writer.Write(item.Text);
						writer.Write("&nbsp;");
						writer.WriteBeginTag("b");
						writer.WriteAttribute("class", "caret");
						writer.Write(HtmlTextWriter.TagRightChar);
						writer.WriteEndTag("b");
						writer.WriteEndTag("a");

						BuildItems(item.ChildItems, MenuItemType.Normal, writer);
					}
				}
				else
				{
					if (IsLink(item))
					{
						writer.WriteBeginTag("a");
						if (!String.IsNullOrEmpty(item.NavigateUrl))
						{
							writer.WriteAttribute("href", Page.Server.HtmlEncode(ResolveClientUrl(item.NavigateUrl)));
						}
						else
						{
							writer.WriteAttribute("href",
							Page.ClientScript.GetPostBackClientHyperlink(this,
								"b" + item.ValuePath.Replace(PathSeparator.ToString(), "\\"), true));
						}
						cssClass = GetItemClass(this, item);
						writer.WriteAttribute("class", cssClass);
						writer.WriteTargetAttribute(item.Target);

						if (!String.IsNullOrEmpty(item.ToolTip))
						{
							writer.WriteAttribute("title", item.ToolTip);
						}
						else
						{
							if (!String.IsNullOrEmpty(ToolTip))
							{
								writer.WriteAttribute("title", ToolTip);
							}
						}
						writer.Write(HtmlTextWriter.TagRightChar);
						writer.Indent++;
						writer.WriteLine();
						writer.Write(item.Text);

						writer.Indent--;
						writer.WriteEndTag("a");
					}
					else
					{
						writer.WriteBeginTag("li");
						writer.WriteAttribute("class", GetItemClass(this, item));
						writer.Write(HtmlTextWriter.TagRightChar);
						writer.Indent++;
						writer.WriteLine();
					}
				}
				writer.Indent--;
				writer.WriteLine();
				writer.WriteEndTag("li");
			}
		}

		/// <summary>
		/// Determines whether the specified item is link.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns><c>true</c> if the specified item is link; otherwise, <c>false</c>.</returns>
		private bool IsLink(MenuItem item)
		{
			return (item != null) && item.Enabled && (!String.IsNullOrEmpty(item.NavigateUrl) || item.Selectable);
		}

		/// <summary>
		/// Gets the item class.
		/// </summary>
		/// <param name="menu">The menu.</param>
		/// <param name="item">The item.</param>
		/// <returns>System.String.</returns>
		private string GetItemClass(Menu menu, MenuItem item)
		{
			var value = string.Empty;
			if (item != null)
			{
				if (((item.Depth < menu.StaticDisplayLevels) && (menu.StaticItemTemplate != null)) || ((item.Depth >= menu.StaticDisplayLevels) && (menu.DynamicItemTemplate != null)))
				{
					value = string.Empty;
				}
				else
				{
					if (IsLink(item))
					{
						value = string.Empty;
					}
				}
				var selectedStatusClass = GetSelectStatusClass(item);
				if (!String.IsNullOrEmpty(selectedStatusClass))
				{
					value += " " + selectedStatusClass;
				}
			}
			return value.Trim();
		}

		/// <summary>
		/// Gets the select status class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns>System.String.</returns>
		private string GetSelectStatusClass(MenuItem item)
		{
			var value = String.Empty;
			if (item.Selected || IsChildItemSelected(item) || IsParentItemSelected(item))
			{
				value = "active";
			}
			return value.Trim();
		}

		/// <summary>
		/// Determines whether [is child item selected] [the specified item].
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns><c>true</c> if [is child item selected] [the specified item]; otherwise, <c>false</c>.</returns>
		private bool IsChildItemSelected(MenuItem item)
		{
			var bRet = false;

			if ((item != null) && (item.ChildItems != null))
			{
				bRet = IsChildItemSelected(item.ChildItems);
			}
			return bRet;
		}

		/// <summary>
		/// Determines whether [is child item selected] [the specified items].
		/// </summary>
		/// <param name="items">The items.</param>
		/// <returns><c>true</c> if [is child item selected] [the specified items]; otherwise, <c>false</c>.</returns>
		private bool IsChildItemSelected(MenuItemCollection items)
		{
			var bRet = false;

			if (items != null)
			{
				foreach (MenuItem item in items)
				{
					if (item.Selected || IsChildItemSelected(item.ChildItems))
					{
						bRet = true;
						break;
					}
				}
			}

			return bRet;
		}

		/// <summary>
		/// Determines whether [is parent item selected] [the specified item].
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns><c>true</c> if [is parent item selected] [the specified item]; otherwise, <c>false</c>.</returns>
		private bool IsParentItemSelected(MenuItem item)
		{
			var bRet = false;

			if ((item != null) && (item.Parent != null))
			{
				if (item.Parent.Selected)
				{
					bRet = true;
				}
				else
				{
					bRet = IsParentItemSelected(item.Parent);
				}
			}

			return bRet;
		}
	}
}
