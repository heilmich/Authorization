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
            if (Authorization.SignCheck(SignPanelPassField.Password, SignPanelLoginField.Text, this) == false) return;

            //Запись данных учетки в БД
            using (UserContext db = new UserContext()) 
            {
                User user = new User {Login = SignPanelLoginField.Text, Password = SignPanelPassField.Password, Date_Last_Login = DateTime.Now, LoginTime = 0  };
                db.Users.Add(user);
                db.SaveChanges();
            }
            
            MessageBox.Show("Учетная запись зарегестрирована", "Сообщение");
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {

            //Попытка входа
            AuthPanelResult.Content = "Выполняем вход...";

            //Проверка на соответствие правилам ввода логина и пароля
            if (Authorization.SignCheck(AuthPanelPassField.Password, AuthPanelLoginField.Text, this) == false) return;

            using (UserContext db = new UserContext())
            {
                var users = from p in db.Users
                            where (p.Login == AuthPanelLoginField.Text && p.Password == AuthPanelPassField.Password)
                            select p;
                foreach (var item in users)
                {
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

    }

}
