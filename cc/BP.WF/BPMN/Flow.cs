using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;

namespace BP.BPMN
{
    /// <summary>
    /// ������������
    /// </summary>
    public class FlowAttr : BP.En.EntityNoNameAttr
    {
        /// <summary>
        /// �Ƿ���Ҫ�ʹ�
        /// </summary>
        public const string IsNeedSend = "IsNeedSend";
        /// <summary>
        /// Ϊ���ɵ���ʹ��
        /// </summary>
        public const string IDX = "IDX";
        /// <summary>
        /// Ҫ�ų����ֶ�
        /// </summary>
        public const string ExpField = "ExpField";
        /// <summary>
        /// Ҫ�滻��ֵ
        /// </summary>
        public const string ReplaceVal = "ReplaceVal";
        /// <summary>
        /// ��������
        /// </summary>
        public const string FK_FlowSort = "FK_FlowSort";
        /// <summary>
        /// ͼ������
        /// </summary>
        public const string Graph = "Graph";
    }
    /// <summary>
    /// ��������
    /// </summary>
    public class Flow : EntityNoName
    {
        #region  ����
        /// <summary>
        /// ��������
        /// </summary>
        public string FK_FlowSort
        {
            get
            {
                return this.GetValStringByKey(FlowAttr.FK_FlowSort);
            }
            set
            {
                this.SetValByKey(FlowAttr.FK_FlowSort, value);
            }
        }
        /// <summary>
        /// ����ͼ����
        /// </summary>
        public string FlowJson
        {
            get
            {
                return this.GetBigTextFromDB("FlowJson");
            }
            set
            {
                this.SaveBigTxtToDB("FlowJson", value);
            }
        }
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
    
        #endregion

        #region ���캯��
        /// <summary>
        /// ��������
        /// </summary>
        public Flow() { }
        public Flow(string no)
            : base(no.Replace("\n", "").Trim())
        {
        }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_Flow");
                map.EnDesc = "��������"; // "��������";
                map.EnType = EnType.Admin;
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "6";

                map.AddTBStringPK(FlowAttr.No, null, "No", true, false, 1, 300, 6);
                map.AddTBString(FlowAttr.Name, null, "Name", true, false, 0, 200, 20);
                map.AddTBString(FlowAttr.FK_FlowSort, null, "���", true, false, 0, 200, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public string GenerBPMN20Format()
        {
            return "";
        }
    }
    /// <summary>
    /// ��������s
    /// </summary>
    public class Flows : EntitiesNoName
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Flow();
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public Flows()
        {
        }
        #endregion

        #region ��ѯ�빹��
        #endregion ��ѯ�빹��

    }

}
