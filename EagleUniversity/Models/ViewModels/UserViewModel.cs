﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models.ViewModels
{
    public class UserViewModel
    {

        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get { return LastName + " " + FirstName; } }
        public DateTime RegistrationTime { get; set; }
        public string Role
            {
            get
                {
                var role = UserViewModel.userToRole(Id)?.Name ?? "Not Assigned";
                return role;
                }
            }
        //public string AuthUserRole { get; set; }
        public Course course
            {
            get
            {
                var course = new Course()
                {
                    Id = Assignments.userToCourse(Id)?.Course.Id ?? 0
                    ,
                    CourseName = Assignments.userToCourse(Id)?.Course.CourseName ?? "Not Assigned"
                };
                return course;
                }
            }

        public static IdentityRole userToRole(string userId)
        {
            string xxx = "";
            ApplicationDbContext _db = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(_db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var user = userManager.FindById(userId);
            foreach (var item in user.Roles)
            {
                xxx = item.RoleId;
            }            

            var role = (from r in _db.Roles where r.Id.Contains(xxx) select r).FirstOrDefault();

            return role;
        }

    }
}