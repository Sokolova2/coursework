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
    public partial class HomePage : Form
    {
        private List<Item> items;
        public HomePage()
        {
            InitializeComponent();
            this.items = new List<Item>();
        }
        public HomePage(List<Item> items)
        {
            InitializeComponent();
            this.items = items;

        }

        public Item Item
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
        private void MenuButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu form1 = new Menu();
            form1.Show();
        }
        private void OrderButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            InsertOrders form = new InsertOrders(this.items);
            form.Show();
        }
        private void LoyaltyCardButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoyaltyCard form = new LoyaltyCard();
            form.Show();
        }
        private void EmployeeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employee form = new Employee();
            form.Show();
        }
        private void SalaryButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Salary form = new Salary();
            form.Show();
        }
        private void FeedbackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            FeedbackAndSuggestion form = new FeedbackAndSuggestion();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reports reports = new Reports();
            reports.Show();
        }
    }
}
