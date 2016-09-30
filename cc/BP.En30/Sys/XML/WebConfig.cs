using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.XML;

namespace BP.Sys.Xml
{
    /// <summary>
    /// ����
    /// </summary>
    public class WebConfigDescAttr
    {
        /// <summary>
        /// ������Ϊ
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// Name
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// ���ʽ
        /// </summary>
        public const string URL = "URL";
        /// <summary>
        /// ����
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// ����
        /// </summary>
        public const string DBType = "DBType";
        /// <summary>
        /// IsEnable
        /// </summary>
        public const string IsEnable = "IsEnable";
        public const string IsShow = "IsShow";
        /// <summary>
        /// CfgVal
        /// </summary>
        public const string CfgVal = "CfgVal";
        /// <summary>
        /// ����
        /// </summary>
        public const string Group = "Group";
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public const string DefVal = "DefVal";
    }
    /// <summary>
    /// �����ļ���Ϣ
    /// </summary>
    public class WebConfigDesc : XmlEn
    {
        #region ����
        private string _No = "";
        public string No
        {
            get
            {
                if (_No == "")
                    return this.GetValStringByKey(WebConfigDescAttr.No);
                else
                    return _No;
            }
            set
            {
                _No = value;
            }
        }
        public string Name
        {
            get
            {
                return this.GetValStringByKey(WebConfigDescAttr.Name);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Group
        {
            get
            {
                return this.GetValStringByKey(WebConfigDescAttr.Group);
            }
        }
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public string DefVal
        {
            get
            {
                return this.GetValStringByKey(WebConfigDescAttr.DefVal);
            }
        }
        public bool IsEnable
        {
            get
            {
                if (this.GetValStringByKey(WebConfigDescAttr.IsEnable) == "0")
                    return false;
                return true;
            }
        }
        public bool IsShow
        {
            get
            {
                if (this.GetValStringByKey(WebConfigDescAttr.IsShow) == "0")
                    return false;
                return true;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStringByKey(WebConfigDescAttr.Note);
            }
        }
        public string CfgVal
        {
            get
            {
                return this.GetValStringByKey(WebConfigDescAttr.CfgVal);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string DBType
        {
            get
            {
                string str= this.GetValStringByKey(WebConfigDescAttr.DBType);
                if (string.IsNullOrEmpty(str))
                    str = "String";
                return str;
            }
        }
        #endregion

        #region ����
        public WebConfigDesc()
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new WebConfigDescs();
            }
        }
        #endregion
    }
    /// <summary>
    /// �����ļ���Ϣ
    /// </summary>
    public class WebConfigDescs : XmlEns
    {
        #region ����
        /// <summary>
        /// �����ļ���Ϣ
        /// </summary>
        public WebConfigDescs() { }
        #endregion

        #region ��д�������Ի򷽷���
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new WebConfigDesc();
            }
        }
        /// <summary>
        /// �ļ�
        /// </summary>
        public override string File
        {
            get
            {
                return SystemConfig.PathOfXML + "\\WebConfigDesc.xml";
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public override string TableName
        {
            get
            {
                return "Add";
            }
        }
        public override Entities RefEns
        {
            get
            {
                return null;
            }
        }
        #endregion
    }
}
