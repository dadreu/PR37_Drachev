using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop_Drachev.Data.Models;
using Shop_Drachev.Data.Interfaces;

namespace Shop_Drachev.Data.Mocks
{
    public class MockItems : Items
    {
        public ICategorys _category = new MockCategorys();

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
                        Img = "",
                        Price = 3369,
                        Category = _category.AllCategorys.Where(x => x.Id == 0).First() 
                    } 
                }; 
            }
        }
    }
}
