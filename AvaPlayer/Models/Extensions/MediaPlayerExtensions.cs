using AFTPlayer.Models.Logging;
using AvaPlayer.Models.Logic;
using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AvaPlayer.Models.Extensions
{
    public static class MediaPlayerExtensions
    {
        public static void PlayPlaylist(this MediaPlayer mediaPlayer, Playlist playlist)
        {
            mediaPlayer.EndReached += (object? sender, EventArgs e) =>
            {
                // Запускаем задачу в отдельном потоке, только так мы имеем возможность
                // обработать событие достижения конца видеоролика
                Task.Run(() =>
                {
                    Media media = playlist.GetMedia();
                    mediaPlayer.Play(media);
                });
            };
            mediaPlayer.Play(playlist.GetMedia());
        }
    }
}
