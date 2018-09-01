using System;

namespace Brack.Exceptions
{
    /// <summary>
    /// Exception for the attempted use of undelcared LocalMemories.
    /// </summary>
    public class NoLocalMemoryException : Exception
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public NoLocalMemoryException() : base("Failed to find a local memory in the RAM. You must add a LocalMemory when it is needed with r.AddLocal().") {}
    }
}
