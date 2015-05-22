using Beyondbit.Push.Service.Bo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PushSharp;
using PushSharp.Apple;
using PushSharp.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Beyondbit.Push.Service
{
    public class PushService : IPushService
    {
        public void Push(string applicationId, PlatformType? platformType, List<string> uids, Payload payload)
        {
            if (string.IsNullOrWhiteSpace(applicationId))
                throw new ArgumentNullException("applicationId");

            if (uids == null || uids.Count == 0)
                return;

            foreach (string uid in uids)
            {
                foreach (var subscription in PushBo.Instance.QueryDeviceSubscriptions(applicationId, platformType, uid))
                {
                    Push(subscription, payload);
                }
            }
        }

        public void Push(DeviceSubscription subscription, Payload payload)
        {
            var context = new PushContext { Subscription = subscription, Payload = payload };
            if (OperationContext.Current != null)
                context.Callback = OperationContext.Current.GetCallbackChannel<IPushCallback>();

            if (subscription.Channel.PlatformType == PlatformType.Apple)
            {
                var notification = new AppleNotification(subscription.SubscriptionId) { Tag = context };
                if (payload != null)
                {
                    var p = new AppleNotificationPayload();
                    p.Alert = new AppleNotificationAlert { Body = payload.Alert };
                    p.Badge = payload.Badge;
                    p.Sound = payload.Sound;
                    p.CustomJson = payload.Custom;  //https://github.com/caniusq/PushSharp
                    notification.Payload = p;
                }

                PushBrokerManager.Broker.QueueNotification(notification, subscription.Channel.ApplicationId);
            }
            else if (subscription.Channel.PlatformType == PlatformType.Beyondbit)
            {
                var notification = new BeyondBit.PushClientLib.BeyondBitNotification { DeviceToken = subscription.SubscriptionId, Tag = context };
                if (payload != null)
                {
                    var bnp = new BeyondBit.PushClientLib.BeyondbitNotificationPayload();
                    bnp.Alert = payload.Alert;
                    bnp.Badge = payload.Badge;
                    bnp.Sound = payload.Sound;
                    bnp.CustomJson = payload.Custom;
                    notification.Payload = bnp;
                }

                PushBrokerManager.Broker.QueueNotification(notification, subscription.Channel.ApplicationId);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void Regist(string applicationId, PlatformType platformType, string uid, string subscriptionId)
        {
            if (string.IsNullOrWhiteSpace(uid))
                throw new ArgumentNullException("uid");

            if (string.IsNullOrWhiteSpace(applicationId))
                throw new ArgumentNullException("applicationId");

            if (string.IsNullOrWhiteSpace(subscriptionId))
                throw new ArgumentNullException("subscriptionId");

            PushBo.Instance.RegSubscription(applicationId, platformType, uid, subscriptionId);
        }

        public void Discard(string applicationId, PlatformType platformType, string subscriptionId)
        {
            if (string.IsNullOrWhiteSpace(applicationId))
                throw new ArgumentNullException("applicationId");

            if (string.IsNullOrWhiteSpace(subscriptionId))
                throw new ArgumentNullException("subscriptionId");

            PushBo.Instance.RemoveSubscription(applicationId, platformType, subscriptionId);
        }
    }
}
