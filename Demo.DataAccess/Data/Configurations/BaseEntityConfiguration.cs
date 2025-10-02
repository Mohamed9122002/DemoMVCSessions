using Demo.DataAccess.Models;

namespace Demo.DataAccess.Data.Configurations
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GetDate()");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GetDate()");
        }
    }
}
