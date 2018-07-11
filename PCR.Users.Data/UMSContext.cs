using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PCR.Users.Data
{
    public class UMSContext : DbContext
    {
        public UMSContext() : base("PCROnBoardUMS")
        {
            //Database.SetInitializer<UMSContext>(null);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UMSContext, PCR.Users.Data.Migrations.Configuration>("PCROnBoardUMS"));
        }

        public UMSContext(IDbConnection con) : base((DbConnection)con, true)
        {
            Database.SetInitializer<UMSContext>(null);
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<UMSContext, PCR.Users.Data.Migrations.Configuration>(true));
        }

        //public UMSContext(IDbConnection con) : base((DbConnection)con, true)
        //{

        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<WorkAddress> WorkAddress { get; set; }
        public DbSet<LinkedPCRUsers> LinkedPCRUsers { get; set; }

        public DbSet<OnBoardingEmployeeSteps> OnBoardingEmployeeSteps { get; set; }

        public DbSet<I9AcceptableDocumentPackages> I9AcceptableDocumentPackages { get; set; }

        public DbSet<DocumentPackage> DocumentPackages { get; set; }

        public DbSet<Clients> Clients { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
