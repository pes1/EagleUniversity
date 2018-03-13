using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models
{
    public class Course
    {
            public int Id { get; set; }
            public string CourseName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string OwnerId { get; set; }
            //Nav Prop
            public virtual ICollection<Module> Modules { get; set; }
            public virtual ICollection<Assignments> UserCourseAssignments { get; set; }
            public virtual ICollection<CourseDocument> DocumentCourseAssignments { get; set; }
    }
}