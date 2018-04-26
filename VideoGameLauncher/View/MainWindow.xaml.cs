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
            settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            LoadPlayerCustomizationData();
        }

        private void LoadPlayerCustomizationData()
        {

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

        private void PlayerCustomize_Click(object sender, RoutedEventArgs e)
        {
            FlyoutHandler(FlyoutPlayerCustomize);
        }

        private void GitHub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/s00172994/VideoGameLauncher");
        }

        #endregion
    }
}
