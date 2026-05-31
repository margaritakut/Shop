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
    /// Логика взаимодействия для AddEditProductWindow.xaml
    /// </summary>
    public partial class AddEditProductWindow : Window
    {
        private DatabasesEntities db = new DatabasesEntities();
        private MessegeHelper _mh = new MessegeHelper();

        private bool _isEditing;
        private Product _product;

        public AddEditProductWindow(int? id)
        {
            InitializeComponent();

            if (id == null)
            {
                _isEditing = false;
            }
            else
            {
                _isEditing = true;
                _product = db.Product.Find(id);
            }
            LoadData();
            LoadUI();
        }

        private void LoadUI()
        {
            User user = CurentSession.CurrentUser;

            // Кнопка удаления видна только администратору (роль 1) и только при редактировании
            if (user != null && user.RoleId == 1 && _isEditing == true)
            {
                DeleteButton.Visibility = Visibility.Visible;
            }
            else
            {
                DeleteButton.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadData()
        {
            var units = db.Unit.ToList();
            var categories = db.Category.ToList();
            var producers = db.Producer.ToList();
            var providers = db.Provider.ToList();

            ProductUnit.ItemsSource = units;
            ProductUnit.DisplayMemberPath = "Name";
            ProductUnit.SelectedValuePath = "Id";
            ProductUnit.SelectedIndex = 0;

            ProductProducer.ItemsSource = producers;
            ProductProducer.DisplayMemberPath = "Name";
            ProductProducer.SelectedValuePath = "Id";
            ProductProducer.SelectedIndex = 0;

            ProductProvider.ItemsSource = providers;
            ProductProvider.DisplayMemberPath = "Name";
            ProductProvider.SelectedValuePath = "Id";
            ProductProvider.SelectedIndex = 0;

            ProductCategory.ItemsSource = categories;
            ProductCategory.DisplayMemberPath = "Name";
            ProductCategory.SelectedValuePath = "Id";
            ProductCategory.SelectedIndex = 0;

            if (_isEditing == true)
                FillData();
        }

        private void FillData()
        {
            ProductArticle.Text = _product.Article;
            ProductName.Text = _product.Name;
            ProductPrice.Text = _product.Price.ToString();
            ProductDiscount.Text = _product.Discount.ToString();
            ProductAmountInStock.Text = _product.AmountinStock.ToString();
            ProductDescription.Text = _product.Discription;
            ProductPhoto.Text = _product.Photo;

            ProductUnit.SelectedValue = _product.Unitid;
            ProductProducer.SelectedValue = _product.ProducerId;
            ProductProvider.SelectedValue = _product.ProviderId;
            ProductCategory.SelectedValue = _product.CategoryId;
        }

        private void CencelButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().Show();
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;

            if (_isEditing == true)
            {
                UpdateProduct();
            }
            else
            {
                CreateProduct();
            }
        }

        private void CreateProduct()
        {
            Product product = new Product();

            string article = ProductArticle.Text;
            string name = ProductName.Text;
            decimal price = Convert.ToDecimal(ProductPrice.Text);
            decimal discount = Convert.ToDecimal(ProductDiscount.Text);
            decimal amountInStock = Convert.ToDecimal(ProductAmountInStock.Text);
            string description = ProductDescription.Text;
            string photo = ProductPhoto.Text;

            product.Article = article;
            product.Name = name;
            product.Price = price;
            product.Discount = discount;
            product.AmountinStock = amountInStock;
            product.Discription = description;
            product.Photo = photo;

            product.Unitid = (int)ProductUnit.SelectedValue;
            product.ProducerId = (int)ProductProducer.SelectedValue;
            product.ProviderId = (int)ProductProvider.SelectedValue;
            product.CategoryId = (int)ProductCategory.SelectedValue;

            try
            {
                db.Product.Add(product);
                db.SaveChanges();
                _mh.ShowInfo("Продукт успешно создан!");
                CencelButton_Click(null, null);
            }
            catch (Exception ex)
            {
                _mh.ShowError(ex.Message);
            }
        }

        private void UpdateProduct()
        {
            string article = ProductArticle.Text;
            string name = ProductName.Text;
            decimal price = Convert.ToDecimal(ProductPrice.Text);
            decimal discount = Convert.ToDecimal(ProductDiscount.Text);
            decimal amountInStock = Convert.ToDecimal(ProductAmountInStock.Text);
            string description = ProductDescription.Text;
            string photo = ProductPhoto.Text;

            _product.Article = article;
            _product.Name = name;
            _product.Price = price;
            _product.Discount = discount;
            _product.AmountinStock = amountInStock;
            _product.Discription = description;
            _product.Photo = photo;

            _product.Unitid = (int)ProductUnit.SelectedValue;
            _product.ProducerId = (int)ProductProducer.SelectedValue;
            _product.ProviderId = (int)ProductProvider.SelectedValue;
            _product.CategoryId = (int)ProductCategory.SelectedValue;

            try
            {
                db.SaveChanges();
                _mh.ShowInfo("Продукт успешно изменён!");
                CencelButton_Click(null, null);
            }
            catch (Exception ex)
            {
                _mh.ShowError(ex.Message);
            }
        }

        private bool ValidateInput()
        {
            StringBuilder errors = new StringBuilder();

            string article = ProductArticle.Text;
            string name = ProductName.Text;
            string price = ProductPrice.Text;
            string discount = ProductDiscount.Text;
            string amountInStock = ProductAmountInStock.Text;
            string description = ProductDescription.Text;

            if (string.IsNullOrWhiteSpace(article))
                errors.AppendLine("Поле артикул не заполнено!");

            if (string.IsNullOrWhiteSpace(name))
                errors.AppendLine("Поле наименования не заполнено!");

            if (string.IsNullOrWhiteSpace(price)
                || !decimal.TryParse(price, out decimal priceDecimal)
                || priceDecimal < 0)
                errors.AppendLine("Поле цены заполнено неверно!");

            if (string.IsNullOrWhiteSpace(discount)
                || !decimal.TryParse(discount, out decimal discountDecimal)
                || discountDecimal > 100 || discountDecimal < 0)
                errors.AppendLine("Поле скидки заполнено неверно (0-100)!");

            if (string.IsNullOrWhiteSpace(amountInStock)
                || !decimal.TryParse(amountInStock, out decimal amountInStockDecimal)
                || amountInStockDecimal < 0)
                errors.AppendLine("Поле количества заполнено неверно!");

            if (string.IsNullOrWhiteSpace(description))
                errors.AppendLine("Поле описания не заполнено!");

            if (errors.Length > 0)
            {
                _mh.ShowError(errors.ToString());
                return false;
            }
            return true;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_product == null) return;

            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить товар {_product.Name}?",
                                                        "Подтверждение удаления",
                                                        MessageBoxButton.YesNo,
                                                        MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Проверяем, есть ли товар в заказах
                    var productInOrders = db.ProductinOrder.Where(p => p.ProductId == _product.Id).ToList();
                    if (productInOrders.Any())
                    {
                        db.ProductinOrder.RemoveRange(productInOrders);
                    }

                    db.Product.Remove(_product);
                    db.SaveChanges();

                    _mh.ShowInfo("Товар успешно удален!");
                    new ProductWindow().Show();
                    Close();
                }
                catch (Exception ex)
                {
                    _mh.ShowError("Ошибка при удалении: " + ex.Message);
                }
            }
        }
    }
}
