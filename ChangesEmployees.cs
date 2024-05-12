using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace coursework
{
    public partial class ChangesEmployees : Form
    {
        private DataBase db;
        public ChangesEmployees()
        {
            InitializeComponent();
            db = new DataBase();
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
            Employee form = new Employee();
            form.Show();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void InsertEmployeeButton_Click(object sender, EventArgs e)
        {
            this.db = new DataBase();
            string firstname = richTextBox3.Text;
            string lastname = richTextBox2.Text;
            string surname = richTextBox4.Text;
            string phoneNumber = richTextBox5.Text;
            DateTime dateOfBirth = DateTime.Parse(richTextBox6.Text);
            string formattedDateOfBirth = dateOfBirth.ToString("yyyy-MM-dd");
            string adress = richTextBox7.Text;
            string salary = richTextBox8.Text;
            string jobtitle = richTextBox9.Text;
            using (MySqlConnection connection = db.getConnection())
            {
                try
                {
                    connection.Open();

                    string findSalaryIdQuery = "SELECT salary_id FROM salary WHERE salary = @salary";
                    MySqlCommand findSalaryIdCommand = new MySqlCommand(findSalaryIdQuery, connection);
                    findSalaryIdCommand.Parameters.AddWithValue("@salary", salary);
                    int salaryId = Convert.ToInt32(findSalaryIdCommand.ExecuteScalar());

                    string findJobTitleIdQuery = "SELECT jobTitle_id FROM jobtitle WHERE jobTitle = @jobtitle";
                    MySqlCommand findJobTitleIdCommand = new MySqlCommand(findJobTitleIdQuery, connection);
                    findJobTitleIdCommand.Parameters.AddWithValue("@jobtitle", jobtitle);
                    int jobTitleId = Convert.ToInt32(findJobTitleIdCommand.ExecuteScalar());

                    
                    string checkEmployeeQuery = "SELECT COUNT(*) FROM employee WHERE firstname = @firstname AND surname = @surname AND lastname = @lastname AND phoneNumber = @phoneNumber AND dateOfBirth = @dateOfBirth AND adress = @adress";
                    using (MySqlCommand checkEmployeeCommand = new MySqlCommand(checkEmployeeQuery, connection))
                    {
                        checkEmployeeCommand.Parameters.AddWithValue("@firstname", firstname);
                        checkEmployeeCommand.Parameters.AddWithValue("@surname", surname);
                        checkEmployeeCommand.Parameters.AddWithValue("@lastname", lastname);
                        checkEmployeeCommand.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                        checkEmployeeCommand.Parameters.AddWithValue("@dateOfBirth", formattedDateOfBirth);
                        checkEmployeeCommand.Parameters.AddWithValue("@adress", adress);

                        int existingClientCount = Convert.ToInt32(checkEmployeeCommand.ExecuteScalar());

                        if (existingClientCount != 0)
                        {
                            MessageBox.Show("Запис з такими даними вже існує в базі даних.");
                        }
                        else
                        {
                            string insertQuery = "INSERT INTO employee (firstname, lastname, surname, phoneNumber, dateOfBirth, adress, salary, jobtitle) VALUES (@firstname, @lastname, @surname, @phoneNumber, @dateOfBirth, @adress, @salary, @jobtitle)";
                            using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@firstname", firstname);
                                insertCommand.Parameters.AddWithValue("@lastname", lastname);
                                insertCommand.Parameters.AddWithValue("@surname", surname);
                                insertCommand.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                                insertCommand.Parameters.AddWithValue("@dateOfBirth", formattedDateOfBirth );
                                insertCommand.Parameters.AddWithValue("@adress", adress);
                                insertCommand.Parameters.AddWithValue("@salary", salaryId);
                                insertCommand.Parameters.AddWithValue("@jobtitle", jobTitleId);
                                insertCommand.ExecuteNonQuery();
                                MessageBox.Show("Співробітник успішно додан до бази даних.");
                                richTextBox3.Clear();
                                richTextBox2.Clear();
                                richTextBox4.Clear();
                                richTextBox5.Clear();
                                richTextBox6.Clear();
                                richTextBox7.Clear();
                                richTextBox8.Clear();
                                richTextBox9.Clear();

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Виникла помилка: " + ex.Message);
                }
            }
        }

        private void FoundButton_Click(object sender, EventArgs e)
        {
            this.db = new DataBase();
            string employee = richTextBox10.Text;

            MySqlConnection connection = db.getConnection();

            try
            {
                connection.Open();

                string[] names = employee.Split(' ');
                string firstname = names[0];
                string lastname = names[1];
                string checkEmployeeQuery = "SELECT employee.firstname, employee.lastname, employee.surname, " +
                         "employee.phoneNumber, employee.dateOfBirth, employee.adress, salary.salary, jobtitle.jobTitle " +
                         "FROM employee " +
                         "JOIN salary ON salary.salary_id = employee.salary " +
                         "JOIN jobtitle ON jobtitle.jobTitle_id = employee.jobTitle " +
                         "WHERE employee.firstname = @firstname AND employee.lastname = @lastname";

                using (MySqlCommand checkEmployeeCommand = new MySqlCommand(checkEmployeeQuery, connection))
                {
                    checkEmployeeCommand.Parameters.AddWithValue("@firstname", firstname);
                    checkEmployeeCommand.Parameters.AddWithValue("@lastname", lastname);
                    MySqlDataReader reader = checkEmployeeCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        string surname = reader["surname"].ToString();
                        string phoneNumber = reader["phoneNumber"].ToString();
                        string dateOfBirth = DateTime.Parse(reader["dateOfBirth"].ToString()).ToString("yyyy-MM-dd");
                        string adress = reader["adress"].ToString();
                        string salary = reader["salary"].ToString();
                        string jobTitle = reader["jobTitle"].ToString();

                        richTextBox3.Text = firstname;
                        richTextBox2.Text = lastname;
                        richTextBox4.Text = surname;
                        richTextBox5.Text = phoneNumber;
                        richTextBox6.Text = dateOfBirth;
                        richTextBox7.Text = adress;
                        richTextBox8.Text = salary;
                        richTextBox9.Text = jobTitle;

                        MessageBox.Show("Інформація про співробітника успішно отримана.");
                    }
                    else
                    {
                        MessageBox.Show("Співробітник не знайден в базі даних.");
                        richTextBox3.Clear();
                        richTextBox4.Clear();
                        richTextBox5.Clear();
                        richTextBox6.Clear();
                        richTextBox7.Clear();
                        richTextBox8.Clear();
                        richTextBox9.Clear();
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Виникла помилка: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void UpdateEmployeeButton_Click(object sender, EventArgs e)
        {
            string firstname = richTextBox3.Text;
            string lastname = richTextBox2.Text;
            string surname = richTextBox4.Text;
            string phoneNumber = richTextBox5.Text;
            string dateOfBirth = richTextBox6.Text;
            string adress = richTextBox7.Text;
            string salary = richTextBox8.Text;
            string jobtitle = richTextBox9.Text;

            using (MySqlConnection connection = db.getConnection())
            {
                try
                {
                    connection.Open();

                    string findSalaryIdQuery = "SELECT salary_id FROM salary WHERE salary = @salary";
                    MySqlCommand findSalaryIdCommand = new MySqlCommand(findSalaryIdQuery, connection);
                    findSalaryIdCommand.Parameters.AddWithValue("@salary", salary);
                    int salaryId = Convert.ToInt32(findSalaryIdCommand.ExecuteScalar());

                    string findJobTitleIdQuery = "SELECT jobTitle_id FROM jobtitle WHERE jobTitle = @jobtitle";
                    MySqlCommand findJobTitleIdCommand = new MySqlCommand(findJobTitleIdQuery, connection);
                    findJobTitleIdCommand.Parameters.AddWithValue("@jobtitle", jobtitle);
                    int jobTitleId = Convert.ToInt32(findJobTitleIdCommand.ExecuteScalar());
                    string findEmployeeIdQuery = "SELECT employeeId FROM employee WHERE firstname = @firstname AND lastname = @lastname AND phoneNumber = @phoneNumber";


                    string updateQuery = "UPDATE employee SET firstname = @firstname, lastname = @lastname, surname = @surname, phoneNumber = @phoneNumber, dateOfBirth = @dateOfBirth, adress = @adress, salary = @salary, jobtitle = @jobtitle WHERE firstname = @firstname AND surname = @surname";
                    using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                    {

                        updateCommand.Parameters.AddWithValue("@firstname", firstname);
                        updateCommand.Parameters.AddWithValue("@lastname", lastname);
                        updateCommand.Parameters.AddWithValue("@surname", surname);
                        updateCommand.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                        updateCommand.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                        updateCommand.Parameters.AddWithValue("@adress", adress);
                        updateCommand.Parameters.AddWithValue("@salary", salaryId);
                        updateCommand.Parameters.AddWithValue("@jobtitle", jobTitleId);
                        updateCommand.ExecuteNonQuery();
                        MessageBox.Show("Дані про співробітника успішно оновлені.");
                        richTextBox3.Clear();
                        richTextBox2.Clear();
                        richTextBox4.Clear();
                        richTextBox5.Clear();
                        richTextBox6.Clear();
                        richTextBox7.Clear();
                        richTextBox8.Clear();
                        richTextBox9.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Виникла помилка: " + ex.Message);
                }
            }
        }

        private void DeleteEmployeeButton_Click(object sender, EventArgs e)
        {
            string firstname = richTextBox3.Text;
            string lastname = richTextBox2.Text;
            string surname = richTextBox4.Text;
            string phoneNumber = richTextBox5.Text;
            string dateOfBirth = richTextBox6.Text;
            string adress = richTextBox7.Text;
            string salary = richTextBox8.Text;
            string jobtitle = richTextBox9.Text;

            using (MySqlConnection connection = db.getConnection())
            {
                try
                {
                    connection.Open();

                    string updateQuery = "DELETE FROM employee WHERE firstname = @firstname AND surname = @surname";
                    using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                    {

                        updateCommand.Parameters.AddWithValue("@firstname", firstname);
                        updateCommand.Parameters.AddWithValue("@surname", surname);


                        updateCommand.ExecuteNonQuery();
                        MessageBox.Show("Дані про співробітника успішно видалені.");
                        richTextBox3.Clear();
                        richTextBox2.Clear();
                        richTextBox4.Clear();
                        richTextBox5.Clear();
                        richTextBox6.Clear();
                        richTextBox7.Clear();
                        richTextBox8.Clear();
                        richTextBox9.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Виникла помилка: " + ex.Message);
                }

            }
        }
    }
}
