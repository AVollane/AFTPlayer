using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaPlayer.Models.Logic
{
    public class Playlist
    {
        public delegate void PlaylistHandler(object? sender, EventArgs e);
        public event PlaylistHandler EndReached;

        private Queue<Media> _mediaQueue;
        public Playlist()
        {
            _mediaQueue = new Queue<Media>();
        }
        public void AddMedia(Media media) => _mediaQueue.Enqueue(media);
        public bool IsEmpty
        {
            get
            {
                if (_mediaQueue.Count == 0)
                {
                    EndReached.Invoke(this, new EventArgs());
                    return true;
                }
                return false;
            }
        }
        public Media GetMedia()
        {
            return _mediaQueue.Dequeue();
        }
    }
}
