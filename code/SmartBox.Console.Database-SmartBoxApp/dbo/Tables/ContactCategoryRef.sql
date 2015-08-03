CREATE TABLE [dbo].[ContactCategoryRef] (
    [ContactId]         INT NOT NULL,
    [ContactCategoryId] INT NOT NULL,
    CONSTRAINT [PK_CONTACTCATEGORYREF] PRIMARY KEY CLUSTERED ([ContactId] ASC, [ContactCategoryId] ASC),
    CONSTRAINT [FK_CONTACTC_FK_CONTAC_CONTACET] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacet] ([Id]),
    CONSTRAINT [FK_CONTACTC_FK_CONTAC_CONTACTC] FOREIGN KEY ([ContactCategoryId]) REFERENCES [dbo].[ContactCategory] ([Id])
);

