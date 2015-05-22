using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework;
using Beyondbit.Framework.Core.Proxy;
using SmartBox.Console.Bo;
using SmartBox.Console.Bo.AppCenter;
using SmartBox.Console.Common.Entities;
using System.IO;

namespace SyncFaqConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationCenterWS.WebServiceSoapClient client = new ApplicationCenterWS.WebServiceSoapClient();
            ApplicationCenterWS.SMC_PackageFAQ[] outFaqs = client.GetNeedSyncToInsideFAQ();
                       
            //外网提问写入内网
            foreach (var f in outFaqs)
            {
                SMC_PackageFAQ faq = new SMC_PackageFAQ();
                faq.pe_id = f.pe_id;
                faq.pf_answer = f.pf_answer;
                faq.pf_askdate = f.pf_askdate;
                faq.pf_askemail = f.pf_askemail;
                faq.pf_askmobile = f.pf_askmobile;
                faq.pf_id = f.pf_id;
                faq.pf_need_syncto_inside = f.pf_need_syncto_inside;
                faq.pf_need_syncto_outside = f.pf_need_syncto_outside;
                faq.pf_peplyman = f.pf_peplyman;
                faq.pf_question = f.pf_question;
                faq.pf_uid = f.pf_uid;
                faq.pf_uname = f.pf_uname;

                BoFactory.GetVersionTrackBo.InsertPackageFAQ(faq);

            }


            //内网回复同步至外网
            IProxy proxy = ProxyFactory.CreateProxy(); 
            SmartBox.Console.Bo.AppCenter.AppCenterBO bo = null;               
            bo = proxy.CreateObject<SmartBox.Console.Bo.AppCenter.AppCenterBO>();
       
            IList<SMC_PackageFAQ> inFaqs = bo.GetNeedSyncToOutsideFAQ();
            foreach (var f in inFaqs)
            {
                ApplicationCenterWS.SMC_PackageFAQ faq = new ApplicationCenterWS.SMC_PackageFAQ();
                faq.pe_id = f.pe_id;
                faq.pf_answer = f.pf_answer;
                faq.pf_askdate = f.pf_askdate;
                faq.pf_askemail = f.pf_askemail;
                faq.pf_askmobile = f.pf_askmobile;
                faq.pf_id = f.pf_id;
                faq.pf_need_syncto_inside = f.pf_need_syncto_inside;
                faq.pf_need_syncto_outside = f.pf_need_syncto_outside;
                faq.pf_peplyman = f.pf_peplyman;
                faq.pf_question = f.pf_question;
                faq.pf_uid = f.pf_uid;
                faq.pf_uname = f.pf_uname;

                client.PackageFAQSync(faq);
            }
        }
    }
}
