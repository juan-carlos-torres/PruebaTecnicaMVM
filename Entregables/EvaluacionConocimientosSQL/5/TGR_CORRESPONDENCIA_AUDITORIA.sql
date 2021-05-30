CREATE TRIGGER TGR_CORRESPONDENCIA_AUDITORIA  
ON correspondencia
AFTER UPDATE   
AS 
	INSERT INTO correspondencia_auditoria (cau_id, cau_id_correspondencia, 
											cau_asunto, cau_descripcion,
											cau_fecha_registro)
	SELECT 
		NEWID(), 
		cor_id, 
		cor_asunto, 
		cor_descripcion,
		GETDATE()
	FROM deleted
GO  