Este fué un sistema de gestión creado en forma grupal por compañeros de la universidad UTN. El TPI está realizado en .NET Framework 4.8, utiliazando WebForms. Con conexión a Base de Datos, utilizando SQL Server.
Toda la lógica, las clases, los objetos, están diseñados con el lenguaje C# primordialmente. También intentamos manejar todas las Querys SQL dentro del mismo programa.
La arquitectura es algo más bien clásico, en donde separamos en 3 capas relacionada (Acceso Datos, Negocio y Vistas) y una capa llamada Entidades, la cual es independiente.
La idea fué recrear el funcionamiento de una clínica real, en donde hay usuarios médicos, administradores, pacientes. 
Todos los datos de valor se guardan en la DB.  
permiten realizar una cantidad de funciones bastante grande, incluyendo lo clásico: ABML.

ATENCIÓN: para que el programa pueda correr sin errores, es necesario crear la Base de datos y mantener la conexión abierta, los scripts se encuentran en este mismo proyecto. También se incluyen los scripts de 
datos que necesitan utilizarse para llenar las tablas y que la aplicación se encuentre completamente funcional. De más está decir que necesitarían un IDE como el Visual Studio Community 2022, que es donde se
realizó de principio a fin.


