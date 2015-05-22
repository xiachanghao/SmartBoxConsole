using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using SmartBox.Console.Dao;

namespace SmartBox.Console.Bo
{
    public class Package4AIBO : SmartBox.Console.Bo.BaseBO<Package4AI>
    {
        public Package4AIBO(BaseDao<Package4AI> dao)
            : base(dao)
        {
        }

        public Package4AIBO()
        {
            base._dao = this.Package4AIDao;
        }

        private Package4AIDao _Package4AIDao;
        protected Package4AIDao Package4AIDao
        {
            get
            {
                if (_Package4AIDao == null)
                {
                    _Package4AIDao = new Package4AIDao(AppConfig.mainDbKey);
                }
                return _Package4AIDao;
            }
        }
    }
}
