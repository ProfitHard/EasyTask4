<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTask.aspx.cs" Inherits="EasyTask4.PL.AddTask" Async="true" %>
<%@ Register Assembly="DevExpress.Web.v24.2, Version=24.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Task</title>
    <link href="https://cdn.devexpress.com/ASPNet/DevExpress.Web.v24.2/css/dx.common.css" rel="stylesheet" />
    <link href="https://cdn.devexpress.com/ASPNet/DevExpress.Web.v24.2/css/dx.light.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Add New Task</h2>

            <dx:ASPxLabel ID="lblTitle" runat="server" Text="Title:"></dx:ASPxLabel>
            <dx:ASPxTextBox ID="txtTitle" runat="server"></dx:ASPxTextBox><br />

            <dx:ASPxLabel ID="lblDescription" runat="server" Text="Description:"></dx:ASPxLabel>
            <dx:ASPxTextBox ID="txtDescription" runat="server" TextMode="MultiLine" Height="100px" Width="300px"></dx:ASPxTextBox><br />

<dx:ASPxLabel ID="lblStatus" runat="server" Text="Status:"></dx:ASPxLabel>
<dx:ASPxComboBox ID="cbStatus" runat="server" Placeholder="Select Status">
</dx:ASPxComboBox>

            <dx:ASPxLabel ID="lblCreatedBy" runat="server" Text="Created By:"></dx:ASPxLabel>
            <dx:ASPxComboBox ID="cbCreatedBy" runat="server"></dx:ASPxComboBox><br />

            <dx:ASPxLabel ID="lblAssignedTo" runat="server" Text="Assigned To:"></dx:ASPxLabel>
            <dx:ASPxComboBox ID="cbAssignedTo" runat="server"></dx:ASPxComboBox><br />

            <dx:ASPxButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"></dx:ASPxButton>
            <dx:ASPxButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"></dx:ASPxButton>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.devexpress.com/ASPNet/DevExpress.Web.v24.2/js/dx.all.js"></script>
</body>
</html>