using VolunteerManagementSystem.Models;

namespace VolunteerManagementSystem.Data
{
    public class OpportunityRepository : IOpportunityRepository
    {
        private readonly ApplicationDbContext _context;

        public OpportunityRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Opportunity GetById(int id)
        {
            return _context.Opportunities.Find(id);
        }

        public IEnumerable<Opportunity> GetAll()
        {
            return _context.Opportunities.ToList();
        }

        public void Add(Opportunity opportunity)
        {
            _context.Opportunities.Add(opportunity);
            _context.SaveChanges();
        }

        public void Update(Opportunity opportunity)
        {
            _context.Opportunities.Update(opportunity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var opportunity = _context.Opportunities.Find(id);
            if (opportunity != null)
            {
                _context.Opportunities.Remove(opportunity);
                _context.SaveChanges();
            }
        }
    }
}
