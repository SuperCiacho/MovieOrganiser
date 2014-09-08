using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieOrganiser.Utils
{
    public static class Logger
    {
        private enum InformationLevel
        {
            Verbose,
            Information,
            Warning,
            Error,
            Debug
        }

        private static ASCIIEncoding _encoder;
        private static readonly string LogEntryPattern = "[{0}][{1}] - {2}" + Environment.NewLine;
        private static readonly string LogFileName = Path.Combine(Environment.CurrentDirectory, "logs.txt");
        private static readonly FileStream LogFile = File.OpenWrite(LogFileName);

        public static ASCIIEncoding Encoder
        {
            get { return _encoder ?? (_encoder = new ASCIIEncoding()); }
        }

        private static void WriteLogEntry(string log, InformationLevel informationLevel)
        {
            if (string.IsNullOrEmpty(log)) return;
            var properLog = string.Format(LogEntryPattern, DateTime.Now, informationLevel.ToString(), log);
            var bytes = Encoder.GetBytes(properLog);
            LogFile.WriteAsync(bytes, 0, bytes.Length);
        }

        public static void Error(string log)
        {
            WriteLogEntry(log, InformationLevel.Error);
        }

        public static void Information(string log)
        {
            WriteLogEntry(log, InformationLevel.Information);
        }

        public static void Verbose(string log)
        {
            WriteLogEntry(log, InformationLevel.Verbose);
        }

        public static void Warning(string log)
        {
            WriteLogEntry(log, InformationLevel.Warning);
        }

        public static void Exception<T>(T exception, string title = "") where T : Exception
        {
            #if DEBUG
            var newLine = Environment.NewLine;
            var exceptionMsg = new StringBuilder(string.Format(LogEntryPattern, DateTime.Now, exception.GetType(), title));
            exceptionMsg.AppendFormat( exception.Message + newLine);
            exceptionMsg.AppendFormat("{0}*** Stack trace: ***{0}{1}", newLine, exception.StackTrace);

            if (exception.InnerException != null)
            {
                exceptionMsg.AppendFormat("{0}Inner exception message:{0}", newLine)
                    .AppendLine(exception.InnerException.Message + newLine)
                    .AppendFormat("{0}*** Inner exception stack trace: ***{0}{1}", newLine, exception.InnerException.StackTrace);
            }


            WriteLogEntry(exceptionMsg.ToString(), InformationLevel.Error);
            #endif
        }

        public static void Debug(string log)
        {
            #if DEBUG
            if (System.Diagnostics.Debugger.IsAttached) WriteLogEntry(log, InformationLevel.Debug);
            #endif
        }
    }
}
