using Shop.Databases;
using Shop.Helpers;
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
    /// Логика взаимодействия для AddEditOrderWindow.xaml
    /// </summary>
    public partial class AddEditOrderWindow : Window
    {
        private DatabasesEntities db = new DatabasesEntities();
        private MessegeHelper _mh = new MessegeHelper();
        private bool _isEditing;
        private Order _order;

        public AddEditOrderWindow(int? id)
        {
            InitializeComponent();

            if (id == null)
            {
                _isEditing = false;
                DeleteButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                _isEditing = true;
                _order = db.Order.Find(id);
                DeleteButton.Visibility = Visibility.Visible;
            }
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Загружаем всех клиентов
                var users = db.User.ToList();
                OrderUser.ItemsSource = users;
                OrderUser.DisplayMemberPath = "Suname";
                OrderUser.SelectedValuePath = "Id";
                if (users.Any())
                    OrderUser.SelectedIndex = 0;

                // Загружаем статусы
                var statuses = db.OrderStatus.ToList();
                OrderStatus.ItemsSource = statuses;
                OrderStatus.DisplayMemberPath = "Name";
                OrderStatus.SelectedValuePath = "Id";
                if (statuses.Any())
                    OrderStatus.SelectedIndex = 0;

                // Загружаем пункты выдачи
                var pickUpPoints = db.PickUpPoint.ToList();
                PickUpPoint.ItemsSource = pickUpPoints;
                PickUpPoint.SelectedValuePath = "Id";
                if (pickUpPoints.Any())
                    PickUpPoint.SelectedIndex = 0;

                if (_isEditing == true)
                {
                    FillData();
                }
                else
                {
                    // При создании - поля пустые
                    CreationDate.SelectedDate = DateTime.Now;
                    DeliveriDate.SelectedDate = DateTime.Now.AddDays(7);
                    ReceptCode.Text = string.Empty;

                    OrderStatus.SelectedIndex = 0;
                    OrderUser.SelectedIndex = 0;
                    PickUpPoint.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _mh.ShowError("Ошибка загрузки данных: " + ex.Message);
            }
        }

        private void FillData()
        {
            ReceptCode.Text = _order.ReceptCode;
            CreationDate.SelectedDate = _order.CreationDate;
            DeliveriDate.SelectedDate = _order.DeliveriDate;
            OrderStatus.SelectedValue = _order.StatusId;
            OrderUser.SelectedValue = _order.UserId;
            PickUpPoint.SelectedValue = _order.PickUpPointId;
        }

        private void CencelButton_Click(object sender, RoutedEventArgs e)
        {
            new OrderWindow().Show();
            Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить заказ №{_order.ReceptCode}?",
                                                        "Подтверждение удаления",
                                                        MessageBoxButton.YesNo,
                                                        MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var productInOrders = db.ProductinOrder.Where(p => p.OrderId == _order.Id).ToList();
                    if (productInOrders.Any())
                        db.ProductinOrder.RemoveRange(productInOrders);

                    db.Order.Remove(_order);
                    db.SaveChanges();

                    _mh.ShowInfo("Заказ успешно удален!");
                    new OrderWindow().Show();
                    Close();
                }
                catch (Exception ex)
                {
                    _mh.ShowError("Ошибка при удалении: " + ex.Message);
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
                return;

            if (_isEditing == true)
            {
                UpdateOrder();
            }
            else
            {
                CreateOrder();
            }
        }

        private void CreateOrder()
        {
            try
            {
                // Проверяем все значения перед созданием
                string receptCode = ReceptCode.Text;
                int statusId = (int)OrderStatus.SelectedValue;
                DateTime creationDate = CreationDate.SelectedDate ?? DateTime.Now;
                DateTime deliveriDate = DeliveriDate.SelectedDate ?? DateTime.Now.AddDays(7);
                int userId = (int)OrderUser.SelectedValue;
                int pickUpPointId = (int)PickUpPoint.SelectedValue;

                // Проверка на пустой артикул
                if (string.IsNullOrWhiteSpace(receptCode))
                {
                    _mh.ShowError("Артикул не может быть пустым!");
                    return;
                }

                Order order = new Order();
                order.ReceptCode = receptCode;
                order.StatusId = statusId;
                order.CreationDate = creationDate;
                order.DeliveriDate = deliveriDate;
                order.UserId = userId;
                order.PickUpPointId = pickUpPointId;

                db.Order.Add(order);
                int result = db.SaveChanges();

                if (result > 0)
                {
                    _mh.ShowInfo("Заказ успешно создан!");
                    CencelButton_Click(null, null);
                }
                else
                {
                    _mh.ShowError("Не удалось сохранить заказ в базу данных!");
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                // Ошибка валидации EF
                string errors = "";
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errors += $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}\n";
                    }
                }
                _mh.ShowError("Ошибка валидации:\n" + errors);
            }
            catch (Exception ex)
            {
                _mh.ShowError("Ошибка при создании заказа: " + ex.Message + "\n\n" + ex.InnerException?.Message);
            }
        }

        private void UpdateOrder()
        {
            try
            {
                // Получаем свежие данные из БД
                var orderToUpdate = db.Order.Find(_order.Id);
                if (orderToUpdate == null)
                {
                    _mh.ShowError("Заказ не найден в базе данных!");
                    return;
                }

                // Обновляем поля
                orderToUpdate.StatusId = (int)OrderStatus.SelectedValue;
                orderToUpdate.DeliveriDate = DeliveriDate.SelectedDate ?? orderToUpdate.DeliveriDate;
                orderToUpdate.PickUpPointId = (int)PickUpPoint.SelectedValue;
                orderToUpdate.ReceptCode = ReceptCode.Text;
                orderToUpdate.CreationDate = CreationDate.SelectedDate ?? orderToUpdate.CreationDate;
                orderToUpdate.UserId = (int)OrderUser.SelectedValue;

                db.SaveChanges();
                _mh.ShowInfo("Заказ успешно изменен!");
                CencelButton_Click(null, null);
            }
            catch (Exception ex)
            {
                _mh.ShowError("Ошибка при обновлении: " + ex.Message);
            }
        }

        private bool ValidateInput()
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(ReceptCode.Text))
                errors.AppendLine("Поле артикула не заполнено!");

            if (OrderStatus.SelectedValue == null)
                errors.AppendLine("Статус не выбран!");

            if (OrderUser.SelectedValue == null)
                errors.AppendLine("Клиент не выбран!");

            if (PickUpPoint.SelectedValue == null)
                errors.AppendLine("Пункт выдачи не выбран!");

            if (CreationDate.SelectedDate == null)
                errors.AppendLine("Дата заказа не выбрана!");

            if (DeliveriDate.SelectedDate == null)
                errors.AppendLine("Дата выдачи не выбрана!");

            if (DeliveriDate.SelectedDate < CreationDate.SelectedDate)
                errors.AppendLine("Дата выдачи не может быть раньше даты заказа!");

            if (errors.Length > 0)
            {
                _mh.ShowError(errors.ToString());
                return false;
            }
            return true;
        }
    }
}
