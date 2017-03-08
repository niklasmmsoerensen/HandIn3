CREATE TABLE [dbo].[Telefon] (
    [TelefonID]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [TelefonType]   NVARCHAR (20) NOT NULL,
    [TelefonNummer] NVARCHAR (20) NOT NULL,
    [PersonID]      BIGINT        NOT NULL,
    CONSTRAINT [pk_Telefon] PRIMARY KEY CLUSTERED ([TelefonID] ASC),
    CONSTRAINT [fk_Telefon] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([PersonID]) ON DELETE CASCADE ON UPDATE CASCADE 
);

