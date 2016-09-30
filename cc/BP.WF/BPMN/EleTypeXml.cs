using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;

namespace BP.BPMN
{
    /// <summary>
    /// Ԫ��
    /// </summary>
    public class EleTypeXml : XmlEn
    {
        #region ����
        public string No
        {
            get
            {
                return this.GetValStringByKey("No");
            }
        }
        public string Name
        {
            get
            {
                return this.GetValStringByKey("Name");
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string EventDesc
        {
            get
            {
                return this.GetValStringByKey("EleDesc");
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string EleType
        {
            get
            {
                return this.GetValStringByKey("EleType");
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// �¼�
        /// </summary>
        public EleTypeXml()
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new EleTypeXmls();
            }
        }
        #endregion
    }
    /// <summary>
    /// Ԫ��s
    /// </summary>
    public class EleTypeXmls : XmlEns
    {
        #region ����
        /// <summary>
        /// �¼�s
        /// </summary>
        public EleTypeXmls() { }
        #endregion

        #region ��д�������Ի򷽷���
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new EleTypeXml();
            }
        }
        /// <summary>
        /// ���·��
        /// </summary>
        public override string File
        {
            get
            {
                return SystemConfig.PathOfWebApp + "\\WF\\Admin\\BPMN\\Designer.xml";
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public override string TableName
        {
            get
            {
                return "EleType";
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
