using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.ServiceModel;

namespace Com.HaiLong.Lib.Common
{
    /// <summary>
    /// WCF安全客户端基类 
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public class WcfClient<TService> where TService : class
    {
        string remoteAddress = "";
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="address"></param>
        public WcfClient(string address)
        {
            remoteAddress = address;
        }

        /// <summary>
        /// 调用服务方法
        /// 使用示例：
        /// var client = new WcfClient<IZzkDocumentService>();
        /// var docs = client.UseService(s => s.GetZzkDocuments("0", 10));
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="operation"></param>
        /// <returns></returns>
        public TReturn UseService<TReturn>(Expression<Func<TService, TReturn>> operation) 
        {
            var channelFactory = new ChannelFactory<TService>("*", new EndpointAddress(remoteAddress));
            TService channel = channelFactory.CreateChannel(); 
            var client = (IClientChannel)channel;
            client.Open();
            TReturn result = operation.Compile().Invoke(channel); 
            try 
            { 
                if (client.State != CommunicationState.Faulted)
                { 
                    client.Close(); 
                } 
            } 
            catch 
            { 
                client.Abort(); 
            } 
            return result; 
        }
    }
}
