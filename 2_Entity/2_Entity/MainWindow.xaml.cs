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

namespace _2_Entity
{
    public partial class MainWindow : Window
    {
        private storeP8Entities categories = new storeP8Entities();
        private storeP8Entities orders = new storeP8Entities();
        private storeP8Entities products = new storeP8Entities();
        public MainWindow()
        {
            InitializeComponent();
            dataGrid1.ItemsSource = products.Products.ToList();
            dataGrid2.ItemsSource = orders.OrderArchive.ToList();
            dataGrid3.ItemsSource = categories.Categories.ToList();
        }
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newProduct = new Products
                {
                    ProductName = txtProductName.Text,
                    PorductDescription = txtProductDescription.Text,
                    Category_ID = int.Parse(txtCategoryId.Text),
                    Price = decimal.Parse(txtPrice.Text)
                };

                products.Products.Add(newProduct);
                products.SaveChanges();
                dataGrid1.ItemsSource = products.Products.ToList();
                MessageBox.Show("Продукт успешно добавлен.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                var selectedProduct = dataGrid1.SelectedItem as Products;
                selectedProduct.ProductName = txtProductName.Text;
                selectedProduct.PorductDescription = txtProductDescription.Text;
                selectedProduct.Category_ID = int.Parse(txtCategoryId.Text);
                selectedProduct.Price = decimal.Parse(txtPrice.Text);

                products.SaveChanges();
                dataGrid2.Items.Refresh();
                MessageBox.Show("Продукт успешно изменен.");
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продукт для изменения.");
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                var selectedProduct = dataGrid1.SelectedItem as Products;
                products.Products.Remove(selectedProduct);
                products.SaveChanges();
                dataGrid1.ItemsSource = products.Products.ToList();
                MessageBox.Show("Продукт успешно удален.");
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продукт для удаления.");
            }
        }
        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newOrder = new OrderArchive
                {
                    Product_ID = int.Parse(txtProductId.Text),
                    OrderPrice = int.Parse(txtOrderPrice.Text),
                };

                orders.OrderArchive.Add(newOrder);
                orders.SaveChanges();
                dataGrid2.ItemsSource = orders.OrderArchive.ToList();
                MessageBox.Show("Продукт успешно добавлен.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid2.SelectedItem != null)
            {
                var selectedOrder = dataGrid2.SelectedItem as OrderArchive;
                orders.OrderArchive.Remove(selectedOrder);
                orders.SaveChanges();
                dataGrid2.ItemsSource = orders.OrderArchive.ToList();
                MessageBox.Show("Заказ успешно удален.");
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заказ для удаления.");
            }
        }
        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newCategory = new Categories
                {
                    CategoryName = txtCategoryName.Text
                };

                categories.Categories.Add(newCategory);
                categories.SaveChanges();
                dataGrid3.ItemsSource = categories.Categories.ToList();
                MessageBox.Show("Категория успешно добавлена.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid3.SelectedItem != null)
            {
                var selectedCategory = dataGrid3.SelectedItem as Categories;
                categories.Categories.Remove(selectedCategory);
                categories.SaveChanges();
                dataGrid3.ItemsSource = categories.Categories.ToList();
                MessageBox.Show("Категория успешно удалена.");
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите категорию для удаления.");
            }
        }
























        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                var selectedProduct = dataGrid1.SelectedItem as Products;
                txtProductName.Text = selectedProduct.ProductName;
                txtProductDescription.Text = selectedProduct.PorductDescription;
                txtCategoryId.Text = selectedProduct.Category_ID.ToString();
                txtPrice.Text = selectedProduct.Price.ToString();
            }
        }
        private void OrdersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid2.SelectedItem != null)
            {
                var selectedOrder = dataGrid2.SelectedItem as OrderArchive;
                txtProductId.Text = selectedOrder.Product_ID.ToString();
                txtOrderPrice.Text = selectedOrder.OrderPrice.ToString();

            }
        }
        private void CategoriesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid3.SelectedItem != null)
            {
                var selectedCategory = dataGrid3.SelectedItem as Categories;
                txtCategoryName.Text = selectedCategory.CategoryName;
            }
        }
    }
}
