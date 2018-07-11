using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCR.Users.Services.Helpers
{
    public class Logger
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly ILog _Logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logName"></param>
        /// <returns></returns>
        private static ILog GetLogger(string logName)
        {
            ILog log = LogManager.GetLogger(logName);
            return log;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        static Logger()
        {
            //logger names are mentioned in <log4net> section of config file
            _Logger = GetLogger("MyApplicationDebugLog");
        }

        /// <summary>
        /// This method will write log in Log_USERNAME_date{yyyyMMdd}.log file
        /// </summary>
        /// <param name="message"></param>
        public static void WriteTrace(string message)
        {
            _Logger.DebugFormat(message);
        }

        /// <summary>
        /// Writes exception
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public static void WriteException(string source, Exception ex)
        {
            string text = source + "-->" + ex.Message;

            source = text + "\r\n" + ex.StackTrace;

            if (ex.InnerException != null)
            {
                source = source + "\r\n" + ex.InnerException.Message;
                if (ex.InnerException.InnerException != null)
                    source = source + "\r\n" + ex.InnerException.InnerException.Message;
                source = source + "\r\n" + ex.InnerException.StackTrace;
            }

            _Logger.DebugFormat(source);
        }
    }

}