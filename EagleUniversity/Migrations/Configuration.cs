namespace EagleUniversity.Migrations
{
    using EagleUniversity.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EagleUniversity.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EagleUniversity.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Courses.AddOrUpdate(
              c => c.CourseName,
            new Course { CourseName = ".NET VT18", StartDate = DateTime.Parse("2018-03-01"), EndDate = DateTime.Parse("2018-09-30") },
            new Course { CourseName = "OFFICE 365 VT18", StartDate = DateTime.Parse("2018-06-01"), EndDate = DateTime.Parse("2018-12-31") },
            new Course { CourseName = "JAVA HT18", StartDate = DateTime.Parse("2018-09-01"), EndDate = DateTime.Parse("2019-03-31") },
            new Course { CourseName = ".NET HT18", StartDate = DateTime.Parse("2018-10-01"), EndDate = DateTime.Parse("2018-04-30") },
            new Course { CourseName = "OFFICE 365 HT18", StartDate = DateTime.Parse("2018-11-01"), EndDate = DateTime.Parse("2019-05-31") },
            new Course { CourseName = "JAVA VT19", StartDate = DateTime.Parse("2018-02-01"), EndDate = DateTime.Parse("2019-08-31") }
            );
            context.SaveChanges();

            var courseDot = context.Courses.Where(r => r.CourseName.Contains(".NET VT18")).SingleOrDefault();
            var courseOff = context.Courses.Where(r => r.CourseName.Contains("OFFICE 365 VT18")).SingleOrDefault();
            var courseJav = context.Courses.Where(r => r.CourseName.Contains("JAVA HT18")).SingleOrDefault();

            context.Modules.AddOrUpdate(
              c => c.ModuleName,
            new Module { ModuleName = ".NET C#", StartDate = DateTime.Parse("2018-03-01"), EndDate = DateTime.Parse("2018-03-31"), CourseId = courseDot.Id },
            new Module { ModuleName = ".NET WEBB", StartDate = DateTime.Parse("2018-04-01"), EndDate = DateTime.Parse("2018-04-30"), CourseId = courseDot.Id },
            new Module { ModuleName = ".NET MVC P1", StartDate = DateTime.Parse("2018-05-01"), EndDate = DateTime.Parse("2018-05-31"), CourseId = courseDot.Id },
            new Module { ModuleName = ".NET Database", StartDate = DateTime.Parse("2018-06-01"), EndDate = DateTime.Parse("2018-06-30"), CourseId = courseDot.Id },
            new Module { ModuleName = ".NET MVC P2", StartDate = DateTime.Parse("2018-07-01"), EndDate = DateTime.Parse("2018-09-30"), CourseId = courseDot.Id },

            new Module { ModuleName = "O365 MOD 1", StartDate = DateTime.Parse("2018-06-01"), EndDate = DateTime.Parse("2018-06-30"), CourseId = courseOff.Id },
            new Module { ModuleName = "O365 MOD 2", StartDate = DateTime.Parse("2018-07-01"), EndDate = DateTime.Parse("2018-07-31"), CourseId = courseOff.Id },
            new Module { ModuleName = "O365 MOD 3", StartDate = DateTime.Parse("2018-08-01"), EndDate = DateTime.Parse("2018-08-31"), CourseId = courseOff.Id },
            new Module { ModuleName = "O365 MOD 4", StartDate = DateTime.Parse("2018-09-01"), EndDate = DateTime.Parse("2018-09-30"), CourseId = courseOff.Id },
            new Module { ModuleName = "O365 MOD 5", StartDate = DateTime.Parse("2018-10-01"), EndDate = DateTime.Parse("2018-12-31"), CourseId = courseOff.Id },

            new Module { ModuleName = "JAVA MOD 1", StartDate = DateTime.Parse("2018-09-01"), EndDate = DateTime.Parse("2018-09-30"), CourseId = courseJav.Id },
            new Module { ModuleName = "JAVA MOD 2", StartDate = DateTime.Parse("2018-10-01"), EndDate = DateTime.Parse("2018-10-31"), CourseId = courseJav.Id },
            new Module { ModuleName = "JAVA MOD 3", StartDate = DateTime.Parse("2018-11-01"), EndDate = DateTime.Parse("2018-11-30"), CourseId = courseJav.Id },
            new Module { ModuleName = "JAVA MOD 4", StartDate = DateTime.Parse("2018-12-01"), EndDate = DateTime.Parse("2018-12-31"), CourseId = courseJav.Id },
            new Module { ModuleName = "JAVA MOD 5", StartDate = DateTime.Parse("2019-01-01"), EndDate = DateTime.Parse("2019-03-31"), CourseId = courseJav.Id }
            );
            context.SaveChanges();

            context.ActivityTypes.AddOrUpdate(
                c => c.ActivityTypeName,
                new ActivityType { ActivityTypeName = "E-Learning" },
                new ActivityType { ActivityTypeName = "Seminar" },
                new ActivityType { ActivityTypeName = "Exercise" }
                );
            context.SaveChanges();

            var moduleMod1 = context.Modules.Where(m => m.ModuleName.Contains(".NET C#")).SingleOrDefault();
            var moduleMod2 = context.Modules.Where(m => m.ModuleName.Contains(".NET WEBB")).SingleOrDefault();
            var moduleMod3 = context.Modules.Where(m => m.ModuleName.Contains(".NET MVC P1")).SingleOrDefault();
            var moduleMod4 = context.Modules.Where(m => m.ModuleName.Contains(".NET Database")).SingleOrDefault();
            var moduleMod5 = context.Modules.Where(m => m.ModuleName.Contains(".NET MVC P2")).SingleOrDefault();

            var moduleMod6 = context.Modules.Where(m => m.ModuleName.Contains("O365 MOD 1")).SingleOrDefault();
            var moduleMod7 = context.Modules.Where(m => m.ModuleName.Contains("O365 MOD 2")).SingleOrDefault();
            var moduleMod8 = context.Modules.Where(m => m.ModuleName.Contains("O365 MOD 3")).SingleOrDefault();
            var moduleMod9 = context.Modules.Where(m => m.ModuleName.Contains("O365 MOD 4")).SingleOrDefault();
            var moduleMod10 = context.Modules.Where(m => m.ModuleName.Contains("O365 MOD 5")).SingleOrDefault();

            var moduleMod11 = context.Modules.Where(m => m.ModuleName.Contains("JAVA MOD 1")).SingleOrDefault();
            var moduleMod12 = context.Modules.Where(m => m.ModuleName.Contains("JAVA MOD 2")).SingleOrDefault();
            var moduleMod13 = context.Modules.Where(m => m.ModuleName.Contains("JAVA MOD 3")).SingleOrDefault();
            var moduleMod14 = context.Modules.Where(m => m.ModuleName.Contains("JAVA MOD 4")).SingleOrDefault();
            var moduleMod15 = context.Modules.Where(m => m.ModuleName.Contains("JAVA MOD 5")).SingleOrDefault();

            var activityTypeElearning = context.ActivityTypes.Where(m => m.ActivityTypeName.Contains("E-Learning")).SingleOrDefault();
            var activityTypeSeminar = context.ActivityTypes.Where(m => m.ActivityTypeName.Contains("Seminar")).SingleOrDefault();
            var activityTypeExercise = context.ActivityTypes.Where(m => m.ActivityTypeName.Contains("Exercise")).SingleOrDefault();

            context.Activities.AddOrUpdate(
              c => c.ActivityName,
            new Activity { ActivityName = ".NET C# ACT 1", StartDate = DateTime.Parse("2018-03-01"), EndDate = DateTime.Parse("2018-03-10"), ModuleId = moduleMod1.Id, ActivityTypeId = activityTypeElearning.Id },
            new Activity { ActivityName = ".NET C# ACT 2", StartDate = DateTime.Parse("2018-03-11"), EndDate = DateTime.Parse("2018-03-20"), ModuleId = moduleMod1.Id, ActivityTypeId = activityTypeSeminar.Id },
            new Activity { ActivityName = ".NET C# ACT 3", StartDate = DateTime.Parse("2018-03-21"), EndDate = DateTime.Parse("2018-03-31"), ModuleId = moduleMod1.Id, ActivityTypeId = activityTypeExercise.Id },

            new Activity { ActivityName = ".NET WEBB ACT 1", StartDate = DateTime.Parse("2018-04-01"), EndDate = DateTime.Parse("2018-04-10"), ModuleId = moduleMod2.Id, ActivityTypeId = activityTypeElearning.Id },
            new Activity { ActivityName = ".NET WEBB ACT 2", StartDate = DateTime.Parse("2018-04-11"), EndDate = DateTime.Parse("2018-04-20"), ModuleId = moduleMod2.Id, ActivityTypeId = activityTypeSeminar.Id },
            new Activity { ActivityName = ".NET WEBB ACT 3", StartDate = DateTime.Parse("2018-04-21"), EndDate = DateTime.Parse("2018-04-30"), ModuleId = moduleMod2.Id, ActivityTypeId = activityTypeExercise.Id },

            new Activity { ActivityName = ".NET MVC P1 ACT 1", StartDate = DateTime.Parse("2018-05-01"), EndDate = DateTime.Parse("2018-05-10"), ModuleId = moduleMod3.Id, ActivityTypeId = activityTypeElearning.Id },
            new Activity { ActivityName = ".NET MVC P1 ACT 2", StartDate = DateTime.Parse("2018-05-11"), EndDate = DateTime.Parse("2018-05-20"), ModuleId = moduleMod3.Id, ActivityTypeId = activityTypeSeminar.Id },
            new Activity { ActivityName = ".NET MVC P1 ACT 3", StartDate = DateTime.Parse("2018-05-21"), EndDate = DateTime.Parse("2018-05-31"), ModuleId = moduleMod3.Id, ActivityTypeId = activityTypeExercise.Id },

            new Activity { ActivityName = ".NET Database ACT 1", StartDate = DateTime.Parse("2018-06-01"), EndDate = DateTime.Parse("2018-06-10"), ModuleId = moduleMod4.Id, ActivityTypeId = activityTypeElearning.Id },
            new Activity { ActivityName = ".NET Database ACT 2", StartDate = DateTime.Parse("2018-06-11"), EndDate = DateTime.Parse("2018-06-20"), ModuleId = moduleMod4.Id, ActivityTypeId = activityTypeSeminar.Id },
            new Activity { ActivityName = ".NET Database ACT 3", StartDate = DateTime.Parse("2018-06-21"), EndDate = DateTime.Parse("2018-06-30"), ModuleId = moduleMod4.Id, ActivityTypeId = activityTypeExercise.Id },

            new Activity { ActivityName = ".NET MVC P2 ACT 1", StartDate = DateTime.Parse("2018-07-01"), EndDate = DateTime.Parse("2018-07-31"), ModuleId = moduleMod5.Id, ActivityTypeId = activityTypeElearning.Id },
            new Activity { ActivityName = ".NET MVC P2 ACT 2", StartDate = DateTime.Parse("2018-08-01"), EndDate = DateTime.Parse("2018-08-31"), ModuleId = moduleMod5.Id, ActivityTypeId = activityTypeSeminar.Id },
            new Activity { ActivityName = ".NET MVC P2 ACT 3", StartDate = DateTime.Parse("2018-09-01"), EndDate = DateTime.Parse("2018-09-30"), ModuleId = moduleMod5.Id, ActivityTypeId = activityTypeExercise.Id },

            new Activity { ActivityName = "O365 MOD 1 ACT 1", StartDate = DateTime.Parse("2018-06-01"), EndDate = DateTime.Parse("2018-06-10"), ModuleId = moduleMod6.Id, ActivityTypeId = activityTypeElearning.Id },
            new Activity { ActivityName = "O365 MOD 1 ACT 2", StartDate = DateTime.Parse("2018-06-11"), EndDate = DateTime.Parse("2018-06-20"), ModuleId = moduleMod6.Id, ActivityTypeId = activityTypeSeminar.Id },
            new Activity { ActivityName = "O365 MOD 1 ACT 3", StartDate = DateTime.Parse("2018-06-21"), EndDate = DateTime.Parse("2018-06-30"), ModuleId = moduleMod6.Id, ActivityTypeId = activityTypeExercise.Id },

            new Activity { ActivityName = "O365 MOD 2 ACT 1", StartDate = DateTime.Parse("2018-07-01"), EndDate = DateTime.Parse("2018-07-10"), ModuleId = moduleMod7.Id, ActivityTypeId = activityTypeElearning.Id },
            new Activity { ActivityName = "O365 MOD 2 ACT 2", StartDate = DateTime.Parse("2018-07-11"), EndDate = DateTime.Parse("2018-07-20"), ModuleId = moduleMod7.Id, ActivityTypeId = activityTypeSeminar.Id },
            new Activity { ActivityName = "O365 MOD 2 ACT 3", StartDate = DateTime.Parse("2018-07-21"), EndDate = DateTime.Parse("2018-07-31"), ModuleId = moduleMod7.Id, ActivityTypeId = activityTypeExercise.Id },

            new Activity { ActivityName = "O365 MOD 3 ACT 1", StartDate = DateTime.Parse("2018-08-01"), EndDate = DateTime.Parse("2018-08-10"), ModuleId = moduleMod8.Id, ActivityTypeId = activityTypeElearning.Id },
            new Activity { ActivityName = "O365 MOD 3 ACT 2", StartDate = DateTime.Parse("2018-08-11"), EndDate = DateTime.Parse("2018-08-20"), ModuleId = moduleMod8.Id, ActivityTypeId = activityTypeSeminar.Id },
            new Activity { ActivityName = "O365 MOD 3 ACT 3", StartDate = DateTime.Parse("2018-08-21"), EndDate = DateTime.Parse("2018-08-31"), ModuleId = moduleMod8.Id, ActivityTypeId = activityTypeExercise.Id },

            new Activity { ActivityName = "O365 MOD 4 ACT 1", StartDate = DateTime.Parse("2018-09-01"), EndDate = DateTime.Parse("2018-09-10"), ModuleId = moduleMod9.Id, ActivityTypeId = activityTypeElearning.Id },
            new Activity { ActivityName = "O365 MOD 4 ACT 2", StartDate = DateTime.Parse("2018-09-11"), EndDate = DateTime.Parse("2018-09-20"), ModuleId = moduleMod9.Id, ActivityTypeId = activityTypeSeminar.Id },
            new Activity { ActivityName = "O365 MOD 4 ACT 3", StartDate = DateTime.Parse("2018-09-21"), EndDate = DateTime.Parse("2018-09-30"), ModuleId = moduleMod9.Id, ActivityTypeId = activityTypeExercise.Id },

            new Activity { ActivityName = "O365 MOD 5 ACT 1", StartDate = DateTime.Parse("2018-10-01"), EndDate = DateTime.Parse("2018-10-31"), ModuleId = moduleMod10.Id, ActivityTypeId = activityTypeElearning.Id },
            new Activity { ActivityName = "O365 MOD 5 ACT 2", StartDate = DateTime.Parse("2018-11-01"), EndDate = DateTime.Parse("2018-11-30"), ModuleId = moduleMod10.Id, ActivityTypeId = activityTypeSeminar.Id },
            new Activity { ActivityName = "O365 MOD 5 ACT 3", StartDate = DateTime.Parse("2018-12-01"), EndDate = DateTime.Parse("2018-12-31"), ModuleId = moduleMod10.Id, ActivityTypeId = activityTypeExercise.Id },

            new Activity { ActivityName = "JAVA MOD 1 ACT 1", StartDate = DateTime.Parse("2018-09-01"), EndDate = DateTime.Parse("2018-09-10"), ModuleId = moduleMod11.Id, ActivityTypeId = activityTypeElearning.Id },
            new Activity { ActivityName = "JAVA MOD 1 ACT 2", StartDate = DateTime.Parse("2018-09-11"), EndDate = DateTime.Parse("2018-09-20"), ModuleId = moduleMod11.Id, ActivityTypeId = activityTypeSeminar.Id },
            new Activity { ActivityName = "JAVA MOD 1 ACT 3", StartDate = DateTime.Parse("2018-09-21"), EndDate = DateTime.Parse("2018-09-30"), ModuleId = moduleMod11.Id, ActivityTypeId = activityTypeExercise.Id },

            new Activity { ActivityName = "JAVA MOD 2 ACT 1", StartDate = DateTime.Parse("2018-10-01"), EndDate = DateTime.Parse("2018-10-10"), ModuleId = moduleMod12.Id, ActivityTypeId = activityTypeElearning.Id },
            new Activity { ActivityName = "JAVA MOD 2 ACT 2", StartDate = DateTime.Parse("2018-10-11"), EndDate = DateTime.Parse("2018-10-20"), ModuleId = moduleMod12.Id, ActivityTypeId = activityTypeSeminar.Id },
            new Activity { ActivityName = "JAVA MOD 2 ACT 3", StartDate = DateTime.Parse("2018-10-21"), EndDate = DateTime.Parse("2018-10-31"), ModuleId = moduleMod12.Id, ActivityTypeId = activityTypeExercise.Id },

            new Activity { ActivityName = "JAVA MOD 3 ACT 1", StartDate = DateTime.Parse("2018-11-01"), EndDate = DateTime.Parse("2018-11-10"), ModuleId = moduleMod13.Id, ActivityTypeId = activityTypeElearning.Id },
            new Activity { ActivityName = "JAVA MOD 3 ACT 2", StartDate = DateTime.Parse("2018-11-11"), EndDate = DateTime.Parse("2018-11-20"), ModuleId = moduleMod13.Id, ActivityTypeId = activityTypeSeminar.Id },
            new Activity { ActivityName = "JAVA MOD 3 ACT 3", StartDate = DateTime.Parse("2018-11-21"), EndDate = DateTime.Parse("2018-11-30"), ModuleId = moduleMod13.Id, ActivityTypeId = activityTypeExercise.Id },

            new Activity { ActivityName = "JAVA MOD 4 ACT 1", StartDate = DateTime.Parse("2018-12-01"), EndDate = DateTime.Parse("2018-12-10"), ModuleId = moduleMod14.Id, ActivityTypeId = activityTypeElearning.Id },
            new Activity { ActivityName = "JAVA MOD 4 ACT 2", StartDate = DateTime.Parse("2018-12-11"), EndDate = DateTime.Parse("2018-12-20"), ModuleId = moduleMod14.Id, ActivityTypeId = activityTypeSeminar.Id },
            new Activity { ActivityName = "JAVA MOD 4 ACT 3", StartDate = DateTime.Parse("2018-12-21"), EndDate = DateTime.Parse("2018-12-31"), ModuleId = moduleMod14.Id, ActivityTypeId = activityTypeExercise.Id },

            new Activity { ActivityName = "JAVA MOD 5 ACT 1", StartDate = DateTime.Parse("2019-01-01"), EndDate = DateTime.Parse("2019-01-31"), ModuleId = moduleMod15.Id, ActivityTypeId = activityTypeElearning.Id },
            new Activity { ActivityName = "JAVA MOD 5 ACT 2", StartDate = DateTime.Parse("2019-02-01"), EndDate = DateTime.Parse("2019-02-28"), ModuleId = moduleMod15.Id, ActivityTypeId = activityTypeSeminar.Id },
            new Activity { ActivityName = "JAVA MOD 5 ACT 3", StartDate = DateTime.Parse("2019-03-01"), EndDate = DateTime.Parse("2019-03-31"), ModuleId = moduleMod15.Id, ActivityTypeId = activityTypeExercise.Id }
            );

            context.SaveChanges();    

            context.DocumentTypes.AddOrUpdate(
                c => c.DocumentTypeName,
                    new DocumentType { DocumentTypeName= "Description" },
                    new DocumentType { DocumentTypeName = "Task" },
                    new DocumentType { DocumentTypeName = "Report" }
                    );

            var rolestore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(rolestore);

            var roleNames = new[] { "Admin", "Student", "Teacher" };
            
            foreach (var roleName in roleNames)
            {
                if (!context.Roles.Any(r => r.Name == roleName))
                {
                    var role = new IdentityRole { Name = roleName };
                    var result = roleManager.Create(role);
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                }
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var emails = new[] { "admin@eagle.com", "teacher@eagle.com", "student@eagle.com", "evorn.martensson@eagle.com",
                "pavel.bespalov@eagle.com", "dany.kassdaood@eagle.com", "pereinar.stromme@eagle.com",
                "laura.brannigan@eagle.com", "madonna.louise.ciccone@eagle.com", "maria.magdalena@eagle.com",
                "alan.shepard@eagle.com", "virgil.grissom@eagle.com", "john.glenn@eagle.com", "scott.carpenter@eagle.com",
                "walter.schirra@eagle.com", "gordon.cooper@eagle.com", "deke.slayton@eagle.com" };
            
            foreach (var email in emails)
            {
                if (!context.Users.Any(r => r.UserName == email))
                {
                    var lastName = "";
                    var firstName = "";
                    if (email == "admin@eagle.com")
                    {
                        lastName = "admin@eagle.com";
                    }
                    else if (email == "teacher@eagle.com")
                    {
                        lastName = "teacher@eagle.com";
                    }
                    else if (email == "student@eagle.com")
                    {
                        lastName = "student@eagle.com";
                    }
                    else if (email == "evorn.martensson@eagle.com")
                    {
                        firstName = "Evorn";
                        lastName = "Mårtensson";
                    }
                    else if (email == "pavel.bespalov@eagle.com")
                    {
                        firstName = "Pavel";
                        lastName = "Bespalov";
                    }
                    else if (email == "dany.kassdaood@eagle.com")
                    {
                        firstName = "Dany Kass";
                        lastName = "Daood";
                    }
                    else if (email == "pereinar.stromme@eagle.com")
                    {
                        firstName = "Per Einar";
                        lastName = "Strömme";
                    }
                    else if (email == "laura.brannigan@eagle.com")
                    {
                        firstName = "Laura";
                        lastName = "Brannigan";
                    }
                    else if (email == "madonna.louise.ciccone@eagle.com")
                    {
                        firstName = "Madonna Louise";
                        lastName = "Ciccone";
                    }
                    else if (email == "maria.magdalena@eagle.com")
                    {
                        firstName = "Maria";
                        lastName = "Magdalena";
                    }
                    else if (email == "alan.shepard@eagle.com")
                    {
                        firstName = "Alan";
                        lastName = "Shepard";
                    }
                    else if (email == "virgil.grissom@eagle.com")
                    {
                        firstName = "Virgil";
                        lastName = "Grissom";
                    }
                    else if (email == "john.glenn@eagle.com")
                    {
                        firstName = "John";
                        lastName = "Glenn";
                    }
                    else if (email == "scott.carpenter@eagle.com")
                    {
                        firstName = "Scott";
                        lastName = "Carpenter";
                    }
                    else if (email == "walter.schirra@eagle.com")
                    {
                        firstName = "Walter";
                        lastName = "Schirra";
                    }
                    else if (email == "gordon.cooper@eagle.com")
                    {
                        firstName = "Gordon";
                        lastName = "Cooper";
                    }
                    else if (email == "deke.slayton@eagle.com")
                    {
                        firstName = "Deke";
                        lastName = "Slayton";
                    }


                    var user = new ApplicationUser { UserName = email, Email = email, FirstName = firstName, LastName = lastName, RegistrationTime=DateTime.Now};
                    var result = userManager.Create(user, "Password12345");
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                }
            }
            //Add to role
            foreach (var item in context.Users.ToList())
            {
                if (item.UserName == "admin@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Admin");
                }
                else if (item.UserName == "teacher@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Teacher");
                }
                else if (item.UserName == "student@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Student");
                }
                else if (item.UserName == "evorn.martensson@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Student");
                }
                else if (item.UserName == "pavel.bespalov@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Student");
                }
                else if (item.UserName == "dany.kassdaood@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Student");
                }
                else if (item.UserName == "pereinar.stromme@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Student");
                }
                else if (item.UserName == "laura.brannigan@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Student");
                }
                else if (item.UserName == "madonna.louise.ciccone@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Student");
                }
                else if (item.UserName == "maria.magdalena@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Student");
                }
                else if (item.UserName == "alan.shepard@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Student");
                }
                else if (item.UserName == "virgil.grissom@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Student");
                }
                else if (item.UserName == "john.glenn@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Student");
                }
                else if (item.UserName == "scott.carpenter@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Student");
                }
                else if (item.UserName == "walter.schirra@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Student");
                }
                else if (item.UserName == "gordon.cooper@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Student");
                }
                else if (item.UserName == "deke.slayton@eagle.com")
                {
                    userManager.AddToRole(item.Id, "Student");
                }
            }
        }
    }
}
