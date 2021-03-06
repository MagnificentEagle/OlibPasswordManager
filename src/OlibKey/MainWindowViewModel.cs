﻿using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;
using OlibKey.Core;
using OlibKey.Views.Controls;
using OlibKey.Structures;
using OlibKey.ViewModels.Pages;
using OlibKey.Views.Windows;
using System;
using System.Collections.Generic;
using Avalonia;

namespace OlibKey
{
    public class MainWindowViewModel : ReactiveObject
    {
        private bool _isUnlockDatabase;
        private bool _isLockDatabase;

        private int _countLogins;

        private ChangeMasterPasswordWindow _windowChangeMasterPassword;
        private SettingsWindow _settingsWindow;
        public PasswordGeneratorWindow PasswordGenerator;
        public CheckingWeakPasswordsWindow CheckingWindow;
        public TrashWindow TrashWindow;

        private ObservableCollection<TabItem> _tabItems = new ObservableCollection<TabItem>();
        private TabItem _selectedTabItem;

        #region ReactiveCommand's   

        private ReactiveCommand<Unit, Unit> ExitProgramCommand { get; }
        private ReactiveCommand<Unit, Unit> CreateLoginCommand { get; }
        private ReactiveCommand<Unit, Unit> CreateDatabaseCommand { get; }
        private ReactiveCommand<Unit, Unit> SaveDatabaseCommand { get; }
        private ReactiveCommand<Unit, Unit> UnlockDatabaseCommand { get; }
        private ReactiveCommand<Unit, Unit> LockDatabaseCommand { get; }
        private ReactiveCommand<Unit, Unit> OpenDatabaseCommand { get; }
        private ReactiveCommand<Unit, Unit> ShowSearchWindowCommand { get; }
        private ReactiveCommand<Unit, Unit> ChangeMasterPasswordCommand { get; }
        private ReactiveCommand<Unit, Unit> OpenPasswordGeneratorWindowCommand { get; }
        private ReactiveCommand<Unit, Unit> OpenAboutWindowCommand { get; }
        private ReactiveCommand<Unit, Unit> OpenSettingsWindowCommand { get; }
        private ReactiveCommand<Unit, Unit> CheckUpdateCommand { get; }
        private ReactiveCommand<Unit, Unit> LockAllDatabasesCommand { get; }
        private ReactiveCommand<Unit, Unit> SaveAllDatabasesCommand { get; }
        private ReactiveCommand<Unit, Unit> UnlockAllDatabasesCommand { get; }
        private ReactiveCommand<Unit, Unit> OpenCheckingWeakPasswordsWindowCommand { get; }
        private ReactiveCommand<Unit, Unit> ClearGCCommand { get; }
        private ReactiveCommand<Unit, Unit> OpenTrashWindowCommand { get; }

        #endregion

        #region Propertie's

        public ObservableCollection<TabItem> TabItems
        {
            get => _tabItems;
            set => this.RaiseAndSetIfChanged(ref _tabItems, value);
        }
        public int CountLogins
        {
            get => _countLogins;
            set => this.RaiseAndSetIfChanged(ref _countLogins, value);
        }
        public bool IsUnlockDatabase
        {
            get => _isUnlockDatabase;
            set => this.RaiseAndSetIfChanged(ref _isUnlockDatabase, value);
        }
        private bool IsLockDatabase
        {
            get => _isLockDatabase;
            set => this.RaiseAndSetIfChanged(ref _isLockDatabase, value);
        }
        public TabItem SelectedTabItem
        {
            get => _selectedTabItem;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedTabItem, value);
                if (SelectedTabItem != null)
                {
                    IsUnlockDatabase = ((DatabaseControl)SelectedTabItem.Content).ViewModel.IsUnlockDatabase;
                    IsLockDatabase = ((DatabaseControl)SelectedTabItem.Content).ViewModel.IsLockDatabase;
                    CountLogins = ((DatabaseControl)SelectedTabItem.Content).ViewModel.LoginList.Count;
                }
                else
                {
                    IsUnlockDatabase = false;
                    IsLockDatabase = false;
                }
            }
        }
        public string[] OpenStorages { get; set; }

        #endregion

        public MainWindowViewModel()
        {
            ExitProgramCommand = ReactiveCommand.Create(ExitApplication);
            CreateLoginCommand = ReactiveCommand.Create(CreateLogin);
            OpenSettingsWindowCommand = ReactiveCommand.Create(OpenSettingsWindow);
            CreateDatabaseCommand = ReactiveCommand.Create(CreateDatabase);
            SaveDatabaseCommand = ReactiveCommand.Create(() => SaveDatabase((DatabaseControl)SelectedTabItem.Content));
            UnlockDatabaseCommand = ReactiveCommand.Create(UnlockDatabase);
            LockDatabaseCommand = ReactiveCommand.Create(LockDatabase);
            OpenDatabaseCommand = ReactiveCommand.Create(OpenDatabase);
            CheckUpdateCommand = ReactiveCommand.Create(CheckUpdate);
            ShowSearchWindowCommand = ReactiveCommand.Create(ShowSearchWindow);
            OpenPasswordGeneratorWindowCommand = ReactiveCommand.Create(() => { new PasswordGeneratorWindow().ShowDialog(App.MainWindow); });
            OpenAboutWindowCommand = ReactiveCommand.Create(() => { new AboutWindow().ShowDialog(App.MainWindow); });
            ChangeMasterPasswordCommand = ReactiveCommand.Create(ChangeMasterPassword);
            LockAllDatabasesCommand = ReactiveCommand.Create(LockAllDatabases);
            SaveAllDatabasesCommand = ReactiveCommand.Create(SaveAllDatabases);
            UnlockAllDatabasesCommand = ReactiveCommand.Create(UnlockAllDatabases);
            OpenCheckingWeakPasswordsWindowCommand = ReactiveCommand.Create(OpenCheckingWeakPasswordsWindow);
            ClearGCCommand = ReactiveCommand.Create(ClearGC);
            OpenTrashWindowCommand = ReactiveCommand.Create(OpenTrashWindow);

            App.Autoblock.Tick += AutoblockStorage;
        }

        public void Loading()
        {
            for (int i = 0; i < OpenStorages.Length; i++)
            {
                string item = OpenStorages[i];
                if (Path.GetExtension(item) == ".olib")
                    CreateTab(Program.Settings.PathDatabase = item, true, false);
            }

            if (OpenStorages.Length > 0)
            {
                OpenStorages = null;
                return;
            }

            if (!string.IsNullOrEmpty(Program.Settings.PathDatabase) && File.Exists(Program.Settings.PathDatabase))
                CreateTab(Program.Settings.PathDatabase, true, false);
        }
        public void OpenStorageDnD(IEnumerable<string> files)
        {
            foreach (string item in files.Where(item => Path.GetExtension(item) == ".olib").Where(item =>
                TabItems.All(i => ((DatabaseControl)i.Content).ViewModel.PathDatabase != item)))
                CreateTab(Program.Settings.PathDatabase = item, true, false);

            App.MainWindow.MessageStatusBar((string)Application.Current.FindResource("Not6"));
        }
        public void ProgramClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSettings();
            for (int index = 0; index < TabItems.Count; index++) SaveDatabase((DatabaseControl)TabItems[index].Content);
        }
        public void SaveDatabase(DatabaseControl db)
        {
            if (db == null || db.ViewModel.IsLockDatabase || !db.ViewModel.IsUnlockDatabase) return;
            if (db.ViewModel.PathDatabase != null)
            {
                db.ViewModel.Database.Logins = db.ViewModel.LoginList.Select(item => item.LoginItem).ToList();
                SaveAndLoadDatabase.SaveFiles(db);
                App.MainWindow.MessageStatusBar((string)Application.Current.FindResource("Not4") + $" {Path.GetFileNameWithoutExtension(db.ViewModel.PathDatabase)}");
            }
            for (int i = 0; i < db.ViewModel.Router.NavigationStack.Count - 1; i++)
                db.ViewModel.Router.NavigationStack.Remove(db.ViewModel.Router.NavigationStack[i]);
        }
        private void CreateTab(string path, bool isLockDatabase, bool isUnlockDatabase)
        {
            string id = Guid.NewGuid().ToString("N");

            DatabaseControl db = new DatabaseControl
            {
                ViewModel =
                        {
                            Database = new Database(),
                            IsLockDatabase = isLockDatabase,
                            IsUnlockDatabase = isUnlockDatabase,
                            PathDatabase = path,
                            TabID = id
                        }
            };
            DatabaseTabHeader tabHeader = new DatabaseTabHeader(id, Path.GetFileNameWithoutExtension(Program.Settings.PathDatabase))
            {
                CloseTab = CloseTab,
                iLock = { IsVisible = true },
                iUnlock = { IsVisible = false }
            };

            TabItems.Add(new TabItem { Header = tabHeader, Content = db });

            if(Equals((DatabaseControl)SelectedTabItem.Content, db))
            {
                IsLockDatabase = true;
                IsUnlockDatabase = false;
            }

            new RequireMasterPasswordWindow
            {
                LoadStorageCallback = LoadDatabase,
                databaseControl = db,
                databaseTabHeader = tabHeader,
                tbNameStorage = { Text = Path.GetFileNameWithoutExtension(Program.Settings.PathDatabase) }
            }.ShowDialog(App.MainWindow);
        }
        private void OpenCheckingWeakPasswordsWindow()
        {
            if (SelectedTabItem == null || ((DatabaseControl)SelectedTabItem.Content).ViewModel.IsLockDatabase) return;
            CheckingWindow = new CheckingWeakPasswordsWindow();
            CheckingWindow.ShowDialog(App.MainWindow);

            ((DatabaseControl)SelectedTabItem.Content).ViewModel.SelectedIndex = -1;
            ((DatabaseControl)SelectedTabItem.Content).ViewModel.Router.Navigate.Execute(new StartPageViewModel());
        }
        private void CloseTab(string tabID)
        {
            for (int i = 0; i < TabItems.Count; i++)
            {
                DatabaseControl item = (DatabaseControl)TabItems[i].Content;
                if (item.ViewModel.TabID == tabID)
                {
                    SaveDatabase(item);

                    TabItems.RemoveAt(i);
                    break;
                }
            }
        }
        private void AutoblockStorage(object sender, EventArgs e)
        {
            App.Autoblock.Stop();

            App.SearchWindow?.Close();
            _windowChangeMasterPassword?.Close();
            _settingsWindow?.Close();
            PasswordGenerator?.Close();
            CheckingWindow?.Close();
            TrashWindow?.Close();

            for (int index = 0; index < TabItems.Count; index++)
            {
                DatabaseControl control = (DatabaseControl)TabItems[index].Content;
                DatabaseTabHeader header = (DatabaseTabHeader)TabItems[index].Header;

                if (control.ViewModel.IsUnlockDatabase && header != null)
                {
                    if (control.ViewModel.LoginList.Any(login => login.LoginItem.IsReminderActive)) continue;

                    SaveDatabase(control);
                    control.ViewModel.ClearLoginsList();
                    IsUnlockDatabase = control.ViewModel.IsUnlockDatabase = header.iUnlock.IsVisible = false;
                    IsLockDatabase = control.ViewModel.IsLockDatabase = header.iLock.IsVisible = true;
                    control.ViewModel.Router.Navigate.Execute(new StartPageViewModel(control.ViewModel));
                }
            }
        }
        private async void UnlockDatabase()
        {
            if (SelectedTabItem == null || ((DatabaseControl)SelectedTabItem.Content).ViewModel.IsUnlockDatabase) return;

            await new RequireMasterPasswordWindow
            {
                LoadStorageCallback = LoadDatabase,
                databaseControl = (DatabaseControl)SelectedTabItem.Content,
                databaseTabHeader = (DatabaseTabHeader)SelectedTabItem.Header,
                tbNameStorage = { Text = Path.GetFileNameWithoutExtension(((DatabaseControl)SelectedTabItem.Content).ViewModel.PathDatabase) }
            }.ShowDialog(App.MainWindow);
        }
        private async void UnlockAllDatabases()
        {
            for (int i = 0; i < TabItems.Count; i++)
            {
                TabItem item = TabItems[i];
                if (((DatabaseControl)item.Content).ViewModel.IsLockDatabase)
                {
                    await new RequireMasterPasswordWindow
                    {
                        LoadStorageCallback = LoadDatabase,
                        databaseControl = (DatabaseControl)item.Content,
                        databaseTabHeader = (DatabaseTabHeader)item.Header,
                        tbNameStorage = { Text = Path.GetFileNameWithoutExtension(((DatabaseControl)item.Content).ViewModel.PathDatabase) }
                    }.ShowDialog(App.MainWindow);
                }
            }

            App.MainWindow.MessageStatusBar((string)Application.Current.FindResource("Not8"));
        }
        private void LockDatabase()
        {
            if (SelectedTabItem == null || ((DatabaseControl)SelectedTabItem.Content).ViewModel.IsLockDatabase) return;

            SaveDatabase((DatabaseControl)SelectedTabItem.Content);
            ((DatabaseControl)SelectedTabItem.Content).ViewModel.ClearLoginsList();
            IsUnlockDatabase = ((DatabaseControl)SelectedTabItem.Content).ViewModel.IsUnlockDatabase = ((DatabaseTabHeader)SelectedTabItem.Header).iUnlock.IsVisible = false;
            IsLockDatabase = ((DatabaseControl)SelectedTabItem.Content).ViewModel.IsLockDatabase = ((DatabaseTabHeader)SelectedTabItem.Header).iLock.IsVisible = true;
            ((DatabaseControl)SelectedTabItem.Content).ViewModel.Router.Navigate.Execute(new StartPageViewModel(((DatabaseControl)SelectedTabItem.Content).ViewModel));
        }
        private void LockAllDatabases()
        {
            for (int index = 0; index < TabItems.Count; index++)
            {
                DatabaseControl control = (DatabaseControl)TabItems[index].Content;
                DatabaseTabHeader header = (DatabaseTabHeader)TabItems[index].Header;

                SaveDatabase(control);
                if (header != null)
                {
                    control.ViewModel.ClearLoginsList();
                    IsUnlockDatabase = control.ViewModel.IsUnlockDatabase = header.iUnlock.IsVisible = false;
                    IsLockDatabase = control.ViewModel.IsLockDatabase = header.iLock.IsVisible = true;
                    control.ViewModel.Router.Navigate.Execute(new StartPageViewModel(control.ViewModel));
                }
            }

            App.MainWindow.MessageStatusBar((string)Application.Current.FindResource("Not7"));
        }
        private async void OpenDatabase()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog { AllowMultiple = true };
                dialog.Filters.Add(new FileDialogFilter { Name = (string)Application.Current.FindResource("FileOlib"), Extensions = { "olib" } });
                List<string> files = (await dialog.ShowAsync(App.MainWindow)).ToList();

                if (files.Count == 0) return;

                foreach (string file in files.Where(file =>
                    TabItems.All(i => ((DatabaseControl)i.Content).ViewModel.PathDatabase != file)))
                    CreateTab(Program.Settings.PathDatabase = file, true, false);
            }
            catch
            {
                // ignored
            }
        }
        private void CreateLogin()
        {
            if (SelectedTabItem == null || ((DatabaseControl)SelectedTabItem.Content).ViewModel.IsLockDatabase) return;

            ((DatabaseControl)SelectedTabItem.Content).ViewModel.SelectedIndex = -1;

            ((DatabaseControl)SelectedTabItem.Content).ViewModel.Router.Navigate.Execute(new CreateLoginPageViewModel(((DatabaseControl)SelectedTabItem.Content).ViewModel.Database, ((DatabaseControl)SelectedTabItem.Content).ViewModel)
            {
                BackPageCallback = ((DatabaseControl)SelectedTabItem.Content).ViewModel.StartPage,
                CreateLoginCallback = ((DatabaseControl)SelectedTabItem.Content).ViewModel.AddLogin
            });
        }
        private void LoadDatabase(DatabaseControl db, DatabaseTabHeader dbHeader)
        {
            db.ViewModel.Database = SaveAndLoadDatabase.LoadFiles(db);
            for (int index = 0; index < db.ViewModel.Database.Logins.Count; index++)
                db.ViewModel.AddLogin(db.ViewModel.Database.Logins[index]);

            db.ViewModel.IsLockDatabase = dbHeader.iLock.IsVisible = false;
            db.ViewModel.IsUnlockDatabase = dbHeader.iUnlock.IsVisible = true;

            if (Equals((DatabaseControl)SelectedTabItem.Content, db))
            {
                IsLockDatabase = false;
                IsUnlockDatabase = true;
            }

            if (Program.Settings.AutoRemoveItemsTrash)
            {
                if (db.ViewModel.Database.Trash != null)
                {
                    for (int i = db.ViewModel.Database.Trash.Logins.Count - 1; i > -1; i--)
                        if (DateTime.Parse(db.ViewModel.Database.Trash.Logins[i].DeleteDate, System.Threading.Thread.CurrentThread.CurrentUICulture).AddDays(Program.Settings.DaysAfterDeletion) <= DateTime.Now)
                            db.ViewModel.Database.Trash.Logins.Remove(db.ViewModel.Database.Trash.Logins[i]);

                    for (int i = db.ViewModel.Database.Trash.Folders.Count - 1; i > -1; i--)
                        if (DateTime.Parse(db.ViewModel.Database.Trash.Folders[i].DeleteDate, System.Threading.Thread.CurrentThread.CurrentUICulture).AddDays(Program.Settings.DaysAfterDeletion) <= DateTime.Now)
                            db.ViewModel.Database.Trash.Folders.Remove(db.ViewModel.Database.Trash.Folders[i]);
                }
            }

            App.MainWindow.MessageStatusBar((string)Application.Current.FindResource("Not9") + $" {Path.GetFileNameWithoutExtension(db.ViewModel.PathDatabase)}");
        }
        private async void CreateDatabase()
        {
            CreateDatabaseWindow window = new CreateDatabaseWindow();
            if (await window.ShowDialog<bool>(App.MainWindow))
            {
                string id = Guid.NewGuid().ToString("N");

                DatabaseTabHeader tabHeader = new DatabaseTabHeader(id, Path.GetFileNameWithoutExtension(Program.Settings.PathDatabase = window.TbPathDatabase.Text))
                {
                    CloseTab = CloseTab,
                    iLock = { IsVisible = false },
                    iUnlock = { IsVisible = true }
                };

                DatabaseControl db = new DatabaseControl
                {
                    ViewModel =
                    {
                        Database = new Database { Folders = new List<Folder>(), Logins = new List<Login>() },
                        MasterPassword = window.TbPassword.Text,
                        Iterations = int.Parse(window.TbIteration.Text),
                        NumberOfEncryptionProcedures = int.Parse(window.TbNumberOfEncryptionProcedures.Text),
                        IsLockDatabase = false,
                        IsUnlockDatabase = true,
                        PathDatabase = window.TbPathDatabase.Text,
                        TabID = id,
                        UseCompression = window.CbUseCompression.IsChecked ?? false,
                        UseTrash = window.CbUseTrash.IsChecked ?? false
                    }
                };

                TabItems.Add(new TabItem { Header = tabHeader, Content = db });

                SaveDatabase(db);
                App.MainWindow.MessageStatusBar((string)Application.Current.FindResource("Not3") + $" {Path.GetFileNameWithoutExtension(Program.Settings.PathDatabase)}");
            }
        }
        private void ShowSearchWindow()
        {
            if (SelectedTabItem == null || ((DatabaseControl)SelectedTabItem.Content).ViewModel.IsLockDatabase) return;
            App.SearchWindow = new SearchWindow();

            for (int index = 0; index < ((DatabaseControl)SelectedTabItem.Content).ViewModel.Database.Folders.Count; index++)
                App.SearchWindow.SearchViewModel.AddFolder(((DatabaseControl)SelectedTabItem.Content).ViewModel.Database.Folders[index]);

            App.SearchWindow.ShowDialog(App.MainWindow);
        }
        private void SaveAllDatabases()
        {
            for (int index = 0; index < TabItems.Count; index++)
                SaveDatabase((DatabaseControl)TabItems[index].Content);
        }
        private void OpenSettingsWindow()
        {
            _settingsWindow = new SettingsWindow();
            _settingsWindow.ShowDialog(App.MainWindow);
        }
        private void ChangeMasterPassword()
        {
            _windowChangeMasterPassword = new ChangeMasterPasswordWindow();
            _windowChangeMasterPassword.ShowDialog(App.MainWindow);
        }
        private void ClearGC()
        {
            for (int index = 0; index < TabItems.Count; index++)
            {
                DatabaseControl item = (DatabaseControl)TabItems[index].Content;
                for (int i = 0; i < item.ViewModel.Router.NavigationStack.Count; i++)
                    item.ViewModel.Router.NavigationStack.Remove(item.ViewModel.Router.NavigationStack[i]);
            }
            GC.Collect(0, GCCollectionMode.Forced);
            App.MainWindow.MessageStatusBar((string)Application.Current.FindResource("ClearGC"));
        }
        private void OpenTrashWindow()
        {
            if (IsUnlockDatabase)
            {
                TrashWindow = new TrashWindow();
                TrashWindow.ShowDialog(App.MainWindow);
            }
        }

        private void CheckUpdate() => App.CheckUpdate(true);
        private void ExitApplication() => App.MainWindow.Close();
        private void SaveSettings() => SaveAndLoadSettings.SaveSettings();
    }
}