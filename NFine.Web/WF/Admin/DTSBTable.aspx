<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Admin/WinOpen.master" AutoEventWireup="true"
    CodeBehind="DTSBTable.aspx.cs" Inherits="CCFlow.WF.Admin.DTSBTable" %>

<%@ Register Src="Pub.ascx" TagName="Pub" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table
        {
            margin: 90px;
        }
        div
        {
            align: center;
        }
    </style>
    <script type="text/javascript">
        function winClose() {
            window.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"
    align='center'>
    <uc1:Pub ID="Pub1" runat="server" />
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            font-family: '宋体' !important;
        }
        table
        {
            margin: 0 auto;
            width: 100%;
        }
        #ContentPlaceHolder1_Pub1_Btn_Save
        {
            margin: 0 29%;
            color: blue;
        }
        #ContentPlaceHolder1_Pub1_Btn_Close
        {
            color: blue;
        }
    </style>
</asp:Content>
