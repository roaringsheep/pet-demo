using Models;
using PetDL;

namespace BL
{
    public class PetBL : IPetBL
    {
        private IPetRepo _repo;

        public PetBL(IPetRepo repo)
        {
            _repo = repo;
        }

        public List<Cat> ViewAllCats()
        {
            return _repo.GetAllCats();
        }

        public Cat AddACat(Cat cat)
        {
            return _repo.AddACat(cat);
        }

        public Meal AddAMeal(Meal meal)
        {
            return _repo.AddAMeal(meal);
        }

        public Cat SearchCatByName(string name)
        {
            return _repo.SearchCatByName(name);
        }

        public List<Meal> GetMealsByCatId(int catId)
        {
            return _repo.GetMealsByCatId(catId);
        }

        public void DeleteACat(Cat cat)
        {
            _repo.DeleteACat(cat);
        }

    }
}