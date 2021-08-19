using Models;

namespace BL
{
    public interface IPetBL
    {
        List<Cat> ViewAllCats();

        Cat AddACat(Cat cat);

        Meal AddAMeal(Meal meal);

        Cat SearchCatByName(string name);

        List<Meal> GetMealsByCatId(int catId);

        void DeleteACat(Cat cat);
    }
}