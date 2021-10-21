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
        private string? _mediaFolderPath; // Путь к папке с видеороликами
        private string? _introFilePath; // Путь к папке с интро
        public MainWindowViewModel()
        {
            MediaPlayer = new MediaPlayer(_libVlc);
            _introFilePath = ConfigurationManager.AppSettings.Get("IntroPath");
            _mediaFolderPath = ConfigurationManager.AppSettings.Get("MediaFolder");
        }

        /// <summary>
        /// Начинает проигрывать видео
        /// </summary>
        public void Play()
        {
            Media media = new Media(_libVlc, _introFilePath);
            MediaPlayer.Play(media);
            Playlist playlist = PlaylistCreator.CreatePlayList(_libVlc, _mediaFolderPath);
            MediaPlayer.EndReached += (object? sender, EventArgs e) =>
            {
                Task.Run(() =>
                {
                    MediaPlayer.PlayPlaylist(playlist);
                });
            };
        }

        /// <summary>
        /// Основной медиаплеер
        /// </summary>
        public MediaPlayer MediaPlayer { get; }

        /// <summary>
        /// Освобождает ресурсы модели представления
        /// </summary>
        public void Dispose()
        {
            MediaPlayer?.Dispose();
            _libVlc?.Dispose();
        }
    }

}
