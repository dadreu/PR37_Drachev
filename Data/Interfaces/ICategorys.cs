using Shop_Drachev.Data.Models;
using System.Collections.Generic;

namespace Shop_Drachev.Data.Interfaces
{
    public interface ICategorys
    {
        IEnumerable<Models.Categorys> AllCategorys { get; }
    }
}
