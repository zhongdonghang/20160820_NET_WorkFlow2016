using BP.WF;
using NFine.Code;
using NFine.Web.Areas.CC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.CC.Controllers
{
    public class StartController : ControllerBase
    {
        //
        // GET: /CC/Start/

        //public override ActionResult Index()
        //{
        //   string empNo = OperatorProvider.Provider.GetCurrent().UserCode;
        //    DataTable dt = Dev2Interface.DB_GenerCanStartFlowsOfDataTable(empNo);
        //    List<SimpleFlowViewModel> list = SimpleFlowViewModel.Table2Obj(dt);
        //    ViewResult vr = new ViewResult();
        //    vr.ViewName = "Index";
        //    ViewDataDictionary dic = new ViewDataDictionary(vm);
        //    vr.ViewData = dic;
        //    return vr;
        //}


        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson()
        {
            ///获得流程分类
            var treeList = new List<TreeViewModel>();



            BP.WF.FlowSorts ens = new BP.WF.FlowSorts();
            ens.RetrieveAll();
            foreach (BP.WF.FlowSort item in ens)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.No;
                tree.text = item.Name;
                tree.value = item.TreeNo;
                tree.parentId = item.ParentNo;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = true;
                treeList.Add(tree);
            }

            return Content(treeList.TreeViewJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string itemId, string keyword)
        {
            BP.WF.Flows fls = new BP.WF.Flows();
            fls.RetrieveAll();
            System.Data.DataTable dt = BP.WF.Dev2Interface.DB_GenerCanStartFlowsOfDataTable(BP.Web.WebUser.No);
            List<SimpleFlowViewModel> data = new List<SimpleFlowViewModel>();

            foreach (BP.WF.Flow fl in fls)
            {
                if (fl.FK_FlowSort != itemId) continue;
                if (fl.FlowAppType == BP.WF.FlowAppType.DocFlow) continue;
                fl.IsCanStart = false;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    if (dr["No"].ToString() == fl.No)
                    {
                        fl.IsCanStart = true;
                        break;
                    }
                    else
                    {
                        fl.IsCanStart = false;
                        break;
                    }
                }
                SimpleFlowViewModel vm = SimpleFlowViewModel.Flow2Obj(fl);
                data.Add(vm);
            }
            return Content(data.ToJson());
        }

    }
}
