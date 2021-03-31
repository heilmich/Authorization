 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для AppWindow.xaml
    /// </summary>

    public partial class AppWindow : Window
    {
        int currentUserId;
        public AppWindow(int currentId)
        {
            InitializeComponent();
            //Отображение данных аккаунта
            currentUserId = currentId;
            
            using (UserContext db = new UserContext())
            {
                var users = from p in db.Users
                            where (currentUserId == p.Id)
                            select p;
                foreach (var p in users)
                {
                    TimeLabel.Content = p.LoginTime;
                    IdLabel.Content = "ID: " + p.Id;
                    LoginLabel.Content = "Логин: " + p.Login;
                    DateLastLoginLabel.Content = "Дата последнего входа: " + p.Date_Last_Login;
                }
            }

            //Косячный таймер
            TimerCallback tm = new TimerCallback(AddMinute);
            Timer timer = new Timer(tm,999,0,60000);

        }

        
        public void AddMinute(Object stateInfo)
        {
            using (UserContext db = new UserContext())
            {
                var users = from p in db.Users
                            where (currentUserId == p.Id)
                            select p;
                foreach (var p in users)
                {
                    p.LoginTime += 1;
                    TimeLabel.Content = "Вы были в сети уже " + p.LoginTime + " минут с момента создания аккаунта";
                    db.SaveChanges();
                }

                
            }
        }
        
        
    }
}
