using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
namespace SmartBox.Server.Service
{
    internal static class SerializeHelp
    {
        public static XmlSerializer RequestSer;
        public static XmlSerializer ResponseSer;

        public static MemoryStream SerializeRequset(Object request)
        {
            var ms = new MemoryStream();
            var ns = new XmlSerializerNamespaces();
            ns.Add("c", "http://www.beyondbit.com/smartbox/common");
            RequestSer = new XmlSerializer(request.GetType());
            RequestSer.Serialize(ms, request, ns);
            ms.Position = 0;
            return ms;
        }

        public static string SerializeRequestToXml(object request)
        {
            var stream = new MemoryStream();
            var ns = new XmlSerializerNamespaces();
            ns.Add("c", "http://www.beyondbit.com/smartbox/common");

            TextWriter writer = new StreamWriter(stream, Encoding.UTF8);

            RequestSer.Serialize(writer, request,ns);
            int count = (int)stream.Length;
            byte[] arr = new byte[count];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(arr, 0, count);
            UTF8Encoding utf = new UTF8Encoding();
            return utf.GetString(arr).Trim();
        }

        public static MemoryStream SerializeResponse(object response)
        {
            var ms = new MemoryStream();
            var ns = new XmlSerializerNamespaces();
            ns.Add("c", "http://www.beyondbit.com/smartbox/common");
            ResponseSer.Serialize(ms, response, ns);
            ms.Position = 0;
            return ms;
        }
    }
}
