CREATE TABLE [dbo].[Contato]
(--chave primaria auto encrement
[Id] INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
[Nome] NVARCHAR(MAX),
[Telefone] NVARCHAR(MAX)
);

INSERT INTO Contato (Nome, Telefone) VALUES ('Cássio', '27999885463');
INSERT INTO Contato (Nome, Telefone) VALUES ('João', '27999776523');
INSERT INTO Contato (Nome, Telefone) VALUES ('Pedro', '27999662541');