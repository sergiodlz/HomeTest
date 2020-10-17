﻿namespace HomeTest.Data.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }
    }
}