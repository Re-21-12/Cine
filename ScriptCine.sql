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