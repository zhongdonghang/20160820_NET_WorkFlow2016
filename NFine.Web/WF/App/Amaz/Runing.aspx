<%@ Page Title="" Language="C#" MasterPageFile="~/WF/App/Amaz/Site.Master" AutoEventWireup="true" CodeBehind="Runing.aspx.cs" Inherits="CCFlow.WF.App.Amaz.Runing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="assets/css/amazeui.min.css"/>
    <link rel="stylesheet" href="assets/css/admin.css"/>
    <script type="text/javascript" src="assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="assets/js/amazeui.min.js"></script>
    <script type="text/javascript">
        // 撤销。
        function UnSend(appPath, fk_flow, workid) {
            if (window.confirm('您确定要撤销本次发送吗？') == false)
                return;
            var url = appPath + 'WF/Do.aspx?DoType=UnSend&WorkID=' + workid + '&FK_Flow=' + fk_flow;
            window.location.href = url;
            return;
        }
        function Press(appPath, fk_flow, workid) {
            var url = appPath + 'WF/WorkOpt/Press.aspx?WorkID=' + workid + '&FK_Flow=' + fk_flow;
            var v = window.showModalDialog(url, 'sd', 'dialogHeight: 220px; dialogWidth: 430px;center: yes; help: no');
        }
        function CopyAndStart(appPath, fk_flow, CopyFormNode, CopyFormWorkID) {
            var url = appPath + 'WF/MyFlow.aspx?CopyFormWorkID=' + CopyFormWorkID + '&CopyFormNode=' + CopyFormNode + '&FK_Flow=' + fk_flow;
            var v = window.open(url, 'sd', 'dialogHeight: 220px; dialogWidth: 430px;center: yes; help: no');
        }

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
				<strong class="am-text-primary am-text-lg">在途</strong> / <small>列表</small>
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
                                <th class='table-title'>当前处理人</th>
								<th class='table-title'>操作</th>
							</tr>
						</thead>
						<tbody>
						<%
                            //获取在途工作。
                            System.Data.DataTable dt = null;
                                if (BP.Sys.SystemConfig.AppSettings["IsAddCC"] == "1")
                                    dt = BP.WF.Dev2Interface.DB_GenerRuning();
                                else
                                    dt = BP.WF.Dev2Interface.DB_GenerRuningAndCC();
        
                            string path = this.Request.ApplicationPath;

                            foreach (System.Data.DataRow dr in dt.Rows)
                            {
                                string workid = dr["WorkID"].ToString();
                                string fk_flow = dr["FK_Flow"].ToString();
                                string nodeID = dr["FK_Node"].ToString();
                        %>
                        <tr>
                            <td nowrap="nowrap">
                                <% if ( 1==1 || dr["Type"] + "" == "RUNNING")
                                   { %>
                                <a href="/WF/WFRpt.aspx?FK_Flow=<%= dr["FK_Flow"] %>&WorkID=<%= dr["WorkID"] %>"
                                    target="_blank">
                                    <%= dr["Title"] %>
                                </a>
                                <% }
                                   else
                                   { %>
                                <a href='javascript:WinOpenCC("<%=dr["MyPk"] %> "," <%=dr["FK_Flow"] %>  ","<%=dr["FK_Node"] %> "," <%=dr["WorkID"] %> ","<%=dr["FID"] %>","<%=dr["Sta"] %>")'>
                                    <%=dr["Title"] %></a>
                                <% } %>
                            </td>

                            <td nowrap="nowrap"><%= dr["FlowName"] %></td>
                            <td nowrap="nowrap"><%= dr["RDT"] %></td>
                            <td nowrap="nowrap"><%= dr["StarterName"] %></td>
                            <td nowrap="nowrap"><%= dr["NodeName"] %></td>
                            <td nowrap="nowrap"><%= dr["TodoEmps"] %></td>
                            <% if (1 == 1 ||  dr["Type"] + "" == "RUNNING")
                               { %>
                            <td nowrap="nowrap">
                               <% if (dr["FID"].ToString() != "0")
                                  { %>
                                [<a href="/WF/DeleteWorkFlow.aspx?FK_Flow=<%= fk_flow %>&WorkID=<%= workid %>" target=_blank >删除</a>]
                                <% }
                                  else
                                  { %>

                                 [<a href="/WF/WorkOpt/UnSend.aspx?FK_Flow=<%= fk_flow %>&WorkID=<%= workid %>" target=_blank >撤销发送</a>]
                                -[<a href="javascript:CopyAndStart('<%= path %>','<%= fk_flow %>','<%= nodeID %>','<%= workid %>')" >Copy发起</a>]
                                <%} %>
                                -[<a href="javascript:Press('<%= path %>','<%= fk_flow %>','<%= workid %>')" >催办</a>]

                            </td>
                            <% }
                               else
                               {%>
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
