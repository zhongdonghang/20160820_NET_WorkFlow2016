using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;
using BP.Sys;

namespace BP.BPMN
{
    /// <summary>
    /// �����������
    /// </summary>
    public class FlowSortAttr : EntityTreeAttr
    {
    }
    /// <summary>
    ///  �������
    /// </summary>
    public class FlowSort : EntityTree
    {
        #region ���췽��
        /// <summary>
        /// �������
        /// </summary>
        public FlowSort()
        {
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="_No"></param>
        public FlowSort(string _No) : base(_No) { }
        #endregion

        /// <summary>
        /// �������Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_FlowSort");
                map.EnDesc = "�������";
                map.CodeStruct = "2";
                map.IsAllowRepeatName = false;
                 

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(FlowSortAttr.No, null, "���", true, true, 1, 10, 20);
                map.AddTBString(FlowSortAttr.Name, null, "����", true, false, 0, 100, 30);
                map.AddTBString(FlowSortAttr.ParentNo, null, "���ڵ�No", false, false, 0, 100, 30);
                map.AddTBString(FlowSortAttr.TreeNo, null, "TreeNo", false, false, 0, 100, 30);

                map.AddTBInt(FlowSortAttr.Idx, 0, "Idx", false, false);
                map.AddTBInt(FlowSortAttr.IsDir, 0, "IsDir", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }

        #region ��д����
        #endregion ��д����

    }
    /// <summary>
    /// �������
    /// </summary>
    public class FlowSorts : EntitiesTree
    {
        /// <summary>
        /// �������s
        /// </summary>
        public FlowSorts() { }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FlowSort();
            }

        }
        /// <summary>
        /// �������s
        /// </summary>
        /// <param name="no">ss</param>
        /// <param name="name">anme</param>
        public void AddByNoName(string no, string name)
        {
            FlowSort en = new FlowSort();
            en.No = no;
            en.Name = name;
            this.AddEntity(en);
        }
        public override int RetrieveAll()
        {
            int i = base.RetrieveAll();
            if (i == 0)
            {
                FlowSort fs = new FlowSort();
                fs.Name = "������";
                fs.No = "01";
                fs.Insert();

                fs = new FlowSort();
                fs.Name = "�칫��";
                fs.No = "02";
                fs.Insert();
                i = base.RetrieveAll();
            }

            return i;
        }
    }
}
