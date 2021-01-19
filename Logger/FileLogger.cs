using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Logger
{
    class FileLogger : BaseLogger
    {
        private String? filePath;

        public FileLogger(String newFilePath)
        {
            if (newFilePath is null) throw new ArgumentNullException(nameof(FileLogger));

            filePath = newFilePath;
        }

        public override void Log(LogLevel logLevel, string message)
        {
            //Check date X
            //Check if on new line X
            File.AppendAllText(filePath,$"{DateTime.Now} {ClassName} {logLevel}: {message}");
            File.AppendAllText(filePath, Environment.NewLine);
        }
    }
}
