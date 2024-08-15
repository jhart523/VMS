using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunteerManagementSystem.Data;
using VolunteerManagementSystem.Models;

namespace VolunteerManagementSystem.Controllers
{
    [Authorize]
    public class VolunteersController : Controller
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IOpportunityRepository _opportunityRepository;

        public VolunteersController(IVolunteerRepository volunteerRepository, IOpportunityRepository opportunityRepository)
        {
            _volunteerRepository = volunteerRepository;
            _opportunityRepository = opportunityRepository;
        }

        public IActionResult Index(string filter, string search)
        {          
            var volunteers = _volunteerRepository.GetAll();

            // Apply the filter first, then that filtered selection can go through the search if there is a search term included by the user.
            switch (filter)
            {
                case "Approved/Pending Approval":
                    volunteers = volunteers.Where(v => v.ApprovalStatus == "Approved" || v.ApprovalStatus == "Pending Approval");
                    break;
                case "Approved":
                    volunteers = volunteers.Where(v => v.ApprovalStatus == "Approved");
                    break;
                case "Pending Approval":
                    volunteers = volunteers.Where(v => v.ApprovalStatus == "Pending Approval");
                    break;
                case "Disapproved":
                    volunteers = volunteers.Where(v => v.ApprovalStatus == "Disapproved");
                    break;
                case "Inactive":
                    volunteers = volunteers.Where(v => v.ApprovalStatus == "Inactive");
                    break;
                case "All":
                default:
                    // This is the option if no filter is applied to our search
                    break;
            }

            // This part is for the search bar logic, it is not case-sensitive and does not include Emergency contact info in the search
            if (!string.IsNullOrEmpty(search))
            {
                volunteers = volunteers.Where(v =>
                    v.FirstName.ToLower().Contains(search.ToLower()) ||
                    v.LastName.ToLower().Contains(search.ToLower()) ||
                    v.Username.ToLower().Contains(search.ToLower()) ||
                    v.Email.ToLower().Contains(search.ToLower()) ||
                    v.Centers.ToLower().Contains(search.ToLower()) ||
                    v.Skills.ToLower().Contains(search.ToLower()) ||
                    v.Availability.ToLower().Contains(search.ToLower()) ||
                    v.Address.ToLower().Contains(search.ToLower()) ||
                    v.PhoneNumbers.ToLower().Contains(search.ToLower()) ||
                    v.EducationalBackground.ToLower().Contains(search.ToLower()) ||
                    v.Licenses.ToLower().Contains(search.ToLower()) ||                  
                    v.ApprovalStatus.ToLower().Contains(search.ToLower())
                    );
            }

            // This will display a message to the Admin if no volunteers match the search criteria
            if (!volunteers.Any())
            {
                ViewBag.Message = "No Volunteers match the search criteria.";
            }
            
            return View(volunteers);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Volunteer obj)
        {
           
            if (ModelState.IsValid)
            {
                _volunteerRepository.Add(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var volunteer = _volunteerRepository.GetById(id);
            
            
            return View(volunteer);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Volunteer obj)
        {

            if (ModelState.IsValid)
            {
                _volunteerRepository.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Matches(int id)
        {
            var opps = _opportunityRepository.GetAll();
            var vol = _volunteerRepository.GetById(id);
            // can add more, not sure what to compare
            var matches = opps.Where(x => x.Title.Contains(vol.Skills) || x.Description.Contains(vol.Skills) 
                                     || x.Title.Contains(vol.Centers) || x.Description.Contains(vol.Centers));

            // if no matches
            if (!matches.Any())
            {
                ViewBag.Message = "No opportunities found.";
            }
            return View(matches);
        }
    }
}
