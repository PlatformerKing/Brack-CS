using Brack.Data.Interfaces;
using Brack.Exceptions;
using System;
using System.Collections.Generic;

namespace Brack.Data.Memory
{
    /// <summary>
    /// Class for managing global memory data for a Brack application.
    /// </summary>
    public class GlobalMemory : IGlobalVariableDictionary, IScriptDictionary
    {
        #region Private Properties
        /// <summary>
        /// The Scripts of this GlobalMemory.
        /// </summary>
        private Dictionary<string, Script> _Scripts;
        /// <summary>
        /// The globalvars of this GlobalMemory.
        /// </summary>
        private Dictionary<string, object> _Globals;
        #endregion
        #region Constructors
        /// <summary>
        /// The default contsructor.
        /// </summary>
        public GlobalMemory()
        {
            ResetGlobals();
            ResetScripts();
        }
        #endregion
        #region IGlobalVariableDictionary Implementation
        /// <summary>
        /// The amount of globalvars.
        /// </summary>
        public int GlobalCount
        {
            get
            {
                return _Globals.Count;
            }
        }
        /// <summary>
        /// Does the given globalvar exist with the given name?
        /// </summary>
        /// <param name="varName">The name of the globalvar to check for.</param>
        /// <returns>If a globalvar exists with the given name.</returns>
        public bool HasGlobal(string varName)
        {
            return _Globals.ContainsKey(varName);
        }
        /// <summary>
        /// Does the given globalvar exist with the given name?
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the globalvar to check for (nested brack operations execute).</param>
        /// <returns>If a globalvar exists with the given name.</returns>
        public bool HasGlobal(RAM r, object varName)
        {
            return HasGlobal(r.GetName(varName));
        }
        /// <summary>
        /// Set the globalvar with the given name to have the given value, and declare a globalvar with the given name if none exist already.
        /// </summary>
        /// <param name="varName">The name of the globalvar to alter or declare.</param>
        /// <param name="value">The value to store in the globalvar.</param>
        public void SetGlobal(string varName, object value)
        {
            _Globals[varName] = value;
        }
        /// <summary>
        /// Set the globalvar with the given name to have the given value, and declare a globalvar with the given name if none exist already.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the globalvar to check for (nested brack operations execute).</param>
        /// <param name="value">The value to store in the globalvar.</param>
        /// <returns>If a globalvar exists with the given name.</returns>
        public void SetGlobal(RAM r, object varName, object value)
        {
            SetGlobal(r.GetName(varName), value);
        }
        /// <summary>
        /// Get the value of the globalvar with the given name.
        /// </summary>
        /// <param name="varName">The name of the globalvar.</param>
        /// <returns>The value found in the globalvar.</returns>
        public object GetGlobal(string varName)
        {
            try
            {
                return _Globals[varName];
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (KeyNotFoundException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new GlobalUndeclaredException(varName);
            }
        }
        /// <summary>
        /// Get the value of the globalvar with the given name.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the globavar to get from (nested brack operations execute).</param>
        /// <returns>The value found in the globalvar with the given name.</returns>
        public object GetGlobal(RAM r, object varName)
        {
            return GetGlobal(r.GetName(varName));
        }
        /// <summary>
        /// Delete the globalvar with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="varName">The name of the globalvar to delete.</param>
        public void DeleteGlobal(string varName)
        {
            try
            {
                _Globals.Remove(varName);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (KeyNotFoundException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new GlobalUndeclaredException(varName);
            }
        }
        /// <summary>
        /// Delete the globavar with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the globavar to delete (nested brack operations execute).</param>
        public void DeleteGlobal(RAM r, object varName)
        {
            DeleteGlobal(r.GetName(varName));
        }
        /// <summary>
        /// Instantiate a new globalvar dictionary.
        /// </summary>
        public void ResetGlobals()
        {
            _Globals = new Dictionary<string, object>();
        }
        /// <summary>
        /// Get the names of all globalvars.
        /// </summary>
        public string[] GlobalVarnames
        {
            get
            {
                List<string> ret = new List<string>();
                foreach (var g in _Globals)
                {
                    ret.Add(g.Key);
                }
                return ret.ToArray();
            }
        }
        #endregion
        #region IScriptDictionary Implementation
        /// <summary>
        /// The amount of Scripts.
        /// </summary>
        public int ScriptCount
        {
            get
            {
                return _Scripts.Count;
            }
        }
        /// <summary>
        /// Does a Script exist with the given name?
        /// </summary>
        /// <param name="scriptName">The name of the Script to check for.</param>
        /// <returns>If a Script exists with the given name.</returns>
        public bool HasScript(string scriptName)
        {
            try
            {
                return _Scripts.ContainsKey(scriptName);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (KeyNotFoundException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new ScriptUndeclaredException(scriptName);
            }
        }
        /// <summary>
        /// Does a Script exist with the given name?
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The name of the Script to check for (nested brack operations execute).</param>
        /// <returns>If a Script exists with the given name.</returns>
        public bool HasScript(RAM r, object scriptName)
        {
            return HasScript(r.GetName(scriptName));
        }
        /// <summary>
        /// Set the Script with the given name to have the given value, and declare a Script with the given name if none exist already.
        /// </summary>
        /// <param name="scriptName">The name of the Script to alter or declare.</param>
        /// <param name="script">The Script to add.</param>
        public void SetScript(string scriptName, Script script)
        {
            _Scripts[scriptName] = script;
        }
        /// <summary>
        /// Set the Script with the given name to have the given value, and declare a Script with the given name if none exist already.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The name of the Script to check for (nested brack operations execute).</param>
        /// <param name="script">The Script to add.</param>
        /// <returns>If a Script exists with the given name.</returns>
        public void SetScript(RAM r, object scriptName, Script script)
        {
            SetScript(r.GetName(scriptName), script);
        }
        /// <summary>
        /// Get the Script with the given name.
        /// </summary>
        /// <param name="scriptName">The name of the Script.</param>
        /// <returns>The Script found with the given name.</returns>
        public Script GetScript(string scriptName)
        {
            try
            {
                return _Scripts[scriptName];
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (KeyNotFoundException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new ScriptUndeclaredException(scriptName);
            }
        }
        /// <summary>
        /// Get the Script with the given name.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The name of the Script to get from (nested brack operations execute).</param>
        /// <returns>The Script found with the given name.</returns>
        public Script GetScript(RAM r, object scriptName)
        {
            return GetScript(r.GetName(scriptName));
        }
        /// <summary>
        /// Delete the Script with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="scriptName">The name of the Script to delete.</param>
        public void DeleteScript(string scriptName)
        {
            try
            {
                _Scripts.Remove(scriptName);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (KeyNotFoundException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new ArgumentException();
            }
        }
        /// <summary>
        /// Delete the Script with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The name of the Script to delete (nested brack operations execute).</param>
        public void DeleteScript(RAM r, object scriptName)
        {
            DeleteScript(r.GetName(scriptName));
        }
        /// <summary>
        /// Instantiate a new Script Dictionary.
        /// </summary>
        public void ResetScripts()
        {
            _Scripts = new Dictionary<string, Script>();
        }
        /// <summary>
        /// Get the names of all Scripts.
        /// </summary>
        public String[] ScriptNames
        {
            get
            {
                List<string> ret = new List<string>();
                foreach (var s in _Scripts)
                {
                    ret.Add(s.Key);
                }
                return ret.ToArray();
            }
        }
        /// <summary>
        /// Get the names of the arguments of the Script of the given name.
        /// </summary>
        /// <param name="scriptName">The name of the Script .</param>
        /// <returns>The argument names.</returns>
        public string[] GetScriptArguments(string scriptName)
        {
            try
            {
                return _Scripts[scriptName].GetArgNames();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (KeyNotFoundException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new ScriptUndeclaredException(scriptName);
            }
        }
        /// <summary>
        /// Get the names of the arguments of the given Script from this GlobalMemory.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The name of the Script (nested operators are executed).</param>
        /// <returns>The argument names</returns>
        public string[] GetScriptArguments(RAM r, object scriptName)
        {
            return GetScriptArguments(r.GetName(scriptName));
        }
        /// <summary>
        /// Execute a Script.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The Script name.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The resulting return.</returns>
        public object ExecuteScript(RAM r, string scriptName, object[] args)
        {
            try
            {
                return _Scripts[scriptName].Execute(r, args);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (KeyNotFoundException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new ScriptUndeclaredException(scriptName);
            }
        }
        /// <summary>
        /// Execute a Script.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The Script name.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The resulting return.</returns>
        public object ExecuteScript(RAM r, object scriptName, object[] args)
        {
            return ExecuteScript(r, r.GetName(scriptName), args);
        }
        #endregion
    }
}
