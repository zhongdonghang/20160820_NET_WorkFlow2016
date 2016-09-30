using System;
using System.Data.SqlClient;

namespace BP.DA
{
	/// <summary>
	///�����ӵ��ĸ����ϣ�
	///  ���Ǵ���� web.config ���б��ڣ�
	/// </summary>
	public enum DBUrlType
	{ 
		/// <summary>
		/// ��Ӧ�ó���
		/// </summary>
		AppCenterDSN,
		/// <summary>
		/// 1������
		/// </summary>
		DBAccessOfOracle1,
		/// <summary>
		/// 2������
		/// </summary>
		DBAccessOfOracle2,
		/// <summary>
		/// 1������
		/// </summary>
        DBAccessOfMSSQL1,
        /// <summary>
        /// 2������
        /// </summary>
		DBAccessOfMSSQL2,
		/// <summary>
		/// access�����ӣ�
		/// </summary>
		DBAccessOfOLE,
		/// <summary>
		/// ODBC����
		/// </summary>
		DBAccessOfODBC,
        /// <summary>
        /// ����Դ����
        /// </summary>
        DBSrc
	}
	/// <summary>
	/// DBUrl ��ժҪ˵����
	/// </summary>
	public class DBUrl
	{
		/// <summary>
		/// ����
		/// </summary>
		public DBUrl()
		{
		} 
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="type">����type</param>
		public DBUrl(DBUrlType type)
		{
			this.DBUrlType=type;
		}
        /// <summary>
        /// �������ݿ�����
        /// </summary>
        /// <param name="type">����type</param>
        public DBUrl(string dbSrc)
        {
            //���ݿ�����.
            this.DBUrlType =  DA.DBUrlType.DBSrc;

            //���ݿ�����.
            this.HisDBSrc = new BP.Sys.SFDBSrc(dbSrc);
        }

        #region ��������Դ.
        private BP.Sys.SFDBSrc _HisDBSrc = null;
        public BP.Sys.SFDBSrc HisDBSrc
        {
            get
            {
                return _HisDBSrc;
            }
            set
            {
                _HisDBSrc = value;
            }
        }
        #endregion ��������Դ.

        /// <summary>
		/// Ĭ��ֵ
		/// </summary>
		public DBUrlType  _DBUrlType=DBUrlType.AppCenterDSN;
		/// <summary>
		/// Ҫ���ӵĵ��Ŀ⡣
		/// </summary>
		public DBUrlType DBUrlType
		{
			get
			{
				return _DBUrlType;
			}
			set
			{
				_DBUrlType=value;
			}
		}
        public string DBVarStr
        {
            get
            {
                switch (this.DBType)
                {
                    case DBType.Oracle:
                        return ":";
                    case DBType.MySQL:
                    case DBType.MSSQL:
                        return "@";
                    case DBType.Informix:
                        return "?";
                    default:
                        return "@";
                }
            }
        }
		/// <summary>
		/// ���ݿ�����
		/// </summary>
		public DBType DBType
		{
			get
			{
				switch(this.DBUrlType)
				{
					case DBUrlType.AppCenterDSN:
						return DBAccess.AppCenterDBType ; 
					case DBUrlType.DBAccessOfMSSQL1:
                    case DBUrlType.DBAccessOfMSSQL2:
						return DBType.MSSQL;
					case DBUrlType.DBAccessOfOLE:
						return DBType.Access;
					case DBUrlType.DBAccessOfOracle1:
                    case DBUrlType.DBAccessOfOracle2:
						return DBType.Oracle ;
                    case DBUrlType.DBSrc:
                        return this.HisDBSrc.HisDBType;
					default:
						throw new Exception("����ȷ������");
				}
			}
		}
	}

}
