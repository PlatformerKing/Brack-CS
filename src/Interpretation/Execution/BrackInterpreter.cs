using Brack.Data;
using System;
using System.IO;

namespace Brack.Interpretation
{
    /// <summary>
    /// A class for managing a Brack runtime enviornment.
    /// </summary>
    public class BrackInterpreter
    {
        #region Static Utility Methods
        /// <summary>
        /// Execute the given raw Brack.
        /// </summary>
        /// <param name="r">The current RAM.</param>
        /// <param name="brack">The raw Brack.</param>
        /// <returns>The returned result of this execution.</returns>
        public static object Execute(RAM r, object[][] brack)
        {
            r.PushNewLocalMemory();
            foreach (var s in brack)
            {
                var obj = r.GetValue(s);
                if (obj is Return || obj is FlowControl)
                {
                    return obj;
                }
            }
            r.RemoveLastLocalMemory();
            return null;
        }
        #endregion
        #region Private Properties
        private RAM _RAM;
        private BrackParser _BrackParser;
        #endregion
    }
}
