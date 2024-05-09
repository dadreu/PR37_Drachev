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
                        Name = ItemsReader.IsDBNull(1) ? "" : ItemsReader.GetString(1),
                        Description = ItemsReader.IsDBNull(2) ? "" : ItemsReader.GetString(2),
                        Img = ItemsReader.IsDBNull(3) ? "" : ItemsReader.GetString(3),
                        Price = ItemsReader.IsDBNull(4) ? -1 : ItemsReader.GetInt32(4),
                        Category = ItemsReader.IsDBNull(5) ? null : Categorys.Where(x => x.Id == ItemsReader.GetInt32(5)).First()
                    });
                }
                MySqlConnection.Close();
                return items;
            }
        }

        public int Add(Items item)
        {
            MySqlConnection MySqlConnection = Connection.MySqlOpen();
            Connection.MySqlQuery($"Insert into `items` (`Name`, `Description`, `Img`, `Price`, `IdCategory`) Values ('{item.Name}', '{item.Description}', '{item.Img}', {item.Price}, {item.Category.Id});", MySqlConnection);
            MySqlConnection.Close();

            int IdItem = -1;
            MySqlConnection = Connection.MySqlOpen();
            MySqlDataReader mySqlDataReaderItem = Connection.MySqlQuery($"Select `Id` from `items` where `Name` = '{item.Name}' and `Description` = '{item.Description}' and `Price` = {item.Price} and `IdCategory` = {item.Category.Id};", MySqlConnection);
            if (mySqlDataReaderItem.HasRows)
            {
                mySqlDataReaderItem.Read();
                IdItem = mySqlDataReaderItem.GetInt32(0);
            }
            MySqlConnection.Close();
            return IdItem;
        }
        
        public void Delete(int id)
        {
            MySqlConnection mySqlConnection = Connection.MySqlOpen();
            Connection.MySqlQuery(
                $"DELETE FROM items WHERE Id = {id}", mySqlConnection);
            mySqlConnection.Close();
        }
       
        public void Update(Items Item, int categId)
        {
            MySqlConnection mySqlConnection = Connection.MySqlOpen();
            MySqlDataReader CategoryId = Connection.MySqlQuery(
                $"SELECT * FROM items WHERE Id = {Item.Id}", mySqlConnection);
            int newCategoryId = 0;
            if (CategoryId.HasRows)
            {
                CategoryId.Read();
                newCategoryId = CategoryId.GetInt32(5);
            }
            mySqlConnection.Close();
            mySqlConnection.Open();
            Connection.MySqlQuery($"UPDATE items SET Name = '{Item.Name}', Description = '{Item.Description}', Img = '{Item.Img}', Price = '{Item.Price}', IdCategory = '{categId}' WHERE Id = {Item.Id}", mySqlConnection);
            mySqlConnection.Close();
        }
    }
}
