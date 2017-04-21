﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using ND.FlightKGService.TaskPlatformCore.w_51book_getModifyAndRefundStipulates;
using Newtonsoft.Json;



/// <summary>
///_51bookHelper 接口访问帮助类
/// </summary>
public class _51bookHelper
{
    public _51bookHelper()
    {
    }
    //创建日记对象

    //公司代码
    private readonly static string agencyCode = "NDLXS";
    //安全码
    private readonly static string safetyCode = "H&*WUgd2";  //正式账号

    

    

    #region 全取退改签规定 getModifyAndRefundStipulates(GetModifyAndRefundStipulatesRequest  model)
    //根据航空公司、舱位获取退改签规定
    public static object getModifyAndRefundStipulates(getModifyAndRefundStipulatesRequest model)
    {
        model.agencyCode = agencyCode;
        model.sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((agencyCode + model.lastSeatId + model.rowPerPage + safetyCode), "MD5").ToLower();
        try
        {
            getModifyAndRefundStipulatesReply result = new ND.FlightKGService.TaskPlatformCore.w_51book_getModifyAndRefundStipulates.GetModifyAndRefundStipulatesServiceImpl_1_0Service().getModifyAndRefundStipulates(model);
            return result;
          
        }
        catch (Exception e)
        {
            return e.Message+"："+JsonConvert.SerializeObject(e);
        }
    }
    #endregion

   
}
