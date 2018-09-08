using Brack.Data.Interfaces;
using Brack.Exceptions;
using System;
using System.Collections.Generic;

namespace Brack.Data.Memory
{
    /// <summary>
    /// A single Scope of LocalMemory.
    /// </summary>
    public class Scope : ILocalVariableDictionary
    {
        #region Private Properties
        private Dictionary<string, object> _Locals;
        #endregion
        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Scope()
        {
            ResetLocals();
        }
       
        #endregion
        #region ILocalVariableDictionary Implementation
        /// <summary>
        /// The amount of localvars.
        /// </summary>
        public int LocalCount
        {
            get
            {
                return _Locals.Count;
            }
        }
        /// <summary>
        /// Does a localvars exist with the given name?
        /// </summary>
        /// <param name="varName">The name of the localvars to check for.</param>
        /// <returns>If a localvars exists with the given name.</returns>
        public bool HasLocal(string varName)
        {
            return _Locals.ContainsKey(varName);
        }
        /// <summary>
        /// Does a localvar exist with the given name?
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the localvars to check for (nested brack operations execute).</param>
        /// <returns>If a localvars exists with the given name.</returns>
        public bool HasLocal(RAM r, object varName)
        {
            return HasLocal(r.GetValue(varName).ToString());
        }
        /// <summary>
        /// Set the localvar with the given name to have the given value, and declare a localvar with the given name if none exist already.
        /// </summary>
        /// <param name="varName">The name of the localvar to alter or declare.</param>
        /// <param name="value">The value to store in the localvar.</param>
        public void SetLocal(string varName, object value)
        {
            _Locals[varName] = value;
        }
        /// <summary>
        /// Set the localvar with the given name to have the given value, and declare a localvar with the given name if none exist already.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the localvar to check for (nested brack operations execute).</param>
        /// <param name="value">The value to store in the localvars.</param>
        /// <returns>If a localvar exists with the given name.</returns>
        public void SetLocal(RAM r, object varName, object value)
        {
            SetLocal(r.GetValue(varName).ToString(), value);
        }
        /// <summary>
        /// Get the value of the localvar with the given name.
        /// </summary>
        /// <param name="varName">The name of the localvar.</param>
        /// <returns>The value found in the localvar.</returns>
        public object GetLocal(string varName)
        {
            try
            {
                return _Locals[varName];
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (KeyNotFoundException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new LocalUndeclaredException(varName);
            }
        }
        /// <summary>
        /// Get the value of the localvar with the given name.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the localvar to get from (nested brack operations execute).</param>
        /// <returns>The value found in the localvar with the given name.</returns>
        public object GetLocal(RAM r, object varName)
        {
            return GetLocal(r.GetValue(varName).ToString());
        }
        /// <summary>
        /// Delete the localvar with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="varName">The name of the localvar to delete.</param>
        public void DeleteLocal(string varName)
        {
            try
            {
                _Locals.Remove(varName);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (KeyNotFoundException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new LocalUndeclaredException(varName);
            }
        }
        /// <summary>
        /// Delete the localvar with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the localvar to delete (nested brack operations execute).</param>
        public void DeleteLocal(RAM r, object varName)
        {
            DeleteLocal(r.GetValue(varName).ToString());
        }
        /// <summary>
        /// Instantiate a new localvar Dictionary.
        /// </summary>
        public void ResetLocals()
        {
            _Locals = new Dictionary<string, object>();
        }
        /// <summary>
        /// Get the names of all localvar.
        /// </summary>
        public String[] LocalVarnames
        {
            get
            {
                List<string> ret = new List<string>();
                foreach (var l in _Locals)
                {
                    ret.Add(l.Key);
                }
                return ret.ToArray();
            }
        }
        #endregion
    }
}
