<%@ Page Title="" Language="C#" MasterPageFile="~/WF/App/Amaz/Site.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="CCFlow.WF.App.Amaz.Welcome" %>
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
				<strong class="am-text-primary am-text-lg">首页</strong> / <small>工作流程管理</small>
			</div>
		</div>
		<ul class="am-avg-sm-1 am-avg-md-5 am-margin am-padding am-text-center admin-content-list ">
			<li><a href="Start.aspx" class="am-text-success"><span class="am-icon-btn am-icon-file-text"></span>
            <br />发起<br /></a></li>
			<li><a href="Todolist.aspx" class="am-text-warning"><span class="am-icon-btn am-icon-briefcase"></span>
            <br />待办<br /><%=BP.WF.Dev2Interface.Todolist_EmpWorks%></a></li>
			<li><a href="Runing.aspx" class="am-text-danger"><span class="am-icon-btn am-icon-recycle"></span>
            <br />在途<br /><%=BP.WF.Dev2Interface.Todolist_Runing%></a></li>
			<li><a href="CC.aspx" class="am-text-secondary"><span class="am-icon-btn am-icon-user-md"></span>
            <br />抄送<br /></a></li>
            <li><a href="CC.aspx" class="am-text-primary"><span class="am-icon-btn am-icon-user-md"></span>
            <br />完成<br /></a></li>
		</ul>
    </div>
</asp:Content>
