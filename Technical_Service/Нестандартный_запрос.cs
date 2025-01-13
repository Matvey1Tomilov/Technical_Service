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
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;
using static Technical_Service.Вход;

namespace Technical_Service
{
    public partial class Нестандартный_запрос : Form
    {
        string connectionString = "Host=localhost;Port=5432;Database=арм;Username=postgres;Password=12345";

        List<string> sql_table=new List<string>();
        List<string> list_relations=new List<string>();//список со связями  

        public string sql = "";
        public string sql_where = "";
        public string sql_orderby = "";
        public string sql_orderby_end = "";
        string sql_where_relation = "";
       // string sql_dop_table = "";

        bool check_where = true;
        bool check_orderby = true;  
        public Нестандартный_запрос()
        {
            InitializeComponent();
        }
        //ВЫБОР ПОЛЕЙ
        private void Нестандартный_запрос_Load(object sender, EventArgs e)
        {
            using (var cn = new NpgsqlConnection(connectionString))
            {
                cn.Open();
                var sql = "select *from Meta";
                var command = new NpgsqlCommand(sql, cn);
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    listBox1.Items.Add(dr[0]);//загружаем названия столбцов
                    comboBox1.Items.Add(dr[0]);//загружаем названия столбцов
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)//>
        {
            try
            {
                if (!checkedListBox1.Items.Contains(listBox1.SelectedItem.ToString()))
                {
                    checkedListBox1.Items.Add(listBox1.SelectedItem.ToString());
                    listBox2.Items.Add(listBox1.SelectedItem.ToString());
                    GetTable_Name(listBox1.SelectedItem.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Не выбрано имя поля");
            }
        }

        private void button2_Click(object sender, EventArgs e)//<
        {
            try
            {
                sql_table.Remove(checkedListBox1.SelectedItem.ToString());
                listBox2.Items.Remove(checkedListBox1.SelectedItem.ToString());
                checkedListBox2.Items.Remove(checkedListBox1.SelectedItem.ToString());
                checkedListBox1.Items.Remove(checkedListBox1.SelectedItem.ToString()); 
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)//>>
        {

            foreach(var item in listBox1.Items)
            {
                if (!checkedListBox1.Items.Contains(item.ToString())) 
                { 
                    checkedListBox1.Items.Add(item.ToString());
                    listBox2.Items.Add(item.ToString());
                }
                if (!sql_table.Contains(item.ToString()))
                { 
                    GetTable_Name(item.ToString()); 
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)//<<
        {
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            listBox2.Items.Clear();
            sql_table.Clear();
            list_relations.Clear();
        }
        public string Add_SQL_Table_Name() 
        {
            string result="";
            try { 
                
                foreach (var item in sql_table)
                {
                    result += item.ToString()+" ,";
                }
                int s_end = result.Length - 1;
                result = result.Remove(s_end);
                
            }catch (Exception ex) { MessageBox.Show(ex.Message); }
            return result;
        }
        public void GetTable_Name(string column)//получаем название таблицы исходя из колонки
        {
            string result = "";
            try
            {
                using (var cn = new NpgsqlConnection(connectionString))
                {
                    cn.Open();
                    var sql1 = @"select table_name from Meta where column_name=@column";
                    var command = new NpgsqlCommand(sql1, cn);
                    command.Parameters.AddWithValue("@column", column);
                    var dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        result = dr[0].ToString();
                    }
                }
                if (!sql_table.Contains(result))
                    sql_table.Add(result);
            }
            catch (Exception ex)
            { 
            MessageBox.Show(ex.Message);    
            }
        }
        public string Get_Table_Relations(string table_name1,string table_name2)//получить строку со связями
        {
            string result = "";
            try
            {
                using (var cn = new NpgsqlConnection(connectionString))
                {
                    cn.Open();
                    var sql1 = @"select* from Связи where (table_name1=@table_name1 and table_name2=@table_name2) or 
                                                            (table_name1=@table_name2 and table_name2=@table_name1)";
                    var command = new NpgsqlCommand(sql1, cn);
                    command.Parameters.AddWithValue("@table_name1", table_name1);
                    command.Parameters.AddWithValue("@table_name2", table_name2);
                    var dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr[2].ToString() != "-")
                        {
                            if (!list_relations.Contains(dr[2].ToString()))
                                list_relations.Add(dr[2].ToString());
                        }
                        else if (dr[2].ToString() == "-")
                        {
                            string temp = Get_Table_Relations(table_name1, dr[3].ToString()) + " AND " + Get_Table_Relations(dr[3].ToString(), table_name2);
                            if (temp != " AND ")
                                result += temp;
                            if (!sql_table.Contains(dr[3].ToString()))
                                sql_table.Add(dr[3].ToString());

                            if (!list_relations.Contains(Get_Table_Relations(table_name1, dr[3].ToString())) && Get_Table_Relations(table_name1, dr[3].ToString()) != "")
                                list_relations.Add(Get_Table_Relations(table_name1, dr[3].ToString()));

                            if (!list_relations.Contains(Get_Table_Relations(dr[3].ToString(), table_name2)) && Get_Table_Relations(dr[3].ToString(), table_name2) != "")
                                list_relations.Add(Get_Table_Relations(dr[3].ToString(), table_name2));

                            // sql_where_relation_end = result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           // sql_where_relation += result;
            return result;
        }
        private void button5_Click(object sender, EventArgs e)//просмотр sql
        {
            string column="";
            string table = "";
            try
            {
                foreach (var item in checkedListBox1.Items)//добавление в строку колонки 
                {
                    column += item.ToString() + " ,";
                }
                int s_end = column.Length - 1;
                column = column.Remove(s_end);

                list_relations.Clear();
                for (int i = 0; i < sql_table.Count - 1; i++)
                    Get_Table_Relations(sql_table[i], sql_table[i + 1]);//добавим связей

                sql_where_relation = "";
                if (list_relations.Count != 0)//строка со связями
                {
                    for (int i = 0; i < list_relations.Count - 1; i++)
                        sql_where_relation += list_relations[i] + " AND ";
                    sql_where_relation += list_relations[list_relations.Count - 1];
                }

                table = Add_SQL_Table_Name();

                if (sql_where == " WHERE")
                    sql_where = "";

                if (sql_orderby.EndsWith(sql_orderby_end) && sql_orderby != "")
                    sql_orderby = sql_orderby.Replace((sql_orderby_end), (sql_orderby_end).Replace(",", ""));///удаление из orderby запятой в конце

                if (sql_where_relation == "")
                {
                    sql = "SELECT " + column + " \nFROM " + table + sql_where + sql_orderby;//конечная строка
                }
                else if (sql_where_relation != "")
                {
                    if (sql_where != "")
                        sql_where = sql_where.Replace(" WHERE ", " AND ");
                    sql = "SELECT " + column + " \nFROM " + table + " WHERE " + sql_where_relation + sql_where + sql_orderby;//конечная строка со связями
                    sql_where_relation = "";
                }
                MessageBox.Show(sql);
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
       //УСЛОВИЕ ДЛЯ SQL
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)//выбрано имя поля
        {
            string col_type="";
            string tab_name="";
            
            comboBox3.Items.Clear();//чистка старых записей 
            comboBox2.Items.Clear();

            using (var cn = new NpgsqlConnection(connectionString))//вытаскиваем из мета название таблицы и тип столбца
            {
                cn.Open();
                var sql1 = @"select column_type,table_name from Meta where column_name=@column";
                var command = new NpgsqlCommand(sql1, cn);
                command.Parameters.AddWithValue("@column", comboBox1.Text.ToString());
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    col_type = dr[0].ToString();
                    tab_name= dr[1].ToString(); 
                }
            }
           
            using (var cn = new NpgsqlConnection(connectionString))//добавляем значения из столбца в выражение 
            {
                cn.Open();
                var sql1 = $"SELECT \"{comboBox1.Text}\" FROM \"{tab_name}\""; 
                var command = new NpgsqlCommand(sql1, cn);
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    if(!comboBox3.Items.Contains(dr[comboBox1.Text].ToString()))
                         comboBox3.Items.Add(dr[comboBox1.Text].ToString());
                }
            }
            if (col_type == "C")
            {
                comboBox2.Items.Add("=");
            }
            else if (col_type == "D")
            { 
                comboBox2.Items.Add("=");
                comboBox2.Items.Add("<");
                comboBox2.Items.Add(">");
                comboBox2.Items.Add("<=");
                comboBox2.Items.Add(">=");

            }
        }
        public string GetColumn_Type(string column_type)
        {
            string result = "";
            try
            {
                using (var cn = new NpgsqlConnection(connectionString))//вытаскиваем из мета название таблицы и тип столбца
                {
                    cn.Open();
                    var sql1 = @"select column_type from Meta where column_name=@column";
                    var command = new NpgsqlCommand(sql1, cn);
                    command.Parameters.AddWithValue("@column", column_type);
                    var dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        result = dr[0].ToString();
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            return result;
        }

        private void button8_Click(object sender, EventArgs e)//добавить запись
        {
            string result = "";
            /* result=comboBox1.Text.ToString()+"         "+comboBox2.Text.ToString() +
                 "         " + comboBox3.Text.ToString() + "         " + comboBox3.Text.ToString();*/
            try
            {
                ListViewItem list = new ListViewItem(new string[] { comboBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text });
                bool isListExists = false;
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.SubItems[0].Text == list.SubItems[0].Text &&
                        item.SubItems[1].Text == list.SubItems[1].Text &&
                        item.SubItems[2].Text == list.SubItems[2].Text &&
                        item.SubItems[3].Text == list.SubItems[3].Text)
                    {
                        isListExists = true;
                        break;
                    }
                }

                string sviazka = "";
                string virazhenie = "";
                if (GetColumn_Type(list.SubItems[0].Text.ToString()) == "D")
                {
                    virazhenie = "Date('" + list.SubItems[2].Text.ToString() + "')";
                }
                else
                {
                    virazhenie = "'" + list.SubItems[2].Text.ToString() + "'";
                }
                if (!isListExists)//добавление условия
                {
                    listView1.Items.Add(list);
                    if (list.SubItems[3].Text.ToString() == "И")
                    {
                        sviazka = "AND";
                    }
                    else if (list.SubItems[3].Text.ToString() == "ИЛИ")
                    {
                        sviazka = "OR";
                    }

                    if (check_where)
                    {
                        sql_where += " WHERE " + list.SubItems[0].Text.ToString() + list.SubItems[1].Text.ToString() + " " + virazhenie + " " + sviazka;//тут еще должен быть Date 
                        GetTable_Name(list.SubItems[0].Text.ToString());//ЗДЕСЬ ИЗМЕНЕНО
                        check_where = false;
                    }
                    else
                    {
                        sql_where += " " + list.SubItems[0].Text.ToString() + list.SubItems[1].Text.ToString() + " " + virazhenie + " " + sviazka;//и тут 
                        GetTable_Name(list.SubItems[0].Text.ToString());//ЗДЕСЬ ИЗМЕНЕНО
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button9_Click(object sender, EventArgs e)//удалить условие 
        {
            ListView.SelectedIndexCollection collection=listView1.SelectedIndices;//не удаляется в sql коде
            if (collection.Count != 0)
            {
                // sql_where_remove.Add(collection[0].ToString());
                
                string sviazka="";
                StringBuilder sb = new StringBuilder(sql_where);
                if (listView1.SelectedItems[0].SubItems[3].Text == "И")
                {
                    sviazka = "AND";
                }
                else if ( listView1.SelectedItems[0].SubItems[3].Text.ToString() == "ИЛИ")
                {
                    sviazka = "OR";
                }
                string sentenceToDelete = " WHERE " + listView1.SelectedItems[0].SubItems[0].Text.ToString() 
                                        + listView1.SelectedItems[0].SubItems[1].Text.ToString() + " '" + listView1.SelectedItems[0].SubItems[2].Text.ToString() + "' "+sviazka;

                string sentenceToDelete2 = " " + listView1.SelectedItems[0].SubItems[0].Text.ToString()
                                        + listView1.SelectedItems[0].SubItems[1].Text.ToString() + " '" + listView1.SelectedItems[0].SubItems[2].Text.ToString() + "' "+sviazka;
                string sentenceToDelete3 = " " + listView1.SelectedItems[0].SubItems[0].Text.ToString()
                                         + listView1.SelectedItems[0].SubItems[1].Text.ToString() + " Date('" + listView1.SelectedItems[0].SubItems[2].Text.ToString() + "') " + sviazka;

                sql_where = sql_where.Replace(sentenceToDelete2, string.Empty);
                sql_where = sql_where.Replace(sentenceToDelete3, string.Empty);

                listView1.Items.RemoveAt(collection[0]);
            }
            //MessageBox.Show(sql_where_remove[0]);
        }


        //ПОРЯДОК
        private void button10_Click(object sender, EventArgs e)//>
        {
            try { 
                if (!checkedListBox2.Items.Contains(listBox2.SelectedItem.ToString()))
                {
                    checkedListBox2.Items.Add(listBox2.SelectedItem.ToString());
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button11_Click(object sender, EventArgs e)//<
        {
            try
            {
                sql_orderby = sql_orderby.Replace(checkedListBox2.SelectedItem.ToString() + " ASC,", "");
                sql_orderby = sql_orderby.Replace(checkedListBox2.SelectedItem.ToString() + " DESC,", "");
                sql_orderby = sql_orderby.Replace(checkedListBox2.SelectedItem.ToString() + " ASC", "");
                sql_orderby = sql_orderby.Replace(checkedListBox2.SelectedItem.ToString() + " DESC", "");
                checkedListBox2.Items.Remove(checkedListBox2.SelectedItem.ToString());

                if (!checkedListBox2.Items.Contains(checkedListBox2.Items))
                {
                    sql_orderby = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    check_orderby = true;
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message ); }   
        }

        private void button12_Click(object sender, EventArgs e)//>>
        {

            foreach (var item in listBox2.Items)
            {
                if (!checkedListBox2.Items.Contains(item.ToString()))
                {
                    checkedListBox2.Items.Add(item.ToString());
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)//<<
        {
            checkedListBox2.Items.Clear();
            sql_orderby = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            check_orderby = true;
        }

        private void checkedListBox2_SelectedValueChanged(object sender, EventArgs e)//выбран елемент checkedlistbox2
        {
            if (check_orderby)
            {
                sql_orderby += " ORDER BY ";
                check_orderby = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBox2.CheckedItems.Count != 0)
                {
                    for (int i = 0; i < checkedListBox2.Items.Count; i++)
                    {
                        if (checkedListBox2.CheckedItems.Contains(checkedListBox2.Items[i].ToString()))
                        {
                            sql_orderby += checkedListBox2.Items[i].ToString();
                            sql_orderby += " ASC,";
                            sql_orderby_end = checkedListBox2.Items[i].ToString() + " ASC,";
                        }
                        else
                        {
                            sql_orderby += checkedListBox2.Items[i].ToString();
                            sql_orderby += " DESC,";
                            sql_orderby_end = checkedListBox2.Items[i].ToString() + " DESC,";
                        }
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBox2.CheckedItems.Count != 0)
                {
                    for (int i = 0; i < checkedListBox2.Items.Count; i++)
                    {
                        if (checkedListBox2.CheckedItems.Contains(checkedListBox2.Items[i].ToString()))
                        {
                            sql_orderby += checkedListBox2.Items[i].ToString();
                            sql_orderby += " DESC,";
                            sql_orderby_end = checkedListBox2.Items[i].ToString() + " DESC,";
                        }
                        else
                        {
                            sql_orderby += checkedListBox2.Items[i].ToString();
                            sql_orderby += " ASC,";
                            sql_orderby_end = checkedListBox2.Items[i].ToString() + " ASC,";
                        }
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }    
        }

        private void button6_Click(object sender, EventArgs e)//выполнить запрос 
        {
            try
            {
                if (dataGridView1.Columns.Count != 0)
                {
                    dataGridView1.Columns.Clear();
                }
                using (var cn = new NpgsqlConnection(connectionString))//вытаскиваем из мета название таблицы и тип столбца
                {
                    cn.Open();
                    var command = new NpgsqlCommand(sql, cn);
                    var dr = command.ExecuteReader();
                    List<string[]> data = new List<string[]>();
                    while (dr.Read())
                    {
                        data.Add(new string[checkedListBox1.Items.Count]);
                        for (int i = 0; i < checkedListBox1.Items.Count; i++)
                        {
                            data[data.Count - 1][i] = dr[i].ToString();
                        }
                    }
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        dataGridView1.Columns.Add($"Column {i}", $"{checkedListBox1.Items[i]}");
                    }
                    foreach (string[] s in data)
                        dataGridView1.Rows.Add(s);
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message, "Error"); }

        }
    }
}
