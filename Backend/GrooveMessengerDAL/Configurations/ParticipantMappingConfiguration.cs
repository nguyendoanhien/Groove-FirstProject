using GrooveMessengerDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Configurations
{
    public class ParticipantMappingConfiguration : BaseConfiguration<ParticipantEntity>
    {
        public override void Configure(EntityTypeBuilder<ParticipantEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("Participant");

            builder.Property(x => x.Status).HasColumnName("Status").IsRequired();
        }


    }
}
