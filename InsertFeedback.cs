using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;
using MySqlX.XDevAPI;
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
    public partial class InsertFeedback : Form
    {
        private DataBase db;
        public InsertFeedback()
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

        private void LeaveFeedbackButton_Click(object sender, EventArgs e)
        {
            this.db = new DataBase();
            string clients = richTextBox10.Text;
            string numberPhone = richTextBox3.Text;
            string feedbackOrSuggestions = richTextBox4.Text;

            MySqlConnection connection = db.getConnection();

            try
            {
                connection.Open();
                string checkClientQuery = "SELECT client_id FROM clients WHERE phoneNumber = @phoneNumber";

                using (MySqlCommand checkClientCommand = new MySqlCommand(checkClientQuery, connection))
                {
                    checkClientCommand.Parameters.AddWithValue("@phoneNumber", numberPhone);
                    object clientId = checkClientCommand.ExecuteScalar();

                    if (clientId != null) 
                    {
                        int clientIdInt = Convert.ToInt32(clientId);
                        string insertFeedbackQuery = "INSERT INTO feedbackandsuggestions(clients_id, clients, numberPhone, feedbackOrSuggestions) VALUES(@clients_id, @clients, @numberPhone, @feedbackOrSuggestions)";

                        using (MySqlCommand insertFeedbackCommand = new MySqlCommand(insertFeedbackQuery, connection))
                        {
                            insertFeedbackCommand.Parameters.AddWithValue("@clients_id", clientIdInt); 
                            insertFeedbackCommand.Parameters.AddWithValue("@clients", clients);
                            insertFeedbackCommand.Parameters.AddWithValue("@numberPhone", numberPhone);
                            insertFeedbackCommand.Parameters.AddWithValue("@feedbackOrSuggestions", feedbackOrSuggestions);
                            int rowsAffected = insertFeedbackCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Відгук додано");
                            }
                            else
                            {
                                MessageBox.Show("Невдалось додати.");
                            }
                        }
                    }
                    else
                    {
                        string insertFeedbackQuery = "INSERT INTO feedbackandsuggestions(clients, numberPhone, feedbackOrSuggestions) VALUES(@clients, @numberPhone, @feedbackOrSuggestions)";

                        using (MySqlCommand insertFeedbackCommand = new MySqlCommand(insertFeedbackQuery, connection))
                        {
                            
                            insertFeedbackCommand.Parameters.AddWithValue("@clients", clients);
                            insertFeedbackCommand.Parameters.AddWithValue("@numberPhone", numberPhone);
                            insertFeedbackCommand.Parameters.AddWithValue("@feedbackOrSuggestions", feedbackOrSuggestions);
                            int rowsAffected = insertFeedbackCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Відгук додано");
                            }
                            else
                            {
                                MessageBox.Show("Невдалось додати.");
                            }
                        }
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

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            FeedbackAndSuggestion form = new FeedbackAndSuggestion();
            form.Show();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
