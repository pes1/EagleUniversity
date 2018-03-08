using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models
{
    public class CourseDocument
    {
        [Key]
        [Column(Order = 2)]
        public int CourseId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int DocumentId { get; set; }

        public DateTime AssignDate { get; set; }
        public string OwnerId { get; set; }
        //Nav Prop
        public virtual Document AssignedDocument { get; set; }
        public virtual Course AssignedCourse { get; set; }
    }
}