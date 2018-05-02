using System;
using System.Windows;
using VideoGameLauncher.View;

namespace VideoGameLauncher
{
    public partial class ModBox
    {
        #region Properties

        private ModManager owner;

        #endregion

        #region Initialize

        public ModBox()
        {
            InitializeComponent();
        }

        private void ModBox_Loaded(object sender, RoutedEventArgs e)
        {
            owner = Owner as ModManager;
        }

        #endregion

        #region Click Events

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {            
                var newMod = new
                {
                    Name = NameBox.Text,
                    Author = AuthorBox.Text,
                    Version = double.Parse(VersionBox.Text),
                    Description = DescriptionBox.Text,
                    Warnings = WarningBox.Text,
                    Location = LocationBox.Text
                };

                // Add to Applied Mods list
                owner.AppliedMods.Add(newMod);

                // Update Footer Count
                owner.lblMyModsCount.Content = owner.AppliedMods.Count;

                Close();
            }
            catch (NullReferenceException emptyException)
            {
                MainWindow.CreateMsgBox(emptyException.Message, "Error: Corrupt mod list, please restart the application.\n");
            }
            catch (FormatException formatException)
            {
                MainWindow.CreateMsgBox(formatException.Message, "Error: Please enter a number for Versions field (one decimal place).\n");
            }
            catch (Exception exception)
            {
                MainWindow.CreateMsgBox("Error: Please check your input fields.\n", exception.Message);
            }
        }

        #endregion
    }
}