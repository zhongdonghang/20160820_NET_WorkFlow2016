using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;

namespace BP.Sys
{
    /// <summary>
    /// 实体版本号属性
    /// </summary>
    public class EnVerAttr : EntityNoNameAttr
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public const string EVer = "EVer";
        /// <summary>
        /// 记录人
        /// </summary>
        public const string Rec = "Rec";
        /// <summary>
        /// 记录日期
        /// </summary>
        public const string RDT = "RDT";
    }
    /// <summary>
    /// 实体版本号
    /// </summary>
    public class EnVer : EntityNoName
    {
        #region 属性
        /// <summary>
        /// 版本号
        /// </summary>
        public string EVer
        {
            get
            {
                return this.GetValStrByKey(EnVerAttr.EVer);
            }
            set
            {
                this.SetValByKey(EnVerAttr.EVer, value);
            }
        }
        /// <summary>
        /// 修改人
        /// </summary>
        public string Rec
        {
            get
            {
                return this.GetValStrByKey(EnVerAttr.Rec);
            }
            set
            {
                this.SetValByKey(EnVerAttr.Rec, value);
            }
        }

        /// <summary>
        /// 修改日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStrByKey(EnVerAttr.RDT);
            }
            set
            {
                this.SetValByKey(EnVerAttr.RDT, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 实体版本号
        /// </summary>
        public EnVer() { }
        /// <summary>
        /// 实体版本号
        /// </summary>
        /// <param name="no">编号</param>
        public EnVer(string no) : base(no) { }
        #endregion

        #region 重写方法
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
        /// Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map();
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN); //连接到的那个数据库上. (默认的是: AppCenterDSN )
                map.PhysicsTable = "Sys_EnVer";
                map.EnType = EnType.Admin;
                map.EnDesc = "实体版本号"; //  实体的描述.
                map.DepositaryOfEntity = Depositary.Application; //实体map的存放位置.
                map.DepositaryOfMap = Depositary.Application;    // Map 的存放位置.
              
                map.AddTBStringPK(EnVerAttr.No, null, "实体类", true, false, 1, 50, 20);
                map.AddTBString(EnVerAttr.Name, null, "实体名", true, false, 0, 100, 30);
                map.AddTBString(EnVerAttr.EVer, null, "版本号", true, true, 0, 100, 30);
                map.AddTBString(EnVerAttr.Rec, null, "修改人", true, true, 0, 100, 30);
                map.AddTBDateTime(EnVerAttr.RDT, null, "修改日期", true, true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    ///实体版本号s
    /// </summary>
    public class EnVers : EntitiesNoName
    {
        /// <summary>
        /// 得到一个新实体
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new EnVer();
            }
        }
        /// <summary>
        /// 实体版本号集合
        /// </summary>
        public EnVers()
        {
        }
    }
}
