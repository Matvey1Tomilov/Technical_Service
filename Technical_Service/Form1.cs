using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Technical_Service
{
    public partial class Form1 : Form
    {
        string connectionString = "Host=localhost;Port=5432;Database=арм;Username=postgres;Password=12345";
        List<string> col_name = new List<string>();
        
        public Form1()
        {
            InitializeComponent();
              string current_role = "";
             //разворачиваем форму на весь экран
             WindowState = FormWindowState.Maximized;
            //Вход вход=new Вход();
            
            using (var cn = new NpgsqlConnection(connectionString))
            {
                cn.Open();
                var sql = $"select Статус from Пользователи where Логин='{ Вход.login}'";
                var command = new NpgsqlCommand(sql, cn);
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    current_role = dr[0].ToString();
                }
            }
           // MessageBox.Show(вход.login);
            ///MessageBox.Show(current_role);
            if(current_role=="Сотрудник")
            {     
                button1.Visible = false;
                button2.Visible = false;
                button4.Visible = false;
            }
        }


        private void нестандартныйЗапросToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Нестандартный_запрос запрос = new Нестандартный_запрос();
            запрос.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Close();
           Application.Exit();
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)//загрузка datagridview
        {
            LoadColumn(listBox1.SelectedItem.ToString());
            try
            {
                if (dataGridView1.Columns.Count != 0)
                {
                    dataGridView1.Columns.Clear();
                    dataGridView1.Rows.Clear(); 
                }
                using (var cn = new NpgsqlConnection(connectionString))
                {
                    cn.Open();
                    var sql = $"SELECT *FROM \"{listBox1.SelectedItem.ToString()}\"";
                    var command = new NpgsqlCommand(sql, cn);
                    var dr = command.ExecuteReader();
                    List<string[]> data = new List<string[]>();
                    while (dr.Read())
                    {
                        data.Add(new string[col_name.Count]);
                        for (int i = 0; i < col_name.Count; i++)
                        {
                                data[data.Count - 1][i] = dr[i].ToString();
                        }
                    }
                    for (int i = 0; i < col_name.Count; i++)
                    {
                        dataGridView1.Columns.Add($"{col_name[i]}", $"{col_name[i]}");
                    }
                    foreach (string[] s in data)
                    {
                            dataGridView1.Rows.Add(s);
                    }
                    if(dataGridView1.Columns.Contains("id_сотрудника"))
                        dataGridView1.Columns["id_сотрудника"].ReadOnly = true;
                    if (dataGridView1.Columns.Contains("id_устройства"))
                        dataGridView1.Columns["id_устройства"].ReadOnly = true;
                    if (dataGridView1.Columns.Contains("id_задачи"))
                        dataGridView1.Columns["id_задачи"].ReadOnly = true;
                    if (dataGridView1.Columns.Contains("id_записи"))
                        dataGridView1.Columns["id_записи"].ReadOnly = true;
                    if (dataGridView1.Columns.Contains("id_заявки"))
                        dataGridView1.Columns["id_заявки"].ReadOnly = true;

                    /* string[] columnsToRemove = { "id_сотрудника", "id_устройства", "id_задачи", "id_записи", "id_заявки" };//удаление id
                     foreach (string columnName in columnsToRemove)
                     {
                         DataGridViewColumn column = dataGridView1.Columns[columnName];
                         if (column != null)
                         {
                             dataGridView1.Columns.Remove(column);
                         }
                         col_name.Remove(columnName);
                     }*/

                }
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();

                if (listBox1.SelectedItem.ToString() == "Запланированные_задачи")
                {
                    using (var cn = new NpgsqlConnection(connectionString))
                    {
                        cn.Open();
                        var sql = "select *from Сотрудники";
                        var command = new NpgsqlCommand(sql, cn);
                        var dr = command.ExecuteReader();
                        while (dr.Read())
                        {
                            listBox2.Items.Add(dr[0].ToString() + " " + dr[1].ToString() + " " + dr[2].ToString() + " " + dr[3].ToString() + " " + dr[4].ToString());//загружаем данные для дальнейшего выбора id
                        }
                    }
                }
                else if (listBox1.SelectedItem.ToString() == "История_обслуживания")
                {
                    using (var cn = new NpgsqlConnection(connectionString))
                    {
                        cn.Open();
                        var sql = "select * from Сотрудники,Заявки_на_обслуживание,Устройства where Сотрудники.id_сотрудника=Заявки_на_обслуживание.id_сотрудника and " +
                            "Заявки_на_обслуживание.id_устройства=Устройства.id_устройства";
                        var command = new NpgsqlCommand(sql, cn);
                        var dr = command.ExecuteReader();
                        while (dr.Read())
                        {
                            listBox2.Items.Add(dr[0].ToString() + " " + dr[1].ToString() + " " + dr[2].ToString() + " " + dr[3].ToString() +
                                " " + dr[4].ToString());//загружаем сотрудников
                            listBox3.Items.Add(dr[5].ToString() + " " + dr[6].ToString() + " " + dr[7].ToString() + " " + dr[8].ToString() +
                                " " + dr[9].ToString());//загружаем заявки
                            listBox4.Items.Add(dr[10].ToString() + " " + dr[11].ToString() + " " + dr[12].ToString() + " " + dr[13].ToString() +
                              " " + dr[14].ToString());//загружаем устройства
                        }
                    }
                } else if (listBox1.SelectedItem.ToString()=="Заявки_на_обслуживание")
                {
                    using (var cn = new NpgsqlConnection(connectionString))
                    {
                        cn.Open();
                        var sql = "select Сотрудники.id_сотрудника,Имя, Фамилия, Должность,Отдел,Устройства.id_устройства,Название, Тип, Серийный_номер, Дата_приобретения" +
                            " from Сотрудники,Устройства,Заявки_на_обслуживание" +
                            " where Сотрудники.id_сотрудника=Заявки_на_обслуживание.id_сотрудника and " +
                            "Заявки_на_обслуживание.id_устройства=Устройства.id_устройства";
                        var command = new NpgsqlCommand(sql, cn);
                        var dr = command.ExecuteReader();
                        while (dr.Read())
                        {
                            listBox2.Items.Add(dr[0].ToString() + " " + dr[1].ToString() + " " + dr[2].ToString() + " " + dr[3].ToString() +
                                " " + dr[4].ToString());//загружаем сотрудников
                            listBox4.Items.Add(dr[5].ToString() + " " + dr[6].ToString() + " " + dr[7].ToString() + " " + dr[8].ToString() +
                                " " + dr[9].ToString());//загружаем заявки
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void Form1_Load(object sender, EventArgs e)///загрузк listbox с нанзваниями таблиц
        {
            using (var cn = new NpgsqlConnection(connectionString))
            {
                cn.Open();
                var sql = "select distinct Table_name from meta";
                var command = new NpgsqlCommand(sql, cn);
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    listBox1.Items.Add(dr[0]);//загружаем названия таблиц
                }
            }
        }
        public void LoadColumn(string tab_name) 
        {
            col_name.Clear();
            using (var cn = new NpgsqlConnection(connectionString))
            {
                cn.Open();
               /// var sql = "select distinct column_name from meta where table_name=@tab_name";
                var sql1=@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.columns WHERE TABLE_NAME =@tab_name AND TABLE_SCHEMA = 'public';";
                
                var command = new NpgsqlCommand(sql1, cn);
                command.Parameters.AddWithValue("@tab_name",tab_name);
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    //if(!dr[0].ToString().Contains("id"))
                        col_name.Add(dr[0].ToString());//загружаем названия столбцов
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)//добавление
        {
            if (listBox1.SelectedItem.ToString() == "Запланированные_задачи" && (listBox2.SelectedItem == null))
            {
                MessageBox.Show("Выберите соответствущие поля слева");
            }
            else if (listBox1.SelectedItem.ToString() == "Заявки_на_обслуживание" && (listBox2.SelectedItem == null || listBox4.SelectedItem == null))
            {
                MessageBox.Show("Выберите соответствущие поля слева");
            }
            else if (listBox1.SelectedItem.ToString() == "История_обслуживания" &&
                (listBox2.SelectedItem == null || listBox3.SelectedItem == null || listBox4.SelectedItem == null))
            {
                MessageBox.Show("Выберите соответствущие поля слева");
            }
            else
            {
                string s_sotrudniki;
                string s_ustroystva;
                string s_zaiavki;
                if (listBox1.SelectedItem.ToString() == "Запланированные_задачи")
                {
                    s_sotrudniki = listBox2.SelectedItem.ToString();
                    s_sotrudniki = s_sotrudniki.Substring(0, 1);

                    int chislo_end = 0;                  
                    for (int id = 0; id < dataGridView1.Rows.Count - 1; id++)
                    {
                        chislo_end = Int32.Parse(dataGridView1.Rows[id].Cells[0].Value.ToString());
                    }
                    chislo_end += 1;
                    string[] row_end = new string[] { chislo_end.ToString(), s_sotrudniki, "", "", "" };
                    object[] rows = new object[] { row_end };

                    foreach (string[] rowArray in rows)
                    {
                        dataGridView1.Rows.Add(rowArray);
                    }
                }
                else if (listBox1.SelectedItem.ToString() == "Заявки_на_обслуживание")
                {
                    s_sotrudniki = listBox2.SelectedItem.ToString();
                    s_sotrudniki = s_sotrudniki.Substring(0, 1);
                    s_ustroystva = listBox4.SelectedItem.ToString();
                    s_ustroystva = s_ustroystva.Substring(0, 1);
                    int chislo_end = 0;
                    for (int id = 0; id < dataGridView1.Rows.Count-1; id++)
                    {
                        chislo_end = Int32.Parse(dataGridView1.Rows[id].Cells[0].Value.ToString());
                    }
                    chislo_end += 1;
                    string[] row_end = new string[] { chislo_end.ToString(), s_sotrudniki, s_ustroystva, "", "" };
                    object[] rows = new object[] { row_end };
                    foreach (string[] rowArray in rows)
                    {
                        dataGridView1.Rows.Add(rowArray);
                    }
                }
                else if (listBox1.SelectedItem.ToString() == "История_обслуживания")
                {
                    s_sotrudniki = listBox2.SelectedItem.ToString();
                    s_sotrudniki = s_sotrudniki.Substring(0, 1);
                    s_ustroystva = listBox4.SelectedItem.ToString();
                    s_ustroystva = s_ustroystva.Substring(0, 1);
                    s_zaiavki = listBox3.SelectedItem.ToString();
                    s_zaiavki = s_zaiavki.Substring(0, 1);

                    int chislo_end = 0;
                    for (int id = 0; id < dataGridView1.Rows.Count-1; id++)
                    {
                        chislo_end = Int32.Parse(dataGridView1.Rows[id].Cells[0].Value.ToString());
                    }
                    chislo_end += 1;

                    string[] row_end = new string[] { chislo_end.ToString(), s_zaiavki, s_sotrudniki, s_ustroystva, "", "" };
                    object[] rows = new object[] { row_end };
                    foreach (string[] rowArray in rows)
                    {
                        dataGridView1.Rows.Add(rowArray);
                    }
                }
                else {
                    int chislo_end=0;
                    dataGridView1.Rows.Add();
                    for (int id = 0; id < dataGridView1.Rows.Count; id++)
                    {
                        if (dataGridView1.Rows[id].Cells[0].Value == null)
                        {
                            dataGridView1.Rows[id].Cells[0].Value = chislo_end + 1;
                            break;
                        }
                        chislo_end = Int32.Parse(dataGridView1.Rows[id].Cells[0].Value.ToString());
                    }
                }

                // Получаем новую строку из DataGridView
                DataGridViewRow newRow = dataGridView1.Rows[dataGridView1.RowCount - 2];

                // Формируем SQL-запрос для вставки данных
                string sql = $"INSERT INTO \"{listBox1.SelectedItem.ToString()}\" (";

                for (int i = 0; i < col_name.Count; i++)
                {
                    if (i != col_name.Count - 1)
                        sql += $"\"{col_name[i]}\", ";
                    else
                        sql += $"\"{col_name[i]}\") ";
                }

                sql += "VALUES (";

                for (int i = 0; i < newRow.Cells.Count; i++)
                {
                    if (i != newRow.Cells.Count - 1)
                    {
                        if (newRow.Cells[i].Value == null|| newRow.Cells[i].Value.ToString() == "")
                            sql += "null,";
                        else
                            sql += $"'{newRow.Cells[i].Value.ToString()}', ";
                    }
                    else 
                    {
                        if (newRow.Cells[i].Value == null || newRow.Cells[i].Value.ToString() == "")
                            sql += "null)";
                        else
                            sql += $"'{newRow.Cells[i].Value.ToString()}') ";
                    }
                }
               // MessageBox.Show(sql);
                // Выполняем SQL-запрос для добавления данных
                using (var cn = new NpgsqlConnection(connectionString))
                {
                    cn.Open();
                    var command = new NpgsqlCommand(sql, cn);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)//удаление
        {
            foreach(DataGridViewRow row in dataGridView1.SelectedRows)
    {
                // Получаем значение первичного ключа для удаления данных
                string primaryKeyValue = row.Cells[0].Value.ToString();

                // Формируем SQL-запрос для удаления данных
                string sql = $"DELETE FROM \"{listBox1.SelectedItem.ToString()}\" WHERE \"{col_name[0].ToString()}\"= '{primaryKeyValue}'";
               // MessageBox.Show(sql);
                // Выполняем SQL-запрос для удаления данных
                using (var cn = new NpgsqlConnection(connectionString))
                {
                    cn.Open();
                    var command = new NpgsqlCommand(sql, cn);
                    command.ExecuteNonQuery();
                }

                // Удаляем строку из DataGridView
                dataGridView1.Rows.Remove(row);
            }
        }

        private void button4_Click(object sender, EventArgs e)//сохранить
        {
            // Получаем измененные строки DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                // Получаем значение первичного ключа измененной строки
                string primaryKeyValue = row.Cells[0].Value.ToString();

                // Формируем SQL-запрос для обновления данных
                string sql = $"UPDATE \"{listBox1.SelectedItem.ToString()}\" SET ";

                for (int i = 1; i < row.Cells.Count; i++)
                {
                    if (i != row.Cells.Count - 1)
                        sql += $"\"{col_name[i]}\" = '{row.Cells[i].Value.ToString()}', ";
                    else
                        sql += $"\"{col_name[i]}\" = '{row.Cells[i].Value.ToString()}' ";
                }
                if(listBox1.SelectedItem.ToString()=="Запланированные_задачи")
                    sql += $"WHERE id_задачи = '{primaryKeyValue}'";
                else if(listBox1.SelectedItem.ToString() == "Устройства")
                    sql += $"WHERE id_устройства = '{primaryKeyValue}'";
                else if (listBox1.SelectedItem.ToString() == "Сотрудники")
                    sql += $"WHERE id_сотрудника = '{primaryKeyValue}'";
                else if (listBox1.SelectedItem.ToString() == "История_обслуживания")
                    sql += $"WHERE id_записи = '{primaryKeyValue}'";
                else if (listBox1.SelectedItem.ToString() == "Заявки_на_обслуживание")
                    sql += $"WHERE id_заявки = '{primaryKeyValue}'";
                ///MessageBox.Show(sql);
                // Выполняем SQL-запрос для обновления данных
                using (var cn = new NpgsqlConnection(connectionString))
                {
                    cn.Open();
                    var command = new NpgsqlCommand(sql, cn);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
