using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace coursework
{
    public partial class LoyaltyCard : Form
    {
        private DataBase db;

        public LoyaltyCard()
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
            HomePage form = new HomePage();
            form.Show();
        }

        private void RegistrationClientsButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ClientRegistration form = new ClientRegistration();
            form.Show();
        }

        private void LoginClientsButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerLogin form = new CustomerLogin();
            form.Show();
        }

        private void LoyaltyCard_Load(object sender, EventArgs e)
        {
            string firstname = "";
            string lastname = "";
            string surname = "";
            string phoneNumber = "";
            string codeCard = "";
            
            using (MySqlConnection connection = db.getConnection())
            {
                try
                {
                    connection.Open();
                    string getClientInfoQuery = "SELECT * FROM clients WHERE phoneNumber = @phoneNumber";
                    using (MySqlCommand getClientInfoCommand = new MySqlCommand(getClientInfoQuery, connection))
                    {
                        getClientInfoCommand.Parameters.AddWithValue("@phoneNumber", richTextBox3.Text);

                        using (MySqlDataReader reader = getClientInfoCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                firstname = reader.GetString("firstname");
                                lastname = reader.GetString("lastname");
                                surname = reader.GetString("surname");
                                phoneNumber = reader.GetString("phoneNumber");
                                codeCard = reader.GetString("codeCard");
                            }
                        }

                    }
                    connection.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Виникла помилка: " + ex.Message);
                }
            }
            FillClientInfo(firstname, lastname, surname, phoneNumber, codeCard);
        }
        public void FillClientInfo(string firstname, string lastname, string surname, string phoneNumber, string codeCard)
        {
            richTextBox2.Text = firstname;
            richTextBox5.Text = lastname;
            richTextBox4.Text = surname;
            richTextBox3.Text = phoneNumber;
            richTextBox6.Text = codeCard;
        }

        private void UpdateClientsButton_Click(object sender, EventArgs e)
        {
            this.db = new DataBase();
            string firstname = richTextBox2.Text;
            string lastname = richTextBox5.Text;
            string surname = richTextBox4.Text;
            string phoneNumber = richTextBox3.Text;
            string codeCard = richTextBox6.Text;

            if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(codeCard))
            {
                MessageBox.Show("Будь ласка, введіть усі дані клієнта.");
                return;
            }

            MySqlConnection connection = db.getConnection();
            try
            {
                connection.Open();

                string updateClientQuery = "UPDATE clients SET firstname = @firstname, lastname = @lastname, surname = @surname, phoneNumber = @phoneNumber, codeCard = @codeCard WHERE codeCard = @codeCard";

                using (MySqlCommand updateCommand = new MySqlCommand(updateClientQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@firstname", firstname);
                    updateCommand.Parameters.AddWithValue("@lastname", lastname);
                    updateCommand.Parameters.AddWithValue("@surname", surname);
                    updateCommand.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                    updateCommand.Parameters.AddWithValue("@codeCard", codeCard);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Дані клієнта успішно оновленні.");
                    }
                    else
                    {
                        MessageBox.Show("Клієнт з іменем " + firstname + " " + lastname + " " + surname + " не знайден.");
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

        private void DeleteClientsButton_Click(object sender, EventArgs e)
        {
            this.db = new DataBase();
            string firstname = richTextBox2.Text;
            string lastname = richTextBox5.Text;
            string surname = richTextBox4.Text;
            string phoneNumber = richTextBox3.Text;
            string codeCard = richTextBox6.Text;
            MySqlConnection connection = db.getConnection();
            try
            {
                connection.Open();

                string deleteClientQuery = "DELETE FROM clients WHERE phoneNumber = @phoneNumber";

                using (MySqlCommand deleteCommand = new MySqlCommand(deleteClientQuery, connection))
                {

                    deleteCommand.Parameters.AddWithValue("@phoneNumber", phoneNumber);

                    int rowsAffected = deleteCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Дані клієнта успішно видалені.");
                    }
                    else
                    {
                        MessageBox.Show("Клієнт з іменем " + firstname + " " + lastname + " " + surname + " не знайден.");
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


