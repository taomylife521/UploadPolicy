using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyReceiveService.Helper
{
   public static class DateTimeExtention
    {
       public static string EnsureDateRight(this DateTime dTime)
       {
           int day = Convert.ToDateTime(dTime).Subtract(Convert.ToDateTime("2099-12-30")).Days;
           int day2 = Convert.ToDateTime(dTime).Subtract(Convert.ToDateTime("1900-01-01")).Days;
           if (Math.Abs(day) <= 2 || Math.Abs(day2) <= 2)
           {
               return "";
           }
           return Convert.ToDateTime(dTime).ToString("yyyy-MM-dd");
       }
    }
}
