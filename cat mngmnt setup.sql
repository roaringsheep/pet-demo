drop table [dbo].[Meals];
drop table [dbo].[Cats];

create Table [dbo].[Cats](
	Id int primary key identity(1,1),
	Name varchar(100) not null
);

create Table [dbo].[Meals](
	Id int primary key identity(1,1),
	Time DateTime not null,
	CatId int Foreign Key References [dbo].[Cats](Id),
	FoodType varchar(100) not null
);

insert into [dbo].[Cats] (Name) values ('Auryn'), ('Stinker');