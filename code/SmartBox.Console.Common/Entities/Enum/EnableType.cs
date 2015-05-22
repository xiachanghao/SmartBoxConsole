using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common.Entities
{
    public class EnableType
    {
        #region Init
        private EnableType() { }
        private EnableType(string value)
        {
            m_value = value;
        }
        private string m_value; 
        #endregion

        #region Member
        public static readonly EnableType Need = new EnableType("Need");
        public static readonly EnableType DefaultTrue = new EnableType("DefaultTrue");
        public static readonly EnableType DefaultFalse = new EnableType("DefaultFalse");
        #endregion
        
        #region Operator
        public override string ToString()
        {
            return m_value;
        }

        public override bool Equals(object obj)
        {
            return m_value.Equals(obj);
        }

        public bool Equals(string str)
        {
            return m_value.Equals(str);
        }

        public bool Equals(EnableType type)
        {
            return m_value.Equals(type.m_value);
        }

        public static bool operator ==(EnableType type, string str)
        {
            return type.m_value == str;
        }

        public static bool operator ==(EnableType type1, EnableType type2)
        {
            return type1.m_value == type2.m_value;
        }

        public static bool operator !=(EnableType type, string str)
        {
            return type.m_value != str;
        }

        public static bool operator !=(EnableType type1, EnableType type2)
        {
            return type1.m_value != type2.m_value;
        }

        public static implicit operator string(EnableType value)
        {
            return ConvertToString(value);
        }

        public static implicit operator EnableType(string value)
        {
            return ConvertToEnableType(value);
        }

        /// <summary>
        /// 将String类型转换为EnableType类型
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="InvalidCastException">
        /// 当用户将Need,DefaultTrue,DefaultFalse以外的值转换为EnableType类型时引发的异常
        /// </exception>
        /// <returns></returns>
        private static EnableType ConvertToEnableType(string value)
        {
            switch (value.ToLower())
            {
                case "need":
                    return Need;
                case "defaulttrue":
                    return DefaultTrue;
                case "defaultfalse":
                    return DefaultFalse;
                default:
                    throw new InvalidCastException();
            }
        }
        /// <summary>
        /// 将EnableType类型转换为String类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ConvertToString(EnableType value)
        {
            return value.m_value;
        }

        #endregion
    }
}
