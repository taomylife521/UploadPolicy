//**********************************************************************
//
// 文件名称(File Name)：SeatDiscountListResponse.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/1/5 10:40:40         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/1/5 10:40:40          
//             修改理由：         
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel.SeatDiscount
{
   public class SeatDiscountListResponse
    {
       public SeatDiscountListResponse()
       {
           SeatDiscountList = new List<SeatDiscountDto>();
       }
       public List<SeatDiscountDto> SeatDiscountList { get; set; }
    }
}
