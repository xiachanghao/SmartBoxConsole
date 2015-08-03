CREATE TABLE [dbo].[ContactProperty] (
    [Id]          INT            NOT NULL,
    [ContactId]   INT            NOT NULL,
    [Type]        INT            NOT NULL,
    [TypeName]    NVARCHAR (32)  NULL,
    [TypeSubName] NVARCHAR (32)  NULL,
    [Value]       NVARCHAR (512) NOT NULL,
    CONSTRAINT [PK_CONTACTPROPERTY] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CONTACTP_FK_CONTAC_CONTACET] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacet] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

