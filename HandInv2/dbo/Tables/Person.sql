CREATE TABLE [dbo].[Person] (
    [PersonID]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [Fornavn]    NVARCHAR (20) NOT NULL,
    [Mellemnavn] NVARCHAR (20) NOT NULL,
    [Efternavn]  NVARCHAR (20) NOT NULL,
    [PersonType] NVARCHAR (20) NOT NULL,
    CONSTRAINT [pk_Person] PRIMARY KEY CLUSTERED ([PersonID] ASC)
);

