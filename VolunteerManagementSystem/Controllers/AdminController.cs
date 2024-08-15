using Microsoft.AspNetCore.Mvc;
using VolunteerManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;

namespace VolunteerManagementSystem.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IVolunteerRepository _volunteerRepository;
        private IOpportunityRepository _opportunityRepository;

        public AdminController(IVolunteerRepository vRepo, IOpportunityRepository oRepo)
        {
            _volunteerRepository = vRepo;
            _opportunityRepository = oRepo;
        }
        public ViewResult Index() => View(_volunteerRepository.GetAll());
    }
}
