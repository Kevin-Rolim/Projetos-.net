use master

create database EventFlow

use EventFlow

--Modelo Lógico
--Eventos (_Id_,titulo,descricao,datahora,situacao,local,capacidadeMaxima)
--Participantes(_Id_,nome,email,telefone)
--Inscricoes (_id_eventos_,_id_participantes_,data_inscricao,situacao)

create TABLE Eventos
(
	id 					int  				primary key identity,
	titulo				varchar(50)			not null,
	descricao			varchar(100)		null,
	datahora			datetime2(0)		not null,
	situacao			int 				not null,
	local				varchar(100)			not null,
	capacidadeMaxima	int					not null,
	
	constraint CK_Eventos_Situacao 			check (situacao in (1, 2, 3, 4)),
	constraint CK_Eventos_Capacidade 		check (capacidadeMaxima > 0)
)

create TABLE Participantes
(
	id 					int  				primary key identity,
	nome				varchar(100)		not null,
	email				varchar(100)		not null,
	telefone			varchar(50)			not null,
	
	constraint UQ_Participantes_Email	   unique (email)
)

create table Inscricoes
(
	evento_id			int 				not null,
	participante_id		int					not null,
	data_inscricao		datetime2(0)		not null,
	situacao			int					not null,
	
	constraint PK_Inscricoes				PRIMARY KEY (evento_id, participante_id),
	constraint FK_Inscricoes_eventos 		FOREIGN KEY (evento_id)
        									REFERENCES Eventos(id),
    constraint FK_Inscricoes_participantes 	FOREIGN KEY (participante_id)
        									REFERENCES Participantes(id),
        
   	constraint CK_Inscricoes_Situacao check (situacao in (1, 2, 3, 4))
)

