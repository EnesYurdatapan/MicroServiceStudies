﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Ms.Services.EmailAPI.Models.Dtos
{
    public class CartDetailsDto
    {
        public int Id { get; set; }
        public int CartHeaderId { get; set; }
        public CartHeaderDto? CartHeader { get; set; }
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
    }
}
