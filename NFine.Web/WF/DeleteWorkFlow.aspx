<%@ Page Title="" Language="C#" MasterPageFile="SDKComponents/Site.Master" AutoEventWireup="true" CodeBehind="DeleteWorkFlow.aspx.cs" Inherits="CCFlow.WF.DeleteWorkFlow" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="./Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script language="JavaScript" src="./Comm/JScript.js" type="text/javascript" ></script>
    <link href="Comm/Style/Table.css" rel="stylesheet" type="text/css" />
    <link href="Comm/Style/Table0.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" >
        function OnChange(ctrl) {

            //        var text = ctrl.options[ctrl.selectedIndex].text;
            //        var user = text.substring(0, text.indexOf('='));
            //        var nodeName = text.substring(text.indexOf('>') + 1, 1000);
            //        var objVal = '您好' + user + ':';
            //        objVal += "  \t\n ";
            //        objVal += "  \t\n ";
            //        objVal += "   您处理的 “" + nodeName + "” 工作有错误，需要您重新办理． ";
            //        objVal += "\t\n   \t\n 礼! ";
            //        objVal += "  \t\n ";
            //        document.getElementById('ContentPlaceHolder1_ReturnWork1_Pub1_TB_Doc').value = objVal;
        }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style=" text-align:left; width:100%">
<caption>您好:<%=BP.WF.Glo.GenerUserImgSmallerHtml(BP.Web.WebUser.No,BP.Web.WebUser.Name) %></caption>
<tr>
<td>

  <fieldset>
            <legend>工作删除</legend>
<div align="center" >
            <div  align="center" style='height:30px;'  >
               <uc2:toolbar ID="ToolBar1" runat="server" />
            </div>
            <div >
                <uc1:pub ID="Pub1" runat="server" />
            </div>
</div>
            </fieldset>
 </td>
</tr>
</table>
</asp:Content>




           

