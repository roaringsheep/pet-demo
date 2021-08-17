using Models;

namespace PetDL
{
    public interface IPetRepo
    {
        List<Cat> GetAllCats();

        Cat AddACat(Cat cat);

        Meal AddAMeal(Meal meal);
    }
}