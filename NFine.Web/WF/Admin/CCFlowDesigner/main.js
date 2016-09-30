/// <reference path="../../Scripts/designer.utility.js" />
/// <reference path="../../Scripts/EasyUIUtility.js" />

var currentFlow = null;
var Action_NodeP = "NodeP"; /// 节点属性
var Action_StaDef = "StaDef"; /// 节点岗位
var Action_Dir = "Dir"; /// 设置方向条件
var Action_MapDefFixModel = "MapDefFixModel"; /// 节点表单设计-傻瓜
var Action_MapDefFreeModel = "MapDefFreeModel"; /// 节点表单设计-自由
var Action_FormFixModel = "FormFixModel"; /// 表单设计-傻瓜
var Action_FormFreeModel = "FormFreeModel"; /// 表单设计-自由
var Action_FrmLib = "FrmLib"; /// 表单库
var Action_Save = "Save";
var Action_Login = "Login";
var Action_ToolBox = "ToolBox";
var Action_FlowSortP = "FlowSortP"; /// 目录权限
var Action_FlowP = "FlowP"; /// 流程属性
var Action_FlowRun = "FlowRun"; /// 运行流程
var Action_FlowCheck = "FlowCheck"; /// 流程检查
var Action_FlowRpt = "FlowRpt"; /// 报表定义
var Action_FlowFrms = "FlowFrms"; /// 独立表单
var Action_FlowDel = "FlowDel";
var Action_FlowNew = "FlowNew";
var Action_FlowExp = "FlowExp";
var Action_FlowOpen = "FlowOpen";
var Action_Help = "Help";

var WindowModel_Dialog = 0;
var WindowModel_Window = 1;
var WindowModel_Max = 2;
/* the current flowid 
,which changed when selected from flowtree or tabpage selectionchanged
*/
var fk_flow = "";
var deleting_fk_flow = null;
var BPMHost = window.location.protocol + "//" + window.location.hostname + ":" + window.location.port;

function load(completed) {
    ajaxService('flow', 'GetFlowTree', '', function (data) {
        jdata = $.parseJSON(data);
        if (jdata) {
            $('#flowTree').tree({
                data: jdata
            });
        }
        else {
        }
        completed();

    }, null, function (msg) {
        completed();
        alert(msg);
    });

    ajaxService('flow', 'GetFormTree', '', function (data) {
        jdata = $.parseJSON(data);
        if (jdata) {
            $('#formTree').tree({
                data: jdata
            });
        }
        else {
        }
    }, null, function (msg) {
        alert(msg);
    });
    
    bindAction();
}
function bindAction() {

    $('#flowTree').tree({
        // openFlow
        onDblClick: function (node) {
            if (node.attributes.IsParent == '0') {
                openFlow(node.text, node.id);
            }
        }
        // contextMenu
        , onContextMenu: function (e, node) {
            e.preventDefault();
            $('#flowTree').tree('select', node.target);

            var div = null;
            if (node.attributes.IsRoot == '1') {
                div = $('#mFlowRoot');
            }
            if (node.attributes.IsParent == '1') {
                div = $('#mFlowSort');
            } else {
                div = $('#mFlow');
            };

            div.menu('show', {
                left: e.pageX,
                top: e.pageY
            });
        }
    });

    $('#formTree').tree({
        // openForm
        onDblClick: function (node) {
            if (node.attributes.IsParent == '0') {
                // TO DO : open the form in new page in browser
                alert("will open one form on in new page in browser");
            }
        }
        // contextMenu
        , onContextMenu: function (e, node) {
            e.preventDefault();
            $('#flowTree').tree('select', node.target);

            var div = null;
            if (node.attributes.IsRoot == '1') {
                div = $('#mFormRoot');
            }
            if (node.attributes.IsParent == '1') {
                div = $('#mFormSort');
            } else {
                div = $('#mForm');
            };

            div.menu('show', {
                left: e.pageX,
                top: e.pageY
            });
        }
    });

    // using the async confirm dialog
    $('#designerArea').tabs({
        // tabPage close
        onBeforeClose: function (title, index) {
            var target = this;

            //todo:此处需要增加额外的逻辑判断，如果是确认删除已经打开的流程，则不再弹出确认关闭对话框，直接关闭
            if (deleting_fk_flow && $(target).tabs('getTab', index)[0].id == deleting_fk_flow) {
                return true;
            }

            $.messager.confirm('确认', '确定退出 ' + title, function (r) {
                if (r) {
                    var opts = $(target).tabs('options');
                    var bc = opts.onBeforeClose;
                    opts.onBeforeClose = function () {

                        // allowed to close now
                    };
                    $(target).tabs('close', index);
                    opts.onBeforeClose = bc;  // restore the event function
                }
            });
            return false; // prevent from closing
        },
        // tabPage Selection Changed 
        onSelect: function (title, index) {
            // the current flow
            // 每个流程的no存储在所在tab的id中 
            var t = $(this).tabs('getTab', index);
            //todo:t[0].id保存流程编号，可能还需要再商榷，因为表单设计器之前打算也在designerArea区域打开编辑，这样t[0].id只保存编号就不能分辨出当前打开的是流程设计还是表单设计
            if (t.length > 0 && t[0].id) {
                fk_flow = t[0].id;
                if (fk_flow != null && fk_flow.length > 0) {
                    var tbIframe = $(t[0]).children(':first-child');
                    if (tbIframe != null && tbIframe.length == 1) {
                        var ff = tbIframe[0];
                        if (ff != null && ff.contentWindow != null) {
                            var tmp = ff.contentWindow.f;
                            currentFlow = tmp;
                        }
                    }
                }
            }
            else {
                //转到默认页时，当前流程置为null
                fk_flow = null;
                currentFlow = null;
            }
        }
    });
}

// 修改当前流程
// 1.流程打开
// 2.选项卡切换
// 3.流程重复打开
function openFlow(title, flowId) {
    fk_flow = flowId;
    if ($('#designerArea').tabs('exists', title)) {
        $('#designerArea').tabs('select', title);
    } else {
        var url = "Designer.htm?FK_Flow=" + fk_flow;
        var content = '<iframe scrolling="auto" frameborder="0" id="flow' + fk_flow
        + '"  name="flow' + fk_flow
        + '"  src="'
        + '" style="width:100%;height:100%;"></iframe>';

        $('#designerArea').tabs('add', {
            id: flowId,
            title: title,
            content: content,
            closable: true
        });

        $('#flow' + fk_flow).attr('src', url);
    }
}

function showFlow() {
    var node = $('#flowTree').tree('getSelected');
    if (!node || node.attributes.IsParent != '0') return;

    openFlow(node.text, node.id);
}

function newFlow() {
    /// <summary>新建流程</summary>
    var currSortId = $('#flowTree').tree('getSelected').id; //liuxc,20150323

    OpenEasyUiDialog('NewFlow.htm?sort=' + currSortId, dgId, '新建流程', 600, 394, 'icon-new', true, function () {
        var win = document.getElementById(dgId).contentWindow;
        var newFlowInfo = win.getNewFlowInfo();

        if (newFlowInfo.flowName == null || newFlowInfo.flowName.length == 0 || newFlowInfo.flowSort == null || newFlowInfo.flowSort.length == 0) {
            $.messager.alert('错误', '信息填写不完整', 'error');
            return false;
        }

        loading();

        var ps = new Params();
        ps.push("doWhat", "NewFlow");
        ps.push("para1", newFlowInfo.flowSort + ',' + newFlowInfo.flowName + ',' + newFlowInfo.dataStoreModel + ',' + newFlowInfo.pTable + ',' + newFlowInfo.flowCode);
        ps.push("isLogin", true);

        ajaxService('flow', 'Do', ps.toJsonDataString(), function (data) {
            var jdata = $.parseJSON(data);
            if (jdata.success) {
                //在左侧流程树上增加新建的流程,并选中
                //获取新建流程所属的类别节点
                //todo:此处还有问题，类别id与流程id可能重复，重复就会出问题，解决方案有待进一步确定
                var parentNode = $('#flowTree').tree('find', newFlowInfo.flowSort);
                $('#flowTree').tree('expand', parentNode.target);

                var node = $('#flowTree').tree('append', {
                    parent: parentNode.target,
                    data: [{
                        id: jdata.data.no,
                        text: jdata.data.name,
                        attributes: { IsParent: '0' },
                        checked: false
                    }]
                });

                $('#flowTree').tree('select', $('#flowTree').tree('find', jdata.data.no).target);

                //在右侧流程设计区域打开新建的流程
                openFlow(jdata.data.name, jdata.data.no);
            }
            else {
                $.messager.alert('错误', '新建流程失败：' + jdata.msg, 'error');
            }
            loaded();
        }, null, function (msg) {
            $.messager.alert('错误', '新建流程失败：' + msg, 'error');
            loaded();
        });
    }, null);
}

function newFlowSort(isSub) {
    /// <summary>新建流程类别</summary>
    /// <param name="isSub" type="Boolean">是否是新建子级流程类别</param>
    var currSort = $('#flowTree').tree('getSelected');
    if (currSort == null || undefined == currSort.attributes.IsParent ||
                currSort.attributes.IsParent != '1' || (currSort.attributes.IsRoot == '1' && isSub == false)) return;

    var propName = (isSub ? '子级' : '同级') + '流程类别';
    OpenEasyUiSampleEditDialog(propName, '新建', null, function (val) {
        if (val == null || val.length == 0) {
            $.messager.alert('错误', '请输入' + propName + '！', 'error');
            return false;
        }

        var method = isSub ? 'NewSubFlowSort' : 'NewSameLevelFlowSort';
        var ps = new Params();
        ps.push("doWhat", method);
        ps.push("para1", currSort.id + ',' + val);
        ps.push("isLogin", true);

        loading();

        ajaxService('flow', 'Do', ps.toJsonDataString(), function (data) {
            var jdata = $.parseJSON(data);
            if (jdata.success) {
                var parentNode = isSub ? currSort : $('#flowTree').tree('getParent', currSort.target);

                $('#flowTree').tree('append', {
                    parent: parentNode.target,
                    data: [{
                        id: jdata.data,
                        text: val,
                        attributes: { IsParent: '1' },
                        checked: false,
                        state: 'open',
                        children: []
                    }]
                });

                $('#flowTree').tree('select', $('#flowTree').tree('find', jdata.data).target);
            }
            else {
                $.messager.alert('错误', '新建' + propName + '失败：' + jdata.msg, 'error');
            }
            loaded();
        }, null, function (msg) {
            $.messager.alert('错误', '新建' + propName + '失败：' + msg, 'error');
            loaded();
        });
    }, null, false, 'icon-new');
}

function editFlowSort() {
    /// <summary>编辑流程类别</summary>
    var currSort = $('#flowTree').tree('getSelected');
    if (currSort == null) return;

    OpenEasyUiSampleEditDialog('流程类别', '编辑', currSort.text, function (val) {
        if (val == null || val.length == 0) {
            $.messager.alert('错误', '请输入流程类别！', 'error');
            return false;
        }

        var ps = new Params();
        ps.push("doWhat", 'EditFlowSort');
        ps.push("para1", currSort.id + ',' + val);
        ps.push("isLogin", true);

        loading();

        ajaxService('flow', 'Do', ps.toJsonDataString(), function (data) {
            var jdata = $.parseJSON(data);
            if (jdata.success) {
                $('#flowTree').tree('update', {
                    target: currSort.target,
                    text: val
                });
            }
            else {
                $.messager.alert('错误', '编辑流程类别失败：' + jdata.msg, 'error');
            }
            loaded();
        }, null, function (msg) {
            $.messager.alert('错误', '编辑流程类别失败：' + msg, 'error');
            loaded();
        });
    }, null, false, 'icon-edit');
}

function deleteFlowSort() {
    /// <summary>删除流程类别</summary>
    var currSort = $('#flowTree').tree('getSelected');
    if (currSort == null || currSort.attributes.IsParent == undefined) return;

    OpenEasyUiConfirm("你确定要删除名称为“" + currSort.text + "”的流程类别吗？", function () {
        var ps = new Params();
        ps.push("doWhat", 'DelFlowSort');
        ps.push("para1", currSort.id);
        ps.push("isLogin", true);

        loading();

        ajaxService('flow', 'Do', ps.toJsonDataString(), function (data) {
            var jdata = $.parseJSON(data);
            if (jdata.success) {
                $('#flowTree').tree('remove', currSort.target);
            }
            else {
                $.messager.alert('错误', '删除流程类别失败：' + jdata.msg, 'error');
            }
            loaded();
        }, null, function (msg) {
            $.messager.alert('错误', '删除流程类别失败：' + msg, 'error');
            loaded();
        });
    });
}

function deleteFlowToolbar() {
    /// <summary>工具栏上的删除流程</summary>
    if (!currentFlow) return;
    var deletingFlow = currentFlow.FK_Flow;

    OpenEasyUiConfirm("你确定要删除名称为“" + currentFlow.Name + "”的流程吗？", function () {
        var ps = new Params();
        ps.push("doWhat", 'DelFlow');
        ps.push("para1", deletingFlow);
        ps.push("isLogin", true);

        loading();

        ajaxService('flow', 'Do', ps.toJsonDataString(), function (data) {
            var jdata = $.parseJSON(data);
            if (jdata.success && jdata.msg == null) {
                //如果右侧有打开该流程，则关闭
                var currFlowTab = $('#designerArea').tabs('getTab', currentFlow.Name);
                if (currFlowTab) {
                    //todo:此处因为有关闭前事件，直接这样用会弹出提示关闭框，怎么解决有待进一步确认
                    deleting_fk_flow = deletingFlow;
                    $('#designerArea').tabs('close', currentFlow.Name);
                    deleting_fk_flow = null;
                }

                var treeFlowNode = $('#flowTree').tree('find', deletingFlow);
                if (treeFlowNode) {
                    $('#flowTree').tree('remove', treeFlowNode.target);
                }
            }
            else {
                $.messager.alert('错误', '删除流程失败：' + jdata.msg, 'error');
            }
            loaded();
        }, null, function (msg) {
            $.messager.alert('错误', '删除流程失败：' + msg, 'error');
            loaded();
        });
    });
}

function deleteFlow() {
    /// <summary>删除流程</summary>
    var currFlow = $('#flowTree').tree('getSelected');
    if (currFlow == null || currFlow.attributes.IsParent != '0') return;

    OpenEasyUiConfirm("你确定要删除名称为“" + currFlow.text + "”的流程吗？", function () {
        var ps = new Params();
        ps.push("doWhat", 'DelFlow');
        ps.push("para1", currFlow.id);
        ps.push("isLogin", true);

        loading();

        ajaxService('flow', 'Do', ps.toJsonDataString(), function (data) {
            var jdata = $.parseJSON(data);
            if (jdata.success && jdata.msg == null) {
                //如果右侧有打开该流程，则关闭
                var currFlowTab = $('#designerArea').tabs('getTab', currFlow.text);
                if (currFlowTab) {
                    //todo:此处因为有关闭前事件，直接这样用会弹出提示关闭框，怎么解决有待进一步确认
                    deleting_fk_flow = currentFlow.FK_Flow;
                    $('#designerArea').tabs('close', currFlow.text);
                    deleting_fk_flow = null;
                }

                $('#flowTree').tree('remove', currFlow.target);
            }
            else {
                $.messager.alert('错误', '删除流程失败：' + jdata.msg, 'error');
            }
            loaded();
        }, null, function (msg) {
            $.messager.alert('错误', '删除流程失败：' + msg, 'error');
            loaded();
        });
    });
}

function maximizeWindow() {
    window.moveTo(0, 0)
    window.resizeTo(screen.width, window.screen.availHeight)
};

//  模式窗体编号
var dgId = "dgFrame";

function action(w) {
    switch (w) {

        case Action_Save:
            save();
            break;
        case Action_Help:
            OpenWindow("http://online.ccflow.org/", "");
            break;
        case Action_Login: // 登录。
            var url = "/WF/App/EasyUI/Login.aspx?DoType=Logout";
            OpenWindow(url, "登录", 850, 990);
            break;
        case Action_FlowRpt:
            if (!fk_flow || fk_flow.length == 0) return;

            url = "/WF/Admin/XAP/DoPort.aspx?RefNo=" + fk_flow + "&DoType=WFRpt&Lang=CH&PK=" + fk_flow;
            OpenEasyUiDialog(url, dgId, '流程报表', 1000, 618, 'icon-design');
            break;
        case Action_FlowP: // 节点属性与流程属性。
            if (!fk_flow || fk_flow.length == 0) return;

            url = "/WF/Admin/XAP/DoPort.aspx?DoType=En&EnName=BP.WF.Flow&PK=" + fk_flow + "&Lang=CH";
            OpenEasyUiDialog(url, dgId, '流程属性', 1000, 618, 'icon-config');
            break;
        case Action_FlowRun:
            if (!fk_flow || fk_flow.length == 0) return;

            url = "/WF/Admin/TestFlow.aspx?FK_Flow=" + fk_flow + "&Lang=CH";
            OpenWindow(url, "运行流程", 850, 990);
            break;
        case Action_FlowCheck:
            if (!fk_flow || fk_flow.length == 0) return;

            url = "/WF/Admin/DoType.aspx?RefNo=" + fk_flow + "&DoType=FlowCheck&Lang=CH";
            OpenEasyUiDialog(url, dgId, '流程检查', 1000, 618, 'icon-check');
            break;
        case Action_FlowExp:
            if (!fk_flow || fk_flow.length == 0) return;

            url = "/WF/Admin/XAP/DoPort.aspx?DoType=ExpFlowTemplete&FK_Flow=" + fk_flow + "&Lang=CH";
            OpenWindow(url, "导出流程", 850, 990);
            break;
    }
};

function save() {
    if (currentFlow != null)
        currentFlow.save();
}


function OpenWinByDoType(dotype, fk_flow, node1, node2) {
    var url = "";
    switch (dotype) {
        case Action_StaDef:
            url = "/WF/Admin/XAP/DoPort.aspx?DoType=StaDef&PK=" + node1 + "&Lang=CH";
            OpenEasyUiDialog(url, dgId, '节点工作岗位', 1000, 618, 'icon-station');
            break;
        case Action_FrmLib:
            url = "/WF/Admin/XAP/DoPort.aspx?DoType=FrmLib&FK_Flow=" + fk_flow + "&FK_Node=0&Lang=CH";
            OpenWindow(url, "执行", 800, 760);
            break;
        case Action_FlowFrms:
            url = "/WF/Admin/XAP/DoPort.aspx?DoType=FlowFrms&FK_Flow=" + fk_flow + "&FK_Node=" + node1 + "&Lang=CH";
            //OpenWindow(url, "执行", 800, 760);
            OpenEasyUiDialog(url, dgId, '绑定独立表单', 1000, 618, 'icon-bind');
            break;
        case Action_FlowSortP:
            url = "/WF/Admin/XAP/DoPort.aspx?DoType=En&EnName=BP.WF.FlowSort&PK=" + node1 + "&Lang=CH";
            OpenDialog(url, "执行", 600, 500);
            break;
        case Action_NodeP:  //节点属性
            url = "/WF/Admin/XAP/DoPort.aspx?DoType=En&EnName=BP.WF.Node&PK=" + node1 + "&Lang=CH";
            OpenEasyUiDialog(url, dgId, '节点属性', 1000, 618, 'icon-config');
            break;

        case Action_MapDefFixModel: // SDK表单设计。
            url = "/WF/Admin/XAP/DoPort.aspx?DoType=MapDefFixModel&FK_MapData=ND" + node1 + "&FK_Node=" + node1 + "&Lang=CH&FK_Flow=" + fk_flow;
            OpenDialog(url, "节点表单设计");
            break;
        case Action_MapDefFreeModel: // 自由表单设计。

            var fk_MapData = "ND" + node1;
            var title = "表单ID: {0} 存储表:{1} 名称:{2}";
            title = string.Format(title, fk_MapData, fk_MapData, node2);

            url = "/WF/Admin/XAP/DoPort.aspx?DoType=MapDefFreeModel&FK_MapData="
                + fk_MapData + "&FK_Node=" + node1 + "&Lang=CH&FK_Flow=" + fk_flow;

            break;
        case Action_FormFixModel: // 节点表单设计。
            url = "/WF/Admin/XAP/DoPort.aspx?DoType=MapDefFixModel&FK_MapData=" + fk_flow;

            OpenDialog(url, "节点表单设计");
            break;
        case Action_FormFreeModel: // 节点表单设计。
            url = "/WF/Admin/XAP/DoPort.aspx?DoType=MapDefFreeModel&FK_MapData=" + fk_flow;
            OpenDialog(url, "节点表单设计");
            break;
        case Action_Dir: // 方向条件。
            url = "/WF/Admin/ConditionLine.aspx?FK_Flow=" + fk_flow + "&FK_MainNode=" + node1 + "&FK_Node=" + node1 + "&ToNodeID=" + node2 + "&CondType=2&Lang=CH";
            OpenEasyUiDialog(url, dgId, '设置方向转向条件', 1000, 618, 'icon-config');
            break;
        case Action_FlowP: // 节点属性与流程属性。
            url = "/WF/Admin/XAP/DoPort.aspx?DoType=En&EnName=BP.WF.Flow&PK=" + fk_flow + "&Lang=CH";
            OpenEasyUiDialog(url, dgId, '流程属性', 1000, 618, 'icon-config');
            break;
        case Action_FlowRun:
            url = "/WF/Admin/TestFlow.aspx?FK_Flow=" + fk_flow + "&Lang=CH";
            OpenWindow(url, "运行流程", 850, 990);
            break;
        case Action_FlowCheck:
            url = "/WF/Admin/DoType.aspx?RefNo=" + fk_flow + "&DoType=FlowCheck&Lang=CH";
            OpenEasyUiDialog(url, dgId, '流程检查', 1000, 618, '');
            break;
        case Action_FlowRpt:
            url = "/WF/Admin/XAP/DoPort.aspx?RefNo=" + fk_flow + "&DoType=WFRpt&Lang=CH&PK=" + fk_flow;
            OpenEasyUiDialog(url, dgId, '流程报表', 1000, 618, '');
            break;
        default:
            alert("没有判断的url执行标记:" + dotype);
            break;
    }
};

function callback() {
    var innerWin = document.getElementById(dgId).contentWindow;
    $('#' + txtId).val(innerWin.getReturnText());
    $('#' + hiddenId).val(innerWin.getReturnValue());

};
function OpenDialog(url, title, h, w, callBack) {
    //    OpenWindowOrDialog(url, title,  WindowModel_Dialog,h,w);
    OpenEasyUiDialog(url, dgId, title, h, w, 'icon-user', true, callBack);
};


function OpenMax(url, title) {
    OpenWindowOrDialog(url, title, WindowModel_Max);
};

function OpenWindow(url, title, h, w) {
    OpenWindowOrDialog(url, title, WindowModel_Window, h, w);
};

function OpenWindowOrDialog(url, title, windowModel
    , height, width, left, top, resizable) {

    if (height == undefined || height == null) height = 0;
    if (width == undefined || width == null) width = 0;
    if (left == undefined || left == null) left = 0;
    if (top == undefined || top == null) top = 0;
    if (resizable == undefined || resizable == null) resizable = true;

    if (url.indexOf("ttp://") == -1 && url.indexOf("http") == -1)
        url = BPMHost + url;

    try {
        if (windowModel == WindowModel_Dialog) {

        }
        else if (windowModel == WindowModel_Max) {
            var parms = "left=0,top=0,height={0},width={1},resizable={2},scrollbars=yes,help=no,toolbar=no,menubar=no,scrollbars=yes,status=yes,location=no";
            width = 0 < width ? width : window.screen.width;
            height = 0 < height ? height : window.screen.height - 100; // 系统任务栏高度？？
            var resize = resizable ? "yes" : "no";
            parms = parms.format(height, width, resize);
            window.open(url, title, parms);
        }
        else {
            if (0 < height && 0 < width) {
                var parms = 'height={0},width={1},resizable=yes,help=no,toolbar =no, menubar=no, scrollbars=yes,status=yes,location=no';
                parms = parms.format(height, width);
                window.open(url, title, parms);
            }
            else
                window.open(url, '_blank');
        }
    }
    catch (e) {
    }
};