﻿using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Autor { get; set; }
        public string Description { get; set; }
        public DateTime YearOfPublish { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public int NumberOfPages { get; set; }

    }
}
