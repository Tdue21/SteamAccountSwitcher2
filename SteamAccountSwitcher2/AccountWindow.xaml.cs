using System;
using System.Windows;

namespace SteamAccountSwitcher2
{
    /// <summary>
    /// Interaction logic for AccountWindow.xaml
    /// </summary>
    public partial class AccountWindow : Window
    {
		/// <summary>
		/// Creates a new instance of the AccountWindow class. Allows the user to create new accounts.
		/// </summary>
		public AccountWindow()
        {
            InitializeComponent();
            comboBoxType.ItemsSource = Enum.GetValues(typeof(AccountType));
            comboBoxType.SelectedIndex = 0;
            labelIsCached.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Creates a new instance of the AccountWindow class. Allows the user to edit new accounts
        /// </summary>
        /// <param name="accToEdit">The account to edit.</param>
        public AccountWindow(SteamAccount accToEdit)
        {
            if (accToEdit == null)
			{
				throw new ArgumentNullException();
			}

			InitializeComponent();
            Title = "Edit Account";
            Account = accToEdit;

            comboBoxType.ItemsSource = Enum.GetValues(typeof(AccountType));
            comboBoxType.SelectedItem = accToEdit.Type;

            textBoxName.Text = accToEdit.Name;
            textBoxUsername.Text = accToEdit.AccountName;
            textBoxPassword.Password = accToEdit.Password;

            textBoxUsername.IsEnabled = accToEdit.CachedAccount;
            labelIsCached.Visibility = accToEdit.CachedAccount ? Visibility.Visible : Visibility.Collapsed;
        }

		/// <summary>
		/// Accessor to the Account associated with the window.
		/// </summary>
		public SteamAccount Account { get; private set; }

		private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                if (Account == null)
                {
	                Account = new SteamAccount(textBoxUsername.Text, textBoxPassword.Password)
	                {
		                Type = (AccountType) comboBoxType.SelectedValue, 
		                Name = textBoxName.Text
	                };
                }
                else
                {
                    Account.AccountName = textBoxUsername.Text;
                    Account.Password = textBoxPassword.Password;
                    Account.Name = textBoxName.Text;
                    Account.Type = (AccountType)comboBoxType.SelectedValue;
                }
                

                Close();
            }
        }

        private bool ValidateInput()
        {
            var success = true;
            var errorString = "";
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                success = false;
                errorString += "Profile name cannot be empty!\n";
            }

            if (string.IsNullOrEmpty(textBoxUsername.Text))
            {
                success = false;
                errorString += "Username cannot be empty!\n";
            }

            if (string.IsNullOrEmpty(textBoxPassword.Password) && labelIsCached.Visibility != Visibility.Visible)
            {
                success = false;
                errorString += "Password cannot be empty!\n";
            }

            if (success)
            {
                return true;
            }

            MessageBox.Show(errorString, "Validation problem", MessageBoxButton.OK, MessageBoxImage.Information);
            return false;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Account = null;
            Close();
        }
    }
}