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
                Task.Run(() =>
                {
                    mediaPlayer.Play(playlist.GetMedia());
                });
            };
            mediaPlayer.Play(playlist.GetMedia());
        }
    }
}
