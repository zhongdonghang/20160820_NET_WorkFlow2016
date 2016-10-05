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

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string itemId, string keyword)
        {
            System.Data.DataTable dt = BP.WF.Dev2Interface.DB_GenerCanStartFlowsOfDataTable(BP.Web.WebUser.No);
            List<SimpleFlowViewModel> data = new List<SimpleFlowViewModel>();
            data = SimpleFlowViewModel.Table2Obj(dt);
            return Content(data.ToJson());
        }

    }
}
