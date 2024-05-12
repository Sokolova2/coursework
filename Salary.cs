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
    public partial class Salary : Form
    {
        private DataBase db;
        public List<Item> items { get; set; } = new List<Item>();
        public Salary()
        {
            InitializeComponent();
            db = new DataBase();
            Salares();
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
        public void Salares()
        {
            DataTable dataTable = new DataTable();
            using (MySqlConnection connection = db.getConnection())
            {
                try
                {
                    connection.Open();
                    string salaryCheck = "SELECT salary FROM salary ";

                    using (MySqlCommand command = new MySqlCommand(salaryCheck, connection))
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
                    string salary = row.Cells["salary"].Value.ToString();
                    this.items.Add(new Item
                    {
                        Salary = salary,
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
            public string Salary { get; set; }
            
        }

        private void ChangesSalaryButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChangesSalary form = new ChangesSalary();
            form.Show();
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
