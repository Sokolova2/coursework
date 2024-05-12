using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace coursework
{
    public partial class Reports : Form
    {
        private DataBase db;
        public List<Item> items { get; set; } = new List<Item>();
        public Reports()
        {
            InitializeComponent();
            db = new DataBase();
            Report();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomePage homePage = new HomePage();
            homePage.Show();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void Report()
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
                    string dishesCheck = "SELECT dishes.dishes AS \"Страва\", SUM(menu.weightInGrams * ordering.numberOfDishes) AS \"Кількість з'їденої страви\" " +
                        "FROM dishes " +
                        "JOIN menu ON menu.dishesInMenu = dishes.dishesInMenu " +
                        "JOIN ordering ON ordering.dishes = dishes.dishes_id " +
                        $"WHERE ordering.dateAndTime BETWEEN '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}' " +
                        "GROUP BY dishes.dishes";
                    using (MySqlCommand command = new MySqlCommand(dishesCheck, connection))
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
                    string dishes = row.Cells["dishes"].Value.ToString();
                    string weightInGram = row.Cells["weightInGram"].Value.ToString();
                    this.items.Add(new Item
                    {
                        Dishes = dishes,
                        WeightInGram = weightInGram                        
                    });
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
            public string Dishes { get; set; }
            public string WeightInGram { get; set; }
           
        }

        private void PopulationDishes_Click(object sender, EventArgs e)
        {

            this.db = new DataBase();
            using (MySqlConnection connection = db.getConnection())
            {
                try
                {
                    connection.Open();
                    DateTime startDate = dateTimePicker1.Value.Date;
                    DateTime endDate = dateTimePicker2.Value.Date.AddDays(1);

                    string PopulationDishesQuery = "SELECT dishes.dishes, SUM(ordering.numberOfDishes) AS PopulationDishes " +
                        "FROM dishes " +
                        "JOIN menu ON menu.dishesInMenu = dishes.dishesInMenu " +
                        "JOIN ordering ON ordering.dishes = dishes.dishes_id " +
                        $"WHERE ordering.dateAndTime BETWEEN '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}' " +
                        "GROUP BY dishes.dishes " +
                        "ORDER BY PopulationDishes DESC ";
                      
                    using (MySqlCommand PopulationDishesCommand = new MySqlCommand(PopulationDishesQuery, connection))
                    {
                        object result = PopulationDishesCommand.ExecuteScalar();
                        if (result != null)
                        {
                            MessageBox.Show($"Найпопулярніша страва: {result.ToString()}"); 
                        }
                        else
                        {
                            MessageBox.Show("Немає даних ");
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Виникла помилка: " + ex.Message);
                }
            }
        }

        private void Summary_Click(object sender, EventArgs e)
        {
            this.db = new DataBase();
            using (MySqlConnection connection = db.getConnection())
            {
                try
                {
                    connection.Open();
                    DateTime startDate = dateTimePicker1.Value.Date;
                    DateTime endDate = dateTimePicker2.Value.Date.AddDays(1);

                    string SummaryQuery = "SELECT SUM(ordering.numberOfDishes * menu.priceInUAH) AS TotalSum " +
                        "FROM dishes " +
                        "JOIN menu ON menu.dishesInMenu = dishes.dishesInMenu " +
                        "JOIN ordering ON ordering.dishes = dishes.dishes_id "+
                        $"WHERE ordering.dateAndTime BETWEEN '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}' ";
                    using (MySqlCommand PopulationDishesCommand = new MySqlCommand(SummaryQuery, connection))
                    {
                        object result = PopulationDishesCommand.ExecuteScalar();
                        if (result != null)
                        {
                            MessageBox.Show($"Усього зароблена сума: {result.ToString()}");
                        }
                        else
                        {
                            MessageBox.Show("Немає даних ");
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Виникла помилка: " + ex.Message);
                }
            }
        }

        private void Found_Click(object sender, EventArgs e)
        {
            Report();
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
