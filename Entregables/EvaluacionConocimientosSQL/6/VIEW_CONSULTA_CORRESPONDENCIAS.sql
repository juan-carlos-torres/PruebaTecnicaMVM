CREATE VIEW VIEW_CONSULTA_CORRESPONDENCIAS
AS 
	SELECT 
	cor_consecutivo AS consecutivo,
	cor_asunto AS asunto,
	cor_descripcion AS descripcion,
	cor_fecha_creacion AS fecha_creacion,
	tco_nombre AS tipo_correspondencia,
	rem_nombres + ' ' + rem_apellidos AS nombre_remitente,
	fun_nombres + ' ' + fun_apellidos AS nombre_destinatario
	FROM correspondencia
	INNER JOIN tipo_correspondencia ON cor_id_tipo_correspondencia = tco_id
	INNER JOIN remitente ON cor_id_remitente = rem_id
	INNER JOIN funcionario ON cor_id_destinatario = fun_id
	WHERE cor_fecha_creacion BETWEEN CONVERT(date,'2021-05-29',102) AND CONVERT(date,'2021-05-30',102)
GO  