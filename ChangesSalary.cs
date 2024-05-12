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
    public partial class ChangesSalary : Form
    {
        private DataBase db;
        public ChangesSalary()
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

        private void FoundButton_Click(object sender, EventArgs e)
        {
            this.db = new DataBase();
            decimal salary = Convert.ToDecimal(richTextBox10.Text);
            MySqlConnection connection = db.getConnection();

            try
            {
                connection.Open();

                string checkSalaryQuery = "SELECT salary FROM salary WHERE salary = @salary";

                using (MySqlCommand checkSalaryCommand = new MySqlCommand(checkSalaryQuery, connection))
                {
                    checkSalaryCommand.Parameters.AddWithValue("@salary", salary);
                    MySqlDataReader reader = checkSalaryCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        string salaryFromDB = reader["salary"].ToString();
                        MessageBox.Show($"Заробітня плата {salaryFromDB} знайдена в базі даних.");
                        richTextBox2.Text = salaryFromDB;


                    }
                    else
                    {
                        MessageBox.Show("Вказанна заробітня плата не знайдена в базі даних.");
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

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Salary form = new Salary();
            form.Show();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UpdateSalaryButton_Click(object sender, EventArgs e)
        {
            this.db = new DataBase();
            decimal salaryOne = Convert.ToDecimal(richTextBox10.Text);
            decimal salaryTwo = Convert.ToDecimal(richTextBox2.Text);
            MySqlConnection connection = db.getConnection();

            try
            {
                connection.Open();

                string updateSalaryQuery = "UPDATE salary SET salary = @salaryTwo WHERE salary = @salaryOne";

                using (MySqlCommand updateSalaryCommand = new MySqlCommand(updateSalaryQuery, connection))
                {
                    updateSalaryCommand.Parameters.AddWithValue("@salaryOne", salaryOne);
                    updateSalaryCommand.Parameters.AddWithValue("@salaryTwo", salaryTwo);
                    int rowsAffected = updateSalaryCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Заробітня плата {salaryOne} успішно змінена на {salaryTwo}.");
                    }
                    else
                    {
                        MessageBox.Show("Вказана заробітня плата не знайдена в базі даних.");
                    }
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

        private void InsertSalaryButton_Click(object sender, EventArgs e)
        {
            this.db = new DataBase();
            decimal salary = Convert.ToDecimal(richTextBox3.Text);
            
            MySqlConnection connection = db.getConnection();

            try
            {
                connection.Open();

                string insertSalaryQuery = "INSERT INTO salary(salary) VALUES(@salary)";

                using (MySqlCommand insertSalaryCommand = new MySqlCommand(insertSalaryQuery, connection))
                {
                    insertSalaryCommand.Parameters.AddWithValue("@salary", salary);
                    int rowsAffected = insertSalaryCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Заробітня плата {salary} успішно додана в базу даних.");
                    }
                    else
                    {
                        MessageBox.Show("Не вдалося додати вказану заробітню плату в базу даних.");
                    }
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

        private void DeleteSalaryButton_Click(object sender, EventArgs e)
        {
            this.db = new DataBase();
            decimal salary = Convert.ToDecimal(richTextBox2.Text);
            MySqlConnection connection = db.getConnection();

            try
            {
                connection.Open();

                string insertSalaryQuery = "DELETE FROM salary WHERE salary = @salary";

                using (MySqlCommand insertSalaryCommand = new MySqlCommand(insertSalaryQuery, connection))
                {
                    insertSalaryCommand.Parameters.AddWithValue("@salary", salary);
                    int rowsAffected = insertSalaryCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Заробітня плата {salary} успішно видалена з бази даних.");
                    }
                    else
                    {
                        MessageBox.Show("Не вдалося видалити вказану заробітню плату в базі даних.");
                    }
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

    }
}
