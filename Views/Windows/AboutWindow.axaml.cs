﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OlibKey.Views.Windows
{
	public class AboutWindow : Window
	{
		private TextBox _tbInformation;
		private Button _bClose;
		private Button _bGitHub;
		private Button _bVK;
		private Button _bFacebook;

		public AboutWindow() => InitializeComponent();

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);

			_tbInformation = this.FindControl<TextBox>("tbInformation");
			_bClose = this.FindControl<Button>("bClose");
			_bGitHub = this.FindControl<Button>("bGitHub");
			_bVK = this.FindControl<Button>("bVK");
			_bFacebook = this.FindControl<Button>("bFacebook");

			_bClose.Click += (s, e) => Close();
			_bGitHub.Click += (s, e) => Process.Start(new ProcessStartInfo
			{
				FileName = "https://github.com/MagnificentEagle/OlibKey",
				UseShellExecute = true
			});
			_bVK.Click += (s, e) => Process.Start(new ProcessStartInfo
			{
				FileName = "https://vk.com/olibkey",
				UseShellExecute = true
			});
			_bFacebook.Click += (s, e) => Process.Start(new ProcessStartInfo
			{
				FileName = "https://www.facebook.com/olibkey",
				UseShellExecute = true
			});
			_tbInformation.Text = $"{Application.Current.FindResource("AccessText1")}\n{Application.Current.FindResource("AccessText2")}\n\nVersion .NET: {Environment.Version}\nOS: {RuntimeInformation.OSDescription}\nArchitecture: {RuntimeInformation.OSArchitecture}";
		}
	}
}
