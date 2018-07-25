using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Models
{
    public class InventoryDataContext : DbContext
    {
        public InventoryDataContext(DbContextOptions<InventoryDataContext> options)
            : base(options)
        {}

        public DbSet<Item> Items { get; set; }
        public DbSet<Chemical> Chemicals { get; set; }
    }
}
