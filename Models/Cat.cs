namespace Models
{
    public class Cat
    {
        public Cat() {}

        public Cat(string name)
        {
            this.Name = name;
        }

        public Cat(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public int Id {get; set;}

        public string Name {get; set;}

        public List<Meal> Meals { get; set; }
    }
}