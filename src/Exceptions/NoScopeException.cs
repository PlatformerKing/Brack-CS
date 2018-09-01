using System;

namespace Brack.Exceptions
{
    /// <summary>
    /// Exception for the attempted use of undelcared Scopes.
    /// </summary>
    public class NoScopeException : Exception
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public NoScopeException() : base("Failed to find a scope in the given local memory. You must add a scope with AddScope() before the local memory can be used to store local information.") { }
    }
}
