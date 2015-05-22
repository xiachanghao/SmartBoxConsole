using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Beyondbit.Push.Service.Bo
{
    public class PushBo
    {
        private static PushBo _inst = new PushBo();
        public static PushBo Instance
        {
            get { return _inst; }
        }

        private PushBo() { }
        private readonly object _lock = new object();
        private List<Channel> _channels;
        public List<Channel> ActiveChannels//cached
        {
            get
            {
                var channels = _channels;
                if (channels != null) return channels;
                lock (_lock)
                {
                    channels = _channels;
                    if (channels != null) return channels;
                    channels = QueryChannels(0);
                    _channels = channels;
                    return channels;
                }
            }
            set
            {
                lock (_lock)
                {
                    _channels = value;
                }
            }
        }

        public void SaveChannel(Channel channel, bool saveCert)
        {
            using (var con = new SqlConnection(ConfigHelp.ConnectionString))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", channel.Id));
                cmd.Parameters.Add(new SqlParameter("@Title", channel.Title));
                cmd.Parameters.Add(new SqlParameter("@ApplicationId", channel.ApplicationId));
                cmd.Parameters.Add(new SqlParameter("@PlatformType", channel.PlatformType.ToString()));
                cmd.Parameters.Add(new SqlParameter("@CertName", channel.CertName ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@CertPassword", channel.CertPassword ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@State", channel.State));

                if (saveCert)
                {
                    cmd.Parameters.Add(new SqlParameter("@Cert", System.Data.SqlDbType.Image) { Value = channel.Cert ?? (object)DBNull.Value });
                    if (channel.Id == 0)
                    {
                        cmd.CommandText = @"insert into Channel (Title,ApplicationId,PlatformType,Cert,CertName,CertPassword,State) 
values (@Title,@ApplicationId,@PlatformType,@Cert,@CertName,@CertPassword,@State)";
                    }
                    else
                    {
                        cmd.CommandText = 
@"update Channel set Title=@Title,ApplicationId=@ApplicationId,PlatformType=@PlatformType,Cert=@Cert,CertName=@CertName,CertPassword=@CertPassword where Id=@Id";
                    }
                }
                else
                {
                    cmd.CommandText =
@"update Channel set Title=@Title,ApplicationId=@ApplicationId,PlatformType=@PlatformType,CertPassword=@CertPassword where Id=@Id";
                }

                cmd.ExecuteNonQuery();
            }
        }

        public List<Channel> QueryChannels(int? state = null)
        {
            var list = new List<Channel>();
            using (var con = new SqlConnection(ConfigHelp.ConnectionString))
            {
                con.Open();
                var cmd = con.CreateCommand();
                if (state.HasValue)
                {
                    cmd.CommandText = "select * from Channel where State=@state";
                    cmd.Parameters.Add(new SqlParameter("@State", state.Value));
                }
                else
                {
                    cmd.CommandText = "select * from Channel";
                }
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(ChannelFromDataReader(rdr));
                    }
                }
            }
            return list;
        }

        public List<Channel> QueryDeletedChannels(bool deleted)
        {
            var list = new List<Channel>();
            using (var con = new SqlConnection(ConfigHelp.ConnectionString))
            {
                con.Open();
                var cmd = con.CreateCommand();
                if (deleted)
                {
                    cmd.CommandText = "select * from Channel where State=2";
                }
                else
                {
                    cmd.CommandText = "select * from Channel where State<>2";
                }
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(ChannelFromDataReader(rdr));
                    }
                }
            }
            return list;
        }

        private List<Channel> QueryChannels(string applicationId, PlatformType? platformType)////from cache
        {
            if (platformType.HasValue)
                return (from channel in this.ActiveChannels
                        where channel.ApplicationId == applicationId && channel.PlatformType == platformType.Value
                        select channel).ToList();
            else
                return (from channel in this.ActiveChannels
                        where channel.ApplicationId == applicationId
                        select channel).ToList();
        }

        private Channel GetChannel(int channelId)////from cache
        {
            return (from channel in this.ActiveChannels
                    where channel.Id == channelId
                    select channel).FirstOrDefault();
        }

        public void UpdateChannelState(int channelId, int state)
        {
            using (var con = new SqlConnection(ConfigHelp.ConnectionString))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = "update Channel set State=@state,DeletedTime=@deletedTime where Id=@id";
                cmd.Parameters.Add(new SqlParameter("@state", System.Data.SqlDbType.Int) { Value = state });
                cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int) { Value = channelId });
                object deletedTime = state == 2 ? (object)DateTime.Now : DBNull.Value;
                cmd.Parameters.Add(new SqlParameter("@deletedTime", System.Data.SqlDbType.DateTime) { Value = deletedTime });
                cmd.ExecuteNonQuery();
            }
        }

        public void ClearDeletedChannels(DateTime before)
        {
            var ids = new List<int>();
            using (var con = new SqlConnection(ConfigHelp.ConnectionString))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = "select Id from Channel where State=2 and DeletedTime<@before";
                cmd.Parameters.Add(new SqlParameter("@before", System.Data.SqlDbType.DateTime) { Value = before });
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        ids.Add(rdr.GetInt32(0));
                    }
                }
            }

            foreach (int id in ids)
            {
                DeleteChannel(id);
            }
        }

        private void DeleteChannel(int id)
        {
            using (var con = new SqlConnection(ConfigHelp.ConnectionString))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int) { Value = id });
                cmd.CommandText = "delete DeviceSubscription where ChannelId=@id";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "delete LogInfo where ChannelId=@id";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "delete Channel where Id=@id";
                cmd.ExecuteNonQuery();
            }
        }

        public List<DeviceSubscription> QueryDeviceSubscriptions(string applicationId, PlatformType? platformType, string uid)
        {
            var channelIds = (from channel in this.QueryChannels(applicationId, platformType) select channel.Id).ToArray();
            return QueryDeviceSubscriptions(channelIds, uid);
        }

        public List<DeviceSubscription> QueryDeviceSubscriptions(IEnumerable<int> channelIds, string uid)
        {
            var list = new List<DeviceSubscription>();
            if (channelIds.Count() == 0) return list;

            string chanelIDs = "";
            foreach(int i in channelIds)
            {
                chanelIDs += "," + i.ToString();
            }
            chanelIDs = chanelIDs.TrimEnd(',');

            const string sqlFormat = "select * from DeviceSubscription where UserUid=@uid and ChannelId in ({0})";

            using (var con = new SqlConnection(ConfigHelp.ConnectionString))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = string.Format(sqlFormat, chanelIDs);
                cmd.Parameters.Add(new SqlParameter("@uid", uid));
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var subscription = new DeviceSubscription();
                        subscription.Id = rdr.GetInt32(rdr.GetOrdinal("Id"));
                        subscription.ChannelId = rdr.GetInt32(rdr.GetOrdinal("ChannelId"));
                        subscription.SubscriptionId = rdr.GetString(rdr.GetOrdinal("SubscriptionId"));
                        subscription.UserUid = rdr.GetString(rdr.GetOrdinal("UserUid"));
                        subscription.Channel = GetChannel(subscription.ChannelId);
                        list.Add(subscription);
                    }
                }
            }

            return list;
        }

        public void RegSubscription(string applicationId, PlatformType platformType, string uid, string subscriptionId)
        {
            var channel = this.QueryChannels(applicationId, platformType).FirstOrDefault();
            if (channel == null) return;

            using (var con = new SqlConnection(ConfigHelp.ConnectionString))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = "select UserUid from DeviceSubscription where SubscriptionId=@subscriptionId and ChannelId=@channelId";
                cmd.Parameters.Add(new SqlParameter("@channelId", channel.Id));
                cmd.Parameters.Add(new SqlParameter("@subscriptionId", subscriptionId));
                string oldUid = cmd.ExecuteScalar() as string;
                if (oldUid == null)
                {
                    cmd.Parameters.Add(new SqlParameter("@uid", uid));
                    cmd.CommandText = "insert into DeviceSubscription (ChannelId,SubscriptionId,UserUid) values (@channelId,@subscriptionId,@uid)";
                    cmd.ExecuteNonQuery();
                }
                else if (uid != oldUid)
                {
                    cmd.Parameters.Add(new SqlParameter("@uid", uid));
                    cmd.CommandText = "update DeviceSubscription set UserUid=@uid where SubscriptionId=@subscriptionId and ChannelId=@channelId";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveSubscription(string applicationId, PlatformType platformType, string subscriptionId)
        {
            var channel = this.QueryChannels(applicationId, platformType).FirstOrDefault();
            if (channel == null) return;
            using (var con = new SqlConnection(ConfigHelp.ConnectionString))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = "delete DeviceSubscription where SubscriptionId=@subscriptionId and ChannelId=@channelId";
                cmd.Parameters.Add(new SqlParameter("@channelId", channel.Id));
                cmd.Parameters.Add(new SqlParameter("@subscriptionId", subscriptionId));
                cmd.ExecuteNonQuery();
            }
        }

        public void Log(LogInfo log)
        {
            using (var con = new SqlConnection(ConfigHelp.ConnectionString))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = @"insert into LogInfo (ChannelId,UserUid,Title,Message,StackTrace,Level,LogTime) 
values (@ChannelId,@UserUid,@Title,@Message,@StackTrace,@Level,@LogTime)";
                cmd.Parameters.Add(new SqlParameter("@ChannelId", log.ChannelId));
                cmd.Parameters.Add(new SqlParameter("@UserUid", log.UserUid ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Title", log.Title));
                cmd.Parameters.Add(new SqlParameter("@Message", log.Message));
                cmd.Parameters.Add(new SqlParameter("@StackTrace", log.StackTrace ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Level", log.Level));
                cmd.Parameters.Add(new SqlParameter("@LogTime", log.LogTime));
                cmd.ExecuteNonQuery();
            }
        }

        public List<LogInfo> QueryLogs(int channelId, string level)
        {
            var list = new List<LogInfo>();
            using (var con = new SqlConnection(ConfigHelp.ConnectionString))
            {
                con.Open();
                var cmd = con.CreateCommand();
                string sql = "select TOP 500 * from LogInfo where ChannelId=@channelId";
                cmd.Parameters.Add(new SqlParameter("@channelId", System.Data.SqlDbType.Int) { Value = channelId });
                if (!string.IsNullOrEmpty(level))
                {
                    sql += " and [Level]=@level";
                    cmd.Parameters.Add(new SqlParameter("@level", level));
                }
                sql += " order by LogTime desc";
                cmd.CommandText = sql;
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(LogInfoFromDataReader(rdr));
                    }
                }
            }
            return list;
        }

        private Channel ChannelFromDataReader(SqlDataReader rdr)
        {
            var channel = new Channel();
            channel.Id = rdr.GetInt32(rdr.GetOrdinal("Id"));
            channel.Title = rdr.GetString(rdr.GetOrdinal("Title"));
            channel.ApplicationId = rdr.GetString(rdr.GetOrdinal("ApplicationId"));
            channel.PlatformType = (PlatformType)Enum.Parse(typeof(PlatformType), rdr.GetString(rdr.GetOrdinal("PlatformType")), true);

            if (!rdr.IsDBNull(rdr.GetOrdinal("Cert")))
                channel.Cert = rdr.GetSqlBinary(rdr.GetOrdinal("Cert")).Value;
            if (!rdr.IsDBNull(rdr.GetOrdinal("CertName")))
                channel.CertName = rdr.GetString(rdr.GetOrdinal("CertName"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("CertPassword")))
                channel.CertPassword = rdr.GetString(rdr.GetOrdinal("CertPassword"));

            channel.State = rdr.GetInt32(rdr.GetOrdinal("State"));

            return channel;
        }

        private LogInfo LogInfoFromDataReader(SqlDataReader rdr)
        {
            var log = new LogInfo();
            log.Id = rdr.GetInt32(rdr.GetOrdinal("Id"));
            log.ChannelId = rdr.GetInt32(rdr.GetOrdinal("ChannelId"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("UserUid")))
                log.UserUid = rdr.GetString(rdr.GetOrdinal("UserUid"));
            log.Title = rdr.GetString(rdr.GetOrdinal("Title"));
            log.Message = rdr.GetString(rdr.GetOrdinal("Message"));
            log.Level = rdr.GetString(rdr.GetOrdinal("Level"));
            if (!rdr.IsDBNull(rdr.GetOrdinal("StackTrace")))
                log.StackTrace = rdr.GetString(rdr.GetOrdinal("StackTrace"));
            log.LogTime = DateTime.SpecifyKind(rdr.GetDateTime(rdr.GetOrdinal("LogTime")), DateTimeKind.Local);
            return log;
        }
    }
}