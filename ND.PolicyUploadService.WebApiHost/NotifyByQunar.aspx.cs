using ND.PolicyReceiveService.OutPutAllPolicyZip;
using ND.PolicyService.Core;
using ND.PolicyService.Core.PolicyCore;
using ND.PolicyService.Core.PolicyCore.impl;
using ND.PolicyService.Enums;
using ND.PolicyUploadService.Core.impl;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyUploadService.DtoModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ND.PolicyUploadService.WebApiHost
{
    public partial class NotifyByQunar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //{"ext":"\"11\"","msg":"","result":"Exec successful,0/1 is error!ReplicateCount is1"}

                
                SortedDictionary<string, string> pams = GetRequestPost(Request.RequestContext.HttpContext.Request);
               // CoreHelper.CreateFile(System.Configuration.ConfigurationManager.AppSettings["NotifyErrLogPath"] + "\\Qunar\\" + System.Guid.NewGuid() + ".txt", JsonConvert.SerializeObject(pams));//创建文件   JsonConvert.SerializeObject(pams);
                string result = pams["result"] == null ? "" : pams["result"].ToString();
                SuccessStatus notifyResult =  SuccessStatus.Other;
                bool isSucess = true;
                if (result.IndexOf("successful") > -1)
                {
                    isSucess = true;
                    notifyResult =  SuccessStatus.Success;
                }
                else
                {
                    isSucess = false;
                    notifyResult = SuccessStatus.Failed;
                    CoreHelper.CreateFile(System.Configuration.ConfigurationManager.AppSettings["NotifyErrLogPath"] + "\\Qunar\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", JsonConvert.SerializeObject(pams));//创建文件

                }
                    
                UpdateNotifyRequest request = new UpdateNotifyRequest()
                {
                    UpdateStatusId =pams["ext"] == null || string.IsNullOrEmpty(pams["ext"])? "0" : pams["ext"].ToString().Replace("\\", ""),
                    ResponseParams = JsonConvert.SerializeObject(pams),
                    NotifyResult = notifyResult,
                    IsSucess=isSucess
                };
                
                IPolicyNotify notify = new DefaultPolicyNotify();
                EmptyResponse res = notify.UpdateNotify(request);
                if (res.ErrCode == PolicyService.Enums.ResultType.Failed)
                {
                    string log = DateTime.Now + ":回调内容:" + JsonConvert.SerializeObject(pams) + ",更新回调结果:" + JsonConvert.SerializeObject(res);
                    CoreHelper.CreateFile(System.Configuration.ConfigurationManager.AppSettings["NotifyErrLogPath"] + "\\Qunar\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", log);//创建文件
                    Response.Write("F");
                    //Response.End();
                }
                else
                Response.Write("S");
                //Response.End();
            }
            catch(Exception ex)
            {
                CoreHelper.CreateFile(System.Configuration.ConfigurationManager.AppSettings["NotifyErrLogPath"] + "\\Qunar\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", JsonConvert.SerializeObject(ex));//创建文件
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        private SortedDictionary<string, string> GetRequestPost(HttpRequestBase request)
        {
            int i = 0;
            SortedDictionary<string, string> sPara = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sPara.Add(requestItem[i], request.Form[requestItem[i]]);
            }

            return sPara;
        }
    }
}