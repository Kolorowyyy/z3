using System.Windows;

namespace MovieAlbumApp
{
    public partial class EditWindow : Window
    {
        public MovieAlbum MovieAlbum { get; private set; }

        public EditWindow(MovieAlbum movieAlbum)
        {
            InitializeComponent();
            MovieAlbum = movieAlbum;
            DataContext = MovieAlbum;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
