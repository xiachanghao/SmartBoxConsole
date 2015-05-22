using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Beyondbit.Push.Service
{
    [ServiceContract(CallbackContract = typeof(IPushCallback))]
    public interface IPushService
    {
        [OperationContract]
        void Push(string applicationId, PlatformType? platformType, List<string> uids, Payload payload);

        [OperationContract]
        void Regist(string applicationId, PlatformType platformType, string uid, string subscriptionId);

        [OperationContract]
        void Discard(string applicationId, PlatformType platformType, string subscriptionId);
    }

    public interface IPushCallback
    {
        [OperationContract(IsOneWay = true)]
        void NotificationSent(string id);

        [OperationContract(IsOneWay = true)]
        void NotificationFailed(string id, Exception ex);
    }

    [DataContract]
    public class Payload
    {
        string id;
        string alert;
        int? badge;
        string sound;

        [DataMember]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Alert
        {
            get { return alert; }
            set { alert = value; }
        }

        [DataMember]
        public int? Badge
        {
            get { return badge; }
            set { badge = value; }
        }

        [DataMember]
        public string Sound
        {
            get { return sound; }
            set { sound = value; }
        }

        /// <summary>
        /// Json对象字符串
        /// </summary>
        [DataMember]
        public string Custom
        {
            get;
            set;
        }
    }

    public enum PlatformType
    {
        Apple,
        Beyondbit,
        Google,
        Windows,
        WindowsPhone
    }

    public class PushContext
    {
        public Beyondbit.Push.Service.Bo.DeviceSubscription Subscription { get; set; }
        public Payload Payload { get; set; }
        public IPushCallback Callback { get; set; }
    }
}
