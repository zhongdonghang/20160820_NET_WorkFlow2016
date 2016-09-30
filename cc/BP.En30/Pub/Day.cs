using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Pub
{
	/// <summary>
	/// ����
	/// </summary>
	public class Day :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����
		 
		/// <summary>
		/// �����
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_Day";
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public override string  Desc
		{
			get
			{
                return  "����";  // "����";
			}
		}
		#endregion 

		#region ���췽��
		public Day()
        {
        }
        /// <summary>
        /// _No
        /// </summary>
        /// <param name="_No"></param>
		public Day(string _No ): base(_No)
        {
        }
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class Days :SimpleNoNameFixs
	{
		/// <summary>
		/// ���ڼ���
		/// </summary>
		public Days()
		{
		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Day();
			}
		}
        public override int RetrieveAll()
        {
            int num= base.RetrieveAll();

            if (num != 12)
            {
                BP.DA.DBAccess.RunSQL("DELETE FROM Pub_Day ");

                for (int i = 1; i <= 31; i++)
                {
                    BP.Pub.Day yf = new Day();
                    yf.No = i.ToString().PadLeft( 2,'0');
                    yf.Name = i.ToString().PadLeft(2, '0');
                    yf.Insert();
                }

               return base.RetrieveAll();
            }
            return 12;
        }
	}
}
