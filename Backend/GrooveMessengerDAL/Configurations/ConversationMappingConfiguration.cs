using GrooveMessengerDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrooveMessengerDAL.Configurations
{
    internal class ConversationMappingConfiguration : BaseConfiguration<ConversationEntity>
    {
        public override void Configure(EntityTypeBuilder<ConversationEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("Conversation");
            builder.Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
            builder.Property(x => x.Avatar).HasColumnName("Avatar").IsRequired().HasMaxLength(1000);

            builder.HasMany(x => x.MessageEntity).WithOne(b => b.ConversationEntity)
                .HasForeignKey(x => x.ConversationId);
            ;
            builder.HasMany(x => x.ParticipantEntity).WithOne(b => b.ConversationEntity)
                .HasForeignKey(x => x.ConversationId);
        }
    }
}