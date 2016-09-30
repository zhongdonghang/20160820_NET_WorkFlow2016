using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Web.Controls;
using BP.WF;
using BP.WF.Template;
using BP.En;
using BP.DA;
using BP.Sys;
using BP;

namespace CCFlow.WF.Admin
{
    public partial class BindFrms : BP.Web.WebPage
    {
        #region 属性.
        public string FK_MapData
        {
            get
            {
                return this.Request.QueryString["FK_MapData"];
            }
        }
        public string FK_Flow
        {
            get
            {
                return this.Request.QueryString["FK_Flow"];
            }
        }
        /// <summary>
        /// 节点
        /// </summary>
        public int FK_Node
        {
            get
            {
                try
                {
                    return int.Parse(this.Request.QueryString["FK_Node"]);
                }
                catch
                {
                    return int.Parse(this.Request.QueryString["FK_Flow"]);
                }
            }
        }
        #endregion 属性.

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (this.DoType)
            {
                case "Up":
                    FrmNode fnU = new FrmNode(this.MyPK);
                    fnU.DoUp();
                    this.BindList();
                    break;
                case "Down":
                    FrmNode fnD = new FrmNode(this.MyPK);
                    fnD.DoDown();
                    this.BindList();
                    break;
                case "SelectedFrm":
                    this.SelectedFrm();
                    break;
                default:
                    this.BindList();
                    break;
            }
        }


        #region 绑定表单.
        public void SelectedFrm()
        {
            BP.WF.Node nd = new BP.WF.Node(this.FK_Node);

            FrmNodes fns = new FrmNodes(this.FK_Flow, this.FK_Node);
            this.Pub1.AddTable("align=left");
            this.Pub1.AddCaption("设置节点:(" + nd.Name + ")绑定的表单");
            this.Pub1.AddTR();
            this.Pub1.AddTDTitle("Idx");
            this.Pub1.AddTDTitle("表单编号");
            this.Pub1.AddTDTitle("名称");
            this.Pub1.AddTDTitle("表/视图");
            this.Pub1.AddTREnd();

            MapDatas mds = new MapDatas();
            QueryObject obj_mds = new QueryObject(mds);
            obj_mds.AddWhere(MapDataAttr.AppType, (int)AppType.Application);
            obj_mds.addOrderBy(MapDataAttr.Name);
            obj_mds.DoQuery();

            SysFormTrees formTrees = new SysFormTrees();
            QueryObject objInfo = new QueryObject(formTrees);
            objInfo.AddWhere(SysFormTreeAttr.ParentNo, "0");
            objInfo.addOrderBy(SysFormTreeAttr.Name);
            objInfo.DoQuery();

            int idx = 0;
            foreach (SysFormTree fs in formTrees)
            {
                idx++;
                this.Pub1.AddTRSum();
                this.Pub1.AddTDIdx(idx);
                this.Pub1.AddTD("colspan=4", fs.Name);
                this.Pub1.AddTREnd();
                foreach (MapData md in mds)
                {
                    if (md.FK_FormTree != fs.No)
                        continue;
                    idx++;
                    this.Pub1.AddTR();
                    this.Pub1.AddTDIdx(idx);

                    CheckBox cb = new CheckBox();
                    cb.ID = "CB_" + md.No;
                    cb.Text = md.No;
                    cb.Checked = fns.Contains(FrmNodeAttr.FK_Frm, md.No);

                    this.Pub1.AddTD(cb);
                    if (cb.Checked)
                    {
                        this.Pub1.AddTDB("<a href=\"javascript:WinOpen('../MapDef/CCForm/Frm.aspx?FK_MapData=" + md.No + "&FK_Flow=" + this.FK_Flow + "');\" ><b>" + md.Name + "</b></a>");
                        this.Pub1.AddTDB(md.PTable);
                    }
                    else
                    {
                        this.Pub1.AddTD("<a href=\"javascript:WinOpen('../MapDef/CCForm/Frm.aspx?FK_MapData=" + md.No + "&FK_Flow=" + this.FK_Flow + "');\" >" + md.Name + "</a>");
                        this.Pub1.AddTD(md.PTable);
                    }
                    this.Pub1.AddTREnd();
                }
                AddChildNode(fs.No, mds, fns);
            }
            Button btn = new Button();
            btn.ID = "Btn_Save";
            btn.Text = "保存并设置绑定方案属性";
            btn.CssClass = "Btn";
            btn.Click += new EventHandler(btn_SaveFlowFrms_Click);
            this.Pub1.AddTR();
            this.Pub1.AddTD("colspan=4", btn);
            this.Pub1.AddTREnd();
            this.Pub1.AddTableEnd();
        }
        private void AddChildNode(string parentNo, MapDatas mds, FrmNodes fns)
        {
            SysFormTrees formTrees = new SysFormTrees();
            QueryObject objInfo = new QueryObject(formTrees);
            objInfo.AddWhere(SysFormTreeAttr.ParentNo, parentNo);
            objInfo.addOrderBy(SysFormTreeAttr.Name);
            objInfo.DoQuery();

            int idx = 0;
            foreach (SysFormTree fs in formTrees)
            {
                idx++;
                foreach (MapData md in mds)
                {
                    if (md.FK_FormTree != fs.No)
                        continue;
                    idx++;
                    this.Pub1.AddTR();
                    this.Pub1.AddTDIdx(idx);

                    CheckBox cb = new CheckBox();
                    cb.ID = "CB_" + md.No;
                    cb.Text = md.No;
                    cb.Checked = fns.Contains(FrmNodeAttr.FK_Frm, md.No);

                    this.Pub1.AddTD(cb);
                    this.Pub1.AddTD(md.Name);
                    this.Pub1.AddTD(md.PTable);
                    this.Pub1.AddTREnd();
                }
                AddChildNode(fs.No, mds, fns);
            }
        }
        void btn_SaveFlowFrms_Click(object sender, EventArgs e)
        {
            FrmNodes fns = new FrmNodes(this.FK_Flow, this.FK_Node);
            MapDatas mds = new MapDatas();
            mds.Retrieve(MapDataAttr.AppType, (int)AppType.Application);
            //BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
            string ids = ",";
            foreach (MapData md in mds)
            {
                CheckBox cb = this.Pub1.GetCBByID("CB_" + md.No);
                if (cb == null || cb.Checked == false)
                    continue;
                ids += md.No + ",";
            }

            //删除已经删除的。
            foreach (FrmNode fn in fns)
            {
                if (ids.Contains("," + fn.FK_Frm + ",") == false)
                {
                    fn.Delete();
                    continue;
                }
            }

            // 增加集合中没有的。
            string[] strs = ids.Split(',');
            foreach (string s in strs)
            {
                if (string.IsNullOrEmpty(s))
                    continue;
                if (fns.Contains(FrmNodeAttr.FK_Frm, s))
                    continue;

                FrmNode fn = new FrmNode();
                fn.FK_Frm = s;
                fn.FK_Flow = this.FK_Flow;
                fn.FK_Node = this.FK_Node;
                fn.Save();
            }
            this.Response.Redirect("BindFrms.aspx?FK_Node=" + this.FK_Node + "&FK_Flow=" + this.FK_Flow, true);
        }
        #endregion 绑定表单.

        #region 设置方案.
        public void BindList()
        {
            string text = "";
            BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
            FrmNodes fns = new FrmNodes(this.FK_Flow, this.FK_Node);
            if (fns.Count == 0)
            {
                text = "当前没有任何独立表单绑定到该节点上，请您执行绑定表单：<input type=button onclick=\"javascript:BindFrms('" + this.FK_Node + "','" + this.FK_Flow + "');\" value='修改表单绑定'  class=Btn />";
                this.Pub1.AddFieldSet("提示", text);
                return;
            }


            this.Pub1.AddTable("width=100%");
            this.Pub1.AddCaption("设置节点:(" + nd.Name + ")绑定表单");
            this.Pub1.AddTR();
            this.Pub1.AddTDTitle("Idx");
            this.Pub1.AddTDTitle("表单编号");
            this.Pub1.AddTDTitle("名称");
            this.Pub1.AddTDTitle("显示方式");
            this.Pub1.AddTDTitle("可编辑否？");
            this.Pub1.AddTDTitle("可打印否？");
            this.Pub1.AddTDTitle("是否启用<br>装载填充事件");
            this.Pub1.AddTDTitle("权限控制<br>方案");
            this.Pub1.AddTDTitle("表单元素<br>自定义设置");
            this.Pub1.AddTDTitle("谁是主键？");
            this.Pub1.AddTDTitle("顺序");
            this.Pub1.AddTDTitle("");
            this.Pub1.AddTDTitle("");
            this.Pub1.AddTREnd();



            int idx = 1;
            foreach (FrmNode fn in fns)
            {
                this.Pub1.AddTR();
                this.Pub1.AddTDIdx(idx++);
                this.Pub1.AddTD(fn.FK_Frm);

                MapData md = new MapData();
                md.No = fn.FK_Frm;
                try
                {
                    md.Retrieve();
                }
                catch
                {
                    //说明该表单不存在了，就需要把这个删除掉.
                    fn.Delete();
                }

                this.Pub1.AddTD("<a href=\"javascript:WinOpen('../MapDef/CCForm/Frm.aspx?FK_MapData=" + md.No + "&FK_Flow=" + this.FK_Flow + "');\" >" + md.Name + "</a>");


                DDL ddl = new DDL();
                ddl.ID = "DDL_FrmType_" + fn.FK_Frm;
                ddl.BindSysEnum("FrmType", (int)fn.HisFrmType);
                this.Pub1.AddTD(ddl);

                CheckBox cb = new CheckBox();
                cb.ID = "CB_IsEdit_" + md.No;
                cb.Text = "可编辑否？";
                cb.Checked = fn.IsEdit;
                this.Pub1.AddTD(cb);

                cb = new CheckBox();
                cb.ID = "CB_IsPrint_" + md.No;
                cb.Text = "打印否？";
                cb.Checked = fn.IsPrint;
                this.Pub1.AddTD(cb);

                cb = new CheckBox();
                cb.ID = "CB_IsEnableLoadData_" + md.No;
                cb.Text = "启用否？";
                cb.Checked = fn.IsEnableLoadData;
                this.Pub1.AddTD(cb);

                ddl = new DDL();
                ddl.ID = "DDL_Sln_" + md.No;
                ddl.Items.Add(new ListItem("默认方案", "0"));
                ddl.Items.Add(new ListItem("自定义", this.FK_Node.ToString()));
                ddl.SetSelectItem(fn.FrmSln); //设置权限控制方案.
                this.Pub1.AddTD(ddl);

                this.Pub1.AddTDBegin();
                this.Pub1.Add("<a href=\"javascript:WinField('" + md.No + "','" + this.FK_Node + "','" + this.FK_Flow + "')\" >字段</a>");
                this.Pub1.Add("-<a href=\"javascript:WinFJ('" + md.No + "','" + this.FK_Node + "','" + this.FK_Flow + "')\" >附件</a>");
                this.Pub1.Add("-<a href=\"javascript:WinDtl('" + md.No + "','" + this.FK_Node + "','" + this.FK_Flow + "')\" >从表</a>");

                if (md.HisFrmType == FrmType.ExcelFrm)
                    this.Pub1.Add("-<a href=\"javascript:ToolbarExcel('" + md.No + "','" + this.FK_Node + "','" + this.FK_Flow + "')\" >ToolbarExcel</a>");

                if (md.HisFrmType == FrmType.WordFrm)
                    this.Pub1.Add("-<a href=\"javascript:ToolbarWord('" + md.No + "','" + this.FK_Node + "','" + this.FK_Flow + "')\" >ToolbarWord</a>");

                this.Pub1.AddTDEnd();

                ddl = new DDL();
                ddl.ID = "DDL_WhoIsPK_" + md.No;
                ddl.BindSysEnum("WhoIsPK");
                ddl.SetSelectItem((int)fn.WhoIsPK); //谁是主键？.
                this.Pub1.AddTD(ddl);

                TextBox tb = new TextBox();
                tb.ID = "TB_Idx_" + md.No;
                tb.Text = fn.Idx.ToString();
                tb.Columns = 5;
                this.Pub1.AddTD(tb);

                this.Pub1.AddTDA("BindFrms.aspx?ShowType=EditPowerOrder&FK_Node=" + this.FK_Node + "&FK_Flow=" + this.FK_Flow + "&MyPK=" + fn.MyPK + "&DoType=Up", "上移");
                this.Pub1.AddTDA("BindFrms.aspx?ShowType=EditPowerOrder&FK_Node=" + this.FK_Node + "&FK_Flow=" + this.FK_Flow + "&MyPK=" + fn.MyPK + "&DoType=Down", "下移");

                this.Pub1.AddTREnd();
            }

            this.Pub1.AddTableEnd();

              text = "<input type=button onclick=\"javascript:BindFrms('" + this.FK_Node + "','" + this.FK_Flow + "');\" value='修改表单绑定'  class=Btn />";
            this.Pub1.Add(text);

            Button btn = new Button();
            btn.ID = "Save";
            btn.Text = "保存设置";
            btn.CssClass = "Btn";
            btn.Click += new EventHandler(btn_SavePowerOrders_Click);
            this.Pub1.Add(btn);

            text = "<input type=button onclick=\"javascript:window.close();\" value='关闭'  class=Btn />";
            this.Pub1.Add(text);
        }
        void btn_EditBindFrms_Click(object sender, EventArgs e)
        {
        }

        void btn_SavePowerOrders_Click(object sender, EventArgs e)
        {
            FrmNodes fns = new FrmNodes(this.FK_Flow, this.FK_Node);
            foreach (FrmNode fn in fns)
            {
                fn.IsEdit = this.Pub1.GetCBByID("CB_IsEdit_" + fn.FK_Frm).Checked;
                fn.IsPrint = this.Pub1.GetCBByID("CB_IsPrint_" + fn.FK_Frm).Checked;

                //是否启
                fn.IsEnableLoadData = this.Pub1.GetCBByID("CB_IsEnableLoadData_" + fn.FK_Frm).Checked;

                fn.Idx = int.Parse(this.Pub1.GetTextBoxByID("TB_Idx_" + fn.FK_Frm).Text);
                fn.HisFrmType = (BP.Sys.FrmType)this.Pub1.GetDDLByID("DDL_FrmType_" + fn.FK_Frm).SelectedItemIntVal;

                //权限控制方案.
                fn.FrmSln = this.Pub1.GetDDLByID("DDL_Sln_" + fn.FK_Frm).SelectedItemIntVal;
                fn.WhoIsPK = (WhoIsPK)this.Pub1.GetDDLByID("DDL_WhoIsPK_" + fn.FK_Frm).SelectedItemIntVal;

                fn.FK_Flow = this.FK_Flow;
                fn.FK_Node = this.FK_Node;
                //fn.FK_Frm = 

                fn.MyPK = fn.FK_Frm + "_" + fn.FK_Node + "_" + fn.FK_Flow;

                fn.Update();
            }
            this.Response.Redirect("BindFrms.aspx?ShowType=EditPowerOrder&FK_Node=" + this.FK_Node + "&FK_Flow=" + this.FK_Flow, true);
        }
        #endregion 设置方案.


    }
}