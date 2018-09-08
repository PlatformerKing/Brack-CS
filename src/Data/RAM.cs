using Brack.Data.Interfaces;
using Brack.Data.Memory;
using Brack.Data.Operations;
using Brack.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brack.Data
{
    /// <summary>
    /// A wrapper for all Random Access Memory used in a Brack Application.
    /// </summary>
    public class RAM : IGlobalVariableDictionary, ILocalVariableDictionary, IScriptDictionary, IScopeList, IOperatorDictionary,  ILocalMemoryStack
    {
        #region Public Properties
        /// <summary>
        /// The GlobalMemory of this Brack application.
        /// </summary>
        public GlobalMemory GlobalMemory { get; private set; }
        /// <summary>
        /// The OperationSet of this Brack application.
        /// </summary>
        public OperationSet OperationSet { get; private set; }
        #endregion
        #region Private Properties
        private Stack<LocalMemory> LocalMemoryStack;
        #endregion
        #region Constructors
        /// <summary>
        /// Empty Constructor.
        /// </summary>
        public RAM() : this(new GlobalMemory(), new OperationSet()) { }
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="globalMemory">The GlobalMemory of this Brack application.</param>
        /// <param name="operationSet">The OperationSet of this Brack application.</param>
        public RAM(GlobalMemory globalMemory, OperationSet operationSet)
        {
            GlobalMemory = globalMemory;
            OperationSet = operationSet;
            LocalMemoryStack = new Stack<LocalMemory>();
        }
        /// <summary>
        /// Overridden Constructor
        /// </summary>
        /// <param name="operationSet">The OperationSet of this Brack application.</param>
        public RAM(OperationSet operationSet) : this(null, operationSet) { }
        #endregion
        #region IGlobalVariableDictionary Implementation
        /// <summary>
        /// The amount of globalvars.
        /// </summary>
        public int GlobalCount
        {
            get
            {
                return GlobalMemory.GlobalCount;
            }
        }
        /// <summary>
        /// Does the given globalvar exist with the given name?
        /// </summary>
        /// <param name="varName">The name of the globalvar to check for.</param>
        /// <returns>If a globalvar exists with the given name.</returns>
        public bool HasGlobal(string varName)
        {
            return GlobalMemory.HasGlobal(varName);
        }
        /// <summary>
        /// Does the given globalvar exist with the given name?
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the globalvar to check for (nested brack operations execute).</param>
        /// <returns>If a globalvar exists with the given name.</returns>
        public bool HasGlobal(RAM r, object varName)
        {
            return GlobalMemory.HasGlobal(r, varName);
        }
        /// <summary>
        /// Set the globalvar with the given name to have the given value, and declare a globalvar with the given name if none exist already.
        /// </summary>
        /// <param name="varName">The name of the globalvar to alter or declare.</param>
        /// <param name="value">The value to store in the globalvar.</param>
        public void SetGlobal(string varName, object value)
        {
            GlobalMemory.SetGlobal(varName, value);
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
            GlobalMemory.SetGlobal(r, varName, value);
        }
        /// <summary>
        /// Get the value of the globalvar with the given name.
        /// </summary>
        /// <param name="varName">The name of the globalvar.</param>
        /// <returns>The value found in the globalvar.</returns>
        public object GetGlobal(string varName)
        {
            return GlobalMemory.GetGlobal(varName);
        }
        /// <summary>
        /// Get the value of the globalvar with the given name.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the globavar to get from (nested brack operations execute).</param>
        /// <returns>The value found in the globalvar with the given name.</returns>
        public object GetGlobal(RAM r, object varName)
        {
            return GlobalMemory.GetGlobal(r, varName);
        }
        /// <summary>
        /// Delete the globalvar with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="varName">The name of the globalvar to delete.</param>
        public void DeleteGlobal(string varName)
        {
            GlobalMemory.DeleteGlobal(varName);
        }
        /// <summary>
        /// Delete the globavar with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the globavar to delete (nested brack operations execute).</param>
        public void DeleteGlobal(RAM r, object varName)
        {
            GlobalMemory.DeleteGlobal(r, varName);
        }
        /// <summary>
        /// Instantiate a new globalvar dictionary.
        /// </summary>
        public void ResetGlobals()
        {
            GlobalMemory.ResetGlobals();
        }
        /// <summary>
        /// Get the names of all globalvars.
        /// </summary>
        public string[] GlobalVarnames
        {
            get
            {
                return GlobalMemory.GlobalVarnames;
            }
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
                return CurrentLocalMemory.LocalCount;
            }
        }
        /// <summary>
        /// Does a localvars exist with the given name?
        /// </summary>
        /// <param name="varName">The name of the localvars to check for.</param>
        /// <returns>If a localvars exists with the given name.</returns>
        public bool HasLocal(string varName)
        {
            return CurrentLocalMemory.HasLocal(varName);
        }
        /// <summary>
        /// Does a localvar exist with the given name?
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the localvars to check for (nested brack operations execute).</param>
        /// <returns>If a localvars exists with the given name.</returns>
        public bool HasLocal(RAM r, object varName)
        {
            return CurrentLocalMemory.HasLocal(r, varName);
        }
        /// <summary>
        /// Set the localvar with the given name to have the given value, and declare a localvar with the given name if none exist already.
        /// </summary>
        /// <param name="varName">The name of the localvar to alter or declare.</param>
        /// <param name="value">The value to store in the localvar.</param>
        public void SetLocal(string varName, object value)
        {
            CurrentLocalMemory.SetLocal(varName, value);
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
            CurrentLocalMemory.SetLocal(r, varName, value);
        }
        /// <summary>
        /// Get the value of the localvar with the given name.
        /// </summary>
        /// <param name="varName">The name of the localvar.</param>
        /// <returns>The value found in the localvar.</returns>
        public object GetLocal(string varName)
        {
            return CurrentLocalMemory.GetLocal(varName);
        }
        /// <summary>
        /// Get the value of the localvar with the given name.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the localvar to get from (nested brack operations execute).</param>
        /// <returns>The value found in the localvar with the given name.</returns>
        public object GetLocal(RAM r, object varName)
        {
            return CurrentLocalMemory.GetLocal(r, varName);
        }
        /// <summary>
        /// Delete the localvar with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="varName">The name of the localvar to delete.</param>
        public void DeleteLocal(string varName)
        {
            CurrentLocalMemory.DeleteLocal(varName);
        }
        /// <summary>
        /// Delete the localvar with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="varName">The name of the localvar to delete (nested brack operations execute).</param>
        public void DeleteLocal(RAM r, object varName)
        {
            CurrentLocalMemory.DeleteLocal(r, varName);
        }
        /// <summary>
        /// Instantiate a new localvar Dictionary.
        /// </summary>
        public void ResetLocals()
        {
            CurrentLocalMemory.ResetLocals();
        }
        /// <summary>
        /// Get the names of all localvar.
        /// </summary>
        public string[] LocalVarnames
        {
            get
            {
                return CurrentLocalMemory.LocalVarnames;
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
                return GlobalMemory.ScriptCount;
            }
        }
        /// <summary>
        /// Does a Script exist with the given name?
        /// </summary>
        /// <param name="scriptName">The name of the Script to check for.</param>
        /// <returns>If a Script exists with the given name.</returns>
        public bool HasScript(string scriptName)
        {
            return GlobalMemory.HasScript(scriptName);
        }
        /// <summary>
        /// Does a Script exist with the given name?
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The name of the Script to check for (nested brack operations execute).</param>
        /// <returns>If a Script exists with the given name.</returns>
        public bool HasScript(RAM r, object scriptName)
        {
            return GlobalMemory.HasScript(r, scriptName);
        }
        /// <summary>
        /// Set the Script with the given name to have the given value, and declare a Script with the given name if none exist already.
        /// </summary>
        /// <param name="scriptName">The name of the Script to alter or declare.</param>
        /// <param name="script">The Script to add.</param>
        public void SetScript(string scriptName, Script script)
        {
            GlobalMemory.SetScript(scriptName, script);
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
            GlobalMemory.SetScript(r, scriptName, script);
        }
        /// <summary>
        /// Get the Script with the given name.
        /// </summary>
        /// <param name="scriptName">The name of the Script.</param>
        /// <returns>The Script found with the given name.</returns>
        public Script GetScript(string scriptName)
        {
            return GlobalMemory.GetScript(scriptName);
        }
        /// <summary>
        /// Get the Script with the given name.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The name of the Script to get from (nested brack operations execute).</param>
        /// <returns>The Script found with the given name.</returns>
        public Script GetScript(RAM r, object scriptName)
        {
            return GlobalMemory.GetScript(r, scriptName);
        }
        /// <summary>
        /// Delete the Script with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="scriptName">The name of the Script to delete.</param>
        public void DeleteScript(string scriptName)
        {
            GlobalMemory.DeleteScript(scriptName);
        }
        /// <summary>
        /// Delete the Script with the given name from memory (with garbage collection).
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The name of the Script to delete (nested brack operations execute).</param>
        public void DeleteScript(RAM r, object scriptName)
        {
            GlobalMemory.DeleteScript(r, scriptName);
        }
        /// <summary>
        /// Instantiate a new Script Dictionary.
        /// </summary>
        public void ResetScripts()
        {
            GlobalMemory.ResetScripts();
        }
        /// <summary>
        /// Get the names of all Scripts.
        /// </summary>
        public String[] ScriptNames
        {
            get
            {
                return GlobalMemory.ScriptNames;
            }
        }
        /// <summary>
        /// Get the names of the arguments of the Script of the given name.
        /// </summary>
        /// <param name="scriptName">The name of the Script .</param>
        /// <returns>The argument names.</returns>
        public string[] GetScriptArguments(string scriptName)
        {
            return GlobalMemory.GetScriptArguments(scriptName);
        }
        /// <summary>
        /// Get the names of the arguments of the given Script from this GlobalMemory.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="scriptName">The name of the Script (nested operators are executed).</param>
        /// <returns>The argument names</returns>
        public string[] GetScriptArguments(RAM r, object scriptName)
        {
            return GlobalMemory.GetScriptArguments(r, scriptName);
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
            return GlobalMemory.ExecuteScript(r, scriptName, args);
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
            return GlobalMemory.ExecuteScript(r, scriptName, args);
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
                return CurrentLocalMemory.ScopeCount;
            }
        }
        /// <summary>
        /// Add a new Scope.
        /// </summary>
        public void AddScope()
        {
            CurrentLocalMemory.AddScope();
        }
        /// <summary>
        /// Delete the top Scope.
        /// </summary>
        public void DeleteTopScope()
        {
            CurrentLocalMemory.DeleteTopScope();
        }
        /// <summary>
        /// Peek the top Scope.
        /// </summary>
        public Scope TopScope
        {
            get
            {
                return CurrentLocalMemory.TopScope;
            }
        }
        /// <summary>
        /// Reset the Sope Stack.
        /// </summary>
        public void ResetScopes()
        {
            CurrentLocalMemory.ResetScopes();
        }
        #endregion
        #region IOperatorDictionary Implementation
        /// <summary>
        /// All Operator names.
        /// </summary>
        public string[] OpNames
        {
            get
            {
                return OperationSet.OpNames;
            }
        }
        /// <summary>
        /// The amount of Operators.
        /// </summary>
        public int OpCount
        {
            get
            {
                return OperationSet.OpCount;
            }
        }
        /// <summary>
        /// Does an Operator exist with the given name?
        /// </summary>
        /// <param name="opName">The name of the Operator to look for.</param>
        /// <returns>If the Operator exists.</returns>
        public bool HasOpName(string opName)
        {
            return OperationSet.HasOpName(opName);
        }
        /// <summary>
        /// Does an Operator exist with the given name?
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="opName">The name of the Operator to look for (nested brack operations execute).</param>
        /// <returns></returns>
        public bool HasOpName(RAM r, object opName)
        {
            return OperationSet.HasOpName(r, opName);
        }
        /// <summary>
        /// Execute an Operator with the given name and arguments.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="opName">The name of the Operator to execute.</param>
        /// <param name="arguments">The arguments to pass into the Operator.</param>
        /// <returns>The resulting return of the Operator execution.</returns>
        public object ExecuteOperator(RAM r, string opName, object[] arguments)
        {
            return OperationSet.ExecuteOperator(r, opName, arguments);
        }
        /// <summary>
        /// Execute an Operator with the given name and arguments.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="opName">The name of the Operator to execute.</param>
        /// <param name="arguments">The arguments to pass into the Operator (nested brack operations execute).</param>
        /// <returns>The resulting return of the Operator execution.</returns>
        public object ExecuteOperator(RAM r, object opName, object[] arguments)
        {
            return OperationSet.ExecuteOperator(r, opName, arguments);
        }
        #endregion
        #region ILocalMemoryStack
        /// <summary>
        /// Push a new LocalMemory.
        /// </summary>
        public void PushNewLocalMemory()
        {
            LocalMemoryStack.Push(new LocalMemory());
        }
        /// <summary>
        /// Remove the last LocalMemory.
        /// </summary>
        public void RemoveLastLocalMemory()
        {
            try
            {
                LocalMemoryStack.Pop();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (InvalidOperationException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new NoLocalMemoryException();
            }
        }
        /// <summary>
        /// The amount of LocalMemories.
        /// </summary>
        public int LocalMemoryCount
        {
            get
            {
                return LocalMemoryStack.Count;
            }
        }
        /// <summary>
        /// Get the top LocalMemory.
        /// </summary>
        public LocalMemory CurrentLocalMemory
        {
            get
            {
                try
                {
                    return LocalMemoryStack.Peek();
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (InvalidOperationException e)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    throw new NoLocalMemoryException();
                }
            }
        }
        /// <summary>
        /// Reset the LocalMemory List.
        /// </summary>
        public void ResetLocalMemories()
        {
            LocalMemoryStack = new Stack<LocalMemory>();
        }
        #endregion
        #region Public Utility Methods
        /// <summary>
        /// Get a value form an object at runtime.
        /// </summary>
        /// <param name="value">The value (nested Brack statements execute).</param>
        /// <returns>The object value.</returns>
        public object GetValue(object value)
        {
            return (value is object[][])? value: (value is object[]) ? ExecuteOperator(this, (((((object[])value)[0]) is object[])? GetValue((object[])(((object[])value)[0])): (((object[])value)[0]).ToString()), ((object[])value).Skip(1).ToArray()) : value;
        }
        #endregion
    }
}
