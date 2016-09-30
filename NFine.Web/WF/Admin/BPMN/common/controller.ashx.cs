using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Text;
using System.Configuration;
using System.Web.SessionState;
using BP.DA;
using BP.Web;
using BP.BPMN;
using BP.Sys;
using BP.En;
using BP.WF.Template;
using System.Collections.Generic;

namespace CCFlow.WF.Admin.BPMN.common
{
    /// <summary>
    /// controller 的摘要说明
    /// </summary>
    public class controller : IHttpHandler, IRequiresSessionState
    {
        #region 全局变量IRequiresSessionState
        /// <summary>
        /// http请求
        /// </summary>
        public HttpContext _Context
        {
            get;
            set;
        }

        /// <summary>
        /// 公共方法获取值
        /// </summary>
        /// <param name="param">参数名</param>
        /// <returns></returns>
        public string getUTF8ToString(string param)
        {
            return HttpUtility.UrlDecode(_Context.Request[param], System.Text.Encoding.UTF8);
        }
        #endregion

        public void ProcessRequest(HttpContext context)
        {
            _Context = context;

            if (_Context == null) return;

            string action = string.Empty;
            //返回值
            string s_responsetext = string.Empty;
            if (!string.IsNullOrEmpty(context.Request["action"]))
                action = context.Request["action"].ToString();

            switch (action)
            {
                case "load"://获取流程图表数据
                    s_responsetext = LoadFlowJsonData();
                    break;
                case "save"://保存流程图
                    s_responsetext = Save();
                    break;
                case "saveAs"://另存为流程
                    s_responsetext = SaveAs();
                    break;
                case "genernodeid"://创建节点获取节点编号
                    s_responsetext = GenerNodeID();
                    break;
                case "deletenode"://删除流程节点
                    s_responsetext = DeleteNodeOfNodeID();
                    break;
                case "editnodename"://修改节点名称
                    s_responsetext = EditNodeName();
                    break;
            }
            if (string.IsNullOrEmpty(s_responsetext))
                s_responsetext = "";

            //组装ajax字符串格式,返回调用客户端
            context.Response.Charset = "UTF-8";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.ContentType = "text/html";
            context.Response.Expires = 0;
            context.Response.Write(s_responsetext);
            context.Response.End();
        }
        /// <summary>
        /// 加载流程图数据 
        /// </summary>
        /// <returns></returns>
        private string LoadFlowJsonData()
        {
            string diagramId = getUTF8ToString("diagramId");
            Flow fl = new Flow(diagramId);
            return fl.FlowJson;
        }
        /// <summary>
        /// 创建流程节点并返回编号
        /// </summary>
        /// <returns></returns>
        private string GenerNodeID()
        {
            try
            {

                string FK_Flow = getUTF8ToString("FK_Flow");
                string x = getUTF8ToString("x");
                string y = getUTF8ToString("y");
                int iX = 0;
                int iY = 0;
                if (!string.IsNullOrEmpty(x)) iX = int.Parse(x);
                if (!string.IsNullOrEmpty(y)) iY = int.Parse(y);

                int nodeId = BP.BPMN.Glo.NewNode(FK_Flow, iX, iY);
                BP.WF.Node node = new BP.WF.Node(nodeId);
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = true, msg = "", data = new { NodeID = nodeId, text = node.Name } });
            }
            catch (Exception ex)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, msg = ex.Message, data = new { } });
            }
        }
        /// <summary>
        /// 根据节点编号删除流程节点
        /// </summary>
        /// <returns>执行结果</returns>
        private string DeleteNodeOfNodeID()
        {
            try
            {
                int delResult = 0;
                string FK_Node = getUTF8ToString("FK_Node");
                if (!string.IsNullOrEmpty(FK_Node))
                {
                    BP.WF.Node node = new BP.WF.Node(int.Parse(FK_Node));
                    if (node.IsExits == false)
                        return "true";

                    if (node.IsStartNode == true)
                    {
                        return "Start Node not can delete.";
                    }
                    delResult = node.Delete();
                }
                if (delResult > 0) return "true";

                return "Delete Error.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 修改节点名称
        /// </summary>
        /// <returns></returns>
        private string EditNodeName()
        {
            string FK_Node = getUTF8ToString("NodeID");
            string NodeName = getUTF8ToString("NodeName");

            BP.WF.Node node = new BP.WF.Node();
            node.NodeID = int.Parse(FK_Node);
            int iResult = node.RetrieveFromDBSources();
            if(iResult > 0)
            {
                node.Name =NodeName;
                node.Update();
                return "true";
            }
            return "false";
        }
        /// <summary>
        /// 保存流程图信息
        /// </summary>
        /// <returns></returns>
        private string Save()
        {
            //流程格式.
            string diagram = getUTF8ToString("diagram");
            //流程图.
            string png = getUTF8ToString("png");
            // 流程编号.
            string flowNo = getUTF8ToString("diagramId");
            //节点到节点关系
            string direction = getUTF8ToString("direction");
            //直接保存流程图信息
            Flow fl = new Flow(flowNo);
            fl.FlowJson = diagram; //直接保存了.

            //节点方向
            string[] dir_Nodes = direction.Split('@');
            BP.WF.Direction drToNode = new BP.WF.Direction();
            drToNode.Delete(BP.WF.DirectionAttr.FK_Flow, flowNo);
            foreach (string item in dir_Nodes)
            {
                if (string.IsNullOrEmpty(item)) continue;
                string[] nodes = item.Split(':');
                if (nodes.Length == 2)
                {
                    drToNode = new BP.WF.Direction();
                    drToNode.FK_Flow = flowNo;
                    drToNode.Node = int.Parse(nodes[0]);
                    drToNode.ToNode = int.Parse(nodes[1]);
                    drToNode.Insert();
                }
            }
            //节点集合.
            BP.WF.Nodes nds = new BP.WF.Nodes(fl.No);
            //foreach (DataTable dt in ds.Tables)
            //{
            //}
            return "true";
        }
        /// <summary>
        /// 另存为流程图
        /// </summary>
        /// <returns></returns>
        private string SaveAs()
        {
            return "";
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}