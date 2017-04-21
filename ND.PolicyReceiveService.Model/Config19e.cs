using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyReceiveService.Model
{
   public class Config19e:GlobalConfig
    {
        public string SafeCode19e { get; set; }
        public string Username19e { get; set; }
        public string AppCode19e { get; set; }
        public string TimeSpan19e { get; set; }
        public string PerPageSize { get; set; }
        public bool IsCloseSync { get; set; }

        public string QueueName { get; set; }
        public string QueueHost { get; set; }
        public bool IsSendPolicyQueue { get; set; }
    }
}
