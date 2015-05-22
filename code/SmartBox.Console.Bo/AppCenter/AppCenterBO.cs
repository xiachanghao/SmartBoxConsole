using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using SmartBox.Console.Dao;
using SmartBox.Console.Common;
using SmartBox.Console.Common.Entities;
using System.Web.Mvc;
using System.Data;
using Beyondbit.Framework.DataAccess;
using Beyondbit.Framework.Biz;
using System.Configuration;
using System.IO;

namespace SmartBox.Console.Bo.AppCenter
{
    public partial class AppCenterBO
    {
        SMC_PackageFAQDao faqDao = new SMC_PackageFAQDao(AppConfig.statisticDBKey);
        SMC_PackageManualDao manualDao = new SMC_PackageManualDao(AppConfig.statisticDBKey);
        SMC_PackagePictureDao pictureDao = new SMC_PackagePictureDao(AppConfig.statisticDBKey);
        SMC_PackageExtDao packageDao = new SMC_PackageExtDao(AppConfig.statisticDBKey);
        SMC_UnitDao unitDao = new SMC_UnitDao(AppConfig.statisticDBKey);
        SMC_UserDao userDao = new SMC_UserDao(AppConfig.statisticDBKey);

        private Controller _ctroller = null;
        public AppCenterBO()
        {
            
        }

        public virtual void SetController(Controller ctrl) {
            _ctroller = ctrl;
        }

        public virtual SMC_User GetUser(string uid)
        {
            return userDao.GetUser(uid);
        }

        public virtual SMC_PackageExt GetPackage(string tableName, string tableId)
        {
            SMC_PackageExtDao peDao = new SMC_PackageExtDao(AppConfig.statisticDBKey);
            return peDao.getPackage(tableName, tableId);
        }
        public virtual SMC_PackageExt GetPackageExt(int packageId)
        {
            SMC_PackageExtDao peDao = new SMC_PackageExtDao(AppConfig.statisticDBKey);
            SMC_PackageExt ext = peDao.Get(packageId);
            return ext;
        }
        public virtual Hashtable GetPackage(int packageId)
        {
            SMC_PackageExtDao peDao = new SMC_PackageExtDao(AppConfig.statisticDBKey);
            SMC_PackageExt ext = peDao.Get(packageId);
            Hashtable hash = new Hashtable();
            //hash["ItemName"] = ext.pe_Name;// dr["pe_Name"].ToString();// "PPS影音";
            //hash["ItemVersion"] = "";// dr["version"].ToString();// "android手机版";
            hash["Class"] = ext.pe_Category;//"办公/政务";
            hash["Unit"] = ext.pe_UnitName;// "上海互联网软件";
            hash["ItemDate"] = ext.pe_CreatedTime.ToString("yyyy-MM-dd");
            hash["os"] = ext.pe_Firmware;
            hash["DownloadCount"] = ext.pe_DownCount;
            hash["size"] = ext.pe_Size / 1024 / 1024;
            string pe_PictureUrl = ext.pe_PictureUrl;
            if (String.IsNullOrEmpty(ext.pe_PictureUrl))
            {
                hash["appImage"] = this._ctroller.Url.Content("~/AppIcons/No.png");
            }
            else
            {
                hash["appImage"] = this._ctroller.Url.Content(ext.pe_PictureUrl);
            }
            int tableid = Convert.ToInt32(ext.TableID);
            hash["id"] = tableid;
            hash["name"] = ext.pe_Name;
            hash["displayname"] = ext.pe_DisplayName;
            hash["description"] = ext.pe_Description;
            hash["version"] = ext.pe_Version;
            hash["buildver"] = ext.pe_BuildVer;
            hash["downloaduri"] = ext.pe_DownloadUri;
            hash["pe_clienttype"] = ext.pe_ClientType == null ? "" : ext.pe_ClientType;

            string tablename = ext.TableName;
            
            switch (tablename.ToLower())
            {
                case "package4ai":
                    //Package4AIDao padao = new Package4AIDao(AppConfig.mainDbKey);
                    //Package4AI entity = padao.Get(tableid);
                    //hash["id"] = tableid;
                    //hash["name"] = entity.Name;
                    //hash["displayname"] = entity.DisplayName;
                    //hash["description"] = entity.Description;
                    //hash["version"] = entity.Version;
                    //hash["buildver"] = entity.BuildVer;
                    //hash["downloaduri"] = entity.DownloadUri;
                    break;
                case "webapplication":
                    //WebApplicationDao wad = new WebApplicationDao(AppConfig.mainDbKey);
                    //WebApplication application = wad.Get(tableid);
                    //hash["id"] = tableid;
                    //hash["name"] = application.ShortName;
                    //hash["displayname"] = application.ShortName;
                    //hash["description"] = "";
                    //hash["version"] = "";
                    //hash["buildver"] = "";
                    //hash["downloaduri"] = application.Uri;
                    
                    break;
                case "smc_package4out":
                    //SMC_Package4OutDao poDao = new SMC_Package4OutDao(AppConfig.statisticDBKey);
                    //SMC_Package4Out outEntity = poDao.Get(tableid);
                    //hash["id"] = tableid;
                    //hash["name"] = outEntity.Name;
                    //hash["displayname"] = outEntity.DisplayName;
                    //hash["description"] = outEntity.Description;
                    //hash["version"] = outEntity.Version;
                    //hash["buildver"] = outEntity.BuildVer;
                    //hash["downloaduri"] = outEntity.DownloadUri;
                    break;
            }

            //string downUrl = "";//ext.downloaduri;
            //hash["appUrl"] = Url.Content("~/Home/Detail?packageid=") + dr["id"].ToString();
            hash["HelpUrl"] = "";
            //hash["downUrl"] = this._ctroller.Url.Content(downUrl); //"~/Home/Detail?packageid=5"
            return hash;
        }

        public virtual DataSet GetTJPackageList(Common.Entities.Enum.PackageClientType packageClientType, string unitCode, int is_webapp)
        {
            SMC_PackageExtDao peDao = new SMC_PackageExtDao(AppConfig.statisticDBKey);
            DataSet ds = peDao.GetTJPackageList(packageClientType, unitCode, is_webapp);
            return ds;
            //DataColumn clmn = ds.Tables[0].Columns.Add("id", typeof(string));
            //clmn = ds.Tables[0].Columns.Add("name", typeof(string));
            //clmn = ds.Tables[0].Columns.Add("displayname", typeof(string));
            //clmn = ds.Tables[0].Columns.Add("description", typeof(string));
            //clmn = ds.Tables[0].Columns.Add("version", typeof(string));
            //clmn = ds.Tables[0].Columns.Add("buildver", typeof(string));
            //clmn = ds.Tables[0].Columns.Add("downloaduri", typeof(string));

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    string tablename = dr["tablename"].ToString().ToLower();
            //    int tableid = Convert.ToInt32(dr["tableid"]);
            //    switch (tablename)
            //    {
            //        case "package4ai":
            //            Package4AIDao padao = new Package4AIDao(AppConfig.mainDbKey);
            //            Package4AI entity = padao.Get(tableid);
            //            dr["id"] = tableid;
            //            dr["name"] = entity.Name;
            //            dr["displayname"] = entity.DisplayName;
            //            dr["description"] = entity.Description;
            //            dr["version"] = entity.Version;
            //            dr["buildver"] = entity.BuildVer;
            //            dr["downloaduri"] = entity.DownloadUri;
            //            break;
            //        case "webapplication":
            //            WebApplicationDao wad = new WebApplicationDao(AppConfig.mainDbKey);
            //            WebApplication application = wad.Get(tableid);
            //            dr["id"] = tableid;
            //            dr["name"] = application.ShortName;
            //            dr["displayname"] = application.ShortName;
            //            dr["description"] = "";
            //            dr["version"] = "";
            //            dr["buildver"] = "";
            //            dr["downloaduri"] = application.Uri;
            //            break;
            //        case "smc_package4out":
            //            SMC_Package4OutDao poDao = new SMC_Package4OutDao(AppConfig.statisticDBKey);
            //            SMC_Package4Out outEntity = poDao.Get(tableid);
            //            dr["id"] = tableid;
            //            dr["name"] = outEntity.Name;
            //            dr["displayname"] = outEntity.DisplayName;
            //            dr["description"] = outEntity.Description;
            //            dr["version"] = outEntity.Version;
            //            dr["buildver"] = outEntity.BuildVer;
            //            dr["downloaduri"] = outEntity.DownloadUri;
            //            break;
            //    }
            //}

            //return ds;
        }

        public virtual string GetUserUnitCode(string useruid)
        {
            SMC_UserDao udao = new SMC_UserDao(AppConfig.statisticDBKey);
            SMC_User u = udao.GetUser(useruid);
            if (u == null)
                return "";
            else
                return u.U_UNITCODE;
        }

        public virtual DataSet GetBBPackageList(Common.Entities.Enum.PackageClientType packageClientType, string unitCode, int is_webapp)
        {
            SMC_PackageExtDao peDao = new SMC_PackageExtDao(AppConfig.statisticDBKey);
            DataSet ds = peDao.GetBBPackageList(packageClientType, unitCode, is_webapp);
            return ds;
            //DataColumn clmn = ds.Tables[0].Columns.Add("id", typeof(int));
            //clmn = ds.Tables[0].Columns.Add("name");
            //clmn.DataType = typeof(string);
            //clmn = ds.Tables[0].Columns.Add("displayname");
            //clmn.DataType = typeof(string);
            //clmn = ds.Tables[0].Columns.Add("description");
            //clmn.DataType = typeof(string);
            //clmn = ds.Tables[0].Columns.Add("version");
            //clmn.DataType = typeof(string);
            //clmn = ds.Tables[0].Columns.Add("buildver");
            //clmn.DataType = typeof(string);
            //clmn = ds.Tables[0].Columns.Add("downloaduri");
            //clmn.DataType = typeof(string);
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    string tablename = dr["tablename"].ToString().ToLower();
            //    int tableid = Convert.ToInt32(dr["tableid"]);
            //    switch (tablename)
            //    {
            //        case "package4ai":
            //            Package4AIDao padao = new Package4AIDao(AppConfig.mainDbKey);
            //            Package4AI entity = padao.Get(tableid);
            //            dr["id"] = tableid;
            //            dr["name"] = entity.Name;
            //            dr["displayname"] = entity.DisplayName;
            //            dr["description"] = entity.Description;
            //            dr["version"] = entity.Version;
            //            dr["buildver"] = entity.BuildVer;
            //            dr["downloaduri"] = entity.DownloadUri;
            //            break;
            //        case "webapplication":
            //            WebApplicationDao wad = new WebApplicationDao(AppConfig.mainDbKey);
            //            WebApplication application = wad.Get(tableid);
            //            dr["id"] = tableid;
            //            dr["name"] = application.ShortName;
            //            dr["displayname"] = application.ShortName;
            //            dr["description"] = "";
            //            dr["version"] = "";
            //            dr["buildver"] = "";
            //            dr["downloaduri"] = application.Uri;
            //            break;
            //        case "smc_package4out":
            //            SMC_Package4OutDao poDao = new SMC_Package4OutDao(AppConfig.statisticDBKey);
            //            SMC_Package4Out outEntity = poDao.Get(tableid);
            //            dr["id"] = tableid;
            //            dr["name"] = outEntity.Name;
            //            dr["displayname"] = outEntity.DisplayName;
            //            dr["description"] = outEntity.Description;
            //            dr["version"] = outEntity.Version;
            //            dr["buildver"] = outEntity.BuildVer;
            //            dr["downloaduri"] = outEntity.DownloadUri;
            //            break;
            //    }
            //}

            //return ds;
        }

        public virtual DataSet GetNewestPackageList(string unitCode)
        {
            DataSet ds = packageDao.GetNewestPackageList(unitCode);
            return ds;
            //DataColumn clmn = ds.Tables[0].Columns.Add("id", typeof(int));
            //clmn = ds.Tables[0].Columns.Add("name", typeof(string));
            //clmn = ds.Tables[0].Columns.Add("displayname", typeof(string));
            //clmn = ds.Tables[0].Columns.Add("downloaduri", typeof(string));
            //clmn = ds.Tables[0].Columns.Add("version", typeof(string));

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    string tablename = dr["tablename"].ToString().ToLower();
            //    int tableid = Convert.ToInt32(dr["tableid"]);
            //    switch (tablename)
            //    {
            //        case "package4ai":
            //            Package4AIDao padao = new Package4AIDao(AppConfig.mainDbKey);
            //            Package4AI entity = padao.Get(tableid);
            //            dr["id"] = tableid;
            //            dr["name"] = entity.Name;
            //            dr["displayname"] = entity.DisplayName;
            //            dr["downloaduri"] = entity.DownloadUri;
            //            dr["version"] = entity.Version;
            //            break;
            //        case "webapplication":
            //            WebApplicationDao wad = new WebApplicationDao(AppConfig.mainDbKey);
            //            WebApplication application = wad.Get(tableid);
            //            dr["id"] = tableid;
            //            dr["name"] = application.ShortName;
            //            dr["displayname"] = application.ShortName;
            //            dr["downloaduri"] = application.Uri;
            //            dr["version"] = "";
            //            break;
            //        case "smc_package4out":
            //            SMC_Package4OutDao poDao = new SMC_Package4OutDao(AppConfig.statisticDBKey);
            //            SMC_Package4Out outEntity = poDao.Get(tableid);
            //            dr["id"] = tableid;
            //            dr["name"] = outEntity.Name;
            //            dr["displayname"] = outEntity.DisplayName;
            //            dr["downloaduri"] = outEntity.DownloadUri;
            //            dr["version"] = outEntity.Version;
            //            break;
            //    }
            //}

            //return ds;
        }

        public virtual DataSet GetAndroidPhoneAreaPackageList(string unitCode)
        {
            DataSet ds = packageDao.GetAndroidPhoneAreaPackageList(unitCode);
            return ds;
            //DataColumn clmn = ds.Tables[0].Columns.Add("id", typeof(int));

            //clmn = ds.Tables[0].Columns.Add("name");
            //clmn.DataType = typeof(string);
            //clmn = ds.Tables[0].Columns.Add("displayname");
            //clmn.DataType = typeof(string);
            //clmn = ds.Tables[0].Columns.Add("description");
            //clmn.DataType = typeof(string);
            //clmn = ds.Tables[0].Columns.Add("version");
            //clmn.DataType = typeof(string);
            //clmn = ds.Tables[0].Columns.Add("buildver");
            //clmn.DataType = typeof(string);
            //clmn = ds.Tables[0].Columns.Add("downloaduri");
            //clmn.DataType = typeof(string);

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    string tablename = dr["tablename"].ToString().ToLower();
            //    int tableid = Convert.ToInt32(dr["tableid"]);
            //    switch (tablename)
            //    {
            //        case "package4ai":
            //            Package4AIDao padao = new Package4AIDao(AppConfig.mainDbKey);
            //            Package4AI entity = padao.Get(tableid);
            //            dr["id"] = tableid;
            //            dr["name"] = entity.Name;
            //            dr["displayname"] = entity.DisplayName;
            //            dr["description"] = entity.Description;
            //            dr["version"] = entity.Version;
            //            dr["buildver"] = entity.BuildVer;
            //            dr["downloaduri"] = entity.DownloadUri;

            //            break;
            //        case "webapplication":
            //            WebApplicationDao wad = new WebApplicationDao(AppConfig.mainDbKey);
            //            WebApplication application = wad.Get(tableid);
            //            dr["id"] = tableid;
            //            dr["name"] = application.ShortName;
            //            dr["displayname"] = application.ShortName;
            //            dr["description"] = "";
            //            dr["version"] = "";
            //            dr["buildver"] = "";
            //            dr["downloaduri"] = application.Uri;
            //            break;
            //        case "smc_package4out":
            //            SMC_Package4OutDao poDao = new SMC_Package4OutDao(AppConfig.statisticDBKey);
            //            SMC_Package4Out outEntity = poDao.Get(tableid);
            //            dr["id"] = tableid;
            //            dr["name"] = outEntity.Name;
            //            dr["displayname"] = outEntity.DisplayName;
            //            dr["description"] = outEntity.Description;
            //            dr["version"] = outEntity.Version;
            //            dr["buildver"] = outEntity.BuildVer;
            //            dr["downloaduri"] = outEntity.DownloadUri;
            //            break;
            //    }
            //}

            //return ds;
        }

        public virtual DataSet GetiPhoneAreaPackageList(string unitCode)
        {
            DataSet ds = packageDao.GetiPhoneAreaPackageList(unitCode);
            return ds;
            DataColumn clmn = ds.Tables[0].Columns.Add("id", typeof(int));

            clmn = ds.Tables[0].Columns.Add("name");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("displayname");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("description");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("version");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("buildver");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("downloaduri");
            clmn.DataType = typeof(string);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string tablename = dr["tablename"].ToString().ToLower();
                int tableid = Convert.ToInt32(dr["tableid"]);
                switch (tablename)
                {
                    case "package4ai":
                        Package4AIDao padao = new Package4AIDao(AppConfig.mainDbKey);
                        Package4AI entity = padao.Get(tableid);
                        dr["id"] = tableid;
                        dr["name"] = entity.Name;
                        dr["displayname"] = entity.DisplayName;
                        dr["description"] = entity.Description;
                        dr["version"] = entity.Version;
                        dr["buildver"] = entity.BuildVer;
                        dr["downloaduri"] = entity.DownloadUri;

                        break;
                    case "webapplication":
                        WebApplicationDao wad = new WebApplicationDao(AppConfig.mainDbKey);
                        WebApplication application = wad.Get(tableid);
                        dr["id"] = tableid;
                        dr["name"] = application.ShortName;
                        dr["displayname"] = application.ShortName;
                        dr["description"] = "";
                        dr["version"] = "";
                        dr["buildver"] = "";
                        dr["downloaduri"] = application.Uri;
                        break;
                    case "smc_package4out":
                        SMC_Package4OutDao poDao = new SMC_Package4OutDao(AppConfig.statisticDBKey);
                        SMC_Package4Out outEntity = poDao.Get(tableid);
                        dr["id"] = tableid;
                        dr["name"] = outEntity.Name;
                        dr["displayname"] = outEntity.DisplayName;
                        dr["description"] = outEntity.Description;
                        dr["version"] = outEntity.Version;
                        dr["buildver"] = outEntity.BuildVer;
                        dr["downloaduri"] = outEntity.DownloadUri;
                        break;
                }
            }

            return ds;
        }

        public virtual DataSet GetAndroidPadAreaPackageList(string unitCode)
        {
            DataSet ds = packageDao.GetAndroidPadAreaPackageList(unitCode);
            return ds;
            DataColumn clmn = ds.Tables[0].Columns.Add("id", typeof(int));

            clmn = ds.Tables[0].Columns.Add("name");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("displayname");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("description");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("version");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("buildver");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("downloaduri");
            clmn.DataType = typeof(string);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string tablename = dr["tablename"].ToString().ToLower();
                int tableid = Convert.ToInt32(dr["tableid"]);
                switch (tablename)
                {
                    case "package4ai":
                        Package4AIDao padao = new Package4AIDao(AppConfig.mainDbKey);
                        Package4AI entity = padao.Get(tableid);
                        dr["id"] = tableid;
                        dr["name"] = entity.Name;
                        dr["displayname"] = entity.DisplayName;
                        dr["description"] = entity.Description;
                        dr["version"] = entity.Version;
                        dr["buildver"] = entity.BuildVer;
                        dr["downloaduri"] = entity.DownloadUri;

                        break;
                    case "webapplication":
                        WebApplicationDao wad = new WebApplicationDao(AppConfig.mainDbKey);
                        WebApplication application = wad.Get(tableid);
                        dr["id"] = tableid;
                        dr["name"] = application.ShortName;
                        dr["displayname"] = application.ShortName;
                        dr["description"] = "";
                        dr["version"] = "";
                        dr["buildver"] = "";
                        dr["downloaduri"] = application.Uri;
                        break;
                    case "smc_package4out":
                        SMC_Package4OutDao poDao = new SMC_Package4OutDao(AppConfig.statisticDBKey);
                        SMC_Package4Out outEntity = poDao.Get(tableid);
                        dr["id"] = tableid;
                        dr["name"] = outEntity.Name;
                        dr["displayname"] = outEntity.DisplayName;
                        dr["description"] = outEntity.Description;
                        dr["version"] = outEntity.Version;
                        dr["buildver"] = outEntity.BuildVer;
                        dr["downloaduri"] = outEntity.DownloadUri;
                        break;
                }
            }

            return ds;
        }


        public virtual DataSet GetiPadAreaPackageList(string unitCode)
        {
            DataSet ds = packageDao.GetiPadAreaPackageList(unitCode);
            return ds;
            DataColumn clmn = ds.Tables[0].Columns.Add("id", typeof(int));

            clmn = ds.Tables[0].Columns.Add("name");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("displayname");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("description");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("version");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("buildver");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("downloaduri");
            clmn.DataType = typeof(string);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string tablename = dr["tablename"].ToString().ToLower();
                int tableid = Convert.ToInt32(dr["tableid"]);
                switch (tablename)
                {
                    case "package4ai":
                        Package4AIDao padao = new Package4AIDao(AppConfig.mainDbKey);
                        Package4AI entity = padao.Get(tableid);
                        dr["id"] = tableid;
                        dr["name"] = entity.Name;
                        dr["displayname"] = entity.DisplayName;
                        dr["description"] = entity.Description;
                        dr["version"] = entity.Version;
                        dr["buildver"] = entity.BuildVer;
                        dr["downloaduri"] = entity.DownloadUri;

                        break;
                    case "webapplication":
                        WebApplicationDao wad = new WebApplicationDao(AppConfig.mainDbKey);
                        WebApplication application = wad.Get(tableid);
                        dr["id"] = tableid;
                        dr["name"] = application.ShortName;
                        dr["displayname"] = application.ShortName;
                        dr["description"] = "";
                        dr["version"] = "";
                        dr["buildver"] = "";
                        dr["downloaduri"] = application.Uri;
                        break;
                    case "smc_package4out":
                        SMC_Package4OutDao poDao = new SMC_Package4OutDao(AppConfig.statisticDBKey);
                        SMC_Package4Out outEntity = poDao.Get(tableid);
                        dr["id"] = tableid;
                        dr["name"] = outEntity.Name;
                        dr["displayname"] = outEntity.DisplayName;
                        dr["description"] = outEntity.Description;
                        dr["version"] = outEntity.Version;
                        dr["buildver"] = outEntity.BuildVer;
                        dr["downloaduri"] = outEntity.DownloadUri;
                        break;
                }
            }

            return ds;
        }

        public virtual DataSet GetLightAppAreaPackageList(string unitCode)
        {
            DataSet ds = packageDao.GetLightAppAreaPackageList(unitCode);
            return ds;
            DataColumn clmn = ds.Tables[0].Columns.Add("id", typeof(int));

            clmn = ds.Tables[0].Columns.Add("name");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("displayname");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("description");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("version");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("buildver");
            clmn.DataType = typeof(string);
            clmn = ds.Tables[0].Columns.Add("downloaduri");
            clmn.DataType = typeof(string);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string tablename = dr["tablename"].ToString().ToLower();
                int tableid = Convert.ToInt32(dr["tableid"]);
                switch (tablename)
                {
                    case "package4ai":
                        Package4AIDao padao = new Package4AIDao(AppConfig.mainDbKey);
                        Package4AI entity = padao.Get(tableid);
                        dr["id"] = tableid;
                        dr["name"] = entity.Name;
                        dr["displayname"] = entity.DisplayName;
                        dr["description"] = entity.Description;
                        dr["version"] = entity.Version;
                        dr["buildver"] = entity.BuildVer;
                        dr["downloaduri"] = entity.DownloadUri;

                        break;
                    case "webapplication":
                        WebApplicationDao wad = new WebApplicationDao(AppConfig.mainDbKey);
                        WebApplication application = wad.Get(tableid);
                        dr["id"] = tableid;
                        dr["name"] = application.ShortName;
                        dr["displayname"] = application.ShortName;
                        dr["description"] = "";
                        dr["version"] = "";
                        dr["buildver"] = "";
                        dr["downloaduri"] = application.Uri;
                        break;
                    case "smc_package4out":
                        SMC_Package4OutDao poDao = new SMC_Package4OutDao(AppConfig.statisticDBKey);
                        SMC_Package4Out outEntity = poDao.Get(tableid);
                        dr["id"] = tableid;
                        dr["name"] = outEntity.Name;
                        dr["displayname"] = outEntity.DisplayName;
                        dr["description"] = outEntity.Description;
                        dr["version"] = outEntity.Version;
                        dr["buildver"] = outEntity.BuildVer;
                        dr["downloaduri"] = outEntity.DownloadUri;
                        break;
                }
            }

            return ds;
        }

        public virtual List<Hashtable> GetApplicationCategoryList(int maxCount)
        {
            SMC_PackageExtDao peDao = new SMC_PackageExtDao(AppConfig.mainDbKey);
            DataSet ds = peDao.GetApplicationCategoryList(maxCount);

            List<Hashtable> result = new List<Hashtable>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Hashtable hash = new Hashtable();
                    hash["categoryCode"] = dr["id"].ToString();
                    hash["categoryName"] = dr["displayname"].ToString();
                    hash["url"] = this._ctroller.Url.Content("~/home/list?cat3=" + dr["id"].ToString());
                    result.Add(hash);
                }
            }
            return result;
        }

        //public DataSet GetCollectedPackageList(string uid)
        //{
        //    SMC_PackageExtDao collectDAO = new SMC_PackageExtDao(AppConfig.statisticDBKey);
        //    DataSet ds = collectDAO.GetCollectedPackage(uid);
        //    DataColumn clmn = ds.Tables[0].Columns.Add("id", typeof(string));
        //    clmn = ds.Tables[0].Columns.Add("name", typeof(string));
        //    clmn = ds.Tables[0].Columns.Add("displayname", typeof(string));
        //    clmn = ds.Tables[0].Columns.Add("description", typeof(string));
        //    clmn = ds.Tables[0].Columns.Add("version", typeof(string));
        //    clmn = ds.Tables[0].Columns.Add("buildver", typeof(string));
        //    clmn = ds.Tables[0].Columns.Add("downloaduri", typeof(string));


        //    if (ds.Tables[0].Rows.Count > 0)
        //    {

        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            string tablename = dr["tablename"].ToString().ToLower();
        //            int tableid = Convert.ToInt32(dr["tableid"]);
        //            switch (tablename)
        //            {
        //                case "package4ai":
        //                    Package4AIDao padao = new Package4AIDao(AppConfig.mainDbKey);
        //                    Package4AI entity = padao.Get(tableid);
        //                    dr["id"] = dr["pe_id"].ToString();
        //                    dr["name"] = entity.Name;
        //                    dr["displayname"] = entity.DisplayName;
        //                    dr["description"] = entity.Description;
        //                    dr["version"] = entity.Version;
        //                    dr["buildver"] = entity.BuildVer;
        //                    dr["downloaduri"] = entity.DownloadUri;
        //                    break;
        //                case "webapplication":
        //                    WebApplicationDao wad = new WebApplicationDao(AppConfig.mainDbKey);
        //                    WebApplication application = wad.Get(tableid);
        //                    dr["id"] = dr["pe_id"].ToString();
        //                    dr["name"] = application.ShortName;
        //                    dr["displayname"] = application.ShortName;
        //                    dr["description"] = "";
        //                    dr["version"] = "";
        //                    dr["buildver"] = "";
        //                    dr["downloaduri"] = application.Uri;
        //                    break;
        //                case "smc_package4out":
        //                    SMC_Package4OutDao poDao = new SMC_Package4OutDao(AppConfig.statisticDBKey);
        //                    SMC_Package4Out outEntity = poDao.Get(tableid);
        //                    dr["id"] = dr["pe_id"].ToString();
        //                    dr["name"] = outEntity.Name;
        //                    dr["displayname"] = outEntity.DisplayName;
        //                    dr["description"] = outEntity.Description;
        //                    dr["version"] = outEntity.Version;
        //                    dr["buildver"] = outEntity.BuildVer;
        //                    dr["downloaduri"] = outEntity.DownloadUri;
        //                    break;
        //            }
        //        }
        //    }
        //    return ds;
        //}

        //public DataSet SearchBBPackageList(string keyword, string category, string unitCode, Common.Entities.Enum.PackageClientType clientType, Common.Entities.Enum.PackageTrait trait, int page, int pageSize, out int pageCount, out int recordCount)
        public virtual List<Hashtable> GetAllPackageList(SmartBox.Console.Common.Entities.Enum.PackageClientType clientType)
        {
            string sClientType = EnumTranslator.TransPackageClientTypeToString(clientType);
            IList<SMC_PackageExt> ds = packageDao.GetAllPackageList(sClientType);
            List<Hashtable> result = new List<Hashtable>();
            for (int i = 0; i < ds.Count; ++i)
            {
                Hashtable item = SmartBox.Console.Common.TypeToHashtable<SMC_PackageExt>.ToHashtable(ds[i]);
                IList<SMC_PackagePicture> pics = pictureDao.GetList(Convert.ToInt32(ds[i].pe_id));
                item["manualuri"] = ds[i].pe_id;
                if (pics != null && pics.Count > 0)
                {
                    item["picture"] = this._ctroller.Url.Content(pics[0].pp_path);
                }
                else
                    item["picture"] = this._ctroller.Url.Content("~/AppPictures/pictureno.png");

                result.Add(item);
            }
            return result;
        }

        public virtual int GetBBPackageCount()
        {
            return packageDao.GetBBPackageCount();
        }

        public virtual int GetTJPackageCount()
        {
            return packageDao.GetTJPackageCount();
        }

        public virtual int GetSCPackageCount(string uid)
        {
            return packageDao.GetSCPackageCount(uid);
        }

        public virtual DataSet SearchPackageList(string keyword, string category, string unitCode, Common.Entities.Enum.PackageClientType clientType, Common.Entities.Enum.PackageTrait trait, int page, int pageSize, out int pageCount, out int recordCount, string uid, int is_webapp)
        {
            DataSet ds = packageDao.SearchPackageList(keyword, category, unitCode, clientType, trait, page, pageSize, out pageCount, out recordCount, uid, is_webapp);

            return ds;
            /*DataColumn clmn = ds.Tables[0].Columns.Add("id", typeof(string));
            clmn = ds.Tables[0].Columns.Add("name", typeof(string));
            clmn = ds.Tables[0].Columns.Add("displayname", typeof(string));
            clmn = ds.Tables[0].Columns.Add("description", typeof(string));
            clmn = ds.Tables[0].Columns.Add("version", typeof(string));
            clmn = ds.Tables[0].Columns.Add("buildver", typeof(string));
            clmn = ds.Tables[0].Columns.Add("downloaduri", typeof(string));


            if (ds.Tables[0].Rows.Count > 0) {

                foreach (DataRow dr in ds.Tables[0].Rows) {
                    string tablename = dr["tablename"].ToString().ToLower();
                    int tableid = Convert.ToInt32(dr["tableid"]);
                    switch (tablename)
                    {
                        case "package4ai":
                            Package4AIDao padao = new Package4AIDao(AppConfig.mainDbKey);
                            Package4AI entity = padao.Get(tableid);
                            dr["id"] = dr["pe_id"].ToString();
                            dr["name"] = entity.Name;
                            dr["displayname"] = entity.DisplayName;
                            dr["description"] = entity.Description;
                            dr["version"] = entity.Version;
                            dr["buildver"] = entity.BuildVer;
                            dr["downloaduri"] = entity.DownloadUri;
                            break;
                        case "webapplication":
                            WebApplicationDao wad = new WebApplicationDao(AppConfig.mainDbKey);
                            WebApplication application = wad.Get(tableid);
                            dr["id"] = dr["pe_id"].ToString();
                            dr["name"] = application.ShortName;
                            dr["displayname"] = application.ShortName;
                            dr["description"] = "";
                            dr["version"] = "";
                            dr["buildver"] = "";
                            dr["downloaduri"] = application.Uri;
                            break;
                        case "smc_package4out":
                            SMC_Package4OutDao poDao = new SMC_Package4OutDao(AppConfig.statisticDBKey);
                            SMC_Package4Out outEntity = poDao.Get(tableid);
                            dr["id"] = dr["pe_id"].ToString();
                            dr["name"] = outEntity.Name;
                            dr["displayname"] = outEntity.DisplayName;
                            dr["description"] = outEntity.Description;
                            dr["version"] = outEntity.Version;
                            dr["buildver"] = outEntity.BuildVer;
                            dr["downloaduri"] = outEntity.DownloadUri;
                            break;
                    }
                }
            }
            return ds;*/
        }

        //public virtual Hashtable SearchList(bool isOutApp, Common.Entities.Enum.PackageClientType clientType,Common.Entities.Enum.PackageTrait trait, string keyword, string Class, string Unit, int page)
        //{
           
        //    Hashtable result = new Hashtable();
        //    //SmartBox.Console.Common.AppCenterPlatform.
        //    SMC_PackageExtDao peDao = new SMC_PackageExtDao(AppConfig.statisticDBKey);
        //    if (isOutApp)
        //    {

        //        //peDao.GetPackageList(clientType, trait);
        //    }
        //    else
        //    {
        //    }
        //    return result;
        //}

        public virtual bool PostQuestion(string question, int pe_id, string uid, string uname)
        {
            return faqDao.PostQuestion(question, pe_id, uid, uname);
        }

        public virtual bool IsCollected(int pe_id, string uid)
        {
            SMC_CollectDao colDao = new SMC_CollectDao(AppConfig.statisticDBKey);
            SMC_Collect coll = colDao.GetEntity(uid, pe_id);
            if (coll != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual int SCPost(int pe_id, string uid, string uname)
        {
            SMC_CollectDao colDao = new SMC_CollectDao(AppConfig.statisticDBKey);
            SMC_Collect coll = colDao.GetEntity(uid, pe_id);
            int result = 10;
            if (coll == null)
            {
                coll = new SMC_Collect();
                coll.pe_id = pe_id;
                coll.uid = uid;
                coll.uname = uname;
                coll.clt_CollectDate = DateTime.Now;
                try
                {
                    colDao.Insert(coll);
                    result = 11;
                }
                catch
                {
                    result = 10;
                }
            }
            else
            {
                colDao.Delete(coll);
                result = 21;
            }
            return result;
        }

        public virtual List<Hashtable> GetAppPicture(int pe_id)
        {
            IList<SMC_PackagePicture> pics = pictureDao.GetList(pe_id);
            List<Hashtable> result = new List<Hashtable>();

            if (pics != null && pics.Count > 0)
            {
                foreach (SMC_PackagePicture pic in pics)
                {
                    Hashtable hash = new Hashtable();
                    
                    hash["picture"] = this._ctroller.Url.Content(pic.pp_path);
                    result.Add(hash);
                }
            }

            //for (int i = 0; i < 7; ++i)
            //{
                //Hashtable hash = new Hashtable();
                //hash["picture"] = "p1.png";
                //result.Add(hash);

                //hash = new Hashtable();
                //hash["picture"] = "p2.png";
                //result.Add(hash);

                //hash = new Hashtable();
                //hash["picture"] = "p3.png";
                //result.Add(hash);

                //hash = new Hashtable();
                //hash["picture"] = "p3.png";
                //result.Add(hash);

                //hash = new Hashtable();
                //hash["picture"] = "p2.png";
                //result.Add(hash);

                //hash = new Hashtable();
                //hash["picture"] = "p1.png";
                //result.Add(hash);

                //hash = new Hashtable();
                //hash["picture"] = "p2.png";
                //result.Add(hash);
            //}
            return result;
        }
        public virtual List<Hashtable> GetCommentAllAppList()
        {
            SMC_PackageExtDao peDao = new SMC_PackageExtDao(AppConfig.statisticDBKey);
            //IList<SMC_PackageExt> packets = peDao.GetPackageList(SmartBox.Console.Common.Entities.Enum.PackageClientType.All, Common.Entities.Enum.PackageTrait.Tuijian);
            List<Hashtable> result = new List<Hashtable>();

            for (int i = 0; i < 7; ++i)
            {
                Hashtable hash = new Hashtable();
                hash["appName"] = "PPS影音";
                hash["appVersion"] = "android手机版";
                hash["appClass"] = "办公/政务";
                hash["appUnit"] = "上海互联网软件";
                hash["appDate"] = "2014-3-10";
                hash["appImage"] = "app"+(i+1)+".png";
                hash["appUrl"] = _ctroller.Url.Content("~/Home/Detail?packageid=5");
                result.Add(hash);
            }
            return result;
        }

        //public virtual List<Hashtable> GetCategoryList()
        //{
        //    List<Hashtable> result = new List<Hashtable>();

        //    Hashtable hash = new Hashtable();
        //    hash["categoryName"] = "视频软件";
        //    hash["url"] = "#";
        //    result.Add(hash);
            
        //    hash = new Hashtable();
        //    hash["categoryName"] = "聊天工具";
        //    hash["url"] = "#";
        //    result.Add(hash);
            
        //    hash = new Hashtable();
        //    hash["categoryName"] = "游戏娱乐";
        //    hash["url"] = "#";
        //    result.Add(hash);
            
        //    hash = new Hashtable();
        //    hash["categoryName"] = "网络游戏";
        //    hash["url"] = "#";
        //    result.Add(hash);
            
        //    hash = new Hashtable();
        //    hash["categoryName"] = "音乐软件";
        //    hash["url"] = "#";
        //    result.Add(hash); 
            
        //    hash = new Hashtable();
        //    hash["categoryName"] = "安全杀毒";
        //    hash["url"] = "#";
        //    result.Add(hash);
            
        //    hash = new Hashtable();
        //    hash["categoryName"] = "系统工具";
        //    hash["url"] = "#";
        //    result.Add(hash);
            
        //    hash = new Hashtable();
        //    hash["categoryName"] = "办公软件";
        //    hash["url"] = "#";
        //    result.Add(hash);
            
        //    hash = new Hashtable();
        //    hash["categoryName"] = "图形图像";
        //    hash["url"] = "#";
        //    result.Add(hash);
            
        //    hash = new Hashtable();
        //    hash["categoryName"] = "股票网银";
        //    hash["url"] = "#";
        //    result.Add(hash);

        //    return result;
        //}

        public virtual List<Hashtable> GetUnitList(int maxCount)
        {
            IList<SMC_Unit> units = unitDao.QueryChildUnitsByUnitID("", maxCount);
            List<Hashtable> result = new List<Hashtable>();
            foreach (SMC_Unit unit in units)
            {
                Hashtable hash = new Hashtable();
                hash["UnitName"] = unit.Unit_Name;
                hash["unitcode"] = unit.Unit_ID;
                hash["url"] = this._ctroller.Url.Content("~/home/list?unitcode=" + unit.Unit_ID);
                result.Add(hash);
            }
            

            //Hashtable hash = new Hashtable();
            //hash["UnitName"] = "浦东新区环保局";
            //hash["url"] = "#";
            //result.Add(hash);

            //hash = new Hashtable();
            //hash["UnitName"] = "浦东新区民政局";
            //hash["url"] = "#";
            //result.Add(hash);

            //hash = new Hashtable();
            //hash["UnitName"] = "浦东新区工商局";
            //hash["url"] = "#";
            //result.Add(hash);

            //hash = new Hashtable();
            //hash["UnitName"] = "浦东新区公安分局";
            //hash["url"] = "#";
            //result.Add(hash);

            //hash = new Hashtable();
            //hash["UnitName"] = "浦东新区建交委";
            //hash["url"] = "#";
            //result.Add(hash);

            return result;
        }

        public virtual Hashtable GetCommentAndroidAppList()
        {
            Hashtable hash = new Hashtable();
            return hash;
        }

        public virtual IList<SMC_PackageManual> GetPackageManuals(int packageid)
        {
            IList<SMC_PackageManual> r = manualDao.GetPackageManuals(packageid);
            return r;
        }

        public virtual bool PackageExtSyncInsideToOutside(SMC_PackageExt entity)
        {
            SMC_PackageExt ext = packageDao.Get(entity.pe_id);
            string peDownloadUrl = entity.pe_DownloadUri;
            
            if (ext == null)
            {
                packageDao.Insert(entity);                
            }
            else
            {
                packageDao.Update(entity);
            }

            if (peDownloadUrl.EndsWith(".apk"))
            {
                string applicationCenterSiteUrl = ConfigurationManager.AppSettings["applicationCenterSiteUrl"];
                string fileName = Path.GetFileName(peDownloadUrl);
                entity.pe_FileUrl = "~/PackageExt/" + entity.pe_id + "/" + fileName;
                entity.pe_DownloadUri = applicationCenterSiteUrl + "/PackageExt/" + entity.pe_id + "/" + fileName; 
                packageDao.Update(entity);

                string packUploadFolder = ConfigurationManager.AppSettings["packUploadFolder"];
                string packDestFolder = ConfigurationManager.AppSettings["attachPath"] + "\\" + entity.pe_id + "\\";
                try
                {
                    if (!Directory.Exists(packDestFolder))
                        Directory.CreateDirectory(packDestFolder);
                }
                catch (Exception ex)
                {
                    Log4NetHelper.Error(ex);
                }

                try
                {
                    string s = "";
                    if (packUploadFolder.IndexOf("$") != -1)
                    {
                        string[] folders = packUploadFolder.Split("$".ToCharArray());
                        if (folders.Length == 1)
                            s = folders[0];
                        else if (folders.Length >= 2)
                            s = folders[1];
                    }
                    else
                    {
                        s = packUploadFolder;
                    }
                    File.Copy(s + "\\" + fileName, packDestFolder + fileName, true);
                }
                catch (Exception ex)
                {
                    Log4NetHelper.Error(ex);
                }
            }

            return true;
        }

        
        public virtual bool PackageFAQSyncInsideToOutside(SMC_PackageFAQ faq)
        {

            SMC_PackageFAQ _faq = faqDao.Get(faq.pf_id);
            if (_faq == null)
            {
                faq.pf_need_syncto_inside = false;
                faqDao.Insert(faq);
            }
            else
            {
                _faq.pf_answer = faq.pf_answer;
                _faq.pf_peplyman = faq.pf_peplyman;
                _faq.pf_uid = faq.pf_uid;
                _faq.pf_uname = faq.pf_uname;
                _faq.pf_need_syncto_inside = faq.pf_need_syncto_inside;
                faqDao.Update(_faq);
            }
            return true;
        }

        
        public virtual bool PackageManualSyncInsideToOutside(SMC_PackageManual entity)
        {

            SMC_PackageManual ext = manualDao.Get(entity.pm_id);
            if (ext == null)
            {
                manualDao.Insert(entity);
            }
            else
            {
                manualDao.Update(entity);
            }
            return true;
        }

        
        public virtual bool PackagePictuerSyncInsideToOutside(SMC_PackagePicture entity)
        {

            SMC_PackagePicture ext = pictureDao.Get(entity.pp_id);
            if (ext == null)
            {
                int maxid = pictureDao.GetMaxId();
                pictureDao.Insert(entity);
                
            }
            else
            {
                pictureDao.Update(entity);
            }
            return true;
        }

        
        public virtual IList<SMC_PackageExt> GetNeedSyncPackageList()
        {

            return packageDao.GetNeedSyncPackageList();
        }

        public virtual IList<SMC_PackagePicture> GetPackagePictures(int pe_id)
        {
            return pictureDao.GetPackagePictures(pe_id);
        }

        
        public virtual bool SMC_UnitSyncInsideToOutside(List<SMC_Unit> units)
        {
            
            foreach (SMC_Unit unit in units)
            {
                SMC_Unit _unit = unitDao.Get(unit.Unit_ID);
                if (_unit == null)
                {
                    unitDao.Insert(unit);
                }
                else
                {
                    unitDao.Update(unit);
                }
            }
            return true;
        }

        
        public virtual bool SMC_UserSyncInsideToOutside(List<SMC_User> users)
        {
            
            foreach (SMC_User user in users)
            {
                SMC_User _user = userDao.Get(user.U_ID);
                if (_user == null)
                {
                    userDao.Insert(user);
                }
                else
                {
                    userDao.Update(user);
                }
            }
            return true;
        }

        public virtual IList<SMC_Unit> GetNeedSyncSMC_Units()
        {
            return unitDao.GetNeedSyncSMC_Units();
        }

        public virtual IList<SMC_User> GetNeedSyncSMC_Users()
        {
            return userDao.GetNeedSyncSMC_Users();
        }

        public virtual IList<SMC_PackageFAQ> GetPackageFAQs(int pe_id)
        {
            return faqDao.GetPackageFAQs(pe_id);
        }

        public virtual IList<SMC_PackageFAQ> GetNeedSyncToInsideFAQ()
        {
            return faqDao.GetNeedSyncToInsideFAQ();
        }

        public virtual IList<SMC_PackageFAQ> GetNeedSyncToOutsideFAQ()
        {
            return faqDao.GetNeedSyncToOutsideFAQ();
        }

        public virtual void Save(SMC_PackageExt _ext)
        {
            packageDao.Update(_ext);
        }

        public virtual JsonFlexiGridData QueryNeedImportedPackageList(PageView view)
        {
            try
            {
                Package4AIDao dao = new Package4AIDao(AppConfig.statisticDBKey);
                return dao.QueryNeedImportedPackageList(view);
            }
            catch (DalException ex)
            {
                throw new BOException("查询包的同步信息出错", ex);
            }
        }

        //public virtual void AddSMC_PackageExt(SMC_PackageExt ext)
        //{
        //    packageDao.Insert(ext);
        //}
        //public virtual void ModifySMC_PackageExt(SMC_PackageExt ext)
        //{
        //    packageDao.Update(ext);
        //}

        //public virtual void AddSMC_PackageExt(IList<SMC_PackageExt> exts)
        //{
        //    packageDao.InsertList(exts);
        //}

        public virtual bool UpdateDownCount(int pe_id)
        {
            try
            {
                SMC_PackageExt pet = packageDao.Get(pe_id);
                if (pet != null)
                {
                    pet.pe_DownCount = pet.pe_DownCount + 1;
                    packageDao.Update(pet);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (DalException ex)
            {
                throw new BOException("更新安装包的下载数量出错", ex);
            }
        }

        public virtual bool DeletePackageExt(int pe_id)
        {
            SMC_PackageExt pet = packageDao.Get(pe_id);
            if (pet == null)
            {
                Log4NetHelper.Info("DeletePackageExt调用失败,PackageExt不存在");
                return false;
            }
            int i = packageDao.Delete(pet);
            return true;
        }

        /// <summary>
        /// 将内网的插件或主程序保存至外网地址
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public virtual bool AppFileSync(byte[] content, string filename)
        {
            string packUploadFolder = ConfigurationManager.AppSettings["packUploadFolder"];
            string[] packUploadFolders = packUploadFolder.Split("$".ToCharArray());

            int i = 0;
            string firstFilePath = "";
            foreach (string s in packUploadFolders)
            {
                if (String.IsNullOrEmpty(s))
                    continue;
                if (Directory.Exists(s) == false)
                {
                    try
                    {
                        Directory.CreateDirectory(s);
                    }
                    catch (Exception ex)
                    {
                        Log4NetHelper.Error(ex);
                        return false;
                    }
                }

                try
                {
                    string filePath = "";
                    if (s.EndsWith("\\"))
                    {
                        filePath = s  + filename;
                    }
                    else
                    {
                        filePath = s + "\\" + filename;
                    }
                    if (firstFilePath == "")
                    {
                        firstFilePath = filePath;
                        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
                        {
                            fs.Write(content, 0, content.Length);
                            fs.Close();
                            fs.Dispose();
                        }
                    }
                    else
                    {
                        File.Copy(firstFilePath, filePath, true);
                    }
                }
                catch (Exception e)
                {
                    Log4NetHelper.Error(e);
                }

                ++i;
            }

           
            return true;
        }
    }
}
