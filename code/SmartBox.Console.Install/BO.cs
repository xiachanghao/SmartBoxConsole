using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SmartBox.Console.Install
{
    public class BO
    {
        public BO(TextBox tbDataSource, TextBox tbUID, TextBox tbPassword, TextBox tbSmartBox, TextBox tbSmartBoxApp, TextBox tbSmartBoxAppOut)
        {
            this.tbDataSource = tbDataSource;
            this.tbUID = tbUID;
            this.tbPassword = tbPassword;
            this.tbSmartBox = tbSmartBox;
            this.tbSmartBoxApp = tbSmartBoxApp;
            this.tbSmartBoxAppOut = tbSmartBoxAppOut;

            this.connectionStr = this.BuildConnectionString(this.tbSmartBox);
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(this.connectionStr);
            this.Connection = conn;
        }

        public TextBox tbDataSource;
        public TextBox tbUID;
        public TextBox tbPassword;
        public TextBox tbSmartBox;
        public TextBox tbSmartBoxApp;
        public TextBox tbSmartBoxAppOut;
        public SqlConnection Connection;

        public string connectionStr;

        private SqlConnection GetConnection(string ConnStr)
        {
            SqlConnection cn = null;
            cn = new SqlConnection(ConnStr);
            return cn;
        }

        public bool DataBaseExists(string databaseName)
        {
            SqlConnection cn = null;
            try {
                string[] arrs = this.connectionStr.Split(";".ToCharArray());
                string connS = "";
                foreach (string s in arrs)
                {
                    if (s.ToLower().IndexOf("initial catalog") == -1)
                    {
                        connS += s + ";";
                    }
                    else
                    {
                        connS += "Initial Catalog=master;";
                    }
                }
                cn = new SqlConnection(connS);
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = String.Format("select [name] from sys.databases where name='{0}'", databaseName);
                object o = cmd.ExecuteScalar();
                if (o != null && o.ToString() != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally {
                cn.Close();
            }
            
        }

        public bool CreateDatabase(string databaseName)
        {
            SqlConnection cn = null;
            try
            {
                string[] arrs = this.connectionStr.Split(";".ToCharArray());
                string connS = "";
                foreach (string s in arrs)
                {
                    if (s.ToLower().IndexOf("initial catalog") == -1)
                    {
                        connS += s + ";";
                    }
                    else
                    {
                        connS += "Initial Catalog=master;";
                    }
                }
                cn = new SqlConnection(connS);
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = String.Format("create database {0}", databaseName);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                cn.Close();
            }
        }

        public void SetInitialData()
        {
            string connStr = this.BuildConnectionString(this.tbSmartBox);
            SqlConnection conn = this.GetConnection(connStr);

            string path = System.Environment.CurrentDirectory;
            string sqlPathSmartBox = path + "\\SmartBoxData.Sql";

            //System.Windows.Forms.MessageBox.Show(sqlPathSmartBox);

            StreamReader sr = new StreamReader(sqlPathSmartBox);
            string smartBoxSqlContent = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            smartBoxSqlContent = smartBoxSqlContent.Replace("[SmartBox]", "[" + this.tbSmartBox.Text + "]");

            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = smartBoxSqlContent.Replace("\r\n", ";").Replace("GO", "");
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
            }
            finally
            {
                conn.Close();
            }
        }

        public void SetInitialSmartBoxAppData()
        {
            string connStr = this.BuildConnectionString(this.tbSmartBoxApp);
            SqlConnection conn = this.GetConnection(connStr);

            string path = System.Environment.CurrentDirectory;
            string sqlPathSmartBox = path + "\\SmartBoxAppData.Sql";

            //System.Windows.Forms.MessageBox.Show(sqlPathSmartBox);

            StreamReader sr = new StreamReader(sqlPathSmartBox);
            string smartBoxSqlContent = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            smartBoxSqlContent = smartBoxSqlContent.Replace("[SmartBoxApp]", "[" + this.tbSmartBoxApp.Text + "]");

            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = smartBoxSqlContent.Replace("\r\n", ";").Replace("GO", "");
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
            }
            finally
            {
                conn.Close();
            }
        }

        public void InstallSmartBoxDatabase()
        {
            try
            {
                string path = System.Environment.CurrentDirectory;
                string sqlPathSmartBox = path + "\\SmartBox.Sql";

                //System.Windows.Forms.MessageBox.Show(sqlPathSmartBox);

                StreamReader sr = new StreamReader(sqlPathSmartBox);
                string smartBoxSqlContent = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
                smartBoxSqlContent = smartBoxSqlContent.Replace("[SmartBox]", "[" + this.tbSmartBox.Text + "]");

                bool databaseExists = this.DataBaseExists(this.tbSmartBox.Text);
                if (!databaseExists)
                {
                    this.CreateDatabase(this.tbSmartBox.Text);
                }

                try
                {
                    Connection.Open();
                    SqlCommand cmd = Connection.CreateCommand();
                    cmd.CommandText = smartBoxSqlContent.Replace("\r\nGO", ";");
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Log4NetHelper.Error(e);
                }
                finally
                {
                    Connection.Close();
                }
                
            }
            catch (Exception e)
            {
                Log4NetHelper.Error(e);
            }
        }

        public string BuildConnectionString(TextBox tbInitialCatalog)
        {
            string connStr = "Data Source={0};Uid={1};Pwd={2};Initial Catalog={3}";
            return String.Format(connStr, this.tbDataSource.Text, this.tbUID.Text, this.tbPassword.Text, tbInitialCatalog.Text);
        }
    }
}
