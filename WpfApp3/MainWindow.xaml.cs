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
            base("HeilmichDMEntities")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
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
            string role;

            if (UserRB.IsChecked == true) { role = "user"; }
            else if (AdminRB.IsChecked == true) { role = "admin"; }
            else return;

            Authorization.SignUp(SignPanelPassField.Password, SignPanelLoginField.Text, role, this);
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {

            //Попытка входа
            AuthPanelResult.Content = "Выполняем вход...";

            //Проверка на соответствие правилам ввода логина и пароля
            User user = Authorization.SignIn(AuthPanelPassField.Password, AuthPanelLoginField.Text, this);

            if (user == null) 
            {
                AuthPanelResult.Content = "Вход не выполнен";
                return;
            }

            AuthPanelResult.Content = "Вход выполнен";
            AppWindow appWindow = new AppWindow(user);
            appWindow.Show();
            this.Close();
            return;
            
        }

        

    }
}
