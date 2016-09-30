//定义功能树存储数组，数组中元素对应easyui-tabs控件中的tab页
var functrees = [];
/*
easyui-tabs功能导航区定义，added by liuxc
说明：
1.functrees数组下元素：(1)Id:tab页中的tree控件的id值，tab页的id为"tab_" + 此id；(2)Name:tab页标题；(3)AttrCols:定义WebService获取数据时，要写入node.attributes中的属性列表；(4)ServiceCount:定义此树结构中一共要连接WebService的次数，此处是为便于编程而设置的，一定要设置正确；(5)RootASC:树结构中，如果存在多个根节点，则此项设置是为这多个根节点进行排序，其中Field即排序依据的属性名称，Index即为按Field值排列的顺序；(6)Nodes:tree中的节点数组，功能支持如下：
①支持无限级节点设置；②支持任一级别从WebService获取DataTable的Json数据填充（此连接WebService使用的是CCFlowDesignerData.js文件中的ajaxService方法，未另写方法，请注意）；③支持各级节点的图标、右键绑定菜单、双击链接Url的规则设置，支持多级嵌套规则设置；④链接Url支持node属性值、node.attributes属性值及WebUser属性值的自动替换（使用“@@属性字段名”来代替要替换的属性）
2.Nodes数组下元素：(1)Type:节点类型，Node=普通定义节点，Service=通过获取WebService数据填充的节点；(2)ServiceMethod:ajaxService方法传参中method的值，即调用的获取数据的方法，Service类型节点特有属性；(3)CodId:WebService返回的DataTable Json数据代表节点Id的列名，Service类型节点特有属性；(4)ColParentId:WebService返回的DataTable Json数据代表父级节点Id的列名，Service类型节点特有属性；(5)ColName:WebService返回的DataTable Json数据代表节点文字的列名，Service类型节点特有属性；(6)RootParentId:WebService返回的DataTable Json数据代表根节点的父级Id的值，Service类型节点特有属性；(7)ColDefine:WebService返回的DataTable Json数据，设置的此列，根据此列的值进行设置各节点的图标、右链菜单以及双击打开页面，Service类型节点特有属性；(8)Defines:此数组的元素代表ColDefine所设列的详细规则设置，每个元素代表一种情况，整个设置可以理解为：
swith(ColDefine1.Value){
    case 'aaa':
        node.IconCls='icon-aaa';
        node.MenuId = 'menu-aaa';
        node.Url = 'url-aaa';
        break;
    case 'bbb':
        swith(ColDefine2.Value){
            case 'ccc':
                node.IconCls='icon-ccc';
                node.MenuId = 'menu-ccc';
                node.Url = 'url-ccc';
                break;
            default:
                ......
        }
        break;
    default:    //未设置Value值，则表示此项
        ......
}
此项规则设置，可以进行多级嵌套设置，即Defines元素中再包含ColDefine设置，Service类型节点特有属性；(9)Id:节点node.id值，Node类型节点特有属性；(10)ParentId:节点的父节点node.id，根节点的父节点id请设置为null，Node类型节点特有属性；(11)Name:节点node.text值，Node类型节点特有属性
3.Defines数组下元素：(1)Value:规则判断值；(2)ColDefine:规则判断所用的字段名称；(3)Defines:具体规则设置，见上方规则设置说明；(4)IconCls:节点图标对应的css样式名称；(5)MenuId:节点右链菜单的id，为easyui-menu；(6)Url:节点双击在右侧tab页打开的网页Url，支持node属性值、node.attributes属性值及WebUser属性值的自动替换
*/
//1.流程库
functrees.push({
    Id: "flowTree",
    Name: "流程树",
    AttrCols: ["TType", "DType", "IsParent"],
    ServiceCount: 1,
    Nodes: [
			{ Type: "Service", ServiceMethod: "GetFlowTree", ColId: "No", ColParentId: "ParentNo", ColName: "Name", RootParentId: "F0",
			    ColDefine: "TType", Defines: [
											{ Value: "FLOWTYPE", ColDefine: "ParentNo",
											    Defines: [
															{ Value: "F0", IconCls: "icon-flowtree", MenuId: "mFlowRoot" },
															{ IconCls: "icon-tree_folder", MenuId: "mFlowSort" }
														]
											},
											{ Value: "FLOW", ColDefine: "DType",Defines:[
                                                { Value: "1", IconCls: "icon-flow1", MenuId: "mFlow", Url: "Designer.aspx?FK_Flow=@@id&UserNo=@@WebUser.No&SID=@@WebUser.SID" },
                                                { IconCls: "icon-flow1", MenuId: "mFlow", Url: "DesignerSL.aspx?FK_Flow=@@id&UserNo=@@WebUser.No&SID=@@WebUser.SID" }
                                            ]}
										  ]
			},
			{ Type: "Node", Id: "FlowCloud", ParentId: null, Name: "ccbpm云服务-流程云", TType: "FLOWCLOUD", DType: "-1", IconCls: "icon-flowcloud",
			    Nodes: [
						{ Type: "Node", Id: "ShareFlow", ParentId: "FlowCloud", Name: "共有流程云", TType: "SHAREFLOW", DType: "-1", IconCls: "icon-flowpublic", Url: "" },
						{ Type: "Node", Id: "PriFlow", ParentId: "FlowCloud", Name: "私有流程云", TType: "PRIFLOW", DType: "-1", IconCls: "icon-flowprivate" }
					  ]
			}
		  ]
});
//2.表单库
functrees.push({
    Id: "formTree",
    Name: "表单库",
    AttrCols: ["TType"],
    RootASC: { Field: "TType", Index: ["FORMTYPE", "SRCROOT", "FORMREF", "CLOUNDDATA"] },
    ServiceCount: 2,
    Nodes: [
			{ Type: "Service", ServiceMethod: "GetFormTree", ColId: "No", ColParentId: "ParentNo", ColName: "Name", RootParentId: null,
			    ColDefine: "TType", Defines: [
											{ Value: "FORMTYPE", ColDefine: "ParentNo",
											    Defines: [
															{ Value: null, IconCls: "icon-formtree", MenuId: "mFormRoot" },
															{ IconCls: "icon-tree_folder", MenuId: "mFormSort" }
														]
											},
											{ Value: "FORM", IconCls: "icon-form", MenuId: "mForm", Url: "../../MapDef/CCForm/Frm.aspx?FK_MapData=@@id&UserNo=@@WebUser.No&SID=@@WebUser.SID" },
                                            { Value: "SRC", IconCls: "icon-src" }
										  ]
			},
			{ Type: "Node", Id: "SrcRoot", ParentId: null, Name: "数据源字典表", TType: "SRCROOT", IconCls: "icon-srctree", MenuId: "mSrcRoot",
			    Nodes: [
						{ Type: "Service", ServiceMethod: "GetSrcTree", ColId: "No", ColParentId: "ParentNo", ColName: "Name", RootParentId: "SrcRoot",
						    ColDefine: "TType", Defines: [
											{ Value: "SRC", IconCls: "icon-src", MenuId: "mSrc", Url: "../../Comm/RefFunc/UIEn.aspx?EnsName=BP.Sys.SFDBSrcs&No=@@id&t=" + Math.random() },
											{ Value: "SRCTABLE", IconCls: "icon-srctable", MenuId: "mSrcTable", Url: "../../MapDef/Do.aspx?DoType=EditSFTable&RefNo=@@id&t=" + Math.random() }
										  ]
						}
					  ]
			},
			{ Type: "Node", Id: "FormRef", ParentId: null, Name: "表单相关", TType: "FORMREF", IconCls: "icon-tree_folder",
			    Nodes: [
						{ Type: "Node", Id: "Enums", ParentId: "FormRef", Name: "枚举列表", TType: "ENUMS", IconCls: "icon-enum", Url: "../../Comm/Sys/EnumList.aspx?t=" + Math.random() },
						{ Type: "Node", Id: "JSLib", ParentId: "FormRef", Name: "JS验证库", TType: "JSLIB", IconCls: "icon-js", Url: "../../Comm/Sys/FuncLib.aspx?t=" + Math.random() }
					  ]
			},
			{ Type: "Node", Id: "CloundData", ParentId: null, Name: "ccbpm云服务-表单云", TType: "CLOUNDDATA", IconCls: "icon-formcloud",
			    Nodes: [
						{ Type: "Node", Id: "PriForm", ParentId: "CloundData", Name: "私有表单云", TType: "PRIFORM", IconCls: "icon-formprivate", Url: "../Clound/FormPublic.aspx" },
						{ Type: "Node", Id: "ShareForm", ParentId: "CloundData", Name: "共有表单云", TType: "SHAREFORM", IconCls: "icon-formpublic", Url: "../Clound/FormPrivate.aspx" }
					  ]
			}
		  ]
});
//3.组织结构
functrees.push({
    Id: "OrgTree",
    Name: "组织结构",
    AttrCols: ["TType"],
    ServiceCount: 1,
    Nodes: [
			{ Type: "Node", Id: "BasicSetting", ParentId: null, Name: "基础设置", TType: "BASICROOT", IconCls: "icon-tree_folder",
			    Nodes: [
						{ Type: "Node", Id: "Integration", ParentId: "BasicSetting", Name: "集成设置", TType: "INTEGRATION", IconCls: "icon-form", Url: "../Org/Integration.aspx" },
						{ Type: "Node", Id: "DeptTypies", ParentId: "BasicSetting", Name: "部门类型", TType: "DEPTTYPIES", IconCls: "icon-form", Url: "../../Comm/Search.aspx?EnsName=BP.GPM.DeptTypes" },
						{ Type: "Node", Id: "Duties", ParentId: "BasicSetting", Name: "职务管理", TType: "DUTIES", IconCls: "icon-form", Url: "../../Comm/Search.aspx?EnsName=BP.GPM.Dutys" },
						{ Type: "Node", Id: "Stations", ParentId: "BasicSetting", Name: "岗位管理", TType: "STATIONS", IconCls: "icon-form", Url: "../../Comm/Search.aspx?EnsName=BP.GPM.Stations" }
					  ]
			},
			{ Type: "Service", ServiceMethod: "GetStructureTree", ColId: "No", ColParentId: "ParentNo", ColName: "Name", RootParentId: "0",
			    ColDefine: "TType", Defines: [
											{ Value: "DEPT", ColDefine: "ParentNo",
											    Defines: [
															{ Value: "0", IconCls: "icon-tree_folder" },
															{ IconCls: "icon-dept" }
														]
											},
											{ Value: "STATION", IconCls: "icon-station" },
											{ Value: "EMP", IconCls: "icon-emp" }
										  ]
			}
		  ]
});
//4.系统维护
functrees.push({
    Id: "sysTree",
    Name: "系统维护",
    Nodes: [
			{ Type: "Node", Id: "MenuRole", ParentId: null, Name: "菜单权限", IconCls: "icon-tree_folder",
			    Nodes: [
						{ Type: "Node", Id: "SysConfig", ParentId: "MenuRole", Name: "系统维护", IconCls: "icon-form", Url: "../../../GPM/AppList.aspx" },
						{ Type: "Node", Id: "RoleGroup", ParentId: "MenuRole", Name: "权限组维护", IconCls: "icon-form", Url: "../../Comm/SearchEUI.aspx?EnsName=BP.GPM.Groups" },
						{ Type: "Node", Id: "MenuForRole", ParentId: "MenuRole", Name: "按菜单分配权限", IconCls: "icon-form", Url: "../../../GPM/AppMenuToEmp.aspx" },
						{ Type: "Node", Id: "UserForRole", ParentId: "MenuRole", Name: "按用户分配权限", IconCls: "icon-form", Url: "../../../GPM/EmpForMenus.aspx" },
						{ Type: "Node", Id: "StationForRole", ParentId: "MenuRole", Name: "按岗位分配权限", IconCls: "icon-form", Url: "../../../GPM/StationForMenus.aspx" },
						{ Type: "Node", Id: "GroupForRole", ParentId: "MenuRole", Name: "按权限组分配权限", IconCls: "icon-form", Url: "../../../GPM/EmpGroupForMenus.aspx" }
					  ]
			},
			{ Type: "Node", Id: "BasicSetting2", ParentId: null, Name: "基础设置", IconCls: "icon-tree_folder",
			    Nodes: [
						{ Type: "Node", Id: "HolidaySetting", ParentId: "BasicSetting2", Name: "节假日设置", IconCls: "icon-form", Url: "../../Comm/Sys/Holiday.aspx" },
						{ Type: "Node", Id: "TableStructure", ParentId: "BasicSetting2", Name: "表结构", IconCls: "icon-form", Url: "../../Comm/Sys/SystemClass.aspx" },
						{ Type: "Node", Id: "SysVal", ParentId: "BasicSetting2", Name: "系统变量", IconCls: "icon-form", Url: "javascript:void(0)" },
						{ Type: "Node", Id: "FlowPrevSetting", ParentId: "BasicSetting2", Name: "流程预先审批设置", IconCls: "icon-form", Url: "../GetTask.aspx" },
						{ Type: "Node", Id: "FuncDown", ParentId: "BasicSetting2", Name: "功能执行", IconCls: "icon-form", Url: "../../Comm/MethodLink.aspx" }
					  ]
			},
			{ Type: "Node", Id: "SysLog", ParentId: null, Name: "系统日志", IconCls: "icon-tree_folder",
			    Nodes: [
						{ Type: "Node", Id: "LoginLog", ParentId: "SysLog", Name: "登录日志", IconCls: "icon-form", Url: "javascript:void(0)" },
						{ Type: "Node", Id: "ExceptionLog", ParentId: "SysLog", Name: "异常日志", IconCls: "icon-form", Url: "javascript:void(0)" }
					  ]
			}
		  ]
});

var tabsId = null;

//定义功能树对象，便于之后的操作
function FuncTrees(sTabsId) {
    /// <summary>功能树对象操作类</summary>
    /// <param name="sTabsId" type="String">功能树easyui-tabs控件的id</param>
    this.TabsId = tabsId = sTabsId;
    this.tabs = null;

    if (typeof FuncTrees._initialized == "undefined") {
        FuncTrees.prototype.loadTrees = function () {
            /// <summary>加载功能树，其中功能树的定义放在FuncTree.js中的funcTrees数组对象中</summary>
            var tabid = "#" + this.TabsId;

            //循环增加tab标签
            $.each(functrees, function (fcidx, fc) {
                $(tabid).tabs("add", {
                    id: "tab_" + this.Id,
                    title: this.Name,
                    content: "<ul id=\"" + this.Id + "\" class=\"easyui-tree\"></ul>"
                });

                if (this.ServiceCount > 0) {
                    for (var i = 0; i < this.Nodes.length; i++) {
                        LoadServiceNode(this.Nodes[i], null, this);
                    }
                }
                else {
                    $.each(this.Nodes, function () {
                        LoadTreeNode(this, null, fc);
                    });

                    ExpandFirstLevelNode(this);
                    OnContextMenu(this);
                    OnDbClick(this);
                }
            });
        }

        FuncTrees.prototype.appendNode = function (sTreeId, sParentNodeId, oNode) {
            /// <summary>增加树节点</summary>
            /// <param name="sTreeId" type="String">功能树easyui-tree控件的id</param>
            /// <param name="sParentNodeId" type="String">待增加树节点的父节点id</param>
            /// <param name="oNode" type="Object">待增加的树节点对象，格式如:{ id: 'aaa', text: '节点1', iconCls: 'icon-new', attributes: {MenuId: "myMenu", Url: "xxx.aspx"} } </param>
            $("#" + sTreeId).tree("append", {
                parent: $("#" + sTreeId + " div[node-id='" + sParentNodeId + "']"),
                data: [oNode]
            });

            $("#" + sTreeId).tree("select", $("#" + sTreeId + " div[node-id='" + oNode.id + "']"));
        }

        FuncTrees.prototype.deleteNode = function (sTreeId, sNodeId) {
            /// <summary>删除树节点</summary>
            /// <param name="sTreeId" type="String">功能树easyui-tree控件的id</param>
            /// <param name="sNodeId" type="String">待删除树节点的id</param>
            $("#" + sTreeId).tree("remove", $("#" + sTreeId + " div[node-id='" + sNodeId + "']"));
        }
    }
}

function LoadServiceNode(oNode, oParentNode, oFuncTree) {
    /// <summary>从WebService返回节点数据，生成节点定义对象</summary>
    /// <param name="oNode" type="Object">节点定义对象</param>
    /// <param name="oParentNode" type="Object">节点定义对象的父级对象</param>
    /// <param name="oFuncTree" type="Object">树对象</param>
    if (oNode.Type == "Service") {
        ajaxService({ method: oNode.ServiceMethod }, function (data, nd) {
            var re = $.parseJSON(data);

            //将所有获取的数据转换为Node
            var roots = Find(re, nd.ColParentId, nd.RootParentId);

            if (roots.length > 0) {
                nd.Type = "Node";
                nd.Id = roots[0][nd.ColId];
                nd.ParentId = oParentNode == null ? null : oParentNode.Id; // root[nd.ColParentId];
                nd.Name = roots[0][nd.ColName];

                if (oFuncTree.AttrCols && oFuncTree.AttrCols.length > 0) {
                    $.each(oFuncTree.AttrCols, function (acidx, ac) {
                        nd[ac] = roots[0][ac];
                    });
                }

                var define = FindDefine(nd.ColDefine, nd.Defines, roots[0]);

                if (define) {
                    nd.IconCls = define.IconCls;
                    nd.MenuId = define.MenuId;
                    nd.Url = define.Url;
                }

                //生成子节点
                LoadServiceSubNode(re, nd, oParentNode, nd, oFuncTree);

                for (var i = 1; i < roots.length; i++) {
                    var nextND = {
                        Type: "Node",
                        Id: roots[i][nd.ColId],
                        ParentId: oParentNode == null ? null : oParentNode.Id,
                        Name: roots[i][nd.ColName]
                    };

                    if (oFuncTree.AttrCols && oFuncTree.AttrCols.length > 0) {
                        $.each(oFuncTree.AttrCols, function (acidx, ac) {
                            nextND[ac] = roots[i][ac];
                        });
                    }

                    define = FindDefine(nd.ColDefine, nd.Defines, roots[i]);

                    if (define) {
                        nextND.IconCls = define.IconCls;
                        nextND.MenuId = define.MenuId;
                        nextND.Url = define.Url;
                    }

                    if (oParentNode == null) {
                        oFuncTree.Nodes.push(nextND);
                    }
                    else {
                        if (!oParentNode.Nodes) {
                            oParentNode.Nodes = [];
                        }

                        oParentNode.Nodes.push(nextND);
                    }

                    //生成子节点
                    LoadServiceSubNode(re, nextND, oParentNode, nd, oFuncTree);
                }
            }

            oFuncTree.ServiceCount--;

            //判断是否完成所有的服务调用，如果完成，则进行全树的生成
            if (oFuncTree.ServiceCount == 0) {
                //排序根节点顺序
                if (oFuncTree.RootASC) {
                    oFuncTree.Nodes.sort(function (oNode1, oNode2) {
                        return IndexInArray(oNode1[oFuncTree.RootASC.Field], oFuncTree.RootASC.Index) > IndexInArray(oNode2[oFuncTree.RootASC.Field], oFuncTree.RootASC.Index);
                    });
                }

                $.each(oFuncTree.Nodes, function () {
                    LoadTreeNode(this, null, oFuncTree);
                });

                //只展开第一级节点
                ExpandFirstLevelNode(oFuncTree);
                OnContextMenu(oFuncTree);
                OnDbClick(oFuncTree);
            }
        }, oNode);
    }
    else {
        if (oNode.Nodes && oNode.Nodes.length > 0) {
            $.each(oNode.Nodes, function () {
                LoadServiceNode(this, oNode, oFuncTree);
            });
        }
    }
}

function OnDbClick(oFuncTree) {
    /// <summary>树节点的双击事件处理逻辑</summary>
    /// <param name="oFuncTree" type="Object">树对象</param>
    $("#" + oFuncTree.Id).tree({
        onDblClick: function (node) {
            $("#" + oFuncTree.Id).tree('select', node.target);
            //支持将url中的@@+字段格式自动替换成node节点及其属性、或WebUser中同名的属性值
            if (node.attributes.Url) {
                var url = node.attributes.Url;

                if (node.attributes.Url.indexOf("@@") != -1) {
                    for (field in node) {
                        if (typeof node[field] != "undefined" && url.indexOf("@@" + field) != -1) {
                            url = url.replace("@@" + field, node[field]);
                        }
                    }

                    for (field in node.attributes) {
                        if (typeof node.attributes[field] != "undefined" && url.indexOf("@@" + field) != -1) {
                            url = url.replace("@@" + field, node.attributes[field]);
                        }
                    }

                    for (field in WebUser) {
                        if (typeof WebUser[field] != "undefined" && url.indexOf("@@WebUser." + field) != -1) {
                            url = url.replace("@@WebUser." + field, WebUser[field]);
                        }
                    }
                }

                $(".mymask").show();
                addTab(node.id, node.text, url, node.iconCls);
                setTimeout(DesignerLoaded, 2000);
            }
            else if ($("#" + oFuncTree.Id).tree('isLeaf', node.target) == false) {
                if (node.state == "closed") {
                    $("#" + oFuncTree.Id).tree("expand", node.target);
                }
                else {
                    $("#" + oFuncTree.Id).tree("collapse", node.target);
                }
            }
        }
    });
}

function OnContextMenu(oFuncTree) {
    /// <summary>树的右键菜单处理逻辑</summary>
    /// <param name="oFuncTree" type="Object">树对象</param>
    $("#" + oFuncTree.Id).tree({
        onContextMenu: function (e, node) {
            e.preventDefault();

            $("#" + oFuncTree.Id).tree('select', node.target);

            if (node.attributes && node.attributes.MenuId) {
                $("#" + node.attributes.MenuId).menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });
            }
        }
    });
}
function ExpandFirstLevelNode(oFuncTree) {
    /// <summary>设置树的第一级节点全部为展开状态</summary>
    /// <param name="oFuncTree" type="Object">树对象</param>
    $("#" + oFuncTree.Id).tree("collapseAll");

    $.each(oFuncTree.Nodes, function () {
        $("#" + oFuncTree.Id).tree("expand", $("#" +oFuncTree.Id+ " div[node-id='" + this.Id + "']"));
    });

    //选中第一个标签页
    $("#" + tabsId).tabs("select", 0);
}

function IndexInArray(oVal, aSortArray) {
    /// <summary>获取元素在数组中的索引序号</summary>
    /// <param name="oVal" type="Object">数组中的元素</param>
    /// <param name="aSortArray" type="Array">数组对象</param>
    /// <return type="Int">返回索引序号</return>
    for (var i = 0; i < aSortArray.length; i++) {
        if (aSortArray[i] == oVal) {
            return i;
        }
    }
    return -1;
}

function LoadServiceSubNode(aServiceNodes, oNode, oParentNode, oServiceNode, oFuncTree) {
    /// <summary>加载节点定义对象的子级对象</summary>
    /// <param name="aServiceNodes" type="Array">WebService返回的节点定义对象集合</param>
    /// <param name="oNode" type="Object">节点定义对象</param>
    /// <param name="oParentNode" type="Object">节点定义对象的父级对象</param>
    /// <param name="oServiceNode" type="Object">初始节点定义对象，此对象含有Service类节点的一些参数</param>
    /// <param name="oFuncTree" type="Object">树定义对象</param>
    var subs = Find(aServiceNodes, oServiceNode.ColParentId, oNode.Id);

    $.each(subs, function (sidx, sub) {
        var subNode = {
            Type: "Node",
            Id: this[oServiceNode.ColId],
            ParentId: oNode.Id,
            Name: this[oServiceNode.ColName]
        };

        if (oFuncTree.AttrCols && oFuncTree.AttrCols.length > 0) {
            $.each(oFuncTree.AttrCols, function (acidx, ac) {
                subNode[ac] = sub[ac];
            });
        }

        define = FindDefine(oServiceNode.ColDefine, oServiceNode.Defines, this);

        if (define) {
            subNode.IconCls = define.IconCls;
            subNode.MenuId = define.MenuId;
            subNode.Url = define.Url;
        }

        if (!oNode.Nodes) {
            oNode.Nodes = [];
        }

        oNode.Nodes.push(subNode);

        //生成子节点
        LoadServiceSubNode(aServiceNodes, subNode, oNode, oServiceNode, oFuncTree);
    });
}

function LoadTreeNode(oNode, oParentNode, oFuncTree) {
    /// <summary>加载树节点</summary>
    /// <param name="oNode" type="Object">节点定义对象</param>
    /// <param name="oParentNode" type="Object">节点定义对象的父级对象</param>
    /// <param name="oFuncTree" type="Object">树定义对象</param>
    //生成附加属性
    var attrs = { MenuId: oNode.MenuId, Url: oNode.Url };

    if (oFuncTree.AttrCols) {
        $.each(oFuncTree.AttrCols, function () {
            attrs[this] = oNode[this];
        });
    }

    $("#" + oFuncTree.Id).tree("append", {
        parent: oParentNode,
        data: [{
            id: oNode.Id,
            text: oNode.Name,
            iconCls: oNode.IconCls,
            attributes: attrs
        }]
    });

    if (oNode.Nodes && oNode.Nodes.length > 0) {
        $.each(oNode.Nodes, function () {
            LoadTreeNode(this,  $("#" + oFuncTree.Id + " div[node-id='" + oNode.Id + "']"), oFuncTree);
        });
    }
}

function Find(aItems, sField, oValue) {
    /// <summary>查找数组中指定字段值的元素</summary>
    /// <param name="aItems" type="Array">要查找的数组</param>
    /// <param name="sField" type="String">依据字段名称</param>
    /// <param name="oValue" type="Object">字段的值</param>
    /// <return>返回集合</return>
    var re = [];

    $.each(aItems, function () {
        if (this[sField] == oValue) {
            re.push(this);
        }
    });

    return re;
}

function FindDefine(sColDefine, aDefines, oNode) {
    /// <summary>查找指定节点的设置规则</summary>
    /// <param name="sColDefine" type="String">规则依据的字段名称</param>
    /// <param name="aDefines" type="Array">当前规则集合</param>
    /// <param name="oNode" type="Object">要查找规则的节点</param>
    var define;

    $.each(aDefines, function () {
        if (typeof this.Value != "undefined") {
            if (oNode[sColDefine] == this.Value) {
                if (!this.Defines) {
                    define = this;
                    return false;
                }

                define = FindDefine(this.ColDefine, this.Defines, oNode);
            }
            else {
                return true;
            }
        }
        else {
            define = this;
            return false;
        }
    });

    return define;
}