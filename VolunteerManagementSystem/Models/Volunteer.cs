using System.ComponentModel.DataAnnotations;

namespace VolunteerManagementSystem.Models
{
    public class Volunteer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Centers { get; set; }
        public string Skills { get; set; }
        public string Availability { get; set; }
        public string Address { get; set; }
        public string PhoneNumbers { get; set; }
        public string Email { get; set; }
        public string EducationalBackground { get; set; }
        public string Licenses { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string EmergencyContactEmail { get; set; }
        public string EmergencyContactAddress { get; set; }
        public bool DriverLicenseOnFile { get; set; }
        public bool SocialSecurityCardOnFile { get; set; }
        public string ApprovalStatus { get; set; }
    }

}
