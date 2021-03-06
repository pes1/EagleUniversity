﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models
{
    public class DocumentEntity
    {
        public int Id { get; set; }
        public string EntityType { get; set; }
        public string EntityName { get; set; }
        [Display(Name = "Assigned to:")]
        public string EntityFull { get { return EntityType + " " + EntityName; }}
        public string DocumentTypeName { get; set; }
        public int returnId { get; set; }
        public string returnTarget { get; set; }
    }
}