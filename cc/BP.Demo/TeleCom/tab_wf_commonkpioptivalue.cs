using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;

namespace BP.Demo
{
    /// <summary>
    /// 设备 属性
    /// </summary>
    public class tab_commonkpioptivalueAttr:EntityOIDAttr
    {
        #region 基本属性
        /// <summary>
        /// 与市局关联的id
        /// </summary>
        public const string wf_commonkpioptivalue_id = "wf_commonkpioptivalue_id";
        /// <summary>
        /// kpi_id
        /// </summary>
        public const string kpi_id = "kpi_id";
        /// <summary>
        /// WorkID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// ParentWorkID
        /// </summary>
        public const string ParentWorkID = "ParentWorkID";
        /// <summary>
        /// 流程编号
        /// </summary>
        public const string fk_flow = "fk_flow";
        /// <summary>
        /// 业务字段
        /// </summary>
        public const string region_id = "region_id";
        /// <summary>
        /// 业务字段1
        /// </summary>
        public const string remark = "remark";
        /// <summary>
        /// 负责人
        /// </summary>
        public const string fuzeren = "fuzeren";
        /// <summary>
        /// 位置
        /// </summary>
        public const string addr = "addr";
        #endregion
    }
    /// <summary>
    /// 设备
    /// </summary>
    public class tab_commonkpioptivalue : EntityOID
    {
        #region 属性
        /// <summary>
        /// wf_commonkpioptivalue_id
        /// </summary>
        public int wf_commonkpioptivalue_id
        {
            get
            {
                return this.GetValIntByKey(tab_commonkpioptivalueAttr.wf_commonkpioptivalue_id);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptivalueAttr.wf_commonkpioptivalue_id, value);
            }
        }
        /// <summary>
        /// 流程编号
        /// </summary>
        public string fk_flow
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptivalueAttr.fk_flow);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptivalueAttr.fk_flow, value);
            }
        }
      
        /// <summary>
        /// WorkID
        /// </summary>
        public int WorkID
        {
            get
            {
                return this.GetValIntByKey(tab_commonkpioptivalueAttr.WorkID);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptivalueAttr.WorkID, value);
            }
        }
        /// <summary>
        /// ParentWorkID
        /// </summary>
        public int ParentWorkID
        {
            get
            {
                return this.GetValIntByKey(tab_commonkpioptivalueAttr.ParentWorkID);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptivalueAttr.ParentWorkID, value);
            }
        }
        #endregion

        #region 业务字段
        public string region_id
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptivalueAttr.region_id);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptivalueAttr.region_id, value);
            }
        }
        public string addr
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptivalueAttr.addr);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptivalueAttr.addr, value);
            }
        }
        public string remark
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptivalueAttr.remark);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptivalueAttr.remark, value);
            }
        }
        public string fuzeren
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptivalueAttr.fuzeren);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptivalueAttr.fuzeren, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 设备
        /// </summary>
        public tab_commonkpioptivalue()
        {
        }
    
        public tab_commonkpioptivalue(Int64 WorkID)
        {
            this.Retrieve(tab_commonkpioptivalueAttr.WorkID, WorkID);
        }
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Demo_commonkpioptivalue");
                map.EnDesc = "设备";

                map.AddTBIntPKOID();
                map.AddTBInt(tab_commonkpioptivalueAttr.wf_commonkpioptivalue_id, 0,
                    "wf_commonkpioptivalue_id", true, false);

                map.AddTBInt(tab_commonkpioptivalueAttr.WorkID, 0, "工作ID", true, false);
                map.AddTBInt(tab_commonkpioptivalueAttr.ParentWorkID, 0, "父节点ID", true, false);

                map.AddTBString(tab_commonkpioptivalueAttr.fk_flow, null, "fk_flow", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpioptivalueAttr.fuzeren, null, "负责人", true, false, 0, 200, 10);

                map.AddTBString(tab_commonkpioptivalueAttr.kpi_id, null, "kpi_id", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpioptivalueAttr.region_id, null, "region_id", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpioptivalueAttr.remark, null, "remark", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpioptivalueAttr.addr, null, "addr", true, false, 0, 200, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 设备s
    /// </summary>
    public class tab_commonkpioptivalues : EntitiesOID
    {
        #region 方法
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new tab_commonkpioptivalue();
            }
        }
        /// <summary>
        /// 设备s
        /// </summary>
        public tab_commonkpioptivalues() { }
        #endregion
    }
}
