using GrooveMessengerDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrooveMessengerDAL.Configurations
{
    public class UserInfoMappingConfiguration : BaseConfiguration<UserInfoEntity>
    {
        public override void Configure(EntityTypeBuilder<UserInfoEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("UserInfo");
            builder.Property(x => x.DisplayName).HasColumnName("DisplayName").IsRequired().HasMaxLength(120);
            builder.Property(x => x.Mood).HasColumnName("Mood").HasMaxLength(150);
            builder.Property(x => x.Status).HasColumnName("Status").IsRequired();
            builder.Property(x => x.DisplayName).HasColumnName("DisplayName").IsRequired().HasMaxLength(150);
            builder.HasOne(a => a.ApplicationUser).WithOne(b => b.UserInfoEntity)
                .HasForeignKey<UserInfoEntity>(b => b.UserId);
            builder.HasKey(x => x.Id);
        }
    }
}