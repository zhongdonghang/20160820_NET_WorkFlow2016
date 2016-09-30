using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;

namespace BP.Demo
{
    /// <summary>
    /// 省局 属性
    /// </summary>
    public class tab_commonkpiopti_mainAttr
    {
        #region 基本属性
        /// <summary>
        /// 流程编号
        /// </summary>
        public const string fk_flow = "fk_flow";
        /// <summary>
        /// 工作ID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// 标题
        /// </summary>
        public const string wf_title = "wf_title";
        /// <summary>
        /// 单据编号
        /// </summary>
        public const string wf_no = "wf_no";
        /// <summary>
        /// 流程类别
        /// </summary>
        public const string wf_category = "wf_category";
        /// <summary>
        /// 紧急程度
        /// </summary>
        public const string wf_priority = "wf_priority";
        /// <summary>
        /// 发起人
        /// </summary>
        public const string wf_send_user = "wf_send_user";
        /// <summary>
        /// 发起人部门
        /// </summary>
        public const string wf_send_department = "wf_send_department";
        /// <summary>
        /// 发起时间
        /// </summary>
        public const string wf_send_time = "wf_send_time";
        /// <summary>
        /// 发起人电话
        /// </summary>
        public const string wf_send_phone = "wf_send_phone";
        /// <summary>
        /// 技术信息
        /// </summary>
        public const string techology = "techology";
        /// <summary>
        /// KIPID
        /// </summary>
        public const string kpi_id = "kpi_id";
        /// <summary>
        /// 
        /// </summary>
        public const string threshold = "threshold";
        #endregion
    }
    /// <summary>
    /// 省局
    /// </summary>
    public class tab_commonkpiopti_main : EntityOID
    {
        #region 属性
        /// <summary>
        /// 发起时间
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(tab_commonkpiopti_mainAttr.WorkID);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.WorkID, value);
            }
        }
        /// <summary>
        /// 流程编号
        /// </summary>
        public string fk_flow
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.fk_flow);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.fk_flow, value);
            }
        }
        /// <summary>
        /// 发起人
        /// </summary>
        public string wf_send_user
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.wf_send_user);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.wf_send_user, value);
            }
        }
        /// <summary>
        /// 流程类别
        /// </summary>
        public string wf_category
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.wf_category);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.wf_category, value);
            }
        }
        /// <summary>
        /// 紧急程度
        /// </summary>
        public int wf_priority
        {
            get
            {
                return this.GetValIntByKey(tab_commonkpiopti_mainAttr.wf_priority);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.wf_priority, value);
            }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string wf_title
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.wf_title);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.wf_title, value);
            }
        }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string wf_no
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.wf_no);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.wf_no, value);
            }
        }
        /// <summary>
        /// 发起人部门
        /// </summary>
        public string wf_send_department
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.wf_send_department);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.wf_send_department, value);
            }
        }
        /// <summary>
        /// 发起时间
        /// </summary>
        public string wf_send_time
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.wf_send_time);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.wf_send_time, value);
            }
        }
        /// <summary>
        /// 发起人电话
        /// </summary>
        public string wf_send_phone
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.wf_send_phone);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.wf_send_phone, value);
            }
        }
        /// <summary>
        /// 技术信息
        /// </summary>
        public string techology
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.techology);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.techology, value);
            }
        }
        /// <summary>
        /// kpi_id
        /// </summary>
        public string kpi_id
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.kpi_id);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.kpi_id, value);
            }
        }
        /// <summary>
        /// threshold
        /// </summary>
        public string threshold
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.threshold);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.threshold, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 省局
        /// </summary>
        public tab_commonkpiopti_main()
        {
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
                Map map = new Map("Demo_commonkpiopti_main");
                map.EnDesc = "省局";

                map.AddTBIntPKOID();

                map.AddTBInt(tab_commonkpiopti_mainAttr.WorkID, 0, "工作ID", true, false);

                map.AddTBString(tab_commonkpiopti_mainAttr.fk_flow, null, "流程编号", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.wf_title, null, "标题", true, false, 0, 200, 10);

                map.AddTBString(tab_commonkpiopti_mainAttr.wf_no, null, "单据编号", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.wf_category, null, "流程类别", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.wf_priority, null, "紧急程度", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.wf_send_user, null, "发起人", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.wf_send_department, null, "发起人部门", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.wf_send_time, null, "发起时间", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.wf_send_phone, null, "发起人电话", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.techology, null, "技术信息", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.kpi_id, null, "kpi_id", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.threshold, null, "threshold", true, false, 0, 200, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 省局s
    /// </summary>
    public class tab_commonkpiopti_mains : EntitiesOID
    {
        #region 方法
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new tab_commonkpiopti_main();
            }
        }
        /// <summary>
        /// 省局s
        /// </summary>
        public tab_commonkpiopti_mains() { }
        #endregion
    }
}
