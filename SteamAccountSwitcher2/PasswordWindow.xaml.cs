﻿using System;
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

namespace SteamAccountSwitcher2
{
    /// <summary>
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        private string _password;
        private readonly bool _setNewPw;

        /// <summary>
        /// A password window where the user enters a password (duh?)
        /// </summary>
        /// <param name="setNewPw">Set to true to make the user enter a password twice for verification</param>
        public PasswordWindow(bool setNewPw)
        {
            _setNewPw = setNewPw;
            InitializeComponent();
            passwordBox.Focus();
            if (_setNewPw)
            {
                PwWindow.Title = "Set new password";
                PwWindow.Height = 140;
                repeatPasswordPanel.Visibility = Visibility.Visible;
                Image.Source = ImageHelper.GetIconImageSource("key");
            }
            else
            {
                PwWindow.Title = "Decrypt accounts with password";
                PwWindow.Height = 120;
                repeatPasswordPanel.Visibility = Visibility.Collapsed;
                Image.Source = ImageHelper.GetIconImageSource("unlock");
            }
        }

        public string Password => _password;

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == passwordBoxRepeat.Password || !_setNewPw)
            {
                _password = passwordBox.Password;
                Close();
            }
            else
            {
                MessageBox.Show("Passwords do not match. Try again.", "Passwords not matching", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}