using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using SmartBox.Console.Common;
using SmartBox.Console.Dao;

namespace SmartBox.Console.Bo
{
    public class Action4AndroidBO : SmartBox.Console.Bo.BaseBO<Action4Android>
    {
        public Action4AndroidBO(BaseDao<Action4Android> dao)
            : base(dao)
        {
        }

        public Action4AndroidBO()
        {
            base._dao = this.Action4AndroidDao;
        }

        private Action4AndroidDao _Action4AndroidDao;
        protected Action4AndroidDao Action4AndroidDao
        {
            get
            {
                if (_Action4AndroidDao == null)
                {
                    _Action4AndroidDao = new Action4AndroidDao(AppConfig.mainDbKey);
                }
                return _Action4AndroidDao;
            }
        }
    }
}
