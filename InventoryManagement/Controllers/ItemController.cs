using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class ItemController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly InventoryDataContext _context;

        public ItemController(IHostingEnvironment environment, InventoryDataContext context)
        {
            _context = context;
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }
        [Route("items")]
        public IActionResult Index()
        {
            var items = _context.Items.ToList();
            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Name", "Description", "Quantity", "Unit", "SupplierName")]Item item, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (image != null)
            {
                var filename = Path.Combine(_environment.WebRootPath, Path.GetFileName(image.FileName));
                image.CopyTo(new FileStream(filename, FileMode.Create));
                item.Image = image.FileName;
            }
            _context.Items.Add(item);
            _context.SaveChanges();
            return View();
        }
        [Route("items/show/{id}")]
        public IActionResult Show(int id)
        {
            var item = _context.Items.SingleOrDefault(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        public IActionResult Update(Item i, IFormFile image)
        {
            var item = _context.Items.Find(i.Id);
            if (item == null)
            {
                return NotFound();
            }

            item.Description = i.Description;
            item.Name = i.Name;
            item.Quantity = i.Quantity;
            item.SupplierName = i.SupplierName;
            if (image != null)
            {
                string fullPath = Path.Combine(_environment.WebRootPath, Path.GetFileName(item.Image));
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                var filename = Path.Combine(_environment.WebRootPath, Path.GetFileName(image.FileName));
                image.CopyTo(new FileStream(filename, FileMode.Create));
                item.Image = image.FileName;
            }
            _context.Items.Update(item);
            _context.SaveChanges();
            return Redirect("/Item");
        }
    }
}