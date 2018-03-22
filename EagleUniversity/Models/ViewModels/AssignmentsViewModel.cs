using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models.ViewModels
{
    public class AssignmentsViewModel
    {

        public int CourseId { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime AssignDate { get; set; }
        public string OwnerId { get; set; }
        public UserEntity assignedPropety { get; set; }
    }
}