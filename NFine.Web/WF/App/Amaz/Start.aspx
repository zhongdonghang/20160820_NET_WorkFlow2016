<%@ Page Title="" Language="C#" MasterPageFile="~/WF/App/Amaz/Site.Master" AutoEventWireup="true" CodeBehind="Start.aspx.cs" Inherits="CCFlow.WF.App.Amaz.Start" %>
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
				<strong class="am-text-primary am-text-lg">发起</strong> / <small>列表</small>
			</div>
		</div>
        <div class="am-g">
			<div class="am-u-sm-12">
				<form class="am-form">
					<table class="am-table am-table-striped am-table-hover table-main">
						<thead>
							<tr>
								<th class='table-title'>类型</th>
                                <th class='table-title'>流程</th>
							</tr>
						</thead>
						<tbody>
							<%
                                System.Data.DataTable dt = BP.WF.Dev2Interface.DB_GenerCanStartFlowsOfDataTable(BP.Web.WebUser.No);
							%>
                             <%
                                foreach (System.Data.DataRow dr in dt.Rows)
                                {
                               %>
                            <tr>
                                <td nowrap="nowrap"><%=dr["FK_FlowSortText"] %></td>
                                <td nowrap="nowrap"> <a target=_blank href='/WF/MyFlow.aspx?FK_Flow=<%=dr["No"] %>' ><%=dr["Name"].ToString() %> </a>  </td>
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
