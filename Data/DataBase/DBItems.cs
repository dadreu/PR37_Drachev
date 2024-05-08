using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Shop_Drachev.Data.Common;
using Shop_Drachev.Data.Models;
using Shop_Drachev.Data.Interfaces;
using Shop_Drachev.Data.DataBase;

namespace Shop_Drachev.Data.DataBase
{
    public class DBItems : IItems
    {
        public IEnumerable<Categorys> Categorys = new DBCategory().AllCategorys;

        public IEnumerable<Items> AllItems
        {
            get
            {
                List<Items> items = new List<Items>();
                MySqlConnection MySqlConnection = Connection.MySqlOpen();
                MySqlDataReader ItemsReader = Connection.MySqlQuery("Select * from Items Order By 'Name';", MySqlConnection);
                while (ItemsReader.Read())
                {
                    items.Add(new Items()
                    {
                        Id = ItemsReader.IsDBNull(0) ? -1 : ItemsReader.GetInt32(0),
                        Name = ItemsReader.IsDBNull(1) ? null : ItemsReader.GetString(1),
                        Description = ItemsReader.IsDBNull(2) ? null : ItemsReader.GetString(2),
                        Img = ItemsReader.IsDBNull(3) ? null : ItemsReader.GetString(3),
                        Price = ItemsReader.IsDBNull(4) ? -1 : ItemsReader.GetInt32(4),
                        Category = ItemsReader.IsDBNull(5) ? null : Categorys.First(x => x.Id == ItemsReader.GetInt32(5))
                    });
                }
                MySqlConnection.Close();
                return items;
            }
        }
    }
}
