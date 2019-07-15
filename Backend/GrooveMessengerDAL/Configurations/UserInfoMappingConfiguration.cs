using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Configurations
{
    public class UserInfoMappingConfiguration : IEntityTypeConfiguration<UserInfoEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserInfoEntity> builder)
        {
            builder.ToTable("UserInfo");
            builder.Property(x => x.Mood).HasColumnName("Mood").HasMaxLength(150);
            builder.Property(x => x.Status).HasColumnName("Status").IsRequired();
            builder.HasOne(a => a.ApplicationUser).WithOne(b => b.UserInfoEntity).HasForeignKey<UserInfoEntity>(b => b.UserId);
         
            builder.HasKey(x => x.Id);
        }
    }
}
