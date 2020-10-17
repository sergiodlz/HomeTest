using Microsoft.EntityFrameworkCore;

namespace HomeTest.Data.Configurations.Core
{
    public interface IEntityTypeMap
    {
        void Map(ModelBuilder builder);
    }
}