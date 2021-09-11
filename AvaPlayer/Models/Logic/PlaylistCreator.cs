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
                    playlist.AddMedia(media);
                }
            }
            return playlist;
        }
    }
}
