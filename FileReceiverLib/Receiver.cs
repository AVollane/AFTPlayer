using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTClient
{
    public class Receiver
    {
        private SftpClient _client;
        private string _host;
        private string _username;
        private string _password;

        public Receiver(string host, string username, string password)
        {
            _host = host;
            _username = username;
            _password = password;
            _client = new SftpClient(_host, _username, _password);
        }

        public bool IsConnected
        {
            get
            {
                return _client.IsConnected;
            }
        }

        /// <summary>
        /// Подключается к удаленному SFTP-серверу
        /// </summary>
        public void Connect() => _client.Connect();

        /// <summary>
        /// Отсоединяемся от удаленного SFTP-сервера
        /// </summary>
        public void Disconnect() => _client.Disconnect();

        /// <summary>
        /// Скачивает все файлы из удаленной директории в локальную директорию
        /// </summary>
        /// <param name="remoteDirectory">Удаленная директория</param>
        /// <param name="localDirectory">Локальная директория</param>
        public void DownloadFiles(string remoteDirectory, string localDirectory)
        {
            if (String.IsNullOrEmpty(remoteDirectory) && String.IsNullOrEmpty(localDirectory))
                return;

            if (localDirectory.EndsWith('/'))
                localDirectory += @"/";
            if (remoteDirectory.EndsWith('/'))
                remoteDirectory += @"/";

            // Файлы, которые начинаются с точки, являются системными. Мы не должны их скачивать,
            // по этому фильтруем названия
            IEnumerable<SftpFile> files = _client.ListDirectory(remoteDirectory).Where(x => !x.Name.StartsWith('.'));

            foreach(SftpFile file in files)
            {
                try
                {
                    using (FileStream fileStream = new FileStream($"{localDirectory}/{file.Name}", FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        _client.DownloadFile($"{remoteDirectory}/{file.Name}", fileStream);
                    }
                }
                catch(Exception ex)
                {
                    continue;
                }
            }
        }
    }
}
