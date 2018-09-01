using System;

namespace Brack.Exceptions
{
    /// <summary>
    /// Exception for the attempted use of undelcared globalvars.
    /// </summary>
    public class GlobalUndeclaredException : Exception
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="varName">The name of the globalvar that is undeclared.</param>
        public GlobalUndeclaredException(string varName) : base("Failed to find global with given varname: " + varName + ". You must declare a global before you can access it.") {}
    }
}
