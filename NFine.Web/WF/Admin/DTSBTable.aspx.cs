using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.DA;
using BP.En;
using BP.Sys;
using System.Collections;

namespace CCFlow.WF.Admin
{
    public partial class DTSBTable : System.Web.UI.Page
    {
        #region 属性.
        public string FK_Flow
        {
            get
            {
                return this.Request.QueryString["FK_Flow"];
            }
        }
        #endregion 属性.


        protected void Page_Load(object sender, EventArgs e)
        {
            string rpt = "ND" + int.Parse(this.FK_Flow) + "Rpt";
            MapAttrs attrs = new MapAttrs(rpt);

            Flow fl = new Flow(this.FK_Flow);
            fl.RetrieveFromDBSources();

            fl.No = this.FK_Flow;
            if (string.IsNullOrEmpty(fl.DTSBTable) == true)
            {
                this.Pub1.AddFieldSet("配置错误", "请关闭该窗口，在流程属性里配置业务表名，然后点保存按钮，之后打开该功能界面。");
                return;
            }


            DataTable dt = BP.DA.DBAccess.GetTableSchema(fl.DTSBTable);

            this.Pub1.AddTable();
            this.Pub1.AddTR();
            this.Pub1.AddTDTitle("序");
            this.Pub1.AddTDTitle("是否同步");
            this.Pub1.AddTDTitle("字段描述");
            this.Pub1.AddTDTitle("类型");
            this.Pub1.AddTDTitle("业务表(" + fl.DTSBTable + ")");
            this.Pub1.AddTREnd();

            int idx = 0;

            Hashtable ht = new Hashtable();
            if (string.IsNullOrEmpty(fl.DTSFields))
                fl.DTSFields = "@";

            string[] fieldArray = fl.DTSFields.Split('@');
            string[] lcFieldArray = fieldArray[0].Split(',');

            string[] ywFieldArray = fieldArray[1].Split(',');

            for (int i = 0; i < lcFieldArray.Length; i++)
            {
                ht.Add(lcFieldArray[i], ywFieldArray[i]);
            }

            string dtsFields = fl.DTSFields.Split('@')[0];

            foreach (MapAttr attr in attrs)
            {
                idx++;
                this.Pub1.AddTR();
                this.Pub1.AddTDIdx(idx);

                CheckBox cb = new CheckBox();
                cb.ID = "CB_" + attr.KeyOfEn;
                cb.Text = attr.Name;

                foreach (DictionaryEntry de in ht)
                {
                    if (attr.KeyOfEn == de.Key.ToString())
                        cb.Checked = true;
                }

                this.Pub1.AddTD(cb);
                this.Pub1.AddTD(attr.KeyOfEn);
                this.Pub1.AddTD(attr.MyDataTypeStr);

                BP.Web.Controls.DDL ddl = new BP.Web.Controls.DDL();
                ddl.ID = "DDL_" + attr.KeyOfEn;

                ddl.Bind(dt, "FNAME", "FNAME");
                if (cb.Checked == true)
                {
                    try
                    {
                        ddl.SetSelectItem(ht[attr.KeyOfEn].ToString());
                    }
                    catch (Exception)
                    {
                    }
                }
                this.Pub1.AddTD(ddl);
                this.Pub1.AddTREnd();
            }
            this.Pub1.AddTableEnd();

            Button btn = new Button();
            btn.ID = "Btn_Save";
            btn.Text = "保存";
            btn.Click += new EventHandler(btn_Click);

            Button btnClose = new Button();
            btnClose.ID = "Btn_Close";
            btnClose.Text = "取消";
            btnClose.Click += new EventHandler(btnClose_Click);

            this.Pub1.Add(btn);
            this.Pub1.Add(btnClose);

        }
        /// <summary>
        /// 执行数据的保存操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_Click(object sender, EventArgs e)
        {
            string rpt = "ND" + int.Parse(this.FK_Flow) + "Rpt";
            MapAttrs attrs = new MapAttrs(rpt);
            //string vals = "";

            string lcDTSFields = "";//要同步的流程字段
            string lcZDType = "";//记录同步字段的类型
            string ywDTSFields = "";//第三方字段

            foreach (MapAttr attr in attrs)
            {
                CheckBox cb = this.Pub1.GetCBByID("CB_" + attr.KeyOfEn);
                if (cb == null || cb.Checked == false)
                    continue;

                BP.Web.Controls.DDL ddl = this.Pub1.GetDDLByID("DDL_" + attr.KeyOfEn);
                //vals += "@" + attr.KeyOfEn + "=" + ddl.SelectedItemStringVal;
                lcDTSFields += attr.KeyOfEn + ",";
                lcZDType += attr.MyDataType + ",";

                //如果选中的业务字段重复，抛出异常
                if (ywDTSFields.Contains(ddl.SelectedItemStringVal))
                {
                    BP.Sys.PubClass.Alert("请确保选中业务字段的唯一性.");
                    return;
                }
                ywDTSFields += ddl.SelectedItemStringVal + ",";
            }

            try
            {
                //去除最后一个字符的操作
                lcDTSFields = lcDTSFields.Substring(0, lcDTSFields.Length - 1);
                lcZDType = lcZDType.Substring(0, lcZDType.Length - 1);
                ywDTSFields = ywDTSFields.Substring(0, ywDTSFields.Length - 1);
            }
            catch (Exception)
            {
                BP.Sys.PubClass.Alert("没有任何选中项需要保存!");
                return;
            }


            if (string.IsNullOrEmpty(lcDTSFields) == true || string.IsNullOrEmpty(ywDTSFields) == true)
            {
                BP.Sys.PubClass.Alert("要配置的内容为空...");
                return;
            }

            //保存数据.
            Flow fl = new Flow(this.FK_Flow);
            //数据存储格式   a,b,c@1,2,3@a_1,b_1,c_1
            fl.DTSFields = lcDTSFields + "@" + lcZDType + "@" + ywDTSFields;
            fl.Update();

            BP.Sys.PubClass.WinClose();
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnClose_Click(object sender, EventArgs e)
        {
            BP.Sys.PubClass.WinClose();
        }
    }
}