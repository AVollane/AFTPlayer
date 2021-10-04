using AFTPlayer.Models.Extensions;
using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaPlayer.Models.Logic
{
    public static class PlaylistCreator
    {
        public static Playlist CreatePlayList(LibVLC libVlc, string path)
        {
            string[] files = Directory.GetFiles(path);
            List<FileInfo> filesInfo = new List<FileInfo>();

            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fi = new FileInfo(files[i]);
                filesInfo.Add(fi);
            }
            Playlist playlist = new Playlist();
            List<Media> medias = new List<Media>();
            foreach(FileInfo fi in filesInfo)
            {
                int times;
                bool isDone = Int32.TryParse(fi.Name.Split('.')[0].Split('_')[1], out times);
                if (!isDone)
                    continue;
                // Добавить медиа в плейлист необходимое количество раз
                for(int j = 0; j < times; j++)
                {
                    Media media = new Media(libVlc, new Uri(fi.FullName));
                    medias.Add(media);
                }
            }
            medias.Mix<Media>();
            foreach(Media med in medias)
            {
                playlist.AddMedia(med);
            }
            return playlist;
        }

        // Создаётся плейлист с видеороликами в случайном порядке
        public static Playlist CreateMixedPlaylist(LibVLC libVlc, string path)
        {
            string[] files = Directory.GetFiles(path);
            FileInfo[] fileInfos = new FileInfo[files.Length];

            for (int i = 0; i < files.Length; i++)
                fileInfos[i] = new FileInfo(files[i]);

            Dictionary<string, int> namesRepetitions = new Dictionary<string, int>(); // пары: имя файла - сколько раз
                                                                                      // будет повторяться
            int repetitionsCount = 0; // общее количество повторений
            foreach(FileInfo fi in fileInfos)
            {
                try
                {
                    string fileName = fi.Name.Split('_')[0];
                    int repetitions = Int32.Parse(fi.Name.Split('_')[1]);
                    namesRepetitions.Add(fileName, repetitions);
                    repetitionsCount += repetitions;
                }
                catch (FormatException)
                {
                    continue;
                }
            }



            return new Playlist();
        }
    }
}
