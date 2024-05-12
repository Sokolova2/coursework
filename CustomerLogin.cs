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

    public partial class CustomerLogin : Form
    {
        private DataBase db;
        public CustomerLogin()
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
            LoyaltyCard form = new LoyaltyCard();
            form.Show();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            this.db = new DataBase();
            string firstname = richTextBox2.Text;
            string lastname = richTextBox3.Text;
            string phoneNumber = richTextBox4.Text;
            

            using (MySqlConnection connection = db.getConnection())
            {
                try
                {
                    connection.Open();

                    string checkClientQuery = "SELECT COUNT(*) FROM clients WHERE firstname = @firstname AND lastname = @lastname AND  phoneNumber = @phoneNumber";
                    using (MySqlCommand checkClientCommand = new MySqlCommand(checkClientQuery, connection))
                    {
                        checkClientCommand.Parameters.AddWithValue("@firstname", firstname);
                        checkClientCommand.Parameters.AddWithValue("@lastname", lastname);
                        checkClientCommand.Parameters.AddWithValue("@phoneNumber", phoneNumber);

                        int existingClientCount = Convert.ToInt32(checkClientCommand.ExecuteScalar());

                        if (existingClientCount > 0)
                        {
                            
                            MessageBox.Show("Клієнт існує в базі даних.");
                            this.Hide();
                            LoyaltyCard form = new LoyaltyCard();
                            form.FillClientInfo(firstname, lastname, "", phoneNumber, "");
                            form.Show();

                        }
                        else
                        {
                            
                            MessageBox.Show("Клієнт не знайден в базі даних.");
                            richTextBox2.Clear();
                            richTextBox4.Clear();
                            richTextBox3.Clear();

                        }
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
