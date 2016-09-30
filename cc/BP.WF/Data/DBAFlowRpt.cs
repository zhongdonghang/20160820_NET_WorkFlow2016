using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.Rpt;
using BP.DA;
using BP.En;
using BP.Port;
using BP.Web;

namespace BP.WF.Data
{
    public class DBAFlowRpt:BP.Rpt.Rpt2Base
    {
        /// <summary>
        /// 报表的标题(return null 就不显示.)
        /// </summary>
        public override string Title
        {
            get { return "流程分析"; }
        }
        /// <summary>
        /// 默认选择的维度
        /// </summary>
        public override int AttrDefSelected
        {
            get { return 0; }
        }
        /// <summary>
        /// 要分析的属性列表
        /// </summary>
        public override Rpt2Attrs AttrsOfGroup
        {
            get
            {
                Rpt2Attrs attrs = new Rpt2Attrs();

                Rpt2Attr attr = new Rpt2Attr();
                attr.Title = "流程分布分析";
                if (WebUser.No == "admin")
                {
                    attr.DBSrc = "SELECT B.Name as '流程名称', A.FK_Flow as _FK_FLOW, Count(A.WorkID) AS '发起数量' FROM WF_GenerWorkFlow A, WF_Flow B WHERE A.FK_Flow=B.No AND A.WFState!=0 GROUP BY B.Name, A.FK_Flow";
                    attr.DESC = "全部运行的流程分析,<a href=\"javascript:WinOpen('/WF/Comm/Group.aspx?EnsName=BP.WF.Data.GenerWorkFlowViews')\">高级查询/分析</a>."; //报表底部说明,可以为空.
                    attr.DBSrcOfDtl = "SELECT WorkID as '工作ID', FlowName as '流程名称', Title as '标题',  WFSta as '状态', StarterName as '发起人', TodoEmps as '当前处理人' FROM WF_GenerWorkFlow WHERE FK_FLOW='@_FK_FLOW'";
                }
                else
                {
                    attr.DBSrc = "SELECT B.Name as '流程名称', A.FK_Flow as _FK_FLOW, Count(A.WorkID) AS '发起数量' FROM WF_GenerWorkFlow A, WF_Flow B WHERE A.FK_Flow=B.No AND A.WFState!=0 AND A.FK_Dept='@BP.Web.WebUser.FK_Dept' GROUP BY B.Name, A.FK_Flow";
                    attr.DESC = "本部门的流程分析, <a href=\"javascript:WinOpen('/WF/Comm/Group.aspx?EnsName=BP.WF.Data.GenerWorkFlowViews')\">高级查询/分析</a>."; //报表底部说明,可以为空.
                    attr.DBSrcOfDtl = "SELECT WorkID as '工作ID', FlowName as '流程名称', Title as '标题',  WFSta as '状态', StarterName as '发起人', TodoEmps as '当前处理人' FROM WF_GenerWorkFlow WHERE FK_FLOW='@_FK_FLOW' AND A.FK_Dept='@BP.Web.WebUser.FK_Dept'";
                }
                attrs.Add(attr);

                attr = new Rpt2Attr();
                attr.Title = "流程状态统计";
                attr.DBSrc = "SELECT B.Lab as '流程状态', a.WFState as _WFSTATE, Count(A.WorkID)  AS '发起数量' FROM WF_GenerWorkFlow A, Sys_Enum B ";
                attr.DBSrc += " WHERE A.WFState=B.IntKey";
                attr.DBSrc += " AND B.EnumKey='WFState'  ";
                attr.DBSrc += " GROUP BY B.Lab, a.WFState ";
                attr.DESC = ""; //报表底部说明,可以为空.

                //设置明细表信息.
                attr.DBSrcOfDtl = "SELECT WorkID as '工作ID', FlowName as '流程名称', Title as '标题',  WFSta as '状态', StarterName as '发起人', TodoEmps as '当前处理人' FROM WF_GenerWorkFlow WHERE WFSTATE=@_WFSTATE";
                attrs.Add(attr);
                return attrs;
            }
        }
       
    }
}
