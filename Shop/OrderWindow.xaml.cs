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
using System.Windows.Shapes;

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private DatabasesEntities db = new DatabasesEntities();
        private MessegeHelper _mh = new MessegeHelper();
        private List<Order> _orders = new List<Order>();

        private string[] _sortingTypes = new string[]
        {
            "По умолчанию",
            "По дате создания (новые сначала)",
            "По дате создания (старые сначала)",
            "По дате доставки (ближайшие)",
            "По дате доставки (дальние)"
        };

        public OrderWindow()
        {
            InitializeComponent();
            LoadOrders();
            LoadPData();
            LoadUI();
        }

        private void LoadUI()
        {
            User user = CurentSession.CurrentUser;
            if (user == null || user.RoleId == 3)
            {
                AdminPanel.Visibility = Visibility.Collapsed;
            }

            if (user != null && user.RoleId == 1)
            {
                CreateButton.Visibility = Visibility.Visible;
            }

            if (user != null)
                FullUserName.Text = $"{user.Suname} {user.Name} {user.Patronmic}";
        }

        private void LoadPData()
        {
            SortingComBox.ItemsSource = _sortingTypes;
            SortingComBox.SelectedIndex = 0;

            var statuses = db.OrderStatus.ToList();
            FilteringComBox.ItemsSource = statuses;
            FilteringComBox.DisplayMemberPath = "Name";
            FilteringComBox.SelectedValuePath = "Id";
            FilteringComBox.SelectedIndex = 0;
        }

        private void LoadOrders()
        {
            _orders = db.Order.ToList();
            UpdateOrders();
        }

        private void UpdateOrders()
        {
            OrderList.ItemsSource = null;
            OrderList.ItemsSource = _orders;
        }

        private void SearchingTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchingTextBox.Text.ToLower();

            _orders = db.Order
                .Where(o => o.ReceptCode.ToLower().Contains(searchText) ||
                            o.OrderStatus.Name.ToLower().Contains(searchText) ||
                            (o.User.Suname + " " + o.User.Name + " " + o.User.Patronmic).ToLower().Contains(searchText))
                .ToList();

            UpdateOrders();
        }

        private void SortingComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int sortingType = SortingComBox.SelectedIndex;

            switch (sortingType)
            {
                case 0:
                    LoadOrders();
                    break;
                case 1:
                    _orders = _orders.OrderByDescending(o => o.CreationDate).ToList();
                    break;
                case 2:
                    _orders = _orders.OrderBy(o => o.CreationDate).ToList();
                    break;
                case 3:
                    _orders = _orders.OrderBy(o => o.DeliveriDate).ToList();
                    break;
                case 4:
                    _orders = _orders.OrderByDescending(o => o.DeliveriDate).ToList();
                    break;
            }
            UpdateOrders();
        }

        private void FilteringComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilteringComBox.SelectedItem == null) return;

            var selectedStatus = FilteringComBox.SelectedItem as OrderStatus;
            if (selectedStatus == null)
            {
                LoadOrders();
                return;
            }

            _orders = _orders
                .Where(o => o.StatusId == selectedStatus.Id)
                .ToList();
            UpdateOrders();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().Show();
            Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            new AddEditOrderWindow(null).Show();
            Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            User user = CurentSession.CurrentUser;
            if (user == null) return;

            var border = sender as Border;
            if (border == null) return;

            int id = (int)border.Tag;

            if (user.RoleId == 1)
            {
                // Администратор - редактирование
                var editWindow = new AddEditOrderWindow(id);
                editWindow.Show();
                Close();
            }
            else if (user.RoleId == 2)
            {
                // Менеджер - только просмотр
                var viewWindow = new AddEditOrderWindow(id);
                viewWindow.Title = "Просмотр заказа";
                viewWindow.ReceptCode.IsReadOnly = true;
                viewWindow.OrderStatus.IsEnabled = false;
                viewWindow.PickUpPoint.IsEnabled = false;
                viewWindow.CreationDate.IsEnabled = false;
                viewWindow.DeliveriDate.IsEnabled = false;
                viewWindow.OrderUser.IsEnabled = false;
                viewWindow.SaveButton.Visibility = Visibility.Collapsed;
                viewWindow.DeleteButton.Visibility = Visibility.Collapsed;
                viewWindow.Show();
                Close();
            }
        }
    }
}
