namespace VolunteerManagementSystem.Models
{
    public class Opportunity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Center { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }  
    }
}
