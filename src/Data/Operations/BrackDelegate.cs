namespace Brack.Data.Operations
{
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts or more arguments of a single type.
    /// </summary>
    /// <typeparam name="T">The generic type of all arguments.</typeparam>
    /// <param name="r">The current memory.</param>
    /// <param name="args">The arguments array.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackParamDelegate<T>(RAM r, T[] args);
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts no arguments.
    /// </summary>
    /// <param name="r">The current memory.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackDelegate(RAM r);
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts one argument.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <param name="r">The current memory.</param>
    /// <param name="Arg1">The first argument.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackDelegate<T1>(RAM r, T1 Arg1);
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts two arguments.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <param name="r">The current memory.</param>
    /// <param name="Arg1">The first argument.</param>
    /// <param name="Arg2">The second argument.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackDelegate<T1,T2>(RAM r, T1 Arg1, T2 Arg2);
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts three arguments.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>\
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <param name="r">The current memory.</param>
    /// <param name="Arg1">The first argument.</param>
    /// <param name="Arg2">The second argument.</param>
    /// <param name="Arg3">The third argument.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackDelegate<T1, T2, T3>(RAM r, T1 Arg1, T2 Arg2, T3 Arg3);
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts four arguments.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <param name="r">The current memory.</param>
    /// <param name="Arg1">The first argument.</param>
    /// <param name="Arg2">The second argument.</param>
    /// <param name="Arg3">The third argument.</param>
    /// <param name="Arg4">The fourth argument.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackDelegate<T1, T2, T3, T4>(RAM r, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4);
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts five arguments.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <param name="r">The current memory.</param>
    /// <param name="Arg1">The first argument.</param>
    /// <param name="Arg2">The second argument.</param>
    /// <param name="Arg3">The third argument.</param>
    /// <param name="Arg4">The fourth argument.</param>
    /// <param name="Arg5">The fifth argument.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackDelegate<T1, T2, T3, T4, T5>(RAM r, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5);
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts six arguments.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <param name="r">The current memory.</param>
    /// <param name="Arg1">The first argument.</param>
    /// <param name="Arg2">The second argument.</param>
    /// <param name="Arg3">The third argument.</param>
    /// <param name="Arg4">The fourth argument.</param>
    /// <param name="Arg5">The fifth argument.</param>
    /// <param name="Arg6">The sixth argument.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackDelegate<T1, T2, T3, T4, T5, T6>(RAM r, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6);
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts seven arguments.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <param name="r">The current memory.</param>
    /// <param name="Arg1">The first argument.</param>
    /// <param name="Arg2">The second argument.</param>
    /// <param name="Arg3">The third argument.</param>
    /// <param name="Arg4">The fourth argument.</param>
    /// <param name="Arg5">The fifth argument.</param>
    /// <param name="Arg6">The sixth argument.</param>
    /// <param name="Arg7">The seventh argument.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackDelegate<T1, T2, T3, T4, T5, T6, T7>(RAM r, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6, T7 Arg7);
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts eigth arguments.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <typeparam name="T8">The generic type of the eight argument.</typeparam>
    /// <param name="r">The current memory.</param>
    /// <param name="Arg1">The first argument.</param>
    /// <param name="Arg2">The second argument.</param>
    /// <param name="Arg3">The third argument.</param>
    /// <param name="Arg4">The fourth argument.</param>
    /// <param name="Arg5">The fifth argument.</param>
    /// <param name="Arg6">The sixth argument.</param>
    /// <param name="Arg7">The seventh argument.</param>
    /// <param name="Arg8">The eigth argument.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8>(RAM r, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6, T7 Arg7, T8 Arg8);
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts nine arguments.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <typeparam name="T8">The generic type of the eighth argument.</typeparam>
    /// <typeparam name="T9">The generic type of the ninth argument.</typeparam>
    /// <param name="r">The current memory.</param>
    /// <param name="Arg1">The first argument.</param>
    /// <param name="Arg2">The second argument.</param>
    /// <param name="Arg3">The third argument.</param>
    /// <param name="Arg4">The fourth argument.</param>
    /// <param name="Arg5">The fifth argument.</param>
    /// <param name="Arg6">The sixth argument.</param>
    /// <param name="Arg7">The seventh argument.</param>
    /// <param name="Arg8">The eighth argument.</param>
    /// <param name="Arg9">The ninth argument.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9>(RAM r, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6, T7 Arg7, T8 Arg8, T9 Arg9);
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts ten arguments.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <typeparam name="T8">The generic type of the eighth argument.</typeparam>
    /// <typeparam name="T9">The generic type of the ninth argument.</typeparam>
    /// <typeparam name="T10">The generic type of the tenth argument.</typeparam>
    /// <param name="r">The current memory.</param>
    /// <param name="Arg1">The first argument.</param>
    /// <param name="Arg2">The second argument.</param>
    /// <param name="Arg3">The third argument.</param>
    /// <param name="Arg4">The fourth argument.</param>
    /// <param name="Arg5">The fifth argument.</param>
    /// <param name="Arg6">The sixth argument.</param>
    /// <param name="Arg7">The seventh argument.</param>
    /// <param name="Arg8">The eighth argument.</param>
    /// <param name="Arg9">The ninth argument.</param>
    /// <param name="Arg10">The tenth argument.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(RAM r, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6, T7 Arg7, T8 Arg8, T9 Arg9, T10 Arg10);
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts eleven arguments.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <typeparam name="T8">The generic type of the eighth argument.</typeparam>
    /// <typeparam name="T9">The generic type of the ninth argument.</typeparam>
    /// <typeparam name="T10">The generic type of the tenth argument.</typeparam>
    /// <typeparam name="T11">The generic type of the eleventh argument.</typeparam>
    /// <param name="r">The current memory.</param>
    /// <param name="Arg1">The first argument.</param>
    /// <param name="Arg2">The second argument.</param>
    /// <param name="Arg3">The third argument.</param>
    /// <param name="Arg4">The fourth argument.</param>
    /// <param name="Arg5">The fifth argument.</param>
    /// <param name="Arg6">The sixth argument.</param>
    /// <param name="Arg7">The seventh argument.</param>
    /// <param name="Arg8">The eighth argument.</param>
    /// <param name="Arg9">The ninth argument.</param>
    /// <param name="Arg10">The tenth argument.</param>
    /// <param name="Arg11">The eleventh argument.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(RAM r, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6, T7 Arg7, T8 Arg8, T9 Arg9, T10 Arg10, T11 Arg11);
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts twelve arguments.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <typeparam name="T8">The generic type of the eighth argument.</typeparam>
    /// <typeparam name="T9">The generic type of the ninth argument.</typeparam>
    /// <typeparam name="T10">The generic type of the tenth argument.</typeparam>
    /// <typeparam name="T11">The generic type of the eleventh argument.</typeparam>
    /// <typeparam name="T12">The generic type of the twelvth argument.</typeparam>
    /// <param name="r">The current memory.</param>
    /// <param name="Arg1">The first argument.</param>
    /// <param name="Arg2">The second argument.</param>
    /// <param name="Arg3">The third argument.</param>
    /// <param name="Arg4">The fourth argument.</param>
    /// <param name="Arg5">The fifth argument.</param>
    /// <param name="Arg6">The sixth argument.</param>
    /// <param name="Arg7">The seventh argument.</param>
    /// <param name="Arg8">The eighth argument.</param>
    /// <param name="Arg9">The ninth argument.</param>
    /// <param name="Arg10">The tenth argument.</param>
    /// <param name="Arg11">The eleventh argument.</param>
    /// <param name="Arg12">The twelveth argument.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(RAM r, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6, T7 Arg7, T8 Arg8, T9 Arg9, T10 Arg10, T11 Arg11, T12 Arg12);
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts thirteen arguments.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <typeparam name="T8">The generic type of the eighth argument.</typeparam>
    /// <typeparam name="T9">The generic type of the ninth argument.</typeparam>
    /// <typeparam name="T10">The generic type of the tenth argument.</typeparam>
    /// <typeparam name="T11">The generic type of the eleventh argument.</typeparam>
    /// <typeparam name="T12">The generic type of the twelvth argument.</typeparam>
    /// <typeparam name="T13">The generic type of the thirteenth argument.</typeparam>
    /// <param name="r">The current memory.</param>
    /// <param name="Arg1">The first argument.</param>
    /// <param name="Arg2">The second argument.</param>
    /// <param name="Arg3">The third argument.</param>
    /// <param name="Arg4">The fourth argument.</param>
    /// <param name="Arg5">The fifth argument.</param>
    /// <param name="Arg6">The sixth argument.</param>
    /// <param name="Arg7">The seventh argument.</param>
    /// <param name="Arg8">The eighth argument.</param>
    /// <param name="Arg9">The ninth argument.</param>
    /// <param name="Arg10">The tenth argument.</param>
    /// <param name="Arg11">The eleventh argument.</param>
    /// <param name="Arg12">The twelveth argument.</param>
    /// <param name="Arg13">The thirteenth argument.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(RAM r, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6, T7 Arg7, T8 Arg8, T9 Arg9, T10 Arg10, T11 Arg11, T12 Arg12, T13 Arg13);
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts fourteen arguments.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <typeparam name="T8">The generic type of the eighth argument.</typeparam>
    /// <typeparam name="T9">The generic type of the ninth argument.</typeparam>
    /// <typeparam name="T10">The generic type of the tenth argument.</typeparam>
    /// <typeparam name="T11">The generic type of the eleventh argument.</typeparam>
    /// <typeparam name="T12">The generic type of the twelvth argument.</typeparam>
    /// <typeparam name="T13">The generic type of the thirteenth argument.</typeparam>
    /// <typeparam name="T14">The generic type of the fourteenth argument.</typeparam>
    /// <param name="r">The current memory.</param>
    /// <param name="Arg1">The first argument.</param>
    /// <param name="Arg2">The second argument.</param>
    /// <param name="Arg3">The third argument.</param>
    /// <param name="Arg4">The fourth argument.</param>
    /// <param name="Arg5">The fifth argument.</param>
    /// <param name="Arg6">The sixth argument.</param>
    /// <param name="Arg7">The seventh argument.</param>
    /// <param name="Arg8">The eighth argument.</param>
    /// <param name="Arg9">The ninth argument.</param>
    /// <param name="Arg10">The tenth argument.</param>
    /// <param name="Arg11">The eleventh argument.</param>
    /// <param name="Arg12">The twelveth argument.</param>
    /// <param name="Arg13">The thirteenth argument.</param>
    /// <param name="Arg14">The fourteenth argument.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(RAM r, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6, T7 Arg7, T8 Arg8, T9 Arg9, T10 Arg10, T11 Arg11, T12 Arg12, T13 Arg13, T14 Arg14);
    /// <summary>
    /// A delegate for encapsulating a Brack operation that accepts fifteenth arguments.
    /// </summary>
    /// <typeparam name="T1">The generic type of the first argument.</typeparam>
    /// <typeparam name="T2">The generic type of the second argument.</typeparam>
    /// <typeparam name="T3">The generic type of the third argument.</typeparam>
    /// <typeparam name="T4">The generic type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The generic type of the fifth argument.</typeparam>
    /// <typeparam name="T6">The generic type of the sixth argument.</typeparam>
    /// <typeparam name="T7">The generic type of the seventh argument.</typeparam>
    /// <typeparam name="T8">The generic type of the eighth argument.</typeparam>
    /// <typeparam name="T9">The generic type of the ninth argument.</typeparam>
    /// <typeparam name="T10">The generic type of the tenth argument.</typeparam>
    /// <typeparam name="T11">The generic type of the eleventh argument.</typeparam>
    /// <typeparam name="T12">The generic type of the twelvth argument.</typeparam>
    /// <typeparam name="T13">The generic type of the thirteenth argument.</typeparam>
    /// <typeparam name="T14">The generic type of the fourteenth argument.</typeparam>
    /// <typeparam name="T15">The generic type of the fifteenth argument.</typeparam>
    /// <param name="r">The current memory.</param>
    /// <param name="Arg1">The first argument.</param>
    /// <param name="Arg2">The second argument.</param>
    /// <param name="Arg3">The third argument.</param>
    /// <param name="Arg4">The fourth argument.</param>
    /// <param name="Arg5">The fifth argument.</param>
    /// <param name="Arg6">The sixth argument.</param>
    /// <param name="Arg7">The seventh argument.</param>
    /// <param name="Arg8">The eighth argument.</param>
    /// <param name="Arg9">The ninth argument.</param>
    /// <param name="Arg10">The tenth argument.</param>
    /// <param name="Arg11">The eleventh argument.</param>
    /// <param name="Arg12">The twelveth argument.</param>
    /// <param name="Arg13">The thirteenth argument.</param>
    /// <param name="Arg15">The fifteenth argument.</param>
    /// <returns>The delegate return.</returns>
    public delegate object BrackDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(RAM r, T1 Arg1, T2 Arg2, T3 Arg3, T4 Arg4, T5 Arg5, T6 Arg6, T7 Arg7, T8 Arg8, T9 Arg9, T10 Arg10, T11 Arg11, T12 Arg12, T13 Arg13, T14 Arg14, T15 Arg15);
}