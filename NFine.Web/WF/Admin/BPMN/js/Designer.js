//全局变量
var figureSetsURL = '/WF/Admin/BPMN/lib/sets/bpmn';

$(function () {

    CCBPM_Data_FK_Flow = getArgsFromHref("FK_Flow");
    /**default height for canvas*/
    CanvasProps.DEFAULT_HEIGHT = 680;
    /**default width for canvas*/
    CanvasProps.DEFAULT_WIDTH = 1950;
    //初始化画板
    init(CCBPM_Data_FK_Flow);
    //显示网格
    showGrid();
    //粘合网格线
    snapToGrid();


    //画板右键
    $("#a").bind('contextmenu', function (ev) {
        var coords = getCanvasXY(ev);
        var x = coords[0];
        var y = coords[1];
        lastClick = [x, y];

        // store id value (from Stack) of clicked text primitive
        var textPrimitiveId = -1;

        //find Connector at (x,y)
        var cId = CONNECTOR_MANAGER.connectorGetByXY(x, y);

        // check if we clicked a connector
        if (cId != -1) {
            textPrimitiveId = 0; // (0 by default)

            //右键菜单
            $('#lineMenu').menu({ onShow: function () {
            }, onClick: function (item) {
                Line_MenusFuns(item, cId);
            }
            });

            ev.preventDefault();
            $('#lineMenu').menu('show', {
                left: ev.pageX,
                top: ev.pageY
            });
        } else {
            cId = CONNECTOR_MANAGER.connectorGetByTextXY(x, y);

            // check if we clicked a text of connector
            if (cId != -1) {
                textPrimitiveId = 0; // (0 by default)
            } else {
                //find Figure at (x,y)
                var fId = STACK.figureGetByXY(x, y);

                // check if we clicked a figure
                if (fId != -1) {
                    var figure = STACK.figureGetById(fId);
                    var tId = STACK.textGetByFigureXY(fId, x, y);

                    $('#nodeMenu').menu({ onShow: function () {
                        $("#HD_BPMN_NodeID").val("");
                        $("#HD_BPMN_FigureID").val(fId);
                        var bpm_Node = figure.CCBPM_OID;
                        if (bpm_Node) {
                            $("#HD_BPMN_NodeID").val(bpm_Node);
                        }
                    }, onClick: NodeProperty_Funs
                    });
                    // if we clicked text primitive inside of figure
                    if (tId !== -1) {
                        textPrimitiveId = tId;
                    }
                    if (figure.CCBPM_Shape != null && figure.CCBPM_Shape == CCBPM_Shape_Node) {
                        //右键菜单
                        ev.preventDefault();
                        $('#nodeMenu').menu('show', {
                            left: ev.pageX,
                            top: ev.pageY
                        });
                    }
                } else {
                    //find Container at (x,y)
                    var contId = STACK.containerGetByXY(x, y);
                    ev.preventDefault();
                    $('#mFlowSheet').menu('show', {
                        left: ev.pageX,
                        top: ev.pageY
                    });
                }
            }
        }
    });
});

//统一弹出消息窗口
function Designer_ShowMsg(msg, callBack) {
    if (window.parent && window.parent.BPMN_Msg) {
        window.parent.BPMN_Msg(msg, callBack);
    } else {
        alert(msg);
        if (callBack) callBack();
    }
}

//流程属性
function FlowProperty() {
    url = "../XAP/DoPort.aspx?DoType=En&EnName=BP.WF.Flow&PK=" + CCBPM_Data_FK_Flow + "&Lang=CH";
    if (window.parent) {
        window.parent.addTab(CCBPM_Data_FK_Flow + "PO", "流程属性" + CCBPM_Data_FK_Flow, url);
    } else {
        WinOpen(url);
    }
}
//连线右键
function Line_MenusFuns(item, cId) {
    var rFirstFigure = STACK.figureGetAsFirstFigureForConnector(cId);
    var rSecondFigure = STACK.figureGetAsSecondFigureForConnector(cId);
    //连接线右键菜单点击事件
    switch (item.name) {
        case "linecondition":
            if (rFirstFigure == null) {
                Designer_ShowMsg("所选连线起点没有连接节点，不能设置方向条件。");
                return;
            }
            if (rSecondFigure == null) {
                Designer_ShowMsg("所选连线终点没有连接节点，不能设置方向条件。");
                return;
            }
            if (rFirstFigure.CCBPM_Shape != CCBPM_Shape_Node) {
                Designer_ShowMsg("所选连线起点连接的不是节点，不能设置方向条件。");
                return;
            }
            if (rSecondFigure.CCBPM_Shape != CCBPM_Shape_Node) {
                Designer_ShowMsg("所选连线终点连接的不是节点，不能设置方向条件。");
                return;
            }
            if (rFirstFigure.CCBPM_Shape == CCBPM_Shape_Node && rSecondFigure.CCBPM_Shape == CCBPM_Shape_Node) {
                var fNode = rFirstFigure.CCBPM_OID;
                var tNode = rSecondFigure.CCBPM_OID;
                var url = "../ConditionLine.aspx?FK_Flow=" + CCBPM_Data_FK_Flow + "&FK_MainNode=" + fNode + "&FK_Node=" + fNode + "&ToNodeID=" + tNode + "&CondType=2&Lang=CH";
                window.parent.addTab(CCBPM_Data_FK_Flow + "DIRECTION", "设置方向条件" + fNode + "->" + tNode, url);
            }
            break;
        case "deleteline":
            CONNECTOR_MANAGER.connectorRemoveById(cId, true);
            state = STATE_NONE;
            redraw = true;
            draw();
            break;
    }
}
//节点属性
function NodeProperty_Funs(item) {
    var FK_Node = $("#HD_BPMN_NodeID").val();
    if (FK_Node == "") {
        alert("节点编号不存在，请删除后重新添加。");
        return;
    }
    //根据事件名称进行执行
    switch (item.name) {
        case "nodeproperty": //节点属性
            url = "/WF/Admin/XAP/DoPort.aspx?DoType=En&EnName=BP.WF.Node&PK=" + FK_Node + "&Lang=CH";
            if (window.parent) {
                window.parent.addTab(FK_Node + "PO", "节点属性" + FK_Node, url);
            } else {
                WinOpen(url);
            }
            break;
        case "editnodename": //修改节点名称
            var fId = $("#HD_BPMN_FigureID").val();
            var figure = STACK.figureGetById(fId);
            var tId = 1; //STACK.textGetByFigureXY(fId, x, y);

            // check if we clicked a text primitive inside of shape
            if (tId != -1) {
                // deselect current figure
                selectedFigureId = -1;

                // deselect current container
                selectedContainerId = -1;

                // deselect current connector
                selectedConnectorId = -1;

                // set current state
                state = STATE_TEXT_EDITING;

                // set up text editor
                setUpTextEditorPopup(figure, 1);
                redraw = true;
                draw();
            }
            break;
        case "designernodeform": //设计表单
            url = "../../MapDef/CCForm/Frm.aspx?FK_MapData=" + FK_Node + "&UserNo=" + window.parent.WebUser.No + "&SID=" + window.parent.WebUser.SID;
            if (window.parent) {
                window.parent.addTab(FK_Node + "DSN", "设计表单" + FK_Node, url);
            } else {
                WinOpen(url);
            }
            break;
        case "bindflowfrms": //绑定独立表单
            url = "../BindFrms.aspx?ShowType=FlowFrms&FK_Flow=" + CCBPM_Data_FK_Flow + "&FK_Node=" + FK_Node + "&Lang=CH";
            if (window.parent) {
                window.parent.addTab(FK_Node + "FRM", "绑定独立表单" + FK_Node, url);
            } else {
                WinOpen(url);
            }
            break;
        case "deletenode": //删除节点
            var cmdDelFig = new FigureDeleteCommand(selectedFigureId);
            cmdDelFig.execute();
            draw();
            break;
    }
    $('#nodeMenu').menu('hide');
}

//运行流程
function Run_Flow() {
    var url = "../TestFlow.aspx?FK_Flow=" + CCBPM_Data_FK_Flow + "&Lang=CH";
    WinOpen(url);
}

//检查流程
function Check_Flow() {
    var url = "../DoType.aspx?RefNo=" + CCBPM_Data_FK_Flow + "&DoType=FlowCheck&Lang=CH";
    if (window.parent) {
        window.parent.addTab(CCBPM_Data_FK_Flow + "WFCHECK", "检查流程" + CCBPM_Data_FK_Flow, url);
    } else {
        WinOpen(url);
    }
}

//网格显示
function GridLineVisible() {
    gridVisible = !gridVisible;
    backgroundImage = null; // reset cached background image of canvas
    //trigger a repaint;
    draw();

    var curVisible = document.getElementById("div_gridvisible").innerHTML;
    if (curVisible == "隐藏网格") {
        document.getElementById("div_gridvisible").innerHTML = "显示网格";
    } else if (curVisible == "显示网格") {
        document.getElementById("div_gridvisible").innerHTML = "隐藏网格";
    }
}
//打开窗体
function WinOpen(url) {
    var winWidth = 850;
    var winHeight = 680;
    if (screen && screen.availWidth) {
        winWidth = screen.availWidth;
        winHeight = screen.availHeight - 36;
    }
    window.open(url, "_blank", "height=" + winHeight + ",width=" + winWidth + ",top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no");
}