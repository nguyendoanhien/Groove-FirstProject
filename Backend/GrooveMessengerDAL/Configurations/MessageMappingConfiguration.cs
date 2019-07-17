using GrooveMessengerDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Configurations
{

    public class MessageMappingConfiguration : IEntityTypeConfiguration<MessageEntity>
    {
        public void Configure(EntityTypeBuilder<MessageEntity> builder)
        {
            builder.ToTable("Message");
            //builder.HasOne(x => x.).WithMany(x => x.MessageEntity).HasForeignKey(x => x.Id);
            builder.Property(x => x.SenderId).IsRequired();
            builder.Property(x => x.ConversationId).IsRequired();
            builder.Property(x => x.Content).HasColumnName("Content").IsRequired().HasMaxLength(1000);
            builder.Property(x => x.Type).HasColumnName("Type").IsRequired();

        }
    }
}
