using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Mvc;

namespace SmartBox.Console.Common
{
    public class SelectHelper
    {
        #region Init

        public SelectHelper(DataTable selectSource)
        {
            DataSource = selectSource;
        }

        public SelectHelper(DataTable selectSource,string defaultItemText,string defaultItemValue)
        {
            DataSource = selectSource;
            _defaultItemText = defaultItemText;
            _defaultItemValue = defaultItemValue;
        }

        #endregion

        #region Private Property

        private string _defaultItemText = "请选择";
        private string _defaultItemValue = "";
        private bool _hasDefaultItem = true;

        private SelectListItem PleaseSelect
        {
            get
            {
                return new SelectListItem() { Text = _defaultItemText, Value = _defaultItemValue };
            }
        }


        DataTable _dataSource;
        private DataTable DataSource
        {
            get 
            {
                if (_dataSource == null)
                {
                    _dataSource = new DataTable();
                }
                return _dataSource; 
            }
            set { _dataSource = value; }
        }

        #endregion

        #region Methods

        public SelectList GetSelectList(string displayTextColumnName, string valueColumnName, object selectedValue)
        {
            return GetSelectList(displayTextColumnName, valueColumnName, selectedValue, false);
        }

        public SelectList GetSelectList(string displayTextColumnName, string valueColumnName, object selectedValue, bool IsPleaseSelectShow)
        {
            IList<SelectListItem> selectItemLst = GetSelectListItem(displayTextColumnName, valueColumnName, IsPleaseSelectShow);

            return ResultSelectList(selectItemLst, selectedValue);
        }

        public SelectList GetSelectList(int startInt, int endInt, string displayTextColumnName, string valueColumnName, object selectedValue, bool IsPleaseSelectShow)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(displayTextColumnName);
            dt.Columns.Add(valueColumnName);

            for (int i = startInt; i < endInt; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i;
                dt.Rows.Add(dr);
            }
            IList<SelectListItem> result = new List<SelectListItem>();
            if (IsPleaseSelectShow) result.Add(PleaseSelect);

            result = GetSelectList(displayTextColumnName, valueColumnName, result, dt);

            return ResultSelectList(result, selectedValue);
        }

        public IList<SelectListItem> GetSelectListItem(string displayTextColumnName, string valueColumnName, bool IsPleaseSelectShow)
        {
            IList<SelectListItem> selectItemLst = new List<SelectListItem>();

            //添加“请选择”项
            if (IsPleaseSelectShow) selectItemLst.Add(PleaseSelect);

            selectItemLst = BindSelectItem(DataSource, displayTextColumnName, valueColumnName, selectItemLst);
            return selectItemLst;
        }

        public IList<SelectListItem> GetSelectListItem(string displayTextColumnName, string valueColumnName, IEnumerable<string> selectedValue, bool IsPleaseSelectShow)
        {
            IList<SelectListItem> selectItemLst = new List<SelectListItem>();
            //添加“请选择”项
            if (IsPleaseSelectShow) selectItemLst.Add(PleaseSelect);
            selectItemLst = BindSelectItem(DataSource, displayTextColumnName, valueColumnName, selectItemLst);
            foreach (string t in selectedValue)
            {
                foreach (SelectListItem item in selectItemLst.Where(x => x.Text.Equals(t, StringComparison.CurrentCultureIgnoreCase) || x.Value.Equals(t, StringComparison.CurrentCultureIgnoreCase)))
                {
                    item.Selected = true;
                }
            }
            return selectItemLst;
        }

        private IList<SelectListItem> BindSelectItem(DataTable bindData, string TextName, string valueName, IList<SelectListItem> slctItmLst)
        {
            IList<SelectListItem> result = new List<SelectListItem>();

            if (slctItmLst != null && slctItmLst.Count > 0)
                result = slctItmLst;

            //DataTable bindData = null;//SqlHelper.GetIntence.ExcuteDataTable(dataSource, null);
            return GetSelectList(TextName, valueName, result, bindData);
        }

        private IList<SelectListItem> GetSelectList(string TextName, string valueName, IList<SelectListItem> result, DataTable bindData)
        {
            if (!(bindData.Columns.Contains(TextName) && bindData.Columns.Contains(valueName))) return result;
            if (!(bindData != null && bindData.Rows.Count > 0)) return result;

            try
            {
                foreach (DataRow item in bindData.Rows)
                {
                    string text = item[TextName] == null ? "" : item[TextName].ToString();
                    string value = item[valueName] == null ? "" : item[valueName].ToString();
                    result.Add(new SelectListItem() { Text = text, Value = value });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("数据源中未找到列",ex);
            }

            return result;
        }

        private SelectList ResultSelectList(System.Collections.IEnumerable selectItems, object selectValue)
        {
            return new SelectList(selectItems, "Value", "Text", selectValue);
        }

        #endregion
    }
}
