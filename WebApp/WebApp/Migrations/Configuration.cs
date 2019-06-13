﻿namespace WebApp.Migrations
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
            AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
		}

        protected override void Seed(WebApp.Persistence.ApplicationDbContext context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.

			//  DayType
			if (!context.DayType.Any(d => d.Name == "Radni dan"))
			{
				DayType dayType = new DayType() { Name = "Radni dan", Id=1 };
				context.DayType.Add(dayType);
				context.SaveChanges();
			}

			if (!context.DayType.Any(d => d.Name == "Subota"))
			{
				DayType dayType = new DayType() { Name = "Subota", Id =2 };
				context.DayType.Add(dayType);
				context.SaveChanges();
			}

			if (!context.DayType.Any(d => d.Name == "Nedelja"))
			{
				DayType dayType = new DayType() { Name = "Nedelja", Id =3 };
				context.DayType.Add(dayType);
				context.SaveChanges();
			}

			// TicketType
			if (!context.TicketType.Any(t => t.Name == "Vremenska karta"))
			{
				TicketType ticketType = new TicketType() { Name = "Vremenska karta", Id = 1};
				context.TicketType.Add(ticketType);
				context.SaveChanges();
			}

			if (!context.TicketType.Any(t => t.Name == "Dnevna karta"))
			{
				TicketType ticketType = new TicketType() { Name = "Dnevna karta" , Id =2};
				context.TicketType.Add(ticketType);
				context.SaveChanges();
			}

			if (!context.TicketType.Any(t => t.Name == "Mesečna karta"))
			{
				TicketType ticketType = new TicketType() { Name = "Mesečna karta" , Id =3};
				context.TicketType.Add(ticketType);
				context.SaveChanges();
			}

			if (!context.TicketType.Any(t => t.Name == "Godišnja karta"))
			{
				TicketType ticketType = new TicketType() { Name = "Godišnja karta" , Id =4};
				context.TicketType.Add(ticketType);
				context.SaveChanges();
			}

			//  UserType
			if (!context.UserType.Any(u => u.Name == "Đak"))
			{
				UserType userType = new UserType() { Name = "Đak", Id =1 };
				context.UserType.Add(userType);
				context.SaveChanges();
			}

			if (!context.UserType.Any(u => u.Name == "Penzioner"))
			{
				UserType userType = new UserType() { Name = "Penzioner", Id =2 };
				context.UserType.Add(userType);
				context.SaveChanges();
			}

			if (!context.UserType.Any(u => u.Name == "Regularan"))
			{
				UserType userType = new UserType() { Name = "Regularan", Id =3 };
				context.UserType.Add(userType);
				context.SaveChanges();
			}

			//  TimetableType
			if (!context.TimetableType.Any(t => t.Name == "Gradski"))
			{
				TimetableType timetableType = new TimetableType() { Name = "Gradski", Id =1 };
				context.TimetableType.Add(timetableType);
				context.SaveChanges();
			}

			if (!context.TimetableType.Any(t => t.Name == "Prigradski"))
			{
				TimetableType timetableType = new TimetableType() { Name = "Prigradski", Id =2 };
				context.TimetableType.Add(timetableType);
				context.SaveChanges();
			}

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
                var user = new ApplicationUser() { Id = "admin", UserName = "admin@yahoo.com", Email = "admin@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Admin123!"), TypeId=2 };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "controller@yahoo.com"))
            { 
                var user = new ApplicationUser() { Id = "controller", UserName = "controller@yahoo.com", Email = "controller@yahoo.comm", PasswordHash = ApplicationUser.HashPassword("Controller123!"), TypeId=2 };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Controller");
            }

            if (!context.Users.Any(u => u.UserName == "appu@yahoo.com"))
            {
                var user = new ApplicationUser() { Id = "appu", UserName = "appu@yahoo.com", Email = "appu@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Appu123!"), TypeId = 2 };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "AppUser");
            }

            // Pricelist
            if (!context.Pricelist.Any(t => t.Id == 1))
            {
                Pricelist pricelist = new Pricelist() { Id = 1, From = DateTime.Now.ToString(), To = DateTime.Now.ToString() };
                context.Pricelist.Add(pricelist);
                context.SaveChanges();
            }

            // Stations
            if (!context.Station.Any(t => t.Name == "Prva"))
            {
                Station station = new Station() { Id = 1, Name = "Prva stanica", Address = "Adresa Prve Stanice", X = 22, Y = 35 };
                context.Station.Add(station);
                context.SaveChanges();
            }

            if (!context.Station.Any(t => t.Name == "Druga"))
            {
                Station station = new Station() { Id = 1, Name = "Druga stanica", Address = "Adresa Druge Stanice", X = 22, Y = 35 };
                context.Station.Add(station);
                context.SaveChanges();
            }

            //  line
            if (!context.Line.Any(t => t.Id == 1))
            {
                Line line = new Line() { Id = 1, SerialNumber = 1 };
                context.Line.Add(line);
                context.SaveChanges();
            }

            if (!context.Line.Any(t => t.Id == 2))
            {
                Line line = new Line() { Id = 2, SerialNumber = 2 };
                context.Line.Add(line);
                context.SaveChanges();
            }

            // Timetable

            if (!context.Timetable.Any(t => t.Id == 1))
            {
                Timetable timetable = new Timetable() { Id = 1,LineId=1, TimetableTypeId = 1,DayTypeId = 1,Times="9:50 10:50 11:50" };
                context.Timetable.Add(timetable);
                context.SaveChanges();
            }

            if (!context.Timetable.Any(t => t.Id == 2))
            {
                Timetable timetable = new Timetable() { Id = 2, LineId = 1, TimetableTypeId = 2, DayTypeId = 2 , Times = "9:50 10:50 11:50 12:50 13:50" };
                context.Timetable.Add(timetable);
                context.SaveChanges();
            }


            //  ticketPrice
            if (!context.TicketPrice.Any(t => t.Id == 1))
            {
                TicketPrice ticketPrice = new TicketPrice() {  Price = 50, PricelistId = 1, TicketTypeId = 1};
                context.TicketPrice.Add(ticketPrice);
                context.SaveChanges();
            }

            if (!context.TicketPrice.Any(t => t.Id == 2))
            {
                TicketPrice ticketPrice = new TicketPrice() { Price = 150, PricelistId = 1, TicketTypeId = 2 };
                context.TicketPrice.Add(ticketPrice);
                context.SaveChanges();
            }

            if (!context.TicketPrice.Any(t => t.Id == 3))
            {
                TicketPrice ticketPrice = new TicketPrice() { Price = 1500, PricelistId = 1, TicketTypeId = 3 };
                context.TicketPrice.Add(ticketPrice);
                context.SaveChanges();
            }

            if (!context.TicketPrice.Any(t => t.Id == 4))
            {
                TicketPrice ticketPrice = new TicketPrice() { Price = 5000, PricelistId = 1, TicketTypeId = 4 };
                context.TicketPrice.Add(ticketPrice);
                context.SaveChanges();
            }
        }
    }
}
