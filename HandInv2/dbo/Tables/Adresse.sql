CREATE TABLE [dbo].[Adresse] (
    [AdresseID]  BIGINT       IDENTITY (1, 1) NOT NULL,
    [Vejnavn]    NVARCHAR (MAX) NOT NULL,
    [Husnummer]  BIGINT       NOT NULL,
    [Postnummer] BIGINT       NOT NULL,
    CONSTRAINT [pk_Adresse] PRIMARY KEY CLUSTERED ([AdresseID] ASC)
);

