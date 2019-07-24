using GrooveMessengerDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Configurations
{
    public class BaseConfiguration<TBaseEntity> : IEntityTypeConfiguration<TBaseEntity> where TBaseEntity: BaseEntity<Guid>

    {
        public virtual void Configure(EntityTypeBuilder<TBaseEntity> builder)
        {
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
        }
    }

}
