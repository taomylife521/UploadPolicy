using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDFront.Lib.DtoModel
{
    public class AutoMapperBase : Profile
    {
        public AutoMapperBase()
        {
            Mapper.CreateMap<DateTime, String>().ConvertUsing<DateTimeToStringConverter>();
        }
    }
    /// <summary>
    /// 整数转换成字符串
    /// 1001----->"1001"
    /// </summary>
    public class IntToStringConverter : TypeConverter<int, String>
    {

        protected override String ConvertCore(int source)
        {
            return source.ToString();
        }
    }
    /// <summary>
    /// 日期转换成字符串
    /// 2015-9-24 13:55:06----->"2015-9-24"
    /// </summary>
    public class DateTimeToStringConverter : TypeConverter<DateTime, String>
    {

        protected override String ConvertCore(DateTime source)
        {
            return source.ToString("yyyy-MM-dd");
        }
    }
    /// <summary>
    /// 金额转换成字符串
    /// 100.0000----->"100.0000"
    /// </summary>
    public class DecimalToStringConverter : TypeConverter<decimal, String>
    {
        protected override String ConvertCore(Decimal source)
        {
            return  source.ToString();
        }
    }
}
