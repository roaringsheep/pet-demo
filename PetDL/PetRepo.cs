using Models;
using PetDL.Entities;

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
    }
}