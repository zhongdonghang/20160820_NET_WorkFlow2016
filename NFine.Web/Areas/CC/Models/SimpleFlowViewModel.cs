using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NFine.Web.Areas.CC.Models
{
    public class SimpleFlowViewModel
    {

        public static SimpleFlowViewModel Flow2Obj(BP.WF.Flow item)
        {
            SimpleFlowViewModel vm = new SimpleFlowViewModel();
            vm.No = item.No.ToString();
            vm.Name = item.Name.ToString();
            vm.FK_FlowSort = item.FK_FlowSort.ToString();
            vm.FK_FlowSortText = item.FK_FlowSortText.ToString();
            //  vm.FlowRunWay = int.Parse(item.FlowRunWay.ToString());
            vm.RunObj = item.RunObj.ToString();
            vm.Note = item.Note.ToString();
            // vm.RunSQL = item.RunSQL.ToString();
            vm.NumOfBill = int.Parse(item.NumOfBill.ToString());
            vm.NumOfDtl = int.Parse(item.NumOfDtl.ToString());
            //  vm.FlowAppType = int.Parse(item.FlowAppType.ToString());
            vm.ChartType = int.Parse(item.ChartType.ToString());
            vm.IsCanStart = item.IsCanStart ? 1 : 0;
            vm.AvgDay = int.Parse(item.AvgDay.ToString());
            vm.IsFullSA = item.IsFullSA ? 1 : 0;
            vm.IsMD5 = item.IsMD5 ? 1 : 0;
            // vm.Idx = int.Parse(item.Idx.ToString());
            //  vm.TimelineRole = int.Parse(item.TimelineRole.ToString());
            vm.Paras = item.Paras.ToString();
            vm.PTable = item.PTable.ToString();
            //  vm.Draft = int.Parse(item.Draft.ToString());
            //  vm.DataStoreModel = int.Parse(item.DataStoreModel.ToString());
            vm.TitleRole = item.TitleRole.ToString();
            vm.FlowMark = item.FlowMark.ToString();
            vm.FlowEventEntity = item.FlowEventEntity.ToString();
            vm.HistoryFields = item.HistoryFields.ToString();
            vm.IsGuestFlow = item.IsGuestFlow ? 1 : 0;
            vm.BillNoFormat = item.BillNoFormat.ToString();
            vm.FlowNoteExp = item.FlowNoteExp.ToString();
            // vm.DRCtrlType = int.Parse(item.DRCtrlType.ToString());
            // vm.StartLimitRole = int.Parse(item.StartLimitRole.ToString());
            vm.StartLimitPara = item.StartLimitPara.ToString();
            vm.StartLimitAlert = item.StartLimitAlert.ToString();
            //    vm.StartLimitWhen = int.Parse(item.StartLimitWhen.ToString());
            //  vm.StartGuideWay = int.Parse(item.StartGuideWay.ToString());
            vm.StartGuidePara1 = item.StartGuidePara1.ToString();
            vm.StartGuidePara2 = item.StartGuidePara2.ToString();
            vm.StartGuidePara3 = item.StartGuidePara3.ToString();
            vm.IsResetData = item.IsResetData ? 1 : 0;
            vm.IsLoadPriData = item.IsLoadPriData ? 1 : 0;
            //vm.CFlowWay = int.Parse(item.CFlowWay.ToString());
            vm.CFlowPara = item.CFlowPara.ToString();
            vm.IsBatchStart = item.IsBatchStart ? 1 : 0;
            vm.BatchStartFields = item.BatchStartFields.ToString();
            vm.IsAutoSendSubFlowOver = item.IsAutoSendSubFlowOver ? 1 : 0;
            vm.Ver = item.Ver.ToString();
            //  vm.DType = int.Parse(item.DType.ToString());
            // vm.AtPara = item.AtPara.ToString();
            // vm.DTSWay = int.Parse(item.DTSWay.ToString());
            vm.DTSDBSrc = item.DTSDBSrc.ToString();
            vm.DTSBTable = item.DTSBTable.ToString();
            vm.DTSBTablePK = item.DTSBTablePK.ToString();
            //  vm.DTSTime = int.Parse(item.DTSTime.ToString());
            vm.DTSSpecNodes = item.DTSSpecNodes.ToString();
            //  vm.DTSField = int.Parse(item.DTSField.ToString());
            vm.DTSFields = item.DTSFields.ToString();
            return vm;
        }

        public static List<SimpleFlowViewModel> Table2Obj(DataTable dt )
        {
            List<SimpleFlowViewModel> list = new List<SimpleFlowViewModel>();
            try
            {
                foreach (DataRow r in dt.Rows)
                {
                    SimpleFlowViewModel vm = new SimpleFlowViewModel();
                    vm.No = r["No"].ToString();
                    vm.Name = r["Name"].ToString();
                    vm.FK_FlowSort = r["FK_FlowSort"].ToString();
                    vm.FK_FlowSortText = r["FK_FlowSortText"].ToString();
                    vm.FlowRunWay = int.Parse( r["FlowRunWay"].ToString());
                    vm.RunObj = r["RunObj"].ToString();
                    vm.Note = r["Note"].ToString();
                    vm.RunSQL = r["RunSQL"].ToString();
                    vm.NumOfBill = int.Parse( r["NumOfBill"].ToString());
                    vm.NumOfDtl = int.Parse(r["NumOfDtl"].ToString());
                    vm.FlowAppType = int.Parse(r["FlowAppType"].ToString());
                    vm.ChartType = int.Parse(r["ChartType"].ToString());
                    vm.IsCanStart = int.Parse(r["IsCanStart"].ToString());
                    vm.AvgDay = int.Parse(r["AvgDay"].ToString());
                    vm.IsFullSA = int.Parse(r["IsFullSA"].ToString());
                    vm.IsMD5 = int.Parse(r["IsMD5"].ToString());
                    vm.Idx = int.Parse(r["Idx"].ToString());
                    vm.TimelineRole = int.Parse(r["TimelineRole"].ToString());
                    vm.Paras = r["Paras"].ToString();
                    vm.PTable = r["PTable"].ToString();
                    vm.Draft = int.Parse(r["Draft"].ToString());
                    vm.DataStoreModel = int.Parse(r["DataStoreModel"].ToString());
                    vm.TitleRole = r["TitleRole"].ToString();
                    vm.FlowMark = r["FlowMark"].ToString();
                    vm.FlowEventEntity = r["FlowEventEntity"].ToString();
                    vm.HistoryFields = r["HistoryFields"].ToString();
                    vm.IsGuestFlow = int.Parse(r["IsGuestFlow"].ToString());
                    vm.BillNoFormat = r["BillNoFormat"].ToString();
                    vm.FlowNoteExp = r["FlowNoteExp"].ToString();
                    vm.DRCtrlType = int.Parse(r["DRCtrlType"].ToString());
                    vm.StartLimitRole = int.Parse(r["StartLimitRole"].ToString());
                    vm.StartLimitPara = r["StartLimitPara"].ToString();
                    vm.StartLimitAlert = r["StartLimitAlert"].ToString();
                    vm.StartLimitWhen = int.Parse(r["StartLimitWhen"].ToString());
                    vm.StartGuideWay = int.Parse(r["StartGuideWay"].ToString());
                    vm.StartGuidePara1 = r["StartGuidePara1"].ToString();
                    vm.StartGuidePara2 = r["StartGuidePara2"].ToString();
                    vm.StartGuidePara3 = r["StartGuidePara3"].ToString();
                    vm.IsResetData = int.Parse(r["IsResetData"].ToString());
                    vm.IsLoadPriData = int.Parse(r["IsLoadPriData"].ToString());
                    vm.CFlowWay = int.Parse(r["CFlowWay"].ToString());
                    vm.CFlowPara = r["CFlowPara"].ToString();
                    vm.IsBatchStart = int.Parse(r["IsBatchStart"].ToString());
                    vm.BatchStartFields = r["BatchStartFields"].ToString();
                    vm.IsAutoSendSubFlowOver = int.Parse(r["IsAutoSendSubFlowOver"].ToString());
                    vm.Ver = r["Ver"].ToString();
                    vm.DType = int.Parse(r["DType"].ToString());
                    vm.AtPara = r["AtPara"].ToString();
                    vm.DTSWay = int.Parse(r["DTSWay"].ToString());
                    vm.DTSDBSrc = r["DTSDBSrc"].ToString();
                    vm.DTSBTable = r["DTSBTable"].ToString();
                    vm.DTSBTablePK = r["DTSBTablePK"].ToString();
                    vm.DTSTime = int.Parse(r["DTSTime"].ToString());
                    vm.DTSSpecNodes = r["DTSSpecNodes"].ToString();
                    vm.DTSField = int.Parse(r["DTSField"].ToString());
                    vm.DTSFields = r["DTSFields"].ToString();
                    list.Add(vm);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        #region 公开属性
        public System.String No { get; set; }
        public System.String Name { get; set; }
        public System.String FK_FlowSort { get; set; }
        public System.String FK_FlowSortText { get; set; }
        public System.Int32 FlowRunWay { get; set; }
        public System.String RunObj { get; set; }
        public System.String Note { get; set; }
        public System.String RunSQL { get; set; }
        public System.Int32 NumOfBill { get; set; }
        public System.Int32 NumOfDtl { get; set; }
        public System.Int32 FlowAppType { get; set; }
        public System.Int32 ChartType { get; set; }
        public System.Int32 IsCanStart { get; set; }
        public System.Double AvgDay { get; set; }
        public System.Int32 IsFullSA { get; set; }
        public System.Int32 IsMD5 { get; set; }
        public System.Int32 Idx { get; set; }
        public System.Int32 TimelineRole { get; set; }
        public System.String Paras { get; set; }
        public System.String PTable { get; set; }
        public System.Int32 Draft { get; set; }
        public System.Int32 DataStoreModel { get; set; }
        public System.String TitleRole { get; set; }
        public System.String FlowMark { get; set; }
        public System.String FlowEventEntity { get; set; }
        public System.String HistoryFields { get; set; }
        public System.Int32 IsGuestFlow { get; set; }
        public System.String BillNoFormat { get; set; }
        public System.String FlowNoteExp { get; set; }
        public System.Int32 DRCtrlType { get; set; }
        public System.Int32 StartLimitRole { get; set; }
        public System.String StartLimitPara { get; set; }
        public System.String StartLimitAlert { get; set; }
        public System.Int32 StartLimitWhen { get; set; }
        public System.Int32 StartGuideWay { get; set; }
        public System.String StartGuidePara1 { get; set; }
        public System.String StartGuidePara2 { get; set; }
        public System.String StartGuidePara3 { get; set; }
        public System.Int32 IsResetData { get; set; }
        public System.Int32 IsLoadPriData { get; set; }
        public System.Int32 CFlowWay { get; set; }
        public System.String CFlowPara { get; set; }
        public System.Int32 IsBatchStart { get; set; }
        public System.String BatchStartFields { get; set; }
        public System.Int32 IsAutoSendSubFlowOver { get; set; }
        public System.String Ver { get; set; }
        public System.Int32 DType { get; set; }
        public System.String AtPara { get; set; }
        public System.Int32 DTSWay { get; set; }
        public System.String DTSDBSrc { get; set; }
        public System.String DTSBTable { get; set; }
        public System.String DTSBTablePK { get; set; }
        public System.Int32 DTSTime { get; set; }
        public System.String DTSSpecNodes { get; set; }
        public System.Int32 DTSField { get; set; }
        public System.String DTSFields { get; set; } 
        #endregion
    }
}