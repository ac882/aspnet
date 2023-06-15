--CREATE TABLE  carros (
-- idcar INT IDENTITY PRIMARY KEY,
-- marca NVARCHAR(120),
-- modelo NVARCHAR(120),
-- pu DECIMAL(10,2),
-- foto VARBINARY(max)
--);

--CREATE TABLE marcas(
-- marca NCHAR(50)PRIMARY key
--);
--INSERT INTO marcas VALUES ('Fiat'),('Ford'),('Lancia');
--SELECT * FROM marcas;

--ALTER TABLE carros ADD CONSTRAINT fkmarcascarros FOREIGN KEY (marca)
--     REFERENCES marcas(marca) ON UPDATE CASCADE ON DELETE cascade;

--ALTER TABLE  carros ALTER COLUMN marca NCHAR(50);

--INSERT INTO carros (marca,modelo, pu)VALUES
--  ('Fiat', 'fiat 600', 20.50),
--  ('Fiat', 'fiat Punto', 10.75),
--  ('Ford', 'Ford GT', 30.50),
--  ('Lancia', 'Lancia Delta', 50.50);
--SELECT  * FROM carros c INNER JOIN marcas m ON c.marca=m.marca;

--CREATE TABLE [dbo].[carros] (
--    [idcar]  INT             IDENTITY (1, 1) NOT NULL,
--    [marca]  NCHAR (50)      NULL,
--    [modelo] NVARCHAR (120)  NULL,
--    [pu]     DECIMAL (10, 2) NULL,
--    [foto]   VARBINARY (MAX) NULL,
--    PRIMARY KEY CLUSTERED ([idcar] ASC),
--    CONSTRAINT [fkmarcascarros] FOREIGN KEY ([marca]) REFERENCES [dbo].[marcas] ([marca]) ON DELETE CASCADE ON UPDATE CASCADE
--);

--CREATE TABLE alugueres (
--   idalu INT IDENTITY PRIMARY KEY,
--   idcli INT FOREIGN KEY REFERENCES clientes(idcli),
--   idcar INT FOREIGN KEY REFERENCES carros(idcar),
--   datainicial DATETIME DEFAULT CURRENT_TIMESTAMP,
--   datafinal DATETIME,
--   duracao AS DATEDIFF(HOUR,datainicial,datafinal),
--   CONSTRAINT unicoaluguer UNIQUE (idalu,idcli,datainicial)
--);

INSERT INTO alugueres(idcli,idcar,datafinal)
VALUES (1,2,'2022-05-10T11:30:00' );

SELECT idcli , idcar FROM clientes, carros;

SELECT * FROM alugueres;

ALTER TABLE alugueres ADD custo DECIMAL(10,3);


go
ALTER TRIGGER trig1 
ON alugueres
FOR update , INSERT
AS
BEGIN
  DECLARE @idcar int, @idalu INT ,@idcli INT, @duracao DECIMAL(10,2);
  SELECT @idalu=idalu,@idcar=idcar,@idcli=idcli, @duracao=duracao FROM inserted;
  DECLARE @pu DECIMAL(10,2);
  SELECT @pu =pu FROM carros WHERE idcar =@idcar;
  UPDATE alugueres SET custo = @pu * @duracao WHERE idalu=@idalu;
end
