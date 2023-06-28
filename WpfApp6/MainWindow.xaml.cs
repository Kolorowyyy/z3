using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xml.Serialization;

namespace MovieAlbumApp
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<MovieAlbum> moviesAlbums;
        private MovieAlbum selectedMovieAlbum;

        public ObservableCollection<MovieAlbum> MoviesAlbums
        {
            get { return moviesAlbums; }
            set
            {
                moviesAlbums = value;
                NotifyPropertyChanged();
            }
        }

        public MovieAlbum SelectedMovieAlbum
        {
            get { return selectedMovieAlbum; }
            set
            {
                selectedMovieAlbum = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(IsItemSelected));
            }
        }

        public bool IsItemSelected => SelectedMovieAlbum != null;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            MoviesAlbums = new ObservableCollection<MovieAlbum>();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            MovieAlbum newMovieAlbum = new MovieAlbum();
            EditWindow editWindow = new EditWindow(newMovieAlbum);
            bool? result = editWindow.ShowDialog();
            if (result == true)
            {
                MoviesAlbums.Add(newMovieAlbum);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow(SelectedMovieAlbum);
            bool? result = editWindow.ShowDialog();
            if (result == true)
            {
                NotifyPropertyChanged(nameof(MoviesAlbums));
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MoviesAlbums.Remove(SelectedMovieAlbum);
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<MovieAlbum>));
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    MoviesAlbums = (ObservableCollection<MovieAlbum>)serializer.Deserialize(reader);
                }
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<MovieAlbum>));
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    serializer.Serialize(writer, MoviesAlbums);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
