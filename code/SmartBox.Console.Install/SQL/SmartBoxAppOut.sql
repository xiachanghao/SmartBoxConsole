USE [SmartBoxAppOut]
GO
/****** Object:  Table [dbo].[WebApplication]    Script Date: 12/01/2014 14:44:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WebApplication](
	[ID] [int] NOT NULL,
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
	[Unit] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SMC_User]    Script Date: 12/01/2014 14:44:01 ******/
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
	[u_enable_status] [int] NULL,
	[u_lock_status] [int] NULL,
	[u_auth_time] [datetime] NULL,
	[u_enable_time] [datetime] NULL,
	[u_lock_time] [datetime] NULL,
	[u_unlock_time] [datetime] NULL,
	[u_lock_expire_time] [datetime] NULL,
	[u_auth_submit_time] [datetime] NULL,
	[u_disable_time] [datetime] NULL,
	[u_need_sync] [bit] NULL,
	[u_need_sync_compare_time] [datetime] NULL,
	[u_update_time] [datetime] NULL,
 CONSTRAINT [PK_SMC_User] PRIMARY KEY CLUSTERED 
(
	[u_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SMC_Unit]    Script Date: 12/01/2014 14:44:01 ******/
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
/****** Object:  Table [dbo].[SMC_PackagePicture]    Script Date: 12/01/2014 14:44:01 ******/
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
 CONSTRAINT [PK_SMC_PackagePicture] PRIMARY KEY CLUSTERED 
(
	[pp_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SMC_PackageManual]    Script Date: 12/01/2014 14:44:01 ******/
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
 CONSTRAINT [PK_SMC_PackageManual] PRIMARY KEY CLUSTERED 
(
	[pm_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SMC_PackageFAQ]    Script Date: 12/01/2014 14:44:01 ******/
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
 CONSTRAINT [PK_SMC_PackageFAQ] PRIMARY KEY CLUSTERED 
(
	[pf_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SMC_PackageExt]    Script Date: 12/01/2014 14:44:01 ******/
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
	[pe_ClientType] [varchar](100) NULL,
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
	[pe_Name] [varchar](100) NULL,
	[pe_CategoryID] [varchar](50) NULL,
	[pe_UsefulStstus] [bit] NULL,
	[pe_LastVersion] [nvarchar](64) NULL,
	[pe_Type] [nvarchar](50) NULL,
	[pe_AuthStatus] [int] NULL,
	[pe_AuthSubmitTime] [datetime] NULL,
	[pe_AuthSubmitUID] [nvarchar](50) NULL,
	[pe_AuthSubmitName] [nvarchar](50) NULL,
	[pe_AuthTime] [datetime] NULL,
	[pe_ApplicationCode] [varchar](50) NULL,
	[pe_AuthMan] [varchar](50) NULL,
	[pe_AuthManUID] [varchar](50) NULL,
	[pe_ApplicationName] [varchar](300) NULL,
	[pe_SyncStatus] [int] NULL,
	[pe_UsefulOperatorUID] [nvarchar](50) NULL,
	[pe_UsefulOperatorName] [nvarchar](50) NULL,
	[pe_UsefulTime] [datetime] NULL,
	[pe_Direction] [varchar](50) NULL,
	[pe_FileUrl] [nvarchar](200) NULL,
	[pe_ExtentInfo] [varchar](max) NULL,
 CONSTRAINT [PK_SMC_PackageExt] PRIMARY KEY CLUSTERED 
(
	[pe_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SMC_Package4Out]    Script Date: 12/01/2014 14:44:01 ******/
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
	[UpdateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SMC_Collect]    Script Date: 12/01/2014 14:44:01 ******/
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
	[clt_CollectDate] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SMC_AutoTableID]    Script Date: 12/01/2014 14:44:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMC_AutoTableID](
	[at_id] [int] NOT NULL,
	[AT_TableName] [varchar](50) NULL,
	[AT_MaxID] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[Select_Pagination_ex]    Script Date: 12/01/2014 14:44:02 ******/
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
@WITH NVARCHAR(4000)--定义通用表达式，
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
/****** Object:  StoredProcedure [dbo].[PAGESELECT]    Script Date: 12/01/2014 14:44:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<wangshaoming>
-- Create date: <2008-12-12>
-- Description:	<分页存储过程>
-- =============================================
create PROCEDURE [dbo].[PAGESELECT]
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
/****** Object:  Table [dbo].[Package4AI]    Script Date: 12/01/2014 14:44:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Package4AI](
	[ID] [int] NOT NULL,
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
	[UpdateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Application]    Script Date: 12/01/2014 14:44:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Application](
	[ID] [int] NOT NULL,
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
	[Unit] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[App4AI]    Script Date: 12/01/2014 14:44:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[App4AI](
	[ID] [int] NOT NULL,
	[Package4AIID] [int] NOT NULL,
	[AppID] [int] NULL,
	[AppCode] [nvarchar](128) NULL,
	[ClientType] [nvarchar](64) NOT NULL,
	[IconUri] [nvarchar](128) NULL,
	[Seq] [int] NOT NULL,
	[CreateUid] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateUid] [nvarchar](64) NOT NULL,
	[UpdateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
