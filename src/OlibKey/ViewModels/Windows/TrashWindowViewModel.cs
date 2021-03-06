﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using OlibKey.Structures;
using OlibKey.Views.Controls;
using OlibKey.Views.Windows;
using ReactiveUI;

namespace OlibKey.ViewModels.Windows
{
    public class TrashWindowViewModel : ReactiveObject
    {
        private ObservableCollection<LoginListItem> _loginsList = new ObservableCollection<LoginListItem>();
        private ObservableCollection<FolderListItem> _foldersList = new ObservableCollection<FolderListItem>();

        #region Property's

        private ObservableCollection<LoginListItem> LoginsList
        {
            get => _loginsList;
            set => this.RaiseAndSetIfChanged(ref _loginsList, value);
        }
        private ObservableCollection<FolderListItem> FoldersList
        {
            get => _foldersList;
            set => this.RaiseAndSetIfChanged(ref _foldersList, value);
        }

        #endregion

        #region ReactiveCommand's

        private ReactiveCommand<Unit, Unit> CloseCommand { get; }

        #endregion

        public TrashWindowViewModel()
        {
            CloseCommand = ReactiveCommand.Create(() => { App.MainWindowViewModel.TrashWindow.Close(); });

            if (((DatabaseControl)App.MainWindowViewModel.SelectedTabItem.Content).ViewModel.Database.Trash == null)
                ((DatabaseControl)App.MainWindowViewModel.SelectedTabItem.Content).ViewModel.Database.Trash = new Trash
                {
                    Folders = new List<Folder>(),
                    Logins = new List<Login>()
                };

            for (int i = 0; i < ((DatabaseControl)App.MainWindowViewModel.SelectedTabItem.Content).ViewModel.Database.Trash.Logins.Count; i++)
                AddLogin(((DatabaseControl)App.MainWindowViewModel.SelectedTabItem.Content).ViewModel.Database.Trash.Logins[i]);
            for (int i = 0; i < ((DatabaseControl)App.MainWindowViewModel.SelectedTabItem.Content).ViewModel.Database.Trash.Folders.Count; i++)
                AddFolder(((DatabaseControl)App.MainWindowViewModel.SelectedTabItem.Content).ViewModel.Database.Trash.Folders[i]);
        }

        private async void ClearTrash()
        {
            if (await MessageBox.Show(App.MainWindow, null,
                    (string)Application.Current.FindResource("MB9"), (string)Application.Current.FindResource("Message"),
                    MessageBox.MessageBoxButtons.YesNo, MessageBox.MessageBoxIcon.Question) == MessageBox.MessageBoxResult.Yes)
            {
                ((DatabaseControl)App.MainWindowViewModel.SelectedTabItem.Content).ViewModel.Database.Trash.Logins.Clear();
                ((DatabaseControl)App.MainWindowViewModel.SelectedTabItem.Content).ViewModel.Database.Trash.Folders.Clear();
                LoginsList.Clear();
                FoldersList.Clear();
            }
        }
        private void RemoveSelectedItems()
        {
            for (int x = LoginsList.Count - 1; x > -1; x--)
                if (LoginsList[x].SelectedItem.IsChecked ?? false) LoginsList.Remove(LoginsList[x]);
            for (int x = FoldersList.Count - 1; x > -1; x--)
                if (FoldersList[x].SelectedItem.IsChecked ?? false) FoldersList.Remove(FoldersList[x]);

            ((DatabaseControl)App.MainWindowViewModel.SelectedTabItem.Content).ViewModel.Database.Trash.Logins = LoginsList.Select(item => item.LoginItem).ToList();
            ((DatabaseControl)App.MainWindowViewModel.SelectedTabItem.Content).ViewModel.Database.Trash.Folders = FoldersList.Select(item => item.FolderContext).ToList();
        }
        private void RestoreSelectedItems()
        {
            for (int x = LoginsList.Count - 1; x > -1; x--)
                if (LoginsList[x].SelectedItem.IsChecked ?? false)
                {
                    LoginsList[x].LoginItem.DeleteDate = null;
                    ((DatabaseControl)App.MainWindowViewModel.SelectedTabItem.Content).ViewModel.LoginList.Add(new LoginListItem(LoginsList[x].LoginItem)
                    {
                        LoginID = Guid.NewGuid().ToString("N"),
                        IconLogin = { Source = LoginsList[x].IconLogin.Source },
                    });
                    LoginsList.Remove(LoginsList[x]);
                }
            for (int x = FoldersList.Count - 1; x > -1; x--)
                if (FoldersList[x].SelectedItem.IsChecked ?? false)
                {
                    FoldersList[x].FolderContext.DeleteDate = null;
                    ((DatabaseControl)App.MainWindowViewModel.SelectedTabItem.Content).ViewModel.Database.Folders.Add(FoldersList[x].FolderContext);
                    FoldersList.Remove(FoldersList[x]);
                }

            ((DatabaseControl)App.MainWindowViewModel.SelectedTabItem.Content).ViewModel.Database.Trash.Logins = LoginsList.Select(item => item.LoginItem).ToList();
            ((DatabaseControl)App.MainWindowViewModel.SelectedTabItem.Content).ViewModel.Database.Trash.Folders = FoldersList.Select(item => item.FolderContext).ToList();
        }

        private void AddLogin(Login a)
        {
            LoginListItem login = new LoginListItem(a)
            {
                SelectedItem = { IsVisible = true },
                IsFavorite = { IsEnabled = false }
            };
            LoginsList.Add(login);
            login.GetIconElement();
        }
        private void AddFolder(Folder a) =>
            FoldersList.Add(new FolderListItem(a)
            {
                tbName = { IsVisible = false },
                SelectedItem = { IsVisible = true }
            });
    }
}
