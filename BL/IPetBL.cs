using Models;

namespace BL
{
    public interface IPetBL
    {
        List<Cat> ViewAllCats();

        Cat AddACat(Cat cat);

        Meal AddAMeal(Meal meal);
    }
}