using System;

namespace PCR.Users.API.Helpers
{
    public interface ILogger<T> where T : class
    {

        void Debug(object message);

        void Error(object message);

        void Error(Exception exception);

        void Error(string customMessage, Exception exception);

        void Error(string format, Exception exception, params object[] args);

        void Warn(Exception exception);

        void Warn(object message);

        void Fatal(string message);

        void Info(string message);

    }
}
