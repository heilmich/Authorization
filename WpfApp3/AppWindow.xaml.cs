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

        public int recieverId;
        public AppWindow(User currentUser)
        {
            InitializeComponent();

            Authorization.currentUser = currentUser;

            IdLabel.Text = "ID: " + Authorization.currentUser.Id;
            LoginLabel.Text = "Логин: " + Authorization.currentUser.Login;
            DateLastLoginLabel.Text = "Дата последнего входа: " + Authorization.currentUser.Date_Last_Login;
            TimeLabel.Text = "Вы были в сети уже " + Convert.ToString(Authorization.currentUser.LoginTime) + " минут с момента создания аккаунта";
            RoleLabel.Text = "Ваша роль: " + Authorization.currentUser.Role;

            GetUsers();
            TimerSetup();
        }

        public void GetUsers()
        {
            using (UserContext db = new UserContext())
            {
                var users = from p in db.Users
                            select p;
                foreach (User p in users)
                {
                    usersCB.Items.Add(p.Login);
                    Authorization.UsersList.Add(p);
                }
            }
        }


        public void TimerSetup()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += TimerTick;
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Start();
        }

        public void TimerTick (Object sender, EventArgs e) 
        {
            using (UserContext db = new UserContext())
            {
                var users = from p in db.Users
                            where (Authorization.currentUser.Id == p.Id)
                            select p;
                foreach (var p in users)
                {
                    p.LoginTime += 1;
                    TimeLabel.Text = "Вы были в сети уже " + Convert.ToString(p.LoginTime) + " минут с момента создания аккаунта";
                    db.SaveChanges();
                }
            }
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            using (UserContext db = new UserContext())
            {
                Message msg = new Message { SenderId = Authorization.currentUser.Id, RecieverId = recieverId, Text = MessageTB.Text, Date = DateTime.Now, IsRead = false };
                db.Messages.Add(msg);
                db.SaveChanges();
                AddMessage(msg);
            }
            MessageTB.Text = null;
        }


        private void GetMessages(object sender, SelectionChangedEventArgs e)
        {
            //Очистка панели сообщений
            MessagePanel.Children.Clear();

            //Подключение к базе данных и обновление сообщений
            using (UserContext db = new UserContext())
            {
                foreach (var item in Authorization.UsersList)
                {
                    if (item.Login == usersCB.SelectedItem.ToString())
                        recieverId = item.Id;
                }
                var messages = from p in db.Messages
                               where ((p.SenderId == Authorization.currentUser.Id && p.RecieverId == recieverId) || (p.SenderId == recieverId && p.RecieverId == Authorization.currentUser.Id))
                               select p;
                foreach (var item in messages)
                {
                    AddMessage(item);
                }
            }
        }

        private void AddMessage(Message item)
        {
            MessageObject msg = new MessageObject(item, MessagePanel);
        }

        class MessageObject : StackPanel
        {
            public MessageObject(Message item, StackPanel MessagePanel) 
            {
                Authorization.MessageList.Add(item);
                Orientation = Orientation.Horizontal;
                StackPanel msgSP = new StackPanel();
                TextBlock userTB = new TextBlock();
                TextBlock msgTB = new TextBlock();
                msgSP.Orientation = Orientation.Horizontal;
                msgSP.Children.Add(userTB);
                msgSP.Children.Add(msgTB);
                msgTB.Text = item.Text;
                userTB.Foreground = Brushes.Red;
                userTB.Text = "[" + Convert.ToString(item.SenderId) + "]";
                MessagePanel.Children.Add(msgSP);
            }
        }
    }
}
