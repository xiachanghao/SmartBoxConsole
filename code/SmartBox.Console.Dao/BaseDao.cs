using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.DataAccess.ObjectDAO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data.Common;
using SmartBox.Console.Common;

namespace SmartBox.Console.Dao
{
    public abstract class BaseDao<T> : ObjectDAO<T> where T : class, new()
    {
        public BaseDao(string key)
            : base(key)
        {

        }

        public string GetTableName()
        {
            return typeof(T).Name.ToString(); 
        }

        public string GetPrimaryKeyName(string tableName)
        {
            string _tableName = "";
            if (String.IsNullOrEmpty(tableName))
                _tableName = GetTableName();
            else
                _tableName = tableName;
            string sql = "select name from sys.columns Where OBJECT_ID=OBJECT_ID('" + _tableName + "') AND COLUMN_ID=1";
            object o = this.ExecuteScalar(sql);
            string key = "";
            if (o != null)
            {
                key = Convert.ToString(o);
            }
            return key;
        }

        public int GetMaxId()
        {
            string tableName = GetTableName();
            string key = GetPrimaryKeyName(tableName);
            string sql = "select isnull(max(" + key + "),0) from " + tableName;
            object o = this.ExecuteScalar(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }
            return i;
        }

        public int GetMaxId(string tableName)
        {
            string key = GetPrimaryKeyName(tableName);
            string sql = "select isnull(max(" + key + "),0) from " + tableName;
            object o = this.ExecuteScalar(sql);
            int i = 0;
            if (o != null)
            {
                i = Convert.ToInt32(o);
            }
            return i;
        }

        public virtual T Get(IList<KeyValuePair<string, object>> pars)
        {
            string tableName = GetTableName();
            string sql = String.Format("select * from {0} where 1=1 ", tableName);
            foreach (KeyValuePair<string, object> par in pars)
            {                
                if (par.Value == null)
                {
                }
                else
                {
                    if (par.Value == null)
                    {
                        sql += " and (" + par.Key + " is null) ";
                    }
                    else
                    {
                        sql += " and " + par.Key + "=";
                        Type tp = par.Value.GetType();
                        switch (tp.Name.ToLower())
                        {
                            case "string":
                                sql += "'" + par.Value + "'";
                                break;
                            default:
                                sql += par.Value;
                                break;
                        }
                    }
                }
            }
            IList<T> objs = this.QuerySql(sql);
            if (objs != null && objs.Count > 0)
            {
                return objs[0];
            }
            else
                return null;
        }

        public bool ExistMaxId()
        {
            string tableName = GetTableName();
            string sql = "select count(at_id) from smc_autotableid where at_tablename='" + tableName + "'";
            object o = this.ExecuteScalar(sql);
            if (o == null)
                return false;
            else
                return Convert.ToInt32(o) > 0;
        }

        public bool ExistMaxId(string tableName)
        {
            string sql = "select count(at_id) from smc_autotableid where at_tablename='" + tableName + "'";
            object o = this.ExecuteScalar(sql);
            if (o == null)
                return false;
            else
                return Convert.ToInt32(o) > 0;
        }

        public void UpdateMaxId()
        {
            string tableName = GetTableName();
            int maxId = GetMaxId();
            bool exist = ExistMaxId();
            if (exist)
            {
                string sql = "update smc_autotableid set at_maxid=@maxid where at_tablename='" + tableName + "'";
                Hashtable pars = new Hashtable();
                pars.Add("maxid", maxId);
                this.ExecuteNonQuery(sql, pars);
            }
            else
            {
                string sql = "insert into smc_autotableid values('" + tableName + "', @maxid)";
                Hashtable pars = new Hashtable();
                pars.Add("maxid", maxId);
                this.ExecuteNonQuery(sql, pars);
            }
        }

        public void UpdateMaxId(string tableName)
        {
            int maxId = GetMaxId(tableName);
            bool exist = ExistMaxId(tableName);
            if (exist)
            {
                string sql = "update smc_autotableid set at_maxid=@maxid where at_tablename='" + tableName + "'";
                Hashtable pars = new Hashtable();
                pars.Add("maxid", maxId);
                this.ExecuteNonQuery(sql, pars);
            }
            else
            {
                string sql = "insert into smc_autotableid values('" + tableName + "', @maxid)";
                Hashtable pars = new Hashtable();
                pars.Add("maxid", maxId);
                this.ExecuteNonQuery(sql, pars);
            }
        }

        public SelectPagnationEx<T> SelectPaginationExT(string tableName, string columns, int currentPageIndex, int pageSize, string orderBy, string where, string with)
        {
            SelectPagnationEx ex = SelectPaginationEx(tableName, columns, currentPageIndex, pageSize, orderBy, where, with);
            SelectPagnationEx<T> exusr = SelectPagnationExHelper<T>.Translate(ex);
            return exusr;
        }
        
        public SelectPagnationExDictionary SelectPaginationExDictionary(string tableName, string columns, int currentPageIndex, int pageSize, string orderBy, string where, string with)
        {
            SelectPagnationEx ex = SelectPaginationEx(tableName, columns, currentPageIndex, pageSize, orderBy, where, with);
            SelectPagnationExDictionary exusr = SelectPagnationExDictionaryHelper.Translate(ex);
            return exusr;
        }
        /// <summary>
        /// --=============================================================================================
        /// --单表查询调用
        /// --****************************************************************************************************
        /// --原始查询语句
        /// --SELECT Addressid,AddressLine1,City FROM [AdventureWorks].[Person].[Address] where (2=2 OR 3=3) AND Addressid > 3000
        /// --****************************************************************************************************
        /// --DECLARE @return_value int,
        /// --  @PAGECOUNT int,
        /// --  @RECORDCOUNT INT
        /// --EXEC @return_value = [dbo].[Select_Pagination]
        /// --  @TableName = N'[AdventureWorks].[Person].[Address]',
        /// --  @Columns = N'Addressid,AddressLine1,City',
        /// --  @CurrentPageIndex = 1,
        /// --  @PageSize = 10,
        /// --  @RecordCount = @RecordCount OUTPUT,
        /// --  @PAGECOUNT = @PAGECOUNT OUTPUT,
        /// --  @OrderByColumnS = N'Addressid Asc',
        /// --  @WHERE = N'(2=2 OR 3=3) AND Addressid > 3000'
        /// --SELECT @PAGECOUNT as N'@PAGECOUNT'
        /// --SELECT @RecordCount as N'@RECORDCOUNT'
        /// --SELECT 'Return Value' = @return_value
        /// --GO
        /// --***************************************************************************************
        /// --连接查询调用
        /// --****************************************************************************************************
        /// --原始查询语句
        /// --select [CustomerID],[TerritoryID],[AccountNumber],[CustomerType],[rowguid],[ModifiedDate],CustomerType.[Name]
        /// --FROM [AdventureWorks].[Sales].[Customer] join customertype on [Sales].[Customer].CustomerType = CustomerType.ID
        /// --order by [Sales].[Customer].ModifiedDate desc,[Sales].[Customer].CustomerID DESC
        /// --****************************************************************************************************
        /// --USE [AdventureWorks]
        /// --GO
        /// --DECLARE @return_value int,
        /// --  @PAGECOUNT int,
        /// --  @RECORDCOUNT INT
        /// --EXEC @return_value = [dbo].[Select_Pagination]
        /// --  @TableName = N'[AdventureWorks].[Sales].[Customer] join customertype on [Sales].[Customer].CustomerType = CustomerType.ID',
        /// --  @Columns = N'[CustomerID],[TerritoryID],[AccountNumber],[CustomerType],[rowguid],[ModifiedDate],CustomerType.[Name]',
        /// --  @CurrentPageIndex = 1916,
        /// --  @PageSize = 10,
        /// --  @RecordCount = @RecordCount OUTPUT,
        /// --  @PAGECOUNT = @PAGECOUNT OUTPUT,
        /// --  @OrderByColumnS = N'[Sales].[Customer].ModifiedDate desc,[Sales].[Customer].CustomerID DESC',
        /// --  @wHERE = N''
        /// --SELECT @PAGECOUNT as N'@PAGECOUNT'
        /// --SELECT @RecordCount as N'@RECORDCOUNT'
        /// --SELECT 'Return Value' = @return_value
        /// --GO
        /// --=============================================================================================
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="where"></param>
        /// <param name="with"></param>
        /// <returns></returns>
        public SelectPagnationEx SelectPaginationEx(string tableName, string columns, int currentPageIndex, int pageSize, string orderBy, string where, string with)
        {
            SelectPagnationEx r = new SelectPagnationEx();
            SqlParameter[] Para = new SqlParameter[10];

            Para[0] = new SqlParameter("@TableName", SqlDbType.NVarChar, 4000);
            Para[0].Value = tableName;

            Para[1] = new SqlParameter("@Columns", SqlDbType.NVarChar, 4000);
            Para[1].Value = columns;

            Para[2] = new SqlParameter("@CurrentPageIndex", SqlDbType.Int, 4);
            Para[2].Value = currentPageIndex;

            Para[3] = new SqlParameter("@PageSize", SqlDbType.Int, 4);
            Para[3].Value = pageSize;

            Para[4] = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            Para[4].Direction = ParameterDirection.InputOutput;
            Para[4].Value = 0;

            Para[5] = new SqlParameter("@PAGECOUNT", SqlDbType.Int, 4);
            Para[5].Direction = ParameterDirection.InputOutput;
            Para[5].Value = 0;

            Para[6] = new SqlParameter("@OrderByColumns", SqlDbType.VarChar, 200);
            Para[6].Value = orderBy;

            Para[7] = new SqlParameter("@Where", SqlDbType.NVarChar, 4000);
            Para[7].Value = where;

            Para[8] = new SqlParameter("@WITH", SqlDbType.VarChar, 8000);
            Para[8].Value = with;

            Para[9] = new SqlParameter("@returnvalue", SqlDbType.Int);
            Para[9].Direction = ParameterDirection.ReturnValue;

            DataSet ds = new DataSet();
            try
            {
                string strConn = System.Configuration.ConfigurationManager.ConnectionStrings[this.connectionKey].ConnectionString;
                SqlConnection conn = new SqlConnection(strConn);
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Select_Pagination_ex", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(Para);
                    SqlDataAdapter ada = new SqlDataAdapter(cmd);
                    ada.Fill(ds);
                    //ds = base.ExecuteDataset("Select_Pagination_ex", 600, Para);
                }
                finally
                {
                    conn.Close();
                }
            }
            catch (Exception e)
            {
            }
            r.Result = ds;
            r.PageCount = Convert.ToInt32(Para[5].Value);
            r.RecordCount = Convert.ToInt32(Para[4].Value);
            r.ReturnValue = Convert.ToInt32(Para[9].Value);
            return r;
        }

        public DataTable QueryTableByPager(string columns, string table, string order, string pk, string strparams, PageView view)
        {
            SqlParameter[] Para = new SqlParameter[8];

            Para[0] = new SqlParameter("@SQLPARAMS", SqlDbType.NVarChar, -1);
            Para[0].Value = strparams;

            Para[1] = new SqlParameter("@PAGESIZE", SqlDbType.Int, 4);
            Para[1].Value = view.PageSize;

            Para[2] = new SqlParameter("@PAGEINDEX", SqlDbType.Int, 4);
            Para[2].Value = view.PageIndex;


            Para[3] = new SqlParameter("@SQLTABLE", SqlDbType.NVarChar, -1);
            Para[3].Value = table;

            Para[4] = new SqlParameter("@SQLCOLUMNS", SqlDbType.NVarChar, -1);
            Para[4].Value = columns;


            Para[5] = new SqlParameter("@SQLPK", SqlDbType.VarChar, 50);
            Para[5].Value = pk;

            Para[6] = new SqlParameter("@SQLORDER", SqlDbType.VarChar, 200);
            Para[6].Value = order;

            Para[7] = new SqlParameter("@Count", SqlDbType.Int, 4);
            Para[7].Direction = ParameterDirection.Output;

            DataSet ds = base.ExecuteDataset("PAGESELECT", 600, Para);
            if (ds != null && ds.Tables.Count > 0)
            {

                if (ds.Tables.Count > 1)
                {
                    if (ds.Tables[1].Rows[0][0] != null && ds.Tables[1].Rows[0][0] != System.DBNull.Value)
                        view.RecordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                }
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public IDictionary ExcuteDictionary(string sql, IDictionary paras)
        {
            IDataReader read;
            Hashtable dict = new Hashtable();
            if (paras != null)
            {
                read = base.ExecuteDataReader(sql, paras);
            }
            else
            {
                read = base.ExecuteDataReader(sql);
            }
            try
            {
                if (read.FieldCount >= 2)
                {
                    while (read.Read())
                    {
                        object key = read.GetValue(0);
                        object value = read.GetValue(1);
                        if (key != null && value != null)
                        {
                            dict.Add(key, value);
                        }
                    }
                }
            }
            catch
            {

            }
            finally
            {
                if (read != null)
                {
                    read.Close();
                    read.Dispose();
                }
            }
            return dict;

        }

        public JsonFlexiGridData QueryDataForFlexGrid(string strSql, DbParameter[] parms, string pk)
        {
            var dt = ExecuteDataset(strSql, CommandType.Text, parms).Tables[0];
            PageView view = new PageView { PageIndex = 0, RecordCount = dt.Rows.Count };
            return ConvertJosnFlexGridData(dt, view, pk);
        }

        public JsonFlexiGridData QueryDataForFlexGridByPager(string columns, string table, string order, string pk, string strparams, PageView view)
        {
            DataTable dt = QueryTableByPager(columns, table, order, pk, strparams, view);

            return ConvertJosnFlexGridData(dt, view, pk);
        }

        public List<IDictionary<string, object>> TranslateTable(DataTable dt)
        {
            List<IDictionary<string, object>> Result = new List<IDictionary<string, object>>();

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                IDictionary<string, object> Obj = new Dictionary<string, object>();

                foreach (DataColumn clmn in dr.Table.Columns)
                {

                    if (dr.IsNull(clmn.ColumnName))
                        Obj[clmn.ColumnName] = null;
                    else
                        Obj[clmn.ColumnName] = dr[clmn.ColumnName];
                }
                Result.Add(Obj);
            }
            return Result;
        }

        public static JsonFlexiGridData ConvertJosnFlexGridData(DataTable dt, PageView view, string pk)
        {
            JsonFlexiGridData data = new JsonFlexiGridData { page = view.PageIndex + 1, total = view.RecordCount };

            if (dt != null && dt.Rows.Count > 0)
            {
                if (data.rows == null)
                {
                    data.rows = new List<FlexiGridRow>();
                }
                foreach (DataRow dr in dt.Rows)
                {
                    FlexiGridRow row = new FlexiGridRow { id = dr[pk].ToString(), cell = new List<string>() };
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string value = dr[i] != DBNull.Value ? dr[i].ToString() : null;
                        row.cell.Add(value);
                    }
                    data.rows.Add(row);
                }
            }
            return data;
        }


        public List<T> QueryList(IList<Tuple<string, string, object>> pars)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from ");

            string tableName = this.GetTableName().ToLower();
            sb.Append(tableName);
            sb.Append(" where 1=1 ");

            foreach (Tuple<string, string, object> t in pars)
            {

                sb.Append(" and ");
                sb.Append(t.Item1);
                sb.Append(" ");
                sb.Append(t.Item2);
                sb.Append(" ");
                bool need_add_quote = false;
                switch (t.Item3.GetType().FullName)
                {
                    case "System.String":
                        need_add_quote = true;
                        break;
                }

                if (need_add_quote)
                    sb.Append("'");
                sb.Append(t.Item3);
                if (need_add_quote)
                    sb.Append("'");
            }

            if (tableName == "smc_unit")
            sb.Append(" order by Unit_Sequence");

            string sql = sb.ToString();
            return this.Query(sql).ToList<T>();
        }
    }

    public class CommonDao<T> : BaseDao<T> where T : class, new()
    {
        public CommonDao(string key)
            : base(key)
        {

        }
    }
}
