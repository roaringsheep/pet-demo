using Models;
using PetDL.Entities;
using System.Linq;


namespace PetDL
{
    public class PetRepo : IPetRepo
    {
        private Entities.petdbContext _context;
        public PetRepo(Entities.petdbContext context)
        {
            _context = context;
        }
        public List<Models.Cat> GetAllCats()
        {
            return _context.Cats
            .Select(
                cat => new Models.Cat(cat.Id, cat.Name)
            ).ToList();
        }

        public Models.Cat AddACat(Models.Cat cat)
        {
            Entities.Cat catToAdd = new Entities.Cat();
            catToAdd.Name = cat.Name;
            
            _context.Cats.Add(catToAdd);

            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return cat;
        }

        public Models.Meal AddAMeal(Models.Meal meal)
        {
            Entities.Meal mealToFeed = new Entities.Meal{
                CatId = meal.CatId,
                Time = meal.Time,
                FoodType = meal.FoodType
            };

            _context.Meals.Add(mealToFeed);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return meal;
        }

        public Models.Cat SearchCatByName(string name)
        {
            Entities.Cat foundCat =  _context.Cats
                .FirstOrDefault(cat => cat.Name == name);
            if(foundCat != null)
            {
                return new Models.Cat(foundCat.Id, foundCat.Name);
            }
            return new Models.Cat();
        }

        public List<Models.Meal> GetMealsByCatId(int catId)
        {
            return _context.Meals
                    .Where(meal => meal.CatId == catId)
                    .Select(meal => new Models.Meal{
                        Time = meal.Time,
                        CatId = (int) meal.CatId,
                        FoodType = meal.FoodType
                    })
                    .ToList();
        }

        public void DeleteACat(Models.Cat cat)
        {
            Entities.Cat catToDelete = new Entities.Cat
            {
                Id = cat.Id,
                Name = cat.Name
            };
            _context.Cats.Remove(catToDelete);
            _context.SaveChanges();
        }

    }
}