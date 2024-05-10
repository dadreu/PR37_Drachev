using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop_Drachev.Data.Interfaces;
using Shop_Drachev.Data.ViewModell;
using Shop_Drachev.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Shop_Drachev.Controllers
{

    public class ItemsController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;

        private IItems IAllItems;
        private ICategorys IAllCategorys;
        VMItems VMItems = new VMItems();

        public ItemsController(IItems IAllItems, ICategorys IAllCategorys, IHostingEnvironment environment)
        {
            this.IAllItems = IAllItems;
            this.IAllCategorys = IAllCategorys;
            this.hostingEnvironment = environment;
        }

        public ViewResult List(int id = 0)
        {
            ViewBag.Title = "Страница с предметами";
            VMItems.Items = IAllItems.AllItems;
            VMItems.Categorys = IAllCategorys.AllCategorys;
            VMItems.SelectCategory = id;
            return View(VMItems);
        }
        [HttpGet]
        public ViewResult Add()
        {
            IEnumerable<Categorys> Categorys = IAllCategorys.AllCategorys;
            return View(Categorys);
        }

        [HttpPost]
        public RedirectResult Add(string name, string description, IFormFile files, float price, int idCategory)
        {
            if (files != null)
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "img");
                var filePath = Path.Combine(uploads, files.FileName);
                files.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            Items newItems = new Items();
            newItems.Name = name;
            newItems.Description = description;
            newItems.Img = files.FileName;
            newItems.Price = Convert.ToInt32(price);
            newItems.Category = new Categorys() { Id = idCategory };
            int id = IAllItems.Add(newItems);
            return Redirect("/Items/Update?id=" + id);
        }
        [HttpPost]
        public IActionResult Delete(int itemId)
        {
            IAllItems.Delete(itemId);
            return RedirectToAction("List", "Items");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = IAllCategorys.AllCategorys;
            var editItem = IAllItems.AllItems.FirstOrDefault(i => i.Id == id);
            if (editItem == null) return NotFound();
            return View(editItem);
        }

        [HttpPost]
        public IActionResult Edit(Items item, IFormFile imageFile, int idCategory)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "img");
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(uploads, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }
                    item.Img = fileName;
                }
                IAllItems.Update(item, idCategory);
                return RedirectToAction("List", "Items");
            }
            return View(item);
        }
        public ActionResult Basket(int idItem = -1)
        {
            if (idItem != -1) Startup.BasketItem.Add(new ItemsBasket(1, IAllItems.AllItems.Where(x => x.Id == idItem).First()));
            return Json(Startup.BasketItem);
        }

    }
}

