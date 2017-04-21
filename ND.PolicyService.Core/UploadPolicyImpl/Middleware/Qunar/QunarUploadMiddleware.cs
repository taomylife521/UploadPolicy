using ND.PolicyReceiveService.OutPutAllPolicyZip;
using ND.PolicyService.Enums;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.Qunar;
using ND.PolicyService.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ND.PolicyUploadService.Core.impl.Middleware.Qunar
{
    public class QunarUploadMiddleware : HandlerMiddleware
    {
        public QunarUploadMiddleware(HandlerMiddleware next):base(next)
        {

        }
        public QunarUploadMiddleware()
        {

        }
        public override void Invoke(IHandlerContext context)
        {
            try
            {
                OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "开始上传压缩包,本地压缩包地址:" + context.UploadResponse.FormatPolicyZipFilePath });
                Task.Factory.StartNew(() =>//开始上传
                {
                    try
                    {
                        QunarUploadPolicyRequest qunarRequest = CoreHelper.ChangeToChild<UpLoadPolicyRequest, QunarUploadPolicyRequest>(context.Request);

                        //context.UploadResponse.FormatPolicyZipFilePath // 读取压缩包并上传
                        HttpClient client = new HttpClient();

                        //client.DefaultRequestHeaders.enctype

                        byte[] ct = File.ReadAllBytes(context.UploadResponse.FormatPolicyZipFilePath);////@"D:\ND.Application\File\Qunar\ZipFile\2015\12\2\15\20151202030916.zip"
                        HttpContent con = new ByteArrayContent(ct, 0, ct.Length);//, Encoding.UTF8, "multipart/form-data"
                        con.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data");

                        var res = client.PostAsync(System.Configuration.ConfigurationManager.AppSettings["QunarUpLoadUrl"].ToString(), con).Result;
                        res.EnsureSuccessStatusCode();
                        string backContent = res.Content.ReadAsStringAsync().Result;
                       // OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg ="发送完成,"+ backContent, PurchaserType = PurchaserType.Qunar });
                    }
                    catch(Exception ex)
                    {
                        OnMiddlewareWorking(new EventMsg { Status = RunStatus.Exception, Msg = "QunarUploadMiddleware:去哪儿上传政策失败,"+JsonConvert.SerializeObject(ex), Exception = ex, PurchaserType = PurchaserType.Qunar });
                    }
                });
               
                // qunarRequest.QunarUpLoadUrl
                //OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "上传成功!保存最后更新记录..." });
                //string timeAndId = context.UploadResponse.PolicyRec[context.Request.UploadType].LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "|" + context.UploadResponse.PolicyRec[context.Request.UploadType].LastPolicyId.ToString();
                //string name = context.Request.UploadType == UploadType.FullUpload ? "Qunar\\QunarFullPolicyRecLog" : "Qunar\\QunarIncrementPolicyRecLog";
                //CoreHelper.SaveLastUpTimeAndId(timeAndId, name);
                //OnMiddlewareWorking(new EventMsg { Status = RunStatus.Normal, Msg = "保存最后更新记录成功！执行完毕！" });
                Next.Invoke(context);
            }
            catch(Exception ex)
            {
                OnMiddlewareWorking(new EventMsg { Status = RunStatus.Exception, Msg = "QunarUploadMiddleware:去哪儿上传政策失败", Exception = ex, PurchaserType = PurchaserType.Qunar });
                context.UploadResponse = new UploadPolicyResponse() { ErrCode = ResultType.Failed, ErrMsg = "QunarUploadMiddleware:" + ex.Message, Excption = ex };
                return;
            }
        }
    }
}
