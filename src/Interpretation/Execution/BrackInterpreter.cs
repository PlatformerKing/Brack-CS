using Brack.Data;
using Brack.Interpretation.Conversion;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Brack.Interpretation
{
    /// <summary>
    /// A utility class for managing a Brack runtime.
    /// </summary>
    public static class BrackInterpreter
    {
        #region Static Utility Methods
        /// <summary>
        /// Execute the given raw Brack statement.
        /// </summary>
        /// <param name="r">The current RAM.</param>
        /// <param name="brack">The raw Brack statement.</param>
        /// <returns>The returned result of this execution.</returns>
        public static object Execute(RAM r, object[] brack)
        {
            var obj = r.GetValue(brack);
            if (obj is Return || obj is FlowControl)
            {
                return obj;
            }
            return null;
        }
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
                object v = Execute(r, s);
                if (v != null)
                {
                    return v;
                }
            }
            r.RemoveLastLocalMemory();
            return null;
        }
        /// <summary>
        /// Read the serialized Brack file at the given path.
        /// </summary>
        /// <param name="r">The current RAM.</param>
        /// <param name="path">The path to the file.</param>
        /// <param name="threaded">Should this execution be threaded?</param>
        /// <returns>The returned result of this execution.</returns>
        public static object ExecuteBytes(RAM r, string path, bool threaded = true)
        {
            if (threaded)
            {
                var doneReading = false;
                object ret = null;
                var input = new Queue<object[]>();
                var s = new BrackByteFileStream(path, FileMode.Open);
                Thread readThread = new Thread(() =>
                {
                    while (s.HasNext)
                    {
                        lock (input)
                        {
                            input.Enqueue(s.Next());
                        }
                    }
                    doneReading = true;
                });
                Thread executeThread = new Thread(() =>
                {
                    bool cont = true;
                    while (cont)
                    {
                        object[] cur = null;
                        lock (input)
                        {
                            if (input.Count > 0)
                            {
                                cur = input.Dequeue();
                            }
                            else if (doneReading)
                            {
                                cont = false;
                            }
                        }
                        if (cur != null)
                        {
                            ret = Execute(r, cur);
                            if (ret != null)
                            {
                                cont = false;
                            }
                        }
                    }
                });
                readThread.Start();
                executeThread.Start();
                readThread.Join();
                executeThread.Join();
                return ret;
            }
            else
            {
                return Execute(r, File.ReadAllBytes(path).DeserializeBrack());
            }
        }
        /// <summary>
        /// Read the string Brack file at the given path.
        /// </summary>
        /// <param name="r">The current RAM.</param>
        /// <param name="path">The path to the file.</param>
        /// <param name="threaded">Should this execution be threaded?</param>
        /// <returns>The returned result of this execution.</returns>
        public static object ExecuteRaw(RAM r, string path, bool threaded = true)
        {
            if (threaded)
            {
                var doneReading = false;
                object ret = null;
                var input = new Queue<object[]>();
                var s = new BrackFileStream(path, FileMode.Open);
                Thread readThread = new Thread(() =>
                {
                    while (s.HasNext)
                    {
                        lock (input)
                        {
                            input.Enqueue(s.Next());
                        }
                    }
                    doneReading = true;
                });
                Thread executeThread = new Thread(() =>
                {
                    bool cont = true;
                    while (cont)
                    {
                        object[] cur = null;
                        lock (input)
                        {
                            if (input.Count > 0)
                            {
                                cur = input.Dequeue();
                            }
                            else if (doneReading)
                            {
                                cont = false;
                            }
                        }
                        if (cur != null)
                        {
                            ret = Execute(r, cur);
                            if (ret != null)
                            {
                                cont = false;
                            }
                        }
                    }
                });
                readThread.Start();
                executeThread.Start();
                readThread.Join();
                executeThread.Join();
                return ret;
            }
            else
            {
                return Execute(r, BrackParser.ParseString(File.ReadAllText(path)));
            }
        }
        #endregion
    }
}
