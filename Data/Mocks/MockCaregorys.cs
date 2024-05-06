using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop_Drachev.Data.Models;
using Shop_Drachev.Data.Interfaces;

namespace Shop_Drachev.Data.Mocks
{
    public class MockCaregorys : ICategorys
    {
        public IEnumerable<Categorys> AllCategorys
        {
            get
            {
                return new List<Categorys>
                {
                    new Categorys()
                    {
                        Id = 0,
                        Name = "Микроволновые печи",
                        Description = "Крутые микроволновки"
                    },
                    new Categorys()
                    {
                        Id = 1,
                        Name = "Мультиварки",
                        Description = "Крутые мультиварки"
                    }
                };
            }
        }
    }
}
