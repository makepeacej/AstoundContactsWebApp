using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AstoundContactsWebApp.Models
{
    public class Contact
    {
        
        public int Id { get; set; }
        [DisplayName("Full Name")]
        public string? Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber)]
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
