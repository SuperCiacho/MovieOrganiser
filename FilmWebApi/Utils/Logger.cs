// File created by Bartosz Nowak on 04/10/2014 14:25

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Yorgi.FilmWebApi.Utils
{
    public static class Logger
    {
        private static ASCIIEncoding encoder;
        private static readonly string logEntryPattern = "[{0}][{1}] - {2}" + Environment.NewLine;

        public static ASCIIEncoding Encoder
        {
            get { return encoder ?? (encoder = new ASCIIEncoding()); }
        }

        private static void WriteLogEntry(string log, InformationLevel informationLevel)
        {
            if (string.IsNullOrEmpty(log)) return;
            var properLog = string.Format(logEntryPattern, DateTime.Now, informationLevel.ToString(), log);
            var bytes = Encoder.GetBytes(properLog);
            logFile.WriteAsync(bytes, 0, bytes.Length);
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
            var exceptionMsg = new StringBuilder(string.Format(logEntryPattern, DateTime.Now, exception.GetType(), title));
            exceptionMsg.AppendFormat(exception.Message + Environment.NewLine);
            exceptionMsg.AppendFormat("{0}*** Stack trace: ***{0}{1}", Environment.NewLine, exception.StackTrace);

            var ie = exception.InnerException;
            while (ie != null)
            {
                exceptionMsg.AppendFormat("{0}Inner exception message:{0}", Environment.NewLine)
                    .AppendLine(exception.InnerException.Message + Environment.NewLine)
                    .AppendFormat("{0}*** Inner exception stack trace: ***{0}{1}", Environment.NewLine, exception.InnerException.StackTrace);
                ie = ie.InnerException;
            }
            exceptionMsg.AppendLine("*** END OF EXCEPTION ***");
            WriteLogEntry(exceptionMsg.ToString(), InformationLevel.Error);
#endif
        }

        public static void Debug(string log)
        {
#if DEBUG
            if (Debugger.IsAttached) WriteLogEntry(log, InformationLevel.Debug);
#endif
        }

        #region Nested type: InformationLevel

        private enum InformationLevel
        {
            Verbose,
            Information,
            Warning,
            Error,
            Debug
        }

        #endregion

        private static readonly string logFileName = Path.Combine(Environment.CurrentDirectory, "logs.txt");
        private static readonly FileStream logFile = File.OpenWrite(logFileName);
    }
}