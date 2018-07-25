using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class ChemicalController : Controller
    {
        private readonly InventoryDataContext _context;

        public ChemicalController(InventoryDataContext context)
        {
            _context = context;
        }
        [Route("chemicals")]
        public IActionResult Index()
        {
            var chemicals = _context.Chemicals.ToList();
            return View(chemicals);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Name", "Description", "Quantity", "Unit", "SupplierName")]Chemical chemical)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            chemical.CreatedAt = DateTime.Now;
            chemical.UpdatedAt = DateTime.Now;
            _context.Chemicals.Add(chemical);
            _context.SaveChanges();
            return View();
        }
    }
}