using Shop.Databases;
using Shop.Helpers;
using Shop.Ststics;
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

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabasesEntities db = new DatabasesEntities();
        private MessegeHelper _mh = new MessegeHelper();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new ProductWindow().Show();
            Close();

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginEnter.Text;
            string password = PasswordEnter.Password;

            var user = db.User.Where(u => u.Login == login && u.Password == password).FirstOrDefault();

            if (user == null)
            {
                _mh.ShowError("Введен не правильный логин или пароль!");
                return;
            }
            else
            {  // Сохраняем текущего пользователя
                CurentSession.CurrentUser = user;
                new ProductWindow(user).Show();
                Close();

            }
        }
    }
}
