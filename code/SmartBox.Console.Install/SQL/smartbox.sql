USE [SmartBox]
GO
/****** Object:  Table [dbo].[FileStore]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileStore](
	[Id] [uniqueidentifier] NOT NULL,
	[DisplayName] [nvarchar](256) NOT NULL,
	[DataId] [nvarchar](64) NOT NULL,
	[From] [nvarchar](64) NOT NULL,
	[To] [nvarchar](128) NOT NULL,
	[Size] [bigint] NOT NULL,
	[CreateUid] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateUid] [nvarchar](64) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_FileStore] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'离线文件存储表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FileStore'
GO
/****** Object:  Table [dbo].[DeviceUserApply]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceUserApply](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DeviceUserID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[ApplyTime] [datetime] NOT NULL,
	[CheckUid] [nvarchar](64) NULL,
	[CheckTime] [datetime] NULL,
	[Remark] [nvarchar](512) NULL,
 CONSTRAINT [PK_DeviceUserApply] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：申请中；1：已审核通过；2：已审核不通过；3已审核不通过并禁用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DeviceUserApply', @level2type=N'COLUMN',@level2name=N'Status'
GO
/****** Object:  Table [dbo].[DeviceUser]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceUser](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[DeviceID] [nvarchar](256) NOT NULL,
	[UID] [nvarchar](64) NOT NULL,
	[Status] [int] NOT NULL,
	[NoUseReason] [int] NULL,
	[LastUpdateUID] [nvarchar](64) NULL,
	[LastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_DeviceUser] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:默认，1：可以使用，2：不可使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DeviceUser', @level2type=N'COLUMN',@level2name=N'Status'
GO
/****** Object:  Table [dbo].[DeviceException]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceException](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[useruid] [nvarchar](50) NULL,
	[type] [int] NULL,
 CONSTRAINT [PK_DeviceException] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1启用例外 2禁用例外' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DeviceException', @level2type=N'COLUMN',@level2name=N'type'
GO
/****** Object:  Table [dbo].[DeviceConfig]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceConfig](
	[Key] [nvarchar](64) NOT NULL,
	[ValueType] [int] NOT NULL,
	[Value] [nvarchar](512) NULL,
	[XmlValue] [xml] NULL,
 CONSTRAINT [PK_DeviceConfig] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeviceBind]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DeviceBind](
	[Id] [uniqueidentifier] NOT NULL,
	[UserUid] [varchar](64) NOT NULL,
	[Device] [varchar](32) NOT NULL,
	[DeviceId] [varchar](64) NULL,
	[Status] [varchar](32) NULL,
	[Description] [nvarchar](256) NULL,
 CONSTRAINT [PK_DEVICEBIND] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态 
   ENABLE 启用
   DISABLE 禁用
   LOST 遗失' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DeviceBind', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DeviceBind', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户设备绑定' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DeviceBind'
GO
/****** Object:  Table [dbo].[Device]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Device](
	[ID] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[OS] [nvarchar](256) NOT NULL,
	[Model] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[Remark] [nvarchar](512) NULL,
	[LockTime] [datetime] NULL,
	[LockExpireHours] [int] NULL,
	[UnLockTime] [datetime] NULL,
	[LostTime] [datetime] NULL,
	[UnLostTime] [datetime] NULL,
 CONSTRAINT [PK_Device] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:默认，1：锁定，2：挂失' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Device', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'锁定时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Device', @level2type=N'COLUMN',@level2name=N'LockTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备锁定过期小时数，单位小时' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Device', @level2type=N'COLUMN',@level2name=N'LockExpireHours'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'解除锁定时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Device', @level2type=N'COLUMN',@level2name=N'UnLockTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'挂失时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Device', @level2type=N'COLUMN',@level2name=N'LostTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'解除挂失时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Device', @level2type=N'COLUMN',@level2name=N'UnLostTime'
GO
/****** Object:  Table [dbo].[HomePlan]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomePlan](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[Code] [nvarchar](64) NULL,
	[DisplayName] [nvarchar](64) NULL,
	[Owner] [nvarchar](64) NULL,
	[Format] [nvarchar](64) NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[CreateUid] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateUid] [nvarchar](64) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_HOMEPLAN] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HomePlan', @level2type=N'COLUMN',@level2name=N'CreateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HomePlan', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HomePlan', @level2type=N'COLUMN',@level2name=N'UpdateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HomePlan', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
/****** Object:  Table [dbo].[FriendInviteCache]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FriendInviteCache](
	[Source] [varchar](64) NOT NULL,
	[Dest] [varchar](64) NOT NULL,
	[Type] [varchar](16) NOT NULL,
	[Message] [nvarchar](512) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FriendGroup]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FriendGroup](
	[FriendGroupId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](64) NULL,
	[OwnerUid] [nvarchar](64) NULL,
	[GroupType] [nvarchar](32) NULL,
	[Sequence] [int] NULL,
 CONSTRAINT [PK_FRIENDGROUP] PRIMARY KEY CLUSTERED 
(
	[FriendGroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'好友组id 自增长主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FriendGroup', @level2type=N'COLUMN',@level2name=N'FriendGroupId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'好友组名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FriendGroup', @level2type=N'COLUMN',@level2name=N'GroupName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'好友组所属用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FriendGroup', @level2type=N'COLUMN',@level2name=N'OwnerUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'好友组类型，目前有默认组和用户增加组两类（Default，UserAdd）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FriendGroup', @level2type=N'COLUMN',@level2name=N'GroupType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组顺序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FriendGroup', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'好友组' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FriendGroup'
GO
/****** Object:  Table [dbo].[ConfigTemp]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ConfigTemp](
	[PlugInCode] [varchar](64) NOT NULL,
	[ConfigCategoryCode] [varchar](64) NULL,
	[Key] [varchar](256) NOT NULL,
	[Value] [varchar](512) NULL,
	[Summary] [nvarchar](512) NULL,
	[IsPublic] [bit] NOT NULL,
 CONSTRAINT [PK_CONFIGTEMP] PRIMARY KEY CLUSTERED 
(
	[PlugInCode] ASC,
	[Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件id，如果是主程序该字段为SmartBoxTEST' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigTemp', @level2type=N'COLUMN',@level2name=N'PlugInCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigTemp', @level2type=N'COLUMN',@level2name=N'ConfigCategoryCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置键值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigTemp', @level2type=N'COLUMN',@level2name=N'Key'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置键值所对应的数据值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigTemp', @level2type=N'COLUMN',@level2name=N'Value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigTemp', @level2type=N'COLUMN',@level2name=N'Summary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置临时表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigTemp'
GO
/****** Object:  Table [dbo].[ConfigInfoPC]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ConfigInfoPC](
	[ConfigId] [int] IDENTITY(1,1) NOT NULL,
	[ConfigCategoryCode] [varchar](64) NULL,
	[UserUId] [varchar](64) NULL,
	[PlugInCode] [varchar](64) NULL,
	[Key] [varchar](256) NULL,
	[Value] [varchar](512) NULL,
	[Summary] [nvarchar](512) NULL,
	[IsPublic] [bit] NULL,
 CONSTRAINT [PK_CONFIGINFOPC] PRIMARY KEY CLUSTERED 
(
	[ConfigId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增长主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigInfoPC', @level2type=N'COLUMN',@level2name=N'ConfigId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置分类
   全局配置:GlobalConfig
   常规系统配置:SysCommonConfig
   常规插件配置:PluginConfig
   个人系统配置:PersonalSysConfig
   个人插件配置:PersonalPluginConfig' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigInfoPC', @level2type=N'COLUMN',@level2name=N'ConfigCategoryCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户登录id，如果是用户个人配置则此字段有值，否则没有值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigInfoPC', @level2type=N'COLUMN',@level2name=N'UserUId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件标识，如果是插件配置则有值否则为空' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigInfoPC', @level2type=N'COLUMN',@level2name=N'PlugInCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置的值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigInfoPC', @level2type=N'COLUMN',@level2name=N'Value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigInfoPC', @level2type=N'COLUMN',@level2name=N'Summary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否公开，若为公开的全局参数，则无需登录就能获取到' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigInfoPC', @level2type=N'COLUMN',@level2name=N'IsPublic'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'PC的配置信息表，与UserInfo和PluginInfo有关联，但是可能有些配置信息只在一个字段或两个字段都为空（系统配置等）所以没有设置外键，' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigInfoPC'
GO
/****** Object:  Table [dbo].[ConfigInfo]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ConfigInfo](
	[ConfigId] [int] IDENTITY(1,1) NOT NULL,
	[ConfigCategoryCode] [varchar](64) NULL,
	[UserUId] [varchar](64) NULL,
	[PlugInCode] [varchar](64) NULL,
	[Key] [varchar](256) NULL,
	[Value] [varchar](512) NULL,
	[Summary] [nvarchar](512) NULL,
	[IsPublic] [bit] NOT NULL,
 CONSTRAINT [PK_CONFIGINFO] PRIMARY KEY CLUSTERED 
(
	[ConfigId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增长主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigInfo', @level2type=N'COLUMN',@level2name=N'ConfigId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置分类
   全局配置:GlobalConfig
   常规系统配置:SysCommonConfig
   常规插件配置:PluginConfig
   个人系统配置:PersonalSysConfig
   个人插件配置:PersonalPluginConfig' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigInfo', @level2type=N'COLUMN',@level2name=N'ConfigCategoryCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户登录id，如果是用户个人配置则此字段有值，否则没有值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigInfo', @level2type=N'COLUMN',@level2name=N'UserUId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件标识，如果是插件配置则有值否则为空' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigInfo', @level2type=N'COLUMN',@level2name=N'PlugInCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置的键值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigInfo', @level2type=N'COLUMN',@level2name=N'Key'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置的值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigInfo', @level2type=N'COLUMN',@level2name=N'Value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigInfo', @level2type=N'COLUMN',@level2name=N'Summary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置信息表，与UserInfo和PluginInfo有关联，但是可能有些配置信息只在一个字段或两个字段都为空（系统配置等）所以没有设置外键，' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ConfigInfo'
GO
/****** Object:  Table [dbo].[ClientType]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientType](
	[ClientType] [nvarchar](64) NOT NULL,
	[DisplayName] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](512) NULL,
 CONSTRAINT [PK_CLIENTTYPE] PRIMARY KEY CLUSTERED 
(
	[ClientType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接入端的类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientType', @level2type=N'COLUMN',@level2name=N'ClientType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接入终端类型的显示名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientType', @level2type=N'COLUMN',@level2name=N'DisplayName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接入终端类型的描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientType', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述终端的类型，包含硬件类型（如PC，平板，Phone）和系统类型（如Window, Android, iOS）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientType'
GO
/****** Object:  Table [dbo].[City]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[City](
	[City_Code] [varchar](9) NOT NULL,
	[City_Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CITY] PRIMARY KEY CLUSTERED 
(
	[City_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'天气预报中城市信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'City'
GO
/****** Object:  Table [dbo].[ChatTerminal]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatTerminal](
	[Code] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_ChatTerminal] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatRemoveLog]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatRemoveLog](
	[ChatLogId] [bigint] NOT NULL,
	[ChatId] [nvarchar](64) NOT NULL,
	[ChatTitle] [nvarchar](64) NULL,
	[ChatType] [nvarchar](64) NOT NULL,
	[SenderUid] [nvarchar](64) NOT NULL,
	[SenderName] [nvarchar](64) NULL,
	[SendTime] [datetime] NOT NULL,
	[SearchContent] [nvarchar](4000) NOT NULL,
	[MsgType] [nvarchar](64) NOT NULL,
	[SessionId] [nvarchar](256) NULL,
	[MsgContent] [nvarchar](4000) NOT NULL,
	[CreateUser] [nvarchar](64) NOT NULL,
	[CreateDatetime] [datetime] NOT NULL,
	[TerminalCode] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_ChatLogRemoveRecord] PRIMARY KEY CLUSTERED 
(
	[ChatLogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatLog]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatLog](
	[ChatLogId] [bigint] IDENTITY(1,1) NOT NULL,
	[ChatId] [nvarchar](64) NOT NULL,
	[ChatTitle] [nvarchar](64) NULL,
	[ChatType] [nvarchar](64) NOT NULL,
	[SenderUid] [nvarchar](64) NOT NULL,
	[SenderName] [nvarchar](64) NULL,
	[SendTime] [datetime] NOT NULL,
	[SearchContent] [nvarchar](4000) NOT NULL,
	[MsgType] [nvarchar](64) NOT NULL,
	[SessionId] [nvarchar](256) NULL,
	[MsgContent] [nvarchar](4000) NOT NULL,
	[CreateUser] [nvarchar](64) NOT NULL,
	[CreateDatetime] [datetime] NOT NULL,
	[TerminalCode] [nvarchar](64) NOT NULL,
	[TerminalLogId] [int] NOT NULL,
 CONSTRAINT [PK_SingleChatLog] PRIMARY KEY CLUSTERED 
(
	[ChatLogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Authentication]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authentication](
	[UID] [nvarchar](64) NOT NULL,
	[AuthMode] [nvarchar](64) NOT NULL,
	[ExtID] [nvarchar](64) NULL,
	[Password] [nvarchar](256) NOT NULL,
	[LastUpdateTime] [datetime] NOT NULL,
	[LastUseTime] [datetime] NULL,
 CONSTRAINT [PK_Authentication_1] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppPrivilege]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppPrivilege](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[DisplayName] [nvarchar](128) NOT NULL,
	[BuaAppCode] [nvarchar](128) NOT NULL,
	[BuaPrivilegeCode] [nvarchar](128) NOT NULL,
	[EnableSync] [bit] NOT NULL,
	[SyncIntervalTime] [int] NOT NULL,
	[SyncLastTime] [datetime] NOT NULL,
	[SyncTime] [datetime] NOT NULL,
	[CreateUid] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateUid] [nvarchar](64) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_APPPRIVILEGE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPrivilege', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限的显示名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPrivilege', @level2type=N'COLUMN',@level2name=N'DisplayName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应的Bua的应用系统标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPrivilege', @level2type=N'COLUMN',@level2name=N'BuaAppCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应的Bua的权限标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPrivilege', @level2type=N'COLUMN',@level2name=N'BuaPrivilegeCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用同步' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPrivilege', @level2type=N'COLUMN',@level2name=N'EnableSync'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'同步的间隔时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPrivilege', @level2type=N'COLUMN',@level2name=N'SyncIntervalTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上次同步时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPrivilege', @level2type=N'COLUMN',@level2name=N'SyncLastTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'同步开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPrivilege', @level2type=N'COLUMN',@level2name=N'SyncTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPrivilege', @level2type=N'COLUMN',@level2name=N'CreateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPrivilege', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPrivilege', @level2type=N'COLUMN',@level2name=N'UpdateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPrivilege', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应用的权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppPrivilege'
GO
/****** Object:  Table [dbo].[ApplyDeviceBind]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ApplyDeviceBind](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserUid] [varchar](64) NOT NULL,
	[DeviceId] [varchar](64) NOT NULL,
	[Ip] [varchar](32) NULL,
	[Status] [int] NOT NULL,
	[ApplyTime] [datetime] NULL,
	[CheckTime] [datetime] NULL,
	[CheckUser] [varchar](64) NULL,
	[Description] [nvarchar](256) NULL,
 CONSTRAINT [PK_APPLYDEVICEBIND] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识，自增' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyDeviceBind', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyDeviceBind', @level2type=N'COLUMN',@level2name=N'UserUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyDeviceBind', @level2type=N'COLUMN',@level2name=N'DeviceId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核状态
   0:待审核
   1:审核通过
   2:审核不通过' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyDeviceBind', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyDeviceBind', @level2type=N'COLUMN',@level2name=N'ApplyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyDeviceBind', @level2type=N'COLUMN',@level2name=N'CheckTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyDeviceBind', @level2type=N'COLUMN',@level2name=N'CheckUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyDeviceBind', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请设备绑定' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplyDeviceBind'
GO
/****** Object:  Table [dbo].[ApplicationCategory]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationCategory](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[DisplayName] [nvarchar](128) NOT NULL,
	[Seq] [int] NOT NULL,
	[CreateUid] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateUid] [nvarchar](64) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_APPLICATIONCATEGORY] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应用的分类，一个应用可以有多个分类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ApplicationCategory'
GO
/****** Object:  Table [dbo].[IMGroup]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IMGroup](
	[GroupId] [nvarchar](128) NOT NULL,
	[GroupDisplayName] [nvarchar](64) NOT NULL,
	[Description] [nvarchar](4000) NOT NULL,
	[Announcement] [nvarchar](4000) NULL,
	[Owner] [nvarchar](64) NOT NULL,
	[PublishId] [int] IDENTITY(100000,1) NOT NULL,
	[CreateUid] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateUid] [nvarchar](64) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_IMGroup] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Image](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[HashCode] [char](128) NULL,
	[Data] [image] NOT NULL,
 CONSTRAINT [PK_IMAGE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'存储图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Image'
GO
/****** Object:  Table [dbo].[Icon]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Icon](
	[IconCode] [varchar](64) NOT NULL,
	[IconType] [varchar](64) NULL,
	[UserUid] [varchar](64) NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_ICON] PRIMARY KEY CLUSTERED 
(
	[IconCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键，文件名，guid标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Icon', @level2type=N'COLUMN',@level2name=N'IconCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型分类，暂时包括默认头像Default，用户上传头像Custom' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Icon', @level2type=N'COLUMN',@level2name=N'IconType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Icon', @level2type=N'COLUMN',@level2name=N'UserUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Icon', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
/****** Object:  Table [dbo].[IMGroupUserRole]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IMGroupUserRole](
	[Role] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_IMGroupUserRole] PRIMARY KEY CLUSTERED 
(
	[Role] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PluginInfoTemp]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PluginInfoTemp](
	[PluginCode] [varchar](64) NOT NULL,
	[PluginCateCode] [varchar](64) NULL,
	[HashCode] [varchar](64) NULL,
	[Version] [varchar](32) NULL,
	[VersionSummary] [text] NULL,
	[DisplayName] [nvarchar](256) NULL,
	[DirectoryName] [nvarchar](256) NULL,
	[TypeFullName] [varchar](512) NULL,
	[FileName] [varchar](256) NULL,
	[IsNeed] [bit] NULL,
	[IsDefault] [bit] NULL,
	[IsUse] [bit] NULL,
	[isPublic] [bit] NULL,
	[AppCode] [varchar](64) NULL,
	[PrivilegeCode] [varchar](64) NULL,
	[IsNew] [bit] NULL,
	[PreVersionPCs] [nvarchar](1024) NULL,
	[Sequence] [int] NULL,
	[PluginSummary] [text] NULL,
	[PluginUrl] [varchar](1024) NULL,
	[CompanyName] [nvarchar](1024) NULL,
	[CompanyTel] [varchar](64) NULL,
	[CompanyLinkman] [varchar](64) NULL,
	[CompanyHomePage] [varchar](1024) NULL,
	[ActionCode] [varchar](64) NULL,
	[ActionSummary] [text] NULL,
	[IsIgnoreConfig] [bit] NULL,
 CONSTRAINT [PK_PLUGININFOTEMP] PRIMARY KEY CLUSTERED 
(
	[PluginCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'PluginCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件分类标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'PluginCateCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'哈希码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'HashCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'VersionSummary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件显示名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'DisplayName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'存放目录名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'DirectoryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性表示插件实例化的类型全名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'TypeFullName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件入口程序文件名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'FileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否必须，参见PluginInfo表中该字段描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'IsNeed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否默认，参见PluginInfo表中该字段描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'IsDefault'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'IsUse'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应用系统标识，若IsPublic为false，则需要验证此应用系统标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'AppCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限标识，若IsPublic为false，则需要验证此权限标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'PrivilegeCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是最新版本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'IsNew'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上一个版本的插件codes用_$&_分割' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'PreVersionPCs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顺序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'PluginSummary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'web插件Url地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'PluginUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件开发公司名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'CompanyName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件开发公司电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'CompanyTel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件开发公司联系人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'CompanyLinkman'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件开发公司网站首页地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'CompanyHomePage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'ActionCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'ActionSummary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录是否忽略配置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfoTemp', @level2type=N'COLUMN',@level2name=N'IsIgnoreConfig'
GO
/****** Object:  Table [dbo].[PluginInfo]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PluginInfo](
	[PluginCode] [varchar](64) NOT NULL,
	[PluginCateCode] [varchar](64) NULL,
	[HashCode] [varchar](64) NULL,
	[Version] [varchar](32) NULL,
	[DisplayName] [nvarchar](256) NULL,
	[DirectoryName] [nvarchar](256) NULL,
	[TypeFullName] [varchar](512) NULL,
	[FileName] [varchar](256) NULL,
	[IsNeed] [bit] NULL,
	[IsDefault] [bit] NULL,
	[IsUse] [bit] NULL,
	[isPublic] [bit] NULL,
	[AppCode] [varchar](64) NULL,
	[PrivilegeCode] [varchar](64) NULL,
	[isNew] [bit] NULL,
	[Sequence] [int] NULL,
	[Summary] [text] NULL,
	[PluginUrl] [varchar](1024) NULL,
	[CompanyName] [nvarchar](1024) NULL,
	[CompanyTel] [varchar](64) NULL,
	[CompanyLinkman] [nvarchar](64) NULL,
	[CompanyHomePage] [varchar](1024) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUid] [varchar](64) NULL,
	[LastModTime] [datetime] NULL,
	[LastModUid] [varchar](64) NULL,
 CONSTRAINT [PK_PLUGININFO] PRIMARY KEY CLUSTERED 
(
	[PluginCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'PluginCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件分类标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'PluginCateCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'哈希码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'HashCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件的最新版本名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件显示名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'DisplayName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件存放的文件夹名称，也作为插件的标识，不能重复' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'DirectoryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性表示插件实例化的类型全名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'TypeFullName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件入口程序文件名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'FileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识插件是否是必须插件，如果是必须插件，则所有用户使用SmartBoxTEST时都必须加载该插件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'IsNeed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是默认插件，用于标识该插件是否默认给所有用户使用，' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'IsDefault'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'IsUse'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应用系统标识，若IsPublic为false，则需要验证此应用系统标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'AppCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限标识，若IsPublic为false，则需要验证此权限标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'PrivilegeCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否有最新版本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'isNew'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件默认排序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'Summary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'web插件的Url地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'PluginUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件开发公司名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'CompanyName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件开发公司电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'CompanyTel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件开发公司联系人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'CompanyLinkman'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件开发公司网站首页地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'CompanyHomePage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件创建人标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'CreateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件最后修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'LastModTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件最后修改人标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginInfo', @level2type=N'COLUMN',@level2name=N'LastModUid'
GO
/****** Object:  Table [dbo].[PluginCategory]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PluginCategory](
	[PluginCateId] [int] IDENTITY(1,1) NOT NULL,
	[PluginCateCode] [varchar](64) NULL,
	[DisplayName] [nvarchar](256) NULL,
 CONSTRAINT [PK_PLUGINCATEGORY] PRIMARY KEY CLUSTERED 
(
	[PluginCateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增长主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginCategory', @level2type=N'COLUMN',@level2name=N'PluginCateId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件分类标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginCategory', @level2type=N'COLUMN',@level2name=N'PluginCateCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件分类的显示名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginCategory', @level2type=N'COLUMN',@level2name=N'DisplayName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件类型如面板类插件，后台服务类插件，主菜单类插件等,Action' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PluginCategory'
GO
/****** Object:  Table [dbo].[PersonSetting]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonSetting](
	[Uid] [nvarchar](64) NOT NULL,
	[ClientType] [nvarchar](64) NOT NULL,
	[Key] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_PERSONSETTING] PRIMARY KEY CLUSTERED 
(
	[Uid] ASC,
	[ClientType] ASC,
	[Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置的用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonSetting', @level2type=N'COLUMN',@level2name=N'Uid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用的应用列表，以XML形式保存' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonSetting', @level2type=N'COLUMN',@level2name=N'Value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户的个人配置，如使用的应用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonSetting'
GO
/****** Object:  Table [dbo].[Pattern]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pattern](
	[patternId] [uniqueidentifier] NOT NULL,
	[deviceId] [varchar](64) NULL,
	[UserUid] [varchar](64) NOT NULL,
	[HasPattern] [varchar](5) NOT NULL,
	[Pattern] [varchar](15) NULL,
 CONSTRAINT [PK_LoginPattern] PRIMARY KEY CLUSTERED 
(
	[patternId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[PAGESELECT]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<wangshaoming>
-- Create date: <2008-12-12>
-- Description:	<分页存储过程>
-- =============================================
CREATE PROCEDURE [dbo].[PAGESELECT]
	@SQLPARAMS nvarchar(2000)='', --查询条件
	@PAGESIZE int=20,--每页的记录数
	@PAGEINDEX int=0, --第几页,默认第一页
	@SQLTABLE varchar(5000),--要查询的表或视图,也可以一句sql语句
	@SQLCOLUMNS varchar(4000),--查询的字段
	@SQLPK varchar(50),--主键 
	@SQLORDER varchar(200),--排序
	@Count int=-1 output
AS
BEGIN	
	SET NOCOUNT ON;
	DECLARE @PAGELOWERBOUND INT 
	DECLARE @PAGEUPPERBOUND INT
	DECLARE @SQLSTR nvarchar(4000)
	--获取记录数
	--IF @PAGEINDEX=0  --可根据实际要求修改条件,如果是总是获取记录数
	--BEGIN
	set @SQLSTR=N'select @sCount=count(1) FROM '+@SQLTABLE+' WHERE 1=1'+@SQLPARAMS
	Exec sp_executesql @sqlstr,N'@sCount int outPut',@Count output
	--END
	--ELSE
	--BEGIN
	--	SET @COUNT =-1 
	--END


	SET @PAGELOWERBOUND= @PAGEINDEX *@PAGESIZE+1
	SET @PAGEUPPERBOUND = @PAGELOWERBOUND +@PAGESIZE-1
	IF @SQLORDER=''
	BEGIN
		SET @SQLORDER='ORDER BY '+@SQLPK
	END
	
	
SET @SQLSTR=N'SELECT * FROM (select '+@SQLCOLUMNS+',ROW_NUMBER() Over('+@SQLORDER+') as PAGESELECT_rowNum FROM '+@SQLTABLE+' WHERE 1=1'+@SQLPARAMS+ ') as PAGESELECT_TABLE
				where PAGESELECT_rowNum between '+STR(@PAGELOWERBOUND)+' and '+STR(@PAGEUPPERBOUND)+' '

print @SQLSTR
Exec sp_executesql @SQLSTR

SELECT @COUNT

END
GO
/****** Object:  Table [dbo].[Package4Out]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Package4Out](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Type] [nvarchar](64) NOT NULL,
	[ClientType] [nvarchar](64) NOT NULL,
	[DisplayName] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](512) NOT NULL,
	[Version] [nvarchar](64) NOT NULL,
	[BuildVer] [int] NOT NULL,
	[DownloadUri] [nvarchar](128) NOT NULL,
	[CreateUid] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateUid] [nvarchar](64) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_PACKAGE4OUT] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Package的ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4Out', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Package的包名（例如：com.beyondbit.SmartBoxTEST.phone）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4Out', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Package的显示名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4Out', @level2type=N'COLUMN',@level2name=N'DisplayName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Package的描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4Out', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对外发布的版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4Out', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编译的版本号，解决Bug的快速更新时，发布版本不变，而实际产生变化的情况。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4Out', @level2type=N'COLUMN',@level2name=N'BuildVer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下载地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4Out', @level2type=N'COLUMN',@level2name=N'DownloadUri'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4Out', @level2type=N'COLUMN',@level2name=N'CreateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4Out', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4Out', @level2type=N'COLUMN',@level2name=N'UpdateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4Out', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'外部应用的发布包信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4Out'
GO
/****** Object:  Table [dbo].[Package4AI]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Package4AI](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Type] [nvarchar](64) NOT NULL,
	[ClientType] [nvarchar](64) NULL,
	[DisplayName] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](512) NOT NULL,
	[Version] [nvarchar](64) NOT NULL,
	[BuildVer] [int] NOT NULL,
	[DownloadUri] [nvarchar](128) NOT NULL,
	[CreateUid] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateUid] [nvarchar](64) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_PACKAGE4AI] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Package的ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4AI', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Package的包名（例如：com.beyondbit.SmartBoxTEST.phone）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4AI', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Package的显示名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4AI', @level2type=N'COLUMN',@level2name=N'DisplayName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Package的描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4AI', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对外发布的版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4AI', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编译的版本号，解决Bug的快速更新时，发布版本不变，而实际产生变化的情况。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4AI', @level2type=N'COLUMN',@level2name=N'BuildVer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下载地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4AI', @level2type=N'COLUMN',@level2name=N'DownloadUri'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4AI', @level2type=N'COLUMN',@level2name=N'CreateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4AI', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4AI', @level2type=N'COLUMN',@level2name=N'UpdateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4AI', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Android和iOS的发布包信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package4AI'
GO
/****** Object:  Table [dbo].[Notice]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Notice](
	[NoticeId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](1024) NULL,
	[PublicScope] [char](2) NOT NULL,
	[Content] [text] NULL,
	[PublishUid] [varchar](64) NOT NULL,
	[PublishTime] [datetime] NOT NULL,
	[BeginTime] [datetime] NULL,
	[ExpireTime] [datetime] NULL,
 CONSTRAINT [PK_NOTICE] PRIMARY KEY CLUSTERED 
(
	[NoticeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通知通告标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notice', @level2type=N'COLUMN',@level2name=N'NoticeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通知通告标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notice', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公布范围，01所有用户，02当前在线用户，03特定用户
   发给所有用户时暂不记录查看状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notice', @level2type=N'COLUMN',@level2name=N'PublicScope'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通知通告内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notice', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发布人标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notice', @level2type=N'COLUMN',@level2name=N'PublishUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发布时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notice', @level2type=N'COLUMN',@level2name=N'PublishTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效期开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notice', @level2type=N'COLUMN',@level2name=N'BeginTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效期结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notice', @level2type=N'COLUMN',@level2name=N'ExpireTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通知通告表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notice'
GO
/****** Object:  Table [dbo].[MessageCache]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MessageCache](
	[MessageId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MessageBody] [text] NULL,
	[Target] [text] NULL,
	[MessageCategory] [varchar](32) NULL,
 CONSTRAINT [PK_MESSAGECACHE] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增长id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MessageCache', @level2type=N'COLUMN',@level2name=N'MessageId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'被序列化好的消息内容。SmartBoxTEST server 会直接将该消息内容发送到SmartBoxTEST 客户端，' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MessageCache', @level2type=N'COLUMN',@level2name=N'MessageBody'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目标帐号，PUSH时，为NULL表示发送给所有人，发给多个人uid用英文逗号隔开；REQ时，只支持单个人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MessageCache', @level2type=N'COLUMN',@level2name=N'Target'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息分类，PUSH/REQ PUSH表示将数据推送至客户端，Target为要推送的目标；REQ表示模拟客户端请求（现只支持异步请求）,Target为模拟的客户端uid' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MessageCache', @level2type=N'COLUMN',@level2name=N'MessageCategory'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息缓存表： 与其他系统交互时，通过webservice等方式将外部系统数据写入此表中；SmartBoxTESTserver 会定时扫描改表，并处理；' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MessageCache'
GO
/****** Object:  Table [dbo].[Manager]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Manager](
	[UserUid] [varchar](50) NOT NULL,
	[UserPwd] [varchar](50) NULL,
	[IsMain] [bit] NULL,
 CONSTRAINT [PK_MANAGER] PRIMARY KEY CLUSTERED 
(
	[UserUid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户登录名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Manager', @level2type=N'COLUMN',@level2name=N'UserUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Manager', @level2type=N'COLUMN',@level2name=N'UserPwd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否主管理员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Manager', @level2type=N'COLUMN',@level2name=N'IsMain'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Web控制台非SSO方式登录时的身份认证表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Manager'
GO
/****** Object:  Table [dbo].[LogInfo]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogInfo](
	[LogId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Msg] [text] NULL,
	[UserUid] [nvarchar](128) NULL,
	[Time] [datetime] NULL,
	[Type] [nvarchar](16) NULL,
	[Ip] [nvarchar](32) NULL,
 CONSTRAINT [PK_LOGINFO] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志类型，包括  INFO  EXCEPTION ERROR' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogInfo', @level2type=N'COLUMN',@level2name=N'Type'
GO
/****** Object:  Table [dbo].[UserOnline]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOnline](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [nvarchar](32) NOT NULL,
	[DeviceID] [nvarchar](64) NOT NULL,
	[ClientType] [nvarchar](32) NOT NULL,
	[Status] [int] NOT NULL,
	[LastLoginTime] [datetime] NULL,
	[LastLogoutTime] [datetime] NULL,
 CONSTRAINT [PK_UserOnline] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLoginInfo]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserLoginInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[UserUid] [varchar](64) NOT NULL,
	[Device] [varchar](32) NULL,
	[DeviceId] [varchar](64) NULL,
	[Ip] [varchar](32) NULL,
	[LoginTime] [datetime] NULL,
	[LogoutTime] [datetime] NULL,
	[Result] [varchar](16) NULL,
 CONSTRAINT [PK_USERLOGININFO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserLoginInfo', @level2type=N'COLUMN',@level2name=N'UserUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录结果 
   成功 SUCCESS
   禁用 DISABLE
   遗失 LOST
   密码错误 PASS_ERROR
   
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserLoginInfo', @level2type=N'COLUMN',@level2name=N'Result'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户登录信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserLoginInfo'
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserInfo](
	[UserUId] [varchar](64) NOT NULL,
	[Signature] [text] NULL,
	[UserIconCode] [varchar](64) NULL,
	[Status] [varchar](16) NULL,
	[Lock] [bit] NOT NULL,
	[LastLockTime] [datetime] NULL,
	[LastUnLockTime] [datetime] NULL,
	[LastLoginIP] [varchar](32) NULL,
	[LastLoginTime] [datetime] NULL,
	[LastLogoutTime] [datetime] NULL,
 CONSTRAINT [PK_USERINFO] PRIMARY KEY CLUSTERED 
(
	[UserUId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'UserUId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户签名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Signature'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前使用的头像标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'UserIconCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态 online/offline' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后登录IP地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'LastLoginIP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后登录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'LastLoginTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后退出时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'LastLogoutTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户基本信息表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo'
GO
/****** Object:  Table [dbo].[UserAuthLog]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAuthLog](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UID] [nvarchar](32) NOT NULL,
	[DeviceID] [nvarchar](64) NOT NULL,
	[AuthMode] [nvarchar](32) NOT NULL,
	[LoginTime] [datetime] NOT NULL,
	[Result] [bit] NOT NULL,
	[ErrorPwd] [nvarchar](64) NULL,
 CONSTRAINT [PK_UserAuthLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UpdateInfo]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UpdateInfo](
	[Device] [varchar](32) NOT NULL,
	[Version] [varchar](64) NOT NULL,
	[IsEnable] [bit] NOT NULL,
	[Url] [varchar](512) NULL,
	[LocalFile] [varchar](256) NULL,
 CONSTRAINT [PK_UPDATEINFO] PRIMARY KEY CLUSTERED 
(
	[Device] ASC,
	[Version] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备 ANDROID_PAD ANDROID_PHONE IPHONE IPAD 等' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UpdateInfo', @level2type=N'COLUMN',@level2name=N'Device'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UpdateInfo', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UpdateInfo', @level2type=N'COLUMN',@level2name=N'IsEnable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新文件的url' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UpdateInfo', @level2type=N'COLUMN',@level2name=N'Url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地文件名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UpdateInfo', @level2type=N'COLUMN',@level2name=N'LocalFile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UpdateInfo'
GO
/****** Object:  Table [dbo].[SystemConfig]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemConfig](
	[Key] [nvarchar](64) NOT NULL,
	[Value] [nvarchar](512) NOT NULL,
 CONSTRAINT [PK_SystemConfig] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StyleHomeItem]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StyleHomeItem](
	[StyleID] [int] NOT NULL,
	[App4AIID] [int] NOT NULL,
	[Image] [nvarchar](256) NOT NULL,
	[DispalyName] [nvarchar](64) NOT NULL,
	[Seq] [int] NOT NULL,
 CONSTRAINT [PK_StyleHomeItem] PRIMARY KEY CLUSTERED 
(
	[StyleID] ASC,
	[App4AIID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Style]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Style](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](64) NOT NULL,
	[DipalsyName] [nvarchar](256) NOT NULL,
	[ClientType] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Style] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 12/01/2014 14:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[Split]
(
    @Input nvarchar(max),
    @Separator nvarchar(max)=',', 
    @RemoveEmptyEntries bit=1 
)
returns @TABLE table 
(
    [Value] nvarchar(max)
) 
as
begin 
    declare @Index int, @Entry nvarchar(max)
    set @Index = charindex(@Separator,@Input)

    while (@Index>0)
    begin
        set @Entry=ltrim(rtrim(substring(@Input, 1, @Index-1)))
        
        if (@RemoveEmptyEntries=0) or (@RemoveEmptyEntries=1 and @Entry<>'')
            begin
                insert into @TABLE([Value]) Values(@Entry)
            end

        set @Input = substring(@Input, @Index+datalength(@Separator)/2, len(@Input))
        set @Index = charindex(@Separator, @Input)
    end
    
    set @Entry=ltrim(rtrim(@Input))
    if (@RemoveEmptyEntries=0) or (@RemoveEmptyEntries=1 and @Entry<>'')
        begin
            insert into @TABLE([Value]) Values(@Entry)
        end

    return
end
GO
/****** Object:  Table [dbo].[SessionStore]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SessionStore](
	[SessionID] [uniqueidentifier] NOT NULL,
	[SessionValue] [xml] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_SessionStore] PRIMARY KEY CLUSTERED 
(
	[SessionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Server]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Server](
	[Domain] [nvarchar](256) NOT NULL,
	[Type] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_SERVER] PRIMARY KEY CLUSTERED 
(
	[Domain] ASC,
	[Type] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Select_Pagination_ex]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--=============================================================================================
--单表查询调用
--****************************************************************************************************
--原始查询语句
--SELECT Addressid,AddressLine1,City FROM [AdventureWorks].[Person].[Address] where (2=2 OR 3=3) AND Addressid > 3000
--****************************************************************************************************
--DECLARE @return_value int,
--  @PAGECOUNT int,
--  @RECORDCOUNT INT
--EXEC @return_value = [dbo].[Select_Pagination]
--  @TableName = N'[AdventureWorks].[Person].[Address]',
--  @Columns = N'Addressid,AddressLine1,City',
--  @CurrentPageIndex = 1,
--  @PageSize = 10,
--  @RecordCount = @RecordCount OUTPUT,
--  @PAGECOUNT = @PAGECOUNT OUTPUT,
--  @OrderByColumnS = N'Addressid Asc',
--  @WHERE = N'(2=2 OR 3=3) AND Addressid > 3000'
--SELECT @PAGECOUNT as N'@PAGECOUNT'
--SELECT @RecordCount as N'@RECORDCOUNT'
--SELECT 'Return Value' = @return_value
--GO
--***************************************************************************************
--连接查询调用
--****************************************************************************************************
--原始查询语句
--select [CustomerID],[TerritoryID],[AccountNumber],[CustomerType],[rowguid],[ModifiedDate],CustomerType.[Name]
--FROM [AdventureWorks].[Sales].[Customer] join customertype on [Sales].[Customer].CustomerType = CustomerType.ID
--order by [Sales].[Customer].ModifiedDate desc,[Sales].[Customer].CustomerID DESC
--****************************************************************************************************
--USE [AdventureWorks]
--GO
--DECLARE @return_value int,
--  @PAGECOUNT int,
--  @RECORDCOUNT INT
--EXEC @return_value = [dbo].[Select_Pagination]
--  @TableName = N'[AdventureWorks].[Sales].[Customer] join customertype on [Sales].[Customer].CustomerType = CustomerType.ID',
--  @Columns = N'[CustomerID],[TerritoryID],[AccountNumber],[CustomerType],[rowguid],[ModifiedDate],CustomerType.[Name]',
--  @CurrentPageIndex = 1916,
--  @PageSize = 10,
--  @RecordCount = @RecordCount OUTPUT,
--  @PAGECOUNT = @PAGECOUNT OUTPUT,
--  @OrderByColumnS = N'[Sales].[Customer].ModifiedDate desc,[Sales].[Customer].CustomerID DESC',
--  @wHERE = N''
--SELECT @PAGECOUNT as N'@PAGECOUNT'
--SELECT @RecordCount as N'@RECORDCOUNT'
--SELECT 'Return Value' = @return_value
--GO
--=============================================================================================

create PROC [dbo].[Select_Pagination_ex](
@TableName nVARCHAR(4000),
@Columns nVARCHAR(4000),
@CurrentPageIndex INT,
@PageSize INT,
@RecordCount INT OUTPUT,
@PAGECOUNT INT OUTPUT,
@OrderByColumns nVARCHAR(1000),
@Where NVarchar(4000),
@WITH VARCHAR(8000)--定义通用表达式，
) AS
BEGIN
DECLARE @COUNT_SQL NVARCHAR(4000)
DECLARE @ParmDefinition NVARCHAR(1000)
SET @ParmDefinition = N'@COUNT INT OUTPUT';
IF @WITH <> N''
 SET @COUNT_SQL = @WITH + N'SELECT @COUNT=COUNT(*) FROM ' + @tablename + N' where 1 = 1 '
ELSE
 SET @COUNT_SQL = N'SELECT @COUNT=COUNT(*) FROM ' + @tablename + N' where 1 = 1 '

IF @WHERE <> N''
  SET @COUNT_SQL = @COUNT_SQL + N' AND (' + @Where + N')'
--PRINT @COUNT_SQL
EXECUTE SP_EXECUTESQL @COUNT_SQL,@ParmDefinition,@COUNT=@RecordCount OUTPUT;
IF (@RecordCount % @PageSize) > 0
  SET @PageCount = @RecordCount / @PageSize + 1
ELSE
  SET @PageCount = @RecordCount / @PageSize
Declare @SQL NVARCHAR(4000)
IF @WITH <> N''
BEGIN
 SET @SQL = @WITH
 SET @Sql = @SQL + N', TMPTABLE as('
END
ELSE
 SET @Sql = N'WITH TMPTABLE as('
set @sql = @sql + N'select ' + @columns + N',ROW_NUMBER() over(order by '
set @Sql = @sql + @orderByColumns
set @sql = @sql + N') ROWNO FROM '
SET @SQL = @SQL + @TABLENAME + N' WHERE 1 = 1 '
IF @WHERE <> N''
  SET @SQL = @SQL + N' AND (' + @WHERE + N')'
SET @SQL = @SQL + N')'
DECLARE @BEGINNO INT
DECLARE @ENDNO INT
SET @BEGINNO = (@CURRENTPAGEINDEX - 1) * @PAGESIZE + 1
SET @ENDNO = @BEGINNO + @PAGESIZE - 1
IF @ENDNO > @RecordCount
  SET @ENDNO = @RecordCount
SET @SQL = @SQL + N'SELECT * FROM TMPTABLE WHERE ROWNO >= ' + CONVERT(NVARCHAR(5), @BEGINNO)
  + N' AND ROWNO <=' + CONVERT(NVARCHAR(5), @ENDNO)

EXEC SP_EXECUTESQL @SQL
END
GO
/****** Object:  Table [dbo].[VersionTrack]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VersionTrack](
	[VersionId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[PluginCode] [varchar](64) NOT NULL,
	[VersionName] [nvarchar](128) NULL,
	[PreVersionId] [int] NULL,
	[FilePath] [nvarchar](512) NULL,
	[VersionStatus] [int] NULL,
	[VersionSummary] [text] NULL,
	[CreateUid] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL,
	[LastModUid] [varchar](64) NULL,
	[LastModTime] [datetime] NULL,
 CONSTRAINT [PK_VERSIONTRACK] PRIMARY KEY CLUSTERED 
(
	[VersionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识版本主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VersionTrack', @level2type=N'COLUMN',@level2name=N'VersionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件标识，如果是SmartBoxTEST主程序则为 SmartBoxTEST' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VersionTrack', @level2type=N'COLUMN',@level2name=N'PluginCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户输入的版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VersionTrack', @level2type=N'COLUMN',@level2name=N'VersionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上一个版本号，如果是第一个版本 与VersionId相同' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VersionTrack', @level2type=N'COLUMN',@level2name=N'PreVersionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物理文件存放的绝对路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VersionTrack', @level2type=N'COLUMN',@level2name=N'FilePath'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'-1 新增未发布 0升级未发布 1正在使用,2已过期; 标当前版本是不是正在使用的，每个插件和主程序只有一个版本是当前使用的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VersionTrack', @level2type=N'COLUMN',@level2name=N'VersionStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本的描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VersionTrack', @level2type=N'COLUMN',@level2name=N'VersionSummary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VersionTrack', @level2type=N'COLUMN',@level2name=N'CreateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VersionTrack', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VersionTrack', @level2type=N'COLUMN',@level2name=N'LastModUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'VersionTrack', @level2type=N'COLUMN',@level2name=N'LastModTime'
GO
/****** Object:  Table [dbo].[UserResource]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserResource](
	[Id] [int] IDENTITY(100000,1) NOT NULL,
	[ExtType] [nvarchar](64) NOT NULL,
	[ExtCode] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_USERRESOURCE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebApplication]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WebApplication](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[AppID] [int] NOT NULL,
	[Uri] [nvarchar](128) NOT NULL,
	[ShortName] [nvarchar](64) NOT NULL,
	[ClientType] [nvarchar](64) NOT NULL,
	[Seq] [int] NOT NULL,
	[IconUri] [nvarchar](128) NULL,
	[CreateUid] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateUid] [nvarchar](64) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[Unit] [varchar](300) NULL,
 CONSTRAINT [PK_WEBAPPLICATION] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Web应用ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WebApplication', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应用ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WebApplication', @level2type=N'COLUMN',@level2name=N'AppID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应用ID地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WebApplication', @level2type=N'COLUMN',@level2name=N'Uri'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图标地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WebApplication', @level2type=N'COLUMN',@level2name=N'IconUri'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WebApplication', @level2type=N'COLUMN',@level2name=N'CreateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WebApplication', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WebApplication', @level2type=N'COLUMN',@level2name=N'UpdateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WebApplication', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Web形式的应用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WebApplication'
GO
/****** Object:  Table [dbo].[Weather]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Weather](
	[City_Code] [varchar](9) NOT NULL,
	[Day] [datetime] NOT NULL,
	[Day_Night] [varchar](20) NOT NULL,
	[WeatherIcoIndex] [varchar](2) NULL,
	[Weather] [varchar](20) NULL,
	[Temperature] [varchar](20) NULL,
	[Wind_Direction] [varchar](20) NULL,
	[Wind_Power] [varchar](20) NULL,
 CONSTRAINT [PK_WEATHER] PRIMARY KEY CLUSTERED 
(
	[City_Code] ASC,
	[Day] ASC,
	[Day_Night] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最近City表中所有城市的最近三天记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Weather'
GO
/****** Object:  Table [dbo].[UserPluginRef]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserPluginRef](
	[UserUId] [varchar](64) NOT NULL,
	[PluginCode] [varchar](64) NOT NULL,
	[IsUse] [bit] NULL,
	[Sequence] [int] NULL,
 CONSTRAINT [PK_USERPLUGINREF] PRIMARY KEY CLUSTERED 
(
	[UserUId] ASC,
	[PluginCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPluginRef', @level2type=N'COLUMN',@level2name=N'UserUId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPluginRef', @level2type=N'COLUMN',@level2name=N'PluginCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录用户引用的插件是否引用，' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPluginRef', @level2type=N'COLUMN',@level2name=N'IsUse'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPluginRef', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
/****** Object:  Table [dbo].[PrivilegeUser]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrivilegeUser](
	[ID] [int] NOT NULL,
	[Uid] [nvarchar](64) NOT NULL,
	[CreateUid] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateUid] [nvarchar](64) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_PRIVILEGEUSER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[Uid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PrivilegeUser', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限对应的用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PrivilegeUser', @level2type=N'COLUMN',@level2name=N'Uid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PrivilegeUser', @level2type=N'COLUMN',@level2name=N'CreateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PrivilegeUser', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PrivilegeUser', @level2type=N'COLUMN',@level2name=N'UpdateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PrivilegeUser', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限对应的用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PrivilegeUser'
GO
/****** Object:  Table [dbo].[UserInfoRef]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserInfoRef](
	[UserUid] [varchar](64) NOT NULL,
	[UserName] [nvarchar](64) NULL,
	[OrgCode] [varchar](64) NULL,
	[OrgName] [nvarchar](64) NULL,
	[UnitCode] [varchar](64) NULL,
	[UnitName] [nvarchar](64) NULL,
 CONSTRAINT [PK_USERINFOREF] PRIMARY KEY CLUSTERED 
(
	[UserUid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户关联表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfoRef'
GO
/****** Object:  Table [dbo].[ActionExtend]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ActionExtend](
	[PluginCode] [varchar](64) NOT NULL,
	[ActionCode] [varchar](64) NULL,
	[Summary] [text] NULL,
 CONSTRAINT [PK_ACTIONEXTEND] PRIMARY KEY CLUSTERED 
(
	[PluginCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插件标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActionExtend', @level2type=N'COLUMN',@level2name=N'PluginCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'action说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActionExtend', @level2type=N'COLUMN',@level2name=N'Summary'
GO
/****** Object:  Table [dbo].[LastestWeather]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LastestWeather](
	[City_Code] [varchar](9) NOT NULL,
	[Hour] [char](2) NOT NULL,
	[Temperature] [varchar](20) NULL,
	[Wind_Direction] [varchar](20) NULL,
	[Wind_Power] [varchar](20) NULL,
	[Wind_Direction_Max] [varchar](20) NULL,
	[Wind_Power_Max] [varchar](20) NULL,
	[Humidity] [varchar](20) NULL,
	[Atmospheric_Pressure] [varchar](20) NULL,
 CONSTRAINT [PK_LASTESTWEATHER] PRIMARY KEY CLUSTERED 
(
	[City_Code] ASC,
	[Hour] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当天天气预报实时信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LastestWeather'
GO
/****** Object:  Table [dbo].[NoticeView]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NoticeView](
	[NoticeId] [uniqueidentifier] NOT NULL,
	[UserUid] [varchar](64) NOT NULL,
	[ViewTime] [datetime] NULL,
 CONSTRAINT [PK_NOTICEVIEW] PRIMARY KEY CLUSTERED 
(
	[NoticeId] ASC,
	[UserUid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通知标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeView', @level2type=N'COLUMN',@level2name=N'NoticeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeView', @level2type=N'COLUMN',@level2name=N'UserUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'浏览时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeView', @level2type=N'COLUMN',@level2name=N'ViewTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通知通告查看记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NoticeView'
GO
/****** Object:  Table [dbo].[IMGroupUser]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IMGroupUser](
	[GroupId] [nvarchar](128) NOT NULL,
	[Uid] [nvarchar](64) NOT NULL,
	[Role] [nvarchar](64) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreateUid] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateUid] [nvarchar](64) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_IMGroupUser] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[Uid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Joined,WaitToAudit' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IMGroupUser', @level2type=N'COLUMN',@level2name=N'Status'
GO
/****** Object:  Table [dbo].[Application]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Application](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[DisplayName] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[Enable] [bit] NOT NULL,
	[EnableType] [nvarchar](64) NULL,
	[PrivilegeID] [int] NULL,
	[CategoryIDs] [nvarchar](128) NULL,
	[CreateUid] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateUid] [nvarchar](64) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[Seq] [int] NOT NULL,
	[Unit] [varchar](300) NULL,
 CONSTRAINT [PK_APPLICATION] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Application的ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Application', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Application的名称（标识）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Application', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Application的显示名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Application', @level2type=N'COLUMN',@level2name=N'DisplayName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Application的描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Application', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Application是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Application', @level2type=N'COLUMN',@level2name=N'Enable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Application的启用模式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Application', @level2type=N'COLUMN',@level2name=N'EnableType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Application的启用模式为权限绑定的方式，权限的ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Application', @level2type=N'COLUMN',@level2name=N'PrivilegeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Application', @level2type=N'COLUMN',@level2name=N'CreateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Application', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Application', @level2type=N'COLUMN',@level2name=N'UpdateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Application', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应用系统的启用，启用方式等设置
   应用系统只单一功能的系统（如短信，邮件，即时通讯），而不是指某一个实现模块
   （例如，PC客户端的一个应用可能由多个插件构成，也有可能一个插件中包含了两个应用的更能）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Application'
GO
/****** Object:  Table [dbo].[Friend]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Friend](
	[FriendId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[FriendGroupId] [numeric](18, 0) NULL,
	[FriendUid] [nvarchar](64) NULL,
	[OwnerUId] [nvarchar](64) NULL,
	[Sequence] [int] NULL,
 CONSTRAINT [PK_FRIEND] PRIMARY KEY CLUSTERED 
(
	[FriendId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'好友标识，自增长主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Friend', @level2type=N'COLUMN',@level2name=N'FriendId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Friend', @level2type=N'COLUMN',@level2name=N'FriendGroupId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'好友登录名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Friend', @level2type=N'COLUMN',@level2name=N'FriendUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'好友所属用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Friend', @level2type=N'COLUMN',@level2name=N'OwnerUId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'好友顺序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Friend', @level2type=N'COLUMN',@level2name=N'Sequence'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'好友表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Friend'
GO
/****** Object:  Table [dbo].[ContactCategory]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactCategory](
	[Id] [int] IDENTITY(100000,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Code] [nvarchar](64) NULL,
	[DisplayName] [nvarchar](64) NULL,
 CONSTRAINT [PK_CONTACTCATEGORY] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacet]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacet](
	[Id] [int] IDENTITY(100000,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ExtType] [nvarchar](64) NULL,
	[ExtCode] [nvarchar](64) NULL,
	[ImageId] [int] NULL,
	[DisplayName] [nvarchar](64) NULL,
	[FirstName] [nvarchar](64) NULL,
	[FistNameExt] [nvarchar](64) NULL,
	[MiddleName] [nvarchar](64) NULL,
	[MiddleNameExt] [nvarchar](64) NULL,
	[LastName] [nvarchar](64) NULL,
	[LastNameExt] [nvarchar](64) NULL,
	[Prefix] [nvarchar](64) NULL,
	[Suffix] [nvarchar](64) NULL,
	[NickName] [nvarchar](64) NULL,
	[Birthday] [datetime] NULL,
 CONSTRAINT [PK_CONTACET] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactProperty]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactProperty](
	[Id] [int] IDENTITY(100000,1) NOT NULL,
	[ContactId] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[TypeName] [nvarchar](32) NULL,
	[TypeSubName] [nvarchar](32) NULL,
	[Value] [nvarchar](512) NOT NULL,
 CONSTRAINT [PK_CONTACTPROPERTY] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactCategoryRef]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactCategoryRef](
	[ContactId] [int] NOT NULL,
	[ContactCategoryId] [int] NOT NULL,
 CONSTRAINT [PK_CONTACTCATEGORYREF] PRIMARY KEY CLUSTERED 
(
	[ContactId] ASC,
	[ContactCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[App4AI]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[App4AI](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[Package4AIID] [int] NOT NULL,
	[AppID] [int] NULL,
	[AppCode] [nvarchar](128) NULL,
	[ClientType] [nvarchar](64) NOT NULL,
	[IconUri] [nvarchar](128) NULL,
	[Seq] [int] NOT NULL,
	[CreateUid] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateUid] [nvarchar](64) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_APP4AI] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'App4Android的ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'App4AI', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Android的Package的ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'App4AI', @level2type=N'COLUMN',@level2name=N'Package4AIID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Application的ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'App4AI', @level2type=N'COLUMN',@level2name=N'AppID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接入端类型（用于区分硬件设备）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'App4AI', @level2type=N'COLUMN',@level2name=N'ClientType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图标地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'App4AI', @level2type=N'COLUMN',@level2name=N'IconUri'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'App4AI', @level2type=N'COLUMN',@level2name=N'Seq'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'App4AI', @level2type=N'COLUMN',@level2name=N'CreateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'App4AI', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'App4AI', @level2type=N'COLUMN',@level2name=N'UpdateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'App4AI', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'针对Android和iOS的Application扩展' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'App4AI'
GO
/****** Object:  Table [dbo].[IMGroupRLM]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IMGroupRLM](
	[Uid] [nvarchar](64) NOT NULL,
	[GroupId] [nvarchar](128) NOT NULL,
	[SendUid] [nvarchar](64) NOT NULL,
	[SendClientId] [nvarchar](64) NOT NULL,
	[Message] [nvarchar](4000) NOT NULL,
	[SendTime] [datetime] NOT NULL,
 CONSTRAINT [PK_IMGroupRLM] PRIMARY KEY CLUSTERED 
(
	[Uid] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomePlanDesign]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomePlanDesign](
	[PlanID] [int] NOT NULL,
	[AppID] [int] NOT NULL,
	[Location] [nvarchar](64) NOT NULL,
	[Size] [nvarchar](64) NOT NULL,
	[Type] [nvarchar](64) NOT NULL,
	[ValueUri] [nvarchar](256) NOT NULL,
	[CreateUid] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateUid] [nvarchar](64) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_HOMEPLANDESIGN] PRIMARY KEY CLUSTERED 
(
	[PlanID] ASC,
	[AppID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HomePlanDesign', @level2type=N'COLUMN',@level2name=N'CreateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HomePlanDesign', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HomePlanDesign', @level2type=N'COLUMN',@level2name=N'UpdateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HomePlanDesign', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
/****** Object:  Table [dbo].[IOSOutsideApplication]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IOSOutsideApplication](
	[ID] [int] IDENTITY(100000,1) NOT NULL,
	[AppID] [int] NOT NULL,
	[Uri] [nvarchar](128) NOT NULL,
	[Scheme] [nvarchar](64) NOT NULL,
	[ClientType] [nvarchar](64) NOT NULL,
	[Seq] [int] NOT NULL,
	[IconUri] [nvarchar](128) NULL,
	[CreateUid] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateUid] [nvarchar](64) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_IOSOUTSIDEAPPLICATION] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IOS外部应用ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IOSOutsideApplication', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应用ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IOSOutsideApplication', @level2type=N'COLUMN',@level2name=N'AppID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应用安装地址Uri
   可以是AppStore的iTunes地址
   也可以是其他任意安装页面' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IOSOutsideApplication', @level2type=N'COLUMN',@level2name=N'Uri'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IOS打开外部应用时需要的Scheme' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IOSOutsideApplication', @level2type=N'COLUMN',@level2name=N'Scheme'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图标地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IOSOutsideApplication', @level2type=N'COLUMN',@level2name=N'IconUri'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IOSOutsideApplication', @level2type=N'COLUMN',@level2name=N'CreateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IOSOutsideApplication', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IOSOutsideApplication', @level2type=N'COLUMN',@level2name=N'UpdateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IOSOutsideApplication', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IOS外部应用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IOSOutsideApplication'
GO
/****** Object:  Table [dbo].[Action4Android]    Script Date: 12/01/2014 14:38:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Action4Android](
	[Name] [nvarchar](128) NOT NULL,
	[App4AIID] [int] NOT NULL,
	[DisplayName] [nvarchar](128) NOT NULL,
	[ShortName] [nvarchar](64) NOT NULL,
	[IsLaunch] [bit] NOT NULL,
	[IconUri] [nvarchar](128) NULL,
	[Seq] [int] NOT NULL,
	[CreateUid] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateUid] [nvarchar](64) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ACTION4ANDROID] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Action名，用于Action的调用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Action4Android', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应用的ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Action4Android', @level2type=N'COLUMN',@level2name=N'App4AIID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否为启动Action' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Action4Android', @level2type=N'COLUMN',@level2name=N'IsLaunch'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'action的图标地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Action4Android', @level2type=N'COLUMN',@level2name=N'IconUri'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Action4Android', @level2type=N'COLUMN',@level2name=N'Seq'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Action4Android', @level2type=N'COLUMN',@level2name=N'CreateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Action4Android', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Action4Android', @level2type=N'COLUMN',@level2name=N'UpdateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Action4Android', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Android打Action设置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Action4Android'
GO
/****** Object:  Default [DF_Application_Seq]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[Application] ADD  CONSTRAINT [DF_Application_Seq]  DEFAULT ((1)) FOR [Seq]
GO
/****** Object:  Default [DF_ConfigInfo_IsPublic]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[ConfigInfo] ADD  CONSTRAINT [DF_ConfigInfo_IsPublic]  DEFAULT ((0)) FOR [IsPublic]
GO
/****** Object:  Default [DF_ConfigTemp_IsPublic]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[ConfigTemp] ADD  CONSTRAINT [DF_ConfigTemp_IsPublic]  DEFAULT ((0)) FOR [IsPublic]
GO
/****** Object:  Default [DF_Device_LockExpireHours]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[Device] ADD  CONSTRAINT [DF_Device_LockExpireHours]  DEFAULT ((-1)) FOR [LockExpireHours]
GO
/****** Object:  Default [DF__OfflineFil__Size__3A81B327]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[FileStore] ADD  CONSTRAINT [DF__OfflineFil__Size__3A81B327]  DEFAULT ((0)) FOR [Size]
GO
/****** Object:  Default [DF__Icon__IconType__03F0984C]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[Icon] ADD  DEFAULT ('CustomHeadIcon') FOR [IconType]
GO
/****** Object:  Default [DF_IMGroupUser_Status]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[IMGroupUser] ADD  CONSTRAINT [DF_IMGroupUser_Status]  DEFAULT (N'Joined') FOR [Status]
GO
/****** Object:  Default [DF__LogInfo__Type__05D8E0BE]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[LogInfo] ADD  DEFAULT ('INFO') FOR [Type]
GO
/****** Object:  Default [DF_Manager_IsMain]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[Manager] ADD  CONSTRAINT [DF_Manager_IsMain]  DEFAULT ((0)) FOR [IsMain]
GO
/****** Object:  Default [DF__MessageCa__Messa__06CD04F7]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[MessageCache] ADD  DEFAULT ('Default') FOR [MessageCategory]
GO
/****** Object:  Default [DF__PluginInf__isPub__07C12930]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[PluginInfo] ADD  DEFAULT ((1)) FOR [isPublic]
GO
/****** Object:  Default [DF__PluginInf__isPub__08B54D69]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[PluginInfoTemp] ADD  DEFAULT ((1)) FOR [isPublic]
GO
/****** Object:  Default [DF_UserInfo_Lock]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_Lock]  DEFAULT ((0)) FOR [Lock]
GO
/****** Object:  Check [CKC_Application_EnableType]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [CKC_Application_EnableType] CHECK  (([EnableType] IS NULL OR ([EnableType]='DefaultFalse' OR [EnableType]='DefaultTrue' OR [EnableType]='Need')))
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [CKC_Application_EnableType]
GO
/****** Object:  Check [CKC_Package4Android_Type]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[Package4AI]  WITH CHECK ADD  CONSTRAINT [CKC_Package4Android_Type] CHECK  (([Type]='Plugin' OR [Type]='Main'))
GO
ALTER TABLE [dbo].[Package4AI] CHECK CONSTRAINT [CKC_Package4Android_Type]
GO
/****** Object:  Check [CKC_VERSIONID_VERSIONT]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[VersionTrack]  WITH CHECK ADD  CONSTRAINT [CKC_VERSIONID_VERSIONT] CHECK  (([VersionId]>=(0)))
GO
ALTER TABLE [dbo].[VersionTrack] CHECK CONSTRAINT [CKC_VERSIONID_VERSIONT]
GO
/****** Object:  ForeignKey [FK_App4Android_Action4Android]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[Action4Android]  WITH CHECK ADD  CONSTRAINT [FK_App4Android_Action4Android] FOREIGN KEY([App4AIID])
REFERENCES [dbo].[App4AI] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Action4Android] CHECK CONSTRAINT [FK_App4Android_Action4Android]
GO
/****** Object:  ForeignKey [FK_ACTIONEX_REFERENCE_PLUGININ]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[ActionExtend]  WITH CHECK ADD  CONSTRAINT [FK_ACTIONEX_REFERENCE_PLUGININ] FOREIGN KEY([PluginCode])
REFERENCES [dbo].[PluginInfo] ([PluginCode])
GO
ALTER TABLE [dbo].[ActionExtend] CHECK CONSTRAINT [FK_ACTIONEX_REFERENCE_PLUGININ]
GO
/****** Object:  ForeignKey [FK_Application_AppForAndroid]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[App4AI]  WITH CHECK ADD  CONSTRAINT [FK_Application_AppForAndroid] FOREIGN KEY([AppID])
REFERENCES [dbo].[Application] ([ID])
GO
ALTER TABLE [dbo].[App4AI] CHECK CONSTRAINT [FK_Application_AppForAndroid]
GO
/****** Object:  ForeignKey [FK_Package_AppForAndroid]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[App4AI]  WITH CHECK ADD  CONSTRAINT [FK_Package_AppForAndroid] FOREIGN KEY([Package4AIID])
REFERENCES [dbo].[Package4AI] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[App4AI] CHECK CONSTRAINT [FK_Package_AppForAndroid]
GO
/****** Object:  ForeignKey [FK_AppPrivilege_Application]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_AppPrivilege_Application] FOREIGN KEY([PrivilegeID])
REFERENCES [dbo].[AppPrivilege] ([ID])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_AppPrivilege_Application]
GO
/****** Object:  ForeignKey [FK_CONTACET_FK_USERRE_USERRESO]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[Contacet]  WITH CHECK ADD  CONSTRAINT [FK_CONTACET_FK_USERRE_USERRESO] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserResource] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Contacet] CHECK CONSTRAINT [FK_CONTACET_FK_USERRE_USERRESO]
GO
/****** Object:  ForeignKey [FK_CONTACTC_FK_USERRE_USERRESO]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[ContactCategory]  WITH CHECK ADD  CONSTRAINT [FK_CONTACTC_FK_USERRE_USERRESO] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserResource] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContactCategory] CHECK CONSTRAINT [FK_CONTACTC_FK_USERRE_USERRESO]
GO
/****** Object:  ForeignKey [FK_CONTACTC_FK_CONTAC_CONTACET]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[ContactCategoryRef]  WITH CHECK ADD  CONSTRAINT [FK_CONTACTC_FK_CONTAC_CONTACET] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contacet] ([Id])
GO
ALTER TABLE [dbo].[ContactCategoryRef] CHECK CONSTRAINT [FK_CONTACTC_FK_CONTAC_CONTACET]
GO
/****** Object:  ForeignKey [FK_CONTACTC_FK_CONTAC_CONTACTC]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[ContactCategoryRef]  WITH CHECK ADD  CONSTRAINT [FK_CONTACTC_FK_CONTAC_CONTACTC] FOREIGN KEY([ContactCategoryId])
REFERENCES [dbo].[ContactCategory] ([Id])
GO
ALTER TABLE [dbo].[ContactCategoryRef] CHECK CONSTRAINT [FK_CONTACTC_FK_CONTAC_CONTACTC]
GO
/****** Object:  ForeignKey [FK_CONTACTP_FK_CONTAC_CONTACET]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[ContactProperty]  WITH CHECK ADD  CONSTRAINT [FK_CONTACTP_FK_CONTAC_CONTACET] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contacet] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContactProperty] CHECK CONSTRAINT [FK_CONTACTP_FK_CONTAC_CONTACET]
GO
/****** Object:  ForeignKey [FK_FRIEND_FRIENDGRO_FRIENDGR]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[Friend]  WITH CHECK ADD  CONSTRAINT [FK_FRIEND_FRIENDGRO_FRIENDGR] FOREIGN KEY([FriendGroupId])
REFERENCES [dbo].[FriendGroup] ([FriendGroupId])
GO
ALTER TABLE [dbo].[Friend] CHECK CONSTRAINT [FK_FRIEND_FRIENDGRO_FRIENDGR]
GO
/****** Object:  ForeignKey [FK_HOMEPLAN_FK_APPLIC_APPLICAT]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[HomePlanDesign]  WITH CHECK ADD  CONSTRAINT [FK_HOMEPLAN_FK_APPLIC_APPLICAT] FOREIGN KEY([AppID])
REFERENCES [dbo].[Application] ([ID])
GO
ALTER TABLE [dbo].[HomePlanDesign] CHECK CONSTRAINT [FK_HOMEPLAN_FK_APPLIC_APPLICAT]
GO
/****** Object:  ForeignKey [FK_HOMEPLAN_FK_HOMEPL_HOMEPLAN]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[HomePlanDesign]  WITH CHECK ADD  CONSTRAINT [FK_HOMEPLAN_FK_HOMEPL_HOMEPLAN] FOREIGN KEY([PlanID])
REFERENCES [dbo].[HomePlan] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HomePlanDesign] CHECK CONSTRAINT [FK_HOMEPLAN_FK_HOMEPL_HOMEPLAN]
GO
/****** Object:  ForeignKey [FK_IMGroupRLM_IMGroupUser]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[IMGroupRLM]  WITH CHECK ADD  CONSTRAINT [FK_IMGroupRLM_IMGroupUser] FOREIGN KEY([GroupId], [Uid])
REFERENCES [dbo].[IMGroupUser] ([GroupId], [Uid])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IMGroupRLM] CHECK CONSTRAINT [FK_IMGroupRLM_IMGroupUser]
GO
/****** Object:  ForeignKey [FK_IMGroupUser_IMGroup]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[IMGroupUser]  WITH CHECK ADD  CONSTRAINT [FK_IMGroupUser_IMGroup] FOREIGN KEY([GroupId])
REFERENCES [dbo].[IMGroup] ([GroupId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IMGroupUser] CHECK CONSTRAINT [FK_IMGroupUser_IMGroup]
GO
/****** Object:  ForeignKey [FK_IMGroupUser_IMGroupUserRole]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[IMGroupUser]  WITH CHECK ADD  CONSTRAINT [FK_IMGroupUser_IMGroupUserRole] FOREIGN KEY([Role])
REFERENCES [dbo].[IMGroupUserRole] ([Role])
GO
ALTER TABLE [dbo].[IMGroupUser] CHECK CONSTRAINT [FK_IMGroupUser_IMGroupUserRole]
GO
/****** Object:  ForeignKey [FK_IOSOUTSI_FK_APPLIC_APPLICAT]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[IOSOutsideApplication]  WITH CHECK ADD  CONSTRAINT [FK_IOSOUTSI_FK_APPLIC_APPLICAT] FOREIGN KEY([AppID])
REFERENCES [dbo].[Application] ([ID])
GO
ALTER TABLE [dbo].[IOSOutsideApplication] CHECK CONSTRAINT [FK_IOSOUTSI_FK_APPLIC_APPLICAT]
GO
/****** Object:  ForeignKey [FK_LASTESTW_CODE=CODE_CITY]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[LastestWeather]  WITH CHECK ADD  CONSTRAINT [FK_LASTESTW_CODE=CODE_CITY] FOREIGN KEY([City_Code])
REFERENCES [dbo].[City] ([City_Code])
GO
ALTER TABLE [dbo].[LastestWeather] CHECK CONSTRAINT [FK_LASTESTW_CODE=CODE_CITY]
GO
/****** Object:  ForeignKey [FK_NOTICEVI_REFERENCE_NOTICE]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[NoticeView]  WITH CHECK ADD  CONSTRAINT [FK_NOTICEVI_REFERENCE_NOTICE] FOREIGN KEY([NoticeId])
REFERENCES [dbo].[Notice] ([NoticeId])
GO
ALTER TABLE [dbo].[NoticeView] CHECK CONSTRAINT [FK_NOTICEVI_REFERENCE_NOTICE]
GO
/****** Object:  ForeignKey [FK_AppPrivilege_PrivilegeUser]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[PrivilegeUser]  WITH CHECK ADD  CONSTRAINT [FK_AppPrivilege_PrivilegeUser] FOREIGN KEY([ID])
REFERENCES [dbo].[AppPrivilege] ([ID])
GO
ALTER TABLE [dbo].[PrivilegeUser] CHECK CONSTRAINT [FK_AppPrivilege_PrivilegeUser]
GO
/****** Object:  ForeignKey [FK_USERINFOREF_USERINFO]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[UserInfoRef]  WITH CHECK ADD  CONSTRAINT [FK_USERINFOREF_USERINFO] FOREIGN KEY([UserUid])
REFERENCES [dbo].[UserInfo] ([UserUId])
GO
ALTER TABLE [dbo].[UserInfoRef] CHECK CONSTRAINT [FK_USERINFOREF_USERINFO]
GO
/****** Object:  ForeignKey [FK_USERPLUG_REFERENCE_PLUGININ]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[UserPluginRef]  WITH CHECK ADD  CONSTRAINT [FK_USERPLUG_REFERENCE_PLUGININ] FOREIGN KEY([PluginCode])
REFERENCES [dbo].[PluginInfo] ([PluginCode])
GO
ALTER TABLE [dbo].[UserPluginRef] CHECK CONSTRAINT [FK_USERPLUG_REFERENCE_PLUGININ]
GO
/****** Object:  ForeignKey [FK_WEATHER_CODE=CODE_CITY]    Script Date: 12/01/2014 14:38:47 ******/
ALTER TABLE [dbo].[Weather]  WITH CHECK ADD  CONSTRAINT [FK_WEATHER_CODE=CODE_CITY] FOREIGN KEY([City_Code])
REFERENCES [dbo].[City] ([City_Code])
GO
ALTER TABLE [dbo].[Weather] CHECK CONSTRAINT [FK_WEATHER_CODE=CODE_CITY]
GO
