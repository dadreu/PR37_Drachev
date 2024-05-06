using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop_Drachev.Data.Interfaces;

namespace Shop_Drachev.Controllers
{
    public class ItemsController
    {
        public class ItemsController : Controller
        {
            private IItems IAllItems;
            private ICategorys IAllCategorys;

            public ItemsController(IItems IAllItems, ICategorys IAllCategorys)
            {
                this.IAllItems = IAllItems;
                this.IAllCategorys = IAllCategorys;
            }

            public ViewResult List()
            {
                ViewBag.Title = "Страница с предметами";
                var cars = IAllItems.AllItems;
                return View(cars);
            }
        }
    }
}
