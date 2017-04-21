//**********************************************************************
//
// 文件名称(File Name)：SeatDiscountDto.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/1/5 10:41:36         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/1/5 10:41:36          
//             修改理由：         
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel.SeatDiscount
{
   public class SeatDiscountDto
    {
        #region Model
        private int _id;
        private string _airlinecode = "";
        private string _seat = "";
        private string _discount = "";
        private int _isenabled = 1;
        private DateTime _createtime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AirlineCode
        {
            set { _airlinecode = value; }
            get { return _airlinecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Seat
        {
            set { _seat = value; }
            get { return _seat; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsEnabled
        {
            set { _isenabled = value; }
            get { return _isenabled; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model
    }
}
