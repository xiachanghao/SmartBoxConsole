using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.MVC;

namespace SmartBox.Console.Common
{
    public class User : IUser
    {
        #region IUser 成员

        public Dictionary<string, string> ExtendProperties
        {
            get;
            set;
        }

        public string FullName
        {
            get;
            set;
        }

        public string OrgCode
        {
            get;
            set;
        }

        public string OrgName
        {
            get;
            set;
        }

        public string UnitCode
        {
            get;
            set;
        }

        public string UnitName
        {
            get;
            set;
        }

        public string UserUId
        {
            get;
            set;
        }

        #endregion

        #region ISerializable 成员

        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
