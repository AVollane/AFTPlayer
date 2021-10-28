using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTPlayer.Models.Logging
{
    public class Logger : ILogger, IDisposable
    {
        private string _logFilesPath = ConfigurationManager.AppSettings.Get("LogFilesPath");
        /// <summary>
        /// Стандартный поток логирования
        /// </summary>
        public StreamWriter LogStream { get; set; }

        public Logger()
        {
            //string logFilesPath = ConfigurationManager.AppSettings.Get("LogFilesPath");
            //if(logFilesPath != null && !String.IsNullOrEmpty(logFilesPath) 
            //    && !String.IsNullOrWhiteSpace(logFilesPath) 
            //    && Directory.Exists(logFilesPath))
            //{
            //    if (!logFilesPath.EndsWith('\\')) // если путь к файлам не заканчивается на /, добавляем
            //        logFilesPath += "\\";
            //    string logFile = logFilesPath + $"{DateTime.Today.ToString("d")}Log.txt"; // формируем путь с будущим названием файла

            //    LogStream = new StreamWriter(new FileStream(logFile, FileMode.OpenOrCreate, FileAccess.ReadWrite));
            //}
            if (!_logFilesPath.EndsWith('\\'))
                _logFilesPath += "\\";
        }

        /// <summary>
        /// Логирует сообщение в файл
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            OpenStream();
            if (LogStream != null)
            {
                LogStream.WriteLine($"[{DateTime.Now}]: {message}");
                LogStream.Close();
            }
        }

        private void OpenStream()
        {
            if(_logFilesPath != null
                && !String.IsNullOrEmpty(_logFilesPath) 
                && !String.IsNullOrWhiteSpace(_logFilesPath)
                && Directory.Exists(_logFilesPath)) 
            {
                string logFile = _logFilesPath + $"{DateTime.Today.ToString("d")}Log.txt";
                LogStream = new StreamWriter(new FileStream(logFile, FileMode.OpenOrCreate, FileAccess.Write));
            }
        }

        /// <summary>
        /// Освобождает ресурсы логера
        /// </summary>
        public void Dispose() => LogStream.Dispose();

        /// <summary>
        /// Возвращает абстракцию, связанную с объектом Logger
        /// </summary>
        /// <returns></returns>
        public static ILogger GetLogger() => new Logger();
    }
}
