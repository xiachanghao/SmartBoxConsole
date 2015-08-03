CREATE TABLE [dbo].[HomePlanDesign] (
    [PlanID]     INT            NOT NULL,
    [AppID]      INT            NOT NULL,
    [Location]   NVARCHAR (64)  NOT NULL,
    [Size]       NVARCHAR (64)  NOT NULL,
    [Type]       NVARCHAR (64)  NOT NULL,
    [ValueUri]   NVARCHAR (256) NOT NULL,
    [CreateUid]  NVARCHAR (64)  NOT NULL,
    [CreateTime] DATETIME       NOT NULL,
    [UpdateUid]  NVARCHAR (64)  NOT NULL,
    [UpdateTime] DATETIME       NOT NULL,
    CONSTRAINT [PK_HOMEPLANDESIGN] PRIMARY KEY CLUSTERED ([PlanID] ASC, [AppID] ASC),
    CONSTRAINT [FK_HOMEPLAN_FK_APPLIC_APPLICAT] FOREIGN KEY ([AppID]) REFERENCES [dbo].[Application] ([ID]),
    CONSTRAINT [FK_HOMEPLAN_FK_HOMEPL_HOMEPLAN] FOREIGN KEY ([PlanID]) REFERENCES [dbo].[HomePlan] ([ID]) ON DELETE CASCADE
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HomePlanDesign', @level2type = N'COLUMN', @level2name = N'CreateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HomePlanDesign', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新者', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HomePlanDesign', @level2type = N'COLUMN', @level2name = N'UpdateUid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HomePlanDesign', @level2type = N'COLUMN', @level2name = N'UpdateTime';

