﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using OlibKey.Views.Controls;
using System;

namespace OlibKey.Views.Windows
{
	public class RequireMasterPasswordWindow : Window
	{
		public Action<DatabaseControl, DatabaseTabHeader> LoadStorageCallback { get; set; }

		private TextBox _tbPassword;

		public TextBlock tbNameStorage;
		public DatabaseControl databaseControl;
		public DatabaseTabHeader databaseTabHeader;

		public RequireMasterPasswordWindow()
		{
			InitializeComponent();
			this.FindControl<Button>("bCancel").Click += (s, e) => Close();
			this.FindControl<Button>("bOpen").Click += ButtonLoadStorage;
			_tbPassword.KeyDown += KeyEnterLoadStorage;
		}

		private void KeyEnterLoadStorage(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter) LoadStorage();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
			_tbPassword = this.FindControl<TextBox>("tbPassword");
			tbNameStorage = this.FindControl<TextBlock>("tbNameStorage");
		}

		private void ButtonLoadStorage(object sender, RoutedEventArgs e) => LoadStorage();

		private void LoadStorage()
		{
			try
			{
				databaseControl.ViewModel.MasterPassword = _tbPassword.Text;
				LoadStorageCallback?.Invoke(databaseControl, databaseTabHeader);
				Close();
			}
			catch
			{
				databaseControl.ViewModel.MasterPassword = null;
				_ = MessageBox.Show(this, null, (string)Application.Current.FindResource("MB3"), (string)Application.Current.FindResource("Error"),
					MessageBox.MessageBoxButtons.Ok, MessageBox.MessageBoxIcon.Error);
			}
		}
	}
}