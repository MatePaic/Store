﻿namespace Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public required ProductItemOrdered ItemOrdered { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public required string Type { get; set; }
        public required string Brand { get; set; }
    }
}
