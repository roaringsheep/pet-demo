using Xunit;
using Microsoft.EntityFrameworkCore;
using Entity = PetDL.Entities;
using Models;

//install Microsoft.EntityFrameworkCore.Sqlite for mocking db
namespace PetTest;

public class RepoTest
{
    private readonly DbContextOptions<Entity.petdbContext> options;

    public RepoTest()
    {
        options = new DbContextOptionsBuilder<Entity.petdbContext>().UseSqlite("Filename=Test.db").Options();
        Seed();
    }

    [Fact]
    public void Test1()
    {

    }
}