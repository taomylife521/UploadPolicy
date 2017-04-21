using ND.PolicyService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyUploadService.DtoModel
{
    [DataContract]
    public class ResponseBase
    {
        [DataMember]
        public ResultType ErrCode { get; set; }

        [DataMember]
        public string ErrMsg { get; set; }


        [DataMember]
        public Exception Excption { get; set; }
    }

    //public class ResponseBase<T>
    //{
    //    public ResultType ErrCode { get; set; }
    //    public string ErrMsg { get; set; }

    //    public Exception Excption { get; set; }

    //    public T ResponseData { get; set; }
    //}

    //public class ResponseBase : ResponseBase<EmptyResponse>
    //{

    //}
}
