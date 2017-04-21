using ND.PolicyReceiveService.Core.ReceivePolicy.helper;
using ND.PolicyReceiveService.InterfaceLib;
using ND.PolicyReceiveService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ND.PolicyReceiveService.Core.ReceivePolicy
{
    public class RecPolicy19e : IRecPolicy
    {
        public event EventHandler<EventMessage> onWorklingMsg;
        RecPolicy19eHelper recHelper =null;

        public Config19e config
        {
            get;
            set;
        }
        public RecPolicy19e(Config19e cf)
        {
           
            recHelper = new RecPolicy19eHelper(cf);
            recHelper.onWorklingMsg += recHelper_onWorklingMsg;
        }

        void recHelper_onWorklingMsg(object sender, EventMessage e)
        {
           if(onWorklingMsg !=null)
           {
               onWorklingMsg(sender,e);
           }
        }
      
        /// <summary>
        /// 同步政策
        /// </summary>
        public void SyncPolicy()
        {
           
                    try
                    {
                      
                        recHelper.SyncForAdd(); //政策同步（添加）
                        recHelper.SyncForDel(); //政策同步（删除）
                    }
                    catch (Exception ex)
                    {
                        var str = ex.Message;
                        while (ex.InnerException != null)
                        {
                            ex = ex.InnerException;
                            str += ex.ToString();   // ex.Message;
                        }
                        recHelper.ShowMsgToForm("同步添加异常:"+str);
                    }

                         
        }

        /// <summary>
        /// 全取政策
        /// </summary>
        public void ReceiveAllPolicy()
        {
            try
            {
               
                recHelper.ReceiveAll(); //政策同步（添加）
            }
            catch (Exception ex)
            {
                recHelper.ShowMsgToForm("全取异常:"+ex.Message);
            }

               
        }




       
    }
}
