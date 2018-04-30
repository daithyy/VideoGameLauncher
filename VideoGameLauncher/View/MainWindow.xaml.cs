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
using Microsoft.Win32;
using VideoGameLauncher.View;

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

        #region Flyout Data

        private void CreatePlayerCustomizeData()
        {
            // Set Player Customization property defaults.

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

        private void LoadPlayerCustomizeData(Player player)
        {
            // Set Player Customization properties to the loaded player.

            #region Name

            try
            {
                NameBox.Text = player.Name;
            }
            catch (NullReferenceException error)
            {
                CreateMsgBox("Error: Player name is corrupt.\n", error.Message);
            }

            #endregion

            #region Weapon

            try
            {
                // Set the index of the current list to match the loaded player's weapon.
                cbxWeapon.SelectedIndex = cbxWeapon.ItemsSource
                    .OfType<Weapon>()
                    .ToList()
                    .FindIndex(w => w.Name.Contains(player.CurrentWeapon.Name));
            }
            catch (NullReferenceException error)
            {
                CreateMsgBox("Error: Weapon not found.\n", error.Message);
            }

            #endregion

            #region Armors

            try
            {
                cbxHelmet.SelectedIndex = cbxHelmet.ItemsSource
                    .OfType<Armor>()
                    .ToList()
                    .FindIndex(a => a.Name.Contains(player.Helmet.Name));
            }
            catch (NullReferenceException error)
            {
                CreateMsgBox("Error: Helmet not found.\n", error.Message);
            }


            try
            {
                cbxShoulders.SelectedIndex = cbxShoulders.ItemsSource
                    .OfType<Armor>()
                    .ToList()
                    .FindIndex(a => a.Name.Contains(player.Shoulders.Name));
            }
            catch (NullReferenceException error)
            {
                CreateMsgBox("Loaded Shoulder not found.\n", error.Message);
            }


            try
            {
                cbxChest.SelectedIndex = cbxChest.ItemsSource
                    .OfType<Armor>()
                    .ToList()
                    .FindIndex(a => a.Name.Contains(player.Chest.Name));
            }
            catch (NullReferenceException error)
            {
                CreateMsgBox("Loaded Chest not found.\n", error.Message);
            }

            #endregion

            #region Wrist

            try
            {
                cbxWrist.SelectedIndex = cbxWrist.ItemsSource
                    .OfType<Armor>()
                    .ToList()
                    .FindIndex(a => a.Name.Contains(player.Wrist.Name));
            }
            catch (NullReferenceException error)
            {
                CreateMsgBox("Error: Wrist not found.\n", error.Message);
            }

            #endregion

            #region Utility

            try
            {
                cbxUtility.SelectedIndex = cbxUtility.ItemsSource
                    .OfType<Armor>()
                    .ToList()
                    .FindIndex(a => a.Name.Contains(player.Utility.Name));
            }
            catch (NullReferenceException error)
            {
                CreateMsgBox("Error: Utility not found.\n", error.Message);
            }

            #endregion

            #region Armor Colors

            try
            {
                ClrPrimary.SelectedColor = player.PrimaryColour;
            }
            catch (NullReferenceException error)
            {
                CreateMsgBox("Error: Empty Primary Color.\n", error.Message);
            }

            try
            {
                ClrSecondary.SelectedColor = player.SecondaryColour;
            }
            catch (NullReferenceException error)
            {
                CreateMsgBox("Error: Empty Secondary Color.\n", error.Message);
            }

            try
            {
                ClrLights.SelectedColor = player.LightsColour;
            }
            catch (NullReferenceException error)
            {
                CreateMsgBox("Error: Empty Lights Color.\n", error.Message);
            }

            try
            {
                ClrVisor.SelectedColor = player.VisorColour;
            }
            catch (NullReferenceException error)
            {
                CreateMsgBox("Error: Empty Visor Color.\n", error.Message);
            }

            try
            {
                ClrHolo.SelectedColor = player.HoloColour;
            }
            catch (NullReferenceException error)
            {
                CreateMsgBox("Error: Empty Holo Color.\n", error.Message);
            }

            #endregion
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

        private void GraphicsSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModManager_Click(object sender, RoutedEventArgs e)
        {
            // Create new window object
            ModManager window = new ModManager();

            // Set the owner of this new object
            window.Owner = this;

            // Display the new window
            window.ShowDialog();
        }

        private void VoipOptions_Click(object sender, RoutedEventArgs e)
        {

        }

        // Player Customization Click Events
        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            // Show save window dialog box.
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Json file (*.json)|*.json|Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    string json = JsonConvert.SerializeObject(CreatePlayer(), Formatting.Indented, settings);
                    sw.Write(json);
                }
            }
        }

        private void LoadProfile_Click(object sender, RoutedEventArgs e)
        {
            // Show load window dialog box.
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json file (*.json)|*.json|Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";

            if (openFileDialog.ShowDialog() == true)
            {
                using (StreamReader r = new StreamReader(openFileDialog.FileName))
                {
                    string json = r.ReadToEnd();
                    Player newPlayer = JsonConvert.DeserializeObject<Player>(json, settings);
                    LoadPlayerCustomizeData(newPlayer);
                }
            }
        }

        #endregion

        #region Save Data

        private Player CreatePlayer()
        {
            return new Player(NameBox.Text, 
                ClrPrimary.SelectedColor, ClrSecondary.SelectedColor,
                ClrLights.SelectedColor, ClrVisor.SelectedColor, 
                ClrHolo.SelectedColor)
            {
                CurrentWeapon = (Weapon)cbxWeapon.SelectedItem,
                Helmet = (Armor)cbxHelmet.SelectedItem,
                Shoulders = (Armor)cbxShoulders.SelectedItem,
                Chest = (Armor)cbxChest.SelectedItem,
                Wrist = (Armor)cbxWrist.SelectedItem,
                Utility = (Armor)cbxUtility.SelectedItem
            };
        }

        #endregion

        #region Dialog Events

        public void CreateMsgBox(string header, string text)
        {
            var messageWindow = new MsgBox(header, text);

            messageWindow.Show();
            messageWindow.Focus();
        }

        #endregion
    }
}
