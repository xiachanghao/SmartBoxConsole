using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;

namespace SmartBox.Console.Dao
{
    public class DeviceConfigDao : BaseDao<DeviceConfig>
    {
        public DeviceConfigDao(string key)
            : base(key)
        { }

        
    }
}
