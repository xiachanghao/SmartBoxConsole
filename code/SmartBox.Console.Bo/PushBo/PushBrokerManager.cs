using Beyondbit.Push.Service.Bo;
using PushSharp;
using PushSharp.Apple;
using PushSharp.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using BeyondBit.PushClientLib;

namespace Beyondbit.Push.Service
{
    public class PushBrokerManager
    {
        private static object _lock = new object();

        private static PushBroker _broker;
        public static PushBroker Broker
        {
            get
            {
                if (_broker != null) return _broker;
                lock (_lock)
                {
                    if (_broker != null) return _broker;
                    _broker = CreateBroker();
                    return _broker;
                }
            }
        }

        public static void Restart()
        {
            lock (_lock)
            {
                if (_broker != null)
                {
                    _broker.StopAllServices(true);
                    PushBo.Instance.ActiveChannels = null;
                    _broker = CreateBroker();
                }
            }
        }

        private static PushBroker CreateBroker()
        {
            var broker = new PushBroker();

            broker.OnNotificationSent += NotificationSent;
            broker.OnChannelException += ChannelException;
            broker.OnServiceException += ServiceException;
            broker.OnNotificationFailed += NotificationFailed;
            broker.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
            broker.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
            broker.OnChannelCreated += ChannelCreated;
            broker.OnChannelDestroyed += ChannelDestroyed;

            foreach (var channel in PushBo.Instance.ActiveChannels)
            {
                if (channel.PlatformType == PlatformType.Apple)
                {
                    broker.RegisterAppleService(new ApplePushChannelSettings(ConfigHelp.Production, channel.Cert, channel.CertPassword), channel.ApplicationId);
                }
                else if (channel.PlatformType == PlatformType.Beyondbit)
                {
                    broker.RegisterPushClientService(new BeyondBitPushChannelSettings(channel.ApplicationId)); 
                }
                else throw new NotImplementedException();
            }

            return broker;
        }

        private static void EnablePushSharpLog()
        {
            PushSharp.Core.Log.Logger = new PushSharpLogImpl();
            if (Logger.Log.IsDebugEnabled)
                PushSharp.Core.Log.Level = LogLevel.Debug;
            else if (Logger.Log.IsInfoEnabled)
                PushSharp.Core.Log.Level = LogLevel.Info;
            else if (Logger.Log.IsWarnEnabled)
                PushSharp.Core.Log.Level = LogLevel.Warning;
            else if (Logger.Log.IsErrorEnabled)
                PushSharp.Core.Log.Level = LogLevel.Error;
            else if (Logger.Log.IsFatalEnabled)
                PushSharp.Core.Log.Level = LogLevel.Error;
            else
                PushSharp.Core.Log.Level = LogLevel.None;
        }

        private static void LogPushResultToDatabase(PushContext context, string message, Exception ex)
        {
            var log = new LogInfo();
            log.ChannelId = context.Subscription.ChannelId;
            log.UserUid = context.Subscription.UserUid;
            log.Message = message;
            if (ex == null)
            {
                log.Title = "推送成功";
                log.Level = LogInfo.LEVEL_INFO;
            }
            else
            {
                log.Title = "推送失败";
                log.Level = LogInfo.LEVEL_WARN;
                log.StackTrace = ex.StackTrace;
            }
            log.LogTime = DateTime.Now;
            try
            {
                PushBo.Instance.Log(log);
            }
            catch (Exception e)
            {
                Logger.Log.Error(e);
            }
        }

        #region events

        static void DeviceSubscriptionChanged(object sender, string oldSubscriptionId, string newSubscriptionId, INotification notification)
        {
            //Currently this event will only ever happen for Android GCM
            Logger.Log.Info("Device Registration Changed:  Old-> " + oldSubscriptionId + "  New-> " + newSubscriptionId + " -> " + notification);
        }

        static void NotificationSent(object sender, INotification notification)
        {
            var context = (PushContext)notification.Tag;
            if (!string.IsNullOrEmpty(context.Payload.Id) && context.Callback != null)
            {
                try
                {
                    context.Callback.NotificationSent(context.Payload.Id);
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex);
                }
            }
            string message = "Sent: " + notification;
            Logger.Log.Info(message);
            LogPushResultToDatabase(context, message, null);
        }

        static void NotificationFailed(object sender, INotification notification, Exception notificationFailureException)
        {
            var context = (PushContext)notification.Tag;
            if (!string.IsNullOrEmpty(context.Payload.Id) && context.Callback != null)
            {
                try
                {
                    context.Callback.NotificationFailed(context.Payload.Id, notificationFailureException);
                }
                catch(Exception ex)
                {
                    Logger.Log.Error(ex);
                }
            }
            string message = "Failure: " + notificationFailureException.Message + " -> " + notification;
            Logger.Log.Error(message);
            LogPushResultToDatabase(context, message, notificationFailureException);
        }

        static void ChannelException(object sender, IPushChannel channel, Exception exception)
        {
            Logger.Log.Error("Channel Exception: " + sender + " -> " + exception);
        }

        static void ServiceException(object sender, Exception exception)
        {
            Logger.Log.Error("Channel Exception: " + sender + " -> " + exception);
        }

        static void DeviceSubscriptionExpired(object sender, string expiredDeviceSubscriptionId, DateTime timestamp, INotification notification)
        {
            Logger.Log.Info("Device Subscription Expired: " + sender + " -> " + expiredDeviceSubscriptionId);
        }

        static void ChannelDestroyed(object sender)
        {
            Logger.Log.Info("Channel Destroyed for: " + sender);
        }

        static void ChannelCreated(object sender, IPushChannel pushChannel)
        {
            Logger.Log.Info("Channel Created for: " + sender);
        }

        #endregion
    } 
}