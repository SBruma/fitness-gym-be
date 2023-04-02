using FitnessGym.Domain.Entities.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessGym.Domain.Configurations.Members
{
    public class MembershipConfiguration : AuditableEntityConfiguration<Membership>
    {
        const string INTERVAL_CONSTRAINT_NAME = "CK_Expiration_Interval";
        const string INTERVAL_CONSTRAINT = "(\"RenewalDate\" < \"ExpirationDate\")";

        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            base.Configure(builder);

            builder.ToTable("Memberships");
            builder.ToTable(t => t.HasCheckConstraint(INTERVAL_CONSTRAINT_NAME, INTERVAL_CONSTRAINT));

            builder.HasKey(membership => membership.Id);
            builder.HasIndex(membership => membership.Id)
                .IsUnique();

            builder.Property(membership => membership.Id)
                .HasConversion(membershipId => membershipId.Value,
                                value => new MembershipId(value));

            builder.Property(membership => membership.QRCode)
                .HasConversion(membershipId => membershipId.Value,
                                value => new QRCode(value));

            builder.HasOne(membership => membership.Member)
                .WithMany(member => member.Memberships)
                .HasForeignKey(membership => membership.MemberId);

            builder.HasOne(membership => membership.Gym)
                .WithMany(gym => gym.Memberships)
                .HasForeignKey(membership => membership.GymId);

            //builder.Property(membership => membership.QRCode).IsRequired();
            builder.Property(membership => membership.RenewalDate).IsRequired();
            builder.Property(membership => membership.ExpirationDate).IsRequired();

        }
    }
}
