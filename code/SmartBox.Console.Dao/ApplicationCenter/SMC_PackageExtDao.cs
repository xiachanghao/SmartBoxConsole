using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;
using System.Collections;
using SmartBox.Console.Common;
using System.Data;
using System.Data.SqlClient;

namespace SmartBox.Console.Dao
{
    public partial class SMC_PackageExtDao
    {
        public JsonFlexiGridData QueryPackageExtAsyncList(PageView view, string unitcode)
        {
            string columns = @"pe_id,TableName,pe_Name,pe_ClientType,pe_Category,pe_CreatedTime";
            string sqlorder;

            string sWhere = "";
            if (!String.IsNullOrEmpty(unitcode))
            {
                sWhere = " and pe_unitcode = '" + unitcode.ToString() + "'";
            }

            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  pe_CreatedTime desc ";
            else
                sqlorder = view.OrderBy.ToString();

            return base.QueryDataForFlexGridByPager(columns, "SMC_PackageExt", sqlorder, "pe_id", sWhere, view);
        }



        public JsonFlexiGridData QueryPackageExtList(PageView view, string clientType, string unitcode)
        {
            string columns = @"pe_id,pe_Name,TableName,pe_UsefulStstus, pe_ClientType,pe_UnitName,pe_Category,pe_DownCount,pe_IsTJ,pe_IsBB,pe_CreatedTime";
            string sqlorder;
            string sWhere="";

            if (clientType == "PC")
            { sWhere = " and pe_ClientType='PC/Window' "; }
            else
            {
                sWhere = " and pe_ClientType!='PC/Window' "; 
            }

            if (!String.IsNullOrEmpty(unitcode))
            {
                sWhere += " and pe_unitcode = '" + unitcode.ToString() + "'"; 
            }

            if (string.IsNullOrEmpty(view.OrderBy.ToString()))
                sqlorder = " Order by  pe_CreatedTime desc ";
            else
                sqlorder = view.OrderBy.ToString();

            return base.QueryDataForFlexGridByPager(columns, "SMC_PackageExt", sqlorder, "pe_id", sWhere, view);
        }

        public DataSet GetTJPackageList(Common.Entities.Enum.PackageClientType packageClientType, string unitCode, int is_webapp)
        {
            string sql = "select * from smc_packageext where 1=1  and pe_usefulststus=1 ";
            
            string sClientType = EnumTranslator.TransPackageClientTypeToString(packageClientType);
            if (packageClientType != Common.Entities.Enum.PackageClientType.All)
            {
                sql += " and pe_clienttype='" + sClientType + "' and pe_istj=1 ";
            }
            if (!String.IsNullOrEmpty(unitCode))
            {
                sql += " and pe_UnitCode='" + unitCode + "'";
            }

            if (is_webapp > 0)
            {
                sql += " and tablename='webapplication'";
            }
            else if (is_webapp < 0)
            {
                sql += " and tablename<>'webapplication'";
            }
            DataSet ds = this.ExecuteDataset(sql, CommandType.Text);
            
            return ds;
        }

        public DataSet GetBBPackageList(Common.Entities.Enum.PackageClientType packageClientType, string unitCode, int is_webapp)
        {
            string sql = "select * from smc_packageext where 1=1 and pe_usefulststus=1";
            string sClientType = EnumTranslator.TransPackageClientTypeToString(packageClientType);
            if (packageClientType != Common.Entities.Enum.PackageClientType.All)
            {
                sql += " and pe_clienttype='" + sClientType + "' and pe_isbb=1";
            }
            if (!String.IsNullOrEmpty(unitCode))
            {
                sql += " and pe_UnitCode='" + unitCode + "'";
            }
            if (is_webapp > 0)
            {
                sql += " and tablename='webapplication'";
            }
            else if (is_webapp < 0)
            {
                sql += " and tablename<>'webapplication'";
            }
            DataSet ds = this.ExecuteDataset(sql, CommandType.Text);

            return ds;
        }

        public DataSet GetNewestPackageList(string unitCode)
        {
            string sql = "select top 5 * from smc_packageext where 1=1 and pe_usefulststus=1";

            if (!String.IsNullOrEmpty(unitCode))
            {
                sql += " and pe_UnitCode='" + unitCode + "'";
            }
            sql += " order by pe_CreatedTime desc";
            DataSet ds = this.ExecuteDataset(sql, CommandType.Text);

            return ds;
        }

        public DataSet GetAndroidPhoneAreaPackageList(string unitCode)
        {
            string sql = "select top 3 * from smc_packageext where pe_clienttype='phone/android' and tablename <> 'webapplication' ";

            if (!String.IsNullOrEmpty(unitCode))
            {
                sql += " and pe_UnitCode='" + unitCode + "'";
            }
            sql += " and pe_usefulststus=1 ";
            sql += " order by pe_CreatedTime desc";
            DataSet ds = this.ExecuteDataset(sql, CommandType.Text);

            return ds;
        }

        public DataSet GetiPhoneAreaPackageList(string unitCode)
        {
            string sql = "select top 3 * from smc_packageext where pe_clienttype='phone/ios' and tablename <> 'webapplication' ";

            if (!String.IsNullOrEmpty(unitCode))
            {
                sql += " and pe_UnitCode='" + unitCode + "'";
            }
            sql += " and pe_usefulststus=1 ";
            sql += " order by pe_CreatedTime desc";
            DataSet ds = this.ExecuteDataset(sql, CommandType.Text);

            return ds;
        }

        public DataSet GetAndroidPadAreaPackageList(string unitCode)
        {
            string sql = "select top 3 * from smc_packageext where pe_clienttype='pad/android' and tablename <> 'webapplication' ";

            if (!String.IsNullOrEmpty(unitCode))
            {
                sql += " and pe_UnitCode='" + unitCode + "'";
            }
            sql += " and pe_usefulststus=1 ";
            sql += " order by pe_CreatedTime desc";
            DataSet ds = this.ExecuteDataset(sql, CommandType.Text);

            return ds;
        }

        public DataSet GetiPadAreaPackageList(string unitCode)
        {
            string sql = "select top 3 * from smc_packageext where pe_clienttype='pad/ios' and tablename <> 'webapplication' ";

            if (!String.IsNullOrEmpty(unitCode))
            {
                sql += " and pe_UnitCode='" + unitCode + "'";
            }
            sql += " and pe_usefulststus=1 ";
            sql += " order by pe_CreatedTime desc";
            DataSet ds = this.ExecuteDataset(sql, CommandType.Text);

            return ds;
        }

        public DataSet GetLightAppAreaPackageList(string unitCode)
        {
            string sql = "select top 3 * from smc_packageext where tablename <> 'webapplication' ";

            if (!String.IsNullOrEmpty(unitCode))
            {
                sql += " and pe_UnitCode='" + unitCode + "'";
            }
            sql += " and pe_usefulststus=1 ";
            sql += " order by pe_CreatedTime desc";
            DataSet ds = this.ExecuteDataset(sql, CommandType.Text);

            return ds;
        }

        public DataSet GetApplicationCategoryList(int maxCount)
        {
            string mainDbName = SmartBox.Console.Common.DbSqlHelper.GetMainDBName();
            string sql = "";
            if (maxCount > 0)
            {
                sql = "select top " + maxCount + " id,displayname from [" + mainDbName + "].dbo.ApplicationCategory";
            }
            else
            {
                sql = "select id,displayname from [" + mainDbName + "].dbo.ApplicationCategory";
            }

            DataSet ds = this.ExecuteDataset(sql, CommandType.Text);

            return ds;
        }
        
        public DataSet GetUnitList()
        {
            string sql = "select unit_id,unit_name from dbo.SMC_Unit where upper_unit_id=0";

            DataSet ds = this.ExecuteDataset(sql, CommandType.Text);

            return ds;
        }

        public DataSet SearchPackageList(string keyword, string category, string unitCode, Common.Entities.Enum.PackageClientType clientType, Common.Entities.Enum.PackageTrait trait, int page, int pageSize, out int pageCount, out int recordCount, string uid, int is_webapp)
        { 
            if (pageSize == 0)
            {
                pageSize = 10;
            }
            string sClientType = EnumTranslator.TransPackageClientTypeToString(clientType);
            string sql = "select * from smc_packageext where 1=1";
            sql += " and pe_usefulststus=1";
            if (!String.IsNullOrEmpty(keyword))
            {
                sql += " and pe_Name like '%" + keyword + "%'";
            }

            if (!String.IsNullOrEmpty(category))
            {
                sql += " and pe_CategoryID like '%" + category + "%'";
            }

            if (!String.IsNullOrEmpty(unitCode))
            {
                sql += " and pe_UnitCode = '" + unitCode + "'";
            }

            if (!String.IsNullOrEmpty(sClientType))
            {
                sql += " and pe_ClientType = '" + sClientType + "'";
            }

            if (is_webapp > 0)
            {
                sql += " and tablename='WebApplication' ";
            }
            else if (is_webapp < 0)
            {
                sql += " and tablename <>'WebApplication' ";
            }

            if (trait != Common.Entities.Enum.PackageTrait.All)
            {
                if (trait == Common.Entities.Enum.PackageTrait.Bibei)
                {
                    sql += " and pe_Isbb=1";
                }
                else if (trait == Common.Entities.Enum.PackageTrait.Tuijian)
                {
                    sql += " and pe_IsTJ=1";
                }
            }

            string order = "pe_CreatedTime desc";
            if (trait == Common.Entities.Enum.PackageTrait.Sc)
            {
                sql = "select ext.* from smc_collect c join smc_packageext ext on ext.pe_id=c.pe_id where c.uid='"+uid+"'";
                sql += " and ext.pe_usefulststus=1";
                order = "ext.pe_CreatedTime desc";
            }

            string tableName = SmartBox.Console.Common.StoredProcedureHelper.GetTableName(sql);
            string columns = SmartBox.Console.Common.StoredProcedureHelper.GetColumns(sql);
            string where = SmartBox.Console.Common.StoredProcedureHelper.GetWhere(sql);

            if (trait == Common.Entities.Enum.PackageTrait.ZX)
            {
                columns = " top 20 " + columns;
            }
                
            
            string table = sql;

            SqlParameter[] pars = new SqlParameter[]{
                                    new SqlParameter("@TableName", tableName),
                                    new SqlParameter("@Columns", columns),
                                    new SqlParameter("@CurrentPageIndex", page),
                                    new SqlParameter("@PageSize", pageSize),
                                    new SqlParameter("@RecordCount", 0),
                                    new SqlParameter("@PAGECOUNT", 0),
                                    new SqlParameter("@OrderByColumns", order),
                                    new SqlParameter("@Where", where),
                                    new SqlParameter("@WITH", "")
                                };
            pars[4].Direction = ParameterDirection.InputOutput;
            pars[5].Direction = ParameterDirection.InputOutput;
            DataSet ds = this.ExecuteDataset("Select_Pagination_ex", CommandType.StoredProcedure, pars);
            pageCount = Convert.ToInt32(pars[5].Value);
            recordCount = Convert.ToInt32(pars[4].Value);

            return ds;
        }
//        public DataSet GetPackageList(bool isOutApp, Common.Entities.Enum.PackageClientType packageClientType, SmartBox.Console.Common.Entities.Enum.PackageTrait trait, int page, int pageSize, out int pageCount, out int recordCount)
//        {
//            if (pageSize == 0)
//            {
//                pageSize = 10;
//            }
//            string sClientType = EnumTranslator.TransPackageClientTypeToString(packageClientType);
//            //Hashtable pars = new Hashtable();
//            DataSet ds = new DataSet();
//            if (isOutApp)
//            {
//                string sql = @"select po.po_id,pet.pe_clientType,pet.pe_istj,pet.pe_pictureurl,pet.pe_2dpictureurl,pet.pe_downcount,pet.pe_firmware,
//pet.pe_size,po.[name],po.displayname,po.description,po.version,po.buildver,po.downloaduri 
//from smc_packageext pet join SMC_Package4Out po on pet.Tableid=po.po_id where tablename='SMC_Package4Out'";

//                if (packageClientType == Common.Entities.Enum.PackageClientType.web)
//                {
//                    sql = @" select wp.id,pet.pe_clientType,pet.pe_istj,pet.pe_pictureurl,pet.pe_2dpictureurl,pet.pe_downcount,pet.pe_firmware,
//pet.pe_size,wp.shortname [name],wp.shortname displayname,'' description,'' version,'' buildver,wp.uri downloaduri 
//from smc_packageext pet join [smartbox].dbo.WebApplication wp on pet.Tableid=wp.id where tablename='WebApplication' ";
//                }

//                if (packageClientType != Common.Entities.Enum.PackageClientType.All)
//                {
//                    sql += " and pet.pe_clienttype='" + sClientType + "' ";
//                    //pars.Add("ClientType", sClientType);
//                }


//                if (trait == Common.Entities.Enum.PackageTrait.Bibei)
//                {
//                    sql += " and pet.pe_isbb=1";
                    
//                }
//                else if (trait == Common.Entities.Enum.PackageTrait.Tuijian)
//                {
//                    sql += " and pet.pe_istj=1";
//                }

//                if (page > 0) {
                    
//                    string tableName = SmartBox.Console.Common.StoredProcedureHelper.GetTableName(sql);
//                    string columns = SmartBox.Console.Common.StoredProcedureHelper.GetColumns(sql);
//                    string where = SmartBox.Console.Common.StoredProcedureHelper.GetWhere(sql);
//                    string order = "po.po_id";
//                    string table = sql;

//                    SqlParameter[] pars = new SqlParameter[]{
//                        new SqlParameter("@TableName", tableName),
//                        new SqlParameter("@Columns", columns),
//                        new SqlParameter("@CurrentPageIndex", page),
//                        new SqlParameter("@PageSize", pageSize),
//                        new SqlParameter("@RecordCount", 0),
//                        new SqlParameter("@PAGECOUNT", 0),
//                        new SqlParameter("@OrderByColumns", order),
//                        new SqlParameter("@Where", where),
//                        new SqlParameter("@WITH", "")
//                    };
//                    pars[4].Direction = ParameterDirection.Output;
//                    pars[5].Direction = ParameterDirection.Output;
//                    ds = this.ExecuteDataset("Select_Pagination_ex", pars);
//                    pageCount = Convert.ToInt32(pars[5].Value);
//                    recordCount = Convert.ToInt32(pars[4].Value);
//                } else {
//                    ds = this.ExecuteDataset(sql, CommandType.Text);
//                    pageCount = 0;
//                }
//            }
//            else
//            {
//                string sql = @"select pai.id,pet.pe_clientType,pet.pe_istj,pet.pe_pictureurl,pet.pe_2dpictureurl,pet.pe_downcount,pet.pe_firmware,
//pet.pe_size,pai.[name],pai.displayname,pai.description,pai.version,pai.buildver,pai.downloaduri 
//from smc_packageext pet join [smartbox].dbo.Package4AI pai on pet.Tableid=pai.id where tablename='Package4AI' ";

//                if (packageClientType != Common.Entities.Enum.PackageClientType.All)
//                {
//                    sql += " and pet.pe_clienttype='" + sClientType + "' ";
//                    //pars.Add("ClientType", sClientType);
//                }

//                //and pet.pe_clienttype='pad/android'
//                //and pet.pe_istj=0  and pet.pe_isbb=0

//                if (trait == Common.Entities.Enum.PackageTrait.Bibei)
//                {
//                    sql += " and pet.pe_isbb=1";

//                }
//                else if (trait == Common.Entities.Enum.PackageTrait.Tuijian)
//                {
//                    sql += " and pet.pe_istj=1";
//                }

//                if (page > 0)
//                {
//                    string tableName = SmartBox.Console.Common.StoredProcedureHelper.GetTableName(sql);
//                    string columns = SmartBox.Console.Common.StoredProcedureHelper.GetColumns(sql);
//                    string where = SmartBox.Console.Common.StoredProcedureHelper.GetWhere(sql);
//                    string order = "po.po_id";
//                    string table = sql;

//                    SqlParameter[] pars = new SqlParameter[]{
//                        new SqlParameter("@TableName", tableName),
//                        new SqlParameter("@Columns", columns),
//                        new SqlParameter("@CurrentPageIndex", page),
//                        new SqlParameter("@PageSize", pageSize),
//                        new SqlParameter("@RecordCount", 0),
//                        new SqlParameter("@PAGECOUNT", 0),
//                        new SqlParameter("@OrderByColumns", order),
//                        new SqlParameter("@Where", where),
//                        new SqlParameter("@WITH", "")
//                    };
//                    pars[4].Direction = ParameterDirection.Output;
//                    pars[5].Direction = ParameterDirection.Output;
//                    ds = this.ExecuteDataset("Select_Pagination_ex", pars);
//                    pageCount = Convert.ToInt32(pars[5].Value);
//                    recordCount = Convert.ToInt32(pars[4].Value);
//                }
//                else
//                {
//                    ds = this.ExecuteDataset(sql, CommandType.Text);
//                    pageCount = 0;
//                }
//            }

//            return ds;

//            //Hashtable pars = new Hashtable();
//            //string sql = "";
//            //if (packageClientType != Common.Entities.Enum.PackageClientType.All)
//            //{
//            //    if (trait == Common.Entities.Enum.PackageTrait.All)
//            //    {
//            //        sql = "SELECT * FROM SMC_PackageExt WHERE pe_ClientType = @ClientType";
//            //        pars.Add("ClientType", sClientType);
//            //    }
//            //    else if (trait == Common.Entities.Enum.PackageTrait.Bibei)
//            //    {
//            //        sql = "SELECT * FROM SMC_PackageExt WHERE pe_ClientType = @ClientType and pe_isbb=1";
//            //        pars.Add("ClientType", sClientType);
//            //    }
//            //    else if (trait == Common.Entities.Enum.PackageTrait.Tuijian)
//            //    {
//            //        sql = "SELECT * FROM SMC_PackageExt WHERE pe_ClientType = @ClientType and pe_istj=1";
//            //        pars.Add("ClientType", sClientType);
//            //    }
//            //}
//            //else
//            //{
//            //}
//            //return base.Query(sql, pars);
//        }
    }
}
