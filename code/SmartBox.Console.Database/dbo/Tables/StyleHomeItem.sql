CREATE TABLE [dbo].[StyleHomeItem] (
    [StyleID]     INT            NOT NULL,
    [App4AIID]    INT            NOT NULL,
    [Image]       NVARCHAR (256) NOT NULL,
    [DispalyName] NVARCHAR (64)  NOT NULL,
    [Seq]         INT            NOT NULL,
    CONSTRAINT [PK_StyleHomeItem] PRIMARY KEY CLUSTERED ([StyleID] ASC, [App4AIID] ASC)
);

