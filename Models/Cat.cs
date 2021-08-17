namespace Models
{
    public class Cat
    {
        public Cat() {}

        public string? Name {get; set;}

        public List<Meal>? Meals { get; set; }
    }
}