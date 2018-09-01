using System;

namespace Brack.Exceptions
{
    /// <summary>
    /// Exception for the attempted use of undelcared Scripts.
    /// </summary>
    public class ScriptUndeclaredException : Exception
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="scriptName">The name of the Script that is undeclared.</param>
        public ScriptUndeclaredException(string scriptName) : base("Failed to find script with given scriptname: " + scriptName + ". You must declare a script before you can access it.") {}
    }
}
