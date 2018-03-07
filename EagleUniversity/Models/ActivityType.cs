using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models
{
    public class ActivityType
    {
        public int Id { get; set; }
        public string ActivityTypeName { get; set; }
        //nav prop
        public virtual ICollection<Activity> Activity { get; set; }
    }
}