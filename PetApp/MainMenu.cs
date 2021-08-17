namespace PetApp
{
    public class MainMenu : IMenu
    {
        public void Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("Welcome to Cat Manager!");
                Console.WriteLine("[0] Exit");
                Console.WriteLine("[1] Add a cat");
                Console.WriteLine("[2] Feed a cat");
                Console.WriteLine("[3] View my cats");


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
            throw new NotImplementedException();
        }

        private void FeedACat()
        {
            throw new NotImplementedException();
        }

        private void AddNewCat()
        {
            throw new NotImplementedException();
        }
    }
}