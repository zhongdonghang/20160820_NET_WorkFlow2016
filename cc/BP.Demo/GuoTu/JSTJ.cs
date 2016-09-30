using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.Demo
{
	/// <summary>
	/// 产品
	/// </summary>
	public class JSTJAttr: EntityNoNameAttr
	{
		#region 基本属性
		public const  string FK_SF="FK_SF";
        public const string Addr = "Addr";

		#endregion
	}
	/// <summary>
    /// 产品
	/// </summary>
    public class JSTJ : EntityNoName
    {
        #region 基本属性

        #endregion

        #region 构造函数
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
        /// 产品
        /// </summary>		
        public JSTJ() { }
        /// <summary>
        /// 产品
        /// </summary>
        /// <param name="no"></param>
        public JSTJ(string no)
            : base(no)
        {

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

                #region 基本属性
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
                map.PhysicsTable = "Demo_JSTJ";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.IsCheckNoLength = false;
                map.EnDesc = "产品";
                map.EnType = EnType.App;
                map.CodeStruct = "4";
                #endregion

                #region 字段
                map.AddTBStringPK(JSTJAttr.No, null, "编号", true, false, 0, 50, 50);
                map.AddTBString(JSTJAttr.Name, null, "名称", true, false, 0, 50, 200);

                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
        public override Entities GetNewEntities
        {
            get { return new JSTJs(); }
        }
        #endregion
    }
	/// <summary>
	/// 产品
	/// </summary>
	public class JSTJs : EntitiesNoName
	{
		#region 
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new JSTJ();
			}
		}	
		#endregion 

		#region 构造方法
		/// <summary>
		/// 产品s
		/// </summary>
		public JSTJs(){}
		#endregion
	}
	
}
