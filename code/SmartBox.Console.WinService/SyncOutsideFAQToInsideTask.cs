using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartBox.Console.Common.Entities;

namespace SmartBox.Console.WinService
{
    public class SyncOutsideFAQToInsideTask : WinService.WinService<SMC_PackageFAQ>
    {
        //List<SMC_PackageFAQ> packages = null;
        Service.ApplicationCenterWS.SMC_PackageFAQ[] arr = null;
        SmartBox.Console.Service.ApplicationCenterWS.WebService ws = null;
        public SyncOutsideFAQToInsideTask()
        {
            ws = new Service.ApplicationCenterWS.WebService();
        }

        public override List<SMC_PackageFAQ> GetDataSource()
        {
            //IList<Tuple<string, string, object>> pars = new List<Tuple<string, string, object>>();
            //Tuple<string, string, object> par = new Tuple<string, string, object>("pe_authstatus", "=", 0);//已通过审核
            //pars.Add(par);

            //Tuple<string, string, object> par2 = new Tuple<string, string, object>("pe_id", "=", 75);//未锁定
            //pars.Add(par2);

            //Tuple<string, string, object> par3 = new Tuple<string, string, object>("u_need_sync", "=", 0);//不需要同步的，也就是还没有比较的
            //pars.Add(par3);

            //packages = Bo.BoFactory.GetSMC_PackageFAQBO.QueryList(pars);

            try
            {
                arr = ws.GetNeedSyncToInsideFAQ();
            }
            catch (Exception ex)
            {
                SmartBox.Console.Common.Log4NetHelper.Error(ex);
            }
            
            return null;
        }

        public override void Execute()
        {
            if (arr != null && arr.Length > 0)
            {
                SmartBox.Console.Common.Log4NetHelper.Info("同步外网应用中心问题反馈至内网开始！");
                foreach (Service.ApplicationCenterWS.SMC_PackageFAQ faq in arr)
                {
                    
                    //Service.ApplicationCenterWS.SMC_PackageFAQ faq = new Service.ApplicationCenterWS.SMC_PackageFAQ();
                    //faq.pe_id = ext.pe_id;
                    //faq.pf_answer = ext.pf_answer;
                    //faq.pf_askdate = ext.pf_askdate;
                    //faq.pf_askemail = ext.pf_askemail;
                    //faq.pf_askmobile = ext.pf_askmobile;
                    //faq.pf_id = ext.pf_id;
                    //faq.pf_peplyman = ext.pf_peplyman;
                    //faq.pf_question = ext.pf_question;
                    //faq.pf_uid = ext.pf_uid;
                    //faq.pf_uname = ext.pf_uname;


                    SMC_PackageFAQ entity = new SMC_PackageFAQ();
                    entity.pe_id = faq.pe_id;
                    entity.pf_answer = faq.pf_answer;
                    entity.pf_askdate = faq.pf_askdate;
                    entity.pf_askemail = faq.pf_askemail;
                    entity.pf_askmobile = faq.pf_askmobile;
                    entity.pf_id = faq.pf_id;
                    entity.pf_need_syncto_inside = faq.pf_need_syncto_inside;
                    entity.pf_need_syncto_outside = faq.pf_need_syncto_outside;
                    entity.pf_peplyman = faq.pf_peplyman;
                    entity.pf_question = faq.pf_question;
                    entity.pf_uid = faq.pf_uid;
                    entity.pf_uname = faq.pf_uname;

                    try
                    {
                        //ws.PackageFAQSync(faq);
                        IList<KeyValuePair<string, object>> pars = new List<KeyValuePair<string, object>>();
                        pars.Add(new KeyValuePair<string,object>("pf_id", entity.pf_id));

                        SMC_PackageFAQ faq_inside = Bo.BoFactory.GetSMC_PackageFAQBO.Get(pars);
                        //更新到内网
                        if (faq_inside != null)
                        {
                            Bo.BoFactory.GetSMC_PackageFAQBO.Update(faq_inside);
                        }
                        else
                        {
                            //faq_inside = new SMC_PackageFAQ();
                            //faq_inside.pe_id = faq.pe_id;
                            //faq_inside.pf_answer = faq.pf_answer;
                            //faq_inside.pf_askdate = faq.pf_askdate;
                            //faq_inside.pf_askemail = faq.pf_askemail;
                            //faq_inside.pf_askmobile = faq.pf_askmobile;
                            //faq_inside.pf_id = faq.pf_id;
                            //faq_inside.pf_need_syncto_inside = faq.pf_need_syncto_inside;
                            //faq_inside.pf_need_syncto_outside = faq.pf_need_syncto_outside;
                            //faq_inside.pf_peplyman = faq.pf_peplyman;
                            //faq_inside.pf_question = faq.pf_question;
                            //faq_inside.pf_uid = faq.pf_uid;
                            //faq_inside.pf_uname = faq.pf_uname;

                            Bo.BoFactory.GetSMC_PackageFAQBO.Insert(entity);
                        }
                    }
                    catch (Exception ex)
                    {
                        SmartBox.Console.Common.Log4NetHelper.Error(ex);
                    }

                    faq.pf_need_syncto_inside = false;
                    try
                    {
                        //更新到外网
                        ws.PackageFAQSync(faq);
                    }
                    catch (Exception ex)
                    {
                        SmartBox.Console.Common.Log4NetHelper.Error(ex);
                    }
                    
                    //Bo.BoFactory.GetCommonBO.SMC_PackageExtInternalRelease(ext);
                }
                SmartBox.Console.Common.Log4NetHelper.Info("同步外网应用中心问题反馈至内网结束！");
            }
            else
            {
                SmartBox.Console.Common.Log4NetHelper.Info("未查询到需要同步至内网的问题反馈，将不执行外网应用中心问题反馈至内网的同步任务！");
            }
        }
    }
}
