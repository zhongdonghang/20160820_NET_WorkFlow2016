using System;
using System.Threading;
using System.Collections;
using BP.Web.Controls;
using System.Data;
using BP.DA;
using BP.DTS;
using BP.En;
using BP.Web;
using BP.Sys;
using BP.WF;

namespace BP.FlowEvent
{
    /// <summary>
    /// 报销流程001
    /// </summary>
    public class F001: BP.WF.FlowEventBase
    {
        #region 属性.
        /// <summary>
        /// 重写流程标记
        /// </summary>
        public override string FlowMark
        {
            get { return "BaoXiao";   }
        }
        #endregion 属性.

        #region 构造.
        /// <summary>
        /// 报销流程事件
        /// </summary>
        public F001()
        {
        }
        #endregion 属性.

        /// <summary>
        /// 重写发送前事件
        /// </summary>
        /// <returns></returns>
        public override string SendWhen()
        {
            switch (this.HisNode.NodeID)
            {
                case 101:
                   
                    //string empstr = "zhangsan";
                    ////更新合计小写 , 把合计转化成大写.
                    //string sql = "UPDATE ND101 SET JIeshourn='" + empstr + "'  WHERE OID=" + this.OID;
                    //BP.DA.DBAccess.RunSQL(sql);
                    ////this.ND101_SaveAfter();
                    break;
                default:
                    break;
            }
            return base.SendWhen();
        }

        /// <summary>
        /// 保存后执行的事件
        /// </summary>
        /// <returns></returns>
        public override string SaveAfter()
        {
            switch (this.HisNode.NodeID)
            {
                case 101:
                    this.ND101_SaveAfter();
                    break;
                default:
                    break;
            }
            return base.SaveAfter();
        }
        /// <summary>
        /// 节点保存后事件
        /// 方法命名规则为:ND+节点名_事件名.
        /// </summary>
        public void ND101_SaveAfter()
        {
            //求出明细表的合计.
            float hj = BP.DA.DBAccess.RunSQLReturnValFloat("SELECT SUM(XiaoJi) as Num FROM ND101Dtl1 WHERE RefPK=" + this.OID, 0);

            //更新合计小写 , 把合计转化成大写.
            string sql = "UPDATE ND101 SET DaXie='" + BP.DA.DataType.ParseFloatToCash(hj) + "',HeJi="+hj+"  WHERE OID=" + this.OID;
            BP.DA.DBAccess.RunSQL(sql);

             

            //if (1 == 2)
            //    throw new Exception("@执行错误xxxxxx.");
            //如果你要向用户提示执行成功的信息，就给他赋值，否则就不必赋值。
            //this.SucessInfo = "执行成功提示.";
        }
        /// <summary>
        /// 发送成功事件，发送成功时，把流程的待办写入其他系统里.
        /// </summary>
        /// <returns>返回执行结果，如果返回null就不提示。</returns>
        public override string SendSuccess()
        {
            try
            {
                // 组织必要的变量.
                Int64 workid = this.WorkID; // 工作id.
                string flowNo = this.HisNode.FK_Flow; // 流程编号.
                int currNodeID = this.SendReturnObjs.VarCurrNodeID; //当前节点id
                int toNodeID = this.SendReturnObjs.VarToNodeID; // 到达节点id.
                string toNodeName = this.SendReturnObjs.VarToNodeName; // 到达节点名称。
                string acceptersID = this.SendReturnObjs.VarAcceptersID; // 接受人员id, 多个人员会用 逗号分看 ,比如 zhangsan,lisi。
                string acceptersName = this.SendReturnObjs.VarAcceptersName; // 接受人员名称，多个人员会用逗号分开比如:张三,李四.

                //执行向其他系统写入待办.
                /*
                 * 在这里需要编写你的业务逻辑，根据上面组织的变量.
                 * 
                 */

                //返回.
                return base.SendSuccess();
            }
            catch(Exception ex)
            {
                throw new Exception("向其他系统写入待办失败，详细信息："+ex.Message);
            }
        }
    }
}
