using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
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

namespace VideoGameLauncher.View
{
    /// <summary>
    /// Modification Manager Window
    /// This window allows users to download, add and load new mods.
    /// </summary>
    public partial class ModManager : MetroWindow
    {
        #region Properties

        private MainWindow owner;
        private ObservableCollection<string> profiles;
        public ObservableCollection<object> AppliedMods;
        private ModDBContainer db;

        #endregion

        #region Initialize

        public ModManager()
        {
            InitializeComponent();
        }

        private void ModManager_Loaded(object sender, RoutedEventArgs e)
        {
            owner = Owner as MainWindow;

            profiles = new ObservableCollection<string>();
            cbxProfiles.ItemsSource = profiles;

            LoadDatabase();
            LoadAvailableMods();
        }

        #endregion

        #region Database Initialize

        private void LoadDatabase()
        {
            //using (db = new ModDBContainer())
            //{
            //    db.Database.Connection.Open();
            //}

            db = new ModDBContainer();
        }

        private async void LoadAvailableMods()
        {
            // Show Progress Ring
            progressRing.IsActive = true;

            var query = await // Wait for this query to load
                (from m in db.Mods
                join a in db.Authors on m.Id equals a.Id
                select new
                {
                    ModId = m.Id,
                    AuthorId = a.Id,
                    Name = m.Name,
                    Author = a.Name,
                    Version = m.Version,
                    Description = m.Description,
                    Warnings = m.Warnings,
                    Location = m.Location
                }).ToListAsync(); // Do this asynchronous casting task
            // Continue

            dataGridDownloadableMods.ItemsSource = query;
            AppliedMods = new ObservableCollection<object>();
            dataGridMyMods.ItemsSource = AppliedMods;

            // Set Footer Count
            lblDownloadableModsCount.Content = query.Count();
            lblMyModsCount.Content = AppliedMods.Count;

            // Hide Progress Ring
            progressRing.IsActive = false;
        }

        #endregion

        #region Header Controls

        private async void btnAddProfile_Click(object sender, RoutedEventArgs e)
        {
            var result = await this.ShowInputAsync("Add Profile", "Enter your new profile name:");

            if (result == null) // User pressed cancel.
                return;
            else
            {
                try
                {
                    profiles.Add(result);
                    cbxProfiles.SelectedItem = result;
                }
                catch (InvalidCastException error)
                {
                    MainWindow.CreateMsgBox("Error: Invalid profile type added.", error.Message);
                }
            }
        }

        private void btnDeleteProfile_Click(object sender, RoutedEventArgs e)
        {
            if (cbxProfiles.SelectedItem != null)
            {
                try
                {
                    profiles.Remove(cbxProfiles.SelectedItem.ToString());
                    cbxProfiles.SelectedIndex = 0;
                }
                catch (NullReferenceException error)
                {
                    MainWindow.CreateMsgBox("Error: Current profile is empty.", error.Message);
                }
            }

        }

        private void CreateMod_Click(object sender, RoutedEventArgs e)
        {
            CreateNewMod();
        }

        private void DeleteMod_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridMyMods.SelectedItem != null)
            {
                AppliedMods.Remove(dataGridMyMods.SelectedItem);

                // Set Footer Count
                lblMyModsCount.Content = AppliedMods.Count;
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Sidebar Controls

        private void IncreasePriority_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridMyMods.SelectedItem == null)
                return;

            int currentIndex = AppliedMods.IndexOf(dataGridMyMods.SelectedItem);
            if (currentIndex > 0)
                AppliedMods.Move(currentIndex, currentIndex - 1);
        }

        private void DecreasePriority_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridMyMods.SelectedItem == null)
                return;

            int currentIndex = AppliedMods.IndexOf(dataGridMyMods.SelectedItem);
            if (currentIndex != -1 && currentIndex < AppliedMods.Count - 1)
                AppliedMods.Move(currentIndex, currentIndex + 1);
        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(owner.BasePath);
        }

        private async void DownloadMods_Click(object sender, RoutedEventArgs e)
        {
            var selectedMods = dataGridDownloadableMods.SelectedItems;

            var mySettings = new MetroDialogSettings()
            {
                NegativeButtonText = "Cancel",
                AnimateShow = false,
                AnimateHide = false
            };

            var controller = await this.ShowProgressAsync("Downloading Mods", "Please wait ...", settings: mySettings);
            controller.SetIndeterminate();
            controller.SetCancelable(true); // Shows cancel button

            await Task.Delay(4000);
            await controller.CloseAsync();

            if (controller.IsCanceled) // End operation
            {
                // No mods have been downloaded
                MainWindow.CreateMsgBox("Operation Cancelled", "No mods have been downloaded.");
            }
            else
            {
                // Add selected mods to applied mods collection
                try
                {
                    foreach (var item in selectedMods)
                    {
                        AppliedMods.Add(item);
                    }
                }
                catch (NullReferenceException error)
                {
                    MainWindow.CreateMsgBox("Error: Applied mod is corrupt.", error.Message);
                }

                // Update Footer Count
                lblMyModsCount.Content = AppliedMods.Count;
            }
        }

        private void ApplyMods_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RestoreFiles_Click(object sender, RoutedEventArgs e)
        {
            AppliedMods.Clear();

            // Update Footer Count
            lblMyModsCount.Content = AppliedMods.Count;
        }

        private void LaunchGame_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Dialog Events

        private void CreateNewMod()
        {
            // Create new window object
            var window = new ModBox();

            // Set the owner of this new object
            window.Owner = this;

            // Display the new window
            window.ShowDialog();
        }

        #endregion
    }
}
