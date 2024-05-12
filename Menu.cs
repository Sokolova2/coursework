using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace coursework
{
    public partial class Menu : Form
    {
        private DataBase db;
        public List<Item> items { get; set; } = new List<Item>();

        internal DataBase DataBase
        {
            get => default;
            set
            {
            }
        }

        public Menu()
        {
            InitializeComponent();
            db = new DataBase();
            Menumenu();
        }

        public void Menumenu()
        {
            DataTable dataTable = new DataTable();
            using (MySqlConnection connection = db.getConnection())
            {
                try
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("SELECT dishes.dishes, menu.weightInGrams, menu.priceInUAH FROM menu JOIN dishes ON dishes.dishesInMenu = menu.dishesInMenu", connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }

                    InitTable();
                    FillTable(dataTable);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Виникла помилка: " + ex.Message);
                }
            }

        }
        private void InitTable()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            
            dataGridView1.Columns.Add("DishColumn", "Блюдо");
            dataGridView1.Columns.Add("WeightColumn", "Вага (г)");
            dataGridView1.Columns.Add("PriceColumn", "Цена (UAH)");
            dataGridView1.Columns.Add("NumberOfProduct", "Кількість блюд");
            
        }
        
        private void FillTable(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                string dish = row["dishes"].ToString();
                string weight = row["weightInGrams"].ToString();
                string price = row["priceInUAH"].ToString();
               

                dataGridView1.Rows.Add(dish, weight, price, "0"); 
            }
        }


        

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (row.Cells["DishColumn"].Value != null &&
                        row.Cells["WeightColumn"].Value != null &&
                        row.Cells["NumberOfProduct"].Value != null)
                    {
                        string productName = row.Cells["DishColumn"].Value.ToString();
                        string weight = row.Cells["WeightColumn"].Value.ToString();
                        int clicks = int.Parse(row.Cells["NumberOfProduct"].Value.ToString());
                        int numberOfProduct = clicks;
                        row.Cells["NumberOfProduct"].Value = (clicks + 1).ToString();
                    }
                }

                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }
        
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void InsertInOrdersButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                int numberOfProduct = int.Parse(row.Cells["NumberOfProduct"].Value.ToString());
                
                if (numberOfProduct != 0) {
                    if (row.Cells["DishColumn"].Value != null &&
                        row.Cells["WeightColumn"].Value != null &&
                        row.Cells["NumberOfProduct"].Value != null)
                    {
                        string productName = row.Cells["DishColumn"].Value.ToString();
                        string weight = row.Cells["WeightColumn"].Value.ToString();
                        
                        this.items.Add(new Item { ProductName = productName, NumberOfProduct = numberOfProduct });
                        Item existingItem = items.FirstOrDefault(item => item.ProductName == productName);
                    }
                }
            }
            HomePage form = new HomePage(items);
            form.Show();
        }

        private void BackButton_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            HomePage form = new HomePage();
            form.Show();
        }

        private void InsertDishesButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            InsertDishes form = new InsertDishes();
            form.Show();
        }

        private void UpdateDishesButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            UpdateMenu form = new UpdateMenu();
            form.Show();
        }

        private void DeleteDishesButton_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            DeleteDishes form = new DeleteDishes();
            form.Show();
        }

        private void DeleteFromMenu(string dishName)
        {
            using (MySqlConnection connection = db.getConnection())
            {
                try
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM menu WHERE dishesInMenu = (SELECT dishesInMenu FROM dishes WHERE dishes = @dishName)";
                    using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@dishName", dishName);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Під час видалення страви з меню сталася помилка: " + ex.Message);
                }
            }
        }

        private void DeleteFromDishes(string dishName)
        {
            using (MySqlConnection connection = db.getConnection())
            {
                try
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM dishes WHERE dishes = @dishName";
                    using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@dishName", dishName);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Під час видалення страви з таблиці страв сталася помилка:: " + ex.Message);
                }
            }
        }
    }
    public struct Item
    {
        public string ProductName { get; set; }
        public int NumberOfProduct { get; set; }
        
    }
}
