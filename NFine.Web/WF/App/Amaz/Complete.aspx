<%@ Page Title="" Language="C#" MasterPageFile="~/WF/App/Amaz/Site.Master" AutoEventWireup="true" CodeBehind="Complete.aspx.cs" Inherits="CCFlow.WF.App.Amaz.Complete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" href="assets/css/amazeui.min.css"/>
    <link rel="stylesheet" href="assets/css/admin.css"/>
    <script type="text/javascript" src="assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="assets/js/amazeui.min.js"></script>
    <script type="text/javascript">
        function WinOpenCC(ccid, fk_flow, fk_node, workid, fid, sta) {
            var url = '';
            if (sta == '0') {
                url = 'WF/Do.aspx?DoType=DoOpenCC&FK_Flow=' + fk_flow + '&FK_Node=' + fk_node + '&WorkID=' + workid + '&FID=' + fid + '&Sta=' + sta + '&MyPK=' + ccid + "&T=" + dateNow;
            }
            else {
                url = 'WF/WorkOpt/OneWork/Track.aspx?FK_Flow=' + fk_flow + '&FK_Node=' + fk_node + '&WorkID=' + workid + '&FID=' + fid + '&Sta=' + sta + '&MyPK=' + ccid + "&T=" + dateNow;
            }
            //window.parent.f_addTab("cc" + fk_flow + workid, "抄送" + fk_flow + workid, url);
            var newWindow = window.open(url, 'z');
            newWindow.focus();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="admin-content">
        <div class="am-cf am-padding">
			<div class="am-fl am-cf">
				<strong class="am-text-primary am-text-lg">已完成</strong> / <small>列表</small>
			</div>
		</div>
         <div class="am-g">
			<div class="am-u-sm-12">
				<form class="am-form">
					<table class="am-table am-table-striped am-table-hover table-main">
						<thead>
							<tr>
								<th class='table-title'>标题</th>
								<th class='table-title'>流程</th>
								<th class='table-title'>发起时间</th>
								<th class='table-title'>发起人</th>
								<th class='table-title'>停留节点</th>
								<th class='table-title'>操作</th>
							</tr>
						</thead>
						<tbody>
						<%
                            System.Data.DataTable dt = null;
                            try
                            {
                                if (BP.Sys.SystemConfig.AppSettings["IsAddCC"] == "1")
                                    dt = BP.WF.Dev2Interface.DB_FlowComplete();
                                else
                                    dt = BP.WF.Dev2Interface.DB_FlowCompleteAndCC();

                            }
                            catch (Exception ex)
                            {
                                dt = BP.WF.Dev2Interface.DB_FlowComplete();

                            }
                            string path = this.Request.ApplicationPath;
                            foreach (System.Data.DataRow dr in dt.Rows)
                            {
                                string workid = dr["WorkID"].ToString();
                                string fk_flow = dr["FK_Flow"].ToString();
                        %>
                        <tr>
                            <td nowrap="nowrap">
                                <% if (dr["Type"] + "" == "RUNNING")
                                   { %>
                                <a href="/WF/WFRpt.aspx?FK_Flow=<%=dr["FK_Flow"] %>&WorkID=<%=dr["WorkID"] %>" target="_blank">
                                    <%=dr["Title"]%>
                                </a>
                                <% }
                                   else
                                   { %>
                                <a href='javascript:WinOpenCC("<%=dr["MyPk"] %> "," <%=dr["FK_Flow"] %>  ","<%=dr["FK_Node"] %> "," <%=dr["WorkID"] %> ","<%=dr["FID"] %>","<%=dr["Sta"] %>")'>
                                    <%=dr["Title"] %></a>
                                <% } %>
                            </td>
                            <td nowrap="nowrap">
                                <%=dr["FlowName"]%>
                            </td>
                            <td nowrap="nowrap">
                                <%=dr["RDT"]%>
                            </td>
                            <td nowrap="nowrap">
                                <%=dr["StarterName"]%>
                            </td>
                            <td nowrap="nowrap">
                                <%=dr["NodeName"]%>
                            </td>
                            <% if (dr["Type"] + "" == "RUNNING")
                               { %>
                            <td nowrap="nowrap">
                                <a href="/WF/MyFlow.aspx?FK_Flow=<%= dr["FK_Flow"] %>&CopyFormWorkID=<%= dr["WorkID"] %>&CopyFormNode=<%= dr["FK_Node"] %>"
                                    target="_blank">Copy发起流程</a>
                            </td>
                            <% }
                               else
                               { %>
                            <td nowrap="nowrap">
                            </td>
                            <% } %>
                        </tr>
                        <% } %>
						</tbody>
					</table>
				</form>
			</div>
        </div>
    </div>
</asp:Content>
