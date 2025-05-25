<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Auth.aspx.cs" Inherits="EasyTask4.PL.Auth" Async="true" %>
<%@ Register Assembly="DevExpress.Web.v24.2, Version=24.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Authentication</title>
    <link href="https://cdn.devexpress.com/ASPNet/DevExpress.Web.v24.2/css/dx.common.css" rel="stylesheet" />
    <link href="https://cdn.devexpress.com/ASPNet/DevExpress.Web.v24.2/css/dx.light.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
<div>
    <dx:ASPxLabel ID="lblName" runat="server" Text="Name:" AssociatedControlID="txtName"></dx:ASPxLabel>
    <dx:ASPxTextBox ID="txtName" runat="server" Placeholder="Name"></dx:ASPxTextBox>
    
    <dx:ASPxLabel ID="lblEmail" runat="server" Text="Email:" AssociatedControlID="txtEmail"></dx:ASPxLabel>
    <dx:ASPxTextBox ID="txtEmail" runat="server" Placeholder="Email"></dx:ASPxTextBox>
    
    <dx:ASPxLabel ID="lblPassword" runat="server" Text="Password:" AssociatedControlID="txtPassword"></dx:ASPxLabel>
    <dx:ASPxTextBox ID="txtPassword" runat="server" Placeholder="Password" TextMode="Password"></dx:ASPxTextBox>
    
    <dx:ASPxButton ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
    <dx:ASPxButton ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
    
    <dx:ASPxLabel ID="lblErrorMessage" runat="server" Text="" Visible="False" ForeColor="Red"></dx:ASPxLabel>
</div>
    </form>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.devexpress.com/ASPNet/DevExpress.Web.v24.2/js/dx.all.js"></script>
</body>
</html>