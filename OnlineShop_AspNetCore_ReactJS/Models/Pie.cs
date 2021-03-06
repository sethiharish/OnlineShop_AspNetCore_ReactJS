﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Models
{
    public class Pie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public bool InStock { get; set; }

        public bool IsPieOfTheWeek { get; set; }

        public string ImageUrl { get; set; }

        public string ThumbnailImageUrl { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
