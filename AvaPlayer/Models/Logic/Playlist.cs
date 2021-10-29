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
        private List<Media> _mediaList;
        private Random _random;
        private int _elementIndex = 1;

        public Playlist()
        {
            _mediaList = new List<Media>();
            _random = new Random();
        }

        /// <summary>
        /// Добавляет медиа в плейлист
        /// </summary>
        /// <param name="media"></param>
        public void AddMedia(Media media)
        {
            _mediaList.Add(media);
            _mediaList.Mix();
        }

        /// <summary>
        /// Выдаёт следующее медиа из списка
        /// </summary>
        /// <returns>
        /// Media
        /// </returns>
        public Media GetMedia()
        {
            _elementIndex++;
            if (_elementIndex >= _mediaList.Count || _elementIndex == 0 || _elementIndex > _mediaList.Count - 1)
                _elementIndex = 1;
            try
            {
                return _mediaList[_elementIndex - 1];
            }
            catch(ArgumentOutOfRangeException ex)
            {
                _elementIndex++;
                return _mediaList.First();
            }
        }
    }
}
