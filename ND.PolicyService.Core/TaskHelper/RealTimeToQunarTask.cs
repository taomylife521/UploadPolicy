using ND.PolicyUploadService.Core.impl;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.Qunar;
using ND.PolicyService.Core.SplitCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.TaskHelper
{
  public class RealTimeToQunarTask:ITask
    {
      public event EventHandler<EventMsg> OnWorking;
      CancellationTokenSource ct = new CancellationTokenSource();
      public void OnWork(EventMsg e)
      {
          if(OnWorking != null)
          {
              OnWorking(this, e);
          }
      }
      /// <summary>
      /// 开始任务
      /// </summary>
      /// <param name="timeSpan"></param>
      /// <returns></returns>
        public EmptyResponse StartWork(object request)
        {
          QunarUploadPolicyRequest qunarRequest= CoreHelper.ChangeToChild<object, QunarUploadPolicyRequest>(request);
           var task= Task<UploadPolicyResponse>.Factory.StartNew(() => {
                IUploadPolicy upload = new QunarUpLoadPolicy();
                upload.OnWoking += upload_OnWoking;
               return upload.UpLoadIncrementPolicy(qunarRequest);
            },ct.Token);
            Task.WaitAll(task);
            if(task.Result.ErrCode == PolicyService.Enums.ResultType.Failed)
            {
                return new EmptyResponse { ErrCode = PolicyService.Enums.ResultType.Failed, ErrMsg = task.Result.ErrMsg, Excption = task.Result.Excption };
            }
            return new EmptyResponse { ErrCode = PolicyService.Enums.ResultType.Sucess, ErrMsg = "" };
            
        }

        void upload_OnWoking(object sender, EventMsg e)
        {
            OnWork(e);
        }

      /// <summary>
      /// 取消任务
      /// </summary>
      /// <returns></returns>
        public bool CancelWork()
        {
            try
            {
                ct.Cancel();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }




       
    }
}
