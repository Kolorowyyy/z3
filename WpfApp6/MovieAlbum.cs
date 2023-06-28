using System;

namespace MovieAlbumApp
{
    public class MovieAlbum
    {
        public string Tytuł { get; set; }
        public string Reżyser { get; set; }
        public string Wydawca { get; set; }
        public string Nośnik { get; set; }
        public TimeSpan Długość { get; set; }
        public DateTime DataWydania { get; set; }
    }
}
