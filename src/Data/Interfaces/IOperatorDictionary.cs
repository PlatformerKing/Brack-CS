namespace Brack.Data.Interfaces
{
    /// <summary>
    /// Manager for Operators.
    /// </summary>
    interface IOperatorDictionary
    {
        /// <summary>
        /// All Operator names.
        /// </summary>
        string[] OpNames { get; }
        /// <summary>
        /// The amount of Operators.
        /// </summary>
        int OpCount { get; }
        /// <summary>
        /// Does an Operator exist with the given name?
        /// </summary>
        /// <param name="opName">The name of the Operator to look for.</param>
        /// <returns>If the Operator exists.</returns>
        bool HasOpName(string opName);
        /// <summary>
        /// Does an Operator exist with the given name?
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="opName">The name of the Operator to look for (nested brack operations execute).</param>
        /// <returns></returns>
        bool HasOpName(RAM r, object opName);
        /// <summary>
        /// Execute an Operator with the given name and arguments.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="opName">The name of the Operator to execute.</param>
        /// <param name="args">The arguments to pass into the Operator.</param>
        /// <returns>The resulting return of the Operator execution.</returns>
        object ExecuteOperator(RAM r, string opName, object[] args);
        /// <summary>
        /// Execute an Operator with the given name and arguments.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="opName">The name of the Operator to execute.</param>
        /// <param name="args">The arguments to pass into the Operator (nested brack operations execute).</param>
        /// <returns>The resulting return of the Operator execution.</returns>
        object ExecuteOperator(RAM r, object opName, object[] args);
    }
}
