namespace Brack.Data.Interfaces
{
    /// <summary>
    /// A Manager for localvars.
    /// </summary>
    interface ILocalVariableDictionary
    {
        /// <summary>
        /// The amount of localvars.
        /// </summary>
        int LocalCount { get; }
        /// <summary>
        /// Does a localvars exist with the given name?
        /// </summary>
        /// <param name="varName">The name of the localvars to check for.</param>
        /// <returns>If a localvars exists with the given name.</returns>
        bool HasLocal(string varName);
        /// <summary>
        /// Does a localvar exist with the given name?
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the localvars to check for (nested brack operations execute).</param>
        /// <returns>If a localvars exists with the given name.</returns>
        bool HasLocal(RAM r, object varName);
        /// <summary>
        /// Set the localvar with the given name to have the given value, and declare a localvar with the given name if none exist already.
        /// </summary>
        /// <param name="varName">The name of the localvar to alter or declare.</param>
        /// <param name="value">The value to store in the localvar.</param>
        void SetLocal(string varName, object value);
        /// <summary>
        /// Set the localvar with the given name to have the given value, and declare a localvar with the given name if none exist already.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the localvar to check for (nested brack operations execute).</param>
        /// <param name="value">The value to store in the localvars.</param>
        /// <returns>If a localvar exists with the given name.</returns>
        void SetLocal(RAM r, object varName, object value);
        /// <summary>
        /// Get the value of the localvar with the given name.
        /// </summary>
        /// <param name="varName">The name of the localvar.</param>
        /// <returns>The value found in the localvar.</returns>
        object GetLocal(string varName);
        /// <summary>
        /// Get the value of the localvar with the given name.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the localvar to get from (nested brack operations execute).</param>
        /// <returns>The value found in the localvar with the given name.</returns>
        object GetLocal(RAM r, object varName);
        /// <summary>
        /// Delete the localvar with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="varName">The name of the localvar to delete.</param>
        void DeleteLocal(string varName);
        /// <summary>
        /// Delete the localvar with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the localvar to delete (nested brack operations execute).</param>
        void DeleteLocal(RAM r, object varName);
        /// <summary>
        /// Instantiate a new localvar Dictionary.
        /// </summary>
        void ResetLocals();
        /// <summary>
        /// Get the names of all localvar.
        /// </summary>
        string[] LocalVarnames { get; }
    }
}
