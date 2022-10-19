# README #

# Sistema creado en C# .NET API CORE 6. #

Contiene EndPoint para peticiones a la base de datos, internamente tambien hace una peticion a datos publicos de:
The San Francisco government’s website has a public data source of food trucks
(https://data.sfgov.org/Economy-and-Community/Mobile-Food-Schedule/jjew-r69b).

Genera un token sin buscar usuarios en la base de datos, esto es para ejemplificar el uso de JWT ya que los Controladores tienen seguridad.

### Detalles generales de las tecnologias usadas ###

* EF Core 6
* Swagger
* SQLServer
* JSON Web Token
* Dapper
* Arquitectura multicapa
* Inyeccion de dependencias

### Pasos para compilar ###

* Debe ejecutarse con Visual Studio 2022 por compatibilidad para CORE 6
* Se utilizó base de datos localhost 
* Server=localhost;Database=master;Trusted_Connection=True;
* Se debe aplicar migraciòn con conexion a localhost
* Comando update-migration desde la Consola de  Administrador de Paquetes
* Base de Datos
* Ejecutar el Script que viene en el proyecto en la raiz del proyecto   ../SCRIPT_SPS_Y_TABLA
* Contiene dos procedimientos almacenados para consultar la base de datos y agregar datos iniciales a la tabla.
