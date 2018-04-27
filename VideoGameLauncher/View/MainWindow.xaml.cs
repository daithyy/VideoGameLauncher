using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using MahApps.Metro.Controls;
using Svg2Xaml;
using System.Diagnostics;
using Newtonsoft.Json;
using VideoGameLauncher.Classes;

namespace VideoGameLauncher
{
    public partial class MainWindow : MetroWindow
    {
        /*
         * Extra Features:
         * MahApps Metro Theme
         * Extended WPF Toolkit Color Picker
         * (Expired license requires a reinstall from NuGet Package Manager)
         */

        #region Properties

        JsonSerializerSettings settings;
        public readonly string BasePath = Directory.GetCurrentDirectory();
        private readonly string LogoFilePath = "Images\\logo.svg";

        #endregion

        #region Initialize

        public MainWindow()
        {
            InitializeComponent();

            // Load SVG Image using SVG2XAML converter.
            using (FileStream stream = new FileStream(LogoFilePath, FileMode.Open, FileAccess.Read))
                try
                {
                    imgLogo.Source = SvgReader.Load(stream);
                }
                catch (FileNotFoundException exception)
                {
                    TextBlock error_text_block = new TextBlock();
                    error_text_block.Text = exception.Message;
                    Content = error_text_block;
                }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Set JSON Serializing settings for saving/loading data.
            settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            // Load Flyout Window Content
            CreatePlayerCustomizeData();
        }

        private void CreatePlayerCustomizeData()
        {
            #region Weapons

            List<Weapon> Weapons = new List<Weapon>()
            {
                new Weapon("Assault Rifle", 7.5f, 32),
                new Weapon("Battle Rifle", 6, 36),
                new Weapon("Sniper Rifle", 80, 4),
                new Weapon("Shotgun", 150, 6),
                new Weapon("DMR Rifle", 17.5f, 15),
                new Weapon("Magnum Pistol", 15, 8),
                new Weapon("SMG", 4.8f, 60),
                new Weapon("Grenade Launcher", 150, 1),
                new Weapon("Spartan Laser", 284, 1),
                new Weapon("Rocket Launcher", 220, 2)
            };

            cbxWeapon.ItemsSource = Weapons.OrderBy(w => w.Name);
            cbxWeapon.SelectedIndex = 0;

            #endregion

            #region Armors

            // All Armor Permutations between Helmet, Shoulders and Chest are the same.

            List<Armor> Armors = new List<Armor>()
            {
                new Armor("Mark VI"),
                new Armor("CQB"),
                new Armor("EVA"),
                new Armor("EOD"),
                new Armor("Hayabusa"),
                new Armor("Security"),
                new Armor("Recon"),
                new Armor("ODST"),
                new Armor("Mark V"),
                new Armor("Rogue")
            };

            cbxHelmet.ItemsSource = Armors;
            cbxHelmet.SelectedIndex = 0;

            cbxShoulders.ItemsSource = Armors;
            cbxShoulders.SelectedIndex = 0;

            cbxChest.ItemsSource = Armors;
            cbxChest.SelectedIndex = 0;

            #endregion

            #region Wrist

            List<Armor> Wrists = new List<Armor>()
            {
                new Armor("Mark VI"),
                new Armor("Breacher"),
                new Armor("Assault"),
                new Armor("TacPad"),
                new Armor("UGPS"),
                new Armor("Buckler"),
                new Armor("Bracer")
            };

            cbxWrist.ItemsSource = Wrists;
            cbxWrist.SelectedIndex = 0;

            #endregion

            #region Utility

            List<Armor> Utilties = new List<Armor>()
            {
                new Armor("None"),
                new Armor("Hard Case"),
                new Armor("Trauma Kit"),
                new Armor("Soft Case"),
                new Armor("Chobham"),
                new Armor("NxRA")
            };

            cbxUtility.ItemsSource = Utilties;
            cbxUtility.SelectedIndex = 0;

            #endregion
        }

        #endregion

        #region Flyout Controls

        private async void FlyoutHandler(Flyout sender)
        {
            sender.IsOpen = true;

            foreach (Flyout fly in allFlyouts.FindChildren<Flyout>())
                if (fly.Header != sender.Header)
                {
                    await Task.Run(() => AsyncFlyoutHandler(fly));
                }

            sender.IsOpen = true;
        }

        private void AsyncFlyoutHandler(Flyout fly)
        {
            Dispatcher.Invoke(() =>
            {
                fly.IsOpen = false;
            });
        }

        #endregion

        #region Click Events

        // Main Window Toolbar Click Events
        private void GitHub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/s00172994/VideoGameLauncher");
        }

        // Flyout Click Events
        private void PlayerCustomize_Click(object sender, RoutedEventArgs e)
        {
            FlyoutHandler(FlyoutPlayerCustomize);
        }

        // Player Customization Click Events
        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
