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
    public partial class InsertDishes : Form
    {
        private DataBase db;
        public InsertDishes()
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

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu form = new Menu();
            form.Show();
        }
        

        private void InsertDishesButton_Click(object sender, EventArgs e)
        {
            string dishes = richTextBox2.Text;
            string weightInGrams = richTextBox3.Text;
            string priceInUAH = richTextBox4.Text;
            string employeeName = richTextBox5.Text;

            MySqlConnection connection = db.getConnection();
            connection.Open();

            try
            {
                string checkDishQuery = "SELECT dishes_id FROM dishes WHERE dishes = @dishes";
                int dishesId;
                using (MySqlCommand checkDishCommand = new MySqlCommand(checkDishQuery, connection))
                {
                    checkDishCommand.Parameters.AddWithValue("@dishes", dishes);
                    object result = checkDishCommand.ExecuteScalar();
                    if (result != null)
                    {
                        MessageBox.Show("Блюдо вже існує.");
                        return; 
                    }
                }

                string findEmployeeQuery = "SELECT employee_id FROM employee WHERE CONCAT(firstname, ' ', lastname) = @employeeName";
                int employeeId;
                using (MySqlCommand findEmployeeCommand = new MySqlCommand(findEmployeeQuery, connection))
                {
                    findEmployeeCommand.Parameters.AddWithValue("@employeeName", employeeName);
                    object result = findEmployeeCommand.ExecuteScalar();
                    if (result != null)
                    {
                        employeeId = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("Співробітник з таким іменем та прізвищем не знайден.");
                        return; 
                    }
                }
                string insertDishQuery = "INSERT INTO dishes (employee, dishes) VALUES (@employee, @dishes)";
                using (MySqlCommand insertDishCommand = new MySqlCommand(insertDishQuery, connection))
                {
                    insertDishCommand.Parameters.AddWithValue("@employee", employeeId);
                    insertDishCommand.Parameters.AddWithValue("@dishes", dishes);
                    insertDishCommand.ExecuteNonQuery();
                }
                string getDishIdQuery = "SELECT LAST_INSERT_ID()";
                using (MySqlCommand getDishIdCommand = new MySqlCommand(getDishIdQuery, connection))
                {
                    dishesId = Convert.ToInt32(getDishIdCommand.ExecuteScalar());
                }
                
                
                string insertMenuQuery = "INSERT INTO menu (dishesInMenu, weightInGrams, priceInUAH) VALUES (@dishesInMenu, @weightInGrams, @priceInUAH)";
                using (MySqlCommand insertMenuCommand = new MySqlCommand(insertMenuQuery, connection))
                {
                    insertMenuCommand.Parameters.AddWithValue("@dishesInMenu", dishesId);
                    insertMenuCommand.Parameters.AddWithValue("@weightInGrams", weightInGrams);
                    insertMenuCommand.Parameters.AddWithValue("@priceInUAH", priceInUAH);
                    insertMenuCommand.ExecuteNonQuery();
                }
                string updateDishesQuery = "UPDATE dishes SET dishesInMenu = @dishesInMenu WHERE dishes_id = @dishesId";
                using (MySqlCommand updateDishesCommand = new MySqlCommand(updateDishesQuery, connection))
                {
                    updateDishesCommand.Parameters.AddWithValue("@dishesInMenu", dishesId);
                    updateDishesCommand.Parameters.AddWithValue("@dishesId", dishesId);
                    updateDishesCommand.ExecuteNonQuery();
                }



                MessageBox.Show("Страва успішно додана.");
            }
            finally
            {
                connection.Close();
            }

        }
    }
}
