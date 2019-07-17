using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrooveMessengerDAL.Configurations
{

    public class ApplicationUserMappingConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany<MessageEntity>(x => x.MessageEntity).WithOne(b => b.ApplicationUser).HasForeignKey(x => x.SenderId);
            builder.HasMany<ParticipantEntity>(x => x.ParticipantEntity).WithOne(b => b.ApplicationUser).HasForeignKey(x => x.UserId);
        }
    }
}
