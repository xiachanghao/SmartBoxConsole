using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Sor.Attributes;
using Beyondbit.Framework.Biz.Entry;

namespace SmartBox.Console.Common.Entities
{
    public class Statist:BaseEntry
    {
        public int Pad_Android { get; set; }
        public int Pad_iOS { get; set; }
        public int PC_Windows { get; set; }
        public int Phone_Android { get; set; }
        public int Phone_iOS { get; set; }

        public string mail { get; set; }
        public string calendar { get; set; }
        public string magazine { get; set; }
        public string contact { get; set; }
        public string callboard { get; set; }
        public string keywork { get; set; }
        public string sms { get; set; }
        public string dowork { get; set; }
        public string yqzg { get; set; }
        public string instruction { get; set; }
        public string dbd { get; set; }
        public string fourdo { get; set; }

        private Int32 _usageCount = Int32.MinValue;
        private String _unitName = String.Empty;
        private String _userName = String.Empty;
        private Nullable<DateTime> _opTime = null;
        public int UserCount { get; set; }
        public string UserUid { get; set; }




        /// <summary>
        /// 操作时间（按天
        /// </summary>
        public Nullable<DateTime> OpTime
        {
            get { return _opTime; }
            set { _opTime = value; }
        }


        /// <summary>
        /// 单位名称
        /// </summary>
        public String UnitName
        {
            get { return _unitName; }
            set { _unitName = value; }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public String UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        /// <summary>
        /// 访问总计
        /// </summary>
        public Int32 UsageCount
        {
            get { return _usageCount; }
            set { _usageCount = value; }
        }
    }
}
