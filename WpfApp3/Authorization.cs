using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp3
{
    public static class Authorization
    {
        public static User currentUser;
        
        public static List<User> UsersList = new List<User> { }; // Нужен, чтобы каждый раз не обращаться к БД с запросом на выборку всех пользователей
        
        public static List<Message> MessageList = new List<Message> { }; // Нужен для более удобного редактирования сообщений
        public static bool SignCheck(string password, string login, Window window)
        {
            bool check = true;
            //Проверка длины пароля
            if (password.Length < 8)
            {
                MessageBox.Show("Пароль слишком короткий", "Ошибка");
                check = false;
            }

            //Проверка длины логина
            if (login.Length < 4)
            {
                MessageBox.Show("Логин слишком короткий", "Ошибка");
                check = false;
            }

            //Проверка на наличие хотя бы одной заглавной буквы
            var str = password.ToArray();
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
                check = false;
            }

            if (check == false) return false;
            return true;

        }

        public static void SignUp(string password, string login, string role, Window window)
        {
            //Проверка на соответствие правилам ввода логина и пароля
            if (Authorization.SignCheck(password, login, window) == false) return;

            //Запись данных учетки в БД
            using (UserContext db = new UserContext())
            {
                User user = new User { Login = login, Password = password, Date_Last_Login = DateTime.Now, LoginTime = 0, Role = role};
                db.Users.Add(user);
                db.SaveChanges();
            }

            MessageBox.Show("Учетная запись зарегистрирована", "Сообщение");
            return;
        }

        public static User SignIn(string password, string login, Window window)
        {


            //Проверка на соответствие правилам ввода логина и пароля
            if (Authorization.SignCheck(password, login, window) == false) return null;

            User user = new User();
            using (UserContext db = new UserContext())
            {
                var users = from p in db.Users
                            where (p.Login == login && p.Password == password)
                            select p;
                foreach (var item in users)
                {
                    user = item;
                    return user;
                }
                //Действие при неудачном входе
                return null;
            }
        }
    }
}
