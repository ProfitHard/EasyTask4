<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Queries.aspx.cs" Inherits="EasyTask4.PL.Queries" Async="true" %>
<%@ Register Assembly="DevExpress.Web.v24.2, Version=24.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SQL Queries</title>
    <link href="https://cdn.devexpress.com/ASPNet/DevExpress.Web.v24.2/css/dx.common.css" rel="stylesheet" />
    <link href="https://cdn.devexpress.com/ASPNet/DevExpress.Web.v24.2/css/dx.light.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxGridView ID="gridQueries" runat="server" KeyFieldName="Id" AutoGenerateColumns="False">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="Id" Caption="ID" ReadOnly="True" />
                    <dx:GridViewDataTextColumn FieldName="Title" Caption="Task Title" />
                    <dx:GridViewDataTextColumn FieldName="Description" Caption="Description" />
                    <dx:GridViewDataTextColumn FieldName="Status" Caption="Status" />
                    <dx:GridViewDataTextColumn FieldName="CreatedBy" Caption="Created By" />
                    <dx:GridViewDataTextColumn FieldName="Assignedto" Caption="Assigned to" />
                    <dx:GridViewDataDateColumn FieldName="CreationDate" Caption="Creation Date" />
                    <dx:GridViewDataDateColumn FieldName="UpdateDat" Caption="Update Dat" />
                </Columns>
                <SettingsBehavior AllowSort="True" AllowFocusedRow="True" />
                <SettingsPager PageSize="10" />
            </dx:ASPxGridView>

            <dx:ASPxButton ID="btnBack" runat="server" Text="Back to Tasks" OnClick="btnBack_Click" />
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.devexpress.com/ASPNet/DevExpress.Web.v24.2/js/dx.all.js"></script>
</body>
</html>