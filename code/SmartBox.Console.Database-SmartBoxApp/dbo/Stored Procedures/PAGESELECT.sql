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
