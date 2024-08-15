using System.Collections.Generic;
using VolunteerManagementSystem.Models;

namespace VolunteerManagementSystem.Data
{
    public interface IOpportunityRepository
    {
        Opportunity GetById(int id);
        IEnumerable<Opportunity> GetAll();
        void Add(Opportunity opportunity);
        void Update(Opportunity opportunity);
        void Delete(int id);
    }
}
