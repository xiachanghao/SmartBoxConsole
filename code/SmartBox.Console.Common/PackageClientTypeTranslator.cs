using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartBox.Console.Common
{
    public class EnumTranslator
    {
        public static string TransPackageClientTypeToString(Common.Entities.Enum.PackageClientType packageClientType)
        {
            string sClientType = "";
            switch (packageClientType)
            {
                case Common.Entities.Enum.PackageClientType.androidPad:
                    sClientType = "Pad/Android";
                    break;
                case Common.Entities.Enum.PackageClientType.androidPhone:
                    sClientType = "Phone/Android";
                    break;
                case Common.Entities.Enum.PackageClientType.iPad:
                    sClientType = "Pad/iOS";
                    break;
                case Common.Entities.Enum.PackageClientType.iPhone:
                    sClientType = "Phone/iOS";
                    break;
                case Common.Entities.Enum.PackageClientType.web:
                    sClientType = "web";
                    break;
            }
            return sClientType;
        }

        public static Common.Entities.Enum.PackageClientType TransPackageClientTypeToString(string sClientType)
        {
            Common.Entities.Enum.PackageClientType packageClientType = Entities.Enum.PackageClientType.iPhone;
            switch (sClientType)
            {
                case "Pad/Android":
                    packageClientType = Common.Entities.Enum.PackageClientType.androidPad;
                    break;
                case "Phone/Android":
                    packageClientType = Common.Entities.Enum.PackageClientType.androidPhone;
                    break;
                case "Pad/iOS":
                    packageClientType = Common.Entities.Enum.PackageClientType.iPad;
                    break;
                case "Phone/iOS":
                    packageClientType = Common.Entities.Enum.PackageClientType.iPhone;
                    break;
                case "web":
                    packageClientType = Common.Entities.Enum.PackageClientType.web;
                    break;
            }
            return packageClientType;
        }

        //public static string TransPackageTraitToString(Common.Entities.Enum.PackageTrait packageTrait)
        //{
        //    string sTrait = "";
        //    switch (packageTrait)
        //    {
        //        case Common.Entities.Enum.PackageTrait.Bibei:
        //            sTrait = "Pad/Android";
        //            break;
        //        case Common.Entities.Enum.PackageClientType.androidPhone:
        //            sTrait = "Phone/Android";
        //            break;
        //        case Common.Entities.Enum.PackageClientType.iPad:
        //            sTrait = "Pad/iOS";
        //            break;
        //        case Common.Entities.Enum.PackageClientType.iPhone:
        //            sTrait = "Phone/iOS";
        //            break;
        //        case Common.Entities.Enum.PackageClientType.web:
        //            sTrait = "web";
        //            break;
        //    }
        //    return sClientType;
        //}
    }
}
