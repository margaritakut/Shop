using Shop.Databases;
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
using System.Windows.Shapes;

namespace Shop.RegistrPolz
{
    /// <summary>
    /// Логика взаимодействия для CreateAccWindow.xaml
    /// </summary>
    public partial class CreateAccWindow : Window
    {
        public CreateAccWindow()
        {
            InitializeComponent();
        }

        private void psbPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (psbPass.Password != txbPass.Text)
            {
                btnCreate.IsEnabled = false;
                psbPass.Background = Brushes.LightCoral;
                psbPass.BorderBrush = Brushes.Red;
            }
            else
            {
                btnCreate.IsEnabled = true;
                psbPass.Background = Brushes.LightGreen;
                psbPass.BorderBrush = Brushes.Green;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (AppConnect.model1db.User.Count(x => x.Login == txbLogin.Text) > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!",
                                "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                User userObj = new User()
                {
                    Name = txbName.Text,
                    Login = txbLogin.Text,
                    Password = txbPass.Text,
                    RoleId = 3 // Роль "Пользователь"
                };

                AppConnect.model1db.User.Add(userObj);
                AppConnect.model1db.SaveChanges();

                MessageBox.Show("Аккаунт успешно создан!", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frameMain.GoBack(); // Возврат на страницу авторизации
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!", "Уведомление",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }
    }
}
