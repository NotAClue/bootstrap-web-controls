<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TabsTest.aspx.cs" Inherits="TestWebSite.TabsTest" %>

<%@ Register Assembly="NotAClue.Web.UI.BootstrapWebControls" Namespace="NotAClue.Web.UI.BootstrapWebControls" TagPrefix="nac" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Bootstrap Tab Test Page</title>
	<link href="Content/bootstrap.css" rel="stylesheet" />
	<link href="Content/bootstrap-theme.min.css" rel="stylesheet" />
</head>
<body>
	<form id="form1" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
			<Scripts>
				<asp:ScriptReference Path="~/Scripts/jquery-1.9.0.min.js" />
				<asp:ScriptReference Path="~/Scripts/bootstrap.js" />
				<asp:ScriptReference Path="~/Scripts/jquery.cookie.js" />
			</Scripts>
		</asp:ScriptManager>
		<div>
			<h1>Bootstrap Tab Test Page</h1>
			<nac:BootstrapTabs ID="BootstrapTabs1" runat="server">
				<nac:Tab ID="Tab1" TitleText="Tab 1" Active="true" runat="server">
					Raw denim you probably haven't heard of them jean shorts Austin. Nesciunt tofu stumptown aliqua, retro synth master cleanse. Mustache cliche tempor, williamsburg carles vegan helvetica. Reprehenderit butcher retro keffiyeh dreamcatcher synth. Cosby sweater eu banh mi, qui irure terry richardson ex squid. Aliquip placeat salvia cillum iphone. Seitan aliquip quis cardigan american apparel, butcher voluptate nisi qui.			
				</nac:Tab>
				<nac:Tab ID="Tab2" TitleText="Tab 2" runat="server">
					<div class="form-horizontal" role="form">
						<asp:Label ID="Label1" runat="server" Text="Label" AssociatedControlID="TextBox1"></asp:Label>
						<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
						<asp:LinkButton
							runat="server"
							CommandName="Update"
							CssClass="btn btn-primary"><i class="glyphicon glyphicon-cloud-upload" ></i> Do something on the server (post back)</asp:LinkButton>
					</div>
					Food truck fixie locavore, accusamus mcsweeney's marfa nulla single-origin coffee squid. Exercitation +1 labore velit, blog sartorial PBR leggings next level wes anderson artisan four loko farm-to-table craft beer twee. Qui photo booth letterpress, commodo enim craft beer mlkshk aliquip jean shorts ullamco ad vinyl cillum PBR. Homo nostrud organic, assumenda labore aesthetic magna delectus mollit. Keytar helvetica VHS salvia yr, vero magna velit sapiente labore stumptown. Vegan fanny pack odio cillum wes anderson 8-bit, sustainable jean shorts beard ut DIY ethical culpa terry richardson biodiesel. Art party scenester stumptown, tumblr butcher vero sint qui sapiente accusamus tattooed echo park.			
				</nac:Tab>
			</nac:BootstrapTabs>
			<hr />
			<asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
		</div>
	</form>
	<script type="text/javascript">
		//$(function ()
		//{
		//	$('#BootstrapTabs1 a[data-toggle=tab]').on('shown.bs.tab', function (e)
		//	{
		//		// get parent id
		//		//var parentId = $(this).parent().parent().attr('id');
		//		//alert(parentId);
		//		//save the latest tab using a cookie:
		//		$.cookie('BootstrapTabs1-last_tab', $(e.target).attr('href'));
		//	});
		//	//activate latest tab, if it exists:
		//	var lastTab = $.cookie('BootstrapTabs1-last_tab');
		//	if (lastTab)
		//	{
		//		$('a[href=' + lastTab + ']').tab('show');
		//	}
		//	else
		//	{
		//		// Set the first tab if cookie do not exist
		//		$('a[data-toggle="tab"]:first').tab('show');
		//	}
		//});
	</script>
</body>
</html>
