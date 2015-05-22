using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Dao;
using SmartBox.Console.Common;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Bo
{
    public class AppPrivilegeBO : SmartBox.Console.Bo.BaseBO<AppPrivilege>
    {
        public AppPrivilegeBO(BaseDao<AppPrivilege> dao)
            : base(dao)
        {
        }

        public AppPrivilegeBO()
        {
            base._dao = this.AppPrivilegeDao;
        }

        private AppPrivilegeDao _AppPrivilegeDao;
        protected AppPrivilegeDao AppPrivilegeDao
        {
            get
            {
                if (_AppPrivilegeDao == null)
                {
                    _AppPrivilegeDao = new AppPrivilegeDao(AppConfig.mainDbKey);
                }
                return _AppPrivilegeDao;
            }
        }
    }
}
