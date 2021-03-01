using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD
{
    public partial class AddTeacher : Form
    {
        public AddTeacher()
        {
            InitializeComponent();
        }

        string img = null;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                openFileDialog1.InitialDirectory = wanted_path + "\\images";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    pictureBox1.ImageLocation = openFileDialog1.FileName;
                    this.img = Path.GetFileNameWithoutExtension(openFileDialog1.FileName) + ".jpg";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = MySqlDB.GetDBConnection();
            string name = textBox1.Text;
            string predmet = textBox2.Text;

            conn.Open(); //Открытие соеденения с бд
            string sql = "SELECT `Код дисциплины` FROM `Дисциплины` WHERE `Название` = '" + predmet + "';";

            MySqlCommand command = new MySqlCommand(sql, conn);
            if (command.ExecuteScalar() == null)
            {
                sql = "INSERT INTO `Дисциплины`(`Название`) VALUES ('" + predmet + "')";

                MySqlCommand command1 = new MySqlCommand(sql, conn);
                command1.ExecuteScalar();

                sql = "INSERT INTO `Преподаватели`(`ФИО`, `Код дисциплины`, `image`) VALUES " +
                    "('" + name + "', (SELECT `Код дисциплины` FROM `Дисциплины` WHERE `Название` = '" + predmet + "'), '" + this.img + "')";

                MySqlCommand command2 = new MySqlCommand(sql, conn);
                command2.ExecuteScalar();
            }
            else
            {
                sql = "INSERT INTO `Преподаватели`(`ФИО`, `Код дисциплины`, `image`) VALUES " +
                    "('" + name + "', (SELECT `Код дисциплины` FROM `Дисциплины` WHERE `Название` = '" + predmet + "'), '" + this.img + "')";

                MySqlCommand command1 = new MySqlCommand(sql, conn);
                command1.ExecuteScalar();
            }
            conn.Close(); //Закрытие соеденения с бд

            MessageBox.Show("Учитель добавлен");
            Close();
        }

        private void AddTeacher_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = MySqlDB.GetDBConnection();
            MySqlDataReader reader;

            conn.Open(); //Открытие соеденения с бд

            string sql = "SELECT `Код дисциплины`, `Название` FROM `Дисциплины` WHERE `Название` LIKE '" + textBox2.Text + "%';";
            reader = MySqlDB.GetDBTable(sql, conn);
            while (reader.Read())
            {
                textBox2.AutoCompleteCustomSource.Add(reader["Название"].ToString());
            }
            reader.Close();

            conn.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
