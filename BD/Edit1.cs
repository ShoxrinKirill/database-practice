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
    public partial class Edit1 : Form
    {
        public Edit1()
        {
            InitializeComponent();
        }

        string id;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataReader reader;
                MySqlConnection conn = MySqlDB.GetDBConnection();

                comboBox1.Items.Clear();

                conn.Open(); //Открытие соеденения с бд

                string sql = "UPDATE `Экзамены` SET `Код преподавателя` = (SELECT `Код преподавателя` FROM `Преподаватели` WHERE `Преподаватели`.`ФИО` = '" + comboBox1.Text + "'), " +
                    "`Код дисциплины` = (SELECT `Дисциплины`.`Код дисциплины` FROM `Дисциплины` WHERE `Дисциплины`.`Название` = '" + comboBox2.Text + "'), " +
                    "`№ Билета` = " + textBox5.Text + ", `Оценка` = " + textBox6.Text + " WHERE `Экзамены`.`Код экзамена` = " + this.id;

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

        private void Edit1_Load(object sender, EventArgs e)
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

        public void ld(string id, string name, string group, string name_teacher, string predmet, string bilet, string ocenka)
        {
            label1.Text = name;
            label2.Text = group;
            comboBox1.Text = name_teacher;
            comboBox2.Text = predmet;
            textBox5.Text = bilet;
            textBox6.Text = ocenka;
            this.id = id;
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
