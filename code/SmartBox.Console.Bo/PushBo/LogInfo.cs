using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Beyondbit.Push.Service.Bo
{
    /// <summary>
    /// 日志信息
    /// </summary>
    public class LogInfo
    {
        public const string LEVEL_INFO = "info";
        public const string LEVEL_WARN = "warn";
        public const string LEVEL_ERROR = "error";

        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 通道标识
        /// </summary>
        public int ChannelId { get; set; }
        /// <summary>
        /// 用户标识
        /// </summary>
        public string UserUid { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 堆栈信息
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// 记录级别 info/warn/error
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime LogTime { get; set; }
    }
}