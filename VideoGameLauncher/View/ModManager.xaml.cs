using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            db = new ModDBContainer();
        }

        private void LoadAvailableMods()
        {
            var query =
                from m in db.Mods
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
                };

            dataGridDownloadableMods.ItemsSource = query.ToList();
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
                    owner.CreateMsgBox("Error: Invalid profile type added.", error.Message);
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
                    owner.CreateMsgBox("Error: Current profile is empty.", error.Message);
                }
            }

        }

        private void CreateMod_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteMod_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Sidebar Controls

        private void IncreasePriority_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DecreasePriority_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ApplyMods_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RestoreFiles_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LaunchGame_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
