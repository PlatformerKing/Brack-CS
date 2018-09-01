using System;

namespace Brack.Exceptions
{
    /// <summary>
    /// Exception for the attempted use of an undelcared BrackOperator.
    /// </summary>
    public class OperatorException : Exception
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="opName">The name of the BrackOperator that is undeclared.</param>
        public OperatorException(string opName) : base("Operator not found or two operators created with same name: " + opName + ".") {}
    }
}
