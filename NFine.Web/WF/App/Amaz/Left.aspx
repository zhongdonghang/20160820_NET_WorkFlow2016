<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left.aspx.cs" Inherits="CCFlow.WF.App.Amaz.Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ccflow的Amaze UI模式</title>
    <link rel="stylesheet" href="assets/css/amazeui.min.css"/>
    <link rel="stylesheet" href="assets/css/admin.css"/>
    <script type="text/javascript" src="assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="assets/js/amazeui.min.js"></script>
</head>
<body>
  <div class="" id="admin-offcanvas" style="border-right: 1px solid #cecece;">
    <div class="admin-offcanvas-bar">
      <ul class="am-list admin-sidebar-list">
            <li><a href="Start.aspx" class="am-cf" target="right"><span class="am-icon-pencil-square-o"></span> 发起</a></li>
            <li><a href="Todolist.aspx" target="right"><span class="am-icon-bookmark"></span> 待办</a></li>
            <li><a href="Runing.aspx" target="right"><span class="am-icon-th"></span> 在途</a></li>
            <li><a href="CC.aspx" target="right"><span class="am-icon-calendar"></span> 抄送</a></li>
            <li><a href="Complete.aspx" target="right"><span class="am-icon-check"></span> 完成</a></li>
            <li></li>
      </ul>
    </div>
    <div class="admin-offcanvas-bar" style="height:350px">
      <ul class="am-list admin-sidebar-list">

      </ul>
    </div>
    </div>
</body>
</html>
