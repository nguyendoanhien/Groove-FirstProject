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
            builder.HasOne(a => a.UserInfoEntity).WithOne(b => b.ApplicationUser).HasForeignKey<UserInfoEntity>(b => b.Id);
        }
    }
}
