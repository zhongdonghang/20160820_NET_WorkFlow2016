using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;

namespace BP.BPMN
{
    /// <summary>
    /// 流程属性属性
    /// </summary>
    public class FlowAttr : BP.En.EntityNoNameAttr
    {
        /// <summary>
        /// 是否需要送达
        /// </summary>
        public const string IsNeedSend = "IsNeedSend";
        /// <summary>
        /// 为生成单据使用
        /// </summary>
        public const string IDX = "IDX";
        /// <summary>
        /// 要排除的字段
        /// </summary>
        public const string ExpField = "ExpField";
        /// <summary>
        /// 要替换的值
        /// </summary>
        public const string ReplaceVal = "ReplaceVal";
        /// <summary>
        /// 单据类型
        /// </summary>
        public const string FK_FlowSort = "FK_FlowSort";
        /// <summary>
        /// 图形数据
        /// </summary>
        public const string Graph = "Graph";
    }
    /// <summary>
    /// 流程属性
    /// </summary>
    public class Flow : EntityNoName
    {
        #region  属性
        /// <summary>
        /// 单据类型
        /// </summary>
        public string FK_FlowSort
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.FK_FlowSort);
            }
            set
            {
                this.SetValByKey(FlowAttr.FK_FlowSort, value);
            }
        }
        /// <summary>
        /// 流程图数据
        /// </summary>
        public string FlowJson
        {
            get
            {
                return this.GetBigTextFromDB("FlowJson");
            }
            set
            {
                this.SaveBigTxtToDB("FlowJson", value);
            }
        }
        /// <summary>
        /// UI界面上的访问控制
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
    
        #endregion

        #region 构造函数
        /// <summary>
        /// 流程属性
        /// </summary>
        public Flow() { }
        public Flow(string no)
            : base(no.Replace("\n", "").Trim())
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
                Map map = new Map("WF_Flow");
                map.EnDesc = "流程属性"; // "流程属性";
                map.EnType = EnType.Admin;
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "6";

                map.AddTBStringPK(FlowAttr.No, null, "No", true, false, 1, 300, 6);
                map.AddTBString(FlowAttr.Name, null, "Name", true, false, 0, 200, 20);
                map.AddTBString(FlowAttr.FK_FlowSort, null, "类别", true, false, 0, 200, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public string GenerBPMN20Format()
        {
            return "";
        }
    }
    /// <summary>
    /// 流程属性s
    /// </summary>
    public class Flows : EntitiesNoName
    {
        #region 构造
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Flow();
            }
        }
        /// <summary>
        /// 流程属性
        /// </summary>
        public Flows()
        {
        }
        #endregion

        #region 查询与构造
        #endregion 查询与构造

    }

}
