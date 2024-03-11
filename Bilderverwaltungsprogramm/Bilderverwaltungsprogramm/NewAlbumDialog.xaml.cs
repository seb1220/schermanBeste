using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;

namespace Bilderverwaltungsprogramm
{
    /// <summary>
    /// Interaktionslogik für NewAlbumDialog.xaml
    /// </summary>
    public partial class NewAlbumDialog : Window
    {
        public string AlbumName { get; set; } = "";

        private List<string> albums;

        public NewAlbumDialog(List<string> albums)
        {
            InitializeComponent();

            this.albums = albums;

            this.DataContext = this;
        }

        private void okButton_Click(object sender, RoutedEventArgs e) =>
            DialogResult = true;

        private void cancelButton_Click(object sender, RoutedEventArgs e) =>
            DialogResult = false;

        private void AlbumName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (AlbumName == null)
            {
                okButton.IsEnabled = false;
                return;
            }

            if (albums.Contains(AlbumName))
            {
                var animation = new DoubleAnimation(5, 20, new Duration(TimeSpan.FromSeconds(0.5)))
                {
                    AutoReverse = true,
                    RepeatBehavior = new RepeatBehavior(3)
                };

                AlbumNameTextBox.BeginAnimation(WidthProperty, animation);

                okButton.IsEnabled = false;
                return;
            }

            okButton.IsEnabled = true;
        }
    }
}
