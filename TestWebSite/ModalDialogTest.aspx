<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModalDialogTest.aspx.cs" Inherits="TestWebSite.ModalDialogTest" %>

<%@ Register Assembly="NotAClue.Web.UI.BootstrapWebControls" Namespace="NotAClue.Web.UI.BootstrapWebControls" TagPrefix="nac" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Bootstrap Modal Test Page</title>
	<link href="Content/bootstrap.css" rel="stylesheet" />
	<link href="Content/bootstrap-theme.min.css" rel="stylesheet" />
	<script src="Scripts/jquery-1.9.0.min.js"></script>
	<script src="Scripts/bootstrap.min.js"></script>
</head>
<body>
	<form id="form1" runat="server">
		<%--		<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
			<Scripts>
				<asp:ScriptReference Path="~/Scripts/jquery-1.9.0.min.js" />
				<asp:ScriptReference Path="~/Scripts/bootstrap.js" />
			</Scripts>
		</asp:ScriptManager>--%>
		<script>
		    
		    $('#myModal').modal('show');
		</script>
		<div class="container">
			<h1>Bootstrap Tab Test Page</h1>

			<!-- Button trigger modal -->
			<asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-lg" Text="Show Modal Dialog" UseSubmitBehavior="False" OnClick="Button1_Click" />

			<a href="#" class="btn btn-primary btn-lg" onclick="$('#myModal').modal('show');">
				Launch demo modal
			</a>

			<a href="#" class="btn btn-primary btn-lg"  data-toggle="modal" data-target="#myModal">
				Launch demo modal
			</a>

			<nac:BootstrapModalDialog
				ID="myModal"
				runat="server"
				ClientIDMode="Static"
				DisplayFooter="True"
				ModalSize="Small"
				FadeIn="True"
				Title="Modal Test">
				This is Bootstrap Modal Dialog.
				<nac:FooterButton runat="server" ID="CloseButton" ButtonType="Cancel" ButtonStyle="Default"><span class="glyphicon glyphicon-remove"></span>Close</nac:FooterButton>
				<nac:FooterButton runat="server" ID="SaveButton" ButtonType="OK" ButtonStyle="Primary"><span class="glyphicon glyphicon-save"></span>Save</nac:FooterButton>
			</nac:BootstrapModalDialog>
		</div>
	</form>
</body>
</html>
