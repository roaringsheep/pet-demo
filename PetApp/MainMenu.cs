using BL;
using Models;

namespace PetApp
{
    public class MainMenu : IMenu
    {
        private IPetBL _petBL;
        public MainMenu(IPetBL petBL) {
            _petBL = petBL;
        }
        public void Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("Welcome to Cat Manager!");
                Console.WriteLine("[0] Exit");
                Console.WriteLine("[1] Add a cat");
                Console.WriteLine("[2] Feed a cat");
                Console.WriteLine("[3] View all cats");
                Console.WriteLine("[4] Search for a cat");
                Console.WriteLine("[5] View a cat's meal");
                Console.WriteLine("[6] Remove a cat");

                switch(Console.ReadLine())
                {
                    case "0":
                        repeat = false;
                    break;

                    case "1":
                        AddNewCat();
                    break;

                    case "2":
                        FeedACat();
                    break;

                    case "3":
                        ViewAllCats();
                    break;

                    case "4":
                        SearchCatByName();
                    break;

                    case "5":
                        ViewMealsByCat();
                    break;

                    case "6":
                        DeleteACat();
                    break;

                    default:
                        Console.WriteLine("I don't understand your input, please try again");
                    break;
                }
            
            }while(repeat);
            
        }

        private void ViewAllCats()
        {
            List<Cat> cats = _petBL.ViewAllCats();
            foreach(Cat cat in cats)
            {
                Console.WriteLine("Name: " + cat.Name);
            }
        }

        private void FeedACat()
        {
            List<Cat> cats = _petBL.ViewAllCats();
            string foodType;
            Cat selectedCat = SelectACat(cats, "Pick a cat to feed");
            if(selectedCat is not null) {

                do
                {
                    Console.WriteLine("You picked " + selectedCat.Name);
                    Console.WriteLine("What Type of food are you feeding them?");
                    foodType = Console.ReadLine();
                }while(String.IsNullOrWhiteSpace(foodType));
                
                Meal mealToFeed = new Meal(selectedCat.Id, foodType);

                try
                {
                    mealToFeed = _petBL.AddAMeal(mealToFeed);
                    Console.WriteLine("Meal Added!");
                    Console.WriteLine("Type: " + mealToFeed.FoodType);
                    Console.WriteLine("Time: " + mealToFeed.Time);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            
        }

        private void AddNewCat()
        {
            string name;
            Cat catToAdd;
            
            Console.WriteLine("Add a new cat");
            do
            {
                Console.WriteLine("Enter Name: ");
                name = Console.ReadLine();
                
            }while(String.IsNullOrWhiteSpace(name));

            catToAdd = new Cat(name);
            catToAdd = _petBL.AddACat(catToAdd);

            Console.WriteLine("You added a Cat: " + catToAdd.Name);
        }

        private Cat SelectACat(List<Cat> cats, string prompt)
        {
            Console.WriteLine(prompt);
            int selection;
            bool valid = false;
            do
            {
                for(int i = 0; i < cats.Count; i++)
                {
                    Console.WriteLine($"[{i}] {cats[i].Name}");
                }
                //tryparse returns bool, but also have out argument for number
                valid = int.TryParse(Console.ReadLine(), out selection);

                //was the parsing successful 
                //And the selection within the bounds of cats list?
                if(valid && (selection >= 0 && selection < cats.Count))
                {
                    //if so, return the selected cat
                    return cats[selection];
                }
                Console.WriteLine("Enter a valid number");
            } while(true);

        }

        private void SearchCatByName()
        {
            string input;
            Console.WriteLine("Enter the name of the cat to search: ");
            input = Console.ReadLine();

            Cat foundCat = _petBL.SearchCatByName(input);
            if(foundCat.Name is null)
            {
                Console.WriteLine($"{input} is missing, please return them asap :'(");
            }
            else {
                Console.WriteLine("We found the cat! {0}", foundCat.Name);
            }
        }

        private void ViewMealsByCat()
        {
            List<Cat> cats = _petBL.ViewAllCats();
            Cat selectedCat = SelectACat(cats, "Pick a cat to feed");
            List<Meal> meals = _petBL.GetMealsByCatId(selectedCat.Id);
            if(meals.Count == 0) Console.WriteLine("No meal was found");
            else
            {
                foreach(Meal meal in meals)
                {
                    Console.WriteLine(
                        $"FoodType: {meal.FoodType}, Time {meal.Time}"
                    );
                }
            }
        }

        private void DeleteACat()
        {
            List<Cat> cats = _petBL.ViewAllCats();
            Cat selectedCat = SelectACat(cats, "Pick a cat to remove");
            _petBL.DeleteACat(selectedCat);
            cats = _petBL.ViewAllCats();
            foreach(Cat cat in cats)
            {
                Console.WriteLine(cat.Name);
            }
        }
    }
}