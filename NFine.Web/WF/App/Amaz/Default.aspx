<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CCFlow.WF.App.Amaz.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<div>
<frameset id="mainfs" rows="8%,92%" border="0" style="position:relative;">
	<frame src="Top.aspx" name="top" scrolling="no"></frame>
	<frameset id="ifs" cols="15%,85%" border="0">
		<frame src="Left.aspx" name="left" scrolling="auto"></frame>
		<frame src="Todolist.aspx" name="right" scrolling="auto"></frame>
	</frameset>
</frameset>
</div>
</html>
