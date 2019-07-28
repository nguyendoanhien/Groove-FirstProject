using GrooveMessengerDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrooveMessengerDAL.Configurations
{
    internal class UserInfoContactMappingConfiguration : BaseConfiguration<UserInfoContactEntity>
    {
        public override void Configure(EntityTypeBuilder<UserInfoContactEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("UserInfoContact");
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.ContactId).IsRequired();
            builder.Property(x => x.NickName).HasMaxLength(120);
            builder.Property(b => b.Deleted).HasDefaultValueSql("0");
            //builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            //builder.HasKey(bc => new { bc.ContactId, bc.UserId });

            builder.HasOne(uc => uc.UserInfo)
                .WithMany(u => u.Users)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(uc => uc.ContactInfo)
                .WithMany(u => u.Contacts)
                .HasForeignKey(uc => uc.ContactId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(u => u.ContactInfo).WithMany(z => z.Users).HasForeignKey(u => u.Id);
            //builder.HasOne(u => u.ContactInfo).WithMany(z => z.Contacts).HasForeignKey(u => u.ContactId);
        }
    }
}