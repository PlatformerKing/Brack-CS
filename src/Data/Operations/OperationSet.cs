using Brack.Data.Interfaces;
using Brack.Exceptions;
using System.Collections.Generic;

namespace Brack.Data.Operations
{
    /// <summary>
    /// A manager for all BrackOperators used in a Brack application.
    /// </summary>
    public class OperationSet : IOperatorDictionary
    {
        #region Private Properties
        /// <summary>
        /// Dictionary of all Operators.
        /// </summary>
        private Dictionary<string, BrackOperatorBase> _Operators;
        #endregion
        #region Constructors
        /// <summary>
        /// Empty Constructor.
        /// </summary>
        public OperationSet() : this(new BrackOperatorBase[0]) { }
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="operators"></param>
        public OperationSet(BrackOperatorBase[] operators)
        {
            _Operators = new Dictionary<string, BrackOperatorBase>();
            foreach (var o in operators)
            {
                if (HasOpName(o.Name))
                {
                    throw new OperatorException(o.Name);
                }
                _Operators[o.Name] = o;
            }
        }
        #endregion
        #region IOperatorSet Implementaion
        /// <summary>
        /// All Operator names.
        /// </summary>
        public string[] OpNames
        {
            get
            {
                List<string> ret = new List<string>();
                foreach(var o in _Operators)
                {
                    ret.Add(o.Key);
                }
                return ret.ToArray();
            }
        }
        /// <summary>
        /// The amount of Operators.
        /// </summary>
        public int OpCount
        {
            get
            {
                return _Operators.Count;
            }
        }
        /// <summary>
        /// Does an Operator exist with the given name?
        /// </summary>
        /// <param name="opName">The name of the Operator to look for.</param>
        /// <returns>If the Operator exists.</returns>
        public bool HasOpName(string opName)
        {
            return _Operators.ContainsKey(opName);
        }
        /// <summary>
        /// Does an Operator exist with the given name?
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="opName">The name of the Operator to look for (nested brack operations execute).</param>
        /// <returns></returns>
        public bool HasOpName(RAM r, object opName)
        {
            return HasOpName(r.GetName(opName));
        }
        /// <summary>
        /// Execute an Operator with the given name and arguments.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="opName">The name of the Operator to execute.</param>
        /// <param name="args">The arguments to pass into the Operator.</param>
        /// <returns>The resulting return of the Operator execution.</returns>
        public object ExecuteOperator(RAM r, string opName, object[] args)
        {
            try
            {
                return _Operators[opName].Execute(r, args);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (KeyNotFoundException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new OperatorException(opName);
            }
        }
        /// <summary>
        /// Execute an Operator with the given name and arguments.
        /// </summary>
        /// <param name="r">The RAM used for this execution.</param>
        /// <param name="opName">The name of the Operator to execute.</param>
        /// <param name="args">The arguments to pass into the Operator (nested brack operations execute).</param>
        /// <returns>The resulting return of the Operator execution.</returns>
        public object ExecuteOperator(RAM r, object opName, object[] args)
        {
            return ExecuteOperator(r, r.GetName(opName), args);
        }
        #endregion
    }
}
