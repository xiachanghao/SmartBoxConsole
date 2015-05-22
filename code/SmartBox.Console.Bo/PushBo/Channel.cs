using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Beyondbit.Push.Service.Bo
{
    /// <summary>
    /// 推送通道
    /// </summary>
    public class Channel
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 应用标识
        /// </summary>
        public string ApplicationId { get; set; }
        /// <summary>
        /// 平台类型
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PlatformType PlatformType { get; set; }
        /// <summary>
        /// 证书
        /// </summary>
        [JsonIgnore]
        public byte[] Cert { get; set; }
        /// <summary>
        /// 证书名称
        /// </summary>
        public string CertName { get; set; }
        /// <summary>
        /// 证书密码
        /// </summary>
        public string CertPassword { get; set; }
        /// <summary>
        /// 0:启用 1:停用 2:删除
        /// </summary>
        public int State { get; set; }
    }
}