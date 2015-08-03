CREATE TABLE [dbo].[IMGroup] (
    [GroupId]          NVARCHAR (128)  NOT NULL,
    [GroupDisplayName] NVARCHAR (64)   NOT NULL,
    [Description]      NVARCHAR (4000) NOT NULL,
    [Announcement]     NVARCHAR (4000) NULL,
    [Owner]            NVARCHAR (64)   NOT NULL,
    [PublishId]        INT             IDENTITY (100000, 1) NOT NULL,
    [CreateUid]        NVARCHAR (64)   NOT NULL,
    [CreateTime]       DATETIME        NOT NULL,
    [UpdateUid]        NVARCHAR (64)   NOT NULL,
    [UpdateTime]       DATETIME        NOT NULL,
    CONSTRAINT [PK_IMGroup] PRIMARY KEY CLUSTERED ([GroupId] ASC)
);

