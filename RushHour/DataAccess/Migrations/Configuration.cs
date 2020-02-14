namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.Repository.RushHourDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataAccess.Repository.RushHourDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            if(!context.Users.Any())
            {
                context.Users.Add(new Entity.User { Name = "Admin", Email = "RushHourManager@gmail.com", Password = "admin-123", IsAdmin = true, IsEmailVerified = true });

            }
           // 
        }
    }
}
