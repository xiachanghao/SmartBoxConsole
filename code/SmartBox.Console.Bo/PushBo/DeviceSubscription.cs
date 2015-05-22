using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Beyondbit.Push.Service.Bo
{
    /// <summary>
    /// 设备推送
    /// </summary>
    public class DeviceSubscription
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 通道标识
        /// </summary>
        public int ChannelId { get; set; }
        /// <summary>
        /// 设备推送标识
        /// </summary>
        public string SubscriptionId { get; set; }
        /// <summary>
        /// 用户标识
        /// </summary>
        public string UserUid { get; set; }
        /// <summary>
        /// 推送通道
        /// </summary>
        public Channel Channel { get; set; }

    }
}