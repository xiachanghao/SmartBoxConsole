CREATE TABLE [dbo].[HomePlan] (
    [ID]          INT           IDENTITY (100000, 1) NOT NULL,
    [Code]        NVARCHAR (64) NULL,
    [DisplayName] NVARCHAR (64) NULL,
    [Owner]       NVARCHAR (64) NULL,
    [Format]      NVARCHAR (64) NOT NULL,
    [IsDefault]   BIT           NOT NULL,
    [CreateUid]   NVARCHAR (64) NOT NULL,
    [CreateTime]  DATETIME      NOT NULL,
    [UpdateUid]   NVARCHAR (64) NOT NULL,
    [UpdateTime]  DATETIME      NOT NULL,
    CONSTRAINT [PK_HOMEPLAN] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HomePlan', @level2type = N'COLUMN', @level2name = N'CreateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HomePlan', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HomePlan', @level2type = N'COLUMN', @level2name = N'UpdateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HomePlan', @level2type = N'COLUMN', @level2name = N'UpdateTime';

