using Shop_Drachev.Data.Models;
using System.Collections.Generic;


namespace Shop_Drachev.Data.Interfaces
{
    public interface IItems
    {
        public IEnumerable<Items> AllItems { get; }

        public int Add(Items Item);
        public void Delete(int id);
        public void Update(Items Item, int categId);

    }
}
