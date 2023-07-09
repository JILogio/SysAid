Create Database ValoracionActivos
Go

Use ValoracionActivos
Go

if exists(select name from sys.sysobjects where name = 'Confidencialidad')
	Drop table Confidencialidad
Go

Create Table Confidencialidad
(
idConfidencialidad int IDENTITY(1,1) PRIMARY KEY NOT NULL,
nivelConfidencialidad tinyint NOT NULL,
nivel varchar(30),
descripcion text NOT NULL
)

if exists(select name from sys.sysobjects where name = 'Disponibilidad')
	Drop table Disponibilidad
Go

Create Table Disponibilidad
(
idDisponibilidad int IDENTITY(1,1) PRIMARY KEY NOT NULL,
nivelDisponibilidad tinyint NOT NULL,
nivel varchar(15),
descripcion text NOT NULL
)

if exists(select name from sys.sysobjects where name = 'Integridad')
	Drop table Integridad
Go

Create Table Integridad
(
idIntegridad int IDENTITY(1,1) PRIMARY KEY NOT NULL,
nivelIntegridad tinyint NOT NULL,
nivel varchar(15),
descripcion text NOT NULL
)

if exists(select name from sys.sysobjects where name = 'TipoActivo')
	Drop table TipoActivo
Go

Create Table TipoActivo
(
idTipoActivo int IDENTITY(1,1) PRIMARY KEY NOT NULL,
tipo varchar(80) NOT NULL
)

if exists(select name from sys.sysobjects where name = 'AreaActivo')
	Drop table AreaActivo
Go

Create Table AreaActivo
(
idAreaActivo int IDENTITY(1,1) PRIMARY KEY NOT NULL,
area varchar(100) NOT NULL
)

if exists(select name from sys.sysobjects where name = 'Amenaza')
	Drop table Amenaza
Go

Create Table Amenaza(
	idAmenaza int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	origen varchar(50) NOT NULL,
	nombre varchar(50) NOT NULL,
	probabilidad float NOT NULL,
	impacto float NOT NULL
)
GO

if exists(select name from sys.sysobjects where name = 'Vulnerabilidad')
	Drop table Vulnerabilidad
Go

Create Table Vulnerabilidad(
	idVulnerabilidad int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	tipoActivo int,
	nombre varchar(50) NOT NULL,
	CONSTRAINT fk_Vulne_tipo  FOREIGN KEY(tipoActivo) REFERENCES tipoActivo(idTipoActivo)
)
GO

if exists(select name from sys.sysobjects where name = 'Controles')
	Drop table Controles
Go

Create Table Controles(
	idControl int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	nombre varchar(50) NOT NULL,
	descripcion text NOT NULL,
)
GO

if exists(select name from sys.sysobjects where name = 'ListadoActivos')
	Drop table ListadoActivos
Go

Create Table ListadoActivos
(
codigoActivo varchar(3) Primary Key NOT NULL,
nombreActivo varchar(100) NOT NULL,
descripcionActivo text NOT NULL,
propietario varchar(80) NULL,
protector varchar(80) NULL,
tipoActivo int,
area int,
integridad int,
disponibilidad int,
confidencialidad int,
justificacion text,
valoracion float,
CONSTRAINT fk_idTipoActivo FOREIGN KEY (tipoActivo) REFERENCES TipoActivo(idTipoActivo),
CONSTRAINT fk_ididArea FOREIGN KEY (area) REFERENCES AreaActivo(idAreaActivo),
CONSTRAINT fk_idIntegridad FOREIGN KEY (integridad) REFERENCES Integridad(idIntegridad),
CONSTRAINT fk_idDisponibilidad FOREIGN KEY (disponibilidad) REFERENCES Disponibilidad(idDisponibilidad),
CONSTRAINT fk_idConfidencialidad FOREIGN KEY (confidencialidad) REFERENCES Confidencialidad(idConfidencialidad)
)
Go

if exists(select name from sys.sysobjects where name = 'ListadoRiesgo')
	Drop table ListadoRiesgo
Go

Create Table ListadoRiesgo(
	idListadoRiesgo int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	codigoActivo varchar(3),
	idVulnerabilidad int,
	idAmenaza int,
	idControl int,
	riesgo float,
	CONSTRAINT fk_activo FOREIGN KEY(codigoActivo) REFERENCES ListadoActivos(codigoActivo),
	CONSTRAINT fk_vulnerable FOREIGN KEY(idVulnerabilidad) REFERENCES Vulnerabilidad(idVulnerabilidad),
	CONSTRAINT fk_amenaza FOREIGN KEY(idAmenaza) REFERENCES Amenaza(idAmenaza),
	CONSTRAINT fk_control FOREIGN KEY(idControl) REFERENCES Controles(idControl)
)
GO

CREATE or ALTER TRIGGER riesgoActivo
on ListadoRiesgo
FOR INSERT,UPDATE
AS
	declare @amenaza int = (SELECT idAmenaza FROM inserted);
	declare @probabilidad float = (SELECT probabilidad FROM Amenaza WHERE idAmenaza = @amenaza);
	declare @impacto float = (SELECT impacto FROM Amenaza WHERE idAmenaza = @amenaza);
	declare @riesgo float = @probabilidad * @impacto;
	if @@ROWCOUNT = 0
	return
	else
	begin
		UPDATE ListadoRiesgo
		SET riesgo = @riesgo
		WHERE idListadoRiesgo = (SELECT idListadoRiesgo FROM inserted)
	end
	if UPDATE(idAmenaza)
	begin
		UPDATE ListadoRiesgo
		SET riesgo = @riesgo
		WHERE idListadoRiesgo = (SELECT idListadoRiesgo FROM inserted)
	end
	return
Go

CREATE or ALTER TRIGGER valorarActivo
on ListadoActivos
for insert, update
as
DECLARE @idInte int
SET @idInte = (SELECT integridad FROM inserted)
DECLARE @idDis int
SET @idDis =(SELECT disponibilidad FROM inserted)
DECLARE @idConf int
SET @idConf =(SELECT confidencialidad FROM inserted)
DECLARE @integridad float
SET @integridad = (SELECT nivelIntegridad from Integridad g Where g.idIntegridad = @idInte)
DECLARE @disponibilidad float
SET @disponibilidad = (SELECT nivelDisponibilidad from Disponibilidad Where idDisponibilidad = @idDis)
DECLARE @confidencialidad float
SET @confidencialidad = (SELECT nivelConfidencialidad from Confidencialidad Where idConfidencialidad = @idConf)
DECLARE @rows int
SET @rows = @@ROWCOUNT
DECLARE @valorActivo float
SET @valorActivo = (@integridad + @disponibilidad + @confidencialidad)/3
	
	if @rows = 0 
	return
	else
	begin
	UPDATE ListadoActivos 
	SET valoracion = @valorActivo
	WHERE codigoActivo = (SELECT codigoActivo FROM inserted)
	end
	if UPDATE(integridad) or UPDATE(disponibilidad) or UPDATE(confidencialidad)
	begin 
	UPDATE ListadoActivos 
	SET valoracion = @valorActivo
	WHERE codigoActivo = (SELECT codigoActivo FROM inserted)
	end
	return
	GO 

INSERT INTO AreaActivo(area) VALUES('Bodega')
INSERT INTO AreaActivo(area) VALUES('Atencion al cliente')
INSERT INTO AreaActivo(area) VALUES('Administracion')
INSERT INTO AreaActivo(area) VALUES('Desarrollo')

INSERT INTO Confidencialidad(nivelConfidencialidad,nivel,descripcion) VALUES(3,'Publica reservada','Permitido solo usuario administrador')
INSERT INTO Confidencialidad(nivelConfidencialidad,nivel,descripcion) VALUES(2,'Publica clasificada','Permitido solo administradores y gerencia')
INSERT INTO Confidencialidad(nivelConfidencialidad,nivel,descripcion) VALUES(1,'Publica','Permitido para todo el personal de la empresa')
INSERT INTO Confidencialidad(nivelConfidencialidad,nivel,descripcion) VALUES(0,'No clasificada','Sin clasificar')

INSERT INTO Disponibilidad(nivelDisponibilidad,nivel,descripcion) VALUES(3,'Alta','Debe estar en pleno mantenimiento y funcional')
INSERT INTO Disponibilidad(nivelDisponibilidad,nivel,descripcion) VALUES(2,'Media','Ser funcional la mayoría del tiempo')
INSERT INTO Disponibilidad(nivelDisponibilidad,nivel,descripcion) VALUES(1,'Baja','Debe ser funcional')
INSERT INTO Disponibilidad(nivelDisponibilidad,nivel,descripcion) VALUES(0,'No clasificada','Sin clasificar')

INSERT INTO Integridad(nivelIntegridad,nivel,descripcion) VALUES(3,'Alta','Debe ser fiable y exacta')
INSERT INTO Integridad(nivelIntegridad,nivel,descripcion) VALUES(2,'Media','Debe ser fiable')
INSERT INTO Integridad(nivelIntegridad,nivel,descripcion) VALUES(1,'Baja','Informacion fiable')
INSERT INTO Integridad(nivelIntegridad,nivel,descripcion) VALUES(0,'No clasificada','Sin clasificar')

INSERT INTO TipoActivo(tipo) VALUES('Software')
INSERT INTO TipoActivo(tipo) VALUES('Hardware')
INSERT INTO TipoActivo(tipo) VALUES('Red')
INSERT INTO TipoActivo(tipo) VALUES('Servicios')

INSERT INTO Controles(nombre,descripcion) VALUES('Firewall','Software de control de trafico de red')
INSERT INTO Controles(nombre,descripcion) VALUES('McSafe','Antivirus')
INSERT INTO Controles(nombre,descripcion) VALUES('Wazuh','Software de gestion de vulnerabilidades')

INSERT INTO Amenaza(origen,nombre,probabilidad,impacto) VALUES('Natural','Terromoto',0.41,0.99)
INSERT INTO Amenaza(origen,nombre,probabilidad,impacto) VALUES('Natural','Tormentas eléctricas',0.36,0.97)
INSERT INTO Amenaza(origen,nombre,probabilidad,impacto) VALUES('Errores','Error de usuario',0.87,0.14)
INSERT INTO Amenaza(origen,nombre,probabilidad,impacto) VALUES('Errores','Error de administrador',0.34,0.57)
INSERT INTO Amenaza(origen,nombre,probabilidad,impacto) VALUES('Errores','Deficiencia de organización',0.4,0.7)
INSERT INTO Amenaza(origen,nombre,probabilidad,impacto) VALUES('Ataques intencionados','Manipulación de información',0.6,0.9)
INSERT INTO Amenaza(origen,nombre,probabilidad,impacto) VALUES('Ataques intencionados','Divulgación de software dañiño',0.75,0.8)

INSERT INTO Vulnerabilidad(tipoActivo,nombre) VALUES(1,'Falta de pruevas')
INSERT INTO Vulnerabilidad(tipoActivo,nombre) VALUES(1,'Falta de seguimiento')
INSERT INTO Vulnerabilidad(tipoActivo,nombre) VALUES(1,'Sin antivirus')
INSERT INTO Vulnerabilidad(tipoActivo,nombre) VALUES(2,'Humedad')
INSERT INTO Vulnerabilidad(tipoActivo,nombre) VALUES(2,'Polvo')
INSERT INTO Vulnerabilidad(tipoActivo,nombre) VALUES(2,'Fuego')
INSERT INTO Vulnerabilidad(tipoActivo,nombre) VALUES(3,'Líneas inadecuadas')


INSERT INTO ListadoActivos(codigoActivo,nombreActivo,descripcionActivo,propietario,protector,tipoActivo,area,integridad,disponibilidad,confidencialidad,valoracion)
VALUES('kj4','Computadora','Servicio al cliente','Martin Vargas','Erick Rosero',2,3,2,2,3,4.5)

INSERT INTO ListadoRiesgo(codigoActivo,idVulnerabilidad,idAmenaza,idControl)
VALUES('kj4',3,7,2)