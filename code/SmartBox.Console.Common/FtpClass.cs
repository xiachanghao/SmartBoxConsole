using System;
using System.Data;
using System.Web;
using FtpSupport;
using System.Configuration;
using System.IO;
using System.Net;


namespace SmartBox.Console.Common
{
	/// <summary>
	/// FtpClass 的摘要说明。
	/// </summary>
	public class FtpClass
	{
		/// <summary>
		/// 
		/// </summary>
		public FtpClass()
		{
			
		}

		private string FtpIP=ConfigurationSettings.AppSettings["FtpIP"];
		private string FtpUserName=ConfigurationSettings.AppSettings["FtpUserName"];
		private string FtpPassord=ConfigurationSettings.AppSettings["FtpPassWord"];
		private FtpConnection ftp;
		
		/// <summary>
		/// 连接Ftp
		/// </summary>
		/// <returns></returns>
		private FtpConnection FtpConn()
		{
			ftp=new FtpConnection();
			ftp.Connect(this.FtpIP,this.FtpUserName,this.FtpPassord);
			return ftp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="filedownloadname"></param>
		/// <param name="dir"></param>
		/// <returns></returns>
		public string FtpStream(HttpContext context,string filedownloadname,string dir)
		{
			string stream=string.Empty;
			FtpConnection ftp=this.FtpConn();
			ftp.SetCurrentDirectory(dir);
			
			//ftp.
			
			try
			{
				if(ftp.FileExist(filedownloadname))
				{
				
					FtpStream ftpfs=ftp.OpenFile(filedownloadname,GenericRights.Read);
				
					stream="<";
					StreamReader reader=new StreamReader(ftpfs);
				
					while(reader.Read()>0)
					{
						stream +=reader.ReadToEnd();
					}
				}
				else
				{
					context.Response.Write("<script>alert('file does not exist!');</script>");
				}
			}
			finally
			{
				ftp.Close();
			}
			return stream;
		}

		/// <summary>
		///	删除文件
		/// </summary>
		/// <param name="fileName"></param>
		public void DeleteFile(string fileName)
		{
			FtpConnection ftp=this.FtpConn();
			try
			{
				ftp.DeleteFile(fileName);
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				ftp.Close();
			}
		}


		/// <summary>
		/// 读取读取配置文件
		/// </summary>
		/// <returns></returns>
		private string GetProcEventAttachPath()
		{
			string oProcEventAttachPath = string.Empty;
			try
			{
				oProcEventAttachPath = ConfigurationSettings.AppSettings["ProcEventAttachPath"].ToString();
			}
			catch(Exception ex)
			{
				throw new Exception("读取配置文件失败!",ex);
			}
			return oProcEventAttachPath;
		}


        
        /// <summary>
        /// 通过Ftp拷贝外网的文件
        /// </summary>
        /// <param name="Psa_attachname">文件的名称</param>
        /// <param name="Psa_file_Size">文件的字节数</param>
        /// <param name="AttachPath">存放文件的路径[~/AffAirDirInfo/20080202/082600043/]~为服务器的物理路径</param>
        /// <param name="oYearDim"></param>
        /// <returns>拷贝文件是否成功</returns>
		public bool CopyFile(string Psa_attachname ,int Psa_file_Size , string AttachPath , string oYearDim)
		{
			bool isPass = false;

			if(Psa_attachname == string.Empty || Psa_file_Size <0)
			{
				return false;
			}
			try
			{
				using(FtpConnection ftp=this.FtpConn())
				{
					byte[] bytes = new byte[Psa_file_Size];
					//string oProcEventAttachPath = GetProcEventAttachPath();
					string oProcEventAttachPath = oYearDim+"/";
					string fileUrl =oProcEventAttachPath + Psa_attachname;
					//ftp.SetCurrentDirectory("/");
					if(ftp.FileExist(fileUrl) )
					{
						try
						{	
							//ftp.SetCurrentDirectory(oProcEventAttachPath);
							FtpStream ftpfs=ftp.OpenFile(fileUrl,GenericRights.Read);
					
							//FtpStream ftpfs=ftp.OpenFile(Psa_attachname,GenericRights.Read);

							Stream oRtream  = (Stream) ftpfs;
							FileStream fs = new FileStream(AttachPath + Psa_attachname, FileMode.Create, FileAccess.Write);
							//int index = oRtream.Read(bytes,0,Psa_file_Size);
							int count = oRtream.Read(bytes,0,10240);
						
	//						HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create("http://211.144.95.130/pdhb.synadminweb/AffAirDirInfo/"+fileUrl);
	//						HttpWebResponse webResponse = (HttpWebResponse) webRequest.GetResponse();
	//						Stream oRtream = webResponse.GetResponseStream();
	//						//int index = oRtream.Read(bytes,0,Psa_file_Size);
	//						FileStream fs = new FileStream(AttachPath + Psa_attachname, FileMode.Create, FileAccess.Write);
	//						int count = oRtream.Read( bytes, 0, 1024 );
							fs.Write(bytes, 0, count);
							while (count > 0) 
							{
								// Dump the 256 characters on a string and display the string onto the console.		
								
								count = oRtream.Read(bytes, 0, 10240);
								fs.Write(bytes, 0, count);
							}
							

							oRtream.Close();
	//						webResponse.Close(); 

							
							
							fs.Flush();
							fs.Close();

							

							isPass = true;
						}
						catch(Exception ex)
						{
							throw new Exception("FTP读取文件失败原因:"+ex.Message);
						}
					}
//					else
//					{
//						throw new Exception("文件不存在!");
//					}
				}

			}
			catch(Exception ex)
			{
				throw new Exception("保存文件信息失败!请查看Ftp连接是否成功！原因："+ex.Message,ex);
			}
			finally
			{
				ftp.Close();
			}

			return isPass;

		}
	}
}
