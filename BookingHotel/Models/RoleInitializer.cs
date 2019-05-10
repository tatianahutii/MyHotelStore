using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookingHotel.Models
{
    public class RoleInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context) 
        {
            //context.Hotels.Add(new Hotel { Name = "Винний сад", City = "Львів", Price = 220, Country = "Україна",Image=null });
            //context.Hotels.Add(new Hotel { Name = "Деревінська купіль", City = "Мукачево", Price = 180, Country = "Україна", Image = null });
            //context.Hotels.Add(new Hotel { Name = "Чайка", City = "Львів", Price = 150, Country = "Україна" , Image = null });
            //context.Hotels.Add(new Hotel { Name = "Зірка", City = "Мукачево", Price = 180, Country = "Україна" , Image = null });
            //context.Hotels.Add(new Hotel { Name = "Зозуля", City = "Львів", Price = 150, Country = "Україна" , Image = null });

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);

            // создаем пользователей
            var admin = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com" };
            string password = "admin123";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
            }

            base.Seed(context);
        }
    }
}