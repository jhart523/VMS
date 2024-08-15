using System.Collections.Generic;
using System.Linq;
using VolunteerManagementSystem.Models;

namespace VolunteerManagementSystem.Data
{
    public class FakeVolunteerRepository : IVolunteerRepository
    {
        private readonly List<Volunteer> _volunteers;

        public FakeVolunteerRepository()
        {
            // Seed with some fake volunteers for testing
            _volunteers = new List<Volunteer>
            {
                new Volunteer
                {
                    Id = 1, FirstName = "John", LastName = "Doe", Username = "johndoe", Password = "password123",
                    Centers = "Center1", Skills = "Skill1", Availability = "Weekdays", Address = "123 Main St",
                    PhoneNumbers = "555-1234", Email = "john.doe@example.com", EducationalBackground = "Bachelor's Degree",
                    Licenses = "None", EmergencyContactName = "Jane Doe", EmergencyContactPhone = "555-5678",
                    EmergencyContactEmail = "jane.doe@example.com", EmergencyContactAddress = "123 Elm St",
                    DriverLicenseOnFile = true, SocialSecurityCardOnFile = true, ApprovalStatus = "Approved"
                },
                new Volunteer
                {
                    Id = 2, FirstName = "Jane", LastName = "Smith", Username = "janesmith", Password = "password456",
                    Centers = "Center2", Skills = "Skill2", Availability = "Weekends", Address = "456 Oak St",
                    PhoneNumbers = "555-8765", Email = "jane.smith@example.com", EducationalBackground = "Master's Degree",
                    Licenses = "License1", EmergencyContactName = "John Smith", EmergencyContactPhone = "555-4321",
                    EmergencyContactEmail = "john.smith@example.com", EmergencyContactAddress = "456 Pine St",
                    DriverLicenseOnFile = false, SocialSecurityCardOnFile = true, ApprovalStatus = "Pending Approval"
                }
                
            };
        }

        public Volunteer GetById(int id)
        {
            return _volunteers.FirstOrDefault(v => v.Id == id);
        }

        public IEnumerable<Volunteer> GetAll()
        {
            return _volunteers;
        }

        public void Add(Volunteer volunteer)
        {
            volunteer.Id = _volunteers.Max(v => v.Id) + 1; // Simulate auto-increment ID
            _volunteers.Add(volunteer);
        }

        public void Update(Volunteer volunteer)
        {
            var existingVolunteer = GetById(volunteer.Id);
            if (existingVolunteer != null)
            {
                // Update properties as needed
                existingVolunteer.FirstName = volunteer.FirstName;
                existingVolunteer.LastName = volunteer.LastName;
                existingVolunteer.Username = volunteer.Username;
                existingVolunteer.Password = volunteer.Password;
                existingVolunteer.Centers = volunteer.Centers;
                existingVolunteer.Skills = volunteer.Skills;
                existingVolunteer.Availability = volunteer.Availability;
                existingVolunteer.Address = volunteer.Address;
                existingVolunteer.PhoneNumbers = volunteer.PhoneNumbers;
                existingVolunteer.Email = volunteer.Email;
                existingVolunteer.EducationalBackground = volunteer.EducationalBackground;
                existingVolunteer.Licenses = volunteer.Licenses;
                existingVolunteer.EmergencyContactName = volunteer.EmergencyContactName;
                existingVolunteer.EmergencyContactPhone = volunteer.EmergencyContactPhone;
                existingVolunteer.EmergencyContactEmail = volunteer.EmergencyContactEmail;
                existingVolunteer.EmergencyContactAddress = volunteer.EmergencyContactAddress;
                existingVolunteer.DriverLicenseOnFile = volunteer.DriverLicenseOnFile;
                existingVolunteer.SocialSecurityCardOnFile = volunteer.SocialSecurityCardOnFile;
                existingVolunteer.ApprovalStatus = volunteer.ApprovalStatus;
            }
        }

        public void Delete(int id)
        {
            var volunteer = GetById(id);
            if (volunteer != null)
            {
                _volunteers.Remove(volunteer);
            }
        }
    }
}
