CREATE TABLE [dbo].[APIData] (
    [oid]          INT            IDENTITY (1, 1) NOT NULL,
    [MarstTitle]   NVARCHAR (MAX) NULL,
    [MarstContent] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([oid] ASC)
);

