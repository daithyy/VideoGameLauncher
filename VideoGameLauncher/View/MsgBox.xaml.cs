using System.Windows;

namespace VideoGameLauncher
{
    public partial class MsgBox
    {
        public MsgBox(string header, string text)
        {
            InitializeComponent();
            Msg.Text = text;
            Header.Text = header;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}