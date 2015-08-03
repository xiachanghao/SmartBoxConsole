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
