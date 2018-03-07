﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int ActivityId { get; set; }
        //Nav Prop
        public virtual Activity Activity { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}