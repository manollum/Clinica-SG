--Me ubico en master
USE master;
GO

--Creo la BD en master
CREATE DATABASE [TPI-Grupo10]
GO

--Me ubico en la BD creada
USE [TPI-Grupo10]
GO

--Crear tabla provincia
CREATE TABLE Provincia
(
	CodProvincia_PR INT IDENTITY (1,1) NOT NULL,
	Descripcion_PR NVARCHAR(50) NOT NULL,
	
	CONSTRAINT PK_Provincia PRIMARY KEY (CodProvincia_Pr)
)
GO

--Crear tabla Especialidad
CREATE TABLE Especialidad
(
	CodEspecialidad_ES INT IDENTITY (1,1) NOT NULL,
	Descripcion_ES NVARCHAR(50) NOT NULL,
	
	CONSTRAINT PK_Especialidad PRIMARY KEY (CodEspecialidad_ES)
)
GO

--Crear tabla Dia
CREATE TABLE Dia
(
	NumDia_DI INT IDENTITY (1,1) NOT NULL,
	Descripcion_DI NVARCHAR(10) NOT NULL,
	
	CONSTRAINT PK_Dia PRIMARY KEY (NumDia_DI)
)
GO

--Crear tabla Paciente
CREATE TABLE Paciente
(
	Legajo_PA INT IDENTITY (1,1) NOT NULL,
	DNI_PA CHAR(8) NOT NULL,
	Nombre_PA NVARCHAR(50) NOT NULL, 
	Apellido_PA NVARCHAR(50) NOT NULL,
	Sexo_PA CHAR(1) NOT NULL, --Que se ingrese M para masculino y F para femenino
	Nacionalidad_PA NVARCHAR(50) NOT NULL,
	FechaNacimiento_PA DATE NOT NULL,
	Direccion_PA NVARCHAR(100) NOT NULL,
	Localidad_PA NVARCHAR (50) NOT NULL,
	CodProvincia_PA INT NOT NULL,
	Correo_PA VARCHAR(100) NOT NULL,
	Telefono_PA VARCHAR(16) NOT NULL,
	Estado_PA BIT NOT NULL -- 1(true) para activo y 0(false) para desabilitado
	
	CONSTRAINT PK_Paciente PRIMARY KEY (Legajo_PA),
	
	CONSTRAINT FK_Paciente_PA FOREIGN KEY (CodProvincia_PA) REFERENCES Provincia(CodProvincia_PR)
)
GO

--Crear tabla Medico
CREATE TABLE Medico
(
	Legajo_ME INT IDENTITY (1,1) NOT NULL,
	Nombre_ME NVARCHAR(50) NOT NULL, 
	Apellido_ME NVARCHAR(50) NOT NULL,
	Sexo_ME CHAR(1) NOT NULL, --Que se ingrese M para masculino y F para femenino
	Nacionalidad_ME NVARCHAR(50) NOT NULL,
	FechaNacimiento_ME DATE NOT NULL,
	Direccion_ME NVARCHAR(100) NOT NULL,
	Localidad_ME NVARCHAR (50) NOT NULL,
	CodProvincia_ME INT NOT NULL,
	Correo_ME NVARCHAR(100) NOT NULL, 
	Telefono_ME VARCHAR(16) NOT NULL,
	CodigoEspecialidad_ME INT NOT NULL,
	DNI_ME CHAR(8) NOT NULL,
	Estado_ME BIT NOT NULL, -- 1(true) para activo y 0(false) para desabilitado
	
	CONSTRAINT PK_Medico PRIMARY KEY (Legajo_ME),
	
	CONSTRAINT FK_Medico_Provincia FOREIGN KEY (CodProvincia_ME) REFERENCES Provincia(CodProvincia_PR),
	CONSTRAINT FK_Medico_Especialidad FOREIGN KEY (CodigoEspecialidad_ME) REFERENCES Especialidad(CodEspecialidad_ES)
)
GO

--Crear tabla UsuarioAdministrador
CREATE TABLE UsuarioAdministrador
(
	CodUsuarioAdministrador_UA INT IDENTITY (1,1) NOT NULL,
	Nombre_UA NVARCHAR(50) NOT NULL,
	Contraseña_UA NVARCHAR(30) NOT NULL,
	Estado_UA BIT NOT NULL, --1(true) para activo y 0(false) para desabilitado
	
	CONSTRAINT PK_UsuarioAdministrador PRIMARY KEY (CodUsuarioAdministrador_UA),
	CONSTRAINT UQ_NombreUsuario_UA UNIQUE (Nombre_UA)
)
GO

--Crear tabla UsuarioMedico
CREATE TABLE UsuarioMedico
(
	CodUsuarioMedico_UM INT IDENTITY (1,1) NOT NULL,
	LegajoMedico_UM INT NOT NULL,
	Nombre_UM NVARCHAR(50) NOT NULL,
	Contraseña_UM NVARCHAR(30) NOT NULL,
	Estado_UM BIT NOT NULL, --1(true) para activo y 0(false) para desabilitado
	
	CONSTRAINT PK_UsuarioMedico PRIMARY KEY (CodUsuarioMedico_UM),
	
	CONSTRAINT FK_UsuarioMedico_ME FOREIGN KEY (LegajoMedico_UM) REFERENCES Medico(Legajo_ME)
)
GO

--Crear tabla Disponibilidad
CREATE TABLE Disponibilidad
(
	NumDia_DIS INT NOT NULL,
	LegajoMedico_DIS INT NOT NULL,
	HorarioInicio_DIS TIME NOT NULL,
	HorarioFin_DIS TIME NOT NULL,
	Estado_DIS BIT NOT NULL, --1(true) para activo y 0(false) para desabilitado
	
	CONSTRAINT PK_Disponibilidad PRIMARY KEY (NumDia_DIS, LegajoMedico_DIS),
	
	CONSTRAINT FK_Disponibilidad_ME FOREIGN KEY (LegajoMedico_DIS) REFERENCES Medico(Legajo_ME),
	CONSTRAINT FK_Disponibilidad_DI FOREIGN KEY (NumDia_DIS) REFERENCES Dia(NumDia_DI)
)
GO

--Crear tabla Turno
CREATE TABLE Turno
(
	CodTurno_TU INT IDENTITY (1,1) NOT NULL,
	LegajoMedico_TU INT NOT NULL,
	LegajoPaciente_TU INT NOT NULL,
	Fecha_TU DATETIME NOT NULL,
	Pendiente_TU BIT DEFAULT 1, --1(true) para activo y 0(false) para desabilitado
	Asistencia_TU BIT NULL, --1(true) para activo y 0(false) para desabilitado
	Descripcion_TU NVARCHAR(300) DEFAULT 'Sin Completar',
	Estado_TU BIT NOT NULL, --1(true) para activo y 0(false) para desabilitado
	
	CONSTRAINT PK_Turno PRIMARY KEY (CodTurno_TU),
	
	CONSTRAINT FK_Turno_Medico_ME FOREIGN KEY (LegajoMedico_TU) REFERENCES Medico(Legajo_ME),
	CONSTRAINT FK_Turno_Medico_PA FOREIGN KEY (LegajoPaciente_TU) REFERENCES Paciente(Legajo_PA)
)
GO






