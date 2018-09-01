using System;
using System.Linq;

namespace Brack.Data.Operations
{
    #region Base Class
    /// <summary>
    /// The base of all Brack Operators.
    /// </summary>
    public abstract class BrackOperatorBase
    {
        #region Public Constants
        /// <summary>
        /// The amount of arguments in a Brack param operator.
        /// </summary>
        public const int ParamArgCount = -1;
        #endregion
        #region Protected Properties
        /// <summary>
        /// The types of the arguments accepted in this BrackOperator.
        /// </summary>
        protected object _Types;
        /// <summary>
        /// The delegate of this BrackOperator.
        /// </summary>
        protected Delegate _BrackDelegate;
        #endregion
        #region Immutable Properties
        /// <summary>
        /// The name of this BrackOperator (the OpName).
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Is this BrackOperator a Param Operator?
        /// </summary>
        public bool IsParam
        {
            get
            {
                return (_Types is Type);
            }
        }
        /// <summary>
        /// The amount of arguments accepted in this BrackOperator.
        /// </summary>
        public int ArgCount
        {
            get
            {
                return (IsParam) ? ParamArgCount : ((Type[])_Types).Length;
            }
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperatorBase(string name, Delegate imp)
        {
            _BrackDelegate = imp;
            Name = name;
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The return of this operation.</returns>
        public virtual object Execute(RAM r, object[] args)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Protected Methods
        /// <summary>
        /// Check arguments to be valid for execution.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The calculated arguments.</returns>
        protected object[] CheckArgs(RAM r, object[] args)
        {
            try
            {
                if (args.Length != ArgCount)
                {
                    throw new ArgumentException();
                }
                var ret = new object[ArgCount];
                var i = 0;
                foreach (var a in args)
                {
                    if (a is object[])
                    {
                        ret[i++] = r.GetName(a);
                    }
                    else
                    {
                        ret[i++] = a;
                    }
                }
                return ret;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (InvalidCastException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new ArgumentException();
            }
        }
        #endregion
    }
    #endregion
    #region Sub-Classes
    /// <summary>
    /// A BrackOperator which accepts one or more arguments of a certain type.
    /// </summary>
    /// <typeparam name="T">The generic param type.</typeparam>
    public class BrackParamOperator<T> : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackParamOperator(string name, BrackParamDelegate<T> imp) : base(name, imp)
        {
            _Types = typeof(T);
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            try
            {
                var f = new T[args.Length];
                int i = 0;
                foreach (var a in args)
                {
                    if (a is object[])
                    {
                        f[i++] = (T)r.GetValue(a);
                    }
                    else
                    {
                        f[i++] = (T)a;
                    }
                }
                return ((BrackParamDelegate<T>)_BrackDelegate)(r, f);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (InvalidCastException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new ArgumentException();
            }
        }
        #endregion
    }
    /// <summary>
    /// A BrackOperator which accepts no arguments.
    /// </summary>
    public class BrackOperator : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperator(string name, BrackDelegate imp) : base(name, imp)
        {
            _Types = new Type[] { };
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            CheckArgs(r, args);
            return ((BrackDelegate)_BrackDelegate)(r);
        }
        #endregion
    }
    /// <summary>
    /// A BrackOperator which accepts one argument of a certain type.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    public class BrackOperator<T1> : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperator(string name, BrackDelegate<T1> imp) : base(name, imp)
        {
            _Types = new Type[] { typeof(T1) };
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            var f = CheckArgs(r, args);
            return ((BrackDelegate<T1>)_BrackDelegate)(r, (T1) f[0]);
        }
        #endregion
    }
    /// <summary>
    /// A BrackOperator which accepts two arguments of certain types.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    public class BrackOperator<T1, T2> : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperator(string name, BrackDelegate<T1, T2> imp) : base(name, imp)
        {
            _Types = new Type[] { typeof(T1), typeof(T2) };
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            var f = CheckArgs(r, args);
            return ((BrackDelegate<T1, T2>)_BrackDelegate)(r, (T1)f[0],(T2)f[1]);
        }
        #endregion
    }
    /// <summary>
    /// A BrackOperator which accepts three arguments of certain types.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    public class BrackOperator<T1, T2, T3> : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperator(string name, BrackDelegate<T1, T2, T3> imp) : base(name, imp)
        {
            _Types = new Type[] { typeof(T1), typeof(T2), typeof(T3) };
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            var f = CheckArgs(r, args);
            return ((BrackDelegate<T1, T2, T3>)_BrackDelegate)(r, (T1)f[0], (T2)f[1], (T3)f[2]);
        }
        #endregion
    }
    /// <summary>
    /// A BrackOperator which accepts four arguments of certain types.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    public class BrackOperator<T1, T2, T3, T4> : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperator(string name, BrackDelegate<T1, T2, T3, T4> imp) : base(name, imp)
        {
            _Types = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            var f = CheckArgs(r, args);
            return ((BrackDelegate<T1, T2, T3, T4>)_BrackDelegate)(r, (T1)f[0], (T2)f[1], (T3)f[2], (T4)f[3]);
        }
        #endregion
    }
    /// <summary>
    /// A BrackOperator which accepts five arguments of certain types.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    public class BrackOperator<T1, T2, T3, T4, T5> : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperator(string name, BrackDelegate<T1, T2, T3, T4, T5> imp) : base(name, imp)
        {
            _Types = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) };
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            var f = CheckArgs(r, args);
            return ((BrackDelegate<T1, T2, T3, T4, T5>)_BrackDelegate)(r, (T1)f[0], (T2)f[1], (T3)f[2], (T4)f[3], (T5)f[4]);
        }
        #endregion
    }
    /// <summary>
    /// A BrackOperator which accepts six arguments of certain types.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    public class BrackOperator<T1, T2, T3, T4, T5, T6> : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperator(string name, BrackDelegate<T1, T2, T3, T4, T5, T6> imp) : base(name, imp)
        {
            _Types = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6) };
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            var f = CheckArgs(r, args);
            return ((BrackDelegate<T1, T2, T3, T4, T5, T6>)_BrackDelegate)(r, (T1)f[0], (T2)f[1], (T3)f[2], (T4)f[3], (T5)f[4], (T6)f[5]);
        }
        #endregion
    }
    /// <summary>
    /// A BrackOperator which accepts seven arguments of certain types.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    public class BrackOperator<T1, T2, T3, T4, T5, T6, T7> : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperator(string name, BrackDelegate<T1, T2, T3, T4, T5, T6, T7> imp) : base(name, imp)
        {
            _Types = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7) };
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            var f = CheckArgs(r, args);
            return ((BrackDelegate<T1, T2, T3, T4, T5, T6, T7>)_BrackDelegate)(r, (T1)f[0], (T2)f[1], (T3)f[2], (T4)f[3], (T5)f[4], (T6)f[5], (T7)f[6]);
        }
        #endregion
    }
    /// <summary>
    /// A BrackOperator which accepts eight arguments of certain types.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <typeparam name="T8">The generic type of the eigth argument.</typeparam>
    public class BrackOperator<T1, T2, T3, T4, T5, T6, T7, T8> : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperator(string name, BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8> imp) : base(name, imp)
        {
            _Types = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8) };
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            var f = CheckArgs(r, args);
            return ((BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8>)_BrackDelegate)(r, (T1)f[0], (T2)f[1], (T3)f[2], (T4)f[3], (T5)f[4], (T6)f[5], (T7)f[6], (T8)f[7]);
        }
        #endregion
    }
    /// <summary>
    /// A BrackOperator which accepts nine arguments of certain types.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <typeparam name="T8">The generic type of the eigth argument.</typeparam>
    /// <typeparam name="T9">The generic type of the ninth argument.</typeparam>
    public class BrackOperator<T1, T2, T3, T4, T5, T6, T7, T8, T9> : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperator(string name, BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9> imp) : base(name, imp)
        {
            _Types = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9) };
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            var f = CheckArgs(r, args);
            return ((BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9>)_BrackDelegate)(r, (T1)f[0], (T2)f[1], (T3)f[2], (T4)f[3], (T5)f[4], (T6)f[5], (T7)f[6], (T8)f[7], (T9)f[8]);
        }
        #endregion
    }
    /// <summary>
    /// A BrackOperator which accepts ten arguments of certain types.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <typeparam name="T8">The generic type of the eigth argument.</typeparam>
    /// <typeparam name="T9">The generic type of the ninth argument.</typeparam>
    /// <typeparam name="T10">The generic type of the tenth argument.</typeparam>
    public class BrackOperator<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperator(string name, BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> imp) : base(name, imp)
        {
            _Types = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10) };
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            var f = CheckArgs(r, args);
            return ((BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>)_BrackDelegate)(r, (T1)f[0], (T2)f[1], (T3)f[2], (T4)f[3], (T5)f[4], (T6)f[5], (T7)f[6], (T8)f[7], (T9)f[8],  (T10)f[9]);
        }
        #endregion
    }
    /// <summary>
    /// A BrackOperator which accepts eleven arguments of certain types.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <typeparam name="T8">The generic type of the eigth argument.</typeparam>
    /// <typeparam name="T9">The generic type of the ninth argument.</typeparam>
    /// <typeparam name="T10">The generic type of the tenth argument.</typeparam>
    /// <typeparam name="T11">The generic type of the eleventh argument.</typeparam>
    public class BrackOperator<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperator(string name, BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> imp) : base(name, imp)
        {
            _Types = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11) };
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            var f = CheckArgs(r, args);
            return ((BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>)_BrackDelegate)(r, (T1)f[0], (T2)f[1], (T3)f[2], (T4)f[3], (T5)f[4], (T6)f[5], (T7)f[6], (T8)f[7], (T9)f[8], (T10)f[9], (T11)f[10]);
        }
        #endregion
    }
    /// <summary>
    /// A BrackOperator which accepts tweleve arguments of certain types.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <typeparam name="T8">The generic type of the eigth argument.</typeparam>
    /// <typeparam name="T9">The generic type of the ninth argument.</typeparam>
    /// <typeparam name="T10">The generic type of the tenth argument.</typeparam>
    /// <typeparam name="T11">The generic type of the eleventh argument.</typeparam>
    /// <typeparam name="T12">The generic type of the twelvth argument.</typeparam>
    public class BrackOperator<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperator(string name, BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> imp) : base(name, imp)
        {
            _Types = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12) };
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            var f = CheckArgs(r, args);
            return ((BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>)_BrackDelegate)(r, (T1)f[0], (T2)f[1], (T3)f[2], (T4)f[3], (T5)f[4], (T6)f[5], (T7)f[6], (T8)f[7], (T9)f[8], (T10)f[9], (T11)f[10], (T12)f[11]);
        }
        #endregion
    }
    /// <summary>
    /// A BrackOperator which accepts thirteen arguments of certain types.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <typeparam name="T8">The generic type of the eigth argument.</typeparam>
    /// <typeparam name="T9">The generic type of the ninth argument.</typeparam>
    /// <typeparam name="T10">The generic type of the tenth argument.</typeparam>
    /// <typeparam name="T11">The generic type of the eleventh argument.</typeparam>
    /// <typeparam name="T12">The generic type of the twelvth argument.</typeparam>
    /// <typeparam name="T13">The generic type of the thirteenth argument.</typeparam>
    public class BrackOperator<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperator(string name, BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> imp) : base(name, imp)
        {
            _Types = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13) };
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            var f = CheckArgs(r, args);
            return ((BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>)_BrackDelegate)(r, (T1)f[0], (T2)f[1], (T3)f[2], (T4)f[3], (T5)f[4], (T6)f[5], (T7)f[6], (T8)f[7], (T9)f[8], (T10)f[9], (T11)f[10], (T12)f[11], (T13)f[12]);
        }
        #endregion
    }
    /// <summary>
    /// A BrackOperator which accepts fourteen arguments of certain types.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <typeparam name="T8">The generic type of the eigth argument.</typeparam>
    /// <typeparam name="T9">The generic type of the ninth argument.</typeparam>
    /// <typeparam name="T10">The generic type of the tenth argument.</typeparam>
    /// <typeparam name="T11">The generic type of the eleventh argument.</typeparam>
    /// <typeparam name="T12">The generic type of the twelvth argument.</typeparam>
    /// <typeparam name="T13">The generic type of the thirteenth argument.</typeparam>
    /// <typeparam name="T14">The generic type of the fourteenth argument.</typeparam>
    public class BrackOperator<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperator(string name, BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> imp) : base(name, imp)
        {
            _Types = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14) };
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            var f = CheckArgs(r, args);
            return ((BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>)_BrackDelegate)(r, (T1)f[0], (T2)f[1], (T3)f[2], (T4)f[3], (T5)f[4], (T6)f[5], (T7)f[6], (T8)f[7], (T9)f[8], (T10)f[9], (T11)f[10], (T12)f[11], (T13)f[12], (T14)f[13]);
        }
        #endregion
    }
    /// <summary>
    /// A BrackOperator which accepts fifteen arguments of certain types.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <typeparam name="T8">The generic type of the eigth argument.</typeparam>
    /// <typeparam name="T9">The generic type of the ninth argument.</typeparam>
    /// <typeparam name="T10">The generic type of the tenth argument.</typeparam>
    /// <typeparam name="T11">The generic type of the eleventh argument.</typeparam>
    /// <typeparam name="T12">The generic type of the twelvth argument.</typeparam>
    /// <typeparam name="T13">The generic type of the thirteenth argument.</typeparam>
    /// <typeparam name="T14">The generic type of the fourteenth argument.</typeparam>
    /// <typeparam name="T15">The generic type of the fifteenth argument.</typeparam>
    public class BrackOperator<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : BrackOperatorBase
    {
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="name">The name of this BrackOperator.</param>
        /// <param name="imp">The implementation of this BrackOperator.</param>
        public BrackOperator(string name, BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> imp) : base(name, imp)
        {
            _Types = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15) };
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Execute this BrackOperator.
        /// </summary>
        /// <param name="r">The current memory.</param>
        /// <param name="args">The arguments to pass in.</param>
        /// <returns>The return of this operation.</returns>
        public override object Execute(RAM r, object[] args)
        {
            var f = CheckArgs(r, args);
            return ((BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)_BrackDelegate)(r, (T1)f[0], (T2)f[1], (T3)f[2], (T4)f[3], (T5)f[4], (T6)f[5], (T7)f[6], (T8)f[7], (T9)f[8], (T10)f[9], (T11)f[10], (T12)f[11], (T13)f[12], (T14)f[13], (T15)f[14]);
        }
        #endregion
    }
    #endregion
}
