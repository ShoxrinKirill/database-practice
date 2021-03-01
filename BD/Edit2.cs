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
    public partial class Edit2 : Form
    {
        public Edit2()
        {
            InitializeComponent();
        }

        private void Edit2_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlDataReader reader;
                MySqlConnection conn = MySqlDB.GetDBConnection();

                comboBox2.Items.Clear();

                conn.Open(); //Открытие соеденения с бд

                //Обновление таблицы преподавателей
                string sql = "SELECT `Название` FROM `Дисциплины`";
                reader = MySqlDB.GetDBTable(sql, conn);
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader["Название"]);
                }
                reader.Close();

                conn.Close(); //Закрытие соеденения с бд
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ld(string id, string name, string predmet, string img)
        {
            textBox1.Text = name;
            comboBox2.Text = predmet;
            pictureBox1.ImageLocation = img;
            this.id = id;
        }

        public string img = null;
        public string id = null;

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                openFileDialog1.InitialDirectory = wanted_path + "\\images";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    pictureBox1.ImageLocation = openFileDialog1.FileName;
                    this.img = Path.GetFileNameWithoutExtension(openFileDialog1.FileName) + ".jpg";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataReader reader;
                MySqlConnection conn = MySqlDB.GetDBConnection();

                comboBox2.Items.Clear();

                conn.Open(); //Открытие соеденения с бд

                string sql = "UPDATE `Преподаватели` SET `Код дисциплины` = (SELECT `Код дисциплины` FROM `Дисциплины` WHERE `Название` = '" + comboBox2.Text + "'), image = '" + this.img + "', `ФИО` = '" + textBox1.Text + "' WHERE `Преподаватели`.`Код преподавателя` = " + this.id;

                MySqlCommand command = new MySqlCommand(sql, conn);

                command.ExecuteScalar();

                conn.Close(); //Закрытие соеденения с бд

                MessageBox.Show("Запись изменена!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
