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
		<div class="container">
			<h1>Bootstrap Tab Test Page</h1>
			<p>
				<nac:BootstrapTabs ID="BootstrapTabs1" runat="server">
					<nac:Tab ID="Tab1" TitleText="Tab 1" Active="true" runat="server">
						Raw denim you probably haven't heard of them jean shorts Austin. Nesciunt tofu stumptown aliqua, retro synth master cleanse. Mustache cliche tempor, williamsburg carles vegan helvetica. Reprehenderit butcher retro keffiyeh dreamcatcher synth. Cosby sweater eu banh mi, qui irure terry richardson ex squid. Aliquip placeat salvia cillum iphone. Seitan aliquip quis cardigan american apparel, butcher voluptate nisi qui.			
					</nac:Tab>
					<nac:Tab ID="Tab2" TitleText="Tab 2" runat="server">
						<div class="form-horizontal" role="form">
							<asp:Label ID="Label1" runat="server" Text="Label" AssociatedControlID="TextBox1"></asp:Label>
							<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
							<p class="">
								<asp:LinkButton
									runat="server"
									CommandName="Update"
									CssClass="btn btn-primary"><i class="glyphicon glyphicon-cloud-upload" ></i> Do something on the server (post back)</asp:LinkButton>
							</p>
						</div>
						<p>Food truck fixie locavore, accusamus mcsweeney's marfa nulla single-origin coffee squid. Exercitation +1 labore velit, blog sartorial PBR leggings next level wes anderson artisan four loko farm-to-table craft beer twee. Qui photo booth letterpress, commodo enim craft beer mlkshk aliquip jean shorts ullamco ad vinyl cillum PBR. Homo nostrud organic, assumenda labore aesthetic magna delectus mollit. Keytar helvetica VHS salvia yr, vero magna velit sapiente labore stumptown. Vegan fanny pack odio cillum wes anderson 8-bit, sustainable jean shorts beard ut DIY ethical culpa terry richardson biodiesel. Art party scenester stumptown, tumblr butcher vero sint qui sapiente accusamus tattooed echo park.			</p>
					</nac:Tab>
				</nac:BootstrapTabs>
			</p>
			<asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
			<br />
			<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
		</div>
	</form>
</body>
</html>
