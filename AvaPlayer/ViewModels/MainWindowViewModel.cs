using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AvaPlayer.Models.Extensions;
using AvaPlayer.Models.Logic;
using AvaPlayer.Views;
using LibVLCSharp.Avalonia;
using LibVLCSharp.Shared;
using System.Configuration;

namespace AvaPlayer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IDisposable
    {
        private readonly LibVLC _libVlc = new LibVLC();
        private string _mediaFolderPath;
        public MainWindowViewModel()
        {
            MediaPlayer = new MediaPlayer(_libVlc);
        }

        public void Play()
        {
            _mediaFolderPath = ConfigurationManager.AppSettings.Get("MediaFolder");
            Playlist playlist = PlaylistCreator.CreatePlayList(_libVlc, _mediaFolderPath);
            MediaPlayer.PlayPlaylist(playlist);
        }

        public MediaPlayer MediaPlayer { get; }

        public void Dispose()
        {
            MediaPlayer?.Dispose();
            _libVlc?.Dispose();
        }
    }

}
