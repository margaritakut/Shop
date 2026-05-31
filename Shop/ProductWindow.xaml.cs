using Shop.Databases;
using Shop.Helpers;
using Shop.Ststics;
using Shop.ViewModuels;
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
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private DatabasesEntities db = new DatabasesEntities();
        private MessegeHelper _mh = new MessegeHelper();
        private List<ProductViewModel> _productViewModels = new List<ProductViewModel>();

        private string[] _sortingTypes = new string[]
        {
                   "По умолчанию" ,
                   "По убыванию",
                   "По возростанию",
        };
        private List<string> _filterTypes = new List<string>()
        {
            "Все поставщики",

        };

        public ProductWindow(User user)
        {
            InitializeComponent();
            LoadProducts();
            LoadPData();
            UpdateProducts();
            LoadUI();
        }

        public ProductWindow()
        {
            InitializeComponent();
            LoadProducts();
            LoadPData();
            UpdateProducts();
            LoadUI();
        }

        private void LoadUI()
        {
            User user = CurentSession.CurrentUser;
            if (user == null || user.RoleId == 3)
            {
                AdminPanel.Visibility = Visibility.Collapsed;
            }
            else if (user.RoleId == 1)
            {
                CreateButton.Visibility = Visibility.Visible;
            }

            // Кнопка заказов видна для администратора (роль 1) и менеджера (роль 2)
            if (user != null && (user.RoleId == 1 || user.RoleId == 2))
            {
                OrdersButton.Visibility = Visibility.Visible;
            }
        }


        private void LoadPData()
        {
            SortingComBox.ItemsSource = _sortingTypes;
            SortingComBox.SelectedIndex = 0;

            var provider = db.Provider.ToList();
            foreach (var p in provider)
                _filterTypes.Add(p.Name);

            FilteringComBox.ItemsSource = _filterTypes;
            FilteringComBox.SelectedIndex = 0;

            User user = CurentSession.CurrentUser;
            if (user != null)
                FullUserName.Text = $"{user.Suname} {user.Name} {user.Patronmic}";

        }

        private void LoadProducts()
        {

            var products = db.Product.ToList();

            foreach (var p in products)
            {
                _productViewModels.Add(new ProductViewModel(p));
            }
            ProductList.ItemsSource = _productViewModels;
        }

        private void UpdateProducts()
        {
            ProductList.ItemsSource = _productViewModels;
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            CurentSession.CurrentUser = null;
            new MainWindow().Show();
            Close();
        }

        private void SearchingTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string sefrhingText = SearchingTextBox.Text;
            _productViewModels = db.Product
            .Where(p =>
            p.Category.Name.ToLower().Contains(sefrhingText) ||
            p.Name.ToLower().Contains(sefrhingText) ||
            p.Discription.ToLower().Contains(sefrhingText) ||
            p.Provider.Name.ToLower().Contains(sefrhingText) ||
            p.Producer.Name.ToLower().Contains(sefrhingText) ||
            p.Unit.Name.ToLower().Contains(sefrhingText)
                )
            .ToList()
            .Select(p => new ProductViewModel(p))
            .ToList();
            UpdateProducts();
        }

        private void SortingComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int sortingType = SortingComBox.SelectedIndex;

            if (sortingType == 0)
            {
                LoadProducts();
            }
            else if (sortingType == 1)
            {
                _productViewModels = _productViewModels.OrderByDescending(p => p.AmountinStock).ToList();
                UpdateProducts();
            }
            else if (sortingType == 2)
            {
                _productViewModels = _productViewModels.OrderBy(p => p.AmountinStock).ToList();
                UpdateProducts();
            }
        }

        private void FilteringComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string filterText = FilteringComBox.SelectedValue.ToString();
            if (filterText == "Все поставщики")
            {
                LoadProducts();
                return;
            }
            _productViewModels = _productViewModels
                .Where(p => p.Provider.Name == filterText)
                .ToList();
            UpdateProducts();
        }
        private void CreateButton_Click_1(object sender, RoutedEventArgs e)
        {
            new AddEditProductWindow(null).Show();
            Close();

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            User user = CurentSession.CurrentUser;
            if (user.RoleId != 1)
                return;

            int id = (int)(sender as Border).Tag;

            new AddEditProductWindow(id).Show();
            Close();
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            new OrderWindow().Show();
            Close();
        }
    }
}
