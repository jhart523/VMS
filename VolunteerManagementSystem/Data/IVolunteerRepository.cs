using VolunteerManagementSystem.Models;

namespace VolunteerManagementSystem.Data
{
    public interface IVolunteerRepository
    {
        Volunteer GetById(int id);
        IEnumerable<Volunteer> GetAll();
        void Add(Volunteer volunteer);
        void Update(Volunteer volunteer);
        void Delete(int id);   


    }
}
