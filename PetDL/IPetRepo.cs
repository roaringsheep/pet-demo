using Models;

namespace PetDL
{
    public interface IPetRepo
    {
        List<Cat> GetAllCats();
    }
}