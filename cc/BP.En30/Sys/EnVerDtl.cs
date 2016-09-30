using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.Sys
{

    public class EnVerDtlAttr
    {
        #region 基本属性
        public const string EnName = "EnName";
        public const string AttrKey = "AttrKey";
        public const string AttrName = "AttrName";
        public const string OldVal = "OldVal";
        public const string NewVal = "NewVal";
        public const string EnVer = "EnVer";

        public const string RDT = "RDT";
        public const string Rec = "Rec";

        public const string EnNo = "EnNo";

        #endregion
    }
    /// <summary>
    /// 部门岗位对应 的摘要说明。
    /// </summary>
    public class EnVerDtl : EntityMyPK
    {
        #region 基本属性
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
        /// <summary>
        /// 实体名称
        /// </summary>
        public string EnName
        {
            get
            {
                return this.GetValStringByKey(EnVerDtlAttr.EnName);
            }
            set
            {
                SetValByKey(EnVerDtlAttr.EnName, value);
            }
        }
        /// <summary>
        /// 字段
        /// </summary>
        public string AttrKey
        {
            get
            {
                return this.GetValStringByKey(EnVerDtlAttr.AttrKey);
            }

            set {
                SetValByKey(EnVerDtlAttr.AttrKey, value);
            }
        }
        /// <summary>
        /// 实体选中行No
        /// </summary>
        public string EnNo
        {
            get
            {
                return this.GetValStringByKey(EnVerDtlAttr.EnNo);
            }

            set
            {
                SetValByKey(EnVerDtlAttr.EnNo, value);
            }
        }
        /// <summary>
        ///字段名
        /// </summary>
        public string AttrName
        {
            get
            {
                return this.GetValStringByKey(EnVerDtlAttr.AttrName);
            }
            set
            {
                SetValByKey(EnVerDtlAttr.AttrName, value);
            }
        }



        /// <summary>
        /// 旧值
        /// </summary>
        public string OldVal
        {
            get
            {
                return this.GetValStringByKey(EnVerDtlAttr.OldVal);
            }
            set
            {
                SetValByKey(EnVerDtlAttr.OldVal, value);
            }
        }

        /// <summary>
        /// 新值
        /// </summary>
        public string NewVal
        {
            get
            {
                return this.GetValStringByKey(EnVerDtlAttr.NewVal);
            }
            set
            {
                SetValByKey(EnVerDtlAttr.NewVal, value);
            }
        }

        /// <summary>
        /// 版本号
        /// </summary>
        public string EnVer
        {
            get
            {
                return this.GetValStringByKey(EnVerDtlAttr.EnVer);
            }
            set
            {
                SetValByKey(EnVerDtlAttr.EnVer, value);
            }
        }


        public string RDT
        {
            get
            {
                return this.GetValStringByKey(EnVerDtlAttr.RDT);
            }
            set
            {
                SetValByKey(EnVerDtlAttr.RDT, value);
            }
        }
        public string Rec
        {
            get
            {
                return this.GetValStringByKey(EnVerDtlAttr.Rec);
            }
            set
            {
                SetValByKey(EnVerDtlAttr.Rec, value);
            }
        }

        #endregion

        #region 扩展属性

        #endregion

        #region 构造函数
        /// <summary>
        /// 工作部门岗位对应
        /// </summary> 
        public EnVerDtl() { }

        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Sys_EnVerDtl");
                map.EnDesc = "实体修改明细";
                map.EnType = EnType.Dot2Dot; //实体类型，admin 系统管理员表，PowerAble 权限管理表,也是用户表,你要想把它加入权限管理里面请在这里设置。。

                map.AddMyPK();
                map.AddTBString(EnVerDtlAttr.EnName, null, "实体名", true, false, 0, 200, 30);
                map.AddTBString(EnVerDtlAttr.AttrKey, null, "字段", false, false, 0, 100, 1);
                map.AddTBString(EnVerDtlAttr.AttrName, null, "字段名", true, false, 0, 200, 30);
                map.AddTBString(EnVerDtlAttr.OldVal, null, "旧值", true, false, 0, 100, 30);
                map.AddTBString(EnVerDtlAttr.NewVal, null, "新值", true, false, 0, 100, 30);
                map.AddTBString(EnVerDtlAttr.EnNo, null, "选中行编号", true, false, 0, 100, 30);
                map.AddTBString(EnVerDtlAttr.EnVer, null, "版本号(日期)", true, false, 0, 100, 30);

                map.AddTBDateTime(EnVerDtlAttr.RDT, null, "日期", true, false);
                map.AddTBString(EnVerDtlAttr.Rec, null, "版本号", true, false, 0, 100, 30);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// 部门岗位对应 
    /// </summary>
    public class EnVerDtls : EntitiesMyPK
    {
        #region 构造
       
        public EnVerDtls()
        {
        }
     
        public EnVerDtls(string stationNo)
        {
          
        }
        #endregion

        #region 方法
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new EnVerDtl();
            }
        }
        #endregion

    }
}
