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
            base("AppDB")
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
            if (SignUpCheck() == false) return;

            //Запись данных учетки в БД
            using (UserContext db = new UserContext()) 
            {
                
                User user = new User {Login = SignPanelLoginField.Text, Password = SignPanelPassField.Password };
                db.Users.Add(user);
                db.SaveChanges();
            }
            
            MessageBox.Show("Учетная запись зарегестрирована", "Сообщение");
        }

        private bool SignUpCheck()
        {
            if (SignPanelPassField.Password.Length< 8)
            {
                MessageBox.Show("Пароль слишком короткий","Ошибка");
                return false;
            }
            
            if (SignPanelLoginField.Text.Length< 4) 
            {
                MessageBox.Show("Логин слишком короткий", "Ошибка");
                return false;            
            }
            
            var str = SignPanelPassField.Password.ToArray();
            bool HasUpperChar = false;
            foreach (var item in str) 
            {
                if (char.IsUpper(item))
                {
                    HasUpperChar = true;
                }
            }
            if (HasUpperChar == false)
            {
                MessageBox.Show("Нужна как минимум одна заглавная буква", "Ошибка");
                return false;
            }
            return true;
        }
        private void UserSearch(object sender, RoutedEventArgs e)
        {
            using (UserContext db = new UserContext()) 
            {
                var users = db.Users.Where(p=> p.Login == SearchField.Text);
                foreach (var item in users)
                {
                    SearchResult.Content = item.Password;
                }
            }
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            AuthPanelResult.Content = "Выполняем вход...";
            using (UserContext db = new UserContext())
            {
                var users = from p in db.Users
                            where (p.Login == AuthPanelLoginField.Text && p.Password == AuthPanelPassField.Password)
                            select p;
                foreach (var item in users)
                {
                    AuthPanelResult.Content = "Вход выполнен";
                    return;
                }
                // действие при неудачном входе
                AuthPanelResult.Content = "Вход не выполнен";
            }
        }
    }

}
