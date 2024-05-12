using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace coursework
{
    public partial class ClientRegistration : Form
    {
        private DataBase db;
        private string codeCard;
        public ClientRegistration()
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

        private void RegistrationButton_Click(object sender, EventArgs e)
        {
            string firstname = richTextBox2.Text;
            string surname = richTextBox4.Text;
            string lastname = richTextBox6.Text;
            string phoneNumber = richTextBox3.Text;
            Random random = new Random();

            for (int i = 0; i < 16; i++)
            {
                codeCard += random.Next(0, 10);
            }

            MessageBox.Show("Код вашої карти лояльності: " + codeCard);
            using (MySqlConnection connection = db.getConnection())
            {
                try
                {
                    connection.Open();

                    string checkClientQuery = "SELECT COUNT(*) FROM clients WHERE firstName = @firstname AND surname = @surname AND lastname = @lastname AND phoneNumber = @phoneNumber";

                    using (MySqlCommand checkClientCommand = new MySqlCommand(checkClientQuery, connection))
                    {
                        checkClientCommand.Parameters.AddWithValue("@firstname", firstname);
                        checkClientCommand.Parameters.AddWithValue("@surname", surname);
                        checkClientCommand.Parameters.AddWithValue("@lastname", lastname);
                        checkClientCommand.Parameters.AddWithValue("@phoneNumber", phoneNumber);

                        int existingClientCount = Convert.ToInt32(checkClientCommand.ExecuteScalar());

                        if (existingClientCount!= 0 )
                        {
                            MessageBox.Show("Запис з такими даними вже існує в базі даних.");
                        }
                        else
                        {
                            string insertQuery = "INSERT INTO clients (firstName, surname, lastname, phoneNumber, codeCard) VALUES (@firstname, @surname, @lastname, @phoneNumber, @codeCard)";
                            using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@firstname", firstname);
                                insertCommand.Parameters.AddWithValue("@surname", surname);
                                insertCommand.Parameters.AddWithValue("@lastname", lastname);
                                insertCommand.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                                insertCommand.Parameters.AddWithValue("@codeCard", codeCard);
                                insertCommand.ExecuteNonQuery();
                                MessageBox.Show("Клієнт успішно додан в базу даних.");
                                this.Hide();
                                CustomerLogin form = new CustomerLogin();
                                form.Show();
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
 
    }
}
