using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.Port
{
	/// <summary>
	/// ��Ա��λ
	/// </summary>
	public class EmpStationAttr  
	{
		#region ��������
		/// <summary>
		/// ������ԱID
		/// </summary>
		public const  string FK_Emp="FK_Emp";
		/// <summary>
		/// ������λ
		/// </summary>
		public const  string FK_Station="FK_Station";		 
		#endregion	
	}
	/// <summary>
    /// ��Ա��λ ��ժҪ˵����
	/// </summary>
    public class EmpStation : Entity
    {
        #region ��������
        /// <summary>
        /// UI�����ϵķ��ʿ���
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
        /// ������ԱID
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(EmpStationAttr.FK_Emp);
            }
            set
            {
                SetValByKey(EmpStationAttr.FK_Emp, value);
            }
        }
        public string FK_StationT
        {
            get
            {
                return this.GetValRefTextByKey(EmpStationAttr.FK_Station);
            }
        }
        /// <summary>
        ///������λ
        /// </summary>
        public string FK_Station
        {
            get
            {
                return this.GetValStringByKey(EmpStationAttr.FK_Station);
            }
            set
            {
                SetValByKey(EmpStationAttr.FK_Station, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ������Ա��λ
        /// </summary> 
        public EmpStation() { }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Port_EmpStation");
                map.EnDesc = "��Ա��λ";
                map.EnType = EnType.Dot2Dot;

                map.AddDDLEntitiesPK(EmpStationAttr.FK_Emp, null, "����Ա", new Emps(), true);
                map.AddDDLEntitiesPK(EmpStationAttr.FK_Station, null, "������λ", new Stations(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// ��Ա��λ 
	/// </summary>
	public class EmpStations : Entities
	{
		#region ����
		/// <summary>
		/// ������Ա��λ
		/// </summary>
		public EmpStations()
		{
		}
        /// <summary>
        /// ������Ա��λ
        /// </summary>
        public EmpStations(string empNo)
        {
            if (BP.Sys.SystemConfig.OSDBSrc == Sys.OSDBSrc.Database)
            {
                this.Retrieve("FK_Emp", BP.Web.WebUser.No);
                return;
            }

            if (BP.Sys.SystemConfig.OSDBSrc == Sys.OSDBSrc.WebServices)
            {
                var ws = DataType.GetPortalInterfaceSoapClientInstance();
                DataTable dt = ws.GetEmpHisStations(Web.WebUser.No);
                foreach (DataRow dr in dt.Rows)
                {
                    EmpStation es = new EmpStation();
                    es.FK_Emp = BP.Web.WebUser.No;
                    es.FK_Station = dr[0].ToString();
                    es.Row["FK_StationText"] = dr[1].ToString();
                    this.AddEntity(es);
                }
                return;
            }
        }
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new EmpStation();
			}
		}	
		#endregion 

		
	}
}
