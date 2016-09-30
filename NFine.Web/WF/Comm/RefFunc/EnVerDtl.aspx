<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Comm/RefFunc/WinOpen.Master" AutoEventWireup="true" CodeBehind="EnVerDtl.aspx.cs" Inherits="CCFlow.WF.Comm.RefFunc.EnVerDtl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%
    
    string enName = this.Request.QueryString["EnName"];
    string pkVal = this.Request.QueryString["PKVal"];

    BP.Sys.EnVerDtl dtl = new BP.Sys.EnVerDtl();
    dtl.CheckPhysicsTable();
    
    
    string sql = "select EnVer, Rec,OldVal,NewVal,AttrKey, COUNT(MyPK) AS Num from Sys_EnVerDtl WHERE EnName='" + enName + "' AND EnVer!='1'  GROUP BY EnVer ,Rec,OldVal,NewVal,AttrKey";
        
    System.Data.DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
    
    BP.Port.Dept dept=new BP.Port.Dept();
    
     %>
<table  width="100%">
<caption >历史记录修改版本记录</caption>


<% foreach (System.Data.DataRow dr in dt.Rows)
   {
       %>
        <tr> <th colspan="5"  style=" ">修改日期   <%=dr["EnVer"]%></th></tr>
       <tr>
         <td> 修改人<br /><br /><%=dr["Rec"]%></td> 
         <td>字段名称<br /><br /><%=dr["AttrKey"]%></td>
         <td>旧值<br /><br /><%=dr["OldVal"]%></td>
         <td>新值<br /><br /><%=dr["NewVal"]%></td>
       </tr>

 <% } %>
</table>
</asp:Content>
