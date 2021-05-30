INSERT INTO rol
VALUES(NEWID(),'Administrador',1),
(NEWID(),'Gestor',1),
(NEWID(),'Destinatario',1)


INSERT INTO tipo_correspondencia
VALUES(NEWID(),'Interna','CI',1,1),
(NEWID(),'Externa','CE',1,1)

INSERT INTO funcionario
VALUES(NEWID(),'74C492C6-F3D6-467F-93EC-F79CD244FE36', 'Juan Carlos',
'Torres Torres','1001053192',1)


INSERT INTO remitente
VALUES(NEWID(),'Daniel', 'Goméz','123889481',GETDATE())