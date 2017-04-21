using ND.PolicyReceiveService.DbEntity;
using ND.PolicyService.Enums.Upload.Qunar;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.CompleteUploadPolicy;
using ND.PolicyUploadService.DtoModel.Qunar;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.UploadPolicyImpl.Middleware.Qunar
{
    /// <summary>
    /// 过滤在去哪儿没有上传的政策
    /// </summary>
    public class QunarFilterRepeatUploadMiddleware : HandlerMiddleware
    {
          /// <summary>
        /// 初始化一个新的处理中间件。
        /// </summary>
        /// <param name="next">下一个处理中间件。</param>
        public QunarFilterRepeatUploadMiddleware(HandlerMiddleware next)
            : base(next)
        {
        }

        public QunarFilterRepeatUploadMiddleware()
        {
        }


        public override void Invoke(IHandlerContext context)
        {
            try
            {
                QunarUploadPolicyRequest qunarIncrementRequest = CoreHelper.ChangeToChild<UpLoadPolicyRequest, QunarUploadPolicyRequest>(context.Request);
                //获取所有在去哪儿上传过的政策
                string responseContent = CoreHelper.DoPost(System.Configuration.ConfigurationManager.AppSettings["SearchCompleteUploadPolicyUrl"].ToString(), null);
                if (string.IsNullOrEmpty(responseContent))
                {
                    OnMiddlewareWorking(new EventMsg() { PurchaserType= Enums.PurchaserType.Qunar, Status = Enums.RunStatus.Normal, Msg = "暂未上传过任何政策,不用更新!" });
                    context.UploadResponse = new UploadPolicyResponse() { ErrCode = Enums.ResultType.Sucess };
                    return;
                }
                CompleteUploadPolicyResponse rep = JsonConvert.DeserializeObject<CompleteUploadPolicyResponse>(responseContent);
                if (rep.ErrCode == Enums.ResultType.Failed || rep.CompleteUploadPolicyCollection == null || rep.CompleteUploadPolicyCollection.Count <= 0)
                {
                    OnMiddlewareWorking(new EventMsg() { Status = Enums.RunStatus.Normal, Msg = "暂未上传过任何政策,不用更新!" });
                    context.UploadResponse = new UploadPolicyResponse() { ErrCode = Enums.ResultType.Sucess };
                    return;
                }
               Dictionary<QunarPolicyType,List<Policies>> dic= CoreHelper.ReserveHaveUploadPolicy(qunarIncrementRequest.PolicyDataOrgin, rep.CompleteUploadPolicyCollection);
               if (dic.Count <= 0)
                {
                    OnMiddlewareWorking(new EventMsg() { Status = Enums.RunStatus.Normal, Msg = "此次更新包不在上传政策列表中，不用更新!" });
                    context.UploadResponse = new UploadPolicyResponse() { ErrCode = Enums.ResultType.Sucess };
                    return;
                }
               foreach (KeyValuePair<QunarPolicyType,List<Policies>> item in dic)//分批上传不同类型的政策
               {
                   qunarIncrementRequest.PolicyType = item.Key;
                   qunarIncrementRequest.PolicyDataOrgin = item.Value;
                   context.SetRequest(qunarIncrementRequest);
                   Next.Invoke(context);
               }
                
             
            
            }
            catch(Exception ex)
            {
                OnMiddlewareWorking(new EventMsg() { Status = Enums.RunStatus.Exception, Msg = "QunarFilterRepeatUploadMiddleware:"+ex.Message,Exception= ex, PurchaserType= Enums.PurchaserType.Qunar});
                context.UploadResponse = new UploadPolicyResponse() { ErrCode = Enums.ResultType.Failed, ErrMsg = ex.Message, Excption = ex };
                return;
            }


        }
    }
}
