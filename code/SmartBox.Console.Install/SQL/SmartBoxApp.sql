USE [SmartBoxApp]
GO
/****** Object:  Table [dbo].[monitor_log]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[monitor_log](
	[log_id] [int] IDENTITY(1,1) NOT NULL,
	[log_df_item] [varchar](128) NULL,
	[log_monitorvalue] [int] NULL,
	[log_datetime] [datetime] NULL,
	[log_df_kind] [varchar](128) NULL,
	[log_df_code] [varchar](128) NULL,
	[log_df_lever] [varchar](128) NULL,
	[log_status] [varchar](128) NULL,
	[log_hostip] [varchar](128) NULL,
	[log_hostname] [varchar](128) NULL,
 CONSTRAINT [PK_MONITOR_LOG] PRIMARY KEY CLUSTERED 
(
	[log_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Monitor_linkman]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Monitor_linkman](
	[lm_id] [int] IDENTITY(1,1) NOT NULL,
	[lm_uid] [varchar](128) NULL,
	[lm_uname] [varchar](128) NULL,
	[lm_udept] [varchar](128) NULL,
	[lm_mobile] [varchar](128) NULL,
	[lm_email] [varchar](128) NULL,
 CONSTRAINT [PK_MONITOR_LINKMAN] PRIMARY KEY CLUSTERED 
(
	[lm_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Monitor_Defind]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Monitor_Defind](
	[df_id] [int] IDENTITY(1,1) NOT NULL,
	[df_kind] [varchar](128) NULL,
	[df_item] [varchar](128) NULL,
	[df_maxvalue] [int] NULL,
	[df_minvalue] [int] NULL,
	[df_lever] [varchar](128) NULL,
	[df_code] [varchar](128) NULL,
	[df_sendway] [varchar](128) NULL,
	[df_receptman] [varchar](1024) NULL,
	[df_startsenddate] [varchar](8) NULL,
	[df_endsenddate] [varchar](8) NULL,
	[df_issend] [varchar](128) NULL,
 CONSTRAINT [PK_MONITOR_DEFIND] PRIMARY KEY CLUSTERED 
(
	[df_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Monitor_Config]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Monitor_Config](
	[cfg_id] [int] IDENTITY(1,1) NOT NULL,
	[cfg_hostname] [varchar](128) NULL,
	[cfg_hostip] [varchar](128) NULL,
	[cfg_file] [text] NULL,
	[cfg_createdate] [datetime] NULL,
	[cfg_createman] [varchar](128) NULL,
	[cfg_updatedate] [datetime] NULL,
	[cfg_updateman] [varchar](128) NULL,
	[cfg_updatestatus] [varchar](128) NULL,
	[cfg_isuse] [varchar](128) NULL,
	[cfg_usedate] [datetime] NULL,
 CONSTRAINT [PK_MONITOR_CONFIG] PRIMARY KEY CLUSTERED 
(
	[cfg_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[monitor_cmd]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[monitor_cmd](
	[cmd_id] [int] IDENTITY(1,1) NOT NULL,
	[cmd_title] [varchar](128) NULL,
	[cmd_code] [varchar](1024) NULL,
	[cmd_senddate] [datetime] NULL,
	[cmd_excudedate] [datetime] NULL,
	[cmd_excuderesult] [varchar](1024) NULL,
	[cmd_hostname] [varchar](128) NULL,
	[cmd_hostip] [varchar](128) NULL,
	[cmd_discription] [varchar](1024) NULL,
 CONSTRAINT [PK_MONITOR_CMD] PRIMARY KEY CLUSTERED 
(
	[cmd_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MobileDevices]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MobileDevices](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[DeviceToken] [varchar](32) NULL,
	[MacAddr] [varchar](20) NULL,
	[UniqueIdentifier] [varchar](300) NULL,
 CONSTRAINT [PK_MOBILEDEVICES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'任何形式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MobileDevices', @level2type=N'COLUMN',@level2name=N'UniqueIdentifier'
GO
/****** Object:  Table [dbo].[GlobalParam]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalParam](
	[ConfigKey] [nvarchar](50) NOT NULL,
	[ConfigValue] [nvarchar](50) NULL,
	[ConfigDesc] [nvarchar](max) NULL,
 CONSTRAINT [PK_GlobalParam] PRIMARY KEY CLUSTERED 
(
	[ConfigKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Feedback](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserUID] [varchar](126) NOT NULL,
	[UserName] [nvarchar](126) NULL,
	[OrgCode] [varchar](64) NULL,
	[OrgName] [nvarchar](64) NULL,
	[UnitCode] [varchar](64) NULL,
	[UnitName] [nvarchar](64) NULL,
	[Content] [text] NOT NULL,
	[SubmitTime] [datetime] NOT NULL,
	[Device] [varchar](32) NOT NULL,
	[DeviceId] [varchar](64) NULL,
 CONSTRAINT [PK_FEEDBACK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'意见内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Feedback', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Feedback', @level2type=N'COLUMN',@level2name=N'SubmitTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Feedback', @level2type=N'COLUMN',@level2name=N'Device'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Feedback', @level2type=N'COLUMN',@level2name=N'DeviceId'
GO
/****** Object:  Table [dbo].[SMC_BUAUserSyncToOutside]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMC_BUAUserSyncToOutside](
	[buso_id] [int] NOT NULL,
	[user_uid] [varchar](50) NULL,
	[sync_bat_no] [int] NULL,
	[sync_time] [datetime] NULL,
	[sync_status] [bit] NULL,
	[sync_user_uid] [varchar](50) NULL,
	[sync_user_name] [varchar](50) NULL,
	[description] [varchar](max) NULL,
	[user_name] [varchar](50) NULL,
 CONSTRAINT [PK_SMC_BUAUSERSYNCTOOUTSIDE] PRIMARY KEY CLUSTERED 
(
	[buso_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1成功0失败' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_BUAUserSyncToOutside', @level2type=N'COLUMN',@level2name=N'sync_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'bua用户同步到外网记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_BUAUserSyncToOutside'
GO
/****** Object:  Table [dbo].[SMC_BUAUserSyncToInside]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMC_BUAUserSyncToInside](
	[busi_id] [int] NOT NULL,
	[user_uid] [varchar](50) NULL,
	[sync_bat_no] [int] NULL,
	[sync_time] [datetime] NULL,
	[sync_status] [bit] NULL,
	[sync_user_uid] [varchar](50) NULL,
	[sync_user_name] [varchar](50) NULL,
	[description] [varchar](max) NULL,
	[user_name] [varchar](50) NULL,
 CONSTRAINT [PK_SMC_BUAUSERSYNCTOINSIDE] PRIMARY KEY CLUSTERED 
(
	[busi_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1成功0失败' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_BUAUserSyncToInside', @level2type=N'COLUMN',@level2name=N'sync_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'bua用户同步到内网记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_BUAUserSyncToInside'
GO
/****** Object:  Table [dbo].[SMC_AutoTableID]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMC_AutoTableID](
	[at_id] [int] IDENTITY(1,1) NOT NULL,
	[AT_TableName] [varchar](50) NULL,
	[AT_MaxID] [int] NULL,
 CONSTRAINT [PK_AutoTableID] PRIMARY KEY CLUSTERED 
(
	[at_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[Select_Pagination_ex]    Script Date: 12/01/2014 14:42:34 ******/
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

CREATE PROC [dbo].[Select_Pagination_ex](
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
/****** Object:  StoredProcedure [dbo].[PAGESELECT]    Script Date: 12/01/2014 14:42:34 ******/
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
	@SQLPARAMS nvarchar(max)='', --查询条件
	@PAGESIZE int=20,--每页的记录数
	@PAGEINDEX int=0, --第几页,默认第一页
	@SQLTABLE nvarchar(max),--要查询的表或视图,也可以一句sql语句
	@SQLCOLUMNS nvarchar(max),--查询的字段
	@SQLPK varchar(50),--主键 
	@SQLORDER varchar(200),--排序
	@Count int=-1 output
AS
BEGIN	
	SET NOCOUNT ON;
	DECLARE @PAGELOWERBOUND INT 
	DECLARE @PAGEUPPERBOUND INT
	DECLARE @SQLSTR nvarchar(max)
	--获取记录数
	IF @PAGEINDEX=0  --可根据实际要求修改条件,如果是总是获取记录数
	BEGIN
	set @SQLSTR=N'select @sCount=count(1) FROM '+@SQLTABLE+' WHERE 1=1'+@SQLPARAMS
	Exec sp_executesql @sqlstr,N'@sCount int outPut',@Count output
	END
	ELSE
	BEGIN
		SET @COUNT =-1 
	END


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
/****** Object:  Table [dbo].[Notifications]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppName] [nvarchar](64) NOT NULL,
	[Total] [int] NULL,
	[Title] [nvarchar](256) NULL,
	[Data] [text] NULL,
	[Receive] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_NOTIFICATIONS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Application中的ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notifications', @level2type=N'COLUMN',@level2name=N'AppName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示数字
   Null：保持不变
   0：不显示数字
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notifications', @level2type=N'COLUMN',@level2name=N'Total'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通知内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notifications', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Json数据' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notifications', @level2type=N'COLUMN',@level2name=N'Data'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notifications', @level2type=N'COLUMN',@level2name=N'Receive'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'推送通知' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Notifications'
GO
/****** Object:  Table [dbo].[NotificationReport]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NotificationReport](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[NotificationID] [bigint] NULL,
	[ReportCode] [varchar](64) NOT NULL,
	[ReportMessage] [nvarchar](256) NOT NULL,
	[AppCode] [varchar](64) NOT NULL,
	[DeviceToken] [varchar](64) NOT NULL,
	[Payload] [nvarchar](max) NOT NULL,
	[NotificationIdentifier] [varchar](64) NOT NULL,
	[ExpirationData] [datetime] NOT NULL,
	[Priority] [int] NOT NULL,
 CONSTRAINT [PK_NOTIFICATIONREPORT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0       No errors encountered
   1       Processing error
   2       Missing device token
   3       Missing topic
   4       Missing payload
   5       Invalid token size
   6       Invalid topic size
   7       Invalid payload size
   8       Invalid token
   10     Shutdown
   255   None (unknown)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'NotificationReport', @level2type=N'COLUMN',@level2name=N'ReportCode'
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Notification](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AppCode] [varchar](64) NOT NULL,
	[DeviceToken] [varchar](64) NOT NULL,
	[Payload] [nvarchar](max) NOT NULL,
	[NotificationIdentifier] [varchar](64) NOT NULL,
	[ExpirationData] [datetime] NOT NULL,
	[Priority] [int] NOT NULL,
	[RetryCount] [int] NOT NULL,
	[IsSuccess] [bit] NULL,
 CONSTRAINT [PK_NOTIFICATION] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserResource]    Script Date: 12/01/2014 14:42:33 ******/
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
/****** Object:  Table [dbo].[UsageLogTemp]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsageLogTemp](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AppName] [varchar](128) NOT NULL,
	[UserUid] [varchar](64) NOT NULL,
	[Device] [varchar](32) NULL,
	[DeviceId] [varchar](64) NULL,
	[OpTime] [datetime] NOT NULL,
 CONSTRAINT [PK_USAGELOGTEMP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsageLogTemp', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应用标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsageLogTemp', @level2type=N'COLUMN',@level2name=N'AppName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsageLogTemp', @level2type=N'COLUMN',@level2name=N'UserUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsageLogTemp', @level2type=N'COLUMN',@level2name=N'Device'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsageLogTemp', @level2type=N'COLUMN',@level2name=N'DeviceId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户打开应该的时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsageLogTemp', @level2type=N'COLUMN',@level2name=N'OpTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'app使用统计临时表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsageLogTemp'
GO
/****** Object:  Table [dbo].[UsageLogDaily]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsageLogDaily](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AppName] [varchar](128) NOT NULL,
	[AppTitle] [nvarchar](512) NULL,
	[UserUid] [varchar](64) NOT NULL,
	[UserName] [nvarchar](64) NULL,
	[OrgCode] [varchar](64) NULL,
	[OrgName] [nvarchar](64) NULL,
	[UnitCode] [varchar](64) NULL,
	[UnitName] [nvarchar](64) NULL,
	[Device] [varchar](32) NULL,
	[DeviceId] [varchar](64) NULL,
	[OpTime] [datetime] NOT NULL,
	[UsageCount] [int] NOT NULL,
 CONSTRAINT [PK_USAGELOGDAILY] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自增标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsageLogDaily', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'应用标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsageLogDaily', @level2type=N'COLUMN',@level2name=N'AppName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsageLogDaily', @level2type=N'COLUMN',@level2name=N'UserUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户显示名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsageLogDaily', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsageLogDaily', @level2type=N'COLUMN',@level2name=N'Device'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsageLogDaily', @level2type=N'COLUMN',@level2name=N'DeviceId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户打开应该的时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsageLogDaily', @level2type=N'COLUMN',@level2name=N'OpTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'app每天的使用统计' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UsageLogDaily'
GO
/****** Object:  Table [dbo].[SMC_UserList]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMC_UserList](
	[UL_ID] [int] NOT NULL,
	[Role_ID] [int] NULL,
	[UL_UID] [varchar](50) NOT NULL,
	[Unit_ID] [varchar](300) NULL,
	[UL_PWD] [varchar](50) NOT NULL,
	[UL_Name] [varchar](50) NULL,
	[UL_Demo] [text] NULL,
	[UL_MobilePhone] [varchar](50) NULL,
	[UL_MailAddress] [varchar](500) NULL,
	[UL_CreatedTime] [datetime] NULL,
	[UL_CreatedUser] [varchar](50) NULL,
	[UL_UpdateTime] [datetime] NULL,
	[UL_UpdateUser] [varchar](50) NULL,
	[UL_Sequence] [int] NULL,
	[UL_Gender] [varchar](10) NULL,
	[UL_IsMain] [bit] NULL,
 CONSTRAINT [PK_UserList] PRIMARY KEY CLUSTERED 
(
	[UL_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_UserList', @level2type=N'COLUMN',@level2name=N'Role_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_UserList', @level2type=N'COLUMN',@level2name=N'UL_UID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_UserList', @level2type=N'COLUMN',@level2name=N'Unit_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_UserList', @level2type=N'COLUMN',@level2name=N'UL_PWD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户全名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_UserList', @level2type=N'COLUMN',@level2name=N'UL_Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_UserList', @level2type=N'COLUMN',@level2name=N'UL_Demo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_UserList', @level2type=N'COLUMN',@level2name=N'UL_MobilePhone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮件地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_UserList', @level2type=N'COLUMN',@level2name=N'UL_MailAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_UserList', @level2type=N'COLUMN',@level2name=N'UL_CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_UserList', @level2type=N'COLUMN',@level2name=N'UL_CreatedUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_UserList', @level2type=N'COLUMN',@level2name=N'UL_UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_UserList', @level2type=N'COLUMN',@level2name=N'UL_UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顺序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_UserList', @level2type=N'COLUMN',@level2name=N'UL_Sequence'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是主管理员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_UserList', @level2type=N'COLUMN',@level2name=N'UL_IsMain'
GO
/****** Object:  Table [dbo].[SMC_UserException]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMC_UserException](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[uid] [nvarchar](50) NULL,
	[type] [int] NULL,
 CONSTRAINT [PK_SMC_UserException] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1启用例外 2禁用例外' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_UserException', @level2type=N'COLUMN',@level2name=N'type'
GO
/****** Object:  Table [dbo].[SMC_User]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMC_User](
	[u_id] [int] NOT NULL,
	[u_uid] [varchar](50) NULL,
	[u_name] [varchar](100) NULL,
	[u_unitcode] [varchar](300) NULL,
	[u_unitname] [varchar](300) NULL,
	[u_password] [varchar](60) NULL,
	[u_createddate] [datetime] NULL,
	[u_auth_submit_time] [datetime] NULL,
	[u_auth_time] [datetime] NULL,
	[u_disable_time] [datetime] NULL,
	[u_enable_status] [int] NULL,
	[u_enable_time] [datetime] NULL,
	[u_lock_expire_time] [datetime] NULL,
	[u_lock_status] [int] NULL,
	[u_lock_time] [datetime] NULL,
	[u_unlock_time] [datetime] NULL,
	[u_need_sync] [bit] NULL,
	[u_need_sync_compare_time] [datetime] NULL,
	[u_update_time] [datetime] NULL,
 CONSTRAINT [PK_SMC_USER] PRIMARY KEY CLUSTERED 
(
	[u_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键，自动编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_User', @level2type=N'COLUMN',@level2name=N'u_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户帐号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_User', @level2type=N'COLUMN',@level2name=N'u_uid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_User', @level2type=N'COLUMN',@level2name=N'u_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_User', @level2type=N'COLUMN',@level2name=N'u_unitcode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_User', @level2type=N'COLUMN',@level2name=N'u_password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_User', @level2type=N'COLUMN',@level2name=N'u_createddate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否需要从统一授权同步' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_User', @level2type=N'COLUMN',@level2name=N'u_need_sync'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户比对的时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_User', @level2type=N'COLUMN',@level2name=N'u_need_sync_compare_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_User', @level2type=N'COLUMN',@level2name=N'u_update_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_User'
GO
/****** Object:  Table [dbo].[SMC_Unit]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMC_Unit](
	[Unit_ID] [varchar](300) NOT NULL,
	[Unit_Name] [varchar](200) NULL,
	[Upper_Unit_ID] [varchar](300) NULL,
	[Unit_Demo] [text] NULL,
	[Unit_Path] [varchar](500) NULL,
	[Unit_CreatedTime] [datetime] NULL,
	[Unit_CreatedUser] [varchar](50) NULL,
	[Unit_UpdateTime] [datetime] NULL,
	[Unit_UpdateUser] [varchar](50) NULL,
	[Unit_Sequence] [int] NULL,
 CONSTRAINT [PK_SMC_Unit] PRIMARY KEY CLUSTERED 
(
	[Unit_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Unit', @level2type=N'COLUMN',@level2name=N'Unit_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Unit', @level2type=N'COLUMN',@level2name=N'Unit_Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级代号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Unit', @level2type=N'COLUMN',@level2name=N'Upper_Unit_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Unit', @level2type=N'COLUMN',@level2name=N'Unit_Demo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用..分隔，如1..2..3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Unit', @level2type=N'COLUMN',@level2name=N'Unit_Path'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Unit', @level2type=N'COLUMN',@level2name=N'Unit_CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Unit', @level2type=N'COLUMN',@level2name=N'Unit_CreatedUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Unit', @level2type=N'COLUMN',@level2name=N'Unit_UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Unit', @level2type=N'COLUMN',@level2name=N'Unit_UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顺序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Unit', @level2type=N'COLUMN',@level2name=N'Unit_Sequence'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Unit'
GO
/****** Object:  Table [dbo].[SMC_Role]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMC_Role](
	[Role_ID] [int] NOT NULL,
	[Role_Name] [varchar](200) NOT NULL,
	[Unit_ID] [varchar](300) NULL,
	[Role_Demo] [text] NULL,
	[Role_CreatedTime] [datetime] NULL,
	[Role_CreatedUser] [varchar](50) NULL,
	[Role_UpdateTime] [datetime] NULL,
	[Role_UpdateUser] [varchar](50) NULL,
	[Role_Sequence] [int] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Role_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Role', @level2type=N'COLUMN',@level2name=N'Role_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Role', @level2type=N'COLUMN',@level2name=N'Role_Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Role', @level2type=N'COLUMN',@level2name=N'Unit_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Role', @level2type=N'COLUMN',@level2name=N'Role_Demo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Role', @level2type=N'COLUMN',@level2name=N'Role_CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Role', @level2type=N'COLUMN',@level2name=N'Role_CreatedUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Role', @level2type=N'COLUMN',@level2name=N'Role_UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Role', @level2type=N'COLUMN',@level2name=N'Role_UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Role', @level2type=N'COLUMN',@level2name=N'Role_Sequence'
GO
/****** Object:  Table [dbo].[SMC_PushDll]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMC_PushDll](
	[pd_id] [int] NOT NULL,
	[pd_name] [nvarchar](100) NULL,
	[pd_dll_filename] [nvarchar](100) NULL,
	[pd_xml_filename] [nvarchar](100) NULL,
	[pd_path] [nvarchar](max) NULL,
	[pd_status] [bit] NULL,
	[pd_dll_status] [nvarchar](50) NULL,
	[pd_zip_filename] [nvarchar](100) NULL,
	[pd_zip_extension] [nvarchar](100) NULL,
	[pd_zip_size] [int] NULL,
	[pd_zip_contenttype] [nvarchar](50) NULL,
	[pd_createdtime] [datetime] NULL,
	[pd_updatetime] [datetime] NULL,
 CONSTRAINT [PK_SMC_PullDll] PRIMARY KEY CLUSTERED 
(
	[pd_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SMC_PackageExtHistory]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMC_PackageExtHistory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pe_id] [int] NULL,
	[pe_Version] [nvarchar](50) NULL,
	[pe_PackageUrl] [nvarchar](500) NULL,
	[pe_CreateTime] [datetime] NULL,
 CONSTRAINT [PK_SMC_PackageExtHistory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExtHistory', @level2type=N'COLUMN',@level2name=N'pe_Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安装包存放地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExtHistory', @level2type=N'COLUMN',@level2name=N'pe_PackageUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExtHistory', @level2type=N'COLUMN',@level2name=N'pe_CreateTime'
GO
/****** Object:  Table [dbo].[SMC_PackageExt]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMC_PackageExt](
	[pe_id] [int] NOT NULL,
	[pe_UpdateTime] [datetime] NULL,
	[pe_UpdateUid] [varchar](50) NULL,
	[pe_CreateUid] [varchar](50) NULL,
	[pe_DownloadUri] [varchar](512) NULL,
	[pe_BuildVer] [nvarchar](50) NULL,
	[pe_Version] [nvarchar](64) NULL,
	[pe_Description] [nvarchar](max) NULL,
	[pe_DisplayName] [nvarchar](256) NULL,
	[pe_ClientType] [varchar](50) NULL,
	[pe_IsTJ] [bit] NULL,
	[pe_IsBB] [bit] NULL,
	[pe_PictureUrl] [varchar](200) NULL,
	[pe_2dPictureUrl] [varchar](200) NULL,
	[pe_DownCount] [int] NULL,
	[pe_Firmware] [varchar](50) NULL,
	[pe_Size] [int] NULL,
	[TableName] [varchar](50) NULL,
	[TableID] [int] NULL,
	[pe_UnitCode] [varchar](300) NULL,
	[pe_UnitName] [varchar](100) NULL,
	[pe_CreatedTime] [datetime] NULL,
	[pe_Category] [varchar](100) NULL,
	[pe_CategoryID] [varchar](50) NULL,
	[pe_Name] [varchar](100) NULL,
	[pe_AuthSubmitTime] [datetime] NULL,
	[pe_UsefulStstus] [bit] NULL,
	[pe_AuthStatus] [int] NULL,
	[pe_ApplicationCode] [varchar](50) NULL,
	[pe_ApplicationName] [varchar](300) NULL,
	[pe_AuthMan] [varchar](50) NULL,
	[pe_AuthManUID] [varchar](50) NULL,
	[pe_AuthSubmitName] [nvarchar](50) NULL,
	[pe_AuthSubmitUID] [nvarchar](50) NULL,
	[pe_AuthTime] [datetime] NULL,
	[pe_Direction] [varchar](50) NULL,
	[pe_LastVersion] [nvarchar](64) NULL,
	[pe_SyncStatus] [int] NULL,
	[pe_Type] [varchar](50) NULL,
	[pe_UsefulOperatorUID] [nvarchar](50) NULL,
	[pe_UsefulOperatorName] [nvarchar](50) NULL,
	[pe_UsefulTime] [datetime] NULL,
	[pe_FileUrl] [nvarchar](200) NULL,
	[pe_ExtentInfo] [varchar](max) NULL,
 CONSTRAINT [PK_SMC_PACKAGEEXT] PRIMARY KEY CLUSTERED 
(
	[pe_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安装包扩展编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Phone/Android、Pad/Android、Phone/iOS、Pad/iOS、Web' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_ClientType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否为推荐应用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_IsTJ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否为必备应用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_IsBB'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'icon图片url' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_PictureUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'二维码图片地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_2dPictureUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下载次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_DownCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支持固件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_Firmware'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安装包的大小' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_Size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安装包所在的表名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'TableName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安装包所在的表的id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'TableID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0待审核 1审核通过 2审核不通过' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_AuthStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_AuthTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作方向，比如上架、下架' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_Direction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上一版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_LastVersion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0待同步 1同步成功 2同步失败' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_SyncStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上下架操作人UID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_UsefulOperatorUID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上下架操作人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_UsefulOperatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上下架操作时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_UsefulTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件地址：如~/PackageExt/32/ia.ipa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt', @level2type=N'COLUMN',@level2name=N'pe_FileUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安装包扩展' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageExt'
GO
/****** Object:  Table [dbo].[SMC_Package4Out]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMC_Package4Out](
	[po_ID] [int] NOT NULL,
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
 CONSTRAINT [PK_SMC_PACKAGE4OUT] PRIMARY KEY NONCLUSTERED 
(
	[po_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Package的ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Package4Out', @level2type=N'COLUMN',@level2name=N'po_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Package的包名（例如：com.beyondbit.smartbox.phone）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Package4Out', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Package的显示名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Package4Out', @level2type=N'COLUMN',@level2name=N'DisplayName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Package的描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Package4Out', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对外发布的版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Package4Out', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编译的版本号，解决Bug的快速更新时，发布版本不变，而实际产生变化的情况。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Package4Out', @level2type=N'COLUMN',@level2name=N'BuildVer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下载地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Package4Out', @level2type=N'COLUMN',@level2name=N'DownloadUri'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Package4Out', @level2type=N'COLUMN',@level2name=N'CreateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Package4Out', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Package4Out', @level2type=N'COLUMN',@level2name=N'UpdateUid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Package4Out', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'外部应用的发布包信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Package4Out'
GO
/****** Object:  Table [dbo].[SMC_Functions]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMC_Functions](
	[FN_ID] [int] NOT NULL,
	[FN_Name] [varchar](200) NOT NULL,
	[FN_Code] [varchar](50) NULL,
	[Upper_FN_ID] [int] NOT NULL,
	[FN_Url] [varchar](max) NULL,
	[FN_Type] [varchar](50) NULL,
	[Unit_ID] [varchar](300) NULL,
	[FN_Img] [varchar](50) NULL,
	[FN_Path] [varchar](max) NULL,
	[FN_Demo] [text] NULL,
	[FN_IsDefault] [bit] NULL,
	[FN_CreatedTime] [datetime] NULL,
	[FN_CreatedUser] [varchar](50) NULL,
	[FN_UpdateTime] [datetime] NULL,
	[FN_UpdateUser] [varchar](50) NULL,
	[FN_Sequence] [int] NULL,
	[FN_Disabled] [bit] NULL,
	[FN_VisibleType] [int] NULL,
 CONSTRAINT [PK_Access2] PRIMARY KEY CLUSTERED 
(
	[FN_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FN_ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Functions', @level2type=N'COLUMN',@level2name=N'FN_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Functions', @level2type=N'COLUMN',@level2name=N'FN_Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级ID号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Functions', @level2type=N'COLUMN',@level2name=N'Upper_FN_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'超链接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Functions', @level2type=N'COLUMN',@level2name=N'FN_Url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'menu为菜单，function哦权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Functions', @level2type=N'COLUMN',@level2name=N'FN_Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属单位_Unit_Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Functions', @level2type=N'COLUMN',@level2name=N'Unit_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'正常显示的图标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Functions', @level2type=N'COLUMN',@level2name=N'FN_Img'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用..分隔，如1..2..3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Functions', @level2type=N'COLUMN',@level2name=N'FN_Path'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Functions', @level2type=N'COLUMN',@level2name=N'FN_Demo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否默认功能（点击模块显示的功能菜单） 1--是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Functions', @level2type=N'COLUMN',@level2name=N'FN_IsDefault'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Functions', @level2type=N'COLUMN',@level2name=N'FN_CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Functions', @level2type=N'COLUMN',@level2name=N'FN_CreatedUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Functions', @level2type=N'COLUMN',@level2name=N'FN_UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Functions', @level2type=N'COLUMN',@level2name=N'FN_UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Functions', @level2type=N'COLUMN',@level2name=N'FN_Sequence'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0所有都可见 1只单位管理员可见 2只系统管理员可见' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Functions', @level2type=N'COLUMN',@level2name=N'FN_VisibleType'
GO
/****** Object:  Table [dbo].[Camera]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Camera](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](512) NOT NULL,
	[Category] [nvarchar](256) NOT NULL,
	[Longitude] [float] NOT NULL,
	[Latitude] [float] NOT NULL,
	[Username] [varchar](128) NULL,
	[Password] [varchar](128) NULL,
	[VideoProtocol] [varchar](256) NOT NULL,
	[VideoUrl] [text] NOT NULL,
	[CtrlProtocol] [varchar](256) NOT NULL,
	[CtrlUrl] [text] NOT NULL,
 CONSTRAINT [PK_CAMERA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'摄像头标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Camera', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'摄像头名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Camera', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'摄像头分类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Camera', @level2type=N'COLUMN',@level2name=N'Category'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'经度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Camera', @level2type=N'COLUMN',@level2name=N'Longitude'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'维度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Camera', @level2type=N'COLUMN',@level2name=N'Latitude'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'摄像头登录用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Camera', @level2type=N'COLUMN',@level2name=N'Username'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'摄像头登录密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Camera', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'私有协议' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Camera', @level2type=N'COLUMN',@level2name=N'VideoProtocol'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'摄像头地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Camera', @level2type=N'COLUMN',@level2name=N'VideoUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'摄像头列表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Camera'
GO
/****** Object:  Table [dbo].[AppHttpPost]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AppHttpPost](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppCode] [varchar](50) NOT NULL,
	[AppPostHost] [varchar](50) NOT NULL,
 CONSTRAINT [PK_APPHTTPPOST] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ContactCategory]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactCategory](
	[Id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Code] [nvarchar](64) NULL,
	[DisplayName] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_CONTACTCATEGORY] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacet]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacet](
	[Id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[ExtendType] [nvarchar](64) NULL,
	[ExtendCode] [nvarchar](64) NULL,
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
/****** Object:  Table [dbo].[SMC_FunctionRole]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMC_FunctionRole](
	[FR_ID] [int] NOT NULL,
	[Role_ID] [int] NOT NULL,
	[FN_ID] [int] NULL,
	[FR_CreatedTime] [datetime] NULL,
	[FR_CreatedUser] [varchar](50) NULL,
	[FR_UpdateTime] [datetime] NULL,
	[FR_UpdateUser] [varchar](50) NULL,
	[FR_Sequence] [int] NULL,
 CONSTRAINT [PK_SMC_FUNCTIONROLE] PRIMARY KEY CLUSTERED 
(
	[FR_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FR_ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_FunctionRole', @level2type=N'COLUMN',@level2name=N'FR_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_FunctionRole', @level2type=N'COLUMN',@level2name=N'Role_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_FunctionRole', @level2type=N'COLUMN',@level2name=N'FN_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_FunctionRole', @level2type=N'COLUMN',@level2name=N'FR_CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_FunctionRole', @level2type=N'COLUMN',@level2name=N'FR_CreatedUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_FunctionRole', @level2type=N'COLUMN',@level2name=N'FR_UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_FunctionRole', @level2type=N'COLUMN',@level2name=N'FR_UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_FunctionRole', @level2type=N'COLUMN',@level2name=N'FR_Sequence'
GO
/****** Object:  Table [dbo].[SMC_Collect]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMC_Collect](
	[clt_id] [int] NOT NULL,
	[pe_id] [int] NULL,
	[ClientType] [varchar](50) NULL,
	[uid] [varchar](50) NULL,
	[uname] [varchar](100) NULL,
	[clt_CollectDate] [datetime] NULL,
 CONSTRAINT [PK_SMC_COLLECT] PRIMARY KEY CLUSTERED 
(
	[clt_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键，自动编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Collect', @level2type=N'COLUMN',@level2name=N'clt_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安装包扩展编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Collect', @level2type=N'COLUMN',@level2name=N'pe_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Phone/Android、Pad/Android、Phone/iOS、Pad/iOS、Web' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Collect', @level2type=N'COLUMN',@level2name=N'ClientType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收藏用户帐号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Collect', @level2type=N'COLUMN',@level2name=N'uid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收藏用户姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Collect', @level2type=N'COLUMN',@level2name=N'uname'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收藏日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Collect', @level2type=N'COLUMN',@level2name=N'clt_CollectDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收藏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_Collect'
GO
/****** Object:  Table [dbo].[SMC_PackagePicture]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMC_PackagePicture](
	[pp_id] [int] NOT NULL,
	[pe_id] [int] NULL,
	[pp_path] [varchar](400) NULL,
	[pp_CreatedDate] [datetime] NULL,
	[pp_desc] [varchar](max) NULL,
	[pp_title] [varchar](100) NULL,
 CONSTRAINT [PK_SMC_PACKAGEPICTURE] PRIMARY KEY CLUSTERED 
(
	[pp_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键，自动编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackagePicture', @level2type=N'COLUMN',@level2name=N'pp_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安装包扩展编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackagePicture', @level2type=N'COLUMN',@level2name=N'pe_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackagePicture', @level2type=N'COLUMN',@level2name=N'pp_path'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackagePicture', @level2type=N'COLUMN',@level2name=N'pp_CreatedDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackagePicture', @level2type=N'COLUMN',@level2name=N'pp_desc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'截图标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackagePicture', @level2type=N'COLUMN',@level2name=N'pp_title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安装包截图' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackagePicture'
GO
/****** Object:  Table [dbo].[SMC_PackageManual]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMC_PackageManual](
	[pm_id] [int] NOT NULL,
	[pm_name] [varchar](200) NULL,
	[pm_url] [varchar](500) NULL,
	[pm_createdtime] [datetime] NULL,
	[pm_updatetime] [datetime] NULL,
	[pe_id] [int] NULL,
 CONSTRAINT [PK_SMC_PACKAGEMANUAL] PRIMARY KEY CLUSTERED 
(
	[pm_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帮助手册名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageManual', @level2type=N'COLUMN',@level2name=N'pm_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帮助手册下载地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageManual', @level2type=N'COLUMN',@level2name=N'pm_url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安装包手册' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageManual'
GO
/****** Object:  Table [dbo].[SMC_PackageFAQ]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMC_PackageFAQ](
	[pf_id] [int] NOT NULL,
	[pf_uid] [varchar](50) NULL,
	[pf_uname] [varchar](50) NULL,
	[pf_question] [varchar](500) NULL,
	[pf_answer] [varchar](max) NULL,
	[pf_askdate] [datetime] NULL,
	[pe_id] [int] NULL,
	[pf_askemail] [varchar](50) NULL,
	[pf_askmobile] [varchar](50) NULL,
	[pf_peplyman] [varchar](50) NULL,
	[pf_need_syncto_inside] [bit] NULL,
	[pf_need_syncto_outside] [bit] NULL,
 CONSTRAINT [PK_SMC_PACKAGEFAQ] PRIMARY KEY CLUSTERED 
(
	[pf_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提问用户帐号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageFAQ', @level2type=N'COLUMN',@level2name=N'pf_uid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提问用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageFAQ', @level2type=N'COLUMN',@level2name=N'pf_uname'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageFAQ', @level2type=N'COLUMN',@level2name=N'pf_question'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'答案' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageFAQ', @level2type=N'COLUMN',@level2name=N'pf_answer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提问时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageFAQ', @level2type=N'COLUMN',@level2name=N'pf_askdate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提问人邮件地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageFAQ', @level2type=N'COLUMN',@level2name=N'pf_askemail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提问人手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageFAQ', @level2type=N'COLUMN',@level2name=N'pf_askmobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'答复人帐号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageFAQ', @level2type=N'COLUMN',@level2name=N'pf_peplyman'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问题返馈' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMC_PackageFAQ'
GO
/****** Object:  Table [dbo].[SMC_PackageExtSyncToOutside]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMC_PackageExtSyncToOutside](
	[peso_id] [int] NOT NULL,
	[pe_id] [int] NOT NULL,
	[sync_bat_no] [int] NULL,
	[sync_time] [datetime] NULL,
	[sync_status] [bit] NULL,
	[sync_user_uid] [varchar](50) NULL,
	[sync_user_name] [varchar](50) NULL,
	[description] [varchar](max) NULL,
	[pe_name] [varchar](200) NULL,
 CONSTRAINT [PK_SMC_PackageExtSyncToOutside] PRIMARY KEY CLUSTERED 
(
	[peso_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[monitor_sendwarn]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[monitor_sendwarn](
	[sw_id] [int] IDENTITY(1,1) NOT NULL,
	[sw_log_id] [int] NULL,
	[sw_senddate] [datetime] NULL,
	[sw_sendresult] [varchar](128) NULL,
	[sw_receptman] [varchar](128) NULL,
	[sw_sendway] [varchar](128) NULL,
	[sw_createdate] [datetime] NULL,
	[sw_lastsenddate] [datetime] NULL,
	[sw_mobile] [varchar](128) NULL,
	[sw_email] [varchar](128) NULL,
 CONSTRAINT [PK_MONITOR_SENDWARN] PRIMARY KEY CLUSTERED 
(
	[sw_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ContactProperty]    Script Date: 12/01/2014 14:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactProperty](
	[Id] [int] NOT NULL,
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
/****** Object:  Table [dbo].[ContactCategoryRef]    Script Date: 12/01/2014 14:42:33 ******/
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
/****** Object:  Default [DF__Notificat__IsSuc__08B54D69]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[Notification] ADD  DEFAULT ((0)) FOR [IsSuccess]
GO
/****** Object:  Default [DF_SMC_Functions_FN_Disabled]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[SMC_Functions] ADD  CONSTRAINT [DF_SMC_Functions_FN_Disabled]  DEFAULT ((0)) FOR [FN_Disabled]
GO
/****** Object:  Default [DF_SMC_Functions_FN_VisibleType]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[SMC_Functions] ADD  CONSTRAINT [DF_SMC_Functions_FN_VisibleType]  DEFAULT ((0)) FOR [FN_VisibleType]
GO
/****** Object:  Default [DF_SMC_PackageExt_pe_AuthStatus]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[SMC_PackageExt] ADD  CONSTRAINT [DF_SMC_PackageExt_pe_AuthStatus]  DEFAULT ((0)) FOR [pe_AuthStatus]
GO
/****** Object:  Default [DF_SMC_UserList_UL_IsMain]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[SMC_UserList] ADD  CONSTRAINT [DF_SMC_UserList_UL_IsMain]  DEFAULT ((0)) FOR [UL_IsMain]
GO
/****** Object:  Check [CKC_Package4Android_Type]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[SMC_Package4Out]  WITH CHECK ADD  CONSTRAINT [CKC_Package4Android_Type] CHECK  (([Type]='Plugin' OR [Type]='Main'))
GO
ALTER TABLE [dbo].[SMC_Package4Out] CHECK CONSTRAINT [CKC_Package4Android_Type]
GO
/****** Object:  ForeignKey [FK_CONTACET_FK_USERRE_USERRESO]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[Contacet]  WITH CHECK ADD  CONSTRAINT [FK_CONTACET_FK_USERRE_USERRESO] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserResource] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Contacet] CHECK CONSTRAINT [FK_CONTACET_FK_USERRE_USERRESO]
GO
/****** Object:  ForeignKey [FK_CONTACTC_FK_USERRE_USERRESO]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[ContactCategory]  WITH CHECK ADD  CONSTRAINT [FK_CONTACTC_FK_USERRE_USERRESO] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserResource] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContactCategory] CHECK CONSTRAINT [FK_CONTACTC_FK_USERRE_USERRESO]
GO
/****** Object:  ForeignKey [FK_CONTACTC_FK_CONTAC_CONTACET]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[ContactCategoryRef]  WITH CHECK ADD  CONSTRAINT [FK_CONTACTC_FK_CONTAC_CONTACET] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contacet] ([Id])
GO
ALTER TABLE [dbo].[ContactCategoryRef] CHECK CONSTRAINT [FK_CONTACTC_FK_CONTAC_CONTACET]
GO
/****** Object:  ForeignKey [FK_CONTACTC_FK_CONTAC_CONTACTC]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[ContactCategoryRef]  WITH CHECK ADD  CONSTRAINT [FK_CONTACTC_FK_CONTAC_CONTACTC] FOREIGN KEY([ContactCategoryId])
REFERENCES [dbo].[ContactCategory] ([Id])
GO
ALTER TABLE [dbo].[ContactCategoryRef] CHECK CONSTRAINT [FK_CONTACTC_FK_CONTAC_CONTACTC]
GO
/****** Object:  ForeignKey [FK_CONTACTP_FK_CONTAC_CONTACET]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[ContactProperty]  WITH CHECK ADD  CONSTRAINT [FK_CONTACTP_FK_CONTAC_CONTACET] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contacet] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContactProperty] CHECK CONSTRAINT [FK_CONTACTP_FK_CONTAC_CONTACET]
GO
/****** Object:  ForeignKey [FK_MONITOR__REFERENCE_MONITOR_]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[monitor_sendwarn]  WITH CHECK ADD  CONSTRAINT [FK_MONITOR__REFERENCE_MONITOR_] FOREIGN KEY([sw_log_id])
REFERENCES [dbo].[monitor_log] ([log_id])
GO
ALTER TABLE [dbo].[monitor_sendwarn] CHECK CONSTRAINT [FK_MONITOR__REFERENCE_MONITOR_]
GO
/****** Object:  ForeignKey [FK_SMC_COLL_REFERENCE_SMC_PACK]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[SMC_Collect]  WITH CHECK ADD  CONSTRAINT [FK_SMC_COLL_REFERENCE_SMC_PACK] FOREIGN KEY([pe_id])
REFERENCES [dbo].[SMC_PackageExt] ([pe_id])
GO
ALTER TABLE [dbo].[SMC_Collect] CHECK CONSTRAINT [FK_SMC_COLL_REFERENCE_SMC_PACK]
GO
/****** Object:  ForeignKey [FK_FunctionRole_Role]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[SMC_FunctionRole]  WITH NOCHECK ADD  CONSTRAINT [FK_FunctionRole_Role] FOREIGN KEY([Role_ID])
REFERENCES [dbo].[SMC_Role] ([Role_ID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[SMC_FunctionRole] CHECK CONSTRAINT [FK_FunctionRole_Role]
GO
/****** Object:  ForeignKey [FK_SMC_PackageExtSyncToOutside_SMC_PackageExt]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[SMC_PackageExtSyncToOutside]  WITH CHECK ADD  CONSTRAINT [FK_SMC_PackageExtSyncToOutside_SMC_PackageExt] FOREIGN KEY([pe_id])
REFERENCES [dbo].[SMC_PackageExt] ([pe_id])
GO
ALTER TABLE [dbo].[SMC_PackageExtSyncToOutside] CHECK CONSTRAINT [FK_SMC_PackageExtSyncToOutside_SMC_PackageExt]
GO
/****** Object:  ForeignKey [FK_SMC_PACK_REFERENCE_SMC_PACK]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[SMC_PackageFAQ]  WITH CHECK ADD  CONSTRAINT [FK_SMC_PACK_REFERENCE_SMC_PACK] FOREIGN KEY([pe_id])
REFERENCES [dbo].[SMC_PackageExt] ([pe_id])
GO
ALTER TABLE [dbo].[SMC_PackageFAQ] CHECK CONSTRAINT [FK_SMC_PACK_REFERENCE_SMC_PACK]
GO
/****** Object:  ForeignKey [FK_PackageExt_R_PACKManual]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[SMC_PackageManual]  WITH CHECK ADD  CONSTRAINT [FK_PackageExt_R_PACKManual] FOREIGN KEY([pe_id])
REFERENCES [dbo].[SMC_PackageExt] ([pe_id])
GO
ALTER TABLE [dbo].[SMC_PackageManual] CHECK CONSTRAINT [FK_PackageExt_R_PACKManual]
GO
/****** Object:  ForeignKey [FK_SMCPackPicture_R_SMCPackExt]    Script Date: 12/01/2014 14:42:33 ******/
ALTER TABLE [dbo].[SMC_PackagePicture]  WITH CHECK ADD  CONSTRAINT [FK_SMCPackPicture_R_SMCPackExt] FOREIGN KEY([pe_id])
REFERENCES [dbo].[SMC_PackageExt] ([pe_id])
GO
ALTER TABLE [dbo].[SMC_PackagePicture] CHECK CONSTRAINT [FK_SMCPackPicture_R_SMCPackExt]
GO
