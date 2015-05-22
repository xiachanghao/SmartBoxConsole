using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.BUA.Client;
using Beyondbit.Msg.ReceiveService.Client.MessageService;
using Beyondbit.Msg.ReceiveService.Client.Model;
using Beyondbit.Msg.ReceiveService.Client.Common;
using System.Configuration;

namespace SmartBox.Console.Reminder
{
    public class Reminder
    {
        public void RemindByEmail(string emailList, string senderAccount, string msgTitle, string msgContent)
        {

            if (String.IsNullOrEmpty(emailList))
                return;
            string[] emails = emailList.Split(",".ToCharArray());
            StringBuilder b = new StringBuilder();
            foreach (string s in emails)
            {
                string[] arr = s.Split("@".ToCharArray());
                if (arr != null && arr.Length > 1)
                {
                    b.Append(arr[0]);
                    b.Append(",");
                }
            }

            string ss = b.ToString();
            if (String.IsNullOrEmpty(ss))
                return;
            RemindByAccount(ss, senderAccount, msgTitle, msgContent);
        }

        public void RemindByAccount(string accountList, string senderAccount, string msgTitle, string msgContent)
        {
            if (String.IsNullOrEmpty(senderAccount))
            {
                string msg_sender_account = ConfigurationManager.AppSettings["msg_sender_account"];
                if (!String.IsNullOrEmpty(msg_sender_account))
                    senderAccount = msg_sender_account;
            }
            string[] arr = accountList.Split(",".ToCharArray());
            StringBuilder b = new StringBuilder();

            IUserService userSvc = Beyondbit.BUA.Client.ServiceFactory.Instance().GetUserService();
            foreach (string account in arr)
            {
                if (String.IsNullOrEmpty(account))
                    continue;

                //UserPersonal person = userSvc.GetUserPersonalInfo(account);
                //if (person != null && !String.IsNullOrEmpty(person.UserMobilePhone))
                //{
                //    b.Append(person.UserMobilePhone);
                //    b.Append(",");
                //}
                string[] arrAccount = account.Split("@".ToCharArray());
                b.Append(arrAccount[0]);
                b.Append(",");
            }

            string ss = b.ToString();
            if (String.IsNullOrEmpty(ss))
                return;
            RemindByMobile(ss, senderAccount, msgTitle, msgContent);
        }

        public void RemindByMobile(string mobileList, string senderAccount, string msgTitle, string msgContent)
        {
            ClientConfig config = ClientConfig.Instance();
            SmartBox.Console.Service.msg.MsgService svc = new Service.msg.MsgService();
            svc.Url = config.ServiceUrl;// "http://msgservice.huangpuqu.sh.cn/WebService/MsgService.asmx";
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?><msg:Request xmlns:req=""http://www.beyondbit.com/msg/sao/domains/request400001"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:msg=""http://www.beyondbit.com/msg""><msg:Head><msg:TrCode>400001</msg:TrCode><msg:ApplicationCode>Smartbox</msg:ApplicationCode><msg:ApplicationPassword>123456</msg:ApplicationPassword><msg:UserUid>__sao__</msg:UserUid><msg:UserPassword>App1234</msg:UserPassword><msg:ClientTxSeq>4b46d233817248c098e9d394395ed942</msg:ClientTxSeq><msg:MacCode /></msg:Head><msg:Body xsi:type=""req:RequestBody400001""><req:SenderAccount>wxiaolei</req:SenderAccount><req:ReceiverAccount>fangbin</req:ReceiverAccount><req:ReciverAccountType>USER</req:ReciverAccountType><req:MsgType>DEFAULT</req:MsgType><req:SendType>SMS</req:SendType><req:Group>0</req:Group><req:Priority>0</req:Priority><req:Level>L</req:Level><req:MsgTitle>设备审核通过</req:MsgTitle><req:MsgContent>设备审核通过，您已经可以使用移动政务平台了！</req:MsgContent><req:MsgTemplate>DEFAULT</req:MsgTemplate><req:SendMode>NOW</req:SendMode><req:DelayType>0</req:DelayType><req:DelaySecond>10</req:DelaySecond><req:RepeatStartTime>0001-01-01T00:00:00</req:RepeatStartTime><req:RepeatInterval>10</req:RepeatInterval><req:MsgBatchNo>3c658279356f4ec29fd25d5a5526ba2b</req:MsgBatchNo></msg:Body></msg:Request>";
            if (String.IsNullOrEmpty(senderAccount))
            {
                string msg_sender_account = ConfigurationManager.AppSettings["msg_sender_account"];
                if (!String.IsNullOrEmpty(msg_sender_account))
                    senderAccount = msg_sender_account;
            }

            if (!string.IsNullOrEmpty(mobileList))
            {
                mobileList = mobileList.TrimEnd(',');
            }
            
            
            xml = @"<?xml version=""1.0"" encoding=""utf-8""?><msg:Request xmlns:req=""http://www.beyondbit.com/msg/sao/domains/request400001"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:msg=""http://www.beyondbit.com/msg""><msg:Head><msg:TrCode>400001</msg:TrCode><msg:ApplicationCode>{0}</msg:ApplicationCode><msg:ApplicationPassword>{1}</msg:ApplicationPassword><msg:UserUid>{2}</msg:UserUid><msg:UserPassword>{3}</msg:UserPassword><msg:ClientTxSeq>{4}</msg:ClientTxSeq><msg:MacCode /></msg:Head><msg:Body xsi:type=""req:RequestBody400001""><req:SenderAccount>{5}</req:SenderAccount><req:ReceiverAccount>{6}</req:ReceiverAccount><req:ReciverAccountType>{7}</req:ReciverAccountType><req:MsgType>{8}</req:MsgType><req:SendType>{9}</req:SendType><req:Group>{10}</req:Group><req:Priority>{11}</req:Priority><req:Level>{12}</req:Level><req:MsgTitle>{13}</req:MsgTitle><req:MsgContent>{14}</req:MsgContent><req:MsgTemplate>{15}</req:MsgTemplate><req:SendMode>{16}</req:SendMode><req:DelayType>{17}</req:DelayType><req:DelaySecond>{18}</req:DelaySecond><req:RepeatStartTime>{19}</req:RepeatStartTime><req:RepeatInterval>{20}</req:RepeatInterval><req:MsgBatchNo>{21}</req:MsgBatchNo></msg:Body></msg:Request>";
            xml = String.Format(xml, config.ApplicationCode, config.ApplicationPassword, config.UserUid, config.UserPassword, Guid.NewGuid().ToString().Replace("-", string.Empty), senderAccount, mobileList, 
                "USER", "DEFAULT", "SMS", "0", "0",
                "L", msgTitle, msgContent, "DEFAULT", "NOW",
                "0", "10", "0001-01-01T00:00:00", "10", System.Guid.NewGuid().ToString().Replace("-", ""));
            svc.Manage(xml);
            //return;
            

            //IMessageService messageService = MessageServiceFactory.Instance().GetMessageService();
            //AppMessageInfo msg = new AppMessageInfo();
            
            //msg.AppCode = config.ApplicationCode;
            //msg.AppPwd = config.ApplicationPassword;
            //msg.SendType = "SMS";
            ////msg.MsgType = "DEFAULT";
            //msg.MsgTemplate = "DEFAULT";
            //msg.MsgBatchNo = System.Guid.NewGuid().ToString().Replace("-", "");
            //msg.Level = "L";
            //msg.Priority = 0;
            //msg.ReciverAccountType = "USER";//REALLYACCOUNT
            
            //msg.MsgTitle = msgTitle;
            //msg.MsgContent = msgContent;
            //msg.SendMode = "NOW";
            //msg.DelaySecond = 10;
            //msg.RepeatInterval = 10;
            //msg.Group = 0;
            //msg.SenderAccount = senderAccount;
            //msg.ClientTxSeq = Guid.NewGuid().ToString().Replace("-", string.Empty);
            //messageService.SendMsgFromApp(msg);
        }

        //public void RemindByMobile(string mobileList, string senderAccount, string msgTitle, string msgContent)
        //{
        //    if (String.IsNullOrEmpty(senderAccount))
        //    {
        //        string msg_sender_account = ConfigurationManager.AppSettings["msg_sender_account"];
        //        if (!String.IsNullOrEmpty(msg_sender_account))
        //            senderAccount = msg_sender_account;
        //    }
        //    Beyondbit.MsgSync.Recieve.SendMsg _msg = new Beyondbit.MsgSync.Recieve.SendMsg();
            

        //    //IMessageService messageService = MessageServiceFactory.Instance().GetMessageService();
        //    //AppMessageInfo msg = new AppMessageInfo();
            
        //    Beyondbit.MsgSync.Entity.MsgInfo msginfo = new Beyondbit.MsgSync.Entity.MsgInfo();
        //    msginfo.AppCode = "";
            
        //    ClientConfig config = ClientConfig.Instance();
        //    msginfo.AppCode = config.ApplicationCode;
        //    msginfo.AppPwd = config.ApplicationPassword;
        //    msginfo.SendType = "SMS";
        //    //msg.MsgType = "DEFAULT";
        //    msginfo.MsgTemplate = "DEFAULT";
        //    //msginfo.MsgBatchNo = System.Guid.NewGuid().ToString().Replace("-", "");
        //    msginfo.Level = "L";
        //    msginfo.Priority = 0;
        //    msginfo.ReciverAccountType = "USER";//REALLYACCOUNT
        //    if (!string.IsNullOrEmpty(mobileList))
        //    {
        //        msginfo.ReceiverAccount = mobileList.TrimEnd(',');
        //    }
        //    msginfo.MsgTitle = msgTitle;
        //    msginfo.MsgContent = msgContent;
        //    msginfo.SendMode = "NOW";
        //    msginfo.DelaySecond = 10;
        //    msginfo.RepeatInterval = 10;
        //    msginfo.Group = 0;
        //    msginfo.SenderAccount = senderAccount;
        //    msginfo.ClientTxSeq = Guid.NewGuid().ToString().Replace("-", string.Empty);
        //    //messageService.SendMsgFromApp(msg);
        //    _msg.SendMsgFromApp(msginfo);
        //}
    }
}
