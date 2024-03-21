using System;
using System.Collections.Generic;
using System.Data;
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
using _2_DataSet.storeP8DataSeteeeTableAdapters;

namespace _2_DataSet
{

    public partial class MainWindow : Window
    {
        CategoriesTableAdapter categories = new CategoriesTableAdapter();
        OrderArchiveTableAdapter orders = new OrderArchiveTableAdapter();
        ProductsTableAdapter products = new ProductsTableAdapter();


        public MainWindow()
        {
            InitializeComponent();
            dataGrid1.ItemsSource = products.GetData();
            dataGrid2.ItemsSource = orders.GetData();
            dataGrid3.ItemsSource = categories.GetData();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtCategoryId.Text, out int categoryId) &&
                decimal.TryParse(txtPrice.Text, out decimal price) &&
                !string.IsNullOrWhiteSpace(txtProductName.Text.Trim()))
            {
                string productName = txtProductName.Text.Trim();
                string productDescription = txtProductDescription.Text.Trim();

                products.Insert(productName, productDescription, categoryId, price);
                dataGrid1.ItemsSource = products.GetData();
                txtProductName.Clear();
                txtCategoryId.Clear();
                txtPrice.Clear();
            }
            else
            {
                MessageBox.Show("Введите нормальные данные для ProductName, CategoryID, and Price.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtProductId.Text, out int productId) &&
                decimal.TryParse(txtOrderPrice.Text, out decimal totalPrice))
            {

                orders.Insert(productId,totalPrice);
                dataGrid2.ItemsSource = orders.GetData();
                txtProductId.Clear();
                txtOrderPrice.Clear();
            }
            else
            {
                MessageBox.Show("Введите нормальные данные для ProductID, Quantity, TotalPrice, and OrderDate.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = txtCategoryName.Text.Trim();

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                categories.Insert(categoryName);
                dataGrid3.ItemsSource = categories.GetData();
                txtCategoryName.Clear();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите название категории.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        

      
















        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dataGrid1.SelectedItem;
                txtProductName.Text = row["ProductName"].ToString();
                txtProductDescription.Text = row["ProductDescription"].ToString();
                txtCategoryId.Text = row["CategoryID"].ToString();
                txtPrice.Text = row["Price"].ToString();
            }
            else if (dataGrid2.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dataGrid2.SelectedItem;

                // Заполнение текстовых полей данными выбранной строки для редактирования
                txtProductId.Text = row["ProductID"].ToString();
                txtOrderPrice.Text = row["TotalPrice"].ToString();
            }
            else if (dataGrid3.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dataGrid3.SelectedItem;

                // Заполнение текстовых полей данными выбранной строки для редактирования
                txtCategoryName.Text = row["CategoryName"].ToString();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продукт, заказ или категорию для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


      






        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dataGrid1.SelectedItem;
                int productId = Convert.ToInt32(row["Product_ID"]);
                string productName = row["ProductName"].ToString();
                string productDescription = row["ProductDescription"].ToString();
                int categoryId = Convert.ToInt32(row["CategoryID"]);
                decimal price = Convert.ToDecimal(row["Price"]);

                // Удаляем данные из базы данных
                products.Delete(productId, productName, productDescription, categoryId, price);

                // Обновляем таблицу gridProducts
                dataGrid1.ItemsSource = products.GetData();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продукт для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid2.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dataGrid2.SelectedItem;
                int orderId = Convert.ToInt32(row["OrderID"]);
                int productId = Convert.ToInt32(row["ProductID"]);

                decimal orderPrice = Convert.ToDecimal(row["OrderPrice"]);


                // Удаляем данные из базы данных
                orders.Delete(orderId, productId, orderPrice);

                // Обновляем таблицу gridOrders
                dataGrid2.ItemsSource = orders.GetData();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заказ для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid3.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dataGrid3.SelectedItem;
                int categoryId = Convert.ToInt32(row["CategoryID"]);
                string categoryName = row["CategoryName"].ToString();

                // Удаляем данные из базы данных
                categories.Delete(categoryId, categoryName);

                // Обновляем таблицу gridCategories
                dataGrid3.ItemsSource = categories.GetData();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите категорию для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dataGrid1.SelectedItem;
                string productName = txtProductName.Text;
                string productDescription = txtProductDescription.Text;
                int categoryId = Convert.ToInt32(txtCategoryId.Text);
                decimal price = Convert.ToDecimal(txtPrice.Text);

                // Здесь обновляем данные в базе данных
                //products.Update(productName, productDescription, categoryId, price, row["ProductName"].ToString(), row["ProductDescription"].ToString(), Convert.ToInt32(row["CategoryID"]), Convert.ToDecimal(row["Price"]));

                // Обновляем таблицу gridProducts
                dataGrid1.ItemsSource = products.GetData();
            }
            else if (dataGrid2.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dataGrid1.SelectedItem;
                int orderId = Convert.ToInt32(row["OrderID"]);
                int productId = Convert.ToInt32(txtProductId.Text);
                decimal orderPrice = Convert.ToDecimal(txtOrderPrice.Text);

                // Здесь обновляем данные в базе данных
                //orders.Update(productId, orderPrice, Convert.ToInt32(row["ProductID"]), Convert.ToDecimal(row["TotalPrice"]));

                // Обновляем таблицу gridOrders
                dataGrid2.ItemsSource = orders.GetData();
            }
            else if (dataGrid3.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dataGrid3.SelectedItem;
                int categoryId = Convert.ToInt32(row["CategoryID"]);
                string categoryName = txtCategoryName.Text;

                // Здесь обновляем данные в базе данных
                categories.Update(categoryName, categoryId, row["CategoryName"].ToString());

                // Обновляем таблицу gridCategories
                dataGrid3.ItemsSource = categories.GetData();
            }
        }
    }
}

