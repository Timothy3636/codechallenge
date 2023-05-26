using Api.Models;

namespace Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if the database has already been seeded
            if (context.Users.Any())
            {
                return; // Database has been seeded
            }

            // Seed initial data
            SeedUsers(context);
            // SeedUserRoles(context);
            // SeedCurrencies(context);
            SeedUserRolesMappings(context);
            SeedUserCashBalances(context);
            
            // Save changes to the database
            context.SaveChanges();
        }

        private static void SeedUsers(ApplicationDbContext context)
        {
            var users = new User[]
            {
                new User { FirstName = "user1", LastName = "User1", Email = "user1@example.com", Password = "password", IsActive = true, RegistrationDate = DateTime.Now },
                new User { FirstName = "user2", LastName = "User2", Email = "user2@example.com", Password = "password", IsActive = true, RegistrationDate = DateTime.Now },
                new User { FirstName = "premiumUser1", LastName = "premiumUser1", Email = "pu1@example.com", Password = "password", IsActive = true, RegistrationDate = DateTime.Now }
            };

            context.Users.AddRange(users);
        }

        // private static void SeedUserRoles(ApplicationDbContext context)
        // {
        //     var roles = new UserRoles[]
        //     {
        //         new UserRoles { RoleName = "Admin" },
        //         new UserRoles { RoleName = "User" },
        //     };

        //     context.UserRoles.AddRange(roles);
        // }

        private static void SeedUserRolesMappings(ApplicationDbContext context)
        {
            var user1 = context.Users.FirstOrDefault(u => u.FirstName == "user1");
            var user2 = context.Users.FirstOrDefault(u => u.FirstName == "user2");
            var admin1 = context.Users.FirstOrDefault(u => u.FirstName == "admin1");

            // var userRole = context.UserRoles.FirstOrDefault(ur => ur.RoleName == "User");
            // var adminRole = context.UserRoles.FirstOrDefault(ur => ur.RoleName == "Admin");

            if (user1 == null || user2 == null || admin1 == null)
            {
                // Handle missing user(s)
                throw new Exception("One or more users are missing for role mapping.");
            }

            // if (userRole == null || adminRole == null)
            // {
            //     // Handle missing role(s)
            //     throw new Exception("One or more roles are missing for user mapping.");
            // }

            // var userRolesMappings = new UserRolesMapping[]
            // {
            //     new UserRolesMapping { User = user1, UserRoles = userRole },
            //     new UserRolesMapping { User = user2, UserRoles = userRole },
            //     new UserRolesMapping { User = admin1, UserRoles = adminRole }
            // };

            // context.UserRoleMappings.AddRange(userRolesMappings);
        }        

        private static void SeedUserCashBalances(ApplicationDbContext context)
        {
            var user1 = context.Users.FirstOrDefault(u => u.FirstName == "user1");
            var user2 = context.Users.FirstOrDefault(u => u.FirstName == "user2");
            var admin1 = context.Users.FirstOrDefault(u => u.FirstName == "admin1");

            // var hkdCurrency = context.Currencies.FirstOrDefault(c => c.CurrencyCode == "HKD");
            // var twdCurrency = context.Currencies.FirstOrDefault(c => c.CurrencyCode == "TWD");
            // var cnyCurrency = context.Currencies.FirstOrDefault(c => c.CurrencyCode == "CNY");

            if (user1 == null || user2 == null || admin1 == null)
            {
                throw new Exception("One or more users are missing for cash balance seeding.");
            }

            var UserCashBalances = new UserCashBalance[]
            {
                new UserCashBalance { User = user1, Currency = "HKD", CashBalance = 10000 },
                new UserCashBalance { User = user1, Currency = "AUD", CashBalance = 50000 },
                new UserCashBalance { User = user2, Currency = "HKD", CashBalance = 7500 },
                new UserCashBalance { User = user2, Currency = "AUD", CashBalance = 12000 },
                new UserCashBalance { User = admin1, Currency = "HKD", CashBalance = 20000 }
            };

            context.UserCashBalance.AddRange(UserCashBalances);
        }

    }

}