using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using MySql.Data.MySqlClient;
using KazouInventaris.Classes;
using System.Windows.Media.Media3D;
using System.Runtime.Serialization;

namespace KazouInventaris.Persistence
{
    public class PersistenceCode
    {
        //connectionstring
        string ConnStr = "server=localhost;user id=root; password=nF2554NN!Nibor2004;database=db_kelderkazou";

        //load all items
        public List<inventoryItem> LoadItems(){

            List<inventoryItem> items = new List<inventoryItem>();
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbl_kelderinventory", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    inventoryItem item = new inventoryItem();
                    item.id = reader.GetInt32("id");
                    item.name = reader.GetString("name");
                    item.description = reader.GetString("description");
                    item.amount = reader.GetInt32("amount");
                    item.location = reader.GetString("location");
                    item.category = reader.GetInt32("category");
                    item.purchaseAmount = reader.GetInt32("purchase_amount");
                    items.Add(item);
                }
            }
            return items;
        }

        //load 1 item
        public inventoryItem LoadItem(int id){
            inventoryItem item = new inventoryItem();
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM tbl_kelderinventory WHERE id = {id}", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    item.id = reader.GetInt32("id");
                    item.name = reader.GetString("name");
                    item.description = reader.GetString("description");
                    item.amount = reader.GetInt32("amount");
                    item.location = reader.GetString("location");
                    item.category = reader.GetInt32("category");
                    item.purchaseAmount = reader.GetInt32("purchase_amount");
                }
            }
            return item;
        }

        //load items per category
        public List<inventoryItem> LoadCategoryItems(int categoryId){
            List<inventoryItem> items = new List<inventoryItem>();
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM tbl_kelderinventory where category = {categoryId}", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    inventoryItem item = new inventoryItem();
                    item.id = reader.GetInt32("id");
                    item.name = reader.GetString("name");
                    item.description = reader.GetString("description");
                    item.amount = reader.GetInt32("amount");
                    item.location = reader.GetString("location");
                    item.category = reader.GetInt32("category");
                    item.purchaseAmount = reader.GetInt32("purchase_amount");
                    items.Add(item);
                }
            }
            return items;

        }
        //load all categories
        public List<inventoryCategory> LoadCategories(){
            List<inventoryCategory> categories = new List<inventoryCategory>();
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbl_categories", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    inventoryCategory category = new inventoryCategory();
                    category.id = reader.GetInt32("category_id");
                    category.name = reader.GetString("category_name");
                    categories.Add(category);
                }
            }
            return categories;
        }
        //load 1 category
        public inventoryCategory LoadCategory(int id)
        {
            inventoryCategory category = new inventoryCategory();
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM tbl_categories WHERE category_id = {id}", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    category.id = reader.GetInt32("category_id");
                    category.name = reader.GetString("category_name");
                }
            }
            return category;
        }
        //get category id by name
        public int GetCategoryId(string name)
        {
            int id = 0;
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT category_id FROM tbl_categories WHERE category_name = '{name}'", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32("category_id");
                }
            }
            return id;
        }

        //load all borrowed items
        public List<BorrowedItems> LoadBorrowedItems()
        {
            List<BorrowedItems> items = new List<BorrowedItems>();
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbl_uitgeleendeitems", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BorrowedItems item = new BorrowedItems();
                    item.ID = reader.GetInt32("ID");
                    item.Name = reader.GetString("naam");
                    item.vacantieCode = reader.GetString("vakantiecode");
                    item.Date = reader.GetString("datum");
                    item.uitgeleendItemID = reader.GetInt32("uitgeleendeItem_ID");
                    item.amount = reader.GetInt32("aantal");
                    item.returned = reader.GetInt32("teruggebracht");
                    items.Add(item);
                }
            }
            return items;
        }
        //borrow item
        public void BorrowItem(inventoryItem item,string name, string vakantiecode, int amount)
        {
            
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO tbl_uitgeleendeitems (naam,vakantiecode,aantal,uitgeleendeItem_ID,datum,teruggebracht) values('{name}','{vakantiecode}',{amount},{item.id},'{DateTime.Today.ToShortDateString()}',0)", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            using (MySqlConnection connection = new MySqlConnection(ConnStr))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE tbl_kelderinventory SET amount = amount - {amount} WHERE ID = '{item.id}'", connection);
                command.ExecuteNonQuery();
            }
        }
        //loads borrowed item based on id
        public BorrowedItems LoadBorrowedItem(int id)
        {
            BorrowedItems item = new BorrowedItems();
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM tbl_uitgeleendeitems WHERE ID = {id}", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    item.ID = reader.GetInt32("ID");
                    item.Name = reader.GetString("naam");
                    item.vacantieCode = reader.GetString("vakantiecode");
                    item.Date = reader.GetString("datum");
                    item.uitgeleendItemID = reader.GetInt32("uitgeleendeItem_ID");
                    item.amount = reader.GetInt32("aantal");
                    item.returned = reader.GetInt32("teruggebracht");
                }
            }
            return item;
        }

        //edit item
        public void EditItem(inventoryItem item){
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"UPDATE tbl_kelderinventory SET name = '{item.name}', description = '{item.description}', amount = {item.amount}, location = '{item.location}', category = '{item.category}', purchase_amount = {item.purchaseAmount} WHERE id = {item.id}", conn);
                cmd.ExecuteNonQuery();
            }
        }

        //add item
        public void createItem(inventoryItem item){
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO tbl_kelderinventory (name, description, amount, location, category,purchase_amount) VALUES ('{item.name}', '{item.description}', {item.amount}, '{item.location}', '{item.category}', {item.purchaseAmount})", conn);
                cmd.ExecuteNonQuery();
            }
        }
        //delete item
        public void deleteItem(int id){
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"DELETE FROM tbl_kelderinventory WHERE id = {id}", conn);
                cmd.ExecuteNonQuery();
            }
        }
        //returns borrowed item
        public void returnItem(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"UPDATE tbl_uitgeleendeitems SET teruggebracht  = 1", conn);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
