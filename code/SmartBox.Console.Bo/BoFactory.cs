using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beyondbit.Framework.Core.Proxy;

namespace SmartBox.Console.Bo
{
    public static class BoFactory
    {
        static SmartBox.Console.Bo.VersionTrackBo _VersionTrackBo;
        static readonly MonitorBO _MonitorBO = null;
        static readonly SMC_UnitBo _SMC_UnitBo = null;
        static readonly SMC_AutoTableIDBo _SMC_AutoTableIDBo = null;
        static readonly SMC_FunctionRoleBo _SMC_FunctionRoleBo = null;
        static readonly SMC_FunctionsBo _SMC_FunctionsBo = null;
        static readonly SMC_RoleBo _SMC_RoleBo = null;
        static readonly SMC_UserListBo _SMC_UserListBo = null;
        static readonly PushManageBO _PushManageBO = null;
        static readonly SmartBox.Console.Bo.AppCenter.AppCenterBO _AppCenterBO = null;
        static readonly SmartBox.Console.Bo.SMC_PackageExtBO _SMC_PackageExtBO = null;
        static readonly SmartBox.Console.Bo.SMC_BUAUserSyncToInsideBO _SMC_BUAUserSyncToInsideBO = null;
        static readonly SmartBox.Console.Bo.SMC_BUAUserSyncToOutsideBO _SMC_BUAUserSyncToOutsideBO = null;
        static readonly SmartBox.Console.Bo.SMC_PackageExtSyncToOutsideBO _SMC_PackageExtSyncToOutsideBO = null;
        static readonly SmartBox.Console.Bo.SMC_PushDllBO _SMC_PushDllBO = null;
        static readonly SmartBox.Console.Bo.StyleBO _StyleBO = null;
        static readonly SmartBox.Console.Bo.SMC_UserBo _SMC_UserBo = null;
        static readonly SmartBox.Console.Bo.StyleHomeItemBO _StyleHomeItemBO = null;
        static readonly StatisticsBO _StatisticsBO = null;
        static readonly CommonBO _CommonBO = null;
        static readonly MonitorDefindBO _MonitorDefindBO = null;
        static readonly SMC_UserExceptionBo _SMC_UserExceptionBo = null;
        static readonly DeviceExceptionBo _DeviceExceptionBo = null;
        static readonly DeviceBO _DeviceBo = null;
        static readonly GlobalParamBO _GlobalParamBO = null;
        static readonly SMC_PackageExtHistoryBO _SMC_PackageExtHistoryBO = null;
        static readonly ApplicationBo _ApplicationBo = null;
        static readonly SystemConfigBO _SystemConfigBo = null;
        static readonly DeviceUserApplyBO _DeviceUserApplyBO = null;
        static readonly SMC_PackageFAQBO _SMC_PackageFAQBO = null;
        static readonly UserInfoBO _UserInfoBO = null;
        static readonly ActionExtendBO _ActionExtendBO = null;
        static readonly Action4AndroidBO _Action4AndroidBO = null;
        static readonly App4AIBO _App4AIBO = null;
        static readonly Package4AIBO _Package4AIBO = null;
        static readonly AppPrivilegeBO _AppPrivilegeBO = null;
        static BoFactory()
        {
            IProxy proxy = ProxyFactory.CreateProxy();
            _VersionTrackBo = proxy.CreateObject<SmartBox.Console.Bo.VersionTrackBo>();
            _SMC_UnitBo = proxy.CreateObject<SMC_UnitBo>();
            _SMC_AutoTableIDBo = proxy.CreateObject<SMC_AutoTableIDBo>();
            _SMC_FunctionRoleBo = proxy.CreateObject<SMC_FunctionRoleBo>();
            _SMC_FunctionsBo = proxy.CreateObject<SMC_FunctionsBo>();
            _SMC_RoleBo = proxy.CreateObject<SMC_RoleBo>();
            _SMC_UserListBo = proxy.CreateObject<SMC_UserListBo>();
            _MonitorBO = proxy.CreateObject<MonitorBO>();
            _AppCenterBO = proxy.CreateObject<SmartBox.Console.Bo.AppCenter.AppCenterBO>();
            _SMC_PackageExtBO = proxy.CreateObject<SmartBox.Console.Bo.SMC_PackageExtBO>();
            _PushManageBO = proxy.CreateObject<SmartBox.Console.Bo.PushManageBO>();
            _SMC_BUAUserSyncToInsideBO = proxy.CreateObject<SmartBox.Console.Bo.SMC_BUAUserSyncToInsideBO>();
            _SMC_BUAUserSyncToOutsideBO = proxy.CreateObject<SmartBox.Console.Bo.SMC_BUAUserSyncToOutsideBO>();
            _SMC_PackageExtSyncToOutsideBO = proxy.CreateObject<SmartBox.Console.Bo.SMC_PackageExtSyncToOutsideBO>();
            _StatisticsBO = proxy.CreateObject<StatisticsBO>();
            _SMC_PushDllBO = proxy.CreateObject<SMC_PushDllBO>();
            _StyleBO = proxy.CreateObject<StyleBO>();
            _StyleHomeItemBO = proxy.CreateObject<StyleHomeItemBO>();
            _SMC_UserBo = proxy.CreateObject<SMC_UserBo>();
            _CommonBO = proxy.CreateObject<CommonBO>();
            _SMC_UserExceptionBo = proxy.CreateObject<SMC_UserExceptionBo>();
            _DeviceExceptionBo = proxy.CreateObject<DeviceExceptionBo>();
            _DeviceBo = proxy.CreateObject<DeviceBO>();
            _GlobalParamBO = proxy.CreateObject<GlobalParamBO>();
            _SMC_PackageExtHistoryBO = proxy.CreateObject<SMC_PackageExtHistoryBO>();
            _ApplicationBo = proxy.CreateObject<ApplicationBo>();
            _SystemConfigBo = proxy.CreateObject<SystemConfigBO>();
            _DeviceUserApplyBO = proxy.CreateObject<DeviceUserApplyBO>();
            _SMC_PackageFAQBO = proxy.CreateObject<SMC_PackageFAQBO>();
            _UserInfoBO = proxy.CreateObject<UserInfoBO>();
            _ActionExtendBO = proxy.CreateObject<ActionExtendBO>();
            _Action4AndroidBO = proxy.CreateObject<Action4AndroidBO>();
            _App4AIBO = proxy.CreateObject<App4AIBO>();
            _Package4AIBO = proxy.CreateObject<Package4AIBO>();
            _AppPrivilegeBO = proxy.CreateObject<AppPrivilegeBO>();
            _MonitorDefindBO = proxy.CreateObject<MonitorDefindBO>();
        }

        public static VersionTrackBo GetVersionTrackBo
        {
            get {
                if (_VersionTrackBo == null)
                {
                    IProxy proxy = ProxyFactory.CreateProxy();
                    _VersionTrackBo = proxy.CreateObject<SmartBox.Console.Bo.VersionTrackBo>();
                }
                return _VersionTrackBo; 
            }
        }

        public static CommonBO GetCommonBO
        {
            get {
                return _CommonBO; 
            }
        }

        public static MonitorDefindBO GetMonitorDefindBO
        {
            get {
                return _MonitorDefindBO; 
            }
        }

        public static ApplicationBo GetApplicationBO
        {
            get {
                return _ApplicationBo; 
            }
        }

        public static SystemConfigBO GetSystemConfigBO
        {
            get {
                return _SystemConfigBo; 
            }
        }

        public static ActionExtendBO GetActionExtendBO
        {
            get {
                return _ActionExtendBO; 
            }
        }

        public static Action4AndroidBO GetAction4AndroidBO
        {
            get {
                return _Action4AndroidBO; 
            }
        }

        public static SMC_PackageExtHistoryBO GetSMC_PackageExtHistoryBO
        {
            get {
                return _SMC_PackageExtHistoryBO; 
            }
        }
        
        public static DeviceBO GetDeviceBO
        {
            get {
                return _DeviceBo; 
            }
        }

        public static GlobalParamBO GetGlobalParamBO
        {
            get {
                return _GlobalParamBO; 
            }
        }

        public static AppPrivilegeBO GetAppPrivilegeBO
        {
            get {
                return _AppPrivilegeBO; 
            }
        }

        public static SMC_UserExceptionBo GetSMC_UserExceptionBo
        {
            get {
                return _SMC_UserExceptionBo; 
            }
        }

        public static StatisticsBO GetStatisticsBO
        {
            get {
                return _StatisticsBO; 
            }
        }

        public static DeviceExceptionBo GetDeviceExceptionBO
        {
            get {
                return _DeviceExceptionBo; 
            }
        }

        public static App4AIBO GetApp4AIBO
        {
            get {
                return _App4AIBO; 
            }
        }

        public static SMC_PushDllBO GetSMC_PushDllBO
        {
            get {
                return _SMC_PushDllBO; 
            }
        }

        public static DeviceUserApplyBO GetDeviceUserApplyBO
        {
            get {
                return _DeviceUserApplyBO; 
            }
        }

        public static SMC_BUAUserSyncToInsideBO GetSMC_BUAUserSyncToInsideBO
        {
            get
            {
                return _SMC_BUAUserSyncToInsideBO;
            }
        }
        public static SMC_BUAUserSyncToOutsideBO GetSMC_BUAUserSyncToOutsideBO
        {
            get
            {
                return _SMC_BUAUserSyncToOutsideBO;
            }
        }
        public static StyleBO GetStyleBO
        {
            get
            {
                return _StyleBO;
            }
        }
        public static StyleHomeItemBO GetStyleHomeItemBO
        {
            get
            {
                return _StyleHomeItemBO;
            }
        }

        public static SMC_UserBo GetSMC_UserBo
        {
            get
            {
                return _SMC_UserBo;
            }
        }

        public static Package4AIBO GetPackage4AIBO
        {
            get
            {
                return _Package4AIBO;
            }
        }

        public static SMC_PackageExtSyncToOutsideBO GetSMC_PackageExtSyncToOutsideBO
        {
            get {
                //if (_SMC_PackageExtSyncToOutsideBO == null)
                //{
                //    IProxy proxy = ProxyFactory.CreateProxy();
                //    _SMC_PackageExtSyncToOutsideBO = proxy.CreateObject<SmartBox.Console.Bo.SMC_PackageExtSyncToOutsideBO>();
                //}
                return _SMC_PackageExtSyncToOutsideBO; 
            }
        }

        public static SMC_PackageExtBO GetSMC_PackageExtBO
        {
            get {
                return _SMC_PackageExtBO; 
            }
        }

        public static MonitorBO GetMonitorBO
        {
            get {
                return _MonitorBO; 
            }
        }

        public static PushManageBO GetPushManageBO
        {
            get {
                return _PushManageBO; 
            }
        }

        public static SMC_UnitBo GetSMC_UnitBo
        {
            get
            {
                return _SMC_UnitBo;
            }
        }

        public static SmartBox.Console.Bo.AppCenter.AppCenterBO GetAppCenterBO
        {
            get
            {
                return _AppCenterBO;
            }
        }

        public static SMC_AutoTableIDBo GetSMC_AutoTableIDBo
        {
            get
            {
                return _SMC_AutoTableIDBo;
            }
        }

        public static SMC_FunctionRoleBo GetSMC_FunctionRoleBo
        {
            get
            {
                return _SMC_FunctionRoleBo;
            }
        }

        public static SMC_FunctionsBo GetSMC_FunctionsBo
        {
            get
            {
                return _SMC_FunctionsBo;
            }
        }

        public static SMC_RoleBo GetSMC_RoleBo
        {
            get
            {
                return _SMC_RoleBo;
            }
        }

        public static SMC_UserListBo GetSMC_UserListBo
        {
            get
            {
                return _SMC_UserListBo;
            }
        }

        public static SMC_PackageFAQBO GetSMC_PackageFAQBO
        {
            get
            {
                return _SMC_PackageFAQBO;
            }
        }

        public static UserInfoBO GetUserInfoBO
        {
            get
            {
                return _UserInfoBO;
            }
        }
    }
}
