<%@ Page Title="" Language="C#" MasterPageFile="~/WF/App/Amaz/Site.Master" AutoEventWireup="true" CodeBehind="CC.aspx.cs" Inherits="CCFlow.WF.App.Amaz.CC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" href="assets/css/amazeui.min.css"/>
    <link rel="stylesheet" href="assets/css/admin.css"/>
    <script type="text/javascript" src="assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="assets/js/amazeui.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%
    string ShowType = this.Request.QueryString["ShowType"];
    if (ShowType == null)
        ShowType = "0";
    
    //获取抄送工作。
    System.Data.DataTable dt = null;
    if (ShowType == "0")
    {
        /*未读的抄送*/
        dt = BP.WF.Dev2Interface.DB_CCList_UnRead(BP.Web.WebUser.No);
    }
    if (ShowType == "1")
    {
        /*已读的抄送*/
        dt = BP.WF.Dev2Interface.DB_CCList_Read(BP.Web.WebUser.No);
    }
    if (ShowType == "2")
    {
        /*删除的抄送*/
        dt = BP.WF.Dev2Interface.DB_CCList_Delete(BP.Web.WebUser.No);
    }
    
   // 输出结果
   %>
<div class="admin-content">
        <div class="am-cf am-padding">
			<div class="am-fl am-cf">
				<strong class="am-text-primary am-text-lg">抄送</strong> / <small>列表</small>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <small>[<a href='CC.aspx?ShowType=0'>未读</a>][<a href='CC.aspx?ShowType=1'>已读</a>][<a href='CC.aspx?ShowType=2'>删除</a>]</small>
			</div>
		</div>
        <div class="am-g">
			<div class="am-u-sm-12">
				<form class="am-form">
					<table class="am-table am-table-striped am-table-hover table-main">
						<thead>
							<tr>
								<th class='table-title'>抄送人</th>
								<th class='table-title'>标题</th>
								<th class='table-title'>流程</th>
								<th class='table-title'>发起时间</th>
								<th class='table-title'>详细信息</th>
							</tr>
						</thead>
						<tbody>
						 <%
                            foreach (System.Data.DataRow dr in dt.Rows)
                            {
                                string workid = dr["WorkID"].ToString();
                                string fid = dr["FID"].ToString();
                                string fk_flow = dr["FK_Flow"].ToString();
                                string fk_node = dr["FK_Node"].ToString();

                                string url = "/WF/WFRpt.aspx?FK_Flow="+fk_flow+"&FK_Node="+fk_node+"&WorkID="+workid+"&FID="+fid;
        
                                %>
                                <tr>
                                <td nowrap="nowrap"><%=dr["Rec"]%></td>
                                <td nowrap="nowrap"><%=dr["FlowName"]%></td>
                                <td nowrap="nowrap"><%=dr["RDT"]%></td>
                                <td nowrap="nowrap"><%=dr["NodeName"]%></td>
                                <td nowrap="nowrap"><a href='<%=url %>' target="_blank" >详细</a></td>
                                </tr>

                                <tr>
                                <td colspan=5 nowrap="nowrap">
                                标题：<%=dr["Title"] %>
                                <hr />
                                抄送内容:
                                <hr>
        
                                <%=dr["Doc"]%>
        
                                </td>

                                </tr>

                           <% } %> 
						</tbody>
					</table>
				</form>
			</div>
        </div>
</div>
</asp:Content>
