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
    public partial class Summary : Form
    {
        private DataBase db;
        public Summary()
        {
            InitializeComponent();
            db = new DataBase();
            Sum();
        }

        internal DataBase DataBase
        {
            get => default;
            set
            {
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Orders orders = new Orders();
            orders.Show();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
         
        public void Sum()
        {
            this.db = new DataBase();
            DataTable dataTable = new DataTable();
            using (MySqlConnection connection = db.getConnection())
            {
                try
                {
                    connection.Open();
                    DateTime startDate = dateTimePicker1.Value.Date;
                    DateTime endDate = dateTimePicker2.Value.Date.AddDays(1);
                    string SummaryOrdersQuery = "SELECT ordering.parent_order_id AS \"Номер замовлення\", " +
                        "SUM(ordering.numberOfDishes * menu.priceInUAH) AS \"Загальна сума\" " +
                        "FROM dishes " +
                        "JOIN menu ON menu.dishesInMenu = dishes.dishesInMenu " +
                        "JOIN ordering ON ordering.dishes = dishes.dishes_id " +
                        $"WHERE ordering.dateAndTime BETWEEN '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}' "+
                        "GROUP BY ordering.parent_order_id;";
                    using (MySqlCommand command = new MySqlCommand(SummaryOrdersQuery, connection))
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
                    string summary = row.Cells["Summary"].Value.ToString();
                   
                }

                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void Found_Click(object sender, EventArgs e)
        {
            Sum();
        }
    }
}
