using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop_Drachev.Data.Models;
using Shop_Drachev.Data.Interfaces;
using Shop_Drachev.Data.Mocks;

namespace Shop_Drachev.Data.Mocks
{
    public class MockItems : IItems
    {
        public ICategorys _category = new MockCaregorys();

        public IEnumerable<Items> AllItems
        {
            get
            {
                return new List<Items>()
                {
                    new Items()
                    {
                        Id = 0,
                        Name = "DEXP MS-70",
                        Description = "Черный корпус",
                        Img = "https://c.dns-shop.ru/thumb/st1/fit/300/300/f67706d3e9f41fdc37fbf1d4ea715b35/b1a761fddbd2197e22bdcf5ee0cd1cfd773ce824ab6ef6eba7411b9a626c50a7.jpg.webp",
                        Price = 3369,
                        Category = _category.AllCategorys.Where(x => x.Id == 0).First() 
                    } 
                }; 
            }
        }
    }
}
