namespace PCR.Users.Data.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PCR.Users.Data.UMSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PCR.Users.Data.UMSContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

          
            //context.Users.AddOrUpdate(p => p.UserName,
            //    new User
            //  {
            //      EmailAddress = "Onboardingsuperuser@gamil.com",
            //      Password = "superuser",
            //      UserName = "Onboardingsuperuser@gamil.com",
            //      FirstName = "Admin",
            //      LastName = "Super",
            //      IsDelete = false,
            //      LockOutEnabled = false,
            //      CreatedDate = DateTime.Now,
            //      UpdatedDate = DateTime.Now                  
            //  }
            //);
            //context.SaveChanges();

            context.Roles.AddOrUpdate(
              p => p.RoleName,
              new Role { RoleName = "Super Admin", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
              new Role { RoleName = "Company Admin", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
              new Role { RoleName = "Manager", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
              new Role { RoleName = "Employee", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now }
            );
            context.SaveChanges();

            //context.WorkAddress.AddOrUpdate(p =>p.WorkAddressID,
            //    new WorkAddress
            //    {
            //        RoleID = 1,
            //        UserID = 1
               
            //    });
            //context.SaveChanges();

            //context.UserRoles.AddOrUpdate(p => p.UserRoleID,
            //new UserRole
            //{
            //    RoleID = 1,
            //    UserID = 1,
            //    CreatedDate = DateTime.Now,
            //    UpdatedDate = DateTime.Now
            //});
            //context.SaveChanges();

        }
    }
}
