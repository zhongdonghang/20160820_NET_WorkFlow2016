<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="CCFlow.WF.App.Amaz.Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ccflow的Amaze UI模式</title>
    <link rel="stylesheet" href="assets/css/amazeui.min.css"/>
    <link rel="stylesheet" href="assets/css/admin.css"/>
    <script type="text/javascript" src="assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="assets/js/amazeui.min.js"></script>
</head>
<body>
    <header class="am-topbar admin-header" stylr="position:absolute; top: 10px; left: 10px">
        <div class="am-topbar-brand">
            <strong>CCFlow</strong> <small>AmazeUI模式</small>
        </div>
        <button class="am-topbar-btn am-topbar-toggle am-btn am-btn-sm am-btn-success am-show-sm-only"><span class="am-sr-only">
        </span> <span class="am-icon-bars"></span></button>
        <div class="am-collapse am-topbar-collapse" id="topbar-collapse">
            <ul class="am-nav am-nav-pills am-topbar-nav am-topbar-right admin-header-list">
                <li><a href="Start.aspx" class="am-cf" target="right"><span class="am-icon-pencil-square-o"></span> 发起<span class="am-badge am-badge-warning"></span></a></li>
                <li><a href="Todolist.aspx" target="right"><span class="am-icon-bookmark"></span> 待办<span class="am-badge am-badge-warning"><%=BP.WF.Dev2Interface.Todolist_EmpWorks %></span></a></li>
                <li><a href="Runing.aspx" target="right"><span class="am-icon-th"></span> 在途<span class="am-badge am-badge-warning"><%=BP.WF.Dev2Interface.Todolist_Runing %></span></a></li>
                <li><a href="javascript:;"><span class="am-icon-user"></span> <%=BP.Web.WebUser.Name %>/<%=BP.Web.WebUser.No %> <span class="am-badge am-badge-warning"></span></a></li>
                <li class="am-hide-sm-only"><a href="Login.aspx" target="right"> <span class="am-icon-power-off"></span>重新登录</a></li>
            </ul>
        </div>
    </header>
</body>
</html>
