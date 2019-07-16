using GrooveMessengerDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Configurations
{
    class ConversationMappingConfiguration : IEntityTypeConfiguration<ConversationEntity>
    {

        public void Configure(EntityTypeBuilder<ConversationEntity> builder)
        {
            builder.ToTable("Conversation");
            builder.Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
            builder.Property(x => x.Avatar).HasColumnName("Avatar").IsRequired().HasMaxLength(50);

            builder.HasMany<MessageEntity>(x => x.MessageEntity).WithOne(b => b.ConversationEntity).HasForeignKey(x => x.ConversationId); ;
            builder.HasMany<ParticipantEntity>(x => x.ParticipantEntity).WithOne(b => b.ConversationEntity).HasForeignKey(x => x.ConversationId);
        }
    }
}
