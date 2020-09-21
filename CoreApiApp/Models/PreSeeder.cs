using CoreApiApp.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiApp.Models
{
    public static class PreSeeder
    {
        //feeds the database with demo data for testing and staging
        public static async Task Seeder(AppDbContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            //ensure that a database exists 
            context.Database.EnsureCreated();

            //if no roles exist in the database
            if (!roleManager.Roles.Any())
            {
                //create list of roles
                var allRoles = new List<IdentityRole>
                {
                    new IdentityRole("User")
                };

                //add role(s) to the database
                foreach (var role in allRoles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            //if no users exist in the database
            if (!userManager.Users.Any())
            {
                //create list of users
                var allUsers = new List<User>
                {
                    new User{FirstName="Segzy", LastName="Adaramaja", Email="seguna@gmail.com", UserName="seguna@gmail.com", PhoneNumber="08095784765"},
                    new User{FirstName="Seun", LastName="Oyetoyan", Email="seuno@gmail.com", UserName="seuno@gmail.com", PhoneNumber="07057893783"},
                    new User{FirstName="Micheal", LastName="Nwosu", Email="miken@gmail.com", UserName="miken@gmail.com", PhoneNumber="08036754890"}
                };

                //add role(s) to the database
                foreach (var user in allUsers)
                {
                    var result = await userManager.CreateAsync(user, "Engin&&rkufr&1");

                    //if role successfully added
                    if (result.Succeeded)
                    {
                        //add user role
                        await userManager.AddToRoleAsync(user, "User");
                    }
                }
            }
        }
    }
}
