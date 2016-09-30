using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.Sys;

namespace BP.Port
{
    /// <summary>
    /// ��������
    /// </summary>
    public class DeptAttr : EntityNoNameAttr
    {
        /// <summary>
        /// ���ڵ�ı��
        /// </summary>
        public const string ParentNo = "ParentNo";
    }
    /// <summary>
    /// ����
    /// </summary>
    public class Dept : EntityNoName
    {
        #region ����
        /// <summary>
        /// ���ڵ��ID
        /// </summary>
        public string ParentNo
        {
            get
            {
                return this.GetValStrByKey(DeptAttr.ParentNo);
            }
            set
            {
                this.SetValByKey(DeptAttr.ParentNo, value);
            }
        }
        public int Grade
        {
            get
            {
                return 1;
            }
        }
        private Depts _HisSubDepts = null;
        /// <summary>
        /// �����ӽڵ�
        /// </summary>
        public Depts HisSubDepts
        {
            get
            {
                if (_HisSubDepts == null)
                    _HisSubDepts = new Depts(this.No);
                return _HisSubDepts;
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ����
        /// </summary>
        public Dept() { }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="no">���</param>
        public Dept(string no) : base(no) { }
        #endregion

        #region ��д����
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
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN); //���ӵ����Ǹ����ݿ���. (Ĭ�ϵ���: AppCenterDSN )
                map.PhysicsTable = "Port_Dept";
                map.EnType = EnType.Admin;
                map.IsEnableVer = true;

                map.EnDesc = "����"; //  ʵ�������.
                map.DepositaryOfEntity = Depositary.Application; //ʵ��map�Ĵ��λ��.
                map.DepositaryOfMap = Depositary.Application;    // Map �Ĵ��λ��.

                map.AddTBStringPK(DeptAttr.No, null, "���", true, false, 1, 50, 20);
                map.AddTBString(DeptAttr.Name, null, "����", true, false, 0, 100, 30);
                map.AddTBString(DeptAttr.ParentNo, null, "���ڵ���", true, true, 0, 100, 30);


                RefMethod rm = new RefMethod();
                rm.Title = "��ʷ���";
                rm.ClassMethodName = this.ToString() + ".History";
                rm.RefMethodType = RefMethodType.RightFrameOpen;
                map.AddRefMethod(rm);

                #region ���ӵ�Զ�����
                //���Ĳ���Ȩ��
               // map.AttrsOfOneVSM.Add(new DeptStations(), new Stations(), DeptStationAttr.FK_Dept, DeptStationAttr.FK_Station, StationAttr.Name, StationAttr.No, "��λȨ��");
                #endregion 

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public string History()
        {
            return "EnVerDtl.aspx?EnName="+this.ToString()+"&No="+this.No;
        }

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
                DataTable dt = v.GetDept(this.No);
                if (dt.Rows.Count == 0)
                    throw new Exception("@���Ϊ(" + this.No + ")�Ĳ��Ų����ڡ�");
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
                DataTable dt = v.GetDept(this.No);
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
    ///����s
    /// </summary>
    public class Depts : EntitiesNoName
    {
        #region ��ʼ��ʵ��.
        /// <summary>
        /// �õ�һ����ʵ��
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Dept();
            }
        }
        /// <summary>
        /// ���ż���
        /// </summary>
        public Depts()
        {
        }
        /// <summary>
        /// ���ż���
        /// </summary>
        /// <param name="parentNo">������No</param>
        public Depts(string parentNo)
        {
            if (BP.Sys.SystemConfig.OSDBSrc == OSDBSrc.WebServices)
            {
                this.Clear(); //�����������.
                //�������.
                var v = DataType.GetPortalInterfaceSoapClientInstance();
                DataTable dt = v.GetDeptsByParentNo(parentNo);
                //���ò�ѯ.
                QueryObject.InitEntitiesByDataTable(this, dt, null);
            }
            else
            {
                this.Retrieve(DeptAttr.ParentNo, parentNo);
            }
        }
        #endregion ��ʼ��ʵ��.

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
                DataTable dt = v.GetDepts();
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
                DataTable dt = v.GetDepts();
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
