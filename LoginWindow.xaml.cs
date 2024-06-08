using System.Data.Entity;
using System.Windows;

namespace Practical_work
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            LoginTextBox.Text = AuthorizationSettings.Default.Login;
            PasswordTextBox.Password = AuthorizationSettings.Default.Password;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GiveAccess();
            }
            catch (NullReferenceException)
            {
                System.Windows.MessageBox.Show("Невеный логин или пароль",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GiveAccess()
        {
            string login = LoginTextBox.Text;
            string pass = PasswordTextBox.Password;

            using var context = new EnglishstoreContext();

            var user = context.Users
                .AsNoTracking()
                .FirstOrDefault(l => l.Login == login && l.Password == pass) ?? throw new NullReferenceException();

            SaveAuthorizationData();

            WorkWindow workWindow = new()
            {
                Owner = this
            };
            this.Hide();
            workWindow.Show();
        }

        private void SaveAuthorizationData()
        {
            AuthorizationSettings.Default.Login = LoginTextBox.Text;
            AuthorizationSettings.Default.Password = PasswordTextBox.Password;
            AuthorizationSettings.Default.Save();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult x = MessageBox.Show("Вы действительно хотите выйти?", "Выход",
                MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (x == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}