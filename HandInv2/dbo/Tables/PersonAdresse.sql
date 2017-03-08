CREATE TABLE [dbo].[PersonAdresse] (
    [PersonID]  BIGINT NOT NULL,
    [AdresseID] BIGINT NOT NULL,
    CONSTRAINT [pk_PersonAdresse] PRIMARY KEY CLUSTERED ([PersonID] ASC, [AdresseID] ASC),
    CONSTRAINT [fk_PersonAdresse] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([PersonID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [fk_PersonAdresse2] FOREIGN KEY ([AdresseID]) REFERENCES [dbo].[Adresse] ([AdresseID]) ON UPDATE CASCADE
);

