using System;
using System.Threading;
using System.Collections;
using System.Data;
using BP.DA;
using BP.DTS;
using BP.En;
using BP.Web.Controls;
using BP.Web;
using BP.Sys;

namespace BP.WF
{
    /// <summary>
    /// 流程事件基类
    /// </summary>
    abstract public class FlowEventBase:EventBase
    {
        #region 属性.
        public Entity HisEn = null;
        private Row _SysPara = null;
        /// <summary>
        /// 参数
        /// </summary>
        public Row SysPara
        {
            get
            {
                if (_SysPara == null)
                    _SysPara = new Row();
                return _SysPara;
            }
            set
            {
                _SysPara = value;
            }
        }
        /// <summary>
        /// 成功信息
        /// </summary>
        public string SucessInfo = null;
        private string _title = null;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                if (_title == null)
                    _title = "未命名";
                return _title;
            }
            set
            {
                _title = value;
            }
        }
        #endregion 属性.

        #region 系统参数
        /// <summary>
        /// 表单ID
        /// </summary>
        public string FK_Mapdata
        {
            get
            {
                return this.GetValStr("FK_MapData");
            }
        }
        /// <summary>
        /// 事件类型
        /// </summary>
        public string EventType
        {
            get
            {
                return this.GetValStr("EventType");
            }
        }
        #endregion 

        #region 常用属性.
        /// <summary>
        /// 工作ID
        /// </summary>
        public int OID
        {
            get
            {
                return this.GetValInt("OID");
            }
        }
        /// <summary>
        /// 工作ID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                if (this.OID == 0)
                    return this.GetValInt64("WorkID"); /*有可能开始节点的WorkID=0*/
                return this.OID;
            }
        }
        /// <summary>
        /// FID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64("FID");
            }
        }
        /// <summary>
        /// 流程编号
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStr("FK_Flow");
            }
        }
        /// <summary>
        /// 节点编号
        /// </summary>
        public int FK_Node
        {
            get
            {
                try
                {
                    return this.GetValInt("FK_Node");
                }
                catch {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 传过来的WorkIDs集合，子流程.
        /// </summary>
        public string WorkIDs
        {
            get
            {
                return this.GetValStr("WorkIDs");
            }
        }
        /// <summary>
        /// 编号集合s
        /// </summary>
        public string Nos
        {
            get
            {
                return this.GetValStr("Nos");
            }
        }
        #endregion 常用属性.

        /// <summary>
        /// 流程事件基类
        /// </summary>
        public FlowEventBase()
        {
        }
        /// <summary>
        /// 流程编号
        /// </summary>
        abstract public string FlowNo
        {
            get;
        }
    }
}
