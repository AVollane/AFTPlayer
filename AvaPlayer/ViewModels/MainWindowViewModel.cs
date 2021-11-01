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
using AFTPlayer.Models.Logging;

namespace AvaPlayer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IDisposable
    {
        private readonly LibVLC _libVlc = new LibVLC();
        private string? _mediaFolderPath; // ���� � ����� � �������������
        private string? _introFilePath; // ���� � ����� � �����
        public MainWindowViewModel()
        {
            MediaPlayer = new MediaPlayer(_libVlc);
            _introFilePath = ConfigurationManager.AppSettings.Get("IntroPath");
            _mediaFolderPath = ConfigurationManager.AppSettings.Get("MediaFolder");
        }

        /// <summary>
        /// �������� ����������� �����
        /// </summary>
        public void Play()
        {
            Media media = new Media(_libVlc, _introFilePath);
            MediaPlayer.Play(media);
            Playlist playlist = PlaylistCreator.CreatePlayList(_libVlc, _mediaFolderPath);
            MediaPlayer.PlayPlaylist(playlist);
        }

        /// <summary>
        /// �������� ����������
        /// </summary>
        public MediaPlayer MediaPlayer { get; }

        /// <summary>
        /// ����������� ������� ������ �������������
        /// </summary>
        public void Dispose()
        {
            MediaPlayer?.Dispose();
            _libVlc?.Dispose();
        }
    }

}
