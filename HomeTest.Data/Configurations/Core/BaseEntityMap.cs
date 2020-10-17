using HomeTest.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeTest.Data.Configurations.Core
{
    public abstract class BaseEntityMap<TEntityType> : IEntityTypeMap
        where TEntityType : BaseEntity
    {
        private readonly string TableName;
        private readonly string IdName;

        public BaseEntityMap(string TableName, string IdName)
        {
            this.TableName = TableName;
            this.IdName = IdName;
        }

        public void Map(ModelBuilder builder)
        {
            #region EntityBase Configuration

            builder.Entity<TEntityType>()
                .ToTable(TableName);

            builder.Entity<TEntityType>()
                .HasKey(x => x.Id);

            builder.Entity<TEntityType>()
                .Property(x => x.Id)
                .HasColumnName(IdName)
                .HasDefaultValueSql("NEWID()")
                .IsRequired();

            builder.Entity<TEntityType>()
                .Property(x => x.Enable)
                .IsRequired();

            #endregion EntityBase Configuration

            InternalMap(builder.Entity<TEntityType>());
        }

        protected abstract void InternalMap(EntityTypeBuilder<TEntityType> builder);
    }
}