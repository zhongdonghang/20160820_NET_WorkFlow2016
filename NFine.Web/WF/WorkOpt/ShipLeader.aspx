<%@ Page Title="" Language="C#" MasterPageFile="~/WF/SDKComponents/Site.Master" AutoEventWireup="true" CodeBehind="ShipLeader.aspx.cs" Inherits="LBQZFBpm.WF.WorkOpt.ShipLeader" %>
<%@ Register src="../Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../Comm/Style/Table0.css" rel="stylesheet" type="text/css" />
    <link href="../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../Comm/JScript.js" type="text/javascript"></script>
    <script type="text/javascript">
        function SetUnEable(ctrl) {
            SetEnable(true);
        }

        function RBSameSheet(ctrl) {
            if (ctrl.checked) {
                SetEnable(false);
            }
            else {
                SetEnable(true);
            }
        }

        function SetEnable(enable) {
            var arrObj = document.all;
            for (var i = 0; i < arrObj.length; i++) {
                if (typeof arrObj[i].type != "undefined" && arrObj[i].type == 'checkbox') {
                    arrObj[i].disabled = enable;
                }
            }
        }
        function SetRBSameSheetCheck() {
            var arrObj = document.all;
            for (var i = 0; i < arrObj.length; i++) {
                if (typeof arrObj[i].type != "undefined" && arrObj[i].id.valueOf('RB_SameSheet') != -1) {
                    arrObj[i].checked = true;
                    break;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div align=center>
<table style=" text-align:left; width:600px" >
<caption>您好:<%=BP.WF.Glo.GenerUserImgSmallerHtml(BP.Web.WebUser.No,BP.Web.WebUser.Name) %>   --   请选择分管领导</caption>
<tr>
<td width="20%">
</td>

<td>
    <uc1:Pub ID="Pub1" runat="server" />
</td>
</tr>
</table>
</div>
</asp:Content>
