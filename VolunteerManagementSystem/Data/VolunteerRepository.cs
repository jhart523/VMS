using VolunteerManagementSystem.Models;

namespace VolunteerManagementSystem.Data
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly ApplicationDbContext _context;

        public VolunteerRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public Volunteer GetById(int id) 
        {
            return _context.Volunteers.Find(id);
        }

        public IEnumerable<Volunteer> GetAll()
        {
            return _context.Volunteers.ToList();
        }

        public void Add(Volunteer v) 
        {
            _context.Volunteers.Add(v);
            _context.SaveChanges();
        }

        public void Update(Volunteer v) 
        {
            _context.Volunteers.Update(v);
            _context.SaveChanges();
        }

        public void Delete(int id) 
        {
            var volunteer = _context.Volunteers.Find(id);
            if (volunteer != null) 
            {
                _context.Volunteers.Remove(volunteer);
                _context.SaveChanges();
            }
        }
    }
}
