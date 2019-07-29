using GrooveMessengerDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrooveMessengerDAL.Configurations
{
    internal class NoteMappingConfiguration : IEntityTypeConfiguration<NoteEntity>
    {
        public void Configure(EntityTypeBuilder<NoteEntity> builder)
        {
            builder.ToTable("Notes");
            builder.Property(x => x.Title).HasColumnName("Title").IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).HasColumnName("Description").HasMaxLength(255);
        }
    }
}