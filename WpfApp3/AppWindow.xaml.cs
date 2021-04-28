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
using System.Windows.Threading;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для AppWindow.xaml
    /// </summary>

    public partial class AppWindow : Window
    {
        public AppWindow(int currentId)
        {
            InitializeComponent();

            Authorization.currentUserId = currentId;

            using (UserContext db = new UserContext())
            {
               
                var users = from p in db.Users
                            where (Authorization.currentUserId == p.Id)
                            select p;
                
                foreach (var p in users)
                {
                    IdLabel.Content = "ID: " + p.Id;
                    LoginLabel.Content = "Логин: " + p.Login;
                    DateLastLoginLabel.Content = "Дата последнего входа: " + p.Date_Last_Login;
                    TimeLabel.Content = "Вы были в сети уже " + Convert.ToString(p.LoginTime) + " минут с момента создания аккаунта";
                }
            }
            TimerSetup();

        }

        
        public void TimerSetup() 
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += AddMinuteAsync;
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Start();
        }
        
        async void AddMinuteAsync(object sender, EventArgs e) 
        {
            await Task.Run(()=>AddMinute());
        }
        void AddMinute()
        {
            using (UserContext db = new UserContext())
            {
                var users = from p in db.Users
                            where (Authorization.currentUserId == p.Id)
                            select p;
                foreach (var p in users)
                {
                    p.LoginTime += 1;
                    TimeLabelUpdate(p);
                    db.SaveChanges();
                }
            }
        }

        void TimeLabelUpdate(User p) 
        {
            TimeLabel.Content = "Вы были в сети уже " + Convert.ToString(p.LoginTime) + " минут с момента создания аккаунта";
        }
    }
}
