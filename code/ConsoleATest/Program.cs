using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common;

namespace ConsoleATest
{
    class Program
    {
        static void Main(string[] args)
        {
         
            var xmlConfig = CommonMethods.GetEntityFromXML(
                        @"D:\MyWork\VSS\百宝箱\CreatePluginConfigXml\CreatePluginConfigXml\Plugin.config");
            var a = xmlConfig;


            CommonMethods.WritePluginfoConfigXml(a,@"d:\myXml.xml");
            Console.WriteLine(a);
            Console.ReadKey();
        
            
        }
    }
}
