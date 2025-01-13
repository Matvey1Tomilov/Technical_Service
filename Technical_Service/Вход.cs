using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Technical_Service
{
    public partial class Вход : Form
    {
        string connectionString = "Host=localhost;Port=5432;Database=арм;Username=postgres;Password=12345";
        public class Autorization { 
        public static bool check_in_autoriz=false;        
        }
        
        public static string login="Логин";
        string password="Пароль";
        public Вход()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//войти
        {
            using (var cn = new NpgsqlConnection(connectionString))
            {
                cn.Open();
                var sql = "select count(*) from Пользователи\r\nwhere Логин=@username and Пароль=@password";
                var command = new NpgsqlCommand(sql, cn);
                command.Parameters.AddWithValue("@username", login);
                command.Parameters.AddWithValue("@password", password);


                /*int? result = command.ExecuteScalar() as int?;
                int count = result ?? 0;*/
                string count= command.ExecuteScalar().ToString(); 
                if (count == '1'.ToString())
                {
                    Autorization.check_in_autoriz = true;  
                    this.Hide();
                    Form1 form1 = new Form1();
                    form1.Show();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
            }
        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            login = textBox1.Text;
        }

       public void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            password=richTextBox1.Text;
        }

        private void Вход_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
