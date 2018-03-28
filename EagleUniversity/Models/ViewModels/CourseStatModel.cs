using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models.ViewModels
{
    public class CourseStatModel
    {
        public int students { get; set; }
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }       
        public DateTime DueDate { get; set; }
        public int Deadline { get { return ((DueDate)- DateTime.Now).Days; } }
        public string EntityName { get; set; }
    }
}