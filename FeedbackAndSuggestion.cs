using MySql.Data.MySqlClient;
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
    public partial class FeedbackAndSuggestion : Form
    {
        private DataBase db;
        public List<Item> items { get; set; } = new List<Item>();
        public FeedbackAndSuggestion()
        {
            InitializeComponent();
            db = new DataBase();
            Feedback();
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
        public void Feedback()
        {
            DataTable dataTable = new DataTable();
            using (MySqlConnection connection = db.getConnection())
            {
                try
                {
                    connection.Open();
                    string feedback = "SELECT clients, numberPhone, feedbackOrSuggestions " +
                                   "FROM feedbackandsuggestions";

                    using (MySqlCommand command = new MySqlCommand(feedback, connection))
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
                    string clients = row.Cells["clients"].Value.ToString();
                    string numberPhone = row.Cells["numberPhone"].Value.ToString();
                    string feedbackOrSuggestions = row.Cells["feedbackOrSuggestions"].Value.ToString();
                   
                    this.items.Add(new Item
                    {
                        Clients = clients,
                        NumberPhone = numberPhone,
                        FeedbackOrSuggestions = feedbackOrSuggestions, 
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
            public string Clients { get; set; }
            public string NumberPhone { get; set; }
            public string FeedbackOrSuggestions { get; set; }
            
        }

        private void LeaveFeedbackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            InsertFeedback form = new InsertFeedback();
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
