using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models.ViewModels
{
    public class UserViewModel
    {

        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get { return LastName + " " + FirstName; } }
        public DateTime RegistrationTime { get; set; }
        public string Role { get; set; }
        public string AuthUserRole { get; set; }
    }
}