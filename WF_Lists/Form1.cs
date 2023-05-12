using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
/*Написать приложение – анкету, которую предлагается заполнить пользователю: имя, фамилия, e-mail, телефон. 
После нажатия на кнопку «Добавить» Информация о пользователе попадает в ListBox, 
в котором храниться информация о всех пользователях. 
Также, в ListBox, по клику на строку с информацией о пользователе следует предусмотреть возможность 
удаления пользователя из коллекции элементов ListBox, а также редактирования. 
Редактирование информации о пользователе производиться путем подстановки данных из ListBox, 
в соответствующие поля для ввода новой информации.
Предусмотреть экспорт/импорт всей информации о пользователях в файл.*/
namespace WF_Lists
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "Анкета";
            StartPosition = FormStartPosition.CenterScreen;
        }
        
        public class Human
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }    
            public Human() { }
            public Human(string name, string surname,string email, string phone)
            {
                Name = name;
                Surname = surname;
                Email = email;
                Phone = phone;
            }
            public override string ToString()
            {
                return $"{Name} {Surname}, {Phone}, {Email}";
            }
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            // если поля не пустые добавляем пользователя
            if (!String.IsNullOrEmpty(tB_name.Text) && !String.IsNullOrEmpty(tB_surname.Text)
                && !String.IsNullOrEmpty(tB_mail.Text) && !String.IsNullOrEmpty(tB_phone.Text))
            {
                Human h = new Human(tB_name.Text, tB_surname.Text, tB_mail.Text, tB_phone.Text);
                listBox1.Items.Add(h);
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            tB_name.Text = string.Empty;
            tB_surname.Text = string.Empty;
            tB_mail.Text = string.Empty;
            tB_phone.Text = string.Empty;
        }
        private void btn_edit_Click(object sender, EventArgs e)
        {            
            //поменять полностью все данные
            if (!String.IsNullOrEmpty(tB_name.Text) && !String.IsNullOrEmpty(tB_surname.Text)
                && !String.IsNullOrEmpty(tB_mail.Text) && !String.IsNullOrEmpty(tB_phone.Text))
            {
                //есть выделенные
                if (listBox1.SelectedItems != null)
                {
                    for (int i = 0; i < listBox1.SelectedItems.Count; i++)
                    {
                        listBox1.Items.Remove(listBox1.SelectedItems[i]);
                        Human h = new Human(tB_name.Text, tB_surname.Text, tB_mail.Text, tB_phone.Text);
                        listBox1.Items.Add(h);
                    }
                    tB_name.Text = string.Empty;
                    tB_surname.Text = string.Empty;
                    tB_mail.Text = string.Empty;
                    tB_phone.Text = string.Empty;
                }
            }
            else MessageBox.Show("Перед редагуванням заповніть поля даними\n" +
                "виділіть поле яке хочете редагувати і натисніть \"Редагувати\"","Інструкція");
        }
        private void btn_del_Click(object sender, EventArgs e)
        {
            //список не пуст
            if (listBox1.Items.Count != 0)
            {
                //есть выделенные
                if (listBox1.SelectedItems != null)
                {
                    for (int i = 0; i < listBox1.SelectedItems.Count; i++)
                    {
                        listBox1.Items.Remove(listBox1.SelectedItems[i]);
                    }
                }
            }            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Human h1 = new Human("Ірина", "Самойленко", "isam@gmail.com", "+380667894111");
            Human h2 = new Human("Сергій", "Кравченко", "skrav@gmail.com", "+380931234567");
            listBox1.Items.Add(h1);
            listBox1.Items.Add(h2);
        }
        List<Human> list = new List<Human>();
        private void btn_Import_Click(object sender, EventArgs e)
        {
            try
            {
                // нужно для сохранения данных в файл
                foreach (var item in listBox1.Items)
                {
                    list.Add((Human)item);
                }
                //  Записываем данные в файл
                using (StreamWriter writer = new StreamWriter("Humen.txt"))
                {
                    foreach (var user in list)
                    {
                        writer.WriteLine(user);
                    }
                }
                MessageBox.Show("Дані завантажені у файл");
            }catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                // Загрузить данные с файла
                using (StreamWriter writer = new StreamWriter("Humen.txt"))
                {
                    foreach (Human user in list)
                    {
                        writer.WriteLine($"{user.Name},{user.Surname},{user.Email},{user.Phone}");
                        Human human = new Human(user.Name, user.Surname, user.Email, user.Phone);
                        listBox1.Items.Add(human);
                    }
                }
                MessageBox.Show("Дані завантажені з файлу");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
