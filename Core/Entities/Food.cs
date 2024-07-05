﻿using Core.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entiteti
{
    public class Food : BaseEntity
    {
        [ForeignKey("Category_ID")]
        public Guid? Category_ID { get; set; }


        public Food_category? Category { get; set; }

        public decimal? Price { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }

}

