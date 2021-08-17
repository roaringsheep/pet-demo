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
    }
}