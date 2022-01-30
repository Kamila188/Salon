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
using Магазин_Красавицы.Utilities;
using Магазин_Красавицы.Entities;

namespace Магазин_Красавицы.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductView.xaml
    /// </summary>
    public partial class ProductView : Page
    {
        int CountProduct
        {
            get
            {
                return Transition.Context.Product.ToList().Count;
            }
        }

        #region Конструктор страницы ProductView

        public ProductView()
        {
            InitializeComponent();

            var filtItems = Transition.Context.Manufacturer.ToList();
            filtItems.Insert(0, new Manufacturer { Name = "Все элементы" });
            FiltCBox.ItemsSource = filtItems;

            FiltCBox.SelectedIndex = 0;
            SortCBox.SelectedIndex = 0;

            CountItems.Text = $"Количество записей: {CountProduct} из {CountProduct}";

            ProductsView.ItemsSource = Transition.Context.Product.ToList();
        }

        #endregion

        #region Переход на страницу добавления/изменения продукта

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Transition.MainFrame.Navigate(new AddEditProduct());
        }

        private void ProductsView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Transition.MainFrame.Navigate(new AddEditProduct());
        }

        #endregion

        #region Сортировка данных

        private void DataView()
        {
            var tempDataProduct = Transition.Context.Product.ToList();

            if (FiltCBox.SelectedIndex > 0)
                tempDataProduct = tempDataProduct
                    .Where(p => p.ManufacturerID == (FiltCBox.SelectedItem as Manufacturer).ID)
                    .ToList();

            if (SearchProductBox.Text != "Введите для поиска" &&
                SearchProductBox.Text != null)
                tempDataProduct = tempDataProduct
                    .Where(p => p.Title.ToLower().Contains(SearchProductBox.Text.ToLower())
                    || p.Description.ToLower().Contains(SearchProductBox.Text.ToLower()))
                    .ToList();

            switch (SortCBox.SelectedIndex)
            {
                case 1:
                {
                    if (!(bool)CheckSort.IsChecked)
                        tempDataProduct = tempDataProduct
                                .OrderBy(p => p.Cost)
                                .ToList();
                    else
                        tempDataProduct = tempDataProduct
                                .OrderByDescending(p => p.Cost)
                                .ToList();

                    ProductsView.ItemsSource = tempDataProduct;
                    break;
                }
            }

            ProductsView.ItemsSource = tempDataProduct;

            CountItems.Text = $"Количество записей: {tempDataProduct.Count} из {CountProduct}";
        }

        private void SortCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataView();
        }

        private void FiltCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataView();
        }

        private void SearchProductBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchProductBox.Text != "Введите для поиска")
                DataView();
        }

        private void SearchProductBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchProductBox.Text))
                SearchProductBox.Text = "Введите для поиска";
        }

        private void SearchProductBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchProductBox.Text = null;
        }

        private void CheckSort_Checked(object sender, RoutedEventArgs e)
        {
            DataView();
        }

        private void CheckSort_Unchecked(object sender, RoutedEventArgs e)
        {
            DataView();
        }

        #endregion

        private void DeleteProdBtn_Click(object sender, RoutedEventArgs e)
        {
            var deleteData = ProductsView.SelectedItems.Cast<Product>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить {deleteData.Count} элементов?",
                "Удаление продуктов", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var dataSalesProd = Transition.Context.ProductSale.ToList();

                foreach (var prodSale in deleteData)
                {
                    int bought = dataSalesProd
                        .Where(p => p.ProductID == prodSale.ID)
                        .ToList().Count;

                    if (bought > 0)
                    {
                        MessageBox.Show("Один из выбранных товаров содержит информацию о продажах", "Удаление продуктов", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                try
                {
                    Transition.Context.Product.RemoveRange(deleteData);
                    Transition.Context.SaveChanges();
                    MessageBox.Show("Данные успешно удалены", "Удаление продуктов", MessageBoxButton.OK, MessageBoxImage.Information);

                    DataView();
                }
                catch (Exception er)
                {
                    MessageBox.Show($"При сохранении произошла ошибка:\n{er.Message}", "Удаление продуктов", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ProductsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsView.SelectedItems.Count > 0)
                DeleteProdBtn.Visibility = Visibility.Visible;
            else
                DeleteProdBtn.Visibility = Visibility.Hidden;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Transition.Context.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DataView();
            }
        }
    }
}
