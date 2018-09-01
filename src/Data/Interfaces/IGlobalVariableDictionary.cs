namespace Brack.Data.Interfaces
{
    /// <summary>
    /// Manager for globalvars.
    /// </summary>
    public interface IGlobalVariableDictionary
    {
        /// <summary>
        /// The amount of globalvars.
        /// </summary>
        int GlobalCount { get; }
        /// <summary>
        /// Does a globalvar exist with the given name?
        /// </summary>
        /// <param name="varName">The name of the globalvar to check for.</param>
        /// <returns>If a globalvar exists with the given name.</returns>
        bool HasGlobal(string varName);
        /// <summary>
        /// Does a globalvar exist with the given name?
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the globalvar to check for (nested brack operations execute).</param>
        /// <returns>If a globalvar exists with the given name.</returns>
        bool HasGlobal(RAM r, object varName);
        /// <summary>
        /// Set the globalvar with the given name to have the given value, and declare a globalvar with the given name if none exist already.
        /// </summary>
        /// <param name="varName">The name of the globalvar to alter or declare.</param>
        /// <param name="value">The value to store in the globalvar.</param>
        void SetGlobal(string varName, object value);
        /// <summary>
        /// Set the globalvar with the given name to have the given value, and declare a globalvar with the given name if none exist already.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the globalvar to check for (nested brack operations execute).</param>
        /// <param name="value">The value to store in the globalvar.</param>
        /// <returns>If a globalvar exists with the given name.</returns>
        void SetGlobal(RAM r, object varName, object value);
        /// <summary>
        /// Get the value of the globalvar with the given name.
        /// </summary>
        /// <param name="varName">The name of the globalvar.</param>
        /// <returns>The value found in the globalvar.</returns>
        object GetGlobal(string varName);
        /// <summary>
        /// Get the value of the globalvar with the given name.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the globavar to get from (nested brack operations execute).</param>
        /// <returns>The value found in the globalvar with the given name.</returns>
        object GetGlobal(RAM r, object varName);
        /// <summary>
        /// Delete the globalvar with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="varName">The name of the globalvar to delete.</param>
        void DeleteGlobal(string varName);
        /// <summary>
        /// Delete the globavar with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the globavar to delete (nested brack operations execute).</param>
        void DeleteGlobal(RAM r, object varName);
        /// <summary>
        /// Instantiate a new globalvar Dictionary.
        /// </summary>
        void ResetGlobals();
        /// <summary>
        /// Get the names of all globalvars.
        /// </summary>
        string[] GlobalVarnames { get; }
    }
}
