using Shop_Drachev.Data.Models;
using System.Collections.Generic;

namespace Shop_Drachev.Data.ViewModell
{
    public class VMItems
    {
        public IEnumerable<Models.Items> Items { get; set; }
        public IEnumerable<Models.Categorys> Categorys { get; set; }
        public int SelectCategory = 0;
    }
}
