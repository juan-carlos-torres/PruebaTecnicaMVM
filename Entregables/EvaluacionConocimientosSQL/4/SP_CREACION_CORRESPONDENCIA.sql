CREATE PROCEDURE SP_CREACION_CORRESPONDENCIA
	@id_tipo_correspondencia UNIQUEIDENTIFIER,
	@id_remitente UNIQUEIDENTIFIER,
	@id_destinatario UNIQUEIDENTIFIER,
	@asunto VARCHAR(100),
	@descripcion VARCHAR(200)
AS
BEGIN
	BEGIN TRANSACTION
		BEGIN TRY

			DECLARE @consecutivo_correspondencia VARCHAR(10)

			--Calcula el consecutivo para la nueva correspondencia
			SELECT 
			@consecutivo_correspondencia = tco_prefijo + FORMAT(tco_consecutivo, '00000000')
			FROM tipo_correspondencia
			WHERE tco_id = @id_tipo_correspondencia


			--Inserta la nueva correspondencia
			INSERT INTO correspondencia (cor_id, cor_consecutivo, cor_id_tipo_correspondencia, 
											cor_id_remitente, cor_id_destinatario, cor_asunto,
											cor_descripcion, cor_fecha_creacion)
			VALUES( NEWID(), @consecutivo_correspondencia, @id_tipo_correspondencia, 
					@id_remitente, @id_destinatario, @asunto, @descripcion, GETDATE())


			--Actualiza el consecutivo para el tipo de correspondencia
			UPDATE tipo_correspondencia
			SET tco_consecutivo = tco_consecutivo + 1
			WHERE tco_id = @id_tipo_correspondencia


			COMMIT

		END TRY
		BEGIN CATCH 
			ROLLBACK
		END CATCH
END

