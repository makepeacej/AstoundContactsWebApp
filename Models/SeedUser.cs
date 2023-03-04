using Microsoft.AspNetCore.Identity;

namespace AstoundContactsWebApp.Models
{
    public static class SeedUser
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (UserManager<IdentityUser> _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>())
            {

                var userlist = new List<SeedUserModel>()
            {
                new SeedUserModel(){ UserName="admin@gmail.com",Password= "Password1234!" }
            };


                foreach (var user in userlist)
                {
                    if (!_userManager.Users.Any(r => r.UserName == user.UserName))
                    {
                        var newuser = new IdentityUser { UserName = user.UserName, Email = user.UserName };
                        var result = await _userManager.CreateAsync(newuser, user.Password);
                    }
                }

            }
        }
    }

    internal class SeedUserModel
    {

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
