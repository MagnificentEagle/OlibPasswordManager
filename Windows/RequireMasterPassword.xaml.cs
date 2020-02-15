﻿using Newtonsoft.Json;
using OlibPasswordManager.Properties.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace OlibPasswordManager.Windows
{
    /// <summary>
    /// Логика взаимодействия для RequireMasterPassword.xaml
    /// </summary>
    public partial class RequireMasterPassword
    {
        public RequireMasterPassword() => InitializeComponent();

        private void Button_Click(object sender, RoutedEventArgs e) => Require();
        private void PressEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Require();
        }

        private void Require()
        {
            try
            {
                var s = File.ReadAllText(AppSettings.Items.AppGlobalString);
                User.UsersList = JsonConvert.DeserializeObject<List<User>>(Encryptor.DecryptString(
                    Encryptor.DecryptString(
                        Encryptor.DecryptString(
                            Encryptor.DecryptString(Encryptor.DecryptString(s, TxtPassword.Password),
                                TxtPassword.Password), TxtPassword.Password), TxtPassword.Password),
                    TxtPassword.Password));

                App.MainWindow.PasswordList.ItemsSource = null;
                App.MainWindow.PasswordList.ItemsSource = User.UsersList;
                App.MainWindow.PasswordListNotifyIcon.ItemsSource = null;
                App.MainWindow.PasswordListNotifyIcon.ItemsSource = User.UsersList;
                App.MainWindow.MasterPassword = TxtPassword.Password;
                App.MainWindow.FrameWindow.NavigationService.Navigate(new Uri("/Pages/StartScreen.xaml", UriKind.Relative));
                AppSettings.Items.AppGlobalString = AppSettings.Items.AppGlobalString;

                App.MainWindow.SaveMenuItem.IsEnabled = true;
                App.MainWindow.ChangeMenuItem.IsEnabled = true;
                App.MainWindow.NewLoginMenuItem.IsEnabled = true;
                App.MainWindow.UnlockMenuItem.IsEnabled = false;
                App.MainWindow.LockMenuItem.IsEnabled = true;

                App.MainWindow.LockNotifyIcon.IsEnabled = true;
                App.MainWindow.UnlockNotifyIcon.IsEnabled = false;

                App.MainWindow.AddButton.IsEnabled = true;

                App.MainWindow.IsUnlockedBase = true;

                Close();
            }
            catch
            {
                MessageBox.Show((string)FindResource("MB3"),
                    (string)FindResource("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e) => TxtPassword.Focus();
    }
}
