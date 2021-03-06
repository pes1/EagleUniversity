﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models
{
    public class Assignments
    {
        [Key]
        [Column(Order = 2)]
        public int CourseId { get; set; }

        [Key]
        [Column(Order =1)]
        public string ApplicationUserId { get; set; }

        public DateTime AssignDate { get; set; }
        public string OwnerId { get; set; }
        //Nav Prop
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Course Course { get; set; }


        //public UserEntity assignedEntity { get; set; }
        //Users Views
        public static Assignments userToCourse(string userId)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var assignments = (from r in _db.Assignments where r.ApplicationUserId.Contains(userId) select r).FirstOrDefault();

            return assignments;
        }
    }
}