using Brack.Interpretation;
using System;

namespace Brack.Data.Memory
{
    /// <summary>
    /// Manager for a Brack Script.
    /// </summary>
    public class Script
    {
        #region Private Properties
        /// <summary>
        /// The Brack statements.
        /// </summary>
        private object[][] _Brack;
        /// <summary>
        /// The argument names.
        /// </summary>
        private string[] _ArgNames;
        #endregion
        #region Constructors
        /// <summary>
        /// Overloaded Constructor
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="brack">The brack statements.</param>
        /// <param name="argNames">The arguments.</param>
        public Script(RAM r, object[][] brack, object[] argNames)
        {
            _Brack = brack;
            _ArgNames = new string[argNames.Length];
            int i = 0;
            foreach(var a in argNames)
            {
                _ArgNames[i++] = r.GetName(a);
            }
        }
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="brack">The brack statements.</param>
        /// <param name="argNames">The arguments.</param>
        public Script(object[][] brack, string[] argNames)
        {
            _Brack = brack;
            _ArgNames = argNames;
        }
        #endregion
        #region Immutable Properties
        /// <summary>
        /// The argument count.
        /// </summary>
        public int ArgCount
        {
            get
            {
                return _ArgNames.Length;
            }
        }
        /// <summary>
        /// The Brack statement count.
        /// </summary>
        public int BrackCount
        {
            get
            {
                return _Brack.Length;
            }
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Get the argument name of the given index.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="index">The index of the argument to get (nested Brack statements execute).</param>
        /// <returns>The name of the argument.</returns>
        public string GetArgName(RAM r, object index)
        {
            try
            {
                return GetArgName(Convert.ToInt32(r.GetFloat(index)));
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new ArgumentException();
            }
        }
        /// <summary>
        /// Get the argument name of the given index.
        /// </summary>
        /// <param name="index">The index of the argument to get.</param>
        /// <returns>The name of the argument.</returns>
        public string GetArgName(int index)
        {
            try
            {
                return _ArgNames[index];
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (IndexOutOfRangeException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new ArgumentException();
            }
        }
        /// <summary>
        /// The names of all arguments.
        /// </summary>
        /// <returns></returns>
        public string[] GetArgNames()
        {
            return (string[])_ArgNames.Clone();
        }
        /// <summary>
        /// Execute this script.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The resulting return.</returns>
        public object Execute(RAM r, object[] args)
        {
            if (args.Length != ArgCount)
            {
                throw new ArgumentException();
            }
            r.PushNewLocalMemory();
            var i = 0;
            foreach(var a in args)
            {
                var farg = r.GetName(a);
                if (float.TryParse(farg, out float f))
                {
                    r.SetLocal(_ArgNames[i++], f);
                }
                else
                {
                    r.SetLocal(_ArgNames[i++], farg);
                }
            }
            var obj = BrackInterpreter.Execute(r, _Brack);
            r.RemoveLastLocalMemory();
            return obj;
        }
        #endregion
    }
}
