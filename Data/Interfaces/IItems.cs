using Shop_Drachev.Data.Models;
using System.Collections.Generic;


namespace Shop_Drachev.Data.Interfaces
{
    public interface IItems
    {
        public IEnumerable<IItems> AllItems { get; }

    }
}
