<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="CCFlow.WF.Messages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript">
    function SelectAll() {
        var arrObj = document.all;
        if (document.forms[0].checkedAll.checked) {
            for (var i = 0; i < arrObj.length; i++) {
                if (typeof arrObj[i].type != "undefined" && arrObj[i].type == 'checkbox') {
                    arrObj[i].checked = true;
                }
            }
        } else {
            for (var i = 0; i < arrObj.length; i++) {
                if (typeof arrObj[i].type != "undefined" && arrObj[i].type == 'checkbox')
                    arrObj[i].checked = false;
            }
        }
    }

    function Del() {

        if (window.confirm('您确定要删除吗？') == false)
            return;
    }

   
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%
    string msgType = this.Request.QueryString["MsgType"];
    if (string.IsNullOrEmpty(msgType))
        msgType = "0";
   // string sql = "SELECT * FROM Sys_SMS WHERE EmailSta=" + msgType+" AND SendTo='"+BP.Web.WebUser.No+"'";
    string sql = "SELECT * FROM Sys_SMS WHERE  1=1";
    System.Data.DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
    
    %>
<table width="100%;">
<caption class="CaptionMsg" >我的消息 </caption>
<tr>
<th style="width:5px;"><input type="checkbox"  id='checkedAll' onclick='SelectAll()' /></th>
<th>标题</th>
<th>发送日期</th>
<th>发送人</th>
</tr>

<%
   int idx = 0;
   bool isTr = false;
   foreach (System.Data.DataRow dr in dt.Rows)
   {
       string tr="<tr>";
       if (isTr)
           tr = "<tr bgcolor=AliceBlue>";
       isTr=!isTr;
       %>
       <%=tr %>
       <td><input type="checkbox" id="CB_<%=dr["MyPK"]%>" onclick="SelectAll()" /></td>
       <td><%=dr[ BP.WF.SMSAttr.EmailTitle] %></td>
       <%--<td><%=dr[ BP.WF.SMSAttr.EmailDoc] %></td>--%>
       <td><%=dr[ BP.WF.SMSAttr.RDT] %></td>
       <td><%=dr[ BP.WF.SMSAttr.Sender] %></td>
       </tr>
<%
   }
 %>
</table>

 <input type=button id="Btn_Del"  onclick="Del()"  value="删除" />
 <input type=button id="Btn_Read"  value="标记已读" />
 <input type=button id="Btn_UnRead"  value="标记未读" />

</asp:Content>
