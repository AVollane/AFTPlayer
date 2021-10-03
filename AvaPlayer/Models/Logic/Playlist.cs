using AFTPlayer.Models.Extensions;
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
        private Queue<Media> _mediaQueue;
        private List<Media> _mediaList;
        private Random _random;

        private int _elementIndex = 1;
        public Playlist()
        {
            _mediaQueue = new Queue<Media>();
            _mediaList = new List<Media>();
            _random = new Random();
        }
        public void AddMedia(Media media)
        {
            _mediaQueue.Enqueue(media);
            _mediaList.Add(media);
            _mediaList.Mix();
        }
        public bool IsEmpty
        {
            get
            {
                if (_mediaQueue.Count == 0)
                    return true;
                return false;
            }
        }
        public Media GetMedia()
        {
            _elementIndex++;
            if (_elementIndex >= _mediaList.Count)
                _elementIndex = 1;
            return _mediaList[_elementIndex - 1];
        }
    }
}
