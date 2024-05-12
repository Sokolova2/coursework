using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace coursework
{
    public partial class Orders : Form
    {
        private DataBase db;
        private List<Item> items;
        public List<Item> item { get; set; } = new List<Item>();
      
        public Orders()
        {
            InitializeComponent();
            this.items = new List<Item>();
            db = new DataBase();
            Ordering();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomePage form = new HomePage();
            form.Show();

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void Ordering()
        {
            this.db = new DataBase();
            DataTable dataTable = new DataTable();
            using (MySqlConnection connection = db.getConnection())
            {
                try
                {
                    connection.Open();
                    string orderCheck = "SELECT ordering.parent_order_id AS \"Номер замовлення\", dishes.dishes AS \"Страви\", ordering.tableInRestaurant AS \"Номер столику\", ordering.numberOfDishes AS \"Кількість страв\", ordering.dateAndTime AS \"Дата та час\"  " +
                                            "FROM ordering " +
                                            "JOIN dishes ON dishes.dishes_id = ordering.dishes";

                    using (MySqlCommand command = new MySqlCommand(orderCheck, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                    FillTable(dataTable);


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Виникла помилка: " + ex.Message);
                }
            }

        }
        private void FillTable(DataTable dataTable)
        {
            dataGridView1.DataSource = dataTable;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    string orderNumber = row.Cells["OrderNumber"].Value.ToString();
                    string dishes = row.Cells["Dishes"].Value.ToString();
                    string tableNumber = row.Cells["TableNumber"].Value.ToString();
                    string numberOfDishes = row.Cells["NumberOfDishes"].Value.ToString();
                    string dateAndTime = row.Cells["DateAndTime"].Value.ToString();
                }

                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        public struct Item
        {
            public string OrderNumber { get; set; }
            public string Dishes { get; set; }
            public string TableNumber { get; set; }
            public string NumberOfDishes { get; set; }
        }

        private void Summary_Click(object sender, EventArgs e)
        {
            this.Hide();
            Summary summary = new Summary();
            summary.Show();
        }

        internal DataBase DataBase
        {
            get => default;
            set
            {
            }
        }
    }
}
