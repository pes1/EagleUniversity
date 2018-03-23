using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models.ViewModels
{
    public class ActivityViewModel
    {
        public int Id { get; set; }
        [Required]
        public string ActivityName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        public bool StartDateAm { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public bool EndDateAm { get; set; }
        public int ModuleId { get; set; }
        public int ActivityTypeId { get; set; }
        public commonEntity redirectProperty { get; set; }

    }
}