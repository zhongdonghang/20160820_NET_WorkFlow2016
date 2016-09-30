/// <reference path="../../Scripts/json2.js" />
/// <reference path="../../Scripts/EasyUIUtility.js" />
/// <reference path="../../Scripts/designer.utility.js" />


var f,
    fk_Flow,
    work_ID,
    lastMousePoint = { x: 0, y: 0 };
var STATE_NONE = "none",
    STATE_FIGURE_CREATE = "figure_create",
    STATE_FIGURE_SELECTED = "figure_selected",
    STATE_CONNECTOR_PICK_FIRST = "connector_pick_first",
    STATE_CONNECTOR_PICK_SECOND = "connector_pick_second",
    STATE_CONNECTOR_SELECTED = "connector_selected",
    STATE_CONNECTOR_MOVE_POINT = "connector_move_point",
    STATE_SELECTING_MULTIPLE = "selecting_multiple",
    STATE_CONTAINER_SELECTED = "container_selected",
    STATE_TEXT_EDITING = "text_editing",
    state = STATE_NONE;

function load() {
    /// <summary>加载时绘制流程图/轨迹</summary>
    /// <param name="datas" Type="Object">绘制的数据，包含flow/nodes/labels/dirs，均为Array</param>

    fk_Flow = uh["FK_Flow"];
    if (!fk_Flow) fk_Flow = '001';

    work_ID = uh["WorkID"];
    if (!work_ID) work_ID = '001';

    var data = null;
    //data = "{fk_flow:'" + fk_Flow + "',workid:'" + work_ID + "'}";
    var ps = new Params();
    ps.push('fk_flow', fk_Flow);
    ps.push('workid', work_ID);
    data = ps.toJsonDataString();

    ajaxService('flow', 'GetFlowTrackJsonData', data, function (data) {
        loaded();
        var jdata = $.parseJSON(data);
        if (!jdata.success) {
            $.messager.alert('错误', jdata.msg, 'error');
        }
        else {
            f = CCFlow.load(jdata.ds, $(document).innerWidth(), $(document).innerHeight());
            if (f._initialized) {
                f.draw();
            }

            //增加节点右键菜单打开后的处理逻辑
            $('#nodeMenu').menu({
                onShow: function () {
                    if (!f.focusElement || f.focusElement.oType != ElementType.NODE) return;

                    var currTypeId = 'type';

                    for (var t in FlowNodeType) {
                        if (FlowNodeType[t] == f.focusElement.nodeType) {
                            currTypeId += t;
                            break;
                        }
                    }

                    EasyUiMenuItemsCheckOnlyOne('nodeMenu', 'nodetypes', currTypeId, 'type');
                }
            });

            //增加连线右键菜单打开后的处理逻辑
            $('#dirMenu').menu({
                onShow: function () {
                    if (!f.focusElement || f.focusElement.oType != ElementType.DIRECTION) return;

                    EasyUiMenuShowForCheckedItems('dirMenu', [{ id: 'isBrokeLine', checked: f.focusElement.lineType == DirectionUIType.Polyline }, { id: 'isCanBack', checked: f.focusElement.isCanBack}]);
                }
            });

            window.parent.currentFlow = f;
        }

    });
}

function getCurrentFlow() {
    return f;
}

function getPointForRaphael() {
    var p = $('#holder').offset();
    return { x: lastMousePoint.x - p.left, y: lastMousePoint.y - p.top };
}

function getRaphaelPoint(pageX, pageY) {
    var p = $('#holder').offset();
    return { x: pageX - p.left, y: pageY - p.top };
}

function newNode() {
    /// <summary>添加节点</summary>
    loading();

    var p = getPointForRaphael(),
    name = f.getNewElementName(ElementType.NODE),
    params = new Params();

    params.push('isLogin', true);
    params.push('param', [fk_Flow, name, NODE_ICON_DEFAULT.split('.')[0], p.x.toString(), p.y.toString(), '0']);

    ajaxService('flow', 'DoNewNode', params.toJsonDataString(), function (re) {
        if (re == 0) {
            $.messager.alert('失败', '添加节点失败!', 'error');
        }
        else {
            var node = new CCNode(re, name, p.x, p.y);
            node.add();
        }

        loaded();
    });
}

function newLabel() {
    /// <summary>添加标签</summary>
    var p = getPointForRaphael(),
    name = f.getNewElementName(ElementType.LABNOTE);

    var labNote = new CCLabNote(name, name, p.x, p.y);
    labNote.add();

    //    params = new Params();
    //    params.push('fk_flow', fk_Flow);
    //    params.push('x', p.x);
    //    params.push('y', p.y);
    //    params.push('name', name);
    //    params.push('lableId', null);
    //    ajaxService('DoNewLabel', params.toJsonDataString(), function (re) {
    //        if (re == null || isNaN(re)) {
    //            $.messager.alert('失败', '添加标签失败!', 'error');
    //            return;
    //        }
    //        var labNote = new CCLabNote(re, name, p.x, p.y);
    //        labNote.add();
    //    });
}

function newDiretion() {
    /// <summary>添加连线</summary>
    state = ACTION.DIRECTION_CREATE;
}

function deleteNode() {
    /// <summary>删除节点</summary>
    var rele = f.focusElement;
    if (!rele || rele.oType != ElementType.NODE) {
        $.messager.alert('提示', '请选择要删除的节点', 'warning');
        return;
    }

    OpenEasyUiConfirm("你确定要删除名称为“" + rele.name + "”的节点吗？", function () {
        var ps = new Params();
        ps.push('doWhat', 'DelNode');
        ps.push('para1', rele.id);
        ps.push('isLogin', true);

        loading();

        ajaxService('flow', 'Do', ps.toJsonDataString(), function (re) {
            loaded();

            if (re != null && re.length > 0) {
                $.messager.alert('失败', re, 'error');
                return;
            }

            f.clearFocus();
            rele.del();
        });
    });
}

function deleteDirection(delDir) {
    /// <summary>删除连线</summary>
    /// <param name="delDir" Type="Raphael Element">绘制的连线对象</param>
    var rele = f.focusElement;
    if (!rele || rele.oType != ElementType.DIRECTION) {
        $.messager.alert('提示', '请选择要删除的连接线', 'warning');
        return;
    }

    OpenEasyUiConfirm("你确定要删除此连接线吗？", function () {
        if (delDir != undefined) {
            delDir.del();
            return;
        }

        f.clearFocus();
        rele.del();
    });
}

function deleteLabel() {
    /// <summary>删除标签</summary>
    var rele = f.focusElement;
    if (!rele || rele.oType != ElementType.LABNOTE) {
        $.messager.alert('提示', '请选择要删除的标签', 'warning');
        return false;
    }

    OpenEasyUiConfirm("你确定要删除此标签吗？", function () {
        f.clearFocus();
        rele.del();
    });
}

function editElementName(eleName, editTarget, eleType, editorMultiline) {
    /// <summary>编辑节点名称/标签文本，只修改本地，未同步到服务器</summary>
    /// <param name="eleName" type="String">对象名称，如节点/标签，供本方法显示用</param>
    /// <param name="editTarget" type="String">对象的属性名称，如名称/文本，供本方法显示用</param>
    /// <param name="eleType" type="String">对象类型，如节点[node]/标签[label]</param>
    /// <param name="editorMultiline" type="Boolean">是否显示多行文本编辑框</param>
    /// <desc>本方法只针对节点名称/标签文本这两种对象，只有要修改的属性在对象中对应为name，且中对象中含有名为rText的Raphael对象时，才可用此方法，</desc>
    var rele = f.focusElement;
    if (!rele || rele.oType != eleType) {
        $.messager.alert('提示', '请选择' + eleName, 'error');
        return;
    }

    OpenEasyUiSampleEditDialog(eleName + editTarget, '编辑', rele.name, function (val) {
        if (val == null || val.length == 0) {
            $.messager.alert('错误', '请输入' + eleName + editTarget + '！', 'error');
            return false;
        }

        rele.name = val;
        rele.rText.attr({ text: val });
    }, null, editorMultiline, 'icon-edit');
}

function selectIcon() {
    /// <summary>更换节点图标</summary>
    var rele = f.focusElement;
    if (!rele || rele.oType != ElementType.NODE) {
        $.messager.alert('提示', '请选择更换图标的节点', 'error');
        return;
    }

    var iconName = rele.icon.substr(rele.icon.lastIndexOf('/') + 1).split('.')[0];
    if (iconName == 'Default') iconName = '申请';

    OpenEasyUiDialog('SelectNodeIcon.htm?icon=' + encodeURIComponent(iconName), dgId, '选择节点图标', 600, 371, 'icon-icon', true, function () {
        var win = document.getElementById(dgId).contentWindow;
        var newIconSrc = win.getNewIconSrc();

        if (!newIconSrc) {
            return;
        }

        newIconSrc = decodeURIComponent(newIconSrc);

        var ps = new Params();
        ps.push('doWhat', 'ChangeNodeIcon');
        ps.push('para1', newIconSrc.substr(newIconSrc.lastIndexOf('/') + 1) + ',' + f.FK_Flow + ',' + rele.id);
        ps.push('isLogin', true);

        loading();

        ajaxService('flow', 'Do', ps.toJsonDataString(), function (data) {
            loaded();

            var jdata = $.parseJSON(data);
            if (jdata.success) {
                rele.icon = newIconSrc;
                rele.rIcon.attr('src', rele.icon);
            }
            else {
                $.messager.alert('错误', '更改节点图标失败：' + jdata.msg, 'error');
            }
        }, null, function (msg) {
            loaded();
            $.messager.alert('错误', '更改节点图标失败：' + msg, 'error');
        });
    });
}

function dirBrokenLine() {
    /// <summary>是否为折线</summary>
    var rele = f.focusElement;
    if (!rele || rele.oType != ElementType.DIRECTION) {
        $.messager.alert('提示', '请选择连接线', 'error');
        return;
    }
        
    EasyUiCheckedMenuItemClick('dirMenu', 'isBrokeLine');
    rele.lineType = rele.lineType == DirectionUIType.Polyline ? DirectionUIType.Line : DirectionUIType.Polyline;
    //此处还需要增加折线节点的逻辑，暂没增加
}

function dirCanBack() {
    /// <summary>是否可以原路返回</summary>
    var rele = f.focusElement;
    if (!rele || rele.oType != ElementType.DIRECTION) {
        $.messager.alert('提示', '请选择连接线', 'error');
        return;
    }

    EasyUiCheckedMenuItemClick('dirMenu', 'isCanBack');
    rele.isCanBack = rele.isCanBack ? 0 : 1;
}

function changeNodeType(checkedItemId) {
    /// <summary>修改节点类型</summary>
    var rele = f.focusElement;
    if (!rele || rele.oType != ElementType.NODE) {
        $.messager.alert('提示', '请选择节点', 'error');
        return;
    }

    var ps = new Params(),
        newNodeType = FlowNodeType[checkedItemId.substr('type'.length)];
    ps.push('doWhat', 'ChangeNodeType');
    ps.push('para1', newNodeType + ',' + f.FK_Flow + ',' + rele.id);
    ps.push('isLogin', true);

    loading();

    ajaxService("flow", "Do", ps.toJsonDataString(), function (data) {
        loaded();

        var jdata = $.parseJSON(data);
        if (jdata.success) {
            EasyUiMenuItemsCheckOnlyOne('nodeMenu', 'nodetypes', checkedItemId, 'type');
            rele.nodeType = newNodeType;
        }
        else {
            $.messager.alert('错误', '修改节点类型失败：' + jdata.msg, 'error');
        }
    }, null,
    function (msg) {
        loaded();
        $.messager.alert('错误', '修改节点类型失败：' + msg, 'error');
    });
}

function showHideBgline() {
    /// <summary>显示/隐藏网格线</summary>
    var bgimg = $('#holder').css('background-image');
    if (bgimg && bgimg.length > 4) {
        $('#holder').css('background-image', 'none');
    }
    else {
        $('#holder').css('background-image', "url('Images/bg.png')");
    }
}