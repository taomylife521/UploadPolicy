using ND.PolicyService.CoreLib;
using ND.PolicyService.DbEntity;
using ND.PolicyUploadService.DtoModel.SeatDiscount;
//**********************************************************************
//
// 文件名称(File Name)：DefaultSeatDiscount.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/1/5 10:43:30         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/1/5 10:43:30          
//             修改理由：         
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.PolicyCore.impl
{
    public class DefaultSeatDiscount : ISeatDiscount
    {
        public SeatDiscountListResponse SeatDiscountList()
        {
            SeatDiscountListResponse list = new SeatDiscountListResponse();
            SeatDiscountLib seatLib = new SeatDiscountLib();
            List<SeatDiscount> lstSeat =seatLib.GetModelList();
            lstSeat.ForEach(x =>
            {
                list.SeatDiscountList.Add(new SeatDiscountDto()
                {
                    AirlineCode=x.AirlineCode,
                    CreateTime=x.CreateTime,
                    Discount =x.Discount,
                    id=x.id,
                    IsEnabled=x.IsEnabled,
                    Seat=x.Seat
                });
            });
            return list;
            
        }
    }
}
