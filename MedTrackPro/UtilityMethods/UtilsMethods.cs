using Microsoft.AspNetCore.Identity;


namespace MedTrackPro.UtilityMethods
{
    public static class CommonMethods
    {
        public static string PatientRole = "patient";
        public static string DoctorRole = "doctor";
        public static string AdminRole = "admin";
        public static string EmployeeRole = "employee";
    }
    public class UtilsMethods
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UtilsMethods(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task CreateRolesAndAdminAsync()
        {
            
            var email = "MuhammadUbaid@admin.com";
            var password = "User100+";

            bool isRolesAdded = await AddRolesAsync();
            if (isRolesAdded)
            {
                Console.WriteLine("Roles Added successfully");
            }

            if(await AddAdminAsync(email, password) == true)
            {
                Console.WriteLine("Admin User Added successfully");
            }

        }

        public async Task<bool> AddRolesAsync()
        {
            string[] roles = { "admin", "patient", "doctor", "employee" };
            foreach (var role in roles)
            {
                if (await _roleManager.RoleExistsAsync(role) == false)
                {
                    var identityRole = new IdentityRole(role);
                    await _roleManager.CreateAsync(identityRole);
                }
            }
            return true;
        }

        public async Task<bool> AddAdminAsync(string email, string password)
        {
            var adminUser = new IdentityUser
            {
                Email = email,
                UserName = email
            };
            if (await _userManager.FindByEmailAsync(email) == null)
            {
                var result = await _userManager.CreateAsync(adminUser, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "admin");
                }
            }
            return true;
        }
    }
}
