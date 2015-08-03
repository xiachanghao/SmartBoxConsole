CREATE TABLE [dbo].[SMC_AutoTableID] (
    [at_id]        INT          IDENTITY (1, 1) NOT NULL,
    [AT_TableName] VARCHAR (50) NULL,
    [AT_MaxID]     INT          NULL,
    CONSTRAINT [PK_AutoTableID] PRIMARY KEY CLUSTERED ([at_id] ASC) WITH (FILLFACTOR = 90)
);

