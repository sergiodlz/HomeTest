using System.ComponentModel.DataAnnotations;

namespace HomeTest.Data.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }
    }
}