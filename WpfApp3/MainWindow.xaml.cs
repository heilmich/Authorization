using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Data.SqlClient;

namespace WpfApp3
{
    public class UserContext : DbContext
    {
        public UserContext() :
            base("HeilmichDM")
        { }

        public DbSet<User> Users { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }


        private void SignUpUser(object sender, RoutedEventArgs e)
        {
            //Проверка на соответствие правилам ввода логина и пароля
            if (SignCheck(SignPanelLoginField,SignPanelPassField) == false) return;

            //Запись данных учетки в БД
            using (UserContext db = new UserContext()) 
            {
                User user = new User {Login = SignPanelLoginField.Text, Password = SignPanelPassField.Password, Date_Last_Login = DateTime.Now, LoginTime = 0  };
                db.Users.Add(user);
                db.SaveChanges();
            }
            
            MessageBox.Show("Учетная запись зарегистрирована", "Сообщение");
        }


        private void SignIn(object sender, RoutedEventArgs e)
        {
            AuthPanelResult.Content = "Выполняем вход...";

            //Проверка на соответствие правилам ввода логина и пароля
            if (SignCheck(AuthPanelLoginField, AuthPanelPassField) == false) 
            {
                AuthPanelResult.Content = "Ошибка ввода";
                return;
            }

            //Попытка входа
            using (UserContext db = new UserContext())
            {
                //Поиск строки в БД по логину и паролю
                var users = from p in db.Users
                            where (p.Login == AuthPanelLoginField.Text && p.Password == AuthPanelPassField.Password)
                            select p;
                //Действие при удачном входе
                foreach (var item in users)
                {
                    item.Date_Last_Login = DateTime.Now;
                    AuthPanelResult.Content = "Вход выполнен";
                    AppWindow appWindow = new AppWindow(item.Id);
                    appWindow.Show();
                    this.Close();
                    return;
                }
                //Действие при неудачном входе
                AuthPanelResult.Content = "Вход не выполнен";
            }
        }


        //Проверка правильности ввода
        private bool SignCheck(TextBox loginbox, PasswordBox passbox)
        {
            //Проверка длины логина
            if (loginbox.Text.Length< 4) 
            {
                MessageBox.Show("Логин слишком короткий", "Ошибка");
                return false;            
            }

            //Проверка длины пароля
            if (passbox.Password.Length< 8)
            {
                MessageBox.Show("Пароль слишком короткий","Ошибка");
                return false;
            }
            

            //Проверка на наличие хотя бы одной заглавной буквы
            var str = passbox.Password.ToArray();
            bool HasUpperChar = false;
            foreach (var item in str) 
            {
                if (char.IsUpper(item))
                {
                    return true;
                    
                }
            }
            if (HasUpperChar == false)
            {
                MessageBox.Show("Нужна как минимум одна заглавная буква", "Ошибка");
                return false;
            }
            return true;

        }
        
        
        //Ненужная функция
        private void UserSearch(object sender, RoutedEventArgs e) //Ненужная функция
        {
            if (rbId.IsChecked == true) {
                using (UserContext db = new UserContext())
                {
                    int searchId = Convert.ToInt32(SearchField.Text);
                    var users = db.Users.Where(p => p.Id == searchId);
                    foreach (var item in users)
                    {
                        SearchResult.Content = "Найден пользователь: ID" +item.Id + ", логин " + item.Login.Trim(' ') + ", пароль " +item.Password.Trim(' ');
                        return;
                    }
                }
            }
            if (rbLogin.IsChecked == true)
            {
                using (UserContext db = new UserContext())
                {
                    var users = db.Users.Where(p => p.Login == SearchField.Text);
                    foreach (var item in users)
                    {
                        SearchResult.Content = "Найден пользователь: ID" + item.Id + ", логин " + item.Login.Trim(' ') + ", пароль " + item.Password.Trim(' ');
                        return;
                    }
                }
            }
            if (rbPass.IsChecked == true)
            {
                using (UserContext db = new UserContext())
                {
                    var users = db.Users.Where(p => p.Password == SearchField.Text);
                    foreach (var item in users)
                    {
                        SearchResult.Content = "Найден пользователь: ID" + item.Id + ", логин " + item.Login.Trim(' ') + ", пароль " + item.Password.Trim(' ');
                        return;
                    }
                }
            }
            MessageBox.Show("Пользователь не найден", "Сообщение");
        }
       
    }

}
