CREATE TABLE [dbo].[ApplicationCategory] (
    [ID]          INT            IDENTITY (100000, 1) NOT NULL,
    [Name]        NVARCHAR (64)  NOT NULL,
    [DisplayName] NVARCHAR (128) NOT NULL,
    [Seq]         INT            NOT NULL,
    [CreateUid]   NVARCHAR (64)  NOT NULL,
    [CreateTime]  DATETIME       NOT NULL,
    [UpdateUid]   NVARCHAR (64)  NOT NULL,
    [UpdateTime]  DATETIME       NOT NULL,
    CONSTRAINT [PK_APPLICATIONCATEGORY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'应用的分类，一个应用可以有多个分类', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ApplicationCategory';

