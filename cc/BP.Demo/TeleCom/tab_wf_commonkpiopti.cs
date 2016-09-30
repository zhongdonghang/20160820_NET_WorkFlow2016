using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;

namespace BP.Demo
{
    /// <summary>
    /// 市局 属性
    /// </summary>
    public class tab_commonkpioptiAttr
    {
        #region 基本属性
        /// <summary>
        /// 关联的主键
        /// </summary>
        public const string tab_commonkpiopti_main = "tab_commonkpiopti_main";
        /// <summary>
        /// 流程编号
        /// </summary>
        public const string fk_flow = "fk_flow";
        /// <summary>
        /// WorkID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// 父节点ID
        /// </summary>
        public const string ParentWorkID = "ParentWorkID";
        /// <summary>
        /// regionid
        /// </summary>
        public const string region_id = "region_id";
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


        #endregion
    }
    /// <summary>
    /// 市局
    /// </summary>
    public class tab_commonkpiopti : EntityOID
    {
        #region 属性
        /// <summary>
        /// 发起时间
        /// </summary>
        public int tab_commonkpiopti_main
        {
            get
            {
                return this.GetValIntByKey(tab_commonkpioptiAttr.tab_commonkpiopti_main);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.tab_commonkpiopti_main, value);
            }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string fk_flow
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptiAttr.fk_flow);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.fk_flow, value);
            }
        }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string region_id
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptiAttr.region_id);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.region_id, value);
            }
        }
        /// <summary>
        /// 指标
        /// </summary>
        public string wf_no
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptiAttr.wf_no);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.wf_no, value);
            }
        }
        /// <summary>
        /// 市局负责人
        /// </summary>
        public string wf_category
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptiAttr.wf_category);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.wf_category, value);
            }
        }
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(tab_commonkpioptiAttr.WorkID);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.WorkID, value);
            }
        }
        public Int64 ParentWorkID
        {
            get
            {
                return this.GetValInt64ByKey(tab_commonkpioptiAttr.ParentWorkID);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.ParentWorkID, value);
            }
        }
        /// <summary>
        /// 紧急程度
        /// </summary>
        public int wf_priority
        {
            get
            {
                return this.GetValIntByKey(tab_commonkpioptiAttr.wf_priority);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.wf_priority, value);
            }
        }
        /// <summary>
        /// 发送人
        /// </summary>
        public string wf_send_user
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptiAttr.wf_send_user);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.wf_send_user, value);
            }
        }
        /// <summary>
        /// 处理人部门
        /// </summary>
        public string wf_send_department
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptiAttr.wf_send_department);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.wf_send_department, value);
            }
        }
        /// <summary>
        /// 发起人时间
        /// </summary>
        public string wf_send_time
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptiAttr.wf_send_time);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.wf_send_time, value);
            }
        }
        /// <summary>
        /// 发起人电话
        /// </summary>
        public string wf_send_phone
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptiAttr.wf_send_phone);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.wf_send_phone, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 市局
        /// </summary>
        public tab_commonkpiopti()
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
                Map map = new Map("Demo_commonkpiopti");
                map.EnDesc = "市局";

                map.AddTBIntPKOID();

                map.AddTBInt(tab_commonkpioptiAttr.WorkID, 0, "WorkID", true, false);
                map.AddTBInt(tab_commonkpioptiAttr.ParentWorkID, 0, "父流程ID", true, false);

                map.AddTBInt(tab_commonkpioptiAttr.wf_priority, 0, "紧急程度", true, false);

                map.AddTBString(tab_commonkpioptiAttr.tab_commonkpiopti_main, null, "关联省局ID", true, false, 0, 200, 10);

                map.AddTBString(tab_commonkpioptiAttr.region_id, null, "单据编号", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpioptiAttr.wf_category, null, "类别", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpioptiAttr.wf_no, null, "单据编号", true, false, 0, 200, 10);

                map.AddTBString(tab_commonkpioptiAttr.wf_priority, null, "紧急程度", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpioptiAttr.wf_send_user, null, "发起人", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpioptiAttr.wf_send_department, null, "发起人部门", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpioptiAttr.wf_send_time, null, "发起时间", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpioptiAttr.wf_send_phone, null, "发起人电话", true, false, 0, 200, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 市局s
    /// </summary>
    public class tab_commonkpioptis : EntitiesOID
    {
        #region 方法
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new tab_commonkpiopti();
            }
        }
        /// <summary>
        /// 市局s
        /// </summary>
        public tab_commonkpioptis() { }
        #endregion
    }
}
