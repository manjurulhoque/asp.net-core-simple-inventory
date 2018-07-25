﻿namespace InventoryManagement.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Unit { get; set; }
        public string Image { get; set; }
        public string SupplierName { get; set; }
    }
}
