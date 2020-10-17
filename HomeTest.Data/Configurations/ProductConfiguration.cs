using HomeTest.Data.Configurations.Core;
using HomeTest.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeTest.Data.Configurations
{
    public class ProductConfiguration : BaseEntityMap<Product>
    {
        public ProductConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(255);

            builder
                .Property(x => x.Description)
                .IsUnicode()
                .HasMaxLength(1000);

            builder
                .Property(x => x.Quantity)
                .HasDefaultValueSql("0")
                .IsRequired();
        }
    }
}