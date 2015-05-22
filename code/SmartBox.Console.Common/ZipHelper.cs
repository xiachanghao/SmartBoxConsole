using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.IO.Packaging;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;

namespace SmartBox.Console.Common
{
    public class ZipHelper
    {
        /// <summary>
        /// Create a zip archive.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="directory">The directory to zip.</param> 
        public static void PackFiles(string filename, string directory)
        {
            try
            {
                FastZip fz = new FastZip();
                fz.CreateEmptyDirectories = true;
                fz.CreateZip(filename, directory, true, "");
                fz = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Unpacks the files.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>if succeed return true,otherwise false.</returns>
        public static bool UnpackFiles(Stream stream, string dir)
        {
            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                ZipInputStream s = new ZipInputStream(stream);

                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    if (directoryName != String.Empty)
                        Directory.CreateDirectory(dir + directoryName);

                    if (fileName != String.Empty)
                    {
                        FileStream streamWriter = File.Create(dir + theEntry.Name);

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }

                        streamWriter.Close();
                    }
                }
                s.Close();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}