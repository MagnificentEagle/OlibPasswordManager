﻿using Avalonia;
using Avalonia.Controls;
using OlibKey.Structures;
using OlibKey.Views.Controls;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using OtpNet;
using Avalonia.Threading;
using OlibKey.Views.Windows;

namespace OlibKey.ViewModels.Pages
{
    public class CreateLoginPageViewModel : ReactiveObject, IRoutableViewModel
    {
        private int _selectionFolderIndex;
        private ObservableCollection<CustomFieldListItem> _customFields;
        private ObservableCollection<ImportedFileListItem> _importedFiles;

        private Totp Totp;

        private string _generatedCode;
        private int _timeLeft;

        private DispatcherTimer Timer;

        #region Property's

        private int SelectionFolderIndex
        {
            get => _selectionFolderIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectionFolderIndex, value);
                NewLogin.FolderID = SelectionFolderItem.ID;
            }
        }
        private string GeneratedCode
        {
            get => _generatedCode;
            set => this.RaiseAndSetIfChanged(ref _generatedCode, value);
        }
        private int TimeLeft
        {
            get => _timeLeft;
            set => this.RaiseAndSetIfChanged(ref _timeLeft, value);
        }
        private ObservableCollection<CustomFieldListItem> CustomFields
        {
            get => _customFields;
            set => this.RaiseAndSetIfChanged(ref _customFields, value);
        }
        private ObservableCollection<ImportedFileListItem> ImportedFiles
        {
            get => _importedFiles;
            set => this.RaiseAndSetIfChanged(ref _importedFiles, value);
        }
        private int Type { get; set; }
        private Login NewLogin { get; set; }
        private Folder SelectionFolderItem { get { try { return Folders[SelectionFolderIndex]; } catch { return null; } } }
        private ObservableCollection<Folder> Folders { get; set; }

        #endregion

        public Action BackPageCallback { get; set; }
        public Action<Login> CreateLoginCallback { get; set; }

        // routing
        public string UrlPathSegment => "/addLogin";
        public IScreen HostScreen { get; }

        public CreateLoginPageViewModel(Database db, IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            NewLogin = new Login
            {
                CustomFields = new List<CustomField>(),
                ImportedFiles = new List<ImportedFile>()
            };
            CustomFields = new ObservableCollection<CustomFieldListItem>();
            ImportedFiles = new ObservableCollection<ImportedFileListItem>();

            Folders = new ObservableCollection<Folder>
            {
                new Folder { ID = null, Name = (string)Application.Current.FindResource("NotChosen") }
            };

            if (db.Folders != null) foreach (Folder i in db.Folders) Folders.Add(i);

            SelectionFolderIndex = Type = 0;
        }

        private void CreateLogin()
        {
            NewLogin.CustomFields.AddRange(CustomFields.Select(item => item.HousingElement.CustomField));
            NewLogin.ImportedFiles.AddRange(ImportedFiles.Select(item => item.DataContext as ImportedFile));
            NewLogin.TimeCreate = DateTime.Now.ToString(CultureInfo.CurrentCulture);
            if (!NewLogin.UseColor) NewLogin.Color = null;
            CreateLoginCallback?.Invoke(NewLogin);
            App.MainWindow.MessageStatusBar((string)Application.Current.FindResource("Not1"));
        }
        private void AddCustomField()
        {
            CustomFields.Add(new CustomFieldListItem(new Housing
            {
                CustomField = new CustomField { Type = Type },
                IsEnabled = true
            })
            {
                ID = Guid.NewGuid().ToString("N"),
                DeleteCustomField = DeleteCustomField
            });
        }
        private async void ImportFile()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog { AllowMultiple = true };

                List<string> files = (await dialog.ShowAsync(App.MainWindow)).ToList();

                foreach (var file in files)
                {
                    ImportedFiles.Add(new ImportedFileListItem { 
                        DataContext = new ImportedFile { Name = Path.GetFileName(file) , Data = Core.FileInteractions.ImportFile(file) }, 
                        ID = Guid.NewGuid().ToString("N"),
                        DeleteFileCallback = DeleteImportedFile
                    });
                }

            }
            catch { }
        }
        private void DeleteCustomField(string id)
        {
            foreach (CustomFieldListItem item in CustomFields.Where(item => item.ID == id))
            {
                CustomFields.Remove(item);
                break;
            }
        }
        private void DeleteImportedFile(string id)
        {
            foreach (ImportedFileListItem item in ImportedFiles.Where(item => item.ID == id))
            {
                ImportedFiles.Remove(item);
                break;
            }
        }
        private void Back() => BackPageCallback?.Invoke();
        private async void ActivateTOTP()
        {
            try
            {
                if (Timer != null && Timer.IsEnabled) Timer.Stop();

                Totp = new Totp(Base32Encoding.ToBytes(NewLogin.SecretKey.Replace(" ", "")), step: 30, timeCorrection: new TimeCorrection(DateTime.UtcNow));
                GeneratedCode = Totp.ComputeTotp();
                TimeLeft = Totp.RemainingSeconds();
                Timer = new DispatcherTimer
                {
                    Interval = new TimeSpan(0, 0, 0, 0, 50)
                };
                Timer.Tick += Timer_Tick;
                Timer.Start();
            }
            catch
            {
                await MessageBox.Show(App.MainWindow, null,
                    (string)Application.Current.FindResource("MB10"), (string)Application.Current.FindResource("Error"),
                    MessageBox.MessageBoxButtons.Ok, MessageBox.MessageBoxIcon.Error);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            GeneratedCode = Totp.ComputeTotp();
            TimeLeft = Totp.RemainingSeconds();
        }
    }
}