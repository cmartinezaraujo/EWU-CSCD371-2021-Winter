namespace Logger
{
    public class LogFactory
    {
        private string? fileLoggerPath;

        public BaseLogger? CreateLogger(string className)
        {

            if(!(fileLoggerPath is null))
            {
                return new FileLogger(fileLoggerPath) { ClassName = className };
            }

            return null;
        }

        public void ConfigureFileLogger(string filePath)
        {
            fileLoggerPath = filePath;
        }
    }
}
