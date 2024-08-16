using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunteerManagementSystem.Data;
using VolunteerManagementSystem.Models;

namespace VolunteerManagementSystem.Controllers
{
    public class OpportunitiesController : Controller
    {
        private readonly IOpportunityRepository _opportunityRepository;
        private readonly IVolunteerRepository _volunteerRepository;
        
        public OpportunitiesController(IOpportunityRepository opportunityRepository, IVolunteerRepository volunteerRepository)
        {
            _opportunityRepository = opportunityRepository;
            _volunteerRepository = volunteerRepository;
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
            var matches = vol.Where(v => v.Skills.Contains(opp.Description) || v.Skills.Contains(opp.Title)
            || v.Centers.Contains(opp.Center));

            // if no matches
            if (!matches.Any())
            {
                ViewBag.Message = "No volunteers found.";
            }
            return View(matches);
        }
    }
}
