using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace coursework
{
    public partial class DeleteDishes : Form
    {
        private DataBase db;
        public DeleteDishes()
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

        internal DataBase DataBase1
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

        private void DeleteDishesButton_Click(object sender, EventArgs e)
        {
            this.db = new DataBase();
            string dishes = richTextBox2.Text;
            MySqlConnection connection = db.getConnection();
            try
            {
                connection.Open();
                string selectDishIdQuery = "SELECT dishesInMenu FROM dishes WHERE dishes = @dishes";
                int dishId = 0;
                using (MySqlCommand selectDishIdCommand = new MySqlCommand(selectDishIdQuery, connection))
                {
                    selectDishIdCommand.Parameters.AddWithValue("@dishes", dishes);
                    object result = selectDishIdCommand.ExecuteScalar();
                    if (result != null)
                    {
                        dishId = Convert.ToInt32(result);
                    }
                }
                string deleteDishesQuery = "DELETE FROM dishes WHERE dishes = @dishes";
                using (MySqlCommand deleteDishesCommand = new MySqlCommand(deleteDishesQuery, connection))
                {
                    deleteDishesCommand.Parameters.AddWithValue("@dishes", dishes);
                    deleteDishesCommand.ExecuteNonQuery();
                }
               

                string deleteMenuQuery = "DELETE FROM menu WHERE dishesInMenu = @dishesInMenu";
                using (MySqlCommand deleteMenuCommand = new MySqlCommand(deleteMenuQuery, connection))
                {
                    deleteMenuCommand.Parameters.AddWithValue("@dishesInMenu", dishId);
                    deleteMenuCommand.ExecuteNonQuery();
                }
                MessageBox.Show("Страва успішно видалена.");

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
