using GrooveMessengerDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Configurations
{
    public class ParticipantMappingConfiguration : IEntityTypeConfiguration<ParticipantEntity>
    {
        public void Configure(EntityTypeBuilder<ParticipantEntity> builder)
        {
            builder.ToTable("Participant");

            builder.Property(x => x.Status).HasColumnName("Status").IsRequired();
            //builder.HasOne(x => x.UserId).WithMany(x => x.ParticipantEntity).HasForeignKey(x => x.UserId);

        }


    }
}
