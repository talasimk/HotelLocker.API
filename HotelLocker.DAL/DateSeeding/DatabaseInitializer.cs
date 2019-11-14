using HotelLocker.DAL.EF;
using HotelLocker.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelLocker.DAL.DateSeeding
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var context = provider.GetRequiredService<HotelContext>();
                var userManager = provider.GetRequiredService<UserManager<User>>();
                var roleManager = provider.GetRequiredService<RoleManager<IdentityRole<int>>>();

                await InitializeRolesAsync(userManager, roleManager);
            }
        }

        private static async Task InitializeRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string adminFirstName = "admin";
            string adminLastName = "admin";
            string password = "_Aa123456";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("user"));
            }
            if (await roleManager.FindByNameAsync("hotel-admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("hotel-admin"));
            }
            if (await roleManager.FindByNameAsync("hotel-staff") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("hotel-staff"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User {
                    Email = adminEmail,
                    UserName = adminEmail,
                    EmailConfirmed = true
                };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
            string userEmail = "user@gmail.com";
            string userFirstName = "user";
            string userLastName = "user";
            if (await userManager.FindByNameAsync(userEmail) == null)
            {
                User user = new Guest
                {
                    Email = userEmail,
                    UserName = userEmail,
                    FirstName = userFirstName,
                    LastName = userLastName,
                    EmailConfirmed = true
                };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                }
            }
            string hotelEmail = "hotel@gmail.com";
            string hotelFirstName = "hotel";
            string hotelLastName = "hotel";
            if (await userManager.FindByNameAsync(hotelEmail) == null)
            {
                User hotel = new HotelAdmin
                {
                    Email = hotelEmail,
                    UserName = hotelEmail,
                    EmailConfirmed = true
                };
                IdentityResult result = await userManager.CreateAsync(hotel, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(hotel, "hotel-admin");
                }
            }
            string staffEmail = "staff@gmail.com";
            string staffFirstName = "staff";
            string staffLastName = "staff";
            if (await userManager.FindByNameAsync(staffEmail) == null)
            {
                User staff = new HotelStaff
                {
                    Email = staffEmail,
                    UserName = staffEmail,
                    FirstName = staffFirstName,
                    LastName = staffLastName,
                    EmailConfirmed = true
                };
                IdentityResult result = await userManager.CreateAsync(staff, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(staff, "hotel-staff");
                }
            }

        }
    }
}
