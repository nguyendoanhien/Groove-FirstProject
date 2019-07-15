using GrooveMessengerDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Configurations
{
    class UserInfoContactMappingConfiguration : IEntityTypeConfiguration<UserInfoContactEntity>
    {
        public void Configure(EntityTypeBuilder<UserInfoContactEntity> builder)
        {
            builder.ToTable("UserInfoContact");
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.ContactId).IsRequired();
            builder.Property(x => x.NickName).HasMaxLength(120);
            builder.HasKey(x => x.Id);

            builder.HasOne(uc => uc.UserInfo)
                .WithMany(u => u.Users)
                .HasForeignKey(uc => uc.Id);
            builder.HasOne(uc => uc.ContactInfo)
                .WithMany(u => u.Contacts)
                .HasForeignKey(uc => uc.Id);
        }
    }
}
