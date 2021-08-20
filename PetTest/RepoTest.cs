using Xunit;
using Microsoft.EntityFrameworkCore;
using Entity = PetDL.Entities;
using Models;
using PetDL;

//install Microsoft.EntityFrameworkCore.Sqlite for mocking db
namespace PetTest;

public class RepoTest
{
    private readonly DbContextOptions<Entity.petdbContext> options;

    public RepoTest()
    {
        options = new DbContextOptionsBuilder<Entity.petdbContext>().UseSqlite("Filename=Test.db").Options;
        Seed();
    }

    [Fact]
    public void GetAllCatsShouldGetAllCats()
    {
        using(var context = new Entity.petdbContext(options))
        {
            IPetRepo _repo = new PetRepo(context);

            var cats = _repo.GetAllCats();

            Assert.Equal(2, cats.Count);
        }
    }

    [Fact]
    public void AddACatShouldAddACat()
    {
        //With Create, Update, and Delete operations
        //We want to make sure the changes persist by using two different dbcontexts

        //Arrange
        using (var testcontext = new Entity.petdbContext(options))
        {
            IPetRepo _repo = new PetRepo(testcontext);

            //Act
            _repo.AddACat(
                new Models.Cat {
                    Id = 3,
                    Name = "Ginger"
                }
            );
        }

        using(var assertContext = new Entity.petdbContext(options))
        {
            Entity.Cat cat = assertContext.Cats.FirstOrDefault(cat => cat.Id == 3);

            Assert.NotNull(cat);
            Assert.Equal("Ginger", cat.Name);
        }
    }

    private void Seed()
    {
        using(var context = new Entity.petdbContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Cats.AddRange(
                new Entity.Cat{
                    Id = 1,
                    Name = "Auryn",
                    Meals = new List<Entity.Meal> {
                        new Entity.Meal {
                            Id = 1,
                            Time = DateTime.Now,
                            FoodType = "Dry"
                        },
                        new Entity.Meal {
                            Id = 2,
                            Time = DateTime.Now,
                            FoodType = "Raw"
                        },
                        new Entity.Meal {
                            Id = 3,
                            Time = DateTime.Now,
                            FoodType = "Wet"
                        }
                    }
                },
                new Entity.Cat {
                    Id = 2,
                    Name = "Snow",
                    Meals = new List<Entity.Meal> {
                        new Entity.Meal {
                            Id = 4,
                            Time = DateTime.Now,
                            FoodType = "Dry"
                        },
                        new Entity.Meal {
                            Id = 5,
                            Time = DateTime.Now,
                            FoodType = "Raw"
                        },
                        new Entity.Meal {
                            Id = 6,
                            Time = DateTime.Now,
                            FoodType = "Wet"
                        }
                    }
                }
            );
            context.SaveChanges();
        }
    }
}