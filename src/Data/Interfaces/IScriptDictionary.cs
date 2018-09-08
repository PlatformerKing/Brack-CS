using Brack.Data.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brack.Data.Interfaces
{
    /// <summary>
    /// Manager for Scripts.
    /// </summary>
    interface IScriptDictionary
    {
        /// <summary>
        /// The amount of Scripts.
        /// </summary>
        int ScriptCount { get; }
        /// <summary>
        /// Does a Script exist with the given name?
        /// </summary>
        /// <param name="scriptName">The name of the Script to check for.</param>
        /// <returns>If a Script exists with the given name.</returns>
        bool HasScript(string scriptName);
        /// <summary>
        /// Does a Script exist with the given name?
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The name of the Script to check for (nested brack operations execute).</param>
        /// <returns>If a Script exists with the given name.</returns>
        bool HasScript(RAM r, object scriptName);
        /// <summary>
        /// Set the Script with the given name to have the given value, and declare a Script with the given name if none exist already.
        /// </summary>
        /// <param name="scriptName">The name of the Script to alter or declare.</param>
        /// <param name="script">The Script to add.</param>
        void SetScript(string scriptName, Script script);
        /// <summary>
        /// Set the Script with the given name to have the given value, and declare a Script with the given name if none exist already.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The name of the Script to check for (nested brack operations execute).</param>
        /// <param name="script">The Script to add.</param>
        /// <returns>If a Script exists with the given name.</returns>
        void SetScript(RAM r, object scriptName, Script script);
        /// <summary>
        /// Get the Script with the given name.
        /// </summary>
        /// <param name="scriptName">The name of the Script.</param>
        /// <returns>The Script found with the given name.</returns>
        Script GetScript(string scriptName);
        /// <summary>
        /// Get the Script with the given name.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The name of the Script to get from (nested brack operations execute).</param>
        /// <returns>The Script found with the given name.</returns>
        Script GetScript(RAM r, object scriptName);
        /// <summary>
        /// Delete the Script with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="scriptName">The name of the Script to delete.</param>
        void DeleteScript(string scriptName);
        /// <summary>
        /// Delete the Script with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The name of the Script to delete (nested brack operations execute).</param>
        void DeleteScript(RAM r, object scriptName);
        /// <summary>
        /// Instantiate a new Script Dictionary.
        /// </summary>
        void ResetScripts();
        /// <summary>
        /// Get the names of all Scripts.
        /// </summary>
        string[] ScriptNames { get; }
        /// <summary>
        /// Get the names of the arguments of the Script of the given name.
        /// </summary>
        /// <param name="scriptName">The name of the Script.</param>
        /// <returns>The argument names.</returns>
        string[] GetScriptArguments(string scriptName);
        /// <summary>
        /// Execute a Script.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The Script name.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The resulting return.</returns>
        object ExecuteScript(RAM r, string scriptName, object[] args);
        /// <summary>
        /// Execute a Script.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The name of the Script  (nested brack operations execute).</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The argument names.</returns>
        object ExecuteScript(RAM r, object scriptName, object[] args);
    }
}
