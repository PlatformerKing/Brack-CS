using System;

namespace Brack.Exceptions
{
    /// <summary>
    /// Exception for the attempted use of undelcared localvars.
    /// </summary>
    public class LocalUndeclaredException : Exception
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="varName">The name of the localvar that is undeclared.</param>
        public LocalUndeclaredException(string varName) : base("Failed to find local with given varname: " + varName + ". You must declare a local before you can access it.") {}
    }
}
