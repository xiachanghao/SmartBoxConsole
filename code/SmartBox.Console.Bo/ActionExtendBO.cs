using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Dao;
using SmartBox.Console.Common;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.Bo
{
    public class ActionExtendBO : SmartBox.Console.Bo.BaseBO<ActionExtend>
    {
        public ActionExtendBO(BaseDao<ActionExtend> dao)
            : base(dao)
        {
        }

        public ActionExtendBO()
        {
            base._dao = this.ActionExtendDao;
        }

        private ActionExtendDao _ActionExtendDao;
        protected ActionExtendDao ActionExtendDao
        {
            get
            {
                if (_ActionExtendDao == null)
                {
                    _ActionExtendDao = new ActionExtendDao(AppConfig.mainDbKey);
                }
                return _ActionExtendDao;
            }
        }
    }
}
