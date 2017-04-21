using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyReceiveService.Helper
{
   public class MessageQueueHelper
    {
       #region 把消息发送到消息队列中
       public bool SendMsgToQueue( string queueName, object queueMsg, MessagePriority msgPriority = MessagePriority.Normal,string queueHost = "private$")
       {
           try
           {
               string path = ".\\" + queueHost + "\\" + queueName;
               MessageQueue myQueue = null;

               if (MessageQueue.Exists(path))
               {
                   myQueue = new MessageQueue(path);
               }
               else
               {
                   myQueue = MessageQueue.Create(path, true);
               }
               System.Messaging.Message myMessage = new System.Messaging.Message();
               myMessage.Priority = msgPriority;
               myMessage.Body = queueMsg;
               myMessage.Formatter = new BinaryMessageFormatter();
               //发送消息到队列中
               myQueue.Send(myMessage, MessageQueueTransactionType.Single);
               return true;
           }
           catch (Exception ex)
           {
               return false;
           }
       }
       #endregion
    }
}
