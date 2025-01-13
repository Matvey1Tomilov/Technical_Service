using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Technical_Service
{
    public partial class Добавить : Form
    {
        public string connectionString = "Host=localhost;Port=5432;Database=арм;Username=postgres;Password=12345";

        public bool check_exists_button = false;
        //public bool check_load_column = true;
        public string[] menu_column;
        System.Windows.Forms.TextBox[,] tb;
        public Добавить()
        {
            InitializeComponent();
            string[] menu_table = { "Запланированные задачи", "Сотрудники", "Заявки на обслуживание", "Устройства", "История обслуживания" };//меню
            listBox1.Items.AddRange(menu_table);

        }
        private string[] Select_Table(string table_name)
        {
                List<string>  menu_attribute =new List<string>() { };
                using (var cn = new NpgsqlConnection(connectionString))
                {
                    cn.Open();
                    var sql = "select s.column_name from information_schema.columns s where s.table_name=@table_name\r\n";//нужно получить id объекта выше
                    var cmd = new NpgsqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@table_name", table_name);
                    var dr=cmd.ExecuteReader();
                int i = 0; 
                while (dr.Read())
                {
                    menu_attribute.Add( dr["column_name"].ToString());
                    i++;
                }
                };
            string[]result= new string[menu_attribute.Count];
            result=menu_attribute.ToArray();
          /*  for(int j =0;j<result.Length;j++)
                result[j] = menu_attribute[j];*/
            return result;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //listBox2.Items.Clear();
            string menu_table_item;
            menu_table_item = listBox1.SelectedItem.ToString();
            if (menu_table_item == "Запланированные задачи")
                menu_table_item = "Запланированные_задачи";
            if (menu_table_item == "История обслуживания")
                menu_table_item = "История_обслуживания";
            if (menu_table_item == "Заявки на обслуживание")
                menu_table_item = "Заявки_на_обслуживание";
            
             menu_column = Select_Table(menu_table_item);
            //listBox2.Items.AddRange(menu_column);
                       

            if(check_exists_button)
            { 
            for (int i = 0; i < tb.GetLength(0); i++)
                {
                    for (int j = 0; j < tb.GetLength(1); j++)
                    {
                        Controls.Remove(tb[i, j]);
                        tb[i, j].Dispose();                        
                    }
                }
            }
            tb = new System.Windows.Forms.TextBox[menu_column.Length, 1];
            for (int i = 0; i < tb.GetLength(0); i++)
            {
                for (int j = 0; j < tb.GetLength(1); j++)
                {
                    tb[i, j] = new System.Windows.Forms.TextBox();
                    tb[i, j].Location = new Point(250 + j * 100, 175 + i * 30);
                    tb[i, j].Size = new Size(100, 23);
                    tb[i,j].Text= menu_column[i];
                    Controls.Add(tb[i, j]);
                }
            }
            check_exists_button = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var cn = new NpgsqlConnection(connectionString))
            {
                cn.Open();
                listBox1.SelectedItem.ToString();
                var sql = "";
                NpgsqlCommand cmd;
                if (listBox1.SelectedItem.ToString()=="Запланированные задачи")
                {
                    sql = @"insert into Запланированные_задачи(ID_задачи, ID_сотрудника, Описание_задачи, Статус, Дата_и_время_начала, Дата_и_время_окончания)
                                values (
                                (select count(*)+1 from Запланированные_задачи),
                                (@menu_column1),
                                (@menu_column2),
                                (@menu_column3),
                                (@menu_column4),
                                (@menu_column5)
                                );";
                    cmd = new NpgsqlCommand(sql, cn);

                    string format = "yyyy-MM-dd HH:mm:ss";
                    DateTime dateTime1 = DateTime.ParseExact(tb[2, 0].Text, format, CultureInfo.InvariantCulture);
                    long timestamp1 = (long)(dateTime1 - new DateTime(1970, 1, 1)).TotalMilliseconds;

                    DateTime dateTime2 = DateTime.ParseExact(tb[3, 0].Text, format, CultureInfo.InvariantCulture);
                    long timestamp2 = (long)(dateTime2 - new DateTime(1970, 1, 1)).TotalMilliseconds;

                    cmd.Parameters.AddWithValue("@menu_column1", Int32.Parse(tb[1, 0].Text));
                    cmd.Parameters.AddWithValue("@menu_column2", timestamp1);//время
                    cmd.Parameters.AddWithValue("@menu_column3", timestamp2);
                    cmd.Parameters.AddWithValue("@menu_column4", tb[4, 0].Text);
                    cmd.Parameters.AddWithValue("@menu_column5", tb[5, 0].Text);
                    cmd.ExecuteNonQuery();
                }
                if (tb.GetLength(0) == 5)
                {
                    sql = @"insert into " + listBox1.SelectedItem.ToString() + "(" + menu_column[0] + ", " + menu_column[1] + ", " + menu_column[2] + "," + menu_column[3] + "," + menu_column[4] + ") " +
                        "values(@menu_column0, @menu_column1, @menu_column2, @menu_column3, @menu_column4)";
                    cmd = new NpgsqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@menu_column0", tb[0, 0].Text);
                    cmd.Parameters.AddWithValue("@menu_column1", tb[1, 0].Text);
                    cmd.Parameters.AddWithValue("@menu_column2", tb[2, 0].Text);
                    cmd.Parameters.AddWithValue("@menu_column3", tb[3, 0].Text);
                    cmd.Parameters.AddWithValue("@menu_column4", tb[4, 0].Text);
                    cmd.ExecuteNonQuery();
                }
                else if (tb.GetLength(0) == 6)
                {
                    sql = @"insert into " + listBox1.SelectedItem.ToString() + "(" + tb[0, 0].Text + ", " + tb[1, 0].Text + ", " + tb[2, 0].Text + "," + tb[3, 0].Text + "," + tb[4, 0].Text + "," + tb[5, 0].Text + ") " +
                        "values(@menu_column0, @menu_column1, @menu_column2, @menu_column3, @menu_column4, @menu_column5)";
                    cmd = new NpgsqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@menu_column0", @menu_column[0]);
                    cmd.Parameters.AddWithValue("@menu_column1", @menu_column[1]);
                    cmd.Parameters.AddWithValue("@menu_column2", @menu_column[2]);
                    cmd.Parameters.AddWithValue("@menu_column3", @menu_column[3]);
                    cmd.Parameters.AddWithValue("@menu_column4", @menu_column[4]);
                    cmd.Parameters.AddWithValue("@menu_column5", @menu_column[5]);
                    cmd.ExecuteNonQuery();
                }
            };
        }
    }
}
