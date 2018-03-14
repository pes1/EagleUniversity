using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models.ViewModels
{
    public class ActivityViewModel
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ModuleId { get; set; }
        public int ActivityTypeId { get; set; }
    }
}