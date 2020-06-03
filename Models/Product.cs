using System;

namespace InstrumentsShopAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }
    }
}
