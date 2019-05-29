namespace WebApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApp.Persistence.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApp.Persistence.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Controller"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Controller" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "AppUser"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "AppUser" };

                manager.Create(role);
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(u => u.UserName == "admin@yahoo.com"))
            {
                var user = new ApplicationUser() { Id = "admin", UserName = "admin@yahoo.com", Email = "admin@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Admin123!") };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "appu@yahoo.com"))
            { 
                var user = new ApplicationUser() { Id = "appu", UserName = "appu@yahoo.com", Email = "appu@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Appu123!") };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "AppUser");
            }

            //  DayType
            if (!context.DayType.Any(d => d.Name == "Radni dan"))
            {
                DayType dayType = new DayType() { Name = "Radni dan" };
                context.DayType.Add(dayType);
                context.SaveChanges();
            }

            if (!context.DayType.Any(d => d.Name == "Subota"))
            {
                DayType dayType = new DayType() { Name = "Subota" };
                context.DayType.Add(dayType);
                context.SaveChanges();
            }

            if (!context.DayType.Any(d => d.Name == "Nedelja"))
            {
                DayType dayType = new DayType() { Name = "Nedelja" };
                context.DayType.Add(dayType);
                context.SaveChanges();
            }

            // TicketType
            if (!context.TicketType.Any(t => t.Name == "Vremenska karta"))
            {
                TicketType ticketType = new TicketType() { Name = "Vremenska karta" };
                context.TicketType.Add(ticketType);
                context.SaveChanges();
            }

            if (!context.TicketType.Any(t => t.Name == "Dnevna karta"))
            {
                TicketType ticketType = new TicketType() { Name = "Dnevna karta" };
                context.TicketType.Add(ticketType);
                context.SaveChanges();
            }

            if (!context.TicketType.Any(t => t.Name == "Mesečna karta"))
            {
                TicketType ticketType = new TicketType() { Name = "Mesečna karta" };
                context.TicketType.Add(ticketType);
                context.SaveChanges();
            }

            if (!context.TicketType.Any(t => t.Name == "Godišnja karta"))
            {
                TicketType ticketType = new TicketType() { Name = "Godišnja karta" };
                context.TicketType.Add(ticketType);
                context.SaveChanges();
            }

            //  UserType
            if (!context.UserType.Any(u => u.Name == "Đak"))
            {
                UserType userType = new UserType() { Name = "Đak" };
                context.UserType.Add(userType);
                context.SaveChanges();
            }

            if (!context.UserType.Any(u => u.Name == "Penzioner"))
            {
                UserType userType = new UserType() { Name = "Penzioner" };
                context.UserType.Add(userType);
                context.SaveChanges();
            }

            if (!context.UserType.Any(u => u.Name == "Regularan"))
            {
                UserType userType = new UserType() { Name = "Regularan" };
                context.UserType.Add(userType);
                context.SaveChanges();
            }

            //  TimetableTzpe
            if (!context.TimetableType.Any(t => t.Name == "Gradski"))
            {
                TimetableType timetableType = new TimetableType() { Name = "Gradski" };
                context.TimetableType.Add(timetableType);
                context.SaveChanges();
            }

            if (!context.TimetableType.Any(t => t.Name == "Prigradski"))
            {
                TimetableType timetableType = new TimetableType() { Name = "Prigradski" };
                context.TimetableType.Add(timetableType);
                context.SaveChanges();
            }
        }
    }
}
