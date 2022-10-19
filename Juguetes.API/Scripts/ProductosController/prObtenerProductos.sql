USE [productosDB]
GO
/****** Object:  StoredProcedure [DSC].[prDSCObtenerEmpleados]    Script Date: 14/10/2022 05:27:57 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[prObtenerProductos]
AS
DECLARE @vchError		VARCHAR(500) = '';

DECLARE @Resultado AS TABLE
(
	bitResultado		BIT DEFAULT(1),
	vchMensaje			VARCHAR(500) DEFAULT(''),
	Id					INT DEFAULT(0),
	Nombre				VARCHAR(50) DEFAULT(''),
	Descripcion			VARCHAR(100) DEFAULT(''),
	RestriccionEdad		INT DEFAULT(0),
	Compania			VARCHAR(50) DEFAULT('')
)

BEGIN TRY
	INSERT INTO @Resultado(
	bitResultado, vchMensaje, 
	Id,	Nombre,	Descripcion,
	RestriccionEdad,Compania
	)
	SELECT 1, '',
	Id,	Nombre,	Descripcion,
	RestriccionEdad,Compania	
	FROM [Productos]

	IF NOT  EXISTS(SELECT 1 FROM @Resultado)
	BEGIN 
		INSERT INTO @Resultado (bitResultado, vchMensaje)
		SELECT 0, 'No se encontraron resultados'
	END
END TRY

BEGIN CATCH
	SET @vchError = ERROR_MESSAGE();
	PRINT Concat(ERROR_PROCEDURE(),ERROR_MESSAGE(),' '' ', ERROR_LINE() );
END CATCH;

IF(@vchError <> '')
BEGIN
	INSERT INTO @Resultado (bitResultado, vchMensaje)
	SELECT 0, @vchError
END
SELECT * FROM @Resultado