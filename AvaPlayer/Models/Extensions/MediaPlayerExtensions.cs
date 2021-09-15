using AvaPlayer.Models.Logic;
using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AvaPlayer.Models.Extensions
{
    public static class MediaPlayerExtensions
    {
        /// <summary>
        /// Plays playlist in MediaPlayer control
        /// </summary>
        /// <param name="mediaPlayer">Playable media player</param>
        /// <param name="playlist">Playlist created from folder</param>
        public static void PlayPlaylist(this MediaPlayer mediaPlayer, Playlist playlist)
        {
            mediaPlayer.EndReached += (object? sender, EventArgs e) =>
            {
                Task.Run(() =>
                {
                    if (!playlist.IsEmpty)
                    {
                        mediaPlayer.Play(playlist.GetMedia());
                    }
                });
            };
            mediaPlayer.Play(playlist.GetMedia());
        }
    }
}
