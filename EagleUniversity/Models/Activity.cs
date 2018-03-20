using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models
{
    //public enum Epass { FM = 1, EM, FMEM };

    public class Activity
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        //public Epass Pass { get; set; }
        public int ModuleId { get; set; }
        public int ActivityTypeId { get; set; }

        //Nav Prop
        public virtual ActivityType ActivityTypes { get; set; }
        public virtual Module Modules { get; set; }

        public virtual ICollection<ActivityDocument> DocumentActivityAssignments { get; set; }
    }


}