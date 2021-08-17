using Models;

namespace PetDL
{
    public class PetRepo : IPetRepo
    {
        public List<Cat> GetAllCats()
        {
            return new List<Cat>();
        }
    }
}