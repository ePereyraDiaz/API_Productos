USE [productosDB]
GO
/****** Object:  StoredProcedure [DSC].[prDSC_MantenimientoNuevaSolicitud]    Script Date: 14/09/2022 12:44:25 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[prMantenimientoProductos] (  
@Movimiento INT,
@Id INT,
@Nombre VARCHAR(50),
@Descripcion VARCHAR(100),
@RestriccionEdad INT,
@Compania VARCHAR(50)
)
AS 
BEGIN

DECLARE @trancount INT = -1
DECLARE @vchError VARCHAR(500) = ''
DECLARE @tblKeyOut AS TABLE (intProducto int)
DECLARE @Resultado AS TABLE(
	bitResultado BIT DEFAULT (1),
	vchMensaje VARCHAR(500) DEFAULT('')
)

SET @trancount = @@TRANCOUNT

IF @trancount > 0
		BEGIN	
			SAVE TRANSACTION prMantenimientoProductos
		END
	ELSE
		BEGIN
			BEGIN TRANSACTION 
		END

BEGIN TRY 
	
	IF @Movimiento = 1
		BEGIN
			INSERT INTO Productos (
				Nombre ,Descripcion, RestriccionEdad,Compania)  
				  OUTPUT  
				  inserted.Id  			
				  INTO @tblKeyOut  
				SELECT 	@Nombre, @Descripcion,@RestriccionEdad, @Compania;  

		IF NOT EXISTS (SELECT * FROM @tblKeyOut)
		BEGIN
			INSERT INTO @Resultado  (bitResultado, vchMensaje)
			SELECT 0, 'No se pudo agregar el producto, contactar con el administrador'
			GOTO _ROLLBACK
		END
		ELSE
		BEGIN
			INSERT INTO @Resultado  (bitResultado, vchMensaje)
			SELECT 1, 'Producto agregado correctamente'
			GOTO _FINTRAN	
		END

	END
	ELSE IF(@Movimiento = 2)
		BEGIN
			UPDATE Productos
			SET Nombre = @Nombre
			,Descripcion = @Descripcion
			,RestriccionEdad  = @RestriccionEdad
			,Compania =  @Compania
			WHERE Id = @Id
				
			INSERT INTO @Resultado  (bitResultado, vchMensaje)
			SELECT 1, 'Producto actualizado correctamente'

			GOTO _FINTRAN
		END
	ELSE IF @Movimiento = 3
		BEGIN
			DELETE FROM Productos
			WHERE  Id = @Id

			INSERT INTO @Resultado  (bitResultado, vchMensaje)
			SELECT 1, 'Producto eliminado correctamente'

			GOTO _FINTRAN
		END
	
_ROLLBACK:
	IF @trancount = 0
	BEGIN
		ROLLBACK TRANSACTION;
	END
	ELSE IF(@trancount <> -1 AND XACT_STATE() <> -1)
	BEGIN
		ROLLBACK TRANSACTION prMantenimientoProductos
	END
	GOTO _FIN

_FINTRAN:
	IF @trancount = 0
	BEGIN		
		COMMIT TRANSACTION;
	END
	ELSE IF(@trancount <> -1 AND XACT_STATE() <> -1)
	BEGIN
		COMMIT TRANSACTION prMantenimientoProductos;
	END

END TRY
BEGIN CATCH
	SET @vchError = ERROR_MESSAGE();
	PRINT CONCAT(ERROR_PROCEDURE(), ERROR_MESSAGE(), ERROR_LINE())
   
	IF @trancount = 0
	BEGIN
		ROLLBACK TRANSACTION;
	END
	ELSE IF (@trancount <> -1 AND XACT_STATE() <> -1)
	BEGIN
		ROLLBACK TRANSACTION prMantenimientoProductos;
	END
END CATCH;

_FIN:
SET NOCOUNT OFF

IF(@vchError <> '')
	BEGIN
		INSERT INTO @Resultado (bitResultado, vchMensaje)
		SELECT 0, @vchError
	END

SELECT * FROM @Resultado
END

GO