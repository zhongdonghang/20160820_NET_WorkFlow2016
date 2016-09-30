<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Designer.aspx.cs" Inherits="CCFlow.WF.Admin.BPMN.Designer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CCBPM2.0</title>
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <link href="../../Scripts/easyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/easyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="./assets/javascript/dropdownmenu.js"></script>--%>
    <link rel="stylesheet" media="screen" type="text/css" href="./assets/css/style.css" />
    <link rel="stylesheet" media="screen" type="text/css" href="./assets/css/minimap.css" />
    <script type="text/javascript" src="./assets/javascript/json2.js"></script>
    <script type="text/javascript" src="./assets/javascript/jquery-1.11.0.min.js"></script>
    <script src="../../Scripts/easyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="./assets/javascript/ajaxfileupload.js"></script>--%>
    <%--<link type='text/css' href='./assets/simplemodal/css/diagramo.css' rel='stylesheet'
        media='screen' />--%>
    <%--<script type="text/javascript" src="./assets/simplemodal/js/jquery.simplemodal.js"></script>--%>
    <script type="text/javascript" src="./lib/dashed.js"></script>
    <script type="text/javascript" src="./lib/canvasprops.js"></script>
    <script type="text/javascript" src="./lib/style.js"></script>
    <script type="text/javascript" src="./lib/primitives.js"></script>
    <script type="text/javascript" src="./lib/ImageFrame.js"></script>
    <script type="text/javascript" src="./lib/matrix.js"></script>
    <script type="text/javascript" src="./lib/util.js"></script>
    <script type="text/javascript" src="./lib/key.js"></script>
    <script type="text/javascript" src="./lib/groups.js"></script>
    <script type="text/javascript" src="./lib/stack.js"></script>
    <script type="text/javascript" src="./lib/connections.js"></script>
    <script type="text/javascript" src="./lib/connectionManagers.js"></script>
    <script type="text/javascript" src="./lib/handles.js"></script>
    <script type="text/javascript" src="./lib/builder.js"></script>
    <script type="text/javascript" src="./lib/text.js"></script>
    <script type="text/javascript" src="./lib/log.js"></script>
    <script type="text/javascript" src="./lib/browserReady.js"></script>
    <script type="text/javascript" src="./lib/containers.js"></script>
    <script type="text/javascript" src="./lib/importer.js"></script>
    <script type="text/javascript" src="./lib/main.js" charset="UTF-8"></script>
    <script src="lib/sets/bpmn/Activity/Activity.js" type="text/javascript" charset="gb2312"></script>
    <script src="lib/sets/bpmn/Gateway/Gateway.js" type="text/javascript" charset="gb2312"></script>
    <script src="lib/sets/bpmn/Event/Event.js" type="text/javascript" charset="gb2312"></script>
    <script type="text/javascript" src="./lib/minimap.js"></script>
    <script type="text/javascript" src="./lib/commands/History.js"></script>
    <script type="text/javascript" src="./lib/commands/FigureCreateCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/FigureCloneCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/FigureTranslateCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/FigureRotateCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/FigureScaleCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/FigureZOrderCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/FigureDeleteCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/GroupRotateCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/GroupScaleCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/GroupCreateCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/GroupCloneCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/GroupDestroyCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/GroupDeleteCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/GroupTranslateCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/ContainerCreateCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/ContainerDeleteCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/ContainerTranslateCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/ContainerScaleCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/ConnectorCreateCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/ConnectorDeleteCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/ConnectorAlterCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/ShapeChangePropertyCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/CanvasChangeColorCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/CanvasChangeSizeCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/CanvasFitCommand.js"></script>
    <script type="text/javascript" src="./lib/commands/InsertedImageFigureCreateCommand.js"></script>
    <script type="text/javascript" src="./assets/javascript/colorPicker_new.js"></script>
    <link rel="stylesheet" media="screen" type="text/css" href="./assets/css/colorPicker_new.css" />
    <script src="js/CCFlowDesignerData.js" type="text/javascript"></script>
    <script src="js/Designer.js" type="text/javascript"></script>
    <!--[if IE]>
        <script src="./assets/javascript/excanvas.js"></script>
    <![endif]-->
</head>
<body id="body">
    <div id="actions">
        <a style="text-decoration: none;" href="#" onclick="return save(true);" title="保存(Ctrl-S)">
            <img src="assets/images/icon_save.jpg" border="0" width="18" height="18" /></a>
            <img class="separator" src="assets/images/toolbar_separator.gif" border="0" width="1" height="18" />
        <a style="text-decoration: none;" href="#" onclick="return FlowProperty();" title="属性">
            <img src="assets/images/icon_open.jpg" border="0" width="18" height="18" /></a>
            <img class="separator" src="assets/images/toolbar_separator.gif" border="0" width="1"
            height="18" />
        <a style="text-decoration: none;" href="#" onclick="return Check_Flow();" title="检查流程">
            <img src="assets/images/editdiagram.png" border="0" width="18" height="18" /></a>
        <img class="separator" src="assets/images/toolbar_separator.gif" border="0" width="1"
            height="18" />
        <a style="text-decoration: none;" href="#" onclick="return Run_Flow();" title="运行流程">
            <img src="assets/images/remove.gif" border="0" width="18" height="18" /></a>
        <img class="separator" src="assets/images/toolbar_separator.gif" border="0" width="1"
            height="18" />
        <a href="javascript:action('connector-straight');" title="直线连接">
            <img src="assets/images/icon_connector_straight.gif" border="0" /></a>
        <img class="separator" src="assets/images/toolbar_separator.gif" border="0" width="1"
            height="18" />
        <a href="javascript:action('connector-jagged');" title="折线连接">
            <img src="assets/images/icon_connector_jagged.gif" border="0" /></a>
        <img class="separator" src="assets/images/toolbar_separator.gif" border="0" width="1"
            height="18" />
        <a href="javascript:action('connector-organic');" title="曲线连接">
            <img src="assets/images/icon_connector_organic.gif" border="0" alt="Organic" /></a>
        <%--<img class="separator" src="assets/images/toolbar_separator.gif" border="0" width="1"
            height="18" />
        <a href="javascript:action('container');" title="Container (Experimental)">
            <img src="assets/images/container.png" border="0" alt="Container" /></a>--%>
        <img class="separator" src="assets/images/toolbar_separator.gif" border="0" width="1"
            height="18" />
        <a href="javascript:createFigure(figure_Text, 'assets/images/text.gif');" title="Add text">
            <img src="assets/images/text.gif" border="0" height="18" /></a>
        <%--<img class="separator" src="assets/images/toolbar_separator.gif" border="0" width="1"
            height="18" />
            <a href="javascript:action('duplicate');">Copy</a>--%>
        <%--<img class="separator" src="assets/images/toolbar_separator.gif" border="0" width="1"
            height="18" />
        <a href="javascript:action('undo');" title="Undo (Ctrl-Z)">
            <img src="assets/images/arrow_undo.png" border="0" /></a>
        <img class="separator" src="assets/images/toolbar_separator.gif" border="0" width="1"
            height="18" />
        <a href="javascript:action('redo');" title="Redo (Ctrl-Y)">
            <img src="assets/images/arrow_redo.png" border="0" /></a>--%>
        <script type="text/javascript" language="javascript">
            if (!isBrowserReady()) {
                document.write('<span style="background-color: red;" >');
                document.write("您的浏览器不支持HTML5。请升级您的浏览器到高级版本，或者使用火狐、谷歌浏览器。");
                document.write("</span>");
            }
        </script>
    </div>
    <div id="editor">
        <div id="figures" style="height: 100%;">
            <select style="width: 120px;" onchange="setFigureSet(this.options[this.selectedIndex].value);">
                <script type="text/javascript">
                    for (var setName in figureSets) {
                        var set = figureSets[setName];
                        document.write('<option value="' + setName + '">' + set['name'] + '</option>');
                    }
                </script>
            </select>
            <script type="text/javascript" language="javascript">
                /**Builds the figure panel*/
                function buildPanel() {
                    //var first = true;
                    var firstPanel = true;
                    for (var setName in figureSets) {
                        var set = figureSets[setName];
                        var eSetDiv = document.createElement('div');
                        eSetDiv.setAttribute('id', setName);

                        if (firstPanel) {
                            firstPanel = false;
                        }
                        else {
                            eSetDiv.style.display = 'none';
                        }
                        document.getElementById('figures').appendChild(eSetDiv);

                        //add figures to the div
                        for (var figure in set['figures']) {
                            figure = set['figures'][figure];

                            var figureFunctionName = 'figure_' + figure.figureFunction;
                            var figureThumbURL = 'lib/sets/bpmn/' + setName + '/' + figure.image;

                            var eFigure = document.createElement('img');
                            eFigure.setAttribute('src', figureThumbURL);

                            eFigure.addEventListener('mousedown', function (figureFunction, figureThumbURL) {
                                return function (evt) {
                                    evt.preventDefault();
                                    //Log.info("editor.php:buildPanel: figureFunctionName:" + figureFunctionName);
                                    createFigure(window[figureFunction] /*we need to search for function in window namespace (as all that we have is a simple string)**/
                                            , figureThumbURL);
                                };
                            } (figureFunctionName, figureThumbURL), false);

                            //in case use drops the figure
                            eFigure.addEventListener('mouseup', function () {
                                createFigureFunction = null;
                                selectedFigureThumb = null;
                                state = STATE_NONE;
                            }, false);

                            eFigure.style.cursor = 'pointer';
                            eFigure.style.marginRight = '5px';
                            eFigure.style.marginTop = '2px';

                            eSetDiv.appendChild(eFigure);
                        }
                    }
                }
                buildPanel();
            </script>
            <div id="minimap">
            </div>
        </div>
        <!--THE canvas-->
        <div style="width: 100%">
            <div id="container">
                <canvas id="a" width="800" height="500" style="overflow: auto;">
                <fieldset>
                <legend>提示</legend>
                        您的浏览器不支持HTML5，请升级您的浏览器到高级版本，或者使用火狐、谷歌浏览器。
                        </fieldset>
                </canvas>
                <div id="text-editor">
                </div>
                <div id="text-editor-tools">
                </div>
            </div>
        </div>
        <!--Right panel-->
        <div id="right">
            <center>
                <div style="overflow: auto; display: none;" id="edit">
                </div>
            </center>
        </div>
    </div>
    <script type="text/javascript">
        function loadFill(check) {
            if (check.checked === true) {
                if ($('#colorpickerHolder3').css('display') === 'none') {
                    $('#colorSelector3').click();
                }
            }
            else {
                if ($('#colorpickerHolder3').css('display') === 'block') {
                    $('#colorSelector3').click();
                }
            }
        }
    </script>
    <br />
    <div id="mFlowSheet" class="easyui-menu" style="width: 120px;">
        <div onclick="GridLineVisible()" data-options="iconCls:'icon-new'">
            <span id="div_gridvisible">隐藏网格</span></div>
        <div onclick="FlowProperty()" data-options="iconCls:'icon-edit'">流程属性</div>
    </div>
    <div id="nodeMenu" class="easyui-menu" style="width: 180px;">
        <div data-options="iconCls:'icon-config',name:'nodeproperty'">节点属性</div>
        <div data-options="iconCls:'icon-edit',name:'editnodename'">修改节点名称</div>
        <div class="menu-sep"></div>
        <div data-options="name:'designernodeform'">设计表单</div>
        <div class="menu-sep"></div>
        <div data-options="iconCls:'icon-bind',name:'bindflowfrms'">绑定独立表单</div>
        <div class="menu-sep"></div>
        <div data-options="iconCls:'icon-delete',name:'deletenode'">删除节点</div>
    </div>
    <div id="lineMenu" class="easyui-menu" style="width: 180px;">
        <div data-options="iconCls:'icon-config',name:'linecondition'">设置方向条件</div>
        <div data-options="iconCls:'icon-delete',name:'deleteline'">删除连线</div>
    </div>
    <input type="hidden" id="HD_BPMN_NodeID" />
    <input type="hidden" id="HD_BPMN_FigureID" />
</body>
</html>
