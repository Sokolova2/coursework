using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace coursework
{
    internal class DataBase
    {
        private MySqlConnection connection;

        public DataBase()
        {
            connection = new MySqlConnection("server=127.0.0.1; port=3306; username=root; password=admin; database=restaurantmanagement");
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }
        public void closeConnection()
        {
            this.connection.Close();
        }
    }
}
