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
            new ActivityType { ActivityTypeName = "Forelasningar" }
            );

            context.DocumentTypes.AddOrUpdate(
                c => c.DocumentTypeName,
                    new DocumentType {  DocumentTypeName="Description" },
                    new DocumentType {  DocumentTypeName = "Task" }
                    );

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var emails = new[] { "admin@eagle.com" };
            foreach (var email in emails)
            {
                if (!context.Users.Any(r => r.UserName == email))
                {
                    var user = new ApplicationUser { UserName = email, Email = email };
                    var result = userManager.Create(user, "Admin12345");
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join("\n", result.Errors));
                    }
                }

            }
        }
    }
}
