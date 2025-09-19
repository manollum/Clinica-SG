USE [TPI-Grupo10]
GO

-- Provincias
INSERT INTO Provincia (Descripcion_PR) VALUES
('Buenos Aires'), ('Catamarca'), ('Chaco'), ('Chubut'), ('Córdoba'),
('Corrientes'), ('Entre Ríos'), ('Formosa'), ('Jujuy'), ('La Pampa'),
('La Rioja'), ('Mendoza'), ('Misiones'), ('Neuquén'), ('Río Negro'),
('Salta'), ('San Juan'), ('San Luis'), ('Santa Cruz'), ('Santa Fe'),
('Santiago del Estero'), ('Tierra del Fuego'), ('Tucumán'), ('CABA'),
('Extranjero');
GO

-- Especialidades
INSERT INTO Especialidad (Descripcion_ES) VALUES
('Cardiología'), ('Clínica Médica'), ('Dermatología'), ('Gastroenterología'), ('Ginecología'),
('Neurología'), ('Oftalmología'), ('Otorrinolaringología'), ('Pediatría'), ('Psiquiatría'),
('Reumatología'), ('Traumatología'), ('Urología'), ('Endocrinología'), ('Neumonología');
GO

-- Días
INSERT INTO Dia (Descripcion_DI) VALUES
('Lunes'), ('Martes'), ('Miércoles'), ('Jueves'), ('Viernes'), ('Sábado'), ('Domingo');
GO


-- Pacientes
INSERT INTO Paciente (DNI_PA, Nombre_PA, Apellido_PA, Sexo_PA, Nacionalidad_PA, FechaNacimiento_PA, Direccion_PA, Localidad_PA, CodProvincia_PA, Correo_PA, Telefono_PA, Estado_PA) VALUES
('12345678', 'Juan', 'Pérez', 'M', 'Argentina', '1985-04-12', 'Av. Siempre Viva 123', 'Rosario', 1, 'juan.perez@gmail.com', '3411234567', 1),
('23456789', 'María', 'González', 'F', 'Argentina', '1990-06-22', 'Calle Falsa 456', 'Córdoba', 2, 'maria.gonzalez@hotmail.com', '3519876543', 1),
('34567890', 'Carlos', 'Ramírez', 'M', 'Uruguay', '1978-09-14', 'Mitre 789', 'Mendoza', 3, 'c.ramirez@correo.uy', '2613456789', 1),
('45678901', 'Laura', 'Fernández', 'F', 'Chile', '1982-03-30', 'San Martín 321', 'Salta', 4, 'laura.f@outlook.cl', '3876543210', 1),
('56789012', 'Lucía', 'Martínez', 'F', 'Argentina', '1995-11-10', 'Belgrano 1010', 'La Plata', 5, 'lucia.martinez@gmail.com', '2215678901', 1),
('67890123', 'Diego', 'Sosa', 'M', 'Paraguay', '1988-01-25', 'Rivadavia 2020', 'Resistencia', 6, 'diego.sosa@pymail.com', '3628765432', 1),
('78901234', 'Ana', 'López', 'F', 'Bolivia', '1992-08-05', 'Independencia 3030', 'Formosa', 7, 'ana.lopez.bo@gmail.com', '3707654321', 1),
('89012345', 'Mario', 'Díaz', 'M', 'Argentina', '1980-12-12', 'Italia 4040', 'Jujuy', 8, 'mario.diaz@fibertel.com.ar', '3886543210', 1),
('90123456', 'Sofía', 'Vega', 'F', 'Argentina', '2000-07-18', 'España 5050', 'San Juan', 9, 'sofi.vega@hotmail.com', '2645432109', 1),
('11223344', 'Pedro', 'Navarro', 'M', 'Argentina', '1975-05-20', 'Francia 6060', 'Neuquén', 10, 'pnavarro1975@gmail.com', '2994321098', 1),
('22334455', 'Florencia', 'Torres', 'F', 'Argentina', '1987-10-03', 'Uruguay 7070', 'Tucumán', 11, 'flor.torres@gmail.com', '3813210987', 1),
('33445566', 'Luis', 'Agüero', 'M', 'Brasil', '1993-02-15', 'Brasil 8080', 'Corrientes', 12, 'l.aguero@brmail.com', '3792109876', 1),
('44556677', 'Julieta', 'Moreno', 'F', 'Argentina', '1996-09-27', 'Colón 9090', 'Santa Fe', 13, 'julieta.moreno@gmail.com', '3421098765', 1),
('55667788', 'Tomás', 'Ríos', 'M', 'Argentina', '1984-06-07', '9 de Julio 1111', 'Bahía Blanca', 14, 't.rios84@yahoo.com', '2910987654', 1),
('66778899', 'Camila', 'Benítez', 'F', 'Argentina', '1991-03-09', 'Alsina 1212', 'Posadas', 15, 'camila.benitez@gmail.com', '3769876543', 1);
GO

-- Médicos
INSERT INTO Medico (Nombre_ME, Apellido_ME, Sexo_ME, Nacionalidad_ME, FechaNacimiento_ME, Direccion_ME, Localidad_ME, CodProvincia_ME, Correo_ME, Telefono_ME, CodigoEspecialidad_ME, DNI_ME, Estado_ME) VALUES
('Andrés', 'Ruiz', 'M', 'Argentina', '1975-05-12', 'Calle Salud 100', 'Córdoba', 2, 'aruiz@clinica.com', '3511111111', 1, '12345670', 1),
('Claudia', 'Vera', 'F', 'Argentina', '1980-11-23', 'Av. Médicos 200', 'Mendoza', 3, 'cvera@clinica.com', '2612222222', 2, '23456781', 1),
('Luis', 'Gómez', 'M', 'Argentina', '1985-07-15', 'Sanitaria 300', 'Rosario', 1, 'lgomez@clinica.com', '3413333333', 3, '34567892', 1),
('Paula', 'Martínez', 'F', 'Argentina', '1978-03-01', 'Salud y Vida 400', 'Salta', 4, 'pmartinez@clinica.com', '3874444444', 4, '45678903', 1),
('Javier', 'López', 'M', 'Chile', '1982-09-20', 'Cura 500', 'La Plata', 5, 'jlopez@clinica.com', '2215555555', 5, '56789014', 1),
('Mariana', 'Silva', 'F', 'Argentina', '1990-01-11', 'Consultorio 600', 'Resistencia', 6, 'msilva@clinica.com', '3626666666', 6, '67890125', 1),
('Daniel', 'Pérez', 'M', 'Argentina', '1979-06-18', 'Médica 700', 'Formosa', 7, 'dperez@clinica.com', '3707777777', 7, '78901236', 1),
('Luciana', 'Sosa', 'F', 'Argentina', '1987-12-03', 'Doctoras 800', 'Jujuy', 8, 'lsosa@clinica.com', '3888888888', 8, '89012347', 1),
('Fernando', 'Ibarra', 'M', 'Uruguay', '1983-08-30', 'Especialista 900', 'San Juan', 9, 'fibarra@clinica.com', '2649999999', 9, '90123458', 1),
('Rocío', 'Herrera', 'F', 'Argentina', '1991-02-14', 'Atención 1000', 'Neuquén', 10, 'rherrera@clinica.com', '2991010101', 10, '11223346', 1),
('Emiliano', 'Morales', 'M', 'Argentina', '1976-04-27', 'Cirujanos 1100', 'Tucumán', 11, 'emorales@clinica.com', '3811111111', 11, '22334457', 1),
('Marta', 'Benítez', 'F', 'Argentina', '1989-09-09', 'Salud Pública 1200', 'Corrientes', 12, 'mbenitez@clinica.com', '3791212121', 12, '33445568', 1),
('Sergio', 'Acuña', 'M', 'Argentina', '1981-05-05', 'Hospital 1300', 'Santa Fe', 13, 'sacuna@clinica.com', '3421313131', 13, '44556679', 1),
('Carla', 'Mendoza', 'F', 'Paraguay', '1986-10-10', 'Av. Salud 1400', 'Bahía Blanca', 14, 'cmendoza@clinica.com', '2911414141', 14, '55667780', 1),
('Julio', 'Ortiz', 'M', 'Argentina', '1977-07-07', 'Centro Médico 1500', 'Posadas', 15, 'jortiz@clinica.com', '3761515151', 15, '66778891', 1);
GO

-- Usuario Médico
INSERT INTO UsuarioMedico (LegajoMedico_UM, Nombre_UM, Contraseña_UM, Estado_UM) VALUES
(1, 'andres.ruiz', 'clave1', 1),
(2, 'claudia.vera', 'clave2', 1),
(3, 'luis.gomez', 'clave3', 1),
(4, 'paula.martinez', 'clave4', 1),
(5, 'javier.lopez', 'clave5', 1),
(6, 'mariana.silva', 'clave6', 1),
(7, 'daniel.perez', 'clave7', 1),
(8, 'luciana.sosa', 'clave8', 1),
(9, 'fernando.ibarra', 'clave9', 1),
(10, 'rocio.herrera', 'clave10', 1),
(11, 'emiliano.morales', 'clave11', 1),
(12, 'marta.benitez', 'clave12', 1),
(13, 'sergio.acuna', 'clave13', 1),
(14, 'carla.mendoza', 'clave14', 1),
(15, 'julio.ortiz', 'clave15', 1);
GO

-- Usuario Administrador
INSERT INTO UsuarioAdministrador (Nombre_UA, Contraseña_UA, Estado_UA) VALUES
('sysadmin', 'adminpass1', 1)
GO

-- Disponibilidad
INSERT INTO Disponibilidad (NumDia_DIS, LegajoMedico_DIS, HorarioInicio_DIS, HorarioFin_DIS, Estado_DIS) VALUES
(1, 1, '08:00:00', '17:00:00', 1),
(2, 2, '09:00:00', '13:00:00', 1),
(3, 3, '10:00:00', '16:00:00', 1),
(4, 4, '08:00:00', '10:00:00', 1),
(5, 5, '09:00:00', '20:00:00', 1),
(6, 6, '10:00:00', '19:00:00', 1),
(7, 7, '08:00:00', '11:00:00', 1),
(1, 8, '09:00:00', '13:00:00', 1),
(2, 9, '10:00:00', '14:00:00', 1),
(3, 10, '08:00:00', '12:00:00', 1),
(4, 11, '09:00:00', '13:00:00', 1),
(5, 12, '10:00:00', '17:00:00', 1),
(6, 13, '08:00:00', '18:00:00', 1),
(7, 14, '09:00:00', '14:00:00', 1),
(1, 15, '10:00:00', '20:00:00', 1);
GO



--==================================================================================
-- Bloque 1: TURNOS FUTUROS Y PENDIENTES
-- (Fecha en 2025, Pendiente = 1, Asistencia = NULL, Descripción = DEFAULT)
--==================================================================================
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (15, 12, '2025-03-03 14:00:00', 0, NULL, DEFAULT, 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (5, 3, '2025-06-20 11:00:00', 0, NULL, DEFAULT, 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (1, 8, '2025-09-09 09:00:00', 1, NULL, DEFAULT, 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (11, 1, '2025-02-27 10:00:00', 0, NULL, DEFAULT, 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (6, 14, '2025-07-19 15:00:00', 1, NULL, DEFAULT, 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (13, 5, '2025-11-29 11:00:00', 1, NULL, DEFAULT, 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (2, 9, '2025-04-01 10:00:00', 0, NULL, DEFAULT, 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (10, 2, '2025-08-27 10:00:00', 1, NULL, DEFAULT, 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (7, 11, '2025-01-26 09:00:00', 0, NULL, DEFAULT, 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (9, 7, '2025-10-21 12:00:00', 1, NULL, DEFAULT, 1);
GO

--==================================================================================
-- Bloque 2: TURNOS PASADOS, CON ASISTENCIA Y DESCRIPCIÓN
-- (Fecha pasada, Pendiente = 0, Asistencia = 1, Descripción completa)
--==================================================================================
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (3, 6, '2024-05-22 14:00:00', 0, 1, 'Control de rutina. Paciente estable.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (12, 15, '2024-04-12 16:00:00', 0, 1, 'Seguimiento por hipertensión. Se ajusta medicación.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (8, 4, '2024-03-11 11:00:00', 0, 1, 'Consulta por cuadro gripal. Se receta reposo.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (1, 10, '2024-02-12 15:00:00', 0, 1, 'Presenta alergia estacional. Tratamiento sintomático.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (14, 13, '2024-01-21 10:00:00', 0, 1, 'Revisión de resultados de laboratorio. Todo en orden.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (4, 2, '2024-05-30 08:00:00', 0, 1, 'Dolor de espalda bajo. Se recomienda fisioterapia.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (11, 11, '2023-12-21 11:00:00', 0, 1, 'Chequeo anual completo. Sin novedades.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (5, 5, '2024-05-10 18:00:00', 0, 1, 'Consulta por migrañas recurrentes. Se deriva a neurología.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (6, 1, '2024-04-20 12:00:00', 0, 1, 'Paciente refiere estrés laboral. Se otorgan recomendaciones.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (9, 7, '2024-03-19 10:00:00', 0, 1, 'Control post-operatorio. Evolución favorable.', 1);
GO

--==================================================================================
-- Bloque 3: TURNOS PASADOS, SIN ASISTENCIA
-- (Fecha pasada, Pendiente = 0, Asistencia = 0, Descripción de ausencia)
--==================================================================================
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (7, 3, '2024-02-04 08:00:00', 1, 0, 'Paciente ausente. No se presentó a la cita.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (13, 8, '2024-01-27 16:00:00', 1, 0, 'Paciente ausente. No se presentó a la cita.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (2, 12, '2023-11-21 09:00:00', 1, 0, 'Paciente ausente. No se presentó a la cita.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (10, 14, '2023-10-18 10:00:00', 1, 0, 'Paciente ausente. No se presentó a la cita.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (15, 1, '2024-05-20 18:00:00', 1, 0, 'Paciente ausente. No se presentó a la cita.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (1, 6, '2024-04-22 10:00:00', 1, 0, 'Paciente ausente. No se presentó a la cita.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (3, 9, '2024-03-27 11:00:00', 1, 0, 'Paciente ausente. No se presentó a la cita.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (5, 4, '2024-02-16 14:00:00', 1, 0, 'Paciente ausente. No se presentó a la cita.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (8, 11, '2024-01-29 09:00:00', 1, 0, 'Paciente ausente. No se presentó a la cita.', 1);
INSERT INTO Turno (LegajoMedico_TU, LegajoPaciente_TU, Fecha_TU, Pendiente_TU, Asistencia_TU, Descripcion_TU, Estado_TU) VALUES (12, 13, '2023-12-08 13:00:00', 1, 0, 'Paciente ausente. No se presentó a la cita.', 1);
GO

