using Shop.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Shop.ViewModuels
{
    public class ProductViewModel
    {
        public ProductViewModel(Product product)
        {
            Id = product.Id;
            Article = product.Article;
            Name = product.Name;
            Price = product.Price;
            Discount = product.Discount;
            AmountinStock = product.AmountinStock;
            Discription = product.Discription;
            Photo = product.Photo;
            Category = product.Category;
            Producer = product.Producer;
            Provider = product.Provider;
            Unit = product.Unit;
            GetBackround();
            GetPhoto();
            GetPrice();
        }
        public int Id { get; set; }
        public string Article { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal AmountinStock { get; set; }
        public string Discription { get; set; }
        public string Photo { get; set; }
        public Category Category { get; set; }
        public Producer Producer { get; set; }
        public Provider Provider { get; set; }
        public Unit Unit { get; set; }
        public Brush Background { get; set; }

        private void GetBackround()
        {
            if (Discount >= 15)
            {
                Background = (Brush)new BrushConverter().ConvertFromString("#2e8b57");
                return;
            }
            else if (AmountinStock == 0)
            {
                Background = Brushes.LightBlue;
                return;
            }
            else
            {
                Background = (Brush)new BrushConverter().ConvertFromString("#7fff00");
                return;
            }
        }
        private void GetPhoto()
        {
            if (!string.IsNullOrEmpty(Photo) || Photo != "")
                return;
            Photo = "/Res/picture.png";

        }
        private void GetPrice()
        {
            if (Discount == 0)
                return;
            OldPrice = Price;
            Price = OldPrice * (1  - Discount / 100);
        }
    }
}
