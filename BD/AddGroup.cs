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

namespace BD
{
    public partial class AddGroup : Form
    {
        public AddGroup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = MySqlDB.GetDBConnection();
                conn.Open(); //Открытие соеденения с бд

                string sql = "SELECT `Код группы` FROM `Группы` WHERE `Название группы` = '" + textBox1.Text + "';";

                MySqlCommand command = new MySqlCommand(sql, conn);
                if (command.ExecuteScalar() == null)
                {
                    sql = "INSERT INTO `Группы`(`Название группы`) VALUES ('" + textBox1.Text + "')";

                    MySqlCommand command1 = new MySqlCommand(sql, conn);
                    command1.ExecuteScalar();

                    MessageBox.Show("Группа добавлена");

                    conn.Close(); //Закрытие соеденения с бд
                    Close();
                }
                else
                {
                    MessageBox.Show("Такая группа уже существует!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
