﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppMenu.aspx.cs" Inherits="GMP2.GPM.AppMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统对应的菜单</title>
    <link id="appstyle" href="themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="themes/default/datagrid.css" rel="stylesheet" type="text/css" />
    <link href="themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="jquery/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="jquery/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="javascript/AppData.js" type="text/javascript"></script>
    <script src="javascript/SystemMenus.js" type="text/javascript"></script>
</head>
<body>
    <table id="menuGrid" fit="true" class="easyui-datagrid">
    </table>
    <div id="mm" class="easyui-menu" style="width: 120px;">
        <div data-options="iconCls:'icon-new'">
            <span>创建模型菜单</span>
            <div style="width: 120px;">
                <div onclick="NewFlowModel()">
                    流程式模型</div>
                <%--<div class="menu-sep"></div>--%>
                <div onclick="NewFormModel()">
                    表单式模型</div>

               <%-- <div onclick="AddFlowMenu()">
                    增加流程菜单</div>--%>
            </div>
        </div>
        <div onclick="OpenNewWindow()" data-options="iconCls:'icon-save-close'">
            打开新窗口</div>
        <div onclick="addSampleNode()" data-options="iconCls:'icon-add'">
            增加同级</div>
        <div onclick="addChildNode()" data-options="iconCls:'icon-add'">
            增加下级</div>
        <div onclick="tranUp()" data-options="iconCls:'icon-remove'">
            上移</div>
        <div onclick="tranDown()" data-options="iconCls:'icon-remove'">
            下移</div>
        <div onclick="delNode()" data-options="iconCls:'icon-cancel'">
            删除</div>
        <div onclick="editNode()" data-options="iconCls:'icon-edit'">
            修改</div>
    </div>
    <div id="DIV_toolbar">
        <a href="javascript:void(0)" id="mb1" style=" float:left;" class="easyui-menubutton" data-options="menu:'#mm1',iconCls:'icon-new'">
            创建模型菜单</a> 
        <a href="#" style="float: left;" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save-close'"
                onclick="OpenNewWindow()">打开新窗口</a>
        <a href="#" style="float: left;" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-new'"
                onclick="addSampleNode()">增加同级</a>
        <div id="mm1" style="width: 150px;">
            <div onclick="NewFlowModel()">
                流程式模型</div>
            <div onclick="NewFormModel()">
                表单式模型</div>

                <%--<div onclick="AddFlowMenu()">
                    增加流程菜单</div>--%>
        </div>
    </div>
    <div id="modelDialog">
        <div style="height: 450px; overflow: auto">
            <ul id="mainTree" class="easyui-tree" data-options="animate:true,dnd:false">
            </ul>
        </div>
    </div>
</body>
</html>
