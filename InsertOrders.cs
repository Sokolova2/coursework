using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace coursework
{
    public partial class InsertOrders : Form
    {
        private List<Item> items;
        private DataBase db;
        private List<Orders.Item> items1;

        public InsertOrders(List<Item> items)
        {
            InitializeComponent();
            this.items = items;
            FilldataGridView1();
            db = new DataBase();
        }

        public InsertOrders(List<Orders.Item> items1)
        {
            this.items1 = items1;
        }

        internal DataBase DataBase
        {
            get => default;
            set
            {
            }
        }

        public Item Item
        {
            get => default;
            set
            {
            }
        }

        private void FilldataGridView1()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Add("ProductNameColumn", "Назва продукту");
            dataGridView1.Columns.Add("NumberOfProduct", "Кількість блюд");
            dataGridView1.Columns.Add("OrderNumberColumn", "Номер замовлення");
            dataGridView1.Columns.Add("DateAndTime", "Дата та час");

            int orderNumber = GetNextOrderNumber();
            
            
            foreach (Item item in this.items)
            {
                int rowIndex = dataGridView1.Rows.Add(item.ProductName, item.NumberOfProduct, orderNumber);
                dataGridView1.Rows[rowIndex].Cells["DateAndTime"].Value = DateTime.Now.ToString();
            }
            dataGridView1.ClearSelection();
        }
        private int GetNextOrderNumber()
        {
            this.db = new DataBase();
            MySqlConnection connection = db.getConnection();
            connection.Open();
            string maxOrderNumberQuery = "SELECT MAX(parent_order_id) FROM ordering";
            using (MySqlCommand maxOrderNumberCommand = new MySqlCommand(maxOrderNumberQuery, connection))
            {
                object maxOrderNumberObject = maxOrderNumberCommand.ExecuteScalar();
                int maxOrderNumber = (maxOrderNumberObject == DBNull.Value) ? 0 : Convert.ToInt32(maxOrderNumberObject);
                return maxOrderNumber + 1;
            }
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void IssueOrderButton_Click_1(object sender, EventArgs e)
        {
           
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string productName = row.Cells["ProductNameColumn"].Value.ToString();
                int numberOfProduct = Convert.ToInt32(row.Cells["NumberOfProduct"].Value);
                int orderNumber = Convert.ToInt32(row.Cells["OrderNumberColumn"].Value);
                int tableInRestaurant = int.Parse(richTextBox2.Text);
                string dateAndTime = row.Cells["DateAndTime"].Value.ToString();
                AddOrderToDatabase(orderNumber, productName, numberOfProduct, tableInRestaurant, dateAndTime);
            }
            MessageBox.Show("Замовлення успішно додано!");
        }
        private void AddOrderToDatabase(int orderNumber, string productName, int numberOfProduct, int tableInRestaurant, string dateAndTime)
        {
            this.db = new DataBase();
            try
            {
                MySqlConnection connection = db.getConnection();
                connection.Open();

                string dishQuery = "SELECT dishes_id FROM dishes WHERE dishes = @ProductName";

                using (MySqlCommand command = new MySqlCommand(dishQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", productName);
                    int dishesId = Convert.ToInt32(command.ExecuteScalar());

                    string insertQuery = @"INSERT INTO ordering (parent_order_id, dishes, numberOfDishes, tableInRestaurant, dateAndTime)
                                   VALUES (@ParentOrderId, @DishesId, @NumberOfDishes, @TableInRestaurant, @DateAndTime)";
                    
                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@ParentOrderId", orderNumber);
                        insertCommand.Parameters.AddWithValue("@DishesId", dishesId);
                        insertCommand.Parameters.AddWithValue("@NumberOfDishes", numberOfProduct);
                        insertCommand.Parameters.AddWithValue("@TableInRestaurant", tableInRestaurant);
                        insertCommand.Parameters.AddWithValue("@DateAndTime", DateTime.Now);
                        insertCommand.ExecuteNonQuery();
                        
                    }
                    
                }
             

            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при додавані замовлення до бази даних: " + ex.Message, "Помилка");
            }
        }
       
        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomePage form = new HomePage();
            form.Show();
        }

        private void AllOrdersButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Orders form = new Orders();
            form.Show();
        }
    }
}
