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
    /// 流程类别属性
    /// </summary>
    public class FlowSortAttr : EntityTreeAttr
    {
    }
    /// <summary>
    ///  流程类别
    /// </summary>
    public class FlowSort : EntityTree
    {
        #region 构造方法
        /// <summary>
        /// 流程类别
        /// </summary>
        public FlowSort()
        {
        }
        /// <summary>
        /// 流程类别
        /// </summary>
        /// <param name="_No"></param>
        public FlowSort(string _No) : base(_No) { }
        #endregion

        /// <summary>
        /// 流程类别Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_FlowSort");
                map.EnDesc = "流程类别";
                map.CodeStruct = "2";
                map.IsAllowRepeatName = false;
                 

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(FlowSortAttr.No, null, "编号", true, true, 1, 10, 20);
                map.AddTBString(FlowSortAttr.Name, null, "名称", true, false, 0, 100, 30);
                map.AddTBString(FlowSortAttr.ParentNo, null, "父节点No", false, false, 0, 100, 30);
                map.AddTBString(FlowSortAttr.TreeNo, null, "TreeNo", false, false, 0, 100, 30);

                map.AddTBInt(FlowSortAttr.Idx, 0, "Idx", false, false);
                map.AddTBInt(FlowSortAttr.IsDir, 0, "IsDir", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }

        #region 重写方法
        #endregion 重写方法

    }
    /// <summary>
    /// 流程类别
    /// </summary>
    public class FlowSorts : EntitiesTree
    {
        /// <summary>
        /// 流程类别s
        /// </summary>
        public FlowSorts() { }
        /// <summary>
        /// 得到它的 Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new FlowSort();
            }

        }
        /// <summary>
        /// 流程类别s
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
                fs.Name = "公文类";
                fs.No = "01";
                fs.Insert();

                fs = new FlowSort();
                fs.Name = "办公类";
                fs.No = "02";
                fs.Insert();
                i = base.RetrieveAll();
            }

            return i;
        }
    }
}
