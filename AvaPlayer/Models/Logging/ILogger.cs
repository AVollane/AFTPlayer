using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTPlayer.Models.Logging
{
    public interface ILogger
    {
        public void Log(string message);
        public StreamWriter LogStream { get; set; }
    }
}
