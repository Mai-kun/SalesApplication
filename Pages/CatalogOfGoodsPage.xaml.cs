using Practical_work.Models;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;

namespace Practical_work.Pages
{
    /// <summary>
    /// Логика взаимодействия для CatalogOfGoodsPage.xaml
    /// </summary>
    public partial class CatalogOfGoodsPage : Page
    {
        private int _itemCount;

        public CatalogOfGoodsPage()
        {
            InitializeComponent();

            InitComboBox();
        }

        private void InitComboBox()
        {
            using var context = new EnglishstoreContext();

            var developer = context.Manufacturers.AsNoTracking().OrderBy(x => x.Name).ToList();
            developer.Insert(0, new Manufacturer
            {
                Name = "Все типы"
            });

            DeveloperComboBox.ItemsSource = developer;
            DeveloperComboBox.SelectedIndex = 0;

            GoodsListView.ItemsSource = context.Products.AsNoTracking().OrderBy(x => x.Name).ToList();
            _itemCount = GoodsListView.Items.Count;

            CountTextBlock.Text = $"Результат запроса: {_itemCount} записей из {_itemCount}";
        }

        private async void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            await using var context = new EnglishstoreContext();

            if (Visibility == Visibility.Visible)
            {
                context.ChangeTracker.Entries().ToList().ForEach(e => e.ReloadAsync());
                GoodsListView.ItemsSource = context.Products.AsNoTracking().OrderBy(p => p.Name).ToList();
            }
        }


        private async void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            await UpdateData();
        }

        private async void DeveloperComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await UpdateData();
        }

        private async void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await UpdateData();
        }

        private async Task UpdateData()
        {
            await using var context = new EnglishstoreContext();

            var currentGoods = context.Products.AsNoTracking().OrderBy(p => p.Name).ToList();

            if (DeveloperComboBox.SelectedIndex > 0)
            {
                currentGoods = currentGoods.Where(p => p.Manufacturerid == ((Manufacturer)DeveloperComboBox.SelectedItem).Id).ToList();
            }

            currentGoods = currentGoods.Where(p => p.Name.Contains(SearchTextBox.Text, StringComparison.CurrentCultureIgnoreCase)).ToList();

            if (SortComboBox.SelectedIndex > 0)
            {
                if (SortComboBox.SelectedIndex == 1)
                {
                    currentGoods = currentGoods.OrderBy(p => p.Price).ToList();
                }
                if (SortComboBox.SelectedIndex == 2)
                {
                    currentGoods = currentGoods.OrderByDescending(p => p.Price).ToList();
                }
            }

            GoodsListView.ItemsSource = currentGoods;

            CountTextBlock.Text = $"Результат запроса: {currentGoods.Count} записей из {_itemCount}";
        }

        private async void RibbonCheckBox_Click(object sender, RoutedEventArgs e)
        {
            await UpdateData();
        }
    }
}
