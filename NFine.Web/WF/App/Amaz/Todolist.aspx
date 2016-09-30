<%@ Page Title="" Language="C#" MasterPageFile="~/WF/App/Amaz/Site.Master" AutoEventWireup="true" CodeBehind="Todolist.aspx.cs" Inherits="CCFlow.WF.App.Amaz.Todolist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" href="assets/css/amazeui.min.css"/>
    <link rel="stylesheet" href="assets/css/admin.css"/>
    <script type="text/javascript" src="assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="assets/js/amazeui.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="admin-content">
        <div class="am-cf am-padding">
			<div class="am-fl am-cf">
				<strong class="am-text-primary am-text-lg">待办</strong> / <small>列表</small>
			</div>
		</div>
        <div class="am-g">
			<div class="am-u-sm-12">
				<form class="am-form">
					<table class="am-table am-table-striped am-table-hover table-main">
						<thead>
							<tr>
								<th class='table-title'>是否读取</th>
								<th class='table-title'>标题</th>
								<th class='table-title'>流程</th>
								<th class='table-title'>发起时间</th>
								<th class='table-title'>发起人</th>
								<th class='table-title'>停留节点</th>
								<th class='table-title'>类型</th>
							</tr>
						</thead>
						<tbody>
							<%
								System.Data.DataTable dt = BP.WF.Dev2Interface.DB_GenerEmpWorksOfDataTable();
								string t = DateTime.Now.ToString("MMddhhmmss");
								foreach (System.Data.DataRow dr in dt.Rows)
                                {
                                    string paras = dr["AtPara"] as string;
                                    if (paras == null)
                                    paras = "";
                                    int isRead = int.Parse(dr["IsRead"].ToString());
							%>
							<tr>
                                <% if (isRead == 0)
                                {%>
                                    <td nowrap="nowrap"><span class="am-badge am-badge-warning">未阅</span></td>
                                    <td nowrap="nowrap"><b> <a target="_blank" href='/WF/MyFlow.aspx?FK_Flow=<%=dr["FK_Flow"] %>&FK_Node=<%=dr["FK_Node"] %>&WorkID=<%=dr["WorkID"] %>&FID=<%=dr["FID"] %>&IsRead=<%=isRead%>&Paras=<%=paras %>&T=<%=t %>' ><%=dr["Title"].ToString()%> </a>  </b></td>
                                <% }
                                 else
                                { %>
                                    <td nowrap="nowrap"><span class="am-badge am-badge-secondary">已阅</span></td>
                                    <td nowrap="nowrap"><a target="_blank" href='/WF/MyFlow.aspx?FK_Flow=<%=dr["FK_Flow"] %>&FK_Node=<%=dr["FK_Node"] %>&WorkID=<%=dr["WorkID"] %>&FID=<%=dr["FID"] %>&IsRead=<%=isRead%>&Paras=<%=paras %>&T=<%=t %>' ><%=dr["Title"].ToString()%> </a>  </td>
                                <%} %>
                                <td nowrap="nowrap"><%=dr["FlowName"]%></td>
                                <td nowrap="nowrap"><%=dr["RDT"]%></td>
                                <td nowrap="nowrap"><%=dr["StarterName"]%></td>
                                <td nowrap="nowrap"><%=dr["NodeName"]%></td>
								<% if (paras.Contains("IsCC")) { %>
                                <td nowrap="nowrap">抄送</td>
                                <% } else { %>
                                <td nowrap="nowrap">发送</td>
                                <%} %>
							</tr>
							<%
								}%>
						</tbody>
					</table>
				</form>
			</div>
		</div>
 </div>
</asp:Content>
