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
        public static int currentUserId;
        public static bool SignCheck(string password, string login, Window window)
        {
            //Проверка длины пароля
            if (password.Length < 8)
            {
                MessageBox.Show("Пароль слишком короткий", "Ошибка");
                return false;
            }

            //Проверка длины логина
            if (login.Length < 4)
            {
                MessageBox.Show("Логин слишком короткий", "Ошибка");
                return false;
            }

            //Проверка на наличие хотя бы одной заглавной буквы
            var str = password.ToArray();
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
    }
}
