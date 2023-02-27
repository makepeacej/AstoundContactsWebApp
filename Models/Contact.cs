using System.ComponentModel;

namespace AstoundContactsWebApp.Models
{
    public class Contact
    {
        
        public int Id { get; set; }
        [DisplayName("Full Name")]
        public string? Name { get; set; }
        
        public string? Email { get; set; }
        [DisplayName("Phone Number")]
        public string? Phone { get; set; }
        [DisplayName("Tech Number")]
        public int TechNum { get; set; }
        [DisplayName("Job Category")]
        public Jobs? JobCategory { get; set; }
        //Manager, Install/Service, Network Tech, Sales, Office, Misc
    }

    public enum Jobs
    {
        Manager,
        InstallOrService,
        NetworkTech,
        Sales,
        Office,
        Misc
    }
}
