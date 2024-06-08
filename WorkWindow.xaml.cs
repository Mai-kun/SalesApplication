using Practical_work.Pages;
using System.Windows;

namespace Practical_work
{
    /// <summary>
    /// Логика взаимодействия для WorkWindow.xaml
    /// </summary>
    public partial class WorkWindow : Window
    {
        public WorkWindow()
        {
            InitializeComponent();
            CurrentFrame.Navigate(new CatalogOfGoodsPage());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult x = MessageBox.Show("Вы действительно хотите выйти?", "Выход",
                MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (x == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            Environment.Exit(0);
        }

        private void CurrentFrame_ContentRendered(object sender, EventArgs e)
        {
            if (CurrentFrame.CanGoBack)
            {
                GoBackButton.Visibility = Visibility.Visible;
                EditGoodButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                GoBackButton.Visibility = Visibility.Collapsed;
                EditGoodButton.Visibility = Visibility.Visible;
            }
        }

        private void EditGoodButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentFrame.GoBack();
        }
    }
}
