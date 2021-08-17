using PetApp;
using PetDL.Entities;
using PetDL;
using BL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

//setting up my db connections
string connectionString = configuration.GetConnectionString("petdb");
//we're building the dbcontext using the constructor that takes in options, we're setting the connection
//string outside the context def'n
DbContextOptions<petdbContext> options = new DbContextOptionsBuilder<petdbContext>()
.UseSqlServer(connectionString)
.Options;
//passing the options we just built
var context = new petdbContext(options);

IMenu menu = new MainMenu(new PetBL(new PetRepo(context)));
menu.Start(); 