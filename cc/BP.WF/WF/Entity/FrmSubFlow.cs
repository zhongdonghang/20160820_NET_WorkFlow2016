using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.WF.Template;
using BP.WF;
namespace BP.Sys
{
    /// <summary>
    /// �������̿ؼ�״̬
    /// </summary>
    public enum FrmSubFlowSta
    {
        /// <summary>
        /// ������
        /// </summary>
        Disable,
        /// <summary>
        /// ����
        /// </summary>
        Enable,
        /// <summary>
        /// ֻ��
        /// </summary>
        Readonly
    }
    /// <summary>
    /// ��������
    /// </summary>
    public class FrmSubFlowAttr : EntityNoAttr
    {
        /// <summary>
        /// ״̬
        /// </summary>
        public const string SFSta = "SFSta";
        /// <summary>
        /// X
        /// </summary>
        public const string SF_X = "SF_X";
        /// <summary>
        /// Y
        /// </summary>
        public const string SF_Y = "SF_Y";
        /// <summary>
        /// H
        /// </summary>
        public const string SF_H = "SF_H";
        /// <summary>
        /// W
        /// </summary>
        public const string SF_W = "SF_W";
        /// <summary>
        /// Ӧ������
        /// </summary>
        public const string SFType = "SFType";
        /// <summary>
        /// ����
        /// </summary>
        public const string SFAth = "SFAth";
        /// <summary>
        /// ��ʾ��ʽ.
        /// </summary>
        public const string SFShowModel = "SFShowModel";
        /// <summary>
        /// �켣ͼ�Ƿ���ʾ?
        /// </summary>
        public const string SFTrackEnable = "SFTrackEnable";
        /// <summary>
        /// ��ʷ�����Ϣ�Ƿ���ʾ?
        /// </summary>
        public const string SFListEnable = "SFListEnable";
        /// <summary>
        /// �Ƿ���ʾ���еĲ��裿
        /// </summary>
        public const string SFIsShowAllStep = "SFIsShowAllStep";
        /// <summary>
        /// Ĭ�������Ϣ
        /// </summary>
        public const string SFDefInfo = "SFDefInfo";
        /// <summary>
        /// ����������
        /// </summary>
        public const string SFActiveFlows = "SFActiveFlows";
        /// <summary>
        /// ����
        /// </summary>
        public const string SFCaption = "SFCaption";
        /// <summary>
        /// ����û�δ����Ƿ���Ĭ�������䣿
        /// </summary>
        public const string SFIsFullInfo = "SFIsFullInfo";
        /// <summary>
        /// ��������(��ˣ��󶨣����ģ���ʾ)
        /// </summary>
        public const string SFOpLabel = "SFOpLabel";
        /// <summary>
        /// �������Ƿ���ʾ����ǩ��
        /// </summary>
        public const string SigantureEnabel = "SigantureEnabel";
        /// <summary>
        /// �����ֶ�
        /// </summary>
        public const string SFFields = "SFFields";
    }
    /// <summary>
    /// ��������
    /// </summary>
    public class FrmSubFlow : Entity
    {
        #region ����
        public string No
        {
            get
            {
                return "ND" + this.NodeID;
            }
            set
            {
                string nodeID = value.Replace("ND", "");
                this.NodeID = int.Parse(nodeID);
            }
        }
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public int NodeID
        {
            get
            {
                return this.GetValIntByKey(NodeAttr.NodeID);
            }
            set
            {
                this.SetValByKey(NodeAttr.NodeID, value);
            }
        }
        /// <summary>
        /// �ɴ�����������
        /// </summary>
        public string SFActiveFlows
        {
            get
            {
                return this.GetValStringByKey(NodeAttr.SFActiveFlows);
            }
            set
            {
                this.SetValByKey(NodeAttr.SFActiveFlows, value);
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public FrmSubFlowSta HisFrmSubFlowSta
        {
            get
            {
                return (FrmSubFlowSta)this.GetValIntByKey(FrmSubFlowAttr.SFSta);
            }
            set
            {
                this.SetValByKey(FrmSubFlowAttr.SFSta, (int)value);
            }
        }
        /// <summary>
        /// ��ʾ��ʽ(0=���,1=����.)
        /// </summary>
        public FrmWorkShowModel HisFrmWorkShowModel
        {
            get
            {
                return (FrmWorkShowModel)this.GetValIntByKey(FrmSubFlowAttr.SFShowModel);
            }
            set
            {
                this.SetValByKey(FrmSubFlowAttr.SFShowModel, (int)value);
            }
        }
         
        /// <summary>
        /// Y
        /// </summary>
        public float SF_Y
        {
            get
            {
                return this.GetValFloatByKey(FrmSubFlowAttr.SF_Y);
            }
            set
            {
                this.SetValByKey(FrmSubFlowAttr.SF_Y, value);
            }
        }
        /// <summary>
        /// X
        /// </summary>
        public float SF_X
        {
            get
            {
                return this.GetValFloatByKey(FrmSubFlowAttr.SF_X);
            }
            set
            {
                this.SetValByKey(FrmSubFlowAttr.SF_X, value);
            }
        }
        /// <summary>
        /// W
        /// </summary>
        public float SF_W
        {
            get
            {
                return this.GetValFloatByKey(FrmSubFlowAttr.SF_W);
            }
            set
            {
                this.SetValByKey(FrmSubFlowAttr.SF_W, value);
            }
        }
        public string SF_Wstr
        {
            get
            {
                if (this.SF_W == 0)
                    return "100%";
                return this.SF_W + "px";
            }
        }
        /// <summary>
        /// H
        /// </summary>
        public float SF_H
        {
            get
            {
                return this.GetValFloatByKey(FrmSubFlowAttr.SF_H);
            }
            set
            {
                this.SetValByKey(FrmSubFlowAttr.SF_H, value);
            }
        }
        public string SF_Hstr
        {
            get
            {
                if (this.SF_H == 0)
                    return "100%";
                return this.SF_H + "px";
            }
        }
        /// <summary>
        /// �켣ͼ�Ƿ���ʾ?
        /// </summary>
        public bool SFTrackEnable
        {
            get
            {
                return this.GetValBooleanByKey(FrmSubFlowAttr.SFTrackEnable);
            }
            set
            {
                this.SetValByKey(FrmSubFlowAttr.SFTrackEnable, value);
            }
        }
        /// <summary>
        /// ��ʷ�����Ϣ�Ƿ���ʾ?
        /// </summary>
        public bool SFListEnable
        {
            get
            {
                return this.GetValBooleanByKey(FrmSubFlowAttr.SFListEnable);
            }
            set
            {
                this.SetValByKey(FrmSubFlowAttr.SFListEnable, value);
            }
        }
        /// <summary>
        /// �ڹ켣�����Ƿ���ʾ���еĲ��裿
        /// </summary>
        public bool SFIsShowAllStep
        {
            get
            {
                return this.GetValBooleanByKey(FrmSubFlowAttr.SFIsShowAllStep);
            }
            set
            {
                this.SetValByKey(FrmSubFlowAttr.SFIsShowAllStep, value);
            }
        }
        /// <summary>
        /// ����û�δ����Ƿ���Ĭ��������?
        /// </summary>
        public bool SFIsFullInfo
        {
            get
            {
                return this.GetValBooleanByKey(FrmSubFlowAttr.SFIsFullInfo);
            }
            set
            {
                this.SetValByKey(FrmSubFlowAttr.SFIsFullInfo, value);
            }
        }
        /// <summary>
        /// Ĭ�������Ϣ
        /// </summary>
        public string SFDefInfo
        {
            get
            {
                return this.GetValStringByKey(FrmSubFlowAttr.SFDefInfo);
            }
            set
            {
                this.SetValByKey(FrmSubFlowAttr.SFDefInfo, value);
            }
        }
        /// <summary>
        /// �ڵ�����.
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey("Name");
            }
        }
        /// <summary>
        /// ���⣬���Ϊ����ȡ�ڵ�����.
        /// </summary>
        public string SFCaption
        {
            get
            {
                return this.GetValStringByKey(FrmSubFlowAttr.SFCaption);
            }
        }
        /// <summary>
        /// ��������(��ˣ��󶨣����ģ���ʾ)
        /// </summary>
        public string SFOpLabel
        {
            get
            {
                return this.GetValStringByKey(FrmSubFlowAttr.SFOpLabel);
            }
            set
            {
                this.SetValByKey(FrmSubFlowAttr.SFOpLabel, value);
            }
        }
        /// <summary>
        /// �����ֶ�
        /// </summary>
        public string SFFields
        {
            get
            {
                return this.GetValStringByKey(FrmSubFlowAttr.SFFields);
            }
            set
            {
                this.SetValByKey(FrmSubFlowAttr.SFFields, value);
            }
        }
        /// <summary>
        /// �Ƿ���ʾ����ǩ����
        /// </summary>
        public bool SigantureEnabel
        {
            get
            {
                return this.GetValBooleanByKey(FrmSubFlowAttr.SigantureEnabel);
            }
            set
            {
                this.SetValByKey(FrmSubFlowAttr.SigantureEnabel, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ����
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                uac.IsDelete = false;
                uac.IsInsert = false;
                return uac;
            }
        }
        /// <summary>
        /// ��д����
        /// </summary>
        public override string PK
        {
            get
            {
                return "NodeID";
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public FrmSubFlow()
        {
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="no"></param>
        public FrmSubFlow(string mapData)
        {
            if (mapData.Contains("ND") == false)
            {
                this.HisFrmSubFlowSta = FrmSubFlowSta.Disable;
                return;
            }

            string mapdata = mapData.Replace("ND", "");
            if (DataType.IsNumStr(mapdata) == false)
            {
                this.HisFrmSubFlowSta = FrmSubFlowSta.Disable;
                return;
            }

            try
            {
                this.NodeID = int.Parse(mapdata);
            }
            catch
            {
                return;
            }
            this.Retrieve();
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="no"></param>
        public FrmSubFlow(int nodeID)
        {
            this.NodeID = nodeID;
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
                Map map = new Map("WF_Node");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "��������";
                map.EnType = EnType.Sys;

                map.AddTBIntPK(NodeAttr.NodeID, 0, "�ڵ�ID", true, true);
                map.AddTBString(NodeAttr.Name, null, "�ڵ�����", true, true, 0, 100, 10);

                #region �˴������ NodeSheet���еģ�map �����ò���ҲҪ���.

                map.AddDDLSysEnum(FrmSubFlowAttr.SFSta, (int)FrmSubFlowSta.Disable, "��������״̬",
                   true, true, FrmSubFlowAttr.SFSta, "@0=����@1=����@2=ֻ��");

                map.AddDDLSysEnum(FrmSubFlowAttr.SFShowModel, (int)FrmWorkShowModel.Free, "��ʾ��ʽ",
                    true, true, FrmSubFlowAttr.SFShowModel, "@0=���ʽ@1=����ģʽ"); //��������ʱû����.

                map.AddTBString(FrmSubFlowAttr.SFCaption, null, "����", true, false, 0, 100, 10,true);
                map.AddTBString(FrmSubFlowAttr.SFDefInfo, null, "��������������", true, false, 0, 50, 10,true);
                map.AddTBString(FrmSubFlowAttr.SFActiveFlows, null, "�ɴ�����������", true, false, 0, 50, 10, true);


                //map.AddDDLSysEnum(FrmSubFlowAttr.SFType, (int)SFType.Check, "��������", true, true,
                //    FrmSubFlowAttr.SFType, "@0=��������@1=��־���@2=�ܱ����@3=�±����");

                //map.AddTBString(FrmSubFlowAttr.SFCaption, null, "����", true, false, 0, 100, 10);

               // map.AddDDLSysEnum(FrmSubFlowAttr.SFAth, (int)SFAth.None, "�����ϴ�", true, true,
                 //  FrmSubFlowAttr.SFAth, "@0=������@1=�฽��@2=������(�ݲ�֧��)@3=ͼƬ����(�ݲ�֧��)");

               // map.SetHelperAlert(FrmSubFlowAttr.SFAth,
                 //   "������ڼ䣬�Ƿ������ϴ�����������ʲô���ĸ�����ע�⣺�����������ڽڵ�������á�"); //ʹ��alert�ķ�ʽ��ʾ������Ϣ.

                //map.AddBoolean(FrmSubFlowAttr.SFTrackEnable, true, "�켣ͼ�Ƿ���ʾ��", true, true, false);
                //map.AddBoolean(FrmSubFlowAttr.SFListEnable, true, "��ʷ�����Ϣ�Ƿ���ʾ��(��,��ʷ��Ϣ�����������)", true, true, true);
                //map.AddBoolean(FrmSubFlowAttr.SFIsShowAllStep, false, "�ڹ켣�����Ƿ���ʾ���еĲ��裿", true, true);

                //map.AddTBString(FrmSubFlowAttr.SFOpLabel, "���", "��������(���/����/��ʾ)", true, false, 0, 50, 10);
                //map.AddTBString(FrmSubFlowAttr.SFDefInfo, "ͬ��", "Ĭ�������Ϣ", true, false, 0, 50, 10);
                //map.AddBoolean(FrmSubFlowAttr.SigantureEnabel, false, "�������Ƿ���ʾΪͼƬǩ����", true, true);
                //map.AddBoolean(FrmSubFlowAttr.SFIsFullInfo, true, "����û�δ����Ƿ���Ĭ�������䣿", true, true, true);


                map.AddTBFloat(FrmSubFlowAttr.SF_X, 5, "λ��X", true, false);
                map.AddTBFloat(FrmSubFlowAttr.SF_Y, 5, "λ��Y", true, false);

                map.AddTBFloat(FrmSubFlowAttr.SF_H, 300, "�߶�", true, false);
                map.AddTBFloat(FrmSubFlowAttr.SF_W, 400, "���", true, false);

              //  map.AddTBString(FrmSubFlowAttr.SFFields, null, "������ʽ�ֶ�", true, false, 0, 1000, 10);
                #endregion �˴������ NodeSheet���еģ�map �����ò���ҲҪ���.

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeUpdateInsertAction()
        {
            //FrmAttachment workCheckAth = new FrmAttachment();
            //bool isHave = workCheckAth.RetrieveByAttr(FrmAttachmentAttr.MyPK, this.NodeID + "_FrmSubFlow");
            ////��������������
            //if (isHave == false)
            //{
            //    workCheckAth = new FrmAttachment();
            //    /*���û�в�ѯ����,���п�����û�д���.*/
            //    workCheckAth.MyPK = this.NodeID + "_FrmSubFlow";
            //    workCheckAth.FK_MapData = this.NodeID.ToString();
            //    workCheckAth.NoOfObj = this.NodeID + "_FrmSubFlow";
            //    workCheckAth.Exts = "*.*";

            //    //�洢·��.
            //    workCheckAth.SaveTo = "/DataUser/UploadFile/";
            //    workCheckAth.IsNote = false; //����ʾnote�ֶ�.
            //    workCheckAth.IsVisable = false; // ������form �ϲ��ɼ�.

            //    //λ��.
            //    workCheckAth.X = (float)94.09;
            //    workCheckAth.Y = (float)333.18;
            //    workCheckAth.W = (float)626.36;
            //    workCheckAth.H = (float)150;

            //    //�฽��.
            //    workCheckAth.UploadType = AttachmentUploadType.Multi;
            //    workCheckAth.Name = "��������";
            //    workCheckAth.SetValByKey("AtPara", "@IsWoEnablePageset=1@IsWoEnablePrint=1@IsWoEnableViewModel=1@IsWoEnableReadonly=0@IsWoEnableSave=1@IsWoEnableWF=1@IsWoEnableProperty=1@IsWoEnableRevise=1@IsWoEnableIntoKeepMarkModel=1@FastKeyIsEnable=0@IsWoEnableViewKeepMark=1@FastKeyGenerRole=@IsWoEnableTemplete=1");
            //    workCheckAth.Insert();
            //}   
            return base.beforeUpdateInsertAction();
        }
    }
    /// <summary>
    /// ��������s
    /// </summary>
    public class FrmSubFlows : Entities
    {
        #region ����
        /// <summary>
        /// ��������s
        /// </summary>
        public FrmSubFlows()
        {
        }
        /// <summary>
        /// ��������s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public FrmSubFlows(string fk_mapdata)
        {
            if (SystemConfig.IsDebug)
                this.Retrieve("No", fk_mapdata);
            else
                this.RetrieveFromCash("No", (object)fk_mapdata);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FrmSubFlow();
            }
        }
        #endregion
    }
}
