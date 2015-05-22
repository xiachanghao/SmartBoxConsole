using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace SmartBox.Console.Common
{
    public class PageView : PageViewBase
    {
        public Orderby _orderby;
        public string OrgCode
        {
            get;
            set;
        }
        public PageView()
        {
        }

        public PageView(NameValueCollection nvc)
        {
            if (!string.IsNullOrEmpty(nvc["page"]))
            {
                PageIndex = Convert.ToInt32(nvc["page"]) - 1;
            }
            else
            {
                PageIndex = 0;
            }
            PageSize = !string.IsNullOrEmpty(nvc["rp"]) ? Convert.ToInt32(nvc["rp"]) : 35;

            if (!string.IsNullOrEmpty(nvc["sortname"]) && nvc["sortname"] != "undefined")
            {
                OrderByType type = nvc["sortorder"] == "desc" ? OrderByType.Desc : OrderByType.Asc;
                OrderBy.Add(nvc["sortname"], type);
            }
            if (!string.IsNullOrEmpty(nvc["qtype"]))
            {
                QuickLuanch = new QuickLuanch
                {
                    FieldName = nvc["qtype"],
                    Operator = (nvc["qop"] == "Eq" ? Operator.Eq : Operator.Like),
                    Value = nvc["query"]
                };
            }
        }



        /// <summary>
        /// 排序
        /// </summary>
        /// <value>The order by.</value>
        /// <Author>wangsm 2008/12/30</Author>
        public Orderby OrderBy
        {
            get
            {
                if (_orderby == null)
                {
                    _orderby = new Orderby();
                }
                return _orderby;
            }
        }

        public QuickLuanch QuickLuanch { get; set; }

    }

    /// <summary>
    /// 快速检索
    /// </summary>
    /// <Author>wangsm 2008/12/30</Author>
    public class QuickLuanch
    {
        public string FieldName { get; set; }

        public Operator Operator { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Value))
            {
                if (Operator == Operator.Eq)
                {
                    return " And " + FieldName + "=N'" + Value + "'";
                }
                if (Operator == Operator.Like)
                {
                    return " And " + FieldName + " Like N'%" + Value + "%'";
                }
            }
            return "";
        }
    }

    public enum Operator
    {
        Eq,
        Like
    }

    /// <summary>
    /// 排序类
    /// </summary>
    /// <Author>wangsm 2008/12/30</Author>
    public class Orderby : Dictionary<string, OrderByType>
    {
        public override string ToString()
        {
            if (Count > 0)
            {
                string temp = "";
                foreach (var kv in this)
                {
                    if (temp != "") temp += ",";
                    temp += kv.Key + " " + kv.Value;
                }
                return " Order by " + temp;
            }
            return "";
        }
    }

    public enum OrderByType
    {
        Asc,
        Desc
    }
}
