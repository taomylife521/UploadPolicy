using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyService.Core;
using ND.PolicyService.Core.SplitCore;
using ND.PolicyService.Enums;
using ND.PolicyService.Enums.Upload;
using ND.PolicyService.Enums.Upload.Qunar;
using ND.PolicySplitService.Core;
using ND.PolicySplitService.Core.impl;
using ND.PolicyUploadService.Core.impl;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.dtoEntity;
using ND.PolicyUploadService.DtoModel.Qunar;
using ND.PolicyUploadService.DtoModel.QunarCode;
using ND.PolicyUploadService.DtoModel.QunarUploadConfig;
using ND.PolicyUploadService.DtoModel.RealTimeUpload;
using ND.PolicyUploadService.DtoModel.SplitPolicy;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ND.PolicyUploadService.WinformClient
{
    public partial class Form1 : Form
    {
       // event EventHandler<EventMsg> onListBoxItemAdded;
       // Action<EventMsg> showMsg = null;//用于显示上传日志
       //// Action<DataTable,int> showPolicy = null;//用于显示查询政策
       // Action<DataTable> showUploadRecord = null;//用于显示上传记录

        private delegate void ShowPolicy(DataTable dt,int totalCount);//显示政策
        private delegate void ShowUploadPolicy(EventMsg e);//显示上传日志
       // private delegate void ShowRealTimeUpload(EventMsg e);//显示去哪儿实时上传日志
        private delegate void ShowUploadRec(DataTable dt);//显示上传记录
        private delegate void ClearDataGridViewRec();//显示上传记录
        public static List<string> lstQunarCodes = new List<string>();

        #region 定义委托方法
        public void ShowPolicyData(DataTable dt, int totalCount)
        {
            if (this.InvokeRequired)
            {
                ShowPolicy setpos = new ShowPolicy(ShowPolicyData);
                this.Invoke(setpos, new object[] { dt, totalCount });
            }
            else
            {
                this.lbTotalCount.Text = this.lbTotalCount.Text + totalCount.ToString();

                if (dt.Rows.Count > 0)
                {
                    this.dataGridView1.DataSource = dt;
                }
            }
        }

        #region 实时上传委托
        //public void ShowRealTimeUploadLog(EventMsg e)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        ShowRealTimeUpload setpos = new ShowRealTimeUpload(ShowRealTimeUploadLog);
        //        this.Invoke(setpos, new object[] { e });
        //    }
        //    else
        //    {
        //        LogContext log = new LogContext();
        //        string dis = string.IsNullOrEmpty(e.Msg) ? "" : ",描述:" + e.Msg;
        //        string exption = e.Exception == null ? "" : ",异常:" + e.Exception;
        //        if (this.lstRealTimeToQunar.Items.Count >= 34) this.lstRealTimeToQunar.Items.Remove(this.lstBoxLog.Items[0]);

        //        this.lstRealTimeToQunar.Items.Add(DateTime.Now + ":上传平台:" + e.PurchaserType.ToString() + "状态:" + e.Status.ToString() + dis + exption);

        //        string path = System.IO.Directory.GetCurrentDirectory();
        //        DateTime CurrTime = DateTime.Now;
        //        if (e.Status == RunStatus.Exception)
        //        {
        //            string strPath = path + "\\HandDataLog\\QunarRealTime\\" + CurrTime.Year + "-" + CurrTime.Month + "\\" + CurrTime.Day + ".txt";
        //            log.AddLogInfo(strPath, DateTime.Now + ":" + JsonConvert.SerializeObject(e).ToString(), true);
        //        }
        //    }
        //}
        
        #endregion
        public void ClearDataGirdViewRecord()
        {
            if (this.InvokeRequired)
            {
                ClearDataGridViewRec setpos = new ClearDataGridViewRec(ClearDataGirdViewRecord);
                this.Invoke(setpos);
            }
            else
            {
                //this.dataGridView1.Rows.Clear();
                this.dataGridView1.DataSource = null;
            }
        }

        public void ShowUploadPolicyLog(EventMsg e)
        {
            if (this.InvokeRequired)
            {
                ShowUploadPolicy setpos = new ShowUploadPolicy(ShowUploadPolicyLog);
                this.Invoke(setpos, new object[] { e });
            }
            else
            {
                LogContext log = new LogContext();
                //showMsg = new Action<EventMsg>((txt) =>
                // {
                string dis = string.IsNullOrEmpty(e.Msg) ? "" : ",描述:" + e.Msg;
                string exption = e.Exception == null ? "" : ",异常:" + e.Exception;
                if (this.lstBoxLog.Items.Count >= 34) this.lstBoxLog.Items.Remove(this.lstBoxLog.Items[0]);

                this.lstBoxLog.Items.Add(DateTime.Now + ":上传平台:" + e.PurchaserType.ToString() + "状态:" + e.Status.ToString() + dis + exption);

                string path = System.IO.Directory.GetCurrentDirectory();
                DateTime CurrTime = DateTime.Now;
                string strPath = path + "\\HandDataLog\\" + CurrTime.Year + "-" + CurrTime.Month + "\\" + CurrTime.Day + ".txt";
                log.AddLogInfo(strPath, DateTime.Now + ":" + JsonConvert.SerializeObject(e).ToString(), true);
            }
        }

        public void ShowUploadRecord(DataTable dt)
        {
            if (this.InvokeRequired)
            {
                ShowUploadRec setpos = new ShowUploadRec(ShowUploadRecord);
                this.Invoke(setpos, new object[] { dt });
            }
            else
            {
                 this.dataGridView2.DataSource = dt;
            }
        }
        #endregion
      
        public Form1()
        {
           
            InitializeComponent();
            this.cmbUploadType.SelectedIndex = 1;
            this.cmbPlatform.SelectedIndex = 0;
            this.cmbPolicyType.SelectedIndex = 0;
            this.cmbAirCode.SelectedIndex = 0;
          
           lstQunarCodes = CoreHelper.ReadQunarCodes();
           
           lstQunarCodes.ForEach(x =>
              {
                  lbQunarCodes.Items.Add(x);
              });
           ClearLog(); //清除保留日志
           this.cbCardType.SelectedIndex = 0;
           BindDefaultQunarConfig();
              
        }

        #region 载入默认去哪儿上传配置
       

        public void BindDefaultQunarConfig()
        {
            QunarUploadConfigResponse config =CoreHelper.LoadQunarUploadConfig();
            this.cbCardType.SelectedIndex = int.Parse(config.CardType);
            this.txtMaxAge.Text = config.MaxAge.ToString();
            this.txtMinAge.Text = config.MinAge.ToString();
            this.txtCPCReturnPoint.Text = config.CPCReturnPoint.ToString();
            this.txtCPCReturnPrice.Text = config.CPCReturnPrice.ToString();
            this.txtCPAReturn.Text = config.SpecialConfig.CPAReturn;
            this.txtCPAChange.Text = config.SpecialConfig.CPAChange;
            if (config.SpecialConfig.CPAIsEnrosement == "是")
            {
                this.chkIsEndorsement.Checked = true;
            }
            else
            {
                this.chkIsEndorsement.Checked = false;
            }

            this.txtCPCReturn.Text = config.SpecialConfig.CPCReturn;
            this.txtCPCChange.Text = config.SpecialConfig.CPCChange;
            if (config.SpecialConfig.CPCIsEnrosement == "是")
            {
                this.chkCPCIsEndorsement.Checked = true;
            }
            else
            {
                this.chkCPCIsEndorsement.Checked = false;
            }
            if (string.IsNullOrEmpty(config.SpecialConfig.SpecialTicketRemark))
            {
                return;
            }
            List<string> lstTicketRemark = config.SpecialConfig.SpecialTicketRemark.Split(',').ToList();

            foreach (var item in this.grpSpecialRemark.Controls)
            {
                if (item is CheckBox)
                {
                    var chk = item as CheckBox;
                    foreach (var item2 in lstTicketRemark)
                    {
                        if (chk.Tag.ToString() == item2)
                        {
                            chk.Checked = true;
                        }

                    }
                }
            }

            foreach (var item in this.grpIsShare.Controls)
            {
                if (item is  RadioButton)
                {
                    var chk = item as RadioButton;

                    if (chk.Text.ToString() == config.IsShareFlight)
                        {
                            chk.Checked = true;
                        }

                    
                }
            }

            foreach (var item in this.grpIsStop.Controls)
            {
                if (item is RadioButton)
                {
                    var chk = item as RadioButton;

                    if (chk.Text.ToString() == config.IsStopFlight)
                    {
                        chk.Checked = true;
                    }


                }
            }
        }
        #endregion

        #region 清除5天前的保留日志
        public void ClearLog()
        {
            Task.Factory.StartNew(() =>
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                DateTime CurrTime = DateTime.Now;
                for (int i = 1; i < CurrTime.Day - 7; i++)
                {
                    try
                    {
                        string strPath = path + "\\HandDataLog\\" + CurrTime.Year + "-" + CurrTime.Month + "\\" + i + ".txt";
                        File.Delete(strPath);
                        strPath = path + "\\HandDataLog\\QunarRealTime\\" + CurrTime.Year + "-" + CurrTime.Month + "\\" + i + ".txt";
                         File.Delete(strPath);
                    }
                    catch (Exception ex)
                    { }
                }
            });
            
        }
        #endregion

        #region Form1_Load
        private void Form1_Load(object sender, EventMsg e)
        {

           
        }
        #endregion

        #region 载入上传记录
        public void LoadUploadRecord()
        {
            Task.Factory.StartNew(() =>
            {
                SearchNotifyRequest request = new SearchNotifyRequest();
                string responseContent = CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["PolicyNotifyUrl"].ToString(), request);
                if (string.IsNullOrEmpty(responseContent))
                {
                   // MessageBox.Show("未找到任何上传记录");
                    return;
                }
                SearchNotifyResponse rep = JsonConvert.DeserializeObject<SearchNotifyResponse>(responseContent);
                if (rep.ErrCode == ResultType.Failed)
                {
                   // MessageBox.Show(rep.ErrMsg);
                    return;
                }
                DataTable dt = ListToDataTableWithRec(rep.NotifyList);
                ShowUploadRecord(dt);
               //this.Invoke(showUploadRecord, dt);
              
                //AddDataAction = new Action(() =>
                //{
                   //this.dataGridView2.DataSource = dt;
               // });
              

            });
           

        }
        #endregion

        #region 填充查询政策请求
        public SearchPolicyRequest FillSearchPolicyRequest()
        {
            try
            {
                StringBuilder sqlWhere = new StringBuilder();
                sqlWhere.Append(" 1=1 ");
                if (this.txtDptCode.Text.Length > 0)
                {
                    sqlWhere.Append(" and  pd.DptCity like '%" + this.txtDptCode.Text + "%'  ");//DptCity = " + this.txtDptCode.Text
                }
                if (this.txtArrCode.Text.Length > 0)
                {
                    sqlWhere.Append(" and pd.ArrCity like '%" + this.txtArrCode.Text+"%'");
                }
                if(this.txtSeatCode.Text.Length > 0)
                {
                    sqlWhere.Append(" and pd.Seat like '%" + this.txtSeatCode.Text + "%'");
                }
                if (this.txtCommsionPointMin.Text.Length > 0)
                {
                    sqlWhere.Append(" and pd.CommisionPoint >= " + this.txtCommsionPointMin.Text);
                }
                if (this.txtCommsionPointMax.Text.Length > 0)
                {
                    sqlWhere.Append(" and pd.CommisionPoint <= " + this.txtCommsionPointMax.Text);
                }
                if (this.txtMoneyMin.Text.Length > 0)
                {
                    sqlWhere.Append(" and pd.CommisionMoney >= " + this.txtMoneyMin.Text);
                }
                if (this.txtMoneyMax.Text.Length > 0)
                {
                    sqlWhere.Append(" and pd.CommisionMoney <= " + this.txtMoneyMax.Text);
                }
                if (this.txtPolicyId.Text.Length > 0)
                {
                    sqlWhere.Append(" and ps.Id = " + this.txtPolicyId.Text);
                }
                if(!this.cmbAirCode.SelectedItem.ToString().Contains("全部"))
                {
                    //if (this.cmbAirCode.SelectedItem.ToString().ToUpper() == "G5")
                    //{
                    //    sqlWhere.Append("id IN(SELECT TOP 1 id from dbo.PolicyDetail WHERE CommisionPoint=( SELECT MAX(CommisionPoint) FROM dbo.PolicyDetail WHERE AirlineCode='G5' )UNIONSELECT TOP 1 id FROM dbo.PolicyDetail where Seat =(SELECT TOP 1 Seat FROM dbo.PolicyDetail  WHERE AirlineCode='G5' GROUP BY Seat ORDER BY MAX(LEN(Seat)) desc))");
                    //}
                    //else
                    //{
                        sqlWhere.Append(" and pd.AirlineCode = '" + this.cmbAirCode.SelectedItem.ToString() + "'");
                    //}
                }
                UploadType uType = new UploadType();
                switch (cmbUploadType.SelectedItem.ToString())
                {
                    case "全量上传":
                        uType = UploadType.FullUpload;
                        break;
                    case "增量上传":
                        uType = UploadType.Incremental;
                        break;
                    default:
                        uType = UploadType.FullUpload;
                        break;
                }
                PurchaserType pType = new PurchaserType();
                switch (cmbPlatform.SelectedItem.ToString())
                {
                    case "淘宝":
                        pType = PurchaserType.TaoBao;
                        break;
                    case "去哪儿":
                        pType = PurchaserType.Qunar;
                        break;
                    default:
                        pType = PurchaserType.Qunar;
                        break;
                }
                int pageSize = 100;
                if (this.txtPageSize.Text.Length > 0)
                {
                    pageSize = int.Parse(this.txtPageSize.Text);
                }
                decimal addCommsionPoint = 0;
                if (this.txtAddCommsionPoint.Text.Length> 0)
                {
                    addCommsionPoint = decimal.Parse(this.txtAddCommsionPoint.Text);
                }
                decimal addMoney = 0;
                if (this.txtAddMoney.Text.Length > 0)
                {
                    addMoney = decimal.Parse(this.txtAddMoney.Text);
                }
                QunarPolicyType policyType = new QunarPolicyType();
                switch(cmbPolicyType.SelectedItem.ToString())
                {
                    case "单程普通政策":
                        policyType = QunarPolicyType.COMMON;
                        break;
                    case "单程预付政策":
                        policyType = QunarPolicyType.PREPAY;
                        break;
                    default:
                        break;
                }
                 if(this.txtUploadName.Text.Length <= 0)
                {
                    MessageBox.Show("上传和查询人员姓名不能为空!");
                    return null;
                }
                SearchPolicyRequest request = new SearchPolicyRequest()
                {
                    PageSize = pageSize,
                    SqlWhere = sqlWhere.ToString(),
                    UType = uType,
                    pType = pType,
                    CommisionMoney = addMoney,
                    CommsionPoint = addCommsionPoint,
                    PolicyType = policyType,
                    OperName = this.txtUploadName.Text

                };
               
                return request;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        } 
        #endregion


       

        #region List转DataTable Policy
        /// <summary>
        /// 将泛类型集合List类转换成DataTable
        /// </summary>
        /// <param name="list">泛类型集合</param>
        /// <returns></returns>
        public  DataTable ListToDataTable(List<Policies> lstDatas)
        {
            DataTable dt = ConstrctDataHeader();
            lstDatas.ForEach(x =>
            {
                DataRow dr = dt.NewRow();
                dr[0] = x.Id.ToString();//自有政策代码
                dr[1] = x.PartnerName.ToString();//供应商名称
                dr[2] = x.PartnerPolicyId.ToString();//供应商政策id
                dr[3] = x.PolicyId.ToString();//拆分前政策id
                dr[4] = x.SrcType.ToString();//航程类型
                dr[5] = x.AirlineCode.ToString();//航空公司
                dr[6] = x.DptCity.ToString();//出发城市
                dr[7] = x.ArrCity.ToString();//抵达城市
                dr[8] = x.CommisionPoint.ToString();//返点
                dr[9] = x.CommisionMoney.ToString();//留钱
                dr[10] = x.Comment.ToString();//备注
                dr[11] = x.CommisionType.ToString();//政策类型
                dr[12] = x.FlightIn.ToString();//包含航班
                dr[13] = x.FlightOut.ToString();//不包含航班
                dr[14] = x.Seat.ToString();//舱位码
                dr[15] = x.SaleEffectDate.ToString();//销售有效期开始
                dr[16] = x.SaleExpireDate.ToString();//销售有消息结束
                dr[17] = x.SaleForbidEffectDate.ToString();//禁止销售有效期开始
                dr[18] = x.SaleForbidExpireDate.ToString();//禁止销售有效期结束
                dr[19] = x.FlightEffectDate.ToString();//旅游有效期开始
                dr[20] = x.FlightExpireDate.ToString();//旅游有效期结束
                dr[21] = x.FlightForbidEffectDate.ToString();//禁止旅游有效期开始
                dr[22] = x.FlightForbidExpireDate.ToString();//禁止旅游有效期结束
                dr[23] = x.OfficeNo.ToString();//officeNo
                dr[24] = x.NeedSwitchPNR.ToString();//是否需要更换pnr
                dr[25] = x.IsAutoIssue.ToString();//是否自动出票
                dr[26] = x.IsSharingFlight.ToString();//是否是共享航班
                dr[27] = x.ChangeWorkTime.ToString();//改期时间
                dr[28] = x.ReturnWorkTime.ToString();//退票时间
                dr[29] = x.VtWorkTime.ToString();//废票时间
                dr[30] = x.ChangeWorkTimeWeekend.ToString();//改期周末时间
                dr[31] = x.ReturnWorkTimeWeekend.ToString();//退票周末时间
                dr[32] = x.VtWorkTimeWeekend.ToString();//废票周末时间
                dr[33] = x.IssueWorkTime.ToString();//出票时间
                dr[34] = x.IssueWorkTimeWeekend.ToString();//出票周末时间
                dr[35] = x.FlightCycle.ToString();//航班周期
                dr[36] = x.CreateTime.ToString();//创建时间
                dr[37] = x.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");//更新时间
                dr[38] = x.DelDegree.ToString();//是否有效
                dr[39] = x.IsUpload==1?"已上传":"未上传";//是否有效
                dt.Rows.Add(dr);
               
            });
            return dt;
        }
        public DataTable ConstrctDataHeader()
        {
 
                DataTable dt = new DataTable();
                dt.Columns.Add("自有政策代码", typeof(string));
                dt.Columns.Add("供应商名称", typeof(string));
                dt.Columns.Add("供应商政策id", typeof(string));
                dt.Columns.Add("拆分前政策id", typeof(string));
                dt.Columns.Add("航程类型", typeof(string));
                dt.Columns.Add("航空公司", typeof(string));
                dt.Columns.Add("出发城市", typeof(string));
                dt.Columns.Add("抵达城市", typeof(string));
                dt.Columns.Add("返点", typeof(decimal));
                dt.Columns.Add("留钱", typeof(decimal));
                dt.Columns.Add("政策备注", typeof(string));
                dt.Columns.Add("政策类型", typeof(string));
                dt.Columns.Add("包含航班", typeof(string));
                dt.Columns.Add("不包含航班", typeof(string));
                dt.Columns.Add("舱位码", typeof(string));
                dt.Columns.Add("销售有效期开始", typeof(string));
                dt.Columns.Add("销售有消息结束", typeof(string));
                dt.Columns.Add("禁止销售有效期开始", typeof(string));
                dt.Columns.Add("禁止销售有效期结束", typeof(string));
                dt.Columns.Add("旅游有效期开始", typeof(string));
                dt.Columns.Add("旅游有效期结束", typeof(string));
                dt.Columns.Add("禁止旅游有效期开始", typeof(string));
                dt.Columns.Add("禁止旅游有效期结束", typeof(string));
                dt.Columns.Add("officeNo", typeof(string));
                dt.Columns.Add("是否需要更换pnr", typeof(string));
                dt.Columns.Add("是否自动出票", typeof(string));
                dt.Columns.Add("是否是共享航班", typeof(string));
                dt.Columns.Add("改期时间", typeof(string));
                dt.Columns.Add("退票时间", typeof(string));
                dt.Columns.Add("废票时间", typeof(string));
                dt.Columns.Add("改期周末时间", typeof(string));
                dt.Columns.Add("退票周末时间", typeof(string));
                dt.Columns.Add("废票周末时间", typeof(string));
                dt.Columns.Add("出票时间", typeof(string));
                dt.Columns.Add("出票周末时间", typeof(string));
                dt.Columns.Add("航班周期", typeof(string));
                dt.Columns.Add("创建时间", typeof(string));
                dt.Columns.Add("更新时间", typeof(string));
                dt.Columns.Add("是否有效", typeof(string));
                dt.Columns.Add("是否已上传", typeof(string));
                dt.Columns[0].AutoIncrement = true;
                return dt;
  
        }
        #endregion

        #region List To DataTable UploadRecord
         /// <summary>
        /// 将泛类型集合List类转换成DataTable
        /// </summary>
        /// <param name="list">泛类型集合</param>
        /// <returns></returns>
        public  DataTable ListToDataTableWithRec(List<UpLoadRecordDto> lstDatas)
        {
            DataTable dt = ConstrctRecordDataHeader();
            lstDatas.ForEach(x =>
            {
                DataRow dr = dt.NewRow();
                dr[0] = x.Id.ToString();//自有政策代码
                dr[1] = x.PurchaserType.ToString();//上传平台
                dr[2] = x.UploadType.ToString();//上传类型
                dr[3] = x.CreateTime.ToString();//上传时间
                dr[4] = x.CompleteTime.ToString();//完成时间
                dr[5] = x.CompleteTime.Subtract(x.CreateTime).Seconds;//总耗时
                dr[6] = x.Uploadcount.ToString();//上传条数
                dr[7] = x.NotifyResult == 0 ? "上传中" : (x.NotifyResult == 1 ? "成功":"不成功");
                dr[8] = x.OperName.ToString();//操作人
                dr[9] = x.LastUpdateTime.ToString();//最后一条更新时间
                dr[10] = x.LastPolicyId.ToString();//最后一条更新编号
                dr[11] = x.Beforelastupdatetime.ToString();//上次更新时间
                dr[12] = x.Beforelastpolicyid.ToString();//上次更新编号
                dr[13] = x.RequestParams.ToString();//请求参数
                dr[14] = x.ResponseParams.ToString().Length > 200 ? x.ResponseParams.ToString().Substring(0, 200) + "....." : x.ResponseParams.ToString();//响应参数
                dr[15] = x.UploadFilePath.ToString();//上传包路径
                dr[16] = x.Remark.ToString();//备注
                dt.Rows.Add(dr);
               
            });
            return dt;
        }
        public DataTable ConstrctRecordDataHeader()
        {
 
                DataTable dt = new DataTable();
                dt.Columns.Add("上传编号", typeof(string));
                dt.Columns.Add("上传平台", typeof(string));
                dt.Columns.Add("上传类型", typeof(string));
                dt.Columns.Add("上传时间", typeof(string));
                dt.Columns.Add("完成时间", typeof(string));
                dt.Columns.Add("总耗时(单位:秒)", typeof(string));
                dt.Columns.Add("上传条数", typeof(string));
                dt.Columns.Add("上传结果", typeof(string));
                dt.Columns.Add("操作人", typeof(string));
                dt.Columns.Add("最后一条更新时间", typeof(string));
                dt.Columns.Add("最后一条更新编号", typeof(string));
                dt.Columns.Add("上次更新时间", typeof(string));
                dt.Columns.Add("上次更新编号", typeof(string));
                dt.Columns.Add("请求参数", typeof(string));
                dt.Columns.Add("响应参数", typeof(string));
                dt.Columns.Add("上传文件路径", typeof(string));
                dt.Columns.Add("备注", typeof(string));
                dt.Columns[0].AutoIncrement = true;
                return dt;
  
        }
        #endregion

        #region 查询
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            this.lbTotalCount.Text = "总条数:";
            dataGridView1.DataBindings.Clear();
            SearchPolicyRequest request = FillSearchPolicyRequest();
          
            if (request == null)
            {
                return;
            }
            request.IsUpload = false;
            request.IsSearchTotalCount = true;
           
          Task.Factory.StartNew(() =>
           {
                //  SearchPolicyResponse rep= ClientFactory<ISearchPolicy>.CreateService().SearchPolicy(request);
                string responseContent = CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["SearchPolicyUrl"].ToString(), request);
                if (string.IsNullOrEmpty(responseContent))
                {
                    // MessageBox.Show("未找到符合条件的政策!");
                    return;
                }
                SearchPolicyResponse rep = JsonConvert.DeserializeObject<SearchPolicyResponse>(responseContent);
                if (rep.lstPolicies.Count <= 0)
                {
                    this.lbTotalCount.Text = "总条数:";

                   // dataGridView1.Rows.Clear();
                    ClearDataGirdViewRecord();
                    MessageBox.Show("未找到符合条件的政策!");
                    return;
                }


                DataTable dt = ListToDataTable(rep.lstPolicies);
                //this.Invoke(showPolicy, dt, rep.lstPolicies.Count);
                ShowPolicyData(dt, rep.TotalCount);  
               
            });
           
        }
        #endregion

        #region 上传
        private void btnUpload_Click_1(object sender, EventArgs e)
        {
            SearchPolicyRequest request = FillSearchPolicyRequest();
            if (request == null)
            {
                return;
            }
            this.lstBoxLog.Items.Clear();
            request.IsUpload = true;
            if (MessageBox.Show("确定要上传吗?请认真核对!", "上传提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                // EmptyResponse rep = ClientFactory<IUpload>.CreateService().Upload(request);

                //string searchContent = CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["SearchPolicyUrl"].ToString(), request);
                //SearchPolicyResponse rep = JsonConvert.DeserializeObject<SearchPolicyResponse>(searchContent);//先查询一遍，获取要上传的政策
                //if (rep.lstPolicies.Count <= 0)
                //{
                //    this.lbTotalCount.Text = "总条数:";
                //    dataGridView1.DataBindings.Clear();
                //    MessageBox.Show("未找到符合条件的政策!");
                //    return;
                //}
                List<string> lstQunarCodes = new List<string>();
                foreach (var item in this.lbQunarCodes.Items)
                {
                    lstQunarCodes.Add(item.ToString().Replace("\r\n",""));
                }
                Task.Factory.StartNew(() =>
                {
                    UploadPolicy(request, lstQunarCodes);
                });
              
              
              
                //UploadResponse rep = JsonConvert.DeserializeObject<UploadResponse>(responseContent);
                //if (rep.ErrCode == ResultType.Failed)
                //{
                //    MessageBox.Show("上传失败:错误消息:" + rep.ErrMsg + "\r\n异常:" + rep.Excption);
                //    return;
                //}

               
               // MessageBox.Show("已提交上传请求，请稍后政策上传记录去查看上传状态!");
                //this.lbUploadStatusId.Tag = rep.UploadStatusId;
                //.lbUploadStatusId.Text = "点击查看上传状态!";
            }
        }
          #endregion


      

        #region 上传政策
        public void UploadPolicy(SearchPolicyRequest request,List<string> lstQunarCodes)
        {
            UploadPolicyResponse rep = new UploadPolicyResponse();
            
           
            switch (request.pType)
            {
                case PolicyService.Enums.PurchaserType.Qunar:
                    {
                        IUploadPolicy uplload = new QunarUpLoadPolicy();
                        uplload.OnWoking += uplload_OnWoking;
                        QunarUploadPolicyRequest qunarRequest = new QunarUploadPolicyRequest()
                        {
                            FormatFilePath = ConfigurationManager.AppSettings["FormatQunarFilePath"].ToString(),//xml文件路径
                            FormatZipFilePath = ConfigurationManager.AppSettings["FormatQunarZipFilePath"].ToString(),//压缩包文件路径
                            //IsPrintSql = Convert.ToBoolean(ConfigurationManager.AppSettings["IsPrintSql"].ToString()),
                            MaxTaskCount = int.Parse(ConfigurationManager.AppSettings["MaxTaskCount"].ToString()),
                            PerTaskCount = int.Parse(ConfigurationManager.AppSettings["PerTaskCount"].ToString()),
                            //PageSize = request.PageSize <= 0 ? 50 : request.PageSize,
                            PolicyType = request.PolicyType,
                            SqlWhere = request.SqlWhere,
                            UploadType =request.UType,
                            //QunarUpLoadUrl = ConfigurationManager.AppSettings["QunarUpLoadUrl"].ToString(),
                            CommisionMoney = request.CommisionMoney == null ? 0 : request.CommisionMoney,
                            CommsionPoint = request.CommsionPoint == null ? 0 : request.CommsionPoint,
                            OperName = request.OperName,
                            LstQunarCodes = lstQunarCodes,
                            PageSize = request.PageSize,
                            DefaultUploadConfig =CoreHelper.LoadQunarUploadConfig()
                          
                        };
                        if (request.UType == UploadType.Incremental)
                        {
                            rep = uplload.UpLoadIncrementPolicy(qunarRequest);
                        }
                        else if (request.UType == UploadType.FullUpload)
                        {
                            rep = uplload.UploadFullPolicy(qunarRequest);
                        }
                       
                    }
                    break;
                case PolicyService.Enums.PurchaserType.TaoBao:
                    MessageBox.Show("暂未开通淘宝上传接口");
                    return;
                    break;
                default:
                    MessageBox.Show("未知的上传平台");
                    return;
                    break;

            }
            MessageBox.Show("操作成功!");
        }

        #region 上传日志
        void uplload_OnWoking(object sender, EventMsg e)
        {
            ShowUploadPolicyLog(e);
           
        }  
        #endregion
        #endregion

        private void tbUploadPolicy_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tbUploadPolicy_TabIndexChanged(object sender, EventArgs e)
        {
            

        }

        #region 更改选项卡
        private void tbUploadPolicy_MouseClick(object sender, MouseEventArgs e)
        {
            LoadUploadRecord();
        } 
        #endregion

        #region 上传说明
        private void lbDiscription_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(@"通用：
                                 航司
                                 舱位
                                 政策类型
                                 返点
                                 出发
                                 到达
                                 班期限制
                                 开始售票日期
                                 售票结束日期
                                 旅行开始日期
                                 旅行结束日期
                                 航班号
                                 航班限制
                                 提前出票时限
                                 最早提前出票时限
                                 航班起飞时间
                                退票规则
                                改期规则
                                特殊票务说明
                                Newspecialrule
                                Discount
                                Printprice 
                                Sellprice 以上字段都相同的代表是同一条政策，不会上传到去哪儿！
                                判断政策是否相同的字段
                                普通政策还有：autoTicket是否自动出票，shared支持代码共享航班
                                特价:autoTicket是否自动出票
                                包机切位：restCabin剩余座位数,canPay是否可以支付
                                预付：canPay需要支付,needPat需要pata,needPnr需要pnr
                                申请：kTypeK位方式,autoticket是否自动出票,needPat进行pata校验,checkCycleTime巡查周期,autoCheckOverTime巡查总时间,manualCheckOverTime手工K位时限,downDiscount向下浮动点数
                ", "上传说明");
        } 
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
           
           // LoadUploadRecord();
        }

        #region 添加三字码
        private void btnAddCode_Click(object sender, EventArgs e)
        {
            if(txtQunarCode.Text.Length <= 0)
            {
                MessageBox.Show("要添加的三字码不能为空！");
                return;
            }
            if(!Regex.IsMatch(txtQunarCode.Text,@"^[a-zA-Z]{3}$"))
            {
                MessageBox.Show("输入的三字码必须是三个字母");
                return;
            }
            this.lbQunarCodes.Items.Add(txtQunarCode.Text.ToUpper());
            lstQunarCodes.Add(txtQunarCode.Text.ToUpper());
            QunarCodeList codeList = new QunarCodeList(){
              Code=lstQunarCodes
            };
            Task.Factory.StartNew(() =>
            {
                CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["AddQunarCodeUrl"].ToString(), new QunarCodeRequest() { Code = txtQunarCode.Text.ToUpper() }); 
            });
            //string content = XmlHelper.Serializer(typeof(QunarCodeList), codeList);
            //string path = System.IO.Directory.GetCurrentDirectory();
            //File.WriteAllText(path + "\\QunarCode.xml", content);
            MessageBox.Show("添加成功!");
        } 
        #endregion

        private void lbQunarCodes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region 双击去哪儿三字码
        private void lbQunarCodes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            lstQunarCodes.Remove(this.lbQunarCodes.SelectedItem.ToString());
            QunarCodeList codeList = new QunarCodeList()
            {
                Code = lstQunarCodes
            };
            Task.Factory.StartNew(() =>
            {
                CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["DeleteQunarCodeUrl"].ToString(), new QunarCodeRequest() { Code = this.lbQunarCodes.SelectedItem.ToString().ToUpper() });
            });
          //string content= XmlHelper.Serializer(typeof(QunarCodeList), codeList);
          //   string path = System.IO.Directory.GetCurrentDirectory();
          //   File.WriteAllText(path + "\\QunarCode.xml", content);
             this.lbQunarCodes.Items.Remove(this.lbQunarCodes.SelectedItem);
        }
        
        #endregion

        #region tab选项卡改变事件
        private void tbUploadPolicy_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 1)
            {
                LoadUploadRecord();
            }
        } 
        #endregion

        #region 查询去哪儿三字码
        private void btnSearchQunarCodes_Click(object sender, EventArgs e)
        {
            string code = this.txtQunarCode.Text;
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("查询三字码不能为空!");
                return;
            }
            bool flag = false;
            object selectItem = null;
            foreach (var item in lbQunarCodes.Items)
            {
                if (item.ToString().ToUpper() == code.ToUpper())
                {
                    flag = true;
                    selectItem = item;
                }
            }
            if (!flag)
            {
                MessageBox.Show("不存在该三字码！");
                return;
            }
            lbQunarCodes.SelectedItem = selectItem;
        }
        
        #endregion

        #region 保存去哪儿默认配置
        private void btnSaveSpecialPolicyConfig_Click(object sender, EventArgs e)
        {
            QunarUploadConfigResponse config = new QunarUploadConfigResponse();
              config.MaxAge = this.txtMaxAge.Text;
              config.MinAge = this.txtMinAge.Text;
              config.CardType = this.cbCardType.SelectedItem == "仅身份证"? "1" : "0";
              config.CPCReturnPoint = this.txtCPCReturnPoint.Text;
              config.CPCReturnPrice = this.txtCPCReturnPrice.Text;
              config.SpecialConfig.CPAChange = this.txtCPAChange.Text;
              config.SpecialConfig.CPAIsEnrosement = this.chkCPCIsEndorsement.Checked == true ? "是" : "否";
              config.SpecialConfig.CPAReturn = this.txtCPAReturn.Text;
              config.SpecialConfig.CPCChange = this.txtCPCChange.Text;
              config.SpecialConfig.CPCIsEnrosement = this.chkCPCIsEndorsement.Checked == true ? "是" : "否";
              config.SpecialConfig.CPCReturn = this.txtCPCReturn.Text;
              List<string> specilaTicketRemrk = new List<string>();
              foreach (var item in this.grpSpecialRemark.Controls)
              {
                  if (item is CheckBox)
                  {
                      var chk = item as CheckBox;
                      if (chk.Checked)
                      {
                          specilaTicketRemrk.Add(chk.Tag.ToString());
                      }
                  }
              }

              foreach (var item in this.grpIsShare.Controls)
              {
                  if (item is RadioButton)
                  {
                      var chk = item as RadioButton;
                      if (chk.Checked)
                      {
                          config.IsShareFlight = chk.Text;
                      }
                  }
              }
              foreach (var item in this.grpIsStop.Controls)
              {
                  if (item is RadioButton)
                  {
                      var chk = item as RadioButton;
                      if (chk.Checked)
                      {
                          config.IsStopFlight = chk.Text;
                      }
                  }
              }
              if (specilaTicketRemrk.Count > 4)
              {
                  MessageBox.Show("特殊票务说明不符合规定");
                  return;
              }
              config.SpecialConfig.SpecialTicketRemark = string.Join(",", specilaTicketRemrk.ToArray());
              CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["SaveQunarDefautConfigUrl"].ToString(), config);
             
              MessageBox.Show("保存成功!");

        } 
        #endregion

        #region 实时同步旧代码
        //#region 开始去哪儿实时同步
        //private void btnStartQunarSync_Click(object sender, EventArgs e)
        //{
        //    this.tmrRealTimeUpload.Interval = Convert.ToInt32(this.txtQunarInterval.Text) * 60 * 1000;
        //    this.tmrRealTimeUpload.Start();
        //    this.btnStartQunarSync.Enabled = false;
        //    this.btnStopQunarSync.Enabled = true;
        //}

        //void task_OnWorking(object sender, EventMsg e)
        //{
        //    ShowRealTimeUploadLog(e);

        //}
        //#endregion

        //#region 实时上传timer
        //private void tmrRealTimeUpload_Tick(object sender, EventArgs e)
        //{
        //    QunarPolicyType policyType = new QunarPolicyType();
        //    switch (cmbPolicyType.SelectedItem.ToString())
        //    {
        //        case "单程普通政策":
        //            policyType = QunarPolicyType.COMMON;
        //            break;
        //        default:
        //            break;
        //    }
        //    int interval = Convert.ToInt32(this.txtQunarInterval.Text) * 60 * 1000;
        //    this.tmrRealTimeUpload.Interval = Convert.ToInt32(this.txtQunarInterval.Text) * 60 * 1000;
        //    Task.Factory.StartNew(() =>
        //    {
        //        QunarUploadPolicyRequest qunarRequest = new QunarUploadPolicyRequest()
        //        {
        //            FormatFilePath = ConfigurationManager.AppSettings["FormatQunarFilePath"].ToString(),//xml文件路径
        //            FormatZipFilePath = ConfigurationManager.AppSettings["FormatQunarZipFilePath"].ToString(),//压缩包文件路径
        //            IsPrintSql = Convert.ToBoolean(ConfigurationManager.AppSettings["IsPrintSql"].ToString()),
        //            MaxTaskCount = int.Parse(ConfigurationManager.AppSettings["MaxTaskCount"].ToString()),
        //            PerTaskCount = int.Parse(ConfigurationManager.AppSettings["PerTaskCount"].ToString()),
        //            PolicyType = policyType,
        //            SqlWhere = "",
        //            UploadType = UploadType.Incremental,
        //            CommisionMoney = 0,
        //            CommsionPoint = 0,
        //            OperName = "System",
        //            LstQunarCodes = lstQunarCodes,
        //            PageSize = 999999,
        //            IsRealTimeUpload = true,
        //            RealTimeUploadInterval = interval
        //        };
        //        ITask task = new RealTimeToQunarTask();
        //        task.OnWorking += task_OnWorking;
        //        task.StartWork(qunarRequest);

        //    });

        //}
        //#endregion

        //#region 停止实时上传
        //private void btnStopQunarSync_Click(object sender, EventArgs e)
        //{
        //    this.tmrRealTimeUpload.Stop();

        //    this.btnStartQunarSync.Enabled = true;
        //    this.btnStopQunarSync.Enabled = false;
        //}
        //#endregion
        
        #endregion
                   
            
        
    }
}
