using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // seed the rolls user, admin , superAdmin

            var adminRoleId = "de1a009d-df37-477e-b2fb-4a3d1d9ecd6b";
            var userId = "728ea3b0-884e-45a6-9257-820a77d3142f";

            var superAdminRoleId = "7a2ed168-15d3-4cd2-8507-d2e414e26cc9\r\n";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },

                new IdentityRole
                {
                    Name = "superAdmin",
                    NormalizedName = "superAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp =superAdminRoleId
                },

                new IdentityRole
                {
                    Name = "user",
                    NormalizedName = "user",
                    Id = userId,
                    ConcurrencyStamp = userId
                }




            };

            builder.Entity<IdentityRole>().HasData(roles);


            // seed super Admin user
            var superAdminId = "e6644dad-ae72-4c6a-8cd4-deab739518ff";

            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                Id = superAdminId,


            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "SuperAdmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);





            // add all roles to super admin user

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                  new IdentityUserRole<string>
                {
                    RoleId = userId ,
                    UserId = superAdminId
                },
                    new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId ,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

        }
    }
}
