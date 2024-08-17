using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunteerManagementSystem.Data;
using VolunteerManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VolunteerManagementSystem.Controllers
{
    [Authorize]
    public class OpportunitiesController : Controller
    {
        private readonly IOpportunityRepository _opportunityRepository;
        private readonly IVolunteerRepository _volunteerRepository;
        
        public OpportunitiesController(IOpportunityRepository opportunityRepository, IVolunteerRepository volunteerRepository)
        {
            _opportunityRepository = opportunityRepository;
            _volunteerRepository = volunteerRepository;
        }

        public IActionResult Index(string search)
        {
            // First, get all opportunities from DB
            var opportunities = _opportunityRepository.GetAll();

            // If there is a search term, filter by this term first
            if (!string.IsNullOrEmpty(search))
            {
                opportunities = opportunities.Where(o => o.Title.ToLower().Contains(search.ToLower()) || o.Description.ToLower().Contains(search.ToLower()));
            }
         
            return View(opportunities);
         
        }

        public IActionResult Recent()
        {
            var recentTime = DateTime.Now.AddDays(-60);
            var opportunities = _opportunityRepository.GetAll();
            opportunities = opportunities.Where(o => o.Date >= recentTime).ToList();
            return View("Index", opportunities);
        }


        public IActionResult centerFilter(string filter)
        {
            
            return View(filter);
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

        //GET
        public IActionResult Edit(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var opp = _opportunityRepository.GetById(id);
            return View(opp);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Opportunity obj)
        {
            if (ModelState.IsValid)
            {
                _opportunityRepository.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _opportunityRepository.Delete(id);
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Matches(int id)
        {
            var opp = _opportunityRepository.GetById(id);
            var vol = _volunteerRepository.GetAll();
            // can add more, not sure what to compare
            var matches = vol.Where(v => v.Skills.ToLower().Contains(opp.Description.ToLower()) || v.Skills.ToLower().Contains(opp.Title.ToLower())
            || v.Centers.ToLower().Contains(opp.Center.ToLower()));

            // if no matches, add viewbag message that will display
            if (!matches.Any())
            {
                ViewBag.Message = "No volunteers found.";
            }
            return View(matches);
        }

        
    }
}
