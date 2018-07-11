using log4net;
using System;
using System.Security.Principal;

namespace PCR.Users.API.Helpers
{
    public class Log4NetLogger<T> : ILogger<T>, IDisposable where T : class
    {
        private readonly ILog _logger;
        private bool disposed;

        public Log4NetLogger()
        {
            var windowsIdentity = WindowsIdentity.GetCurrent();
            if (windowsIdentity != null && windowsIdentity.Name != null)
            {
                GlobalContext.Properties["USERNAME"] = windowsIdentity.Name.Split('\\')[1];

            }
            _logger = LogManager.GetLogger(typeof(T));
        }

        public void Debug(object message)
        {
            _logger.Debug(message);
        }

        public void Error(object message)
        {
            _logger.Error(message);
        }

        public void Error(Exception exception)
        {
            _logger.Error(exception.GetBaseException().Message, exception);
        }

        public void Error(string customMessage, Exception exception)
        {
            _logger.Error(customMessage, exception);
        }

        public void Error(string format, Exception exception, params object[] args)
        {

            _logger.Error(string.Format(format, args), exception);
        }

        public void Warn(Exception exception)
        {
            _logger.Warn(exception.GetBaseException().Message, exception);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(object message)
        {
            _logger.Warn(message);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.Dispose();
                }
                disposed = true;
            }
        }

    }
}
