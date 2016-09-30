<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeBehind="NodeAccepterRole.aspx.cs" Inherits="CCFlow.WF.Admin.FlowNodeAttr.NodeAccepterRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Comm/JScript.js" type="text/javascript"></script>

    <style  type="text/css">
    li
    {
          font-size:9px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%
   // int nodeID = int.Parse(this.Request.QueryString["FK_Node"]);
    
    int nodeID = 101; // int.Parse(this.Request.QueryString["FK_Node"]);
    BP.WF.Node nd = new BP.WF.Node(nodeID);
%>
<table border="0" width="100%">
<caption>节点[<%=nd.Name %>]接受人规则设置</caption>

<!-- ===================================  01.按当前操作员所属组织结构逐级查找岗位 -->
<tr>
<th ><asp:RadioButton ID="RB_0" Text="01.按当前操作员所属组织结构逐级查找岗位"  GroupName="xxx" runat="server" />  <div style="float:right"> 参数:  <a href="javascript:WinOpen('/WF/Comm/RefFunc/Dot2DotSingle.aspx?EnsName=BP.WF.Template.Selectors&EnName=BP.WF.Template.Selector&AttrKey=BP.WF.Template.NodeStations&NodeID=<%=nd.NodeID %>&r=1005101248&1=StaGrade&ShowWay=StaGrade')">设置与更改岗位</a> | <a href='http://ccbpm.mydoc.io' target='_blank'><img src='/WF/Img/Help.png' style=" vertical-align:middle">帮助</a></div> </th>
<% if ((int)nd.HisDeliveryWay == 0) this.RB_0.Checked = true;  %>
</tr>

<tr>
<td class="BigDoc">

 <ul>
 <li>该方式是系统默认的方式，也是常用的方式，系统可以智能的寻找您需要的接受人，点击右上角设置岗位。</li>
 <li>在寻找接受人的时候系统会考虑当前人的部门因素，A发到B，在B节点上绑定N个岗位，系统首先判断当前操作员部门内是否具有该岗位集合之一，如果有就投递给他，没有就把自己的部门提高一个级别，在寻找，依次类推，一直查找到最高级，如果没有就抛出异常。</li>
 <li>比如：一个省机关下面有n个县，n个市，n个县. n个所. 一个所员受理人员的业务，只能让自己的所长审批，所长的业务只能投递到本区县的相关业务部分审批，而非其它区县业务部分审批。</li>
 </ul>
</td>
</tr>

<!-- ===================================  02.按节点绑定的部门计算 -->
<tr>
<th ><asp:RadioButton ID="RB_1" Text="02.按节点绑定的部门计算"  GroupName="xxx" runat="server" />  <div style="float:right">参数:  <a href="javascript:WinOpen('/WF/Comm/RefFunc/Dot2DotSingle.aspx?EnsName=BP.WF.Template.Selectors&EnName=BP.WF.Template.Selector&AttrKey=BP.WF.Template.NodeDepts&NodeID=<%=nd.NodeID %>&r=1005101248&1=StaGrade&ShowWay=StaGrade')">设置与更改部门</a></div></th>
<% if ((int)nd.HisDeliveryWay == 1) this.RB_1.Checked = true;  %>
</tr>

<tr>
<td class="BigDoc">

<ul>
<li>该部门下所有的人员都可以处理该节点的工作。</li>
</ul>
</td>
</tr>



<!-- ===================================  03.按设置的SQL获取接受人计算 -->
<tr>
<th ><asp:RadioButton ID="RB_2" Text="03.按设置的SQL获取接受人计算"  GroupName="xxx" runat="server" /> <div style="float:right">参数:  请在文本框里里输入SQL.</div> </th>
<% if ((int)nd.HisDeliveryWay == 2) this.RB_2.Checked = true;  %>
</tr>

<tr>
<td class="BigDoc">
 

<asp:TextBox ID="TB_2" runat="server" Width="98%" Rows=3 Height="63px" ></asp:TextBox>
<ul>
<li>该SQL是需要返回No,Name两个列，分别是人员编号,人员名称。</li>
<li>该配置也适合于</li>
</ul>
</td>
</tr>



<!-- ===================================  04.按节点绑定的人员计算 -->
<tr>
<th ><asp:RadioButton ID="RB_3" Text="04.按节点绑定的人员计算"  GroupName="xxx" runat="server" /> <div style="float:right">参数: <a href="javascript:WinOpen('/WF/Comm/RefFunc/Dot2DotSingle.aspx?EnsName=BP.WF.Template.Selectors&EnName=BP.WF.Template.Selector&AttrKey=BP.WF.Template.NodeEmps&NodeID=<%=nd.NodeID %>&r=1005101248&1=StaGrade&ShowWay=StaGrade')">设置与更改处理人</a></div></th>
<% if ((int)nd.HisDeliveryWay == 3) this.RB_3.Checked = true;  %>
</tr>

<tr>
<td class="BigDoc">

<ul>
<li>绑定的所有的人员，都可以处理该节点的工作。</li>
</ul>

</td>
</tr>



<!-- ===================================  05.由上一节点发送人通过“人员选择器”选择接受人 -->
<tr>
<th ><asp:RadioButton ID="RB_4" Text="05.由上一节点发送人通过“人员选择器”选择接受人"  GroupName="xxx" runat="server" /> <div style="float:right">参数:  <a href="javascript:WinOpen('/WF/Comm/RefFunc/UIEn.aspx?EnName=BP.WF.Template.Selector&PK=<%=nd.NodeID %>')">设置处理人可以选择的范围</a></div> </th>
<% if ((int)nd.HisDeliveryWay == 4) this.RB_4.Checked = true;  %>
</tr>

<tr>
<td class="BigDoc">

<ul>
<li>绑定的所有的人员，都可以处理该节点的工作。</li>
</ul>
</td>
</tr>




<!-- =================================== 是否可以分配工作  -->
<tr>
<th>
    <asp:CheckBox ID="CB_IsSSS"  Text="是否可以分配工作？" runat="server" />
    </th>
</tr>
<tr>
<td>
<ul>
<li>该属性是对于该节点上有多个人处理有效。 </li>
<li>比如:A,发送到B,B节点上有张三，李四，王五可以处理，您可以指定1个或者多个人处理B节点上的工作。</li>
</ul>
</td>
</tr>
 


<!-- =================================== 是否启用自动记忆功能  -->
<tr>
<th>
    <asp:CheckBox ID="CB_IsRememme"  Text="是否启用自动记忆功能？" runat="server" />
    </th>
</tr>
<tr>
<td>
<ul>
<li>该属性是对于该节点上有多个人处理有效。 </li>
<li>比如:A,发送到B,B节点上有张三，李四，王五可以处理，这次你把工作分配给李四，如果设置了记忆，那么ccbpm就在下次发送的时候，自动投递给李四，让然您也可以重新分配。</li>
</ul>
</td>



<!-- =================================== 本节点接收人不允许包含上一步发送人  -->
<tr>
<th>
    <asp:CheckBox ID="CB_IsExpSender"  Text="本节点接收人不允许包含上一步发送人？" runat="server" />
    </th>
</tr>
<tr>
<td>
<ul>
<li>该属性是对于该节点上有多个人处理有效。 </li>
<li>比如:A发送到B,B节点上有张三，李四，王五可以处理，如果是李四发送的，该设置是否需要把李四排除掉。</li>
</ul>
</td>


</tr>

</table>
    <asp:Button ID="Btn_Save" runat="server" Text="保存" onclick="Btn_Save_Click" /> 
    <asp:Button ID="Btn_SaveAndClose" runat="server" Text="保存并关闭" 
        onclick="Btn_SaveAndClose_Click" />
</asp:Content>
