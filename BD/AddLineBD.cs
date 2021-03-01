using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BD
{
    public partial class AddLineBD : Form
    {
        public AddLineBD()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = MySqlDB.GetDBConnection();
                conn.Open(); //Открытие соеденения с бд

                string sql = "INSERT INTO `Экзамены`(`Код студента`, `Код группы`, `Код преподавателя`, `Код дисциплины`, `Оценка`, `№ Билета`) VALUES " +
                    "( (SELECT `Код студента` FROM `Студенты` WHERE `ФИО` = '" + textBox1.Text + "'), " +
                    "(SELECT `Код группы` FROM `Группы` WHERE `Название группы` = '" + label6.Text + "'), " +
                    "(SELECT `Код преподавателя` FROM `Преподаватели` WHERE `ФИО` = '" + comboBox1.Text + "'), " +
                    "(SELECT `Код дисциплины` FROM `Дисциплины` WHERE `Название` = '" + comboBox2.Text + "'), " +
                    textBox2.Text + ", " +
                    textBox3.Text + ");";

                MySqlCommand command = new MySqlCommand(sql, conn);

                command.ExecuteScalar();

                conn.Close(); //Закрытие соеденения с бд
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void add_Studend(string name, string group)
        {
            textBox1.Text = name;
            label6.Text = group;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            Students StudentForm = new Students();
            StudentForm.Show();
            StudentForm.btn3unlock();
            Close();
        }

        private void AddLineBD_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlDataReader reader;
                MySqlConnection conn = MySqlDB.GetDBConnection();

                comboBox1.Items.Clear();

                conn.Open(); //Открытие соеденения с бд

                //Обновление таблицы преподавателей
                string sql = "SELECT `ФИО` FROM `Преподаватели`";
                reader = MySqlDB.GetDBTable(sql, conn);
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["ФИО"]);
                }
                reader.Close();

                conn.Close(); //Закрытие соеденения с бд
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox2.Items.Clear();

                MySqlConnection conn = MySqlDB.GetDBConnection();
                conn.Open(); //Открытие соеденения с бд

                string sql = "SELECT `Название` FROM `Дисциплины` WHERE `Код дисциплины` = (SELECT `Преподаватели`.`Код дисциплины` FROM `Преподаватели` WHERE `Преподаватели`.`ФИО` = '" + comboBox1.Text + "')";
                MySqlDataReader reader = MySqlDB.GetDBTable(sql, conn);
                while (reader.Read())
                {
                    comboBox2.Items.Add(Convert.ToString(reader["Название"]));
                }
                reader.Close();

                conn.Close(); //Закрытие соеденения с бд
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
