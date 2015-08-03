﻿CREATE function [dbo].[Split]
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
