<%@ Page Title="" Language="C#" MasterPageFile="~/WF/App/Amaz/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CCFlow.WF.App.Amaz.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="assets/css/amazeui.min.css"/>
    <link rel="stylesheet" href="assets/css/admin.css"/>
    <script type="text/javascript" src="assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="assets/js/amazeui.min.js"></script>
            <script type="text/javascript">
                function reLoad() {
                   window.parent.location = "Default.aspx";
            }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="admin-content">
    <div class="am-cf am-padding">
      <div class="am-fl am-cf"><strong class="am-text-primary am-text-lg">用户登录</strong> / <small>ccflow流程管理</small></div>
    </div>
    <form class="am-form">
					<table class="am-table am-table-striped am-table-hover table-main">
						<thead>
							<tr>
								<th class='table-title'></th>
                                <th class='table-title'></th>
							</tr>
						</thead>
						<tbody>
                        <tr><td></td><td></td></tr>
                            <tr>
                                <td nowrap="nowrap"><label for="user-name" class="am-u-sm-3 am-form-label">姓名 / Name</label><asp:TextBox ID="name" runat="server" Text=""></asp:TextBox></td>
                             </tr>
                             <tr>
                                <td nowrap="nowrap"><label for="user-pwd" class="am-u-sm-3 am-form-label">密码 / Pwd</label><input type="password" id="pwd" runat="server"></td>
                            </tr>
						</tbody>
					</table>
				</form>
        <div id="btn" style="margin-left:280px">
              <asp:Button ID="Button2" class="am-btn am-btn-primary" runat="server" Text=" 登录 " onclick="Button1_Click" OnClientClick="reLoad()" />
          </div>
      </div>

  </div>

</asp:Content>
