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
            new Course { CourseName = "C#", StartDate=DateTime.Now, EndDate=DateTime.Now },
            new Course { CourseName = ".Net", StartDate = DateTime.Now, EndDate = DateTime.Now }
            );
            context.ActivityTypes.AddOrUpdate(
            c => c.ActivityTypeName,
            new ActivityType { ActivityTypeName="E-Learning" },
            new ActivityType { ActivityTypeName = "Seminar" },
            new ActivityType { ActivityTypeName = "Excersise" }
            );

            context.DocumentTypes.AddOrUpdate(
                c => c.DocumentTypeName,
                    new DocumentType {  DocumentTypeName="Description" },
                    new DocumentType {  DocumentTypeName = "Task" }
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
            var emails = new[] { "admin@eagle.com" };
            foreach (var email in emails)
            {
                if (!context.Users.Any(r => r.UserName == email))
                {
                    var user = new ApplicationUser { UserName = email, Email = email, LastName = email, RegistrationTime = DateTime.Now };
                    var result = userManager.Create(user, "Admin12345");
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                }

            }
            //Add to roles
            var adminUser = userManager.FindByName("admin@eagle.com");
            var adminRole = roleManager.FindByName("Admin");


            //userManager.AddToRole(adminUser.Id, adminRole.Name);
            //userManager.AddToRole(adminUser.Id, "Teacher");
            //if (!adminUser.Roles.Any(r => r.RoleId == adminRole.Id))
            //{

            //    userManager.AddToRole(adminUser.Id, adminRole.Name);
            //}




        }
    }
}
