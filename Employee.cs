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
    public partial class Employee : Form
    {
        private DataBase db;
        public List<Item> items { get; set; } = new List<Item>();
        public Employee()
        {
            InitializeComponent();
            db = new DataBase();
            Employees();
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
        public void Employees()
        {
            DataTable dataTable = new DataTable();
            using (MySqlConnection connection = db.getConnection())
            {
                try
                {
                    connection.Open();
                    string employeeCheck = "SELECT employee.firstname, employee.lastname, employee.surname, " +
                         "employee.phoneNumber, employee.dateOfBirth, employee.adress, salary.salary, jobtitle.jobTitle " +
                         "FROM employee " +
                         "JOIN salary ON salary.salary_id = employee.salary " +
                         "JOIN jobtitle ON jobtitle.jobTitle_id = employee.jobTitle ";

                    using (MySqlCommand command = new MySqlCommand(employeeCheck, connection))
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
                    string firstName = row.Cells["firstname"].Value.ToString();
                    string lastName = row.Cells["lastname"].Value.ToString();
                    string surname = row.Cells["surname"].Value.ToString();
                    string phoneNumber = row.Cells["phoneNumber"].Value.ToString();
                    string dateOfBirth = row.Cells["dateOfBirth"].Value.ToString();
                    string address = row.Cells["adress"].Value.ToString();
                    string salary = row.Cells["salary"].Value.ToString();
                    string jobTitle = row.Cells["jobTitle"].Value.ToString();

                    this.items.Add(new Item
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Surname = surname,
                        PhoneNumber = phoneNumber,
                        DateOfBirth = dateOfBirth,
                        Address = address,
                        Salary = salary,
                        JobTitle = jobTitle
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
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Surname { get; set; }
            public string PhoneNumber { get; set; }
            public string DateOfBirth { get; set; }
            public string Address { get; set; }
            public string Salary { get; set; }
            public string JobTitle { get; set; }
        }

        private void ChangesEmployeeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChangesEmployees form = new ChangesEmployees();
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
