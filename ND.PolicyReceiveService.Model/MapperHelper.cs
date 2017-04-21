using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDFront.Lib.DtoModel.autoMapper
{
    /// <summary>
    /// automapper映射关系辅助类
    /// </summary>
    public static class MapperHelper
    {
        #region 配置映射规则
        /// <summary>  
        /// 确保映射配置只注册一次  
        /// </summary>  
        static MapperHelper()
        {  
        }
        #endregion


        #region 实体映射扩展方法
        /// <summary>  
        /// 将 IDataReader 转为实体对象  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="dr"></param>  
        /// <returns></returns>  
        public static T GetEntity<T>(this IDataReader dr)
        {
            return Mapper.Map<T>(dr);
        }
        /// <summary>  
        /// 将 DataSet 转为实体对象  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="ds"></param>  
        /// <returns></returns>  
        public static T GetEntity<T>(this DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return default(T);
            var dr = ds.Tables[0].CreateDataReader();
            return Mapper.Map<T>(dr);
        }
        /// <summary>  
        /// 将 DataTable 转为实体对象  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        public static T GetEntity<T>(this DataTable dt)
        {
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                    return default(T);
                var dr = dt.CreateDataReader();
                return Mapper.Map<T>(dr);
            }
            catch(Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>  
        /// 将 DataTable 转为实体对象  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        public static TDestination GetEntity<TSource,TDestination>(this TSource Source)
        {
            return Mapper.DynamicMap<TSource, TDestination>(Source);
        }

        public static TDestination GetEntityList<TSource,TDestination>(this TSource Source)
        {
            return Mapper.Map<TDestination>(Source);
        }
        #endregion  
    }
}
