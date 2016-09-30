using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Sys;

namespace BP.Port
{
    /// <summary>
    /// ��λ����
    /// </summary>
    public class StationAttr : EntityNoNameAttr
    {
        public const string StaGrade = "StaGrade";
    }
    /// <summary>
    /// ��λ
    /// </summary>
    public class Station : EntityNoName
    {
        #region ʵ�ֻ����ķ���
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        public new string Name
        {
            get
            {
                return this.GetValStrByKey("Name");
            }
        }
        public int Grade
        {
            get
            {
                return this.No.Length / 2;
            }
        }
        public int StaGrade
        {
            get
            {
                return this.GetValIntByKey(StationAttr.StaGrade);
            }
            set
            {
                this.SetValByKey(StationAttr.StaGrade,value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��λ
        /// </summary> 
        public Station()
        {
        }
        /// <summary>
        /// ��λ
        /// </summary>
        /// <param name="no">��λ���</param>
        public Station(string no)
        {
            this.No = no.Trim();
            if (this.No.Length == 0)
                throw new Exception("@Ҫ��ѯ�ĸ�λ���Ϊ�ա�");

            this.Retrieve();
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Port_Station");
                map.EnDesc = "��λ"; // "��λ";
                map.EnType = EnType.Admin;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.CodeStruct = "2222222"; // ��󼶱��� 7 .

                map.AddTBStringPK(EmpAttr.No, null, "���", true, false, 1, 20, 100);
                map.AddTBString(EmpAttr.Name, null, "����", true, false, 0, 100, 100);
                map.AddDDLSysEnum(StationAttr.StaGrade, 0, "����", true, true, StationAttr.StaGrade,
                    "@1=�߲��@2=�в��@3=ִ�и�");

                //��λ��Ա.
                map.AttrsOfOneVSM.Add(new EmpStations(), new Emps(), EmpStationAttr.FK_Station, EmpStationAttr.FK_Emp,
                  DeptAttr.Name, DeptAttr.No, "��Ա");

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion


        #region ��д��ѯ. 2015.09.31 Ϊ��Ӧws�Ĳ�ѯ.
        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <returns></returns>
        public override int Retrieve()
        {
            if (BP.Sys.SystemConfig.OSDBSrc == OSDBSrc.WebServices)
            {
                var v = DataType.GetPortalInterfaceSoapClientInstance();
                DataTable dt= v.GetStation(this.No);
                if (dt.Rows.Count == 0)
                    throw new Exception("@���Ϊ(" + this.No + ")�ĸ�λ�����ڡ�");
                this.Row.LoadDataTable(dt, dt.Rows[0]);
                return 1;
            }
            else
            {
                return base.Retrieve();
            }
        }
        /// <summary>
        /// ��ѯ.
        /// </summary>
        /// <returns></returns>
        public override int RetrieveFromDBSources()
        {
            if (BP.Sys.SystemConfig.OSDBSrc == OSDBSrc.WebServices)
            {
                var v = DataType.GetPortalInterfaceSoapClientInstance();
                DataTable dt = v.GetStation(this.No);
                if (dt.Rows.Count == 0)
                    return 0;
                this.Row.LoadDataTable(dt, dt.Rows[0]);
                return 1;
            }
            else
            {
                return base.RetrieveFromDBSources();
            }
        }
        #endregion
    }
    /// <summary>
    /// ��λs
    /// </summary>
    public class Stations : EntitiesNoName
    {
        /// <summary>
        /// ��λ
        /// </summary>
        public Stations() { }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Station();
            }
        }

        #region ��д��ѯ,add by stone 2015.09.30 Ϊ����Ӧ�ܹ���webservice����Դ��ѯ����.
        /// <summary>
        /// ��д��ѯȫ����Ӧ��WSȡ������Ҫ
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            if (BP.Sys.SystemConfig.OSDBSrc == OSDBSrc.WebServices)
            {
                this.Clear(); //�����������.
                //�������.
                var v = DataType.GetPortalInterfaceSoapClientInstance();
                DataTable dt = v.GetStations();
                if (dt.Rows.Count == 0)
                    return 0;

                //���ò�ѯ.
                QueryObject.InitEntitiesByDataTable(this, dt, null);
                return dt.Rows.Count;
            }
            else
            {
                return base.RetrieveAll();
            }
        }
        /// <summary>
        /// ��д������Դ��ѯȫ����Ӧ��WSȡ������Ҫ
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAllFromDBSource()
        {
            if (BP.Sys.SystemConfig.OSDBSrc == OSDBSrc.WebServices)
            {
                this.Clear(); //�����������.
                //�������.
                var v = DataType.GetPortalInterfaceSoapClientInstance();
                DataTable dt = v.GetStations();
                if (dt.Rows.Count == 0)
                    return 0;

                //���ò�ѯ.
                QueryObject.InitEntitiesByDataTable(this, dt, null);
                return dt.Rows.Count;
            }
            else
            {
                return base.RetrieveAllFromDBSource();
            }
        }
        #endregion ��д��ѯ.
    }
}
