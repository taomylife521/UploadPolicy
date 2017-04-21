using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ND.PolicyUploadService.WinformClient
{
    public class ClientFactory<TService>
    {
        #region 成员属性

        private static Dictionary<string, string> _configs = null;
        /// <summary>
        /// 返回应用程序AppSettings配置项
        /// </summary>
        public static Dictionary<string, string> Configs
        {
            get
            {
                if (_configs == null || _configs.Count <= 0)
                {
                    _configs = new Dictionary<string, string>();
                    foreach (var item in System.Configuration.ConfigurationManager.AppSettings.AllKeys)
                    {
                        _configs.Add(item.ToLower(), System.Configuration.ConfigurationManager.AppSettings[item]);
                    }
                }
                return _configs;
            }
        }

        private static BasicHttpBinding _basicHttpBinding = null;
        /// <summary>
        /// 返回一个BasicHttpBinding
        /// </summary>
        public static BasicHttpBinding BasicHttpBinding
        {
            get
            {
                if (_basicHttpBinding == null)
                {
                    _basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.None);
                    _basicHttpBinding.MaxBufferPoolSize = int.MaxValue;
                    _basicHttpBinding.MaxBufferSize = int.MaxValue;
                    _basicHttpBinding.MaxReceivedMessageSize = int.MaxValue;
                    _basicHttpBinding.CloseTimeout = TimeSpan.FromMinutes(10);
                    _basicHttpBinding.OpenTimeout = _basicHttpBinding.CloseTimeout;
                    _basicHttpBinding.ReceiveTimeout = _basicHttpBinding.CloseTimeout;
                    _basicHttpBinding.SendTimeout = _basicHttpBinding.CloseTimeout;
                    //_basicHttpBinding.ReaderQuotas.MaxArrayLength = int.MaxValue;
                    //_basicHttpBinding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
                    //_basicHttpBinding.ReaderQuotas.MaxDepth = int.MaxValue;
                    //_basicHttpBinding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
                    //_basicHttpBinding.ReaderQuotas.MaxStringContentLength = int.MaxValue;


                }
                return _basicHttpBinding;
            }
        }

        private static WSHttpBinding _wsHttpBinding = null;
        /// <summary>
        /// 返回一个WsHttpBinding
        /// </summary>
        public static WSHttpBinding WsHttpBinding
        {
            get
            {
                if (_wsHttpBinding == null)
                {
                    _wsHttpBinding = new WSHttpBinding(SecurityMode.None);
                    _wsHttpBinding.MaxBufferPoolSize = int.MaxValue;
                    _wsHttpBinding.MaxReceivedMessageSize = int.MaxValue;
                    _wsHttpBinding.TransactionFlow = true;
                    _wsHttpBinding.CloseTimeout = TimeSpan.FromMinutes(10);
                    _wsHttpBinding.OpenTimeout = _wsHttpBinding.CloseTimeout;
                    _wsHttpBinding.ReceiveTimeout = _wsHttpBinding.CloseTimeout;
                    _wsHttpBinding.SendTimeout = _wsHttpBinding.CloseTimeout;
                    //_wsHttpBinding.ReaderQuotas.MaxArrayLength = int.MaxValue;
                    //_wsHttpBinding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
                    //_wsHttpBinding.ReaderQuotas.MaxDepth = int.MaxValue;
                    //_wsHttpBinding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
                    //_wsHttpBinding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
                }
                return _wsHttpBinding;
            }
        }

        private static Dictionary<string, EndpointAddress> _endpointAddresses = new Dictionary<string, EndpointAddress>();
        /// <summary>
        /// 终结点列表
        /// </summary>
        public static Dictionary<string, EndpointAddress> EndpointAddresses
        {
            get
            {
                return _endpointAddresses;
            }
        }

        private static Dictionary<string, ChannelFactory<TService>> _factoryList = new Dictionary<string, ChannelFactory<TService>>();

        /// <summary>
        /// 通道工厂列表
        /// </summary>
        private static Dictionary<string, ChannelFactory<TService>> FactoryList
        {
            get { return ClientFactory<TService>._factoryList; }
        }

        #endregion

        /// <summary>
        /// 创建和打开服务代理
        /// </summary>
        /// <returns></returns>
        public static TService CreateService()
        {
            Type t = typeof(TService);
            string bindingKey = "binding:" + t.Name.ToLower();
            Binding binding = GetBinding(bindingKey);
            string urlKey = "url:" + t.Name.ToLower();
            try
            {
               
                // new ContractDescriptionColletion
               
                if (!Configs.ContainsKey(urlKey))
                {
                    throw new KeyNotFoundException(string.Format("键URL:{0}未找到(键不区分大小写)。", t.Name));
                }
                if (!_factoryList.ContainsKey(urlKey))
                {
                    // ServiceEndpoint endPoint = new ServiceEndpoint(null, binding, GetEndpointAddress(urlKey));
                    // new ContractDescription{ Operations = new OperationDescriptionCollection()}

                    ChannelFactory<TService> factory = new ChannelFactory<TService>(binding, GetEndpointAddress(urlKey));
                    foreach (System.ServiceModel.Description.OperationDescription op in factory.Endpoint.Contract.Operations)
                    {

                        System.ServiceModel.Description.DataContractSerializerOperationBehavior dataContractBehavior =
                                    op.Behaviors.Find<System.ServiceModel.Description.DataContractSerializerOperationBehavior>()
                                    as System.ServiceModel.Description.DataContractSerializerOperationBehavior;
                        if (dataContractBehavior != null)
                        {
                            dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                        }
                    }
                    try
                    {
                        _factoryList.Add(urlKey, factory);
                    }
                    catch { }
                }
               
            }
            catch(Exception ex)
            {
                //LogHelper.LogWriter(ex);
                
            }
            return _factoryList[urlKey].CreateChannel();
        }

        /// <summary>
        /// 获取绑定
        /// </summary>
        /// <param name="t">类型</param>
        /// <param name="binding">绑定</param>
        /// <returns></returns>
        private static Binding GetBinding(string bindingKey)
        {
            Binding binding = null;
            if (!Configs.ContainsKey(bindingKey) || Configs[bindingKey].ToLower() != "wshttpbinding")
            {
                binding = BasicHttpBinding;
            }
            else
            {
                binding = WsHttpBinding;
            }
            return binding;
        }

        /// <summary>
        /// 根据key获取终结点
        /// </summary>
        /// <param name="urlKey"></param>
        /// <returns></returns>
        private static EndpointAddress GetEndpointAddress(string urlKey)
        {
            if (!EndpointAddresses.ContainsKey(urlKey))
            {
                try
                {
                    EndpointAddresses.Add(urlKey, new EndpointAddress(Configs[urlKey]));
                }
                catch { }
            }
            return EndpointAddresses[urlKey];
        }
    }
}
