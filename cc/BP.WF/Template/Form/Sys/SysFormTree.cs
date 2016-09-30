using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.Sys
{
    /// <summary>
    /// ����
    /// </summary>
    public class SysFormTreeAttr : EntityTreeAttr
    {
        /// <summary>
        /// ����Դ
        /// </summary>
        public const string DBSrc = "DBSrc";
        /// <summary>
        /// �Ƿ���Ŀ¼
        /// </summary>
        public const string IsDir = "IsDir";
    }
    /// <summary>
    ///  ��������
    /// </summary>
    public class SysFormTree : EntityNoName
    {
        #region ����.
        /// <summary>
        /// �Ƿ���Ŀ¼
        /// </summary>
        public bool IsDir
        {
            get
            {
                return this.GetValBooleanByKey(SysFormTreeAttr.IsDir);
            }
            set
            {
                this.SetValByKey(SysFormTreeAttr.IsDir, value);
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(SysFormTreeAttr.Idx);
            }
            set
            {
                this.SetValByKey(SysFormTreeAttr.Idx, value);
            }
        }
        /// <summary>
        /// ���ڵ���
        /// </summary>
        public string ParentNo
        {
            get
            {
                return this.GetValStringByKey(SysFormTreeAttr.ParentNo);
            }
            set
            {
                this.SetValByKey(SysFormTreeAttr.ParentNo, value);
            }
        }
        /// <summary>
        /// ����Դ
        /// </summary>
        public string DBSrc
        {
            get
            {
                return this.GetValStringByKey(SysFormTreeAttr.DBSrc);
            }
            set
            {
                this.SetValByKey(SysFormTreeAttr.DBSrc, value);
            }
        }
        #endregion ����.


        #region ���췽��
        /// <summary>
        /// ��������
        /// </summary>
        public SysFormTree()
        {
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="_No"></param>
        public SysFormTree(string _No) : base(_No) { }
        #endregion

        #region ϵͳ����.
        /// <summary>
        /// ��������Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Sys_FormTree");
                map.EnDesc = "����";
                map.CodeStruct = "2";

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(SysFormTreeAttr.No, null, "���", true, true, 1, 10, 20);
                map.AddTBString(SysFormTreeAttr.Name, null, "����", true, false, 0, 100, 30);
                map.AddTBString(SysFormTreeAttr.ParentNo, null, "���ڵ�No", false, false, 0, 100, 30);
                map.AddTBString(SysFormTreeAttr.DBSrc, "local", "����Դ", false, false, 0, 100, 30);

                map.AddTBInt(SysFormTreeAttr.IsDir, 0, "�Ƿ���Ŀ¼?", false, false);
                map.AddTBInt(SysFormTreeAttr.Idx, 0, "Idx", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion ϵͳ����.

        protected override bool beforeDelete()
        {
            if (!string.IsNullOrEmpty(this.No))
                DeleteChild(this.No);
            return base.beforeDelete();
        }
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="parentNo"></param>
        private void DeleteChild(string parentNo)
        {
            SysFormTrees formTrees = new SysFormTrees();
            formTrees.RetrieveByAttr(SysFormTreeAttr.ParentNo, parentNo);
            foreach (SysFormTree item in formTrees)
            {
                MapData md = new MapData();
                md.FK_FormTree = item.No;
                md.Delete();
                DeleteChild(item.No);
            }
        }
        public SysFormTree DoCreateSameLevelNode()
        {
            SysFormTree en = new SysFormTree();
            en.Copy(this);
            en.No = BP.DA.DBAccess.GenerOID().ToString();
            en.Name = "�½��ڵ�";
            en.Insert();
            return en;
        }
        public EntityNoName DoCreateSubNode()
        {
            SysFormTree en = new SysFormTree();
            en.Copy(this);
            en.No = BP.DA.DBAccess.GenerOID().ToString();
            en.ParentNo = this.No;
            en.Name = "�½��ڵ�";
            en.Insert();
            return en;
        }
        public void DoUp()
        {
            this.Idx = this.Idx --;
            this.DirectUpdate();
        }
        public void DoDown()
        {
            this.Idx = this.Idx++;
            this.DirectUpdate();
        }
    }
    /// <summary>
    /// ��������
    /// </summary>
    public class SysFormTrees : EntitiesNoName
    {
        /// <summary>
        /// ��������s
        /// </summary>
        public SysFormTrees() { }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new SysFormTree();
            }

        }
        public override int RetrieveAll()
        {
            int i = base.RetrieveAll();
            if (i == 0)
            {
                SysFormTree fs = new SysFormTree();
                fs.Name = "������";
                fs.No = "01";
                fs.Insert();

                fs = new SysFormTree();
                fs.Name = "�칫��";
                fs.No = "02";
                fs.Insert();
                i = base.RetrieveAll();
            }
            return i;
        }
    }
}
