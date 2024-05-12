using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace coursework
{
    public partial class UpdateMenu : Form
    {
        private DataBase db;
        
        public UpdateMenu()
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
            Menu form = new Menu();
            form.Show();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FoundDishesButton_Click(object sender, EventArgs e)
        {
            string dishes = richTextBox2.Text;

            MySqlConnection connection = db.getConnection();

            try
            {
                connection.Open();

                string checkDishQuery = "SELECT menu.weightInGrams, menu.priceInUAH, CONCAT(employee.firstname, ' ', employee.lastname, ' ', employee.surname) " +
                    "FROM menu " +
                    "JOIN dishes ON menu.dishesInMenu = dishes.dishesInMenu " +
                    "JOIN employee ON employee.employee_id = dishes.employee " +
                    "WHERE dishes.dishes = @dishes";

                using (MySqlCommand checkDishCommand = new MySqlCommand(checkDishQuery, connection))
                {
                    checkDishCommand.Parameters.AddWithValue("@dishes", dishes);
                    MySqlDataReader reader = checkDishCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        string weightInGrams = reader["weightInGrams"].ToString();
                        string priceInUAH = reader["priceInUAH"].ToString();
                        string employee = reader.GetString(2);

                        richTextBox3.Text = dishes;
                        richTextBox4.Text = weightInGrams;
                        richTextBox6.Text = priceInUAH;
                        richTextBox5.Text = employee;

                        MessageBox.Show("Інформація про страву успішно отримана.");
                    }
                    else
                    {
                        MessageBox.Show("Страва не знайдена в базі даних.");
                        richTextBox3.Clear();
                        richTextBox4.Clear();
                        richTextBox6.Clear();
                        richTextBox5.Clear();
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


        private void UpdateDishesButton_Click_1(object sender, EventArgs e)
        {
            this.db = new DataBase();
            string dishes = richTextBox3.Text;
            decimal weightInGrams = Convert.ToDecimal(richTextBox4.Text);
            decimal price = Convert.ToDecimal(richTextBox6.Text);
            string employee = richTextBox5.Text;

            MySqlConnection connection = db.getConnection();
            connection.Open();

            string selectEmployeeIdQuery = "SELECT employee_id FROM employee WHERE CONCAT(firstname, ' ', surname, ' ', lastname) = @employeeName";
            int employeeId = 0;
            using (MySqlCommand selectEmployeeIdCommand = new MySqlCommand(selectEmployeeIdQuery, connection))
            {
                selectEmployeeIdCommand.Parameters.AddWithValue("@employeeName", employee);
                object result = selectEmployeeIdCommand.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    employeeId = Convert.ToInt32(result);
                }
                else
                {
                    MessageBox.Show("Співробітник не знайден.");
                    connection.Close();
                    return;
                }
            }
            string updateDishesQuery = "UPDATE dishes SET dishes = @dishes, employee = @employeeId WHERE dishes = @dishes";
            using (MySqlCommand updateDishesCommand = new MySqlCommand(updateDishesQuery, connection))
            {
                updateDishesCommand.Parameters.AddWithValue("@dishes", dishes);
                updateDishesCommand.Parameters.AddWithValue("@employeeId", employeeId);
                updateDishesCommand.ExecuteNonQuery();
            }
            string selectDishesInMenuQuery = "SELECT dishesInMenu FROM dishes WHERE dishes = @dishes";
            int dishesInMenu = 0;
            using (MySqlCommand selectDishesInMenuCommand = new MySqlCommand(selectDishesInMenuQuery, connection))
            {
                selectDishesInMenuCommand.Parameters.AddWithValue("@dishes", dishes);
                object result = selectDishesInMenuCommand.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    dishesInMenu = Convert.ToInt32(result);
                }
            }

            string updateMenuQuery = "UPDATE menu SET weightInGrams = @weightInGrams, priceInUAH = @price WHERE dishesInMenu = @dishesInMenu";
            using (MySqlCommand updateMenuCommand = new MySqlCommand(updateMenuQuery, connection))
            {
                updateMenuCommand.Parameters.AddWithValue("@weightInGrams", weightInGrams);
                updateMenuCommand.Parameters.AddWithValue("@price", price);
                updateMenuCommand.Parameters.AddWithValue("@dishesInMenu", dishesInMenu); 
                updateMenuCommand.ExecuteNonQuery();
            }

            connection.Close();

            MessageBox.Show("Дані успішно оновлено!");
        }
    }
}

