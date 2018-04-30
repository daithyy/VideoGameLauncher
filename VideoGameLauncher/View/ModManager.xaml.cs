using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
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

        #endregion

        #region Initialize

        public ModManager()
        {
            InitializeComponent();
        }

        private void ModManager_Loaded(object sender, RoutedEventArgs e)
        {
            owner = Owner as MainWindow;
        }

        #endregion

        private void CreateMod_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteMod_Click(object sender, RoutedEventArgs e)
        {

        }

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

        private void btnAddProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileHandler((Button)sender);
        }

        private async void ProfileHandler(Button sender)
        {
            await this.ShowMessageAsync("This is the title", "Some message");
        }
    }
}
