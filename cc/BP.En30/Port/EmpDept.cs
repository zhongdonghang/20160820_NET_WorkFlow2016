using System;
using System.Data;
using BP.DA;
using BP.En;


namespace BP.Port
{
	/// <summary>
	/// ��Ա���Ŷ�Ӧ -����
	/// </summary>
	public class EmpDeptAttr  
	{
		#region ��������
		/// <summary>
		/// ������ԱID
		/// </summary>
		public const  string FK_Emp="FK_Emp";
		/// <summary>
		/// ����
		/// </summary>
		public const  string FK_Dept="FK_Dept";		 
		#endregion	
	}
	/// <summary>
    /// ��Ա���Ŷ�Ӧ
	/// </summary>
	public class EmpDept :EntityMM
    {
        /// <summary>
        /// ��дʵ��Ȩ��.
        /// </summary>
        public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				if (BP.Web.WebUser.No== "admin"   )
				{
					uac.IsView=true;
					uac.IsDelete=true;
					uac.IsInsert=true;
					uac.IsUpdate=true;
					uac.IsAdjunct=true;
				}
				return uac;
			}
		}

		#region ��������
		/// <summary>
		/// ������ԱID
		/// </summary>
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(EmpDeptAttr.FK_Emp);
			}
			set
			{
				SetValByKey(EmpDeptAttr.FK_Emp,value);
			}
		}
        public string FK_DeptT
        {
            get
            {
                return this.GetValRefTextByKey(EmpDeptAttr.FK_Dept);
            }
        }
		/// <summary>
		///����
		/// </summary>
		public string FK_Dept
		{
			get
			{
				return this.GetValStringByKey(EmpDeptAttr.FK_Dept);
			}
			set
			{
				SetValByKey(EmpDeptAttr.FK_Dept,value);
			}
		}		  
		#endregion
	 

		#region ���캯��
		/// <summary>
		/// ������Ա��λ
		/// </summary> 
		public EmpDept()
        {
        }
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				
				Map map = new Map("Port_EmpDept");
				map.EnDesc="������Ա���Ŷ�Ӧ��Ϣ";	
				map.EnType=EnType.Dot2Dot; //ʵ�����ͣ�admin ϵͳ����Ա��PowerAble Ȩ�޹����,Ҳ���û���,��Ҫ���������Ȩ�޹������������������á���

             //   map.AddTBStringPK(EmpDeptAttr.FK_Emp, null, "Emp", false, false, 1, 15,1);
                //map.AddTBStringPK(EmpDeptAttr.FK_Dept, null, "Dept", false, false, 1, 15,1);
                //map.AddDDLEntitiesPK(EmpDeptAttr.FK_Emp,null,"����Ա",new Emps(),true);

                map.AddTBStringPK(EmpDeptAttr.FK_Emp, null, "����Ա", false, false, 1, 20, 1);
				map.AddDDLEntitiesPK(EmpDeptAttr.FK_Dept,null,"����",new Depts(),true);


                //map.AddDDLEntitiesPK(EmpDeptAttr.FK_Emp,0, DataType.AppInt,"����Ա",new �ؾ�(),"OID","Name",true);
				//map.AddSearchAttr(EmpDeptAttr.FK_Emp);
				//map.AddSearchAttr(EmpDeptAttr.FK_Dept);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion
	}
	/// <summary>
    /// ��Ա���Ŷ�Ӧs -���� 
	/// </summary>
	public class EmpDepts : EntitiesMM
	{
		#region ����
		/// <summary>
		/// ������Ա�벿�ż���
		/// </summary>
		public EmpDepts(){}
		#endregion

		#region ��д����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new EmpDept();
			}
		}	
		#endregion 

		 
				
	}
	
}
