using Brack.Data.Interfaces;
using Brack.Exceptions;
using System;
using System.Collections.Generic;

namespace Brack.Data.Memory
{
    /// <summary>
    /// Class for managing local memory data for a Brack application.
    /// </summary>
    public class LocalMemory : ILocalVariableDictionary, IScopeList
    {
        #region Private Properties
        /// <summary>
        /// The list of Scopes.
        /// </summary>
        private List<Scope> _Scopes;
        #endregion
        #region Constructors
        /// <summary>
        /// The default Constructor.
        /// </summary>
        public LocalMemory()
        {
            ResetScopes();
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
                var ret = 0;
                foreach(var s in _Scopes)
                {
                    ret += s.LocalCount;
                }
                return ret;
            }
        }
        /// <summary>
        /// Does a localvars exist with the given name?
        /// </summary>
        /// <param name="varName">The name of the localvars to check for.</param>
        /// <returns>If a localvars exists with the given name.</returns>
        public bool HasLocal(string varName)
        {
            foreach(var s in _Scopes)
            {
                if (s.HasLocal(varName))
                {
                    return true;
                }
            }
            return false;
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
            try
            {
                foreach (var s in _Scopes)
                {
                    if (s.HasLocal(varName))
                    {
                        s.SetLocal(varName, value);
                        return;
                    }
                }
                _Scopes[_Scopes.Count - 1].SetLocal(varName, value);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (InvalidOperationException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new NoScopeException();
            }
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
            foreach (var s in _Scopes)
            {
                if (s.HasLocal(varName))
                {
                    return s.GetLocal(varName);
                }
            }
            throw new LocalUndeclaredException(varName);
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
            foreach (var s in _Scopes)
            {
                if (s.HasLocal(varName))
                {
                    s.DeleteLocal(varName);
                    return;
                }
            }
            throw new LocalUndeclaredException(varName);
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
            foreach(var s in _Scopes)
            {
                s.ResetLocals();
            }
        }
        /// <summary>
        /// Get the names of all localvar.
        /// </summary>
        public string[] LocalVarnames
        {
            get
            {
                List<string> ret = new List<string>();
                foreach(var s in _Scopes)
                {
                    ret.AddRange(s.LocalVarnames);
                }
                return ret.ToArray();
            }
        }
        #endregion
        #region IScopeList Implementation
        /// <summary>
        /// The amount of Scopes.
        /// </summary>
        public int ScopeCount
        {
            get
            {
                return _Scopes.Count;
            }
        }
        /// <summary>
        /// Add a new Scope.
        /// </summary>
        public void AddScope()
        {
            _Scopes.Add(new Scope());
        }
        /// <summary>
        /// Delete the top Scope.
        /// </summary>
        public void DeleteTopScope()
        {
            try
            {
                _Scopes.RemoveAt(_Scopes.Count - 1);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (InvalidOperationException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new NoScopeException();
            }
        }
        /// <summary>
        /// Peek the top Scope.
        /// </summary>
        public Scope TopScope
        {
            get
            {
                try
                {
                    return _Scopes[_Scopes.Count - 1];
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (IndexOutOfRangeException e)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    throw new NoScopeException();
                }
            }
        }
        /// <summary>
        /// Reset the Sope Stack.
        /// </summary>
        public void ResetScopes()
        {
            _Scopes = new List<Scope>
            {
                new Scope()
            };
        }
        #endregion
    }
}
