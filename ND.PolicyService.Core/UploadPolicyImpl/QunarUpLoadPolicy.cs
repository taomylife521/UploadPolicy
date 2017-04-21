using ND.PolicyUploadService.Core.impl.Middleware.Qunar;
using ND.PolicyUploadService.Core.inter;
using ND.PolicyService.Core;
using ND.PolicyService.Core.SplitCore;
using ND.PolicyService.Core.UploadPolicyImpl.Middleware;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.PolicyUploadService.DtoModel;
using ND.PolicyService.Enums;
using ND.PolicyUploadService.DtoModel.Qunar;
using ND.PolicyService.Core.UploadPolicyImpl.Middleware.Qunar;

namespace ND.PolicyUploadService.Core.impl
{
    /// <summary>
    /// 去哪儿上传政策请求
    /// </summary>
    public class QunarUpLoadPolicy : IUploadPolicy
    {
        public event EventHandler<EventMsg> OnWoking;

        #region 执行事件
        public void ShowMsg(object sender,EventMsg msg)
        {
            msg.PurchaserType = PurchaserType.Qunar;
            if (OnWoking != null)
            {
                OnWoking(sender, msg);
            }
        }
        #endregion

        #region 开始上传去哪儿全量政策
        public UploadPolicyResponse UploadFullPolicy(UpLoadPolicyRequest request)
        {
            OnWoking(this, new EventMsg { Status = RunStatus.Normal, PurchaserType= PolicyService.Enums.PurchaserType.Qunar, Msg = "------------------------------Start------------------------------------" }); 
            ShowMsg(this, new EventMsg() { Status = RunStatus.Normal, PurchaserType = PurchaserType.Qunar, Msg = DateTime.Now+":开始上传政策" });
            IHandlerBuilder builder = new HandlerBuilder();
            builder.Use<QunarLoadFullPolicyMiddleware>()//载入全量政策中间件
                   .Use<QunarFilterSplitMiddleware>()//过滤并拆分中间件
                   .Use<QunarFullDispatcherMiddleware>()//全量分发政策中间件
                   .Use<QunarFormatMiddleware>()//先格式化成去哪儿格式并保存xml文件
                   .Use<PackageZipFileMiddleware>()//压缩成zip文件
                   .Use<QunarUploadMiddleware>();//上传到去哪儿服务器
            IHandlerContext context = new HandlerContext(request);
            IHandler handler = new DefaultHandler(builder, ShowMsg);
            handler.Execute(context);
            OnWoking(this, new EventMsg { Status = RunStatus.Normal, PurchaserType = PolicyService.Enums.PurchaserType.Qunar, Msg = "-------------------------------End---------------------------------------" });
          
            return context.UploadResponse;
        }

       
        
        #endregion

        #region 上传去哪儿增量政策
        public UploadPolicyResponse UpLoadIncrementPolicy(UpLoadPolicyRequest request,bool isTaskPolicy=true)
        {
            OnWoking(this, new EventMsg { Status = RunStatus.Normal, PurchaserType = PolicyService.Enums.PurchaserType.Qunar, Msg = "------------------------------Start------------------------------------" });
            QunarUploadPolicyRequest qunarIncrementRequest = CoreHelper.ChangeToChild<UpLoadPolicyRequest, QunarUploadPolicyRequest>(request);
            IHandlerBuilder builder = new HandlerBuilder();
            if(isTaskPolicy ==true)
            {
                builder.Use<QunarLoadIncrementalPolicyMiddleware>()//载入增量政策
                       .Use<QunarFilterSplitMiddleware>()//过滤并拆分中间件
                      .Use<QunarIncrementalDispatcherMiddleware>()//增量分发政策中间件
                      .Use<QunarFormatMiddleware>()//先格式化成去哪儿格式并保存xml文件
                      .Use<PackageZipFileMiddleware>()//压缩成zip文件
                      .Use<QunarUploadMiddleware>();//上传到去哪儿服务器
            }
            else//有现成的数据
            {
                builder.Use<QunarFilterRepeatUploadMiddleware>()//过滤不在上传列表的政策中间件
                        .Use<QunarFilterSplitMiddleware>()//过滤并拆分中间件
                        .Use<QunarIncrementalDispatcherMiddleware>()//增量分发政策中间件
                        .Use<QunarFormatMiddleware>()//先格式化成去哪儿格式并保存xml文件
                        .Use<PackageZipFileMiddleware>()//压缩成zip文件
                        .Use<QunarUploadMiddleware>();//上传到去哪儿服务器
            }
          
                 
            IHandlerContext context = new HandlerContext(request);
            IHandler handler = new DefaultHandler(builder,ShowMsg);
            handler.Execute(context);
            OnWoking(this, new EventMsg { Status = RunStatus.Normal, PurchaserType = PolicyService.Enums.PurchaserType.Qunar, Msg = "-------------------------------End---------------------------------------" });
            return context.UploadResponse;
        } 
        #endregion


      
    }
}
