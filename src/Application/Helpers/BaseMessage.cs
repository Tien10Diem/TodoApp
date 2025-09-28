using System;

namespace Application.Helpers
{
    public class BaseException : Exception
    {
        public BaseException(string message) : base(message) { } // khong hoat dong
    }
}