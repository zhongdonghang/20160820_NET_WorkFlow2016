using System;
using System.Collections;
using BP.Sys;
using BP.DA;
using BP.En;

namespace BP.WF.Data
{
    /// <summary>
    /// 月份
    /// </summary>
    public class GenerWorkFlowViewNYAttr : EntityNoNameAttr
    {
    }
    /// <summary>
    /// 月份
    /// </summary>
    public class GenerWorkFlowViewNY : EntityNoName
    {
        #region 属性
        #endregion

        #region 构造方法
        /// <summary>
        /// 月份
        /// </summary>
        public GenerWorkFlowViewNY()
        {
        }
        /// <summary>
        /// 月份
        /// </summary>
        /// <param name="mypk"></param>
        public GenerWorkFlowViewNY(string no)
        {
            this.No = no;
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
                Map map = new Map("Pub_NY");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "月份";
                map.EnType = EnType.View;

                map.AddTBStringPK(GenerWorkFlowViewNYAttr.No, null, "编号", true, false, 2, 30, 20);
                map.AddTBString(GenerWorkFlowViewNYAttr.Name, null, "名称", true, false, 0, 3900, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
     
    }
    /// <summary>
    /// 月份s
    /// </summary>
    public class GenerWorkFlowViewNYs : EntitiesNoName
    {
        #region 构造
        /// <summary>
        /// 月份s
        /// </summary>
        public GenerWorkFlowViewNYs()
        {
        }
        /// <summary>
        /// 得到它的 Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GenerWorkFlowViewNY();
            }
        }
        #endregion

        public override int RetrieveAll()
        {
            string sql = "SELECT DISTINCT FK_NY, FK_NY FROM WF_GenerWorkFlow";
            
            return base.RetrieveAll();
        }
    }
}
