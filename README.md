# Proyecto - Sistema Cine
 En el presente prooyecto se solicita crear un sistema de administracion de Cine, donde se desarrollaron las siguientes listas de opciones para dos casos distintos

- Administrador
  - Gestionar Salas: Este puede crear, eliminar, editar y detallar la informacion de la _Sala_.
  - Gestionar Peliculas: Este puede crear, eliminar, editar y detallar la informacion de la _Pelicula_.
  - Gestionar Asientos: Este puede crear, eliminar, editar y detallar la informacion de el _Asiento_.
   
- Cliente
  - Ver Cartelera: Puede visualizar las peliculas y las ultimas 3 peliculas en un carrousel
  - Filtrar Peliculas: Filtra las peliculas por _fecha_, _hora_ y _nombre_
  - Comprar Ticket: Genera la opcion de comprar un ticket segun la pelicula seleccionada.

# Herramientas Utilizadas
- _Back-End_ & _Front-End_ :
  - Asp .Net Core usando el modelo _modelo vista controlador_ en la version de _.Net 6_ 
  - Jquery utilizando para realizar menores ajustes en los cambios de algunos elementos html
  - Boostrap se utilizo para el diseño visual de los elementos renderizados en las vistas
  - Entity Framework utilizando el enfoque de desarrollo _data base first_ donde primeramente se diseño la base de datos para posteriormente generar los modelos y sus controladores.
- Base de datos:
  - Sql Server
<hr/>

## Casos de uso

![CasosdeusoCine](https://github.com/Re-21-12/Cine/assets/104967229/06506fc8-9930-467d-bc01-8cfd72ffbde4)

## Diagrama ER

![Diagrama ER DBML Cine](https://github.com/Re-21-12/Cine/assets/104967229/4adc7e5a-265f-4688-be35-dcac55a271ef)

<hr/>

## Script de la base de datos en SQLSERVER
```
-- create database Cine
use Cine
create table Pelicula(
Id int identity(1,1) primary key not null,
Nombre varchar(255) not null,
Clasificacion varchar(1) not null,
DiaTransmision DATE not null,
Inicio TIME not null,
Final TIME not null,
Idioma varchar(100) not null,
Genero varchar(100) not null,
Sinopsis varchar(100) not null,	
Actores varchar(300) not null,
Director varchar(100) not null,
Duracion varchar(100) not null,
url varchar(2000),
Estado int not null, -- 1 Activo | 0 Inactivo
NumeroSala int,
foreign key (NumeroSala) references Sala(NumeroSala)
)

create table Sala(
NumeroSala int identity(1,1) primary key,
Asientos int not null,
Estado varchar(50)
)

create table Asiento(
NumeroAsiento int primary key,
Estado varchar(50),
NumeroSala int , -- esta fila hace referencia a el numero de sala de la tabla Sala
foreign key (NumeroSala) references Sala(NumeroSala)
)

create table Cliente(
Id int identity(1,1) primary key,
NIT varchar(13),
Nombre varchar(255) not null,
Ticket int not null,
NumeroPelicula int ,
NumeroSala int ,
NumeroAsiento int 
foreign key(NumeroPelicula) references Pelicula(Id),
foreign key(NumeroSala) references Sala(NumeroSala),
foreign key(NumeroAsiento) references Asiento(NumeroAsiento)
)
```
## Script del generado de el diagrama utilizando DBML
```
Table Pelicula {
  Id int [primary key, not null] // identity(1,1)
  Nombre varchar(255) [not null]
  Clasificacion varchar(1) [not null]
  DiaTransmision date [not null]
  Inicio time [not null]
  Final time [not null]
  Idioma varchar(100) [not null]
  Genero varchar(100) [not null]
  Sinopsis varchar(100) [not null]
  Actores varchar(300) [not null]
  Director varchar(100) [not null]
  Duracion varchar(100) [not null]
  url varchar(2000)
  Estado int [not null] // 1 Activo | 0 Inactivo
  NumeroSala int [ref: > Sala.NumeroSala]
}

Table Sala {
  NumeroSala int [primary key]
  Asientos int [not null]
  Estado varchar(50)
}

Table Asiento {

  NumeroAsiento int [primary key]
  Estado varchar(50)
  NumeroSala int [ref: > Sala.NumeroSala]
}

Table Cliente {
  Id int [primary key]
  NIT varchar(13) [not null]
  Nombre varchar(255) [not null]
  Ticket int [not null]
  NumeroPelicula int [ref: > Pelicula.Id]
  NumeroSala int [ref: > Sala.NumeroSala]
  NumeroAsiento int [ref: > Asiento.NumeroAsiento]
}

```
