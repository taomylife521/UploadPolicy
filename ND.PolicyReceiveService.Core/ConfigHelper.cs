using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyReceiveService.Core
{
   public class ConfigHelper
    {
        public static readonly string SafeCode19e = "[66USljYj[3S#lkk3T#930Yj#93*j93&";  //ConfigurationManager.AppSettings["safeCode_19e"];
        public static readonly string Username19e= ConfigurationManager.AppSettings["username_19e"];
        public static readonly string AppCode19e= ConfigurationManager.AppSettings["appCode_19e"];
        public static readonly string TimeSpan19e = ConfigurationManager.AppSettings["timespan_19e"];
        public static readonly string PerPageSize = ConfigurationManager.AppSettings["PerPageSize19e"];
        public static readonly bool IsCloseSync = Convert.ToBoolean(ConfigurationManager.AppSettings["IsCloseSync19e"]);
    }
}
