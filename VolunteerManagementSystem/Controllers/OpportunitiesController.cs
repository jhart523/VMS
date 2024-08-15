using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunteerManagementSystem.Data;
using VolunteerManagementSystem.Models;

namespace VolunteerManagementSystem.Controllers
{
    public class OpportunitiesController : Controller
    {
        private readonly IOpportunityRepository _opportunityRepository;
        
        public OpportunitiesController(IOpportunityRepository opportunityRepository)
        {
            _opportunityRepository = opportunityRepository;
        }

        public IActionResult Index()
        {
            var opportunities = _opportunityRepository.GetAll();
            return View(opportunities);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Opportunity obj)
        {

            if (ModelState.IsValid)
            {
                _opportunityRepository.Add(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
