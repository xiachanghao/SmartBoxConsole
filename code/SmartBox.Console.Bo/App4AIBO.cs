using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Dao;
using SmartBox.Console.Common;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Bo
{
    public class App4AIBO : SmartBox.Console.Bo.BaseBO<App4AI>
    {
        public App4AIBO(BaseDao<App4AI> dao)
            : base(dao)
        {
        }

        public App4AIBO()
        {
            base._dao = this.App4AIDao;
        }

        private App4AIDao _App4AIDao;
        protected App4AIDao App4AIDao
        {
            get
            {
                if (_App4AIDao == null)
                {
                    _App4AIDao = new App4AIDao(AppConfig.mainDbKey);
                }
                return _App4AIDao;
            }
        }
    }
}
