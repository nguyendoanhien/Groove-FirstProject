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
            builder.Property(x => x.DisplayName).HasColumnName("Display Name").IsRequired().HasMaxLength(120);
            builder.Property(x => x.Mood).HasColumnName("Mood").HasMaxLength(150);
            builder.Property(x => x.Status).HasColumnName("Status").IsRequired();

            builder.HasKey(x => x.Id);
           
        }
    }
}
