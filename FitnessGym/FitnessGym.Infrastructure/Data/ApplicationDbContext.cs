using FitnessGym.Domain.Configurations.Gyms;
using FitnessGym.Domain.Configurations.Members;
using FitnessGym.Domain.Entities.Enums;
using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Entities.Identity;
using FitnessGym.Domain.Entities.Members;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresExtension("uuid-ossp");
            ConfigureEntities(builder);
            SeedRoles(builder);
            SeedStaff(builder);
            SeedStaffRoles(builder);
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        private void ConfigureEntities(ModelBuilder builder)
        {
            new ApplicationUserConfiguration().Configure(builder.Entity<ApplicationUser>());
            new StaffBookingConfiguration().Configure(builder.Entity<StaffBooking>());
            new StaffScheduleConfiguration().Configure(builder.Entity<StaffSchedule>());
            new GymConfiguration().Configure(builder.Entity<Gym>());
            new MembershipConfiguration().Configure(builder.Entity<Membership>());
            new FloorConfiguration().Configure(builder.Entity<Floor>());
            new EquipmentConfiguration().Configure(builder.Entity<Equipment>());
            new MaintenanceHistoryConfiguration().Configure(builder.Entity<MaintenanceHistory>());
            new GymCheckInConfiguration().Configure(builder.Entity<GymCheckIn>());
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>
                {
                    Id = new Guid("1F1E61A8-B164-43A9-8238-2902727852CE"),
                    Name = Domain.Entities.Statics.Roles.Member,
                    NormalizedName = Domain.Entities.Statics.Roles.Member.ToUpper()
                },
                new IdentityRole<Guid>
                {
                    Id = new Guid("FF98EC1B-2FFE-4A66-8436-9BAA85329F2C"),
                    Name = Domain.Entities.Statics.Roles.Manager,
                    NormalizedName = Domain.Entities.Statics.Roles.Manager.ToUpper()
                },
                new IdentityRole<Guid>
                {
                    Id = new Guid("43EC62F7-5AAE-4ED8-BFEF-C2DAA8E2E419"),
                    Name = Domain.Entities.Statics.Roles.Trainer,
                    NormalizedName = Domain.Entities.Statics.Roles.Trainer.ToUpper()
                },
                new IdentityRole<Guid>
                {
                    Id = new Guid("ADFC9C57-2D88-4ABD-9F8E-87774A6EB3FA"),
                    Name = Domain.Entities.Statics.Roles.Technician,
                    NormalizedName = Domain.Entities.Statics.Roles.Technician.ToUpper()
                },
                new IdentityRole<Guid>
                {
                    Id = new Guid("D5AFFFB8-B3BA-453B-A85D-FE736F59060D"),
                    Name = Domain.Entities.Statics.Roles.Receptionist,
                    NormalizedName = Domain.Entities.Statics.Roles.Receptionist.ToUpper()
                }
            );
        }

        private void SeedStaff(ModelBuilder builder)
        {
            var managerEmail = "manager@gym.director.com";
            var receptionistEmail = "receptionist@gym.director.com";
            var technicianEmail = "technician@gym.director.com";
            var trainerEmail = "trainer@gym.director.com";

            var manager = new ApplicationUser
            {
                Id = new Guid("8E721037-C9FC-4CA0-80DA-B414F5B72D36"),
                UserName = managerEmail,
                NormalizedUserName = managerEmail.ToUpper(),
                FirstName = "Jim",
                LastName = "Cool",
                Email = managerEmail,
                DateOfBirth = new DateOnly(1990, 05, 20),
                Gender = Gender.Male,
                EmailConfirmed = true
            };

            var trainer = new ApplicationUser
            {
                Id = new Guid("59BDBC09-57B2-426D-AAE9-DA830E0382A0"),
                UserName = trainerEmail,
                NormalizedUserName = trainerEmail.ToUpper(),
                FirstName = "Costel",
                LastName = "Bimius",
                Email = trainerEmail,
                DateOfBirth = new DateOnly(1995, 08, 15),
                Gender = Gender.Male,
                EmailConfirmed = true,
            };

            var tehnician = new ApplicationUser
            {
                Id = new Guid("34C33D5B-E131-4724-95EC-D97F0F4A494B"),
                UserName = technicianEmail,
                NormalizedUserName = technicianEmail.ToUpper(),
                FirstName = "John",
                LastName = "Geri",
                Email = technicianEmail,
                DateOfBirth = new DateOnly(1993, 02, 10),
                Gender = Gender.Male,
                EmailConfirmed = true,
            };

            var receptionist = new ApplicationUser
            {
                Id = new Guid("8EA4FB46-51BC-415D-9EC0-013683D29411"),
                UserName = receptionistEmail,
                NormalizedUserName = receptionistEmail.ToUpper(),
                FirstName = "Miriam",
                LastName = "Tuiar",
                Email = receptionistEmail,
                DateOfBirth = new DateOnly(2000, 08, 6),
                Gender = Gender.Female,
                EmailConfirmed = true,
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            // TODO: REMOVE
            string password = "_String123";

            foreach (var user in new List<ApplicationUser> { manager, trainer, tehnician, receptionist })
            {
                user.PasswordHash = passwordHasher.HashPassword(user, password);
                user.SecurityStamp = Guid.NewGuid().ToString();
            }

            builder.Entity<ApplicationUser>().HasData(manager, trainer, tehnician, receptionist);
        }

        private void SeedStaffRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<Guid>>().HasData(
                   // Manager
                   new IdentityUserRole<Guid>
                   {
                       RoleId = new Guid("FF98EC1B-2FFE-4A66-8436-9BAA85329F2C"),
                       UserId = new Guid("8E721037-C9FC-4CA0-80DA-B414F5B72D36")
                   },
                   // Trainer
                   new IdentityUserRole<Guid>
                   {
                       RoleId = new Guid("43EC62F7-5AAE-4ED8-BFEF-C2DAA8E2E419"),
                       UserId = new Guid("59BDBC09-57B2-426D-AAE9-DA830E0382A0")
                   },
                   // Technician
                   new IdentityUserRole<Guid>
                   {
                       RoleId = new Guid("43EC62F7-5AAE-4ED8-BFEF-C2DAA8E2E419"),
                       UserId = new Guid("34C33D5B-E131-4724-95EC-D97F0F4A494B")
                   },
                   // Receptionist
                   new IdentityUserRole<Guid>
                   {
                       RoleId = new Guid("D5AFFFB8-B3BA-453B-A85D-FE736F59060D"),
                       UserId = new Guid("8EA4FB46-51BC-415D-9EC0-013683D29411")
                   });
        }
    }
}
