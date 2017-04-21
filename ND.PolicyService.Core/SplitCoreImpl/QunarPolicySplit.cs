using ND.PolicyReceiveService.DbEntity;
using ND.PolicyReceiveService.Helper;
using ND.PolicyService.Enums;
using ND.PolicySplitService.Core.impl.splitCoreImpl;
using ND.PolicySplitService.Core.impl.splitCoreImpl.qunar;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyUploadService.DtoModel.SplitPolicy;
using ND.PolicyService.Core.SplitCore;
using ND.PolicyService.Core.SplitCoreImpl.Middleware.Qunar;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ND.PolicyService.Core.SplitCoreImpl
{
    /// <summary>
    /// 去哪儿政策拆分
    /// </summary>
    public class QunarPolicySplit:IPolicySplit
    {
        public event EventHandler<EventMsg> OnWoking;
       private readonly ConcurrentBag<Policies> concurrentPolicies = new ConcurrentBag<Policies>();
    

        public SplitPolicyResponse PolicySplit(SplitPolicyRequest request)
        {
            try
            {
                QunarSplitPolicyRequest qunarReq = request as QunarSplitPolicyRequest;
                if (qunarReq == null)
                {
                    throw new ArgumentNullException("QunarSplitPolicyRequest is null");
                }
               
               
                //QunarSplitPolicyRequest request = context.Get<QunarSplitPolicyRequest>("QunarSplitPolicyRequest");
                List<Policies> lstPolicies = request.Policies;//这要分多线程处理掉的数据，大概有200条，但是每一条有可能都会拆分成上万条
          
                ConcurrentBag<Task> tasks = new ConcurrentBag<Task>();
                #region 多线程分离
             var mangerTask= Task.Factory.StartNew(() =>//开启管理线程分配任务
                         {
                             int currentTaskCount = 0;
                             while (lstPolicies.Count > 0)
                             {
                                 while (currentTaskCount >= request.SplitMaxTaskCount)
                                 {
                                     int index = Task.WaitAny(tasks.ToArray());//等待任何一个线程完成
                                     currentTaskCount--;
                                 }
                                 List<Policies> childCollection = lstPolicies.Take(request.SplitPerTaskMaxCount).ToList();
                                 ISplitHandlerBuilder builder = new SplitHandlerBuilder();
                                 builder
                                     // .Use<QunarDivideTaskMiddleware>()//根据处理政策的数量来分配任务
                                               .Use<QunarDptCityMiddleware>()//拆分出发城市处理中间件
                                               .Use<QunarArrCityMiddleware>()//拆分抵达城市处理中间件
                                               .Use<QunarFlightInMiddleware>()//拆分包含航班处理中间件
                                               .Use<QunarFlightOutMiddleware>()//拆分不包含航班处理中间件
                                               .Use<QunarSeatMiddleware>()//去哪儿舱位处理中间件
                                              // .Use<QunarSaleDateMiddleware>()//拆分销售有效期处理中间件
                                              // .Use<QunarFlightDateMiddleware>()//拆分航班起飞有效期处理中间件
                                               .Use<GenerateResponseMiddleware>();//填充政策列表响应
                                 Dictionary<string, object> dic = new Dictionary<string, object>();
                                 dic.Add("QunarSplitPolicyRequest", qunarReq);
                                 var context = new SplitHandlerContext(dic);
                                 ISplitHandler handler = new SplitHandler(builder);
                                 if (childCollection.Count() > 0)
                                 {
                                     lstPolicies.RemoveRange(0, childCollection.Count());
                                     Task subTask = Task.Factory.StartNew(() =>//开启子线程处理
                                     {
                                         foreach (Policies item in childCollection)
                                         {
                                            // Split(item, qunarReq);

                                             context.RequestPolicy = item;
                                             context.ResponsePolicy.Clear();
                                             handler.Execute(context);
                                             context.ResponsePolicy.ForEach(x =>
                                             {
                                                 concurrentPolicies.Add(x);
                                             });
                                            
                                         }
                                         
                                     });
                                     currentTaskCount++;
                                     tasks.Add(subTask);
                                 }
                             }
                         });
             Task.WaitAll(mangerTask);
             Task.WaitAll(tasks.ToArray());
                       
                #endregion
           // Task.WaitAll(mangerTask);
              
                return new SplitPolicyResponse { ErrCode = ResultType.Sucess, ErrMsg = "", PoliciesData = concurrentPolicies.ToList() };
            }
            catch(Exception ex)
            {
                return new SplitPolicyResponse { ErrCode = ResultType.Failed, ErrMsg = "拆分去哪儿政策失败！", Excption = ex };
            }
        }

        public void Split(Policies policies,QunarSplitPolicyRequest qunarReq)
        {
            ISplitHandlerBuilder builder = new SplitHandlerBuilder();
            builder
              // .Use<QunarDivideTaskMiddleware>()//根据处理政策的数量来分配任务
               .Use<QunarDptCityMiddleware>()//拆分出发城市处理中间件
               .Use<QunarArrCityMiddleware>()//拆分抵达城市处理中间件
               .Use<QunarFlightInMiddleware>()//拆分包含航班处理中间件
               .Use<QunarFlightOutMiddleware>()//拆分不包含航班处理中间件
                .Use<QunarSeatMiddleware>()//去哪儿舱位处理中间件
                
             //  .Use<QunarSaleDateMiddleware>()//拆分销售有效期处理中间件
              // .Use<QunarFlightDateMiddleware>()//拆分航班起飞有效期处理中间件
               .Use<GenerateResponseMiddleware>();//填充政策列表响应
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("QunarSplitPolicyRequest", qunarReq);
            var context = new SplitHandlerContext(dic);
            context.RequestPolicy = policies;
            ISplitHandler handler = new SplitHandler(builder);
            handler.Execute(context);
            context.ResponsePolicy.ForEach(x =>
            {
                concurrentPolicies.Add(x);
            });
            builder = null;
            handler = null;
            context.ResponsePolicy.Clear();
            context = null;
            dic.Clear();
           
        }
    }
}
