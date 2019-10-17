using System;
namespace ResilITApp.Model
{
    public class RegisterModel
    {
        public string code { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public int vereniging { get; set; }
        public bool bus { get; set; }
        public string programme { get; set; }
        public string programmeOther { get; set; }
        public string companyName { get; set; }
        public bool vegetarian { get; set; }
        public string specialNeeds { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string confirm { get; set; }
        public bool privacyPolicyAgree { get; set; }
        public bool subscribe { get; set; }
    }
}
